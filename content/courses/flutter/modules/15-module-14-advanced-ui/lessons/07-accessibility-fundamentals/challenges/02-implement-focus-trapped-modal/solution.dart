import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class AccessibleModal extends StatefulWidget {
  final String title;
  final String content;
  final VoidCallback onClose;
  final VoidCallback onConfirm;

  const AccessibleModal({
    super.key,
    required this.title,
    required this.content,
    required this.onClose,
    required this.onConfirm,
  });

  @override
  State<AccessibleModal> createState() => _AccessibleModalState();
}

class _AccessibleModalState extends State<AccessibleModal> {
  final FocusScopeNode _focusScopeNode = FocusScopeNode();
  final FocusNode _keyboardListenerNode = FocusNode();

  @override
  void dispose() {
    _focusScopeNode.dispose();
    _keyboardListenerNode.dispose();
    super.dispose();
  }

  void _handleKeyEvent(KeyEvent event) {
    if (event is KeyDownEvent &&
        event.logicalKey == LogicalKeyboardKey.escape) {
      widget.onClose();
    }
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      child: FocusScope(
        node: _focusScopeNode,
        autofocus: true,
        child: KeyboardListener(
          focusNode: _keyboardListenerNode,
          onKeyEvent: _handleKeyEvent,
          child: Container(
            constraints: const BoxConstraints(maxWidth: 400),
            padding: const EdgeInsets.all(24),
            child: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // Title with heading semantics
                Semantics(
                  header: true,
                  child: Text(
                    widget.title,
                    style: const TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                const SizedBox(height: 16),
                Text(
                  widget.content,
                  style: const TextStyle(fontSize: 16),
                ),
                const SizedBox(height: 24),
                Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    // Close button
                    OutlinedButton(
                      onPressed: widget.onClose,
                      style: OutlinedButton.styleFrom(
                        minimumSize: const Size(48, 48),
                      ),
                      child: const Text('Cancel'),
                    ),
                    const SizedBox(width: 16),
                    // Confirm button - auto-focused
                    ElevatedButton(
                      autofocus: true,
                      onPressed: widget.onConfirm,
                      style: ElevatedButton.styleFrom(
                        minimumSize: const Size(48, 48),
                      ),
                      child: const Text('Confirm'),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}