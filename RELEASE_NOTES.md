# リリースノート

## Version 1.0.0 (2025-11-16)

### 🎉 初回リリース

TranslationAtGPT の最初の公式リリースです。基本的なウィンドウキャプチャと翻訳機能を実装しました。

### ✨ 主な機能

#### ウィンドウキャプチャ
- Windows上で動作している任意のウィンドウのスクリーンショット撮影
- ウィンドウ一覧の取得と選択機能
- キャプチャ画像のサムネイル表示

#### 翻訳機能
- OpenAI GPT-4o-mini Vision API を使用したOCR＋翻訳
- カスタマイズ可能なプロンプト設定
- 追加プロンプト入力による翻訳調整

#### 画像処理
- 大きな画像の自動縮小（2000px以上の場合）
- キャプチャ画像の自動保存（capturesディレクトリ）
- PNG形式での保存

#### その他
- タイムスタンプ付きログ表示（最新5件）
- settings.iniファイルによる設定管理
- 直感的なUI

### 📋 技術仕様

- **開発言語**: C# 12.0
- **フレームワーク**: .NET 8.0 (net8.0-windows)
- **UIフレームワーク**: Windows Forms
- **対応OS**: Windows x64
- **使用API**: OpenAI API (gpt-4o-mini)
- **依存パッケージ**: OpenAI SDK 2.7.0

### 📝 設定ファイル

`settings.ini.example` を `settings.ini` にコピーして以下を設定：

```ini
[OpenAI]
api_key=your-api-key-here
default_prompt=以下の画像内から英語テキストのみ抽出し、まず原文をそのまま出力し、その下に日本語翻訳を記載してください。
```

### ⚠️ 注意事項

- OpenAI APIキーが必要です
- インターネット接続が必要です
- API使用には従量課金が発生します
- Windows専用アプリケーションです

### 🔮 今後の予定

Version 2.0以降で以下の機能を検討中：
- モデル選択機能（GPT-4o等）
- ホットキー登録機能
- 翻訳履歴保存機能
- ダークモード対応
- 自動定期キャプチャ機能

### 📄 ドキュメント

- [README.md](README.md) - セットアップと使い方
- [docs/要件定義.md](docs/要件定義.md) - 要件定義書
- [docs/基本設計.md](docs/基本設計.md) - 基本設計書
- [docs/UI設計.md](docs/UI設計.md) - UI設計書
- [src/TranslationAtGPT/README.md](src/TranslationAtGPT/README.md) - 開発者向けドキュメント

### 🐛 既知の問題

現時点で既知の重大な問題はありません。

---

## リリース履歴

- **1.0.0** (2025-11-16) - 初回リリース
