using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace TranslationAtGPT;

/// <summary>
/// ウィンドウキャプチャ機能を提供するサービスクラス
/// </summary>
public static class WindowCaptureService
{
    #region Win32 API宣言

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);

    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private const uint PW_CLIENTONLY = 0x1;
    private const uint PW_RENDERFULLCONTENT = 0x2;

    #endregion

    /// <summary>
    /// キャプチャ可能なウィンドウ一覧を取得
    /// </summary>
    /// <returns>ウィンドウ情報のリスト</returns>
    public static List<WindowInfo> GetCaptureableWindows()
    {
        var windows = new List<WindowInfo>();

        EnumWindows((hWnd, lParam) =>
        {
            // 非表示ウィンドウは除外
            if (!IsWindowVisible(hWnd))
                return true;

            // ウィンドウタイトルを取得
            int length = GetWindowTextLength(hWnd);
            if (length == 0)
                return true;

            var sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            string title = sb.ToString();

            // タイトルが空のウィンドウは除外
            if (string.IsNullOrWhiteSpace(title))
                return true;

            windows.Add(new WindowInfo(title, hWnd));
            return true;
        }, IntPtr.Zero);

        return windows;
    }

    /// <summary>
    /// 指定したウィンドウのスクリーンショットを撮影
    /// PrintWindow APIを使用して背面ウィンドウでもキャプチャ可能
    /// </summary>
    /// <param name="windowHandle">対象ウィンドウハンドル</param>
    /// <returns>キャプチャした画像（Bitmap）</returns>
    public static Bitmap? CaptureWindow(IntPtr windowHandle)
    {
        // ウィンドウの矩形を取得
        if (!GetWindowRect(windowHandle, out RECT rect))
            return null;

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        // サイズが無効な場合は null を返す
        if (width <= 0 || height <= 0)
            return null;

        // ウィンドウのデバイスコンテキストを取得
        IntPtr windowDC = GetDC(windowHandle);
        if (windowDC == IntPtr.Zero)
            return null;

        try
        {
            // 互換性のあるデバイスコンテキストとビットマップを作成
            IntPtr memoryDC = CreateCompatibleDC(windowDC);
            if (memoryDC == IntPtr.Zero)
                return null;

            try
            {
                IntPtr hBitmap = CreateCompatibleBitmap(windowDC, width, height);
                if (hBitmap == IntPtr.Zero)
                    return null;

                try
                {
                    // ビットマップをデバイスコンテキストに選択
                    IntPtr oldBitmap = SelectObject(memoryDC, hBitmap);

                    // PrintWindow APIを使用してウィンドウの内容をキャプチャ
                    // PW_RENDERFULLCONTENT フラグで完全なコンテンツをレンダリング
                    bool printResult = PrintWindow(windowHandle, memoryDC, PW_RENDERFULLCONTENT);

                    // 元のビットマップを復元
                    SelectObject(memoryDC, oldBitmap);

                    if (!printResult)
                    {
                        // PrintWindowが失敗した場合は従来の方法にフォールバック
                        DeleteObject(hBitmap);
                        return FallbackCaptureFromScreen(rect, width, height);
                    }

                    // HBITMAPからBitmapオブジェクトを作成
                    Bitmap bitmap = Image.FromHbitmap(hBitmap);
                    
                    return bitmap;
                }
                finally
                {
                    DeleteObject(hBitmap);
                }
            }
            finally
            {
                DeleteDC(memoryDC);
            }
        }
        finally
        {
            ReleaseDC(windowHandle, windowDC);
        }
    }

    /// <summary>
    /// フォールバック用：従来のCopyFromScreenを使用したキャプチャ
    /// </summary>
    private static Bitmap? FallbackCaptureFromScreen(RECT rect, int width, int height)
    {
        try
        {
            var bitmap = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(width, height));
            }
            return bitmap;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// スクリーンショットを指定ディレクトリに保存
    /// </summary>
    /// <param name="bitmap">保存する画像</param>
    /// <param name="directory">保存先ディレクトリ</param>
    /// <returns>保存したファイルのパス</returns>
    public static string SaveScreenshot(Bitmap bitmap, string directory)
    {
        // ディレクトリが存在しない場合は作成
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // ファイル名を生成（capture_yyyyMMdd_HHmmss.png）
        string filename = $"capture_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        string filepath = Path.Combine(directory, filename);

        // PNG形式で保存
        bitmap.Save(filepath, ImageFormat.Png);

        return filepath;
    }

    /// <summary>
    /// 画像を指定サイズ以下に縮小する（アスペクト比維持）
    /// </summary>
    /// <param name="originalImage">元画像</param>
    /// <param name="maxSize">最大サイズ（縦横の最大値）</param>
    /// <param name="wasResized">縮小が行われたかどうか</param>
    /// <returns>縮小後の画像（縮小不要な場合は元画像のコピー）</returns>
    public static Bitmap ResizeImageIfNeeded(Bitmap originalImage, int maxSize, out bool wasResized)
    {
        wasResized = false;
        int originalWidth = originalImage.Width;
        int originalHeight = originalImage.Height;

        // 画像サイズがmaxSize以下の場合は何もしない
        if (originalWidth <= maxSize && originalHeight <= maxSize)
        {
            return new Bitmap(originalImage);
        }

        wasResized = true;

        // アスペクト比を維持して新しいサイズを計算
        double scale = Math.Min((double)maxSize / originalWidth, (double)maxSize / originalHeight);
        int newWidth = (int)(originalWidth * scale);
        int newHeight = (int)(originalHeight * scale);

        // 新しいBitmapを作成して縮小
        var resizedImage = new Bitmap(newWidth, newHeight);
        using (var graphics = Graphics.FromImage(resizedImage))
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
        }

        return resizedImage;
    }

    /// <summary>
    /// 画像をサムネイルサイズに縮小する
    /// </summary>
    /// <param name="originalImage">元画像</param>
    /// <param name="maxWidth">最大幅</param>
    /// <param name="maxHeight">最大高さ</param>
    /// <returns>サムネイル画像</returns>
    public static Bitmap CreateThumbnail(Bitmap originalImage, int maxWidth, int maxHeight)
    {
        int originalWidth = originalImage.Width;
        int originalHeight = originalImage.Height;

        // アスペクト比を維持して新しいサイズを計算
        double scale = Math.Min((double)maxWidth / originalWidth, (double)maxHeight / originalHeight);
        int newWidth = (int)(originalWidth * scale);
        int newHeight = (int)(originalHeight * scale);

        // 新しいBitmapを作成して縮小
        var thumbnail = new Bitmap(newWidth, newHeight);
        using (var graphics = Graphics.FromImage(thumbnail))
        {
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
        }

        return thumbnail;
    }
}
