namespace TranslationAtGPT;

partial class MainForm
{
    /// <summary>
    ///  必要なデザイナー変数
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  使用中のリソースをすべてクリーンアップ
    /// </summary>
    /// <param name="disposing">マネージド リソースを破棄する場合は true、それ以外の場合は false</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows フォーム デザイナーで生成されたコード

    /// <summary>
    ///  デザイナー サポートに必要なメソッド - このメソッドの内容を
    ///  コード エディターで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
        // UI コンポーネントの初期化
        exePathTextBox = new TextBox();
        selectExeButton = new Button();
        captureButton = new Button();
        promptTextBox = new TextBox();
        resultTextBox = new TextBox();
        logTextBox = new TextBox();
        tableLayoutPanel = new TableLayoutPanel();
        topPanel = new Panel();
        promptLabel = new Label();
        resultLabel = new Label();

        tableLayoutPanel.SuspendLayout();
        topPanel.SuspendLayout();
        SuspendLayout();

        // 
        // topPanel - 上段：ExePathTextBox + ボタン配置
        // 
        topPanel.Controls.Add(captureButton);
        topPanel.Controls.Add(selectExeButton);
        topPanel.Controls.Add(exePathTextBox);
        topPanel.Dock = DockStyle.Fill;
        topPanel.Location = new Point(3, 3);
        topPanel.Name = "topPanel";
        topPanel.Size = new Size(794, 34);
        topPanel.TabIndex = 0;

        // 
        // exePathTextBox - 対象EXEパス表示（ReadOnly）
        // 
        exePathTextBox.Location = new Point(3, 6);
        exePathTextBox.Name = "exePathTextBox";
        exePathTextBox.ReadOnly = true;
        exePathTextBox.Size = new Size(500, 23);
        exePathTextBox.TabIndex = 0;
        exePathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        // 
        // selectExeButton - 参照...ボタン
        // 
        selectExeButton.Location = new Point(509, 4);
        selectExeButton.Name = "selectExeButton";
        selectExeButton.Size = new Size(100, 27);
        selectExeButton.TabIndex = 1;
        selectExeButton.Text = "参照...";
        selectExeButton.UseVisualStyleBackColor = true;
        selectExeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        // 
        // captureButton - キャプチャ実行ボタン
        // 
        captureButton.Location = new Point(615, 4);
        captureButton.Name = "captureButton";
        captureButton.Size = new Size(150, 27);
        captureButton.TabIndex = 2;
        captureButton.Text = "キャプチャ実行";
        captureButton.UseVisualStyleBackColor = true;
        captureButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        // 
        // promptLabel - プロンプト入力ラベル
        // 
        promptLabel.AutoSize = true;
        promptLabel.Location = new Point(3, 43);
        promptLabel.Name = "promptLabel";
        promptLabel.Size = new Size(120, 15);
        promptLabel.TabIndex = 1;
        promptLabel.Text = "追加プロンプト:";

        // 
        // promptTextBox - 追加プロンプト入力
        // 
        promptTextBox.Dock = DockStyle.Fill;
        promptTextBox.Location = new Point(3, 61);
        promptTextBox.MaxLength = 200;
        promptTextBox.Name = "promptTextBox";
        promptTextBox.Size = new Size(794, 23);
        promptTextBox.TabIndex = 3;

        // 
        // resultLabel - OCR+翻訳結果ラベル
        // 
        resultLabel.AutoSize = true;
        resultLabel.Location = new Point(3, 87);
        resultLabel.Name = "resultLabel";
        resultLabel.Size = new Size(100, 15);
        resultLabel.TabIndex = 4;
        resultLabel.Text = "OCR+翻訳結果:";

        // 
        // resultTextBox - OCR+翻訳結果表示（ReadOnly、複数行）
        // 
        resultTextBox.Dock = DockStyle.Fill;
        resultTextBox.Location = new Point(3, 105);
        resultTextBox.Multiline = true;
        resultTextBox.Name = "resultTextBox";
        resultTextBox.ReadOnly = true;
        resultTextBox.ScrollBars = ScrollBars.Vertical;
        resultTextBox.Size = new Size(794, 250);
        resultTextBox.TabIndex = 4;

        // 
        // logTextBox - ログ表示（ReadOnly、複数行、固定5行）
        // 
        logTextBox.Dock = DockStyle.Fill;
        logTextBox.Location = new Point(3, 361);
        logTextBox.Multiline = true;
        logTextBox.Name = "logTextBox";
        logTextBox.ReadOnly = true;
        logTextBox.ScrollBars = ScrollBars.Vertical;
        logTextBox.Size = new Size(794, 86);
        logTextBox.TabIndex = 5;

        // 
        // tableLayoutPanel - レイアウト管理用
        // 
        tableLayoutPanel.ColumnCount = 1;
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel.Controls.Add(topPanel, 0, 0);
        tableLayoutPanel.Controls.Add(promptLabel, 0, 1);
        tableLayoutPanel.Controls.Add(promptTextBox, 0, 2);
        tableLayoutPanel.Controls.Add(resultLabel, 0, 3);
        tableLayoutPanel.Controls.Add(resultTextBox, 0, 4);
        tableLayoutPanel.Controls.Add(logTextBox, 0, 5);
        tableLayoutPanel.Dock = DockStyle.Fill;
        tableLayoutPanel.Location = new Point(0, 0);
        tableLayoutPanel.Name = "tableLayoutPanel";
        tableLayoutPanel.RowCount = 6;
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
        tableLayoutPanel.Size = new Size(800, 450);
        tableLayoutPanel.TabIndex = 0;

        // 
        // MainForm - メインフォーム
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(tableLayoutPanel);
        MaximizeBox = false;
        MinimumSize = new Size(600, 400);
        Name = "MainForm";
        Text = "TranslationAtGPT";
        tableLayoutPanel.ResumeLayout(false);
        tableLayoutPanel.PerformLayout();
        topPanel.ResumeLayout(false);
        topPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    // UI コンポーネントのフィールド宣言
    private TextBox exePathTextBox;
    private Button selectExeButton;
    private Button captureButton;
    private TextBox promptTextBox;
    private TextBox resultTextBox;
    private TextBox logTextBox;
    private TableLayoutPanel tableLayoutPanel;
    private Panel topPanel;
    private Label promptLabel;
    private Label resultLabel;
}
