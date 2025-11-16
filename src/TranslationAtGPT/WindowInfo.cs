namespace TranslationAtGPT;

/// <summary>
/// ウィンドウ情報を保持するクラス
/// </summary>
public class WindowInfo
{
    /// <summary>
    /// ウィンドウタイトル
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// ウィンドウハンドル
    /// </summary>
    public IntPtr Handle { get; set; }

    public WindowInfo(string title, IntPtr handle)
    {
        Title = title;
        Handle = handle;
    }

    /// <summary>
    /// ComboBoxでの表示用文字列
    /// </summary>
    public override string ToString()
    {
        return Title;
    }
}
