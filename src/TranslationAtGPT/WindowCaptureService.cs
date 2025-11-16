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

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

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

        // スクリーンショットを撮影
        var bitmap = new Bitmap(width, height);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(width, height));
        }

        return bitmap;
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
}
