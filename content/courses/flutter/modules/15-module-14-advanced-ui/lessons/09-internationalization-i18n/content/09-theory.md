---
type: "THEORY"
title: "Date, Number, and Currency Formatting"
---


**The intl Package**

The `intl` package provides locale-aware formatting for dates, numbers, and currencies:

```dart
import 'package:intl/intl.dart';
```

**DateFormat - Locale-Aware Dates:**

```dart
final date = DateTime(2024, 12, 25);
final locale = Localizations.localeOf(context).toString();

// Short date
DateFormat.yMd(locale).format(date);
// en: 12/25/2024
// es: 25/12/2024
// ar: 25/12/2024

// Long date
DateFormat.yMMMMd(locale).format(date);
// en: December 25, 2024
// es: 25 de diciembre de 2024
// ar: 25 ديسمبر 2024

// Time
DateFormat.jm(locale).format(DateTime.now());
// en: 3:30 PM
// es: 15:30
```

**NumberFormat - Locale-Aware Numbers:**

```dart
final number = 1234567.89;

// Decimal
NumberFormat.decimalPattern(locale).format(number);
// en: 1,234,567.89
// de: 1.234.567,89
// fr: 1 234 567,89

// Compact
NumberFormat.compact(locale: locale).format(number);
// en: 1.2M
// es: 1,2 M
```

**Currency Formatting:**

```dart
final amount = 1234.56;

// With symbol
NumberFormat.currency(
  locale: locale,
  symbol: '\$',
).format(amount);
// en_US: $1,234.56
// de_DE: 1.234,56 $

// Currency name
NumberFormat.currency(
  locale: locale,
  name: 'EUR',
).format(amount);
// en: EUR 1,234.56
// de: 1.234,56 EUR
```

