---
type: "EXAMPLE"
title: "Color and Contrast Best Practices"
---


Implementing accessible color schemes:



```dart
import 'package:flutter/material.dart';

// Accessible color palette
class AccessibleColors {
  // High contrast text colors
  static const Color textPrimary = Color(0xFF1A1A1A);   // Near black
  static const Color textSecondary = Color(0xFF5C5C5C); // Passes AA on white
  static const Color textOnDark = Color(0xFFF5F5F5);    // Near white
  
  // Status colors that work for colorblind users
  static const Color success = Color(0xFF2E7D32);  // Green with good contrast
  static const Color warning = Color(0xFFF57C00);  // Orange
  static const Color error = Color(0xFFC62828);    // Red with good contrast
  static const Color info = Color(0xFF1565C0);     // Blue
  
  // Calculate contrast ratio between two colors
  static double contrastRatio(Color foreground, Color background) {
    final fgLuminance = foreground.computeLuminance();
    final bgLuminance = background.computeLuminance();
    
    final lighter = fgLuminance > bgLuminance ? fgLuminance : bgLuminance;
    final darker = fgLuminance > bgLuminance ? bgLuminance : fgLuminance;
    
    return (lighter + 0.05) / (darker + 0.05);
  }
  
  // Check if contrast meets WCAG AA for normal text
  static bool meetsAA(Color foreground, Color background) {
    return contrastRatio(foreground, background) >= 4.5;
  }
  
  // Check if contrast meets WCAG AAA for normal text
  static bool meetsAAA(Color foreground, Color background) {
    return contrastRatio(foreground, background) >= 7.0;
  }
}

// Status indicator that doesn't rely on color alone
class AccessibleStatusIndicator extends StatelessWidget {
  final StatusType status;
  final String label;

  const AccessibleStatusIndicator({
    super.key,
    required this.status,
    required this.label,
  });

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisSize: MainAxisSize.min,
      children: [
        // Icon + color (not just color)
        Icon(
          _getIcon(),
          color: _getColor(),
          size: 20,
        ),
        const SizedBox(width: 8),
        // Text label (never rely on color alone)
        Text(
          label,
          style: TextStyle(
            color: _getColor(),
            fontWeight: FontWeight.w500,
          ),
        ),
      ],
    );
  }

  IconData _getIcon() {
    switch (status) {
      case StatusType.success:
        return Icons.check_circle;
      case StatusType.warning:
        return Icons.warning;
      case StatusType.error:
        return Icons.error;
      case StatusType.info:
        return Icons.info;
    }
  }

  Color _getColor() {
    switch (status) {
      case StatusType.success:
        return AccessibleColors.success;
      case StatusType.warning:
        return AccessibleColors.warning;
      case StatusType.error:
        return AccessibleColors.error;
      case StatusType.info:
        return AccessibleColors.info;
    }
  }
}

enum StatusType { success, warning, error, info }

// Accessible form validation
class AccessibleTextField extends StatelessWidget {
  final TextEditingController controller;
  final String label;
  final String? errorText;
  final bool hasError;

  const AccessibleTextField({
    super.key,
    required this.controller,
    required this.label,
    this.errorText,
    this.hasError = false,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        TextField(
          controller: controller,
          decoration: InputDecoration(
            labelText: label,
            // Don't rely only on border color for error state
            border: const OutlineInputBorder(),
            errorBorder: const OutlineInputBorder(
              borderSide: BorderSide(color: AccessibleColors.error, width: 2),
            ),
            // Add error icon
            suffixIcon: hasError
                ? const Icon(Icons.error, color: AccessibleColors.error)
                : null,
          ),
        ),
        if (hasError && errorText != null)
          Padding(
            padding: const EdgeInsets.only(top: 8, left: 12),
            child: Row(
              children: [
                const Icon(
                  Icons.error_outline,
                  color: AccessibleColors.error,
                  size: 16,
                ),
                const SizedBox(width: 8),
                Text(
                  errorText!,
                  style: const TextStyle(
                    color: AccessibleColors.error,
                    fontSize: 12,
                  ),
                ),
              ],
            ),
          ),
      ],
    );
  }
}

// Chart with patterns for colorblind users
class AccessibleChartLegend extends StatelessWidget {
  final List<ChartItem> items;

  const AccessibleChartLegend({super.key, required this.items});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: items.map((item) {
        return Padding(
          padding: const EdgeInsets.symmetric(vertical: 4),
          child: Row(
            children: [
              // Use patterns + colors
              Container(
                width: 24,
                height: 24,
                decoration: BoxDecoration(
                  color: item.color,
                  border: Border.all(color: Colors.black54),
                ),
                child: CustomPaint(
                  painter: PatternPainter(item.pattern),
                ),
              ),
              const SizedBox(width: 12),
              Text(item.label),
              const SizedBox(width: 8),
              Text(
                '${item.value}%',
                style: const TextStyle(fontWeight: FontWeight.bold),
              ),
            ],
          ),
        );
      }).toList(),
    );
  }
}

class ChartItem {
  final String label;
  final Color color;
  final PatternType pattern;
  final double value;

  ChartItem({
    required this.label,
    required this.color,
    required this.pattern,
    required this.value,
  });
}

enum PatternType { solid, diagonal, dots, horizontal, vertical }

// Simple pattern painter for chart differentiation
class PatternPainter extends CustomPainter {
  final PatternType pattern;

  PatternPainter(this.pattern);

  @override
  void paint(Canvas canvas, Size size) {
    final paint = Paint()
      ..color = Colors.black26
      ..strokeWidth = 2;

    switch (pattern) {
      case PatternType.solid:
        break;
      case PatternType.diagonal:
        for (var i = -size.width; i < size.width * 2; i += 6) {
          canvas.drawLine(
            Offset(i.toDouble(), 0),
            Offset(i + size.height, size.height),
            paint,
          );
        }
        break;
      case PatternType.dots:
        for (var x = 3.0; x < size.width; x += 8) {
          for (var y = 3.0; y < size.height; y += 8) {
            canvas.drawCircle(Offset(x, y), 2, paint);
          }
        }
        break;
      case PatternType.horizontal:
        for (var y = 4.0; y < size.height; y += 8) {
          canvas.drawLine(Offset(0, y), Offset(size.width, y), paint);
        }
        break;
      case PatternType.vertical:
        for (var x = 4.0; x < size.width; x += 8) {
          canvas.drawLine(Offset(x, 0), Offset(x, size.height), paint);
        }
        break;
    }
  }

  @override
  bool shouldRepaint(covariant CustomPainter oldDelegate) => false;
}
```
