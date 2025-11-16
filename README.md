# TranslationAtGPT
画面のスクリーンショットを撮影⇒GPTでOCR＆翻訳を自動で行ってくれるWindows用プログラム

## セットアップ

### 設定ファイルの準備

1. `settings.ini.example` を `settings.ini` にコピー
2. `settings.ini` を編集して、以下の項目を設定：
   - `api_key`: OpenAI APIキーを設定
   - `default_prompt`: 翻訳時のデフォルトプロンプトを設定（任意）

```ini
[OpenAI]
api_key=sk-xxxxxxxxxxxxxxxxxxxx
default_prompt=次の画像内の英語テキストを読み取り、日本語に翻訳してください。ゲーム内のテキストであることを考慮し、自然な日本語にしてください。
```

**注意**: `settings.ini` ファイルは `.gitignore` に含まれているため、Gitにコミットされません。APIキーを安全に保護してください。

## 使い方

1. アプリケーションを起動
2. 「更新」ボタンでウィンドウ一覧を取得
3. 翻訳したいウィンドウを選択
4. （オプション）追加プロンプトを入力
5. 「キャプチャ実行」ボタンをクリック
6. 翻訳結果が表示される
