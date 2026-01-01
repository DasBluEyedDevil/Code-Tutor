---
type: "EXAMPLE"
title: "Focus Management Implementation"
---


Proper focus handling for keyboard navigation:



```dart
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

// Custom focusable widget with visible focus indicator
class AccessibleCard extends StatefulWidget {
  final String title;
  final String description;
  final VoidCallback onTap;

  const AccessibleCard({
    super.key,
    required this.title,
    required this.description,
    required this.onTap,
  });

  @override
  State<AccessibleCard> createState() => _AccessibleCardState();
}

class _AccessibleCardState extends State<AccessibleCard> {
  final FocusNode _focusNode = FocusNode();
  bool _isFocused = false;

  @override
  void dispose() {
    _focusNode.dispose();
    super.dispose();
  }

  void _handleKeyEvent(KeyEvent event) {
    if (event is KeyDownEvent) {
      if (event.logicalKey == LogicalKeyboardKey.enter ||
          event.logicalKey == LogicalKeyboardKey.space) {
        widget.onTap();
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Focus(
      focusNode: _focusNode,
      onFocusChange: (focused) {
        setState(() => _isFocused = focused);
      },
      onKeyEvent: (node, event) {
        _handleKeyEvent(event);
        return KeyEventResult.handled;
      },
      child: GestureDetector(
        onTap: widget.onTap,
        child: AnimatedContainer(
          duration: const Duration(milliseconds: 150),
          decoration: BoxDecoration(
            border: Border.all(
              color: _isFocused ? Colors.blue : Colors.grey.shade300,
              width: _isFocused ? 3 : 1,
            ),
            borderRadius: BorderRadius.circular(8),
            // Visible focus indicator
            boxShadow: _isFocused
                ? [
                    BoxShadow(
                      color: Colors.blue.withValues(alpha: 0.3),
                      blurRadius: 8,
                      spreadRadius: 2,
                    ),
                  ]
                : null,
          ),
          child: Padding(
            padding: const EdgeInsets.all(16),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  widget.title,
                  style: const TextStyle(
                    fontSize: 18,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 8),
                Text(widget.description),
              ],
            ),
          ),
        ),
      ),
    );
  }
}

// Focus traversal group for logical grouping
class AccessibleForm extends StatelessWidget {
  const AccessibleForm({super.key});

  @override
  Widget build(BuildContext context) {
    return FocusTraversalGroup(
      policy: OrderedTraversalPolicy(),
      child: Column(
        children: [
          // Fields are traversed in order
          FocusTraversalOrder(
            order: const NumericFocusOrder(1),
            child: TextFormField(
              decoration: const InputDecoration(labelText: 'First Name'),
            ),
          ),
          const SizedBox(height: 16),
          FocusTraversalOrder(
            order: const NumericFocusOrder(2),
            child: TextFormField(
              decoration: const InputDecoration(labelText: 'Last Name'),
            ),
          ),
          const SizedBox(height: 16),
          FocusTraversalOrder(
            order: const NumericFocusOrder(3),
            child: TextFormField(
              decoration: const InputDecoration(labelText: 'Email'),
              keyboardType: TextInputType.emailAddress,
            ),
          ),
          const SizedBox(height: 24),
          FocusTraversalOrder(
            order: const NumericFocusOrder(4),
            child: ElevatedButton(
              onPressed: () {},
              child: const Text('Submit'),
            ),
          ),
        ],
      ),
    );
  }
}

// Modal dialog with focus trap
class AccessibleDialog extends StatefulWidget {
  final String title;
  final String content;
  final VoidCallback onConfirm;
  final VoidCallback onCancel;

  const AccessibleDialog({
    super.key,
    required this.title,
    required this.content,
    required this.onConfirm,
    required this.onCancel,
  });

  @override
  State<AccessibleDialog> createState() => _AccessibleDialogState();
}

class _AccessibleDialogState extends State<AccessibleDialog> {
  final FocusScopeNode _focusScopeNode = FocusScopeNode();

  @override
  void dispose() {
    _focusScopeNode.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Dialog(
      child: FocusScope(
        node: _focusScopeNode,
        autofocus: true,
        child: KeyboardListener(
          focusNode: FocusNode(),
          onKeyEvent: (event) {
            // Close on Escape
            if (event is KeyDownEvent &&
                event.logicalKey == LogicalKeyboardKey.escape) {
              widget.onCancel();
            }
          },
          child: Padding(
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
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                const SizedBox(height: 16),
                Text(widget.content),
                const SizedBox(height: 24),
                Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    TextButton(
                      onPressed: widget.onCancel,
                      child: const Text('Cancel'),
                    ),
                    const SizedBox(width: 16),
                    ElevatedButton(
                      autofocus: true, // Focus first action
                      onPressed: widget.onConfirm,
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

// Skip link for screen reader users
class SkipToContentLink extends StatelessWidget {
  final FocusNode contentFocusNode;

  const SkipToContentLink({super.key, required this.contentFocusNode});

  @override
  Widget build(BuildContext context) {
    return Semantics(
      link: true,
      label: 'Skip to main content',
      child: Focus(
        child: Builder(
          builder: (context) {
            final isFocused = Focus.of(context).hasFocus;
            // Only visible when focused
            return Visibility(
              visible: isFocused,
              maintainSize: false,
              child: TextButton(
                onPressed: () {
                  contentFocusNode.requestFocus();
                },
                child: const Text('Skip to main content'),
              ),
            );
          },
        ),
      ),
    );
  }
}
```
