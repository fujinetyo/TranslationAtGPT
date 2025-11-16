namespace TranslationAtGPT;

/// <summary>
/// アプリケーション設定を管理するクラス
/// </summary>
public class Settings
{
    /// <summary>
    /// OpenAI APIキー
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// デフォルトプロンプト
    /// </summary>
    public string DefaultPrompt { get; set; } = string.Empty;

    /// <summary>
    /// settings.iniファイルから設定を読み込む
    /// </summary>
    /// <param name="iniFilePath">INIファイルのパス</param>
    /// <returns>読み込んだ設定</returns>
    public static Settings LoadFromIni(string iniFilePath)
    {
        var settings = new Settings();

        if (!File.Exists(iniFilePath))
        {
            throw new FileNotFoundException($"設定ファイルが見つかりません: {iniFilePath}");
        }

        string? currentSection = null;
        var lines = File.ReadAllLines(iniFilePath);

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 空行やコメント行をスキップ
            if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith(";") || trimmedLine.StartsWith("#"))
            {
                continue;
            }

            // セクション名の処理
            if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
            {
                currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                continue;
            }

            // キー=値の処理
            var separatorIndex = trimmedLine.IndexOf('=');
            if (separatorIndex > 0)
            {
                var key = trimmedLine.Substring(0, separatorIndex).Trim();
                var value = trimmedLine.Substring(separatorIndex + 1).Trim();

                // OpenAIセクションの設定を読み込み
                if (currentSection == "OpenAI")
                {
                    if (key == "api_key")
                    {
                        settings.ApiKey = value;
                    }
                    else if (key == "default_prompt")
                    {
                        settings.DefaultPrompt = value;
                    }
                }
            }
        }

        return settings;
    }
}
