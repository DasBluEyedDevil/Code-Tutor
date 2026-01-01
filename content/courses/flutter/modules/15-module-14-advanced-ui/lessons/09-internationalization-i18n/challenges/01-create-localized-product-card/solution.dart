import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

extension L10nExtension on BuildContext {
  AppLocalizations get l10n => AppLocalizations.of(this);
  String get localeString => Localizations.localeOf(this).toString();
  bool get isRtl => Directionality.of(this) == TextDirection.rtl;
}

class ProductCard extends StatelessWidget {
  final String productName;
  final double price;
  final String currencyCode;
  final int reviewCount;
  final DateTime lastUpdated;

  const ProductCard({
    super.key,
    required this.productName,
    required this.price,
    required this.currencyCode,
    required this.reviewCount,
    required this.lastUpdated,
  });

  @override
  Widget build(BuildContext context) {
    final locale = context.localeString;
    final l10n = context.l10n;
    final theme = Theme.of(context);
    
    // Format price for current locale
    final formattedPrice = NumberFormat.currency(
      locale: locale,
      name: currencyCode,
    ).format(price);
    
    // Format date for current locale
    final formattedDate = DateFormat.yMMMd(locale).format(lastUpdated);
    
    return Card(
      margin: const EdgeInsetsDirectional.all(8),
      child: Padding(
        padding: const EdgeInsetsDirectional.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Product name
            Text(
              productName,
              style: theme.textTheme.titleLarge,
            ),
            const SizedBox(height: 8),
            
            // Price row with star rating
            Row(
              children: [
                // Price at start
                Text(
                  l10n.productPrice(formattedPrice),
                  style: theme.textTheme.titleMedium?.copyWith(
                    color: theme.colorScheme.primary,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const Spacer(),
                // Reviews at end
                Row(
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    const Icon(Icons.star, color: Colors.amber, size: 20),
                    const SizedBox(width: 4),
                    Text(
                      l10n.reviewCount(reviewCount),
                      style: theme.textTheme.bodyMedium,
                    ),
                  ],
                ),
              ],
            ),
            const SizedBox(height: 12),
            
            // Last updated with icon
            Row(
              children: [
                Icon(
                  Icons.update,
                  size: 16,
                  color: theme.colorScheme.onSurfaceVariant,
                ),
                const SizedBox(width: 8),
                Text(
                  l10n.lastUpdated(formattedDate),
                  style: theme.textTheme.bodySmall?.copyWith(
                    color: theme.colorScheme.onSurfaceVariant,
                  ),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}

// Required ARB entries:
// app_en.arb:
// {
//   "productPrice": "Price: {price}",
//   "@productPrice": {
//     "placeholders": { "price": { "type": "String" } }
//   },
//   "reviewCount": "{count, plural, =0{No reviews} =1{1 review} other{{count} reviews}}",
//   "@reviewCount": {
//     "placeholders": { "count": { "type": "int" } }
//   },
//   "lastUpdated": "Last updated: {date}",
//   "@lastUpdated": {
//     "placeholders": { "date": { "type": "String" } }
//   }
// }