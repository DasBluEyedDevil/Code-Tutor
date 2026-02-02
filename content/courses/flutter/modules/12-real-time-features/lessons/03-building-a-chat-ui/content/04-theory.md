---
type: "THEORY"
title: "Chat Input"
---

The chat input field is where users compose messages. It needs to handle multi-line text, show a send button, and optionally support attachments.

**Key Features:**

- Auto-expanding text field (grows with content)
- Send button that enables when text is entered
- Attachment button for images, files, etc.
- Keyboard actions (send on Enter, new line on Shift+Enter)
- Focus management for accessibility

**Best Practices:**

- Limit max lines to prevent input from taking over screen
- Show character count for messages with limits
- Disable send while message is being sent
- Clear input after successful send



```dart
class ChatInputField extends StatefulWidget {
  final Function(String text) onSendMessage;
  final Function()? onAttachmentPressed;
  final bool enabled;
  
  const ChatInputField({
    required this.onSendMessage,
    this.onAttachmentPressed,
    this.enabled = true,
    super.key,
  });
  
  @override
  State<ChatInputField> createState() => _ChatInputFieldState();
}

class _ChatInputFieldState extends State<ChatInputField> {
  final TextEditingController _controller = TextEditingController();
  final FocusNode _focusNode = FocusNode();
  bool _hasText = false;
  bool _isSending = false;
  
  @override
  void initState() {
    super.initState();
    _controller.addListener(_onTextChanged);
  }
  
  void _onTextChanged() {
    final hasText = _controller.text.trim().isNotEmpty;
    if (hasText != _hasText) {
      setState(() => _hasText = hasText);
    }
  }
  
  Future<void> _handleSend() async {
    final text = _controller.text.trim();
    if (text.isEmpty || _isSending) return;
    
    setState(() => _isSending = true);
    
    try {
      await widget.onSendMessage(text);
      _controller.clear();
      _focusNode.requestFocus();
    } finally {
      setState(() => _isSending = false);
    }
  }
  
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    
    return Container(
      decoration: BoxDecoration(
        color: theme.colorScheme.surface,
        boxShadow: [
          BoxShadow(
            color: Colors.black.withOpacity(0.05),
            blurRadius: 10,
            offset: const Offset(0, -2),
          ),
        ],
      ),
      child: SafeArea(
        child: Padding(
          padding: const EdgeInsets.symmetric(
            horizontal: 8,
            vertical: 8,
          ),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.end,
            children: [
              // Attachment button
              if (widget.onAttachmentPressed != null)
                IconButton(
                  icon: const Icon(Icons.attach_file),
                  onPressed: widget.enabled
                      ? widget.onAttachmentPressed
                      : null,
                  tooltip: 'Add attachment',
                ),
              
              // Text input
              Expanded(
                child: Container(
                  decoration: BoxDecoration(
                    color: theme.colorScheme.surfaceVariant,
                    borderRadius: BorderRadius.circular(24),
                  ),
                  child: TextField(
                    controller: _controller,
                    focusNode: _focusNode,
                    enabled: widget.enabled,
                    maxLines: 5,
                    minLines: 1,
                    textCapitalization: TextCapitalization.sentences,
                    textInputAction: TextInputAction.newline,
                    keyboardType: TextInputType.multiline,
                    decoration: InputDecoration(
                      hintText: 'Type a message...',
                      border: InputBorder.none,
                      contentPadding: const EdgeInsets.symmetric(
                        horizontal: 16,
                        vertical: 12,
                      ),
                    ),
                    onSubmitted: (_) => _handleSend(),
                  ),
                ),
              ),
              
              const SizedBox(width: 8),
              
              // Send button
              AnimatedContainer(
                duration: const Duration(milliseconds: 200),
                child: Material(
                  color: _hasText && !_isSending
                      ? theme.colorScheme.primary
                      : theme.colorScheme.surfaceVariant,
                  borderRadius: BorderRadius.circular(24),
                  child: InkWell(
                    onTap: _hasText && !_isSending ? _handleSend : null,
                    borderRadius: BorderRadius.circular(24),
                    child: Container(
                      width: 48,
                      height: 48,
                      alignment: Alignment.center,
                      child: _isSending
                          ? SizedBox(
                              width: 24,
                              height: 24,
                              child: CircularProgressIndicator(
                                strokeWidth: 2,
                                color: theme.colorScheme.onSurfaceVariant,
                              ),
                            )
                          : Icon(
                              Icons.send,
                              color: _hasText
                                  ? theme.colorScheme.onPrimary
                                  : theme.colorScheme.onSurfaceVariant,
                            ),
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
  
  @override
  void dispose() {
    _controller.dispose();
    _focusNode.dispose();
    super.dispose();
  }
}
```
