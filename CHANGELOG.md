# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2025-11-16

### Added
- ウィンドウ一覧の取得と選択機能
- 選択したウィンドウのスクリーンショット撮影機能
- OpenAI GPT-4o-mini Vision API を使用したOCR＋翻訳機能
- キャプチャ画像のサムネイル表示機能
- 追加プロンプト入力機能
- settings.iniファイルによる設定管理（APIキー、デフォルトプロンプト）
- タイムスタンプ付きログ表示機能（最新5件）
- キャプチャ画像の自動保存機能（capturesディレクトリ）
- 大きな画像の自動縮小処理（2000px以上）
- Windows Forms UIの実装
- アプリケーションアイコンの設定
- 完全な日本語ドキュメント（README、要件定義、基本設計、UI設計）

### Technical Details
- C# 12.0 / .NET 8.0 (net8.0-windows)
- Windows Forms UI framework
- OpenAI SDK 2.7.0
- TableLayoutPanel によるレスポンシブレイアウト

[1.0.0]: https://github.com/fujinetyo/TranslationAtGPT/releases/tag/v1.0.0
