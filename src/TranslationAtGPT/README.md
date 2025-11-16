# TranslationAtGPT - WinFormsアプリケーション

## 概要
ゲーム画面やアプリケーションのスクリーンショットを取得し、OpenAI GPT-4o mini Vision APIでOCR＋翻訳を行うWindowsアプリケーション。

Version 1.0 では、基本的なウィンドウキャプチャと翻訳機能を完全実装しています。

## プロジェクト構成

```
src/TranslationAtGPT/
├── MainForm.cs              # メインフォームのロジック（現在は未実装）
├── MainForm.Designer.cs     # UI部品の配置とレイアウト定義
├── MainForm.resx           # リソースファイル
├── Program.cs              # アプリケーションのエントリポイント
└── TranslationAtGPT.csproj # プロジェクト設定ファイル
```

## UI構成

### レイアウト
```
┌─────────────────────────────────────────────────────────┐
│ TranslationAtGPT                                    [_][X]│
├─────────────────────────────────────────────────────────┤
│ ┌─────────────────────────┐ [参照...] [キャプチャ実行]  │ ← 上段
│ │ ExePathTextBox (ReadOnly)│                            │
│ └─────────────────────────┘                            │
├─────────────────────────────────────────────────────────┤
│ 追加プロンプト:                                          │ ← 中段ラベル
│ ┌─────────────────────────────────────────────────────┐│
│ │ PromptTextBox (max 200文字)                         ││ ← 中段
│ └─────────────────────────────────────────────────────┘│
├─────────────────────────────────────────────────────────┤
│ OCR+翻訳結果:                                           │ ← 中央ラベル
│ ┌─────────────────────────────────────────────────────┐│
│ │                                                      ││
│ │ ResultTextBox (ReadOnly, Multiline)                 ││ ← 中央広領域
│ │ ※ウィンドウサイズに追従して拡大/縮小                   ││   (可変)
│ │                                                      ││
│ └─────────────────────────────────────────────────────┘│
├─────────────────────────────────────────────────────────┤
│ ┌─────────────────────────────────────────────────────┐│
│ │ LogTextBox (ReadOnly, Multiline, 5行表示)           ││ ← 下段
│ │                                                      ││   (固定高さ)
│ └─────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────┘
```

### UIコンポーネント一覧

| コンポーネント名 | 種類 | 説明 | プロパティ |
|---|---|---|---|
| `windowComboBox` | ComboBox | ウィンドウ選択ドロップダウン | DropDownStyle=DropDownList |
| `selectExeButton` | Button | ウィンドウ一覧を更新 | Text="更新" |
| `captureButton` | Button | スクリーンショット取得とOCR実行 | Text="キャプチャ実行" |
| `promptTextBox` | TextBox | APIへの追加プロンプト入力 | MaxLength=200 |
| `thumbnailPictureBox` | PictureBox | キャプチャ画像のサムネイル表示 | SizeMode=Zoom |
| `resultTextBox` | TextBox | OCR+翻訳結果の表示 | ReadOnly=true, Multiline=true, ScrollBars=Vertical |
| `logTextBox` | TextBox | 処理ログの表示 | ReadOnly=true, Multiline=true, ScrollBars=Vertical |

### フォームプロパティ

- **タイトル**: "TranslationAtGPT"
- **最大化ボタン**: 無効 (`MaximizeBox = false`)
- **最小サイズ**: 600×400 (`MinimumSize = new Size(600, 400)`)
- **サイズ変更**: 可能（ResultTextBoxが追従して拡大/縮小）

## ビルド方法

### 必要な環境
- .NET 8.0 SDK
- Windows OS（実行時）

### ビルドコマンド
```bash
cd src/TranslationAtGPT
dotnet restore
dotnet build
```

### 実行
```bash
dotnet run
```

または、ビルド後の実行ファイル：
```bash
bin/Debug/net8.0-windows/TranslationAtGPT.exe
```

## 実装状況（Version 1.0）

### ✅ 完了
- [x] プロジェクト構成の作成
- [x] 全UIコンポーネントの配置
- [x] レイアウトの設定（TableLayoutPanel使用）
- [x] フォームプロパティの設定
- [x] ウィンドウ一覧取得機能
- [x] スクリーンショット取得機能
- [x] OpenAI API通信機能
- [x] OCR＋翻訳処理
- [x] ログ出力機能
- [x] 設定ファイル（ini）の読み込み
- [x] サムネイル表示機能
- [x] 画像自動縮小処理（2000px以上）
- [x] キャプチャ画像の自動保存

### 🔄 今後の拡張予定
- [ ] モデル選択機能（GPT-4o等）
- [ ] ホットキー登録機能
- [ ] 履歴保存機能
- [ ] ダークモード対応

## 注意事項

- Windows専用アプリケーションです（.NET 8.0-windows）
- OpenAI APIキーが必要です（settings.iniに設定）
- インターネット接続が必要です（API通信のため）
- キャプチャした画像は`captures`ディレクトリに保存されます

## 技術仕様

- **開発言語**: C# 12.0
- **フレームワーク**: .NET 8.0 (net8.0-windows)
- **UIフレームワーク**: Windows Forms
- **レイアウト**: TableLayoutPanel
- **ターゲットOS**: Windows x64
