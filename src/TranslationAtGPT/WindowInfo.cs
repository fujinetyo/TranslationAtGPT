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

    /// <summary>
    /// ウィンドウが最小化されているかどうか
    /// </summary>
    public bool IsMinimized { get; set; }

    public WindowInfo(string title, IntPtr handle, bool isMinimized = false)
    {
        Title = title;
        Handle = handle;
        IsMinimized = isMinimized;
    }

    /// <summary>
    /// ComboBoxでの表示用文字列
    /// </summary>
    public override string ToString()
    {
        // 最小化されている場合は[最小化]マークを付ける
        return IsMinimized ? $"{Title} [最小化]" : Title;
    }
}
