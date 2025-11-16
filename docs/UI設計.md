# TranslationAtGPT - UI設計書

## UI Components（実装済み）

### 上段エリア
- **ComboBox**: `windowComboBox` - ウィンドウ選択用ドロップダウン
- **Button**: `selectExeButton` - "更新" ボタン（ウィンドウ一覧を更新）
- **Button**: `captureButton` - "キャプチャ実行" ボタン

### 中段エリア
- **Label**: "追加プロンプト:"
- **TextBox**: `promptTextBox` - 追加プロンプト入力欄（maxLength=200）

### 中央エリア（左）
- **Label**: "サムネイル:"
- **PictureBox**: `thumbnailPictureBox` - キャプチャ画像のサムネイル表示

### 中央エリア（右）
- **Label**: "OCR+翻訳結果:"
- **TextBox**: `resultTextBox` - 翻訳結果表示（ReadOnly, Multiline, ScrollBars=Vertical）

### 下段エリア
- **TextBox**: `logTextBox` - ログ表示（ReadOnly, Multiline, 最新5行）

## Layout Rules
- **Top**: ComboBox + 更新ボタン + キャプチャ実行ボタン（横並び）
- **Middle**: 追加プロンプト入力欄（1行）
- **Center-Left**: サムネイル表示（固定サイズ）
- **Center-Right**: 翻訳結果表示（可変サイズ、ウィンドウに追従）
- **Bottom**: ログ表示（固定高さ、自動スクロール）

## ウィンドウプロパティ
- **タイトル**: "TranslationAtGPT"
- **最小サイズ**: 600×400
- **サイズ変更**: 可能
- **最大化**: 無効
- **レイアウト**: TableLayoutPanel使用

## UI操作フロー
1. アプリケーション起動
2. 「更新」ボタンをクリックしてウィンドウ一覧を取得
3. ComboBoxから翻訳対象のウィンドウを選択
4. （オプション）追加プロンプトを入力
5. 「キャプチャ実行」ボタンをクリック
6. サムネイルにキャプチャ画像が表示される
7. 翻訳結果が表示される
8. ログエリアに処理状況が表示される
