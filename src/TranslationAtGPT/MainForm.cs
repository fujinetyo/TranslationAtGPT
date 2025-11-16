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
    /// キャプチャ実行ボタンクリックイベント：スクリーンショット撮影
    /// </summary>
    private void CaptureButton_Click(object? sender, EventArgs e)
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
            MessageBox.Show($"スクリーンショットを保存しました。\n{savedPath}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            AddLog($"エラー: {ex.Message}");
            MessageBox.Show($"スクリーンショットの撮影に失敗しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
