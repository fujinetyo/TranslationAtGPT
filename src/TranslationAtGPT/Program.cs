namespace TranslationAtGPT;

static class Program
{
    /// <summary>
    ///  アプリケーションのメインエントリポイント
    /// </summary>
    [STAThread]
    static void Main()
    {
        // アプリケーション構成の初期化（高DPI設定等）
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }    
}