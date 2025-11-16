using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace TranslationAtGPT;

/// <summary>
/// OpenAI APIを使用した翻訳サービス
/// </summary>
public class OpenAIService
{
    private readonly string _apiKey;
    private readonly string _defaultPrompt;

    public OpenAIService(string apiKey, string defaultPrompt)
    {
        _apiKey = apiKey;
        _defaultPrompt = defaultPrompt;
    }

    /// <summary>
    /// 画像を翻訳する
    /// </summary>
    /// <param name="image">翻訳対象の画像</param>
    /// <param name="additionalPrompt">追加プロンプト（オプション）</param>
    /// <returns>翻訳結果テキスト</returns>
    public async Task<string> TranslateImageAsync(Image image, string additionalPrompt)
    {
        // プロンプトを構築
        string prompt = _defaultPrompt;
        if (!string.IsNullOrWhiteSpace(additionalPrompt))
        {
            prompt += "\n" + additionalPrompt;
        }

        // 画像をBase64エンコード
        string base64Image = ImageToBase64(image);

        // OpenAI クライアントを作成
        var client = new ChatClient(model: "gpt-4o-mini", apiKey: _apiKey);

        // メッセージを作成
        var messages = new List<ChatMessage>
        {
            new UserChatMessage(
                ChatMessageContentPart.CreateTextPart(prompt),
                ChatMessageContentPart.CreateImagePart(BinaryData.FromBytes(Convert.FromBase64String(base64Image)), "image/png")
            )
        };

        // API呼び出し
        ChatCompletion completion = await client.CompleteChatAsync(messages);

        // レスポンスから翻訳テキストを抽出
        string translatedText = completion.Content[0].Text;

        // 改行コードを正規化（LF → CRLF on Windows）
        translatedText = NormalizeLineEndings(translatedText);

        return translatedText;
    }

    /// <summary>
    /// 改行コードを Environment.NewLine に正規化する
    /// </summary>
    /// <param name="text">正規化対象のテキスト</param>
    /// <returns>正規化されたテキスト</returns>
    private string NormalizeLineEndings(string text)
    {
        // まず既存の CRLF を LF に統一し、その後 Environment.NewLine に変換
        return text
            .Replace("\r\n", "\n")
            .Replace("\r", "\n")
            .Replace("\n", Environment.NewLine);
    }

    /// <summary>
    /// ImageをBase64文字列に変換
    /// </summary>
    private string ImageToBase64(Image image)
    {
        using var ms = new MemoryStream();
        image.Save(ms, ImageFormat.Png);
        byte[] imageBytes = ms.ToArray();
        return Convert.ToBase64String(imageBytes);
    }
}
