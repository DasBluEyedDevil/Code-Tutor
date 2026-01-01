---
type: "EXAMPLE"
title: "Locale-Aware Formatting Widget"
---


A reusable widget that formats values based on current locale:



```dart
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

/// Extension for easy locale access
extension LocaleExtension on BuildContext {
  String get localeString => Localizations.localeOf(this).toString();
  Locale get locale => Localizations.localeOf(this);
}

/// Locale-aware date formatting widget
class LocalizedDate extends StatelessWidget {
  final DateTime date;
  final DateFormatType formatType;
  final TextStyle? style;

  const LocalizedDate({
    super.key,
    required this.date,
    this.formatType = DateFormatType.medium,
    this.style,
  });

  @override
  Widget build(BuildContext context) {
    final locale = context.localeString;
    final formatter = _getFormatter(locale);
    
    return Text(
      formatter.format(date),
      style: style,
    );
  }

  DateFormat _getFormatter(String locale) {
    switch (formatType) {
      case DateFormatType.short:
        return DateFormat.yMd(locale);
      case DateFormatType.medium:
        return DateFormat.yMMMd(locale);
      case DateFormatType.long:
        return DateFormat.yMMMMd(locale);
      case DateFormatType.full:
        return DateFormat.yMMMMEEEEd(locale);
      case DateFormatType.time:
        return DateFormat.jm(locale);
      case DateFormatType.dateTime:
        return DateFormat.yMd(locale).add_jm();
      case DateFormatType.relative:
        return DateFormat.yMd(locale); // Handle separately
    }
  }
}

enum DateFormatType { short, medium, long, full, time, dateTime, relative }

/// Locale-aware currency formatting widget
class LocalizedCurrency extends StatelessWidget {
  final double amount;
  final String currencyCode;
  final bool compact;
  final TextStyle? style;

  const LocalizedCurrency({
    super.key,
    required this.amount,
    required this.currencyCode,
    this.compact = false,
    this.style,
  });

  @override
  Widget build(BuildContext context) {
    final locale = context.localeString;
    
    final formatter = compact
        ? NumberFormat.compactCurrency(
            locale: locale,
            name: currencyCode,
          )
        : NumberFormat.currency(
            locale: locale,
            name: currencyCode,
          );
    
    return Text(
      formatter.format(amount),
      style: style,
    );
  }
}

/// Locale-aware number formatting widget
class LocalizedNumber extends StatelessWidget {
  final num value;
  final NumberFormatType formatType;
  final int? decimalDigits;
  final TextStyle? style;

  const LocalizedNumber({
    super.key,
    required this.value,
    this.formatType = NumberFormatType.decimal,
    this.decimalDigits,
    this.style,
  });

  @override
  Widget build(BuildContext context) {
    final locale = context.localeString;
    final formatter = _getFormatter(locale);
    
    return Text(
      formatter.format(value),
      style: style,
    );
  }

  NumberFormat _getFormatter(String locale) {
    switch (formatType) {
      case NumberFormatType.decimal:
        final fmt = NumberFormat.decimalPattern(locale);
        if (decimalDigits != null) {
          fmt.minimumFractionDigits = decimalDigits!;
          fmt.maximumFractionDigits = decimalDigits!;
        }
        return fmt;
      case NumberFormatType.compact:
        return NumberFormat.compact(locale: locale);
      case NumberFormatType.percent:
        return NumberFormat.percentPattern(locale);
      case NumberFormatType.scientific:
        return NumberFormat.scientificPattern(locale);
    }
  }
}

enum NumberFormatType { decimal, compact, percent, scientific }

/// Relative time display (e.g., "2 hours ago")
class RelativeTime extends StatelessWidget {
  final DateTime dateTime;
  final TextStyle? style;

  const RelativeTime({
    super.key,
    required this.dateTime,
    this.style,
  });

  @override
  Widget build(BuildContext context) {
    final now = DateTime.now();
    final difference = now.difference(dateTime);
    
    String text;
    if (difference.inDays > 365) {
      final years = (difference.inDays / 365).floor();
      text = '$years year${years > 1 ? "s" : ""} ago';
    } else if (difference.inDays > 30) {
      final months = (difference.inDays / 30).floor();
      text = '$months month${months > 1 ? "s" : ""} ago';
    } else if (difference.inDays > 0) {
      text = '${difference.inDays} day${difference.inDays > 1 ? "s" : ""} ago';
    } else if (difference.inHours > 0) {
      text = '${difference.inHours} hour${difference.inHours > 1 ? "s" : ""} ago';
    } else if (difference.inMinutes > 0) {
      text = '${difference.inMinutes} minute${difference.inMinutes > 1 ? "s" : ""} ago';
    } else {
      text = 'Just now';
    }
    
    return Text(text, style: style);
  }
}

/// Example usage
class PriceDisplay extends StatelessWidget {
  final double originalPrice;
  final double? salePrice;
  final String currency;

  const PriceDisplay({
    super.key,
    required this.originalPrice,
    this.salePrice,
    this.currency = 'USD',
  });

  @override
  Widget build(BuildContext context) {
    final hasSale = salePrice != null && salePrice! < originalPrice;
    
    return Row(
      mainAxisSize: MainAxisSize.min,
      children: [
        if (hasSale) ...[
          LocalizedCurrency(
            amount: salePrice!,
            currencyCode: currency,
            style: const TextStyle(
              fontWeight: FontWeight.bold,
              color: Colors.red,
            ),
          ),
          const SizedBox(width: 8),
        ],
        LocalizedCurrency(
          amount: originalPrice,
          currencyCode: currency,
          style: TextStyle(
            decoration: hasSale ? TextDecoration.lineThrough : null,
            color: hasSale ? Colors.grey : null,
          ),
        ),
      ],
    );
  }
}
```
