---
type: "EXAMPLE"
title: "Building Adaptive Widget Library"
---


Create a complete set of adaptive widgets:



```dart
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

// Helper to check platform
bool _isIOS(BuildContext context) {
  return Theme.of(context).platform == TargetPlatform.iOS;
}

// Adaptive Button
class AdaptiveButton extends StatelessWidget {
  final VoidCallback? onPressed;
  final Widget child;
  final bool filled;

  const AdaptiveButton({
    super.key,
    required this.onPressed,
    required this.child,
    this.filled = true,
  });

  @override
  Widget build(BuildContext context) {
    if (_isIOS(context)) {
      return filled
          ? CupertinoButton.filled(onPressed: onPressed, child: child)
          : CupertinoButton(onPressed: onPressed, child: child);
    }
    return filled
        ? ElevatedButton(onPressed: onPressed, child: child)
        : TextButton(onPressed: onPressed, child: child);
  }
}

// Adaptive Switch
class AdaptiveSwitch extends StatelessWidget {
  final bool value;
  final ValueChanged<bool>? onChanged;

  const AdaptiveSwitch({
    super.key,
    required this.value,
    required this.onChanged,
  });

  @override
  Widget build(BuildContext context) {
    if (_isIOS(context)) {
      return CupertinoSwitch(value: value, onChanged: onChanged);
    }
    return Switch(value: value, onChanged: onChanged);
  }
}

// Adaptive Progress Indicator
class AdaptiveProgressIndicator extends StatelessWidget {
  final double? value; // null for indeterminate

  const AdaptiveProgressIndicator({super.key, this.value});

  @override
  Widget build(BuildContext context) {
    if (_isIOS(context)) {
      return const CupertinoActivityIndicator();
    }
    return value != null
        ? CircularProgressIndicator(value: value)
        : const CircularProgressIndicator();
  }
}

// Adaptive Dialog Helper
class AdaptiveDialogs {
  static Future<bool?> showConfirmation({
    required BuildContext context,
    required String title,
    required String message,
    String confirmText = 'Confirm',
    String cancelText = 'Cancel',
    bool isDestructive = false,
  }) {
    if (_isIOS(context)) {
      return showCupertinoDialog<bool>(
        context: context,
        builder: (context) => CupertinoAlertDialog(
          title: Text(title),
          content: Text(message),
          actions: [
            CupertinoDialogAction(
              onPressed: () => Navigator.pop(context, false),
              child: Text(cancelText),
            ),
            CupertinoDialogAction(
              isDestructiveAction: isDestructive,
              isDefaultAction: !isDestructive,
              onPressed: () => Navigator.pop(context, true),
              child: Text(confirmText),
            ),
          ],
        ),
      );
    }
    return showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: Text(title),
        content: Text(message),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context, false),
            child: Text(cancelText),
          ),
          TextButton(
            onPressed: () => Navigator.pop(context, true),
            style: isDestructive
                ? TextButton.styleFrom(foregroundColor: Colors.red)
                : null,
            child: Text(confirmText),
          ),
        ],
      ),
    );
  }

  static Future<T?> showOptions<T>({
    required BuildContext context,
    required String title,
    required List<AdaptiveDialogOption<T>> options,
  }) {
    if (_isIOS(context)) {
      return showCupertinoModalPopup<T>(
        context: context,
        builder: (context) => CupertinoActionSheet(
          title: Text(title),
          actions: options
              .map((option) => CupertinoActionSheetAction(
                    isDestructiveAction: option.isDestructive,
                    onPressed: () => Navigator.pop(context, option.value),
                    child: Text(option.label),
                  ))
              .toList(),
          cancelButton: CupertinoActionSheetAction(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
        ),
      );
    }
    return showModalBottomSheet<T>(
      context: context,
      builder: (context) => Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          Padding(
            padding: const EdgeInsets.all(16),
            child: Text(title, style: Theme.of(context).textTheme.titleLarge),
          ),
          ...options.map((option) => ListTile(
                title: Text(
                  option.label,
                  style: option.isDestructive
                      ? const TextStyle(color: Colors.red)
                      : null,
                ),
                onTap: () => Navigator.pop(context, option.value),
              )),
          const SizedBox(height: 16),
        ],
      ),
    );
  }
}

class AdaptiveDialogOption<T> {
  final String label;
  final T value;
  final bool isDestructive;

  const AdaptiveDialogOption({
    required this.label,
    required this.value,
    this.isDestructive = false,
  });
}

// Adaptive Text Field
class AdaptiveTextField extends StatelessWidget {
  final TextEditingController? controller;
  final String? placeholder;
  final String? labelText;
  final bool obscureText;
  final TextInputType? keyboardType;
  final ValueChanged<String>? onChanged;

  const AdaptiveTextField({
    super.key,
    this.controller,
    this.placeholder,
    this.labelText,
    this.obscureText = false,
    this.keyboardType,
    this.onChanged,
  });

  @override
  Widget build(BuildContext context) {
    if (_isIOS(context)) {
      return CupertinoTextField(
        controller: controller,
        placeholder: placeholder ?? labelText,
        obscureText: obscureText,
        keyboardType: keyboardType,
        onChanged: onChanged,
        padding: const EdgeInsets.all(12),
      );
    }
    return TextField(
      controller: controller,
      obscureText: obscureText,
      keyboardType: keyboardType,
      onChanged: onChanged,
      decoration: InputDecoration(
        labelText: labelText,
        hintText: placeholder,
        border: const OutlineInputBorder(),
      ),
    );
  }
}

// Usage Example
class AdaptiveWidgetDemo extends StatefulWidget {
  const AdaptiveWidgetDemo({super.key});

  @override
  State<AdaptiveWidgetDemo> createState() => _AdaptiveWidgetDemoState();
}

class _AdaptiveWidgetDemoState extends State<AdaptiveWidgetDemo> {
  bool _notificationsEnabled = true;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Adaptive Widgets')),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text('Notifications'),
                AdaptiveSwitch(
                  value: _notificationsEnabled,
                  onChanged: (value) {
                    setState(() => _notificationsEnabled = value);
                  },
                ),
              ],
            ),
            const SizedBox(height: 24),
            const AdaptiveTextField(
              labelText: 'Email',
              placeholder: 'Enter your email',
              keyboardType: TextInputType.emailAddress,
            ),
            const SizedBox(height: 24),
            AdaptiveButton(
              onPressed: () async {
                final confirmed = await AdaptiveDialogs.showConfirmation(
                  context: context,
                  title: 'Delete Account',
                  message: 'This action cannot be undone.',
                  confirmText: 'Delete',
                  isDestructive: true,
                );
                if (confirmed == true) {
                  // Handle deletion
                }
              },
              child: const Text('Delete Account'),
            ),
            const SizedBox(height: 16),
            const Center(child: AdaptiveProgressIndicator()),
          ],
        ),
      ),
    );
  }
}
```
