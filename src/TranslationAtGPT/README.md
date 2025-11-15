# TranslationAtGPT - WinFormsアプリケーション

## 概要
ゲーム画面のスクリーンショットを取得し、OpenAI GPT-4o mini Vision APIでOCR＋翻訳を行うWindowsアプリケーション。

このバージョンは、UI部品のみを配置した初期バージョンです。

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
| `exePathTextBox` | TextBox | 対象EXEファイルのパス表示 | ReadOnly=true, Anchor=Left\|Right |
| `selectExeButton` | Button | EXE選択ダイアログを開く（未実装） | Text="参照...", Anchor=Right |
| `captureButton` | Button | スクリーンショット取得とOCR実行（未実装） | Text="キャプチャ実行", Anchor=Right |
| `promptTextBox` | TextBox | APIへの追加プロンプト入力 | MaxLength=200, Dock=Fill |
| `resultTextBox` | TextBox | OCR+翻訳結果の表示 | ReadOnly=true, Multiline=true, ScrollBars=Vertical, Dock=Fill |
| `logTextBox` | TextBox | 処理ログの表示 | ReadOnly=true, Multiline=true, ScrollBars=Vertical, Dock=Fill, 固定高さ |

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

## 実装状況

### ✅ 完了
- [x] プロジェクト構成の作成
- [x] 全UIコンポーネントの配置
- [x] レイアウトの設定（TableLayoutPanel使用）
- [x] フォームプロパティの設定
- [x] ビルド確認

### ⏳ 未実装（今後の対応）
- [ ] ファイル選択ダイアログ機能（SelectExeButton）
- [ ] スクリーンショット取得機能（CaptureButton）
- [ ] OpenAI API通信機能
- [ ] OCR＋翻訳処理
- [ ] ログ出力機能
- [ ] 設定ファイル（ini）の読み込み/保存

## 注意事項

- このバージョンはUI表示のみで、ボタンクリック等の動作は実装されていません
- Linux環境ではビルドは可能ですが、実行にはWindows Desktop Runtimeが必要です
- 実際の動作確認はWindows環境で行ってください

## 技術仕様

- **開発言語**: C# 12.0
- **フレームワーク**: .NET 8.0 (net8.0-windows)
- **UIフレームワーク**: Windows Forms
- **レイアウト**: TableLayoutPanel
- **ターゲットOS**: Windows x64
