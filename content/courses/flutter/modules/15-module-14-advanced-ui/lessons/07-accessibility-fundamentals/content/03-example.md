---
type: "EXAMPLE"
title: "Semantics Widget in Practice"
---


Applying semantics to common UI patterns:



```dart
import 'package:flutter/material.dart';

// Example 1: Custom icon button with proper semantics
class AccessibleIconButton extends StatelessWidget {
  final IconData icon;
  final String label;
  final VoidCallback onPressed;
  final bool isEnabled;

  const AccessibleIconButton({
    super.key,
    required this.icon,
    required this.label,
    required this.onPressed,
    this.isEnabled = true,
  });

  @override
  Widget build(BuildContext context) {
    return Semantics(
      label: label,
      button: true,
      enabled: isEnabled,
      onTap: isEnabled ? onPressed : null,
      child: GestureDetector(
        onTap: isEnabled ? onPressed : null,
        child: Container(
          padding: const EdgeInsets.all(12),
          decoration: BoxDecoration(
            color: isEnabled ? Colors.blue : Colors.grey,
            borderRadius: BorderRadius.circular(8),
          ),
          child: Icon(
            icon,
            color: Colors.white,
            semanticLabel: null, // Provided by parent Semantics
          ),
        ),
      ),
    );
  }
}

// Example 2: Image with accessibility description
class AccessibleImage extends StatelessWidget {
  final String imageUrl;
  final String description;
  final bool isDecorative;

  const AccessibleImage({
    super.key,
    required this.imageUrl,
    required this.description,
    this.isDecorative = false,
  });

  @override
  Widget build(BuildContext context) {
    // Decorative images should be excluded from semantics
    if (isDecorative) {
      return Semantics(
        excludeSemantics: true,
        child: Image.network(imageUrl),
      );
    }

    return Semantics(
      label: description,
      image: true,
      child: Image.network(
        imageUrl,
        semanticLabel: null, // Provided by parent Semantics
      ),
    );
  }
}

// Example 3: Progress indicator with value
class AccessibleProgressBar extends StatelessWidget {
  final double progress; // 0.0 to 1.0
  final String label;

  const AccessibleProgressBar({
    super.key,
    required this.progress,
    this.label = 'Progress',
  });

  @override
  Widget build(BuildContext context) {
    final percentage = (progress * 100).round();
    
    return Semantics(
      label: label,
      value: '$percentage%',
      child: LinearProgressIndicator(
        value: progress,
        semanticsLabel: null, // Provided by parent Semantics
        semanticsValue: null,
      ),
    );
  }
}

// Example 4: Card with grouped semantics
class AccessibleProductCard extends StatelessWidget {
  final String name;
  final String price;
  final double rating;
  final VoidCallback onTap;

  const AccessibleProductCard({
    super.key,
    required this.name,
    required this.price,
    required this.rating,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    // MergeSemantics combines child semantics into one announcement
    return MergeSemantics(
      child: Semantics(
        button: true,
        onTap: onTap,
        child: GestureDetector(
          onTap: onTap,
          child: Card(
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(name, style: const TextStyle(fontSize: 18)),
                  Text(price),
                  // Exclude decorative stars, include rating value
                  Semantics(
                    label: 'Rating: ${rating.toStringAsFixed(1)} out of 5',
                    excludeSemantics: true,
                    child: Row(
                      children: List.generate(
                        5,
                        (i) => Icon(
                          i < rating ? Icons.star : Icons.star_border,
                          color: Colors.amber,
                        ),
                      ),
                    ),
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

// Example 5: Excluding decorative elements
class DecorativeBackground extends StatelessWidget {
  final Widget child;

  const DecorativeBackground({super.key, required this.child});

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        // Exclude decorative background from accessibility tree
        Semantics(
          excludeSemantics: true,
          child: Container(
            decoration: const BoxDecoration(
              gradient: LinearGradient(
                colors: [Colors.blue, Colors.purple],
              ),
            ),
          ),
        ),
        child,
      ],
    );
  }
}
```
