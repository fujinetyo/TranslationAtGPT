# UI Components
- TextBox: ExePathTextBox (ReadOnly, width=auto)
- Button: SelectExeButton ("参照...")
- Button: CaptureButton ("キャプチャ実行")
- TextBox: PromptTextBox (maxLength=200)
- TextBox: ResultTextBox (ReadOnly, Multiline)
- TextBox: LogTextBox (ReadOnly, Multiline, VisibleLines=5)

# Layout Rules
- Top: ExePathTextBox + SelectExeButton + CaptureButton
- Middle: PromptTextBox
- Stretch: ResultTextBox (fills remaining space)
- Bottom: LogTextBox (fixed height, auto-scroll)
