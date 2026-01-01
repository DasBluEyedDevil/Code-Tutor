---
type: "EXAMPLE"
title: "Custom Focus Indicators"
---


Implementing visible, accessible focus indicators:



```dart
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

/// A reusable focus indicator wrapper
/// Adds a visible focus ring around any widget
class FocusIndicator extends StatefulWidget {
  final Widget child;
  final VoidCallback? onTap;
  final Color focusColor;
  final double focusBorderWidth;
  final BorderRadius? borderRadius;
  final bool autofocus;

  const FocusIndicator({
    super.key,
    required this.child,
    this.onTap,
    this.focusColor = Colors.blue,
    this.focusBorderWidth = 3.0,
    this.borderRadius,
    this.autofocus = false,
  });

  @override
  State<FocusIndicator> createState() => _FocusIndicatorState();
}

class _FocusIndicatorState extends State<FocusIndicator> {
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
        widget.onTap?.call();
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final effectiveBorderRadius = widget.borderRadius ?? BorderRadius.circular(8);
    
    return Focus(
      focusNode: _focusNode,
      autofocus: widget.autofocus,
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
            borderRadius: effectiveBorderRadius,
            border: Border.all(
              color: _isFocused ? widget.focusColor : Colors.transparent,
              width: widget.focusBorderWidth,
            ),
            // Add outer glow for extra visibility
            boxShadow: _isFocused
                ? [
                    BoxShadow(
                      color: widget.focusColor.withValues(alpha: 0.4),
                      blurRadius: 8,
                      spreadRadius: 2,
                    ),
                  ]
                : null,
          ),
          child: widget.child,
        ),
      ),
    );
  }
}

/// Custom card with built-in focus indicator
class AccessibleCard extends StatefulWidget {
  final String title;
  final String subtitle;
  final IconData icon;
  final VoidCallback onTap;

  const AccessibleCard({
    super.key,
    required this.title,
    required this.subtitle,
    required this.icon,
    required this.onTap,
  });

  @override
  State<AccessibleCard> createState() => _AccessibleCardState();
}

class _AccessibleCardState extends State<AccessibleCard> {
  final FocusNode _focusNode = FocusNode();
  bool _isFocused = false;
  bool _isHovered = false;

  @override
  void dispose() {
    _focusNode.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final colorScheme = theme.colorScheme;
    
    return Semantics(
      button: true,
      label: '${widget.title}. ${widget.subtitle}',
      child: Focus(
        focusNode: _focusNode,
        onFocusChange: (focused) => setState(() => _isFocused = focused),
        onKeyEvent: (node, event) {
          if (event is KeyDownEvent &&
              (event.logicalKey == LogicalKeyboardKey.enter ||
               event.logicalKey == LogicalKeyboardKey.space)) {
            widget.onTap();
            return KeyEventResult.handled;
          }
          return KeyEventResult.ignored;
        },
        child: MouseRegion(
          onEnter: (_) => setState(() => _isHovered = true),
          onExit: (_) => setState(() => _isHovered = false),
          child: GestureDetector(
            onTap: widget.onTap,
            child: AnimatedContainer(
              duration: const Duration(milliseconds: 200),
              padding: const EdgeInsets.all(16),
              decoration: BoxDecoration(
                color: _isHovered
                    ? colorScheme.surfaceContainerHighest
                    : colorScheme.surface,
                borderRadius: BorderRadius.circular(12),
                border: Border.all(
                  color: _isFocused
                      ? colorScheme.primary
                      : colorScheme.outline.withValues(alpha: 0.3),
                  width: _isFocused ? 3 : 1,
                ),
                boxShadow: [
                  if (_isFocused)
                    BoxShadow(
                      color: colorScheme.primary.withValues(alpha: 0.3),
                      blurRadius: 12,
                      spreadRadius: 2,
                    ),
                  if (_isHovered && !_isFocused)
                    BoxShadow(
                      color: Colors.black.withValues(alpha: 0.1),
                      blurRadius: 8,
                      offset: const Offset(0, 2),
                    ),
                ],
              ),
              child: Row(
                children: [
                  Container(
                    padding: const EdgeInsets.all(12),
                    decoration: BoxDecoration(
                      color: colorScheme.primaryContainer,
                      borderRadius: BorderRadius.circular(8),
                    ),
                    child: Icon(
                      widget.icon,
                      color: colorScheme.onPrimaryContainer,
                    ),
                  ),
                  const SizedBox(width: 16),
                  Expanded(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          widget.title,
                          style: theme.textTheme.titleMedium,
                        ),
                        Text(
                          widget.subtitle,
                          style: theme.textTheme.bodyMedium?.copyWith(
                            color: colorScheme.onSurfaceVariant,
                          ),
                        ),
                      ],
                    ),
                  ),
                  Icon(
                    Icons.chevron_right,
                    color: colorScheme.onSurfaceVariant,
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

/// Theme extension for consistent focus styling
class AccessibleFocusTheme extends ThemeExtension<AccessibleFocusTheme> {
  final Color focusColor;
  final double focusBorderWidth;
  final double focusBlurRadius;

  const AccessibleFocusTheme({
    this.focusColor = Colors.blue,
    this.focusBorderWidth = 3.0,
    this.focusBlurRadius = 8.0,
  });

  @override
  AccessibleFocusTheme copyWith({
    Color? focusColor,
    double? focusBorderWidth,
    double? focusBlurRadius,
  }) {
    return AccessibleFocusTheme(
      focusColor: focusColor ?? this.focusColor,
      focusBorderWidth: focusBorderWidth ?? this.focusBorderWidth,
      focusBlurRadius: focusBlurRadius ?? this.focusBlurRadius,
    );
  }

  @override
  AccessibleFocusTheme lerp(ThemeExtension<AccessibleFocusTheme>? other, double t) {
    if (other is! AccessibleFocusTheme) return this;
    return AccessibleFocusTheme(
      focusColor: Color.lerp(focusColor, other.focusColor, t)!,
      focusBorderWidth: lerpDouble(focusBorderWidth, other.focusBorderWidth, t)!,
      focusBlurRadius: lerpDouble(focusBlurRadius, other.focusBlurRadius, t)!,
    );
  }
}

double? lerpDouble(double? a, double? b, double t) {
  if (a == null || b == null) return null;
  return a + (b - a) * t;
}
```
