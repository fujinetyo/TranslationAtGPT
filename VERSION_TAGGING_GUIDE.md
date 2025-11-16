# Version 1.0 タグ付けガイド

このドキュメントは、Version 1.0 のリリースタグを作成する手順を説明します。

## 前提条件

- すべてのコードとドキュメントの更新が完了していること
- ビルドが成功すること
- すべての変更がメインブランチにマージされていること

## タグ付け手順

### 1. ローカルリポジトリの更新

```bash
git checkout main
git pull origin main
```

### 2. タグの作成

```bash
# アノテーテッドタグを作成（推奨）
git tag -a v1.0.0 -m "Release Version 1.0.0

初回リリース - 基本的なウィンドウキャプチャと翻訳機能を実装

主な機能:
- ウィンドウ一覧の取得と選択
- スクリーンショット撮影
- OpenAI GPT-4o-mini を使用したOCR＋翻訳
- サムネイル表示
- キャプチャ画像の自動保存
- 設定ファイル（settings.ini）対応

詳細は RELEASE_NOTES.md を参照"
```

### 3. タグの確認

```bash
# タグが作成されたことを確認
git tag -l "v1.0.0"

# タグの詳細を表示
git show v1.0.0
```

### 4. リモートへのプッシュ

```bash
# タグをリモートにプッシュ
git push origin v1.0.0
```

### 5. GitHub Releaseの作成

GitHubのWebインターフェースでリリースを作成：

1. リポジトリページで「Releases」をクリック
2. 「Draft a new release」をクリック
3. 「Choose a tag」で `v1.0.0` を選択
4. Release title: `Version 1.0.0`
5. Release description: `RELEASE_NOTES.md` の内容をコピー
6. 必要に応じてバイナリファイルを添付
7. 「Publish release」をクリック

## リリースバイナリの作成（オプション）

ユーザーが直接ダウンロードできるようにビルド済みバイナリを提供する場合：

```bash
# Releaseビルドを作成
cd src/TranslationAtGPT
dotnet publish -c Release -r win-x64 --self-contained false

# または自己完結型バイナリ（.NET Runtimeを含む）
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

ビルドされたファイルは `bin/Release/net8.0-windows/win-x64/publish/` に出力されます。

これをZIPで圧縮してGitHub Releaseに添付します：

```bash
cd bin/Release/net8.0-windows/win-x64/publish/
zip -r TranslationAtGPT-v1.0.0-win-x64.zip *
```

## 次のバージョンへの準備

タグ付け後、次のバージョンの開発を開始する場合：

1. `TranslationAtGPT.csproj` の `<Version>` を `1.1.0` などに更新
2. `CHANGELOG.md` に `## [Unreleased]` セクションを追加

## トラブルシューティング

### タグを間違えて作成した場合

```bash
# ローカルのタグを削除
git tag -d v1.0.0

# リモートのタグを削除（プッシュ済みの場合）
git push origin :refs/tags/v1.0.0
```

その後、正しいタグを再作成してください。

## バージョン番号の規則

このプロジェクトは [Semantic Versioning](https://semver.org/) に従います：

- **MAJOR version** (1.x.x): 互換性のない変更
- **MINOR version** (x.1.x): 後方互換性のある機能追加
- **PATCH version** (x.x.1): 後方互換性のあるバグ修正

## 参考資料

- [RELEASE_NOTES.md](RELEASE_NOTES.md) - リリースノート
- [CHANGELOG.md](CHANGELOG.md) - 変更履歴
- [Semantic Versioning](https://semver.org/)
- [Git Tag Documentation](https://git-scm.com/book/en/v2/Git-Basics-Tagging)
