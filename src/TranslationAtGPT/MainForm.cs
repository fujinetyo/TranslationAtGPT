namespace TranslationAtGPT;

/// <summary>
/// メインフォーム：ゲーム画面のOCR＋翻訳を行うUIを提供
/// </summary>
public partial class MainForm : Form
{
    private readonly Queue<string> logQueue = new Queue<string>();
    private const int MaxLogCount = 5;

    public MainForm()
    {
        InitializeComponent();
        
        // アイコンを設定
        string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "icon.ico");
        if (File.Exists(iconPath))
        {
            Icon = new Icon(iconPath);
        }
        
        // 初期化時のプレースホルダ設定
        windowComboBox.Items.Add("ウィンドウを選択してください");
        windowComboBox.SelectedIndex = 0;
    }

    /// <summary>
    /// ログをTextBoxに追加（最新5件のみ保持）
    /// </summary>
    private void AddLog(string message)
    {
        string timestampedMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";
        
        logQueue.Enqueue(timestampedMessage);
        
        // 最新5件のみ保持
        while (logQueue.Count > MaxLogCount)
        {
            logQueue.Dequeue();
        }
        
        // ログテキストボックスを更新
        logTextBox.Text = string.Join(Environment.NewLine, logQueue);
    }

    /// <summary>
    /// 更新ボタンクリックイベント：ウィンドウ一覧を取得
    /// </summary>
    private void SelectExeButton_Click(object? sender, EventArgs e)
    {
        try
        {
            AddLog("ウィンドウ一覧取得を開始...");
            
            // ウィンドウ一覧を取得
            var windows = WindowCaptureService.GetCaptureableWindows();
            
            // ComboBoxの内容をクリア
            windowComboBox.Items.Clear();
            
            if (windows.Count == 0)
            {
                windowComboBox.Items.Add("ウィンドウが見つかりませんでした");
                windowComboBox.SelectedIndex = 0;
                AddLog("取得完了: ウィンドウが見つかりませんでした");
            }
            else
            {
                // ウィンドウ情報をComboBoxに追加
                foreach (var window in windows)
                {
                    windowComboBox.Items.Add(window);
                }
                
                windowComboBox.SelectedIndex = 0;
                AddLog($"取得完了: {windows.Count}個のウィンドウを検出");
            }
        }
        catch (Exception ex)
        {
            AddLog($"エラー: {ex.Message}");
            MessageBox.Show($"ウィンドウ一覧の取得に失敗しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// キャプチャ実行ボタンクリックイベント：スクリーンショット撮影＋翻訳
    /// </summary>
    private async void CaptureButton_Click(object? sender, EventArgs e)
    {
        try
        {
            // 選択されたウィンドウ情報を取得
            if (windowComboBox.SelectedItem is not WindowInfo selectedWindow)
            {
                MessageBox.Show("ウィンドウを選択してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            AddLog($"キャプチャ開始: {selectedWindow.Title}");
            
            // スクリーンショットを撮影
            using var bitmap = WindowCaptureService.CaptureWindow(selectedWindow.Handle);
            
            if (bitmap == null)
            {
                AddLog("エラー: ウィンドウのキャプチャに失敗しました");
                MessageBox.Show("ウィンドウのキャプチャに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // capturesディレクトリに保存
            string capturesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "captures");
            string savedPath = WindowCaptureService.SaveScreenshot(bitmap, capturesDir);
            
            AddLog($"保存完了: {Path.GetFileName(savedPath)}");

            // サムネイルを更新
            UpdateThumbnail(bitmap);
            AddLog("[INFO] サムネイルを更新しました");

            // 設定ファイルを読み込み
            string settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.ini");
            Settings settings;
            
            try
            {
                settings = Settings.LoadFromIni(settingsPath);
                AddLog("設定ファイル読み込み完了");
            }
            catch (FileNotFoundException)
            {
                AddLog("エラー: settings.iniファイルが見つかりません");
                MessageBox.Show("settings.iniファイルが見つかりません。\n実行ファイルと同じディレクトリに配置してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // APIキーの検証
            if (string.IsNullOrWhiteSpace(settings.ApiKey))
            {
                AddLog("エラー: APIキーが設定されていません");
                MessageBox.Show("settings.iniにAPIキーが設定されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 画像圧縮処理: 2000px以上の場合は縮小
            bool wasResized;
            using (var resizedImage = WindowCaptureService.ResizeImageIfNeeded(bitmap, 2000, out wasResized))
            {
                if (wasResized)
                {
                    int originalMax = Math.Max(bitmap.Width, bitmap.Height);
                    int newMax = Math.Max(resizedImage.Width, resizedImage.Height);
                    AddLog($"[INFO] 画像を縮小しました: 元サイズ={originalMax}px → 新サイズ={newMax}px");
                }

                // OpenAI APIに翻訳リクエストを送信
                AddLog("OpenAI APIにリクエストを送信中...");
                
                var openAIService = new OpenAIService(settings.ApiKey, settings.DefaultPrompt);
                string additionalPrompt = promptTextBox.Text;
                
                string translatedText = await openAIService.TranslateImageAsync(resizedImage, additionalPrompt);
                
                // 翻訳結果を表示
                resultTextBox.Text = translatedText;
                AddLog("翻訳結果を受信しました");
            }
        }
        catch (MinimizedWindowCaptureException ex)
        {
            // 最小化ウィンドウのキャプチャ失敗を明確に通知
            AddLog($"エラー: ウィンドウが最小化されています");
            MessageBox.Show(ex.Message, "最小化ウィンドウのキャプチャ不可", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            AddLog($"エラー: {ex.Message}");
            MessageBox.Show($"処理に失敗しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// サムネイルを更新
    /// </summary>
    private void UpdateThumbnail(Bitmap originalImage)
    {
        // 既存のサムネイルがあれば破棄
        if (thumbnailPictureBox.Image != null)
        {
            thumbnailPictureBox.Image.Dispose();
            thumbnailPictureBox.Image = null;
        }

        // PictureBoxのサイズに合わせたサムネイルを作成
        var thumbnail = WindowCaptureService.CreateThumbnail(originalImage, thumbnailPictureBox.Width, thumbnailPictureBox.Height);
        thumbnailPictureBox.Image = thumbnail;
    }
}
