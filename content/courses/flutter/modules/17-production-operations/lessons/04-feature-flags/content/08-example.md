---
type: "EXAMPLE"
title: "Implementing Feature Flag Patterns"
---


Practical implementations of common patterns:



```dart
// lib/features/feature_gated_widget.dart
import 'package:flutter/material.dart';
import '../services/feature_flag_service.dart';

/// Widget that shows different content based on feature flag
class FeatureGatedWidget extends StatelessWidget {
  final String flagKey;
  final Widget enabledChild;
  final Widget? disabledChild;
  
  const FeatureGatedWidget({
    super.key,
    required this.flagKey,
    required this.enabledChild,
    this.disabledChild,
  });
  
  @override
  Widget build(BuildContext context) {
    final isEnabled = FeatureFlagService().getBool(flagKey);
    
    if (isEnabled) {
      return enabledChild;
    }
    
    return disabledChild ?? const SizedBox.shrink();
  }
}

/// Widget that shows variant-specific content
class VariantWidget extends StatelessWidget {
  final String experimentKey;
  final Map<String, Widget> variants;
  final Widget fallback;
  
  const VariantWidget({
    super.key,
    required this.experimentKey,
    required this.variants,
    required this.fallback,
  });
  
  @override
  Widget build(BuildContext context) {
    final variant = FeatureFlagService().getString(experimentKey);
    return variants[variant] ?? fallback;
  }
}

// Usage examples:

// Simple feature gate
class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home')),
      body: Column(
        children: [
          // Only show if feature is enabled
          FeatureGatedWidget(
            flagKey: 'feature_social_sharing',
            enabledChild: ShareButton(),
            disabledChild: null, // Hide completely if disabled
          ),
          
          // Show different UIs
          FeatureGatedWidget(
            flagKey: 'feature_new_product_list',
            enabledChild: NewProductGrid(),
            disabledChild: OldProductList(),
          ),
        ],
      ),
    );
  }
}

// A/B test variant selection
class OnboardingScreen extends StatelessWidget {
  const OnboardingScreen({super.key});
  
  @override
  Widget build(BuildContext context) {
    return VariantWidget(
      experimentKey: 'experiment_onboarding',
      variants: {
        'control': const OriginalOnboarding(),
        'variant_a': const ShortOnboarding(),
        'variant_b': const VideoOnboarding(),
      },
      fallback: const OriginalOnboarding(),
    );
  }
}

// Gradual rollout with user feedback
class CheckoutButton extends StatelessWidget {
  final VoidCallback onPressed;
  
  const CheckoutButton({super.key, required this.onPressed});
  
  @override
  Widget build(BuildContext context) {
    final flags = FeatureFlagService();
    
    if (flags.isNewCheckoutEnabled) {
      return ElevatedButton.icon(
        onPressed: onPressed,
        icon: const Icon(Icons.flash_on),
        label: const Text('Express Checkout'),
        style: ElevatedButton.styleFrom(
          backgroundColor: Colors.green,
        ),
      );
    }
    
    return ElevatedButton(
      onPressed: onPressed,
      child: const Text('Proceed to Checkout'),
    );
  }
}

// Placeholder widgets for example
class ShareButton extends StatelessWidget {
  @override
  Widget build(BuildContext context) => IconButton(
    onPressed: () {},
    icon: const Icon(Icons.share),
  );
}

class NewProductGrid extends StatelessWidget {
  @override
  Widget build(BuildContext context) => const Text('New Grid');
}

class OldProductList extends StatelessWidget {
  @override
  Widget build(BuildContext context) => const Text('Old List');
}

class OriginalOnboarding extends StatelessWidget {
  const OriginalOnboarding({super.key});
  @override
  Widget build(BuildContext context) => const Text('Original');
}

class ShortOnboarding extends StatelessWidget {
  const ShortOnboarding({super.key});
  @override
  Widget build(BuildContext context) => const Text('Short');
}

class VideoOnboarding extends StatelessWidget {
  const VideoOnboarding({super.key});
  @override
  Widget build(BuildContext context) => const Text('Video');
}
```
