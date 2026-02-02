---
type: "EXAMPLE"
title: "Implementing A/B Tests"
---


Complete A/B testing implementation with analytics tracking:



```dart
// lib/experiments/ab_test_service.dart
import 'package:firebase_analytics/firebase_analytics.dart';
import 'package:firebase_remote_config/firebase_remote_config.dart';

/// Service for managing A/B test experiments
class ABTestService {
  final FirebaseRemoteConfig _remoteConfig;
  final FirebaseAnalytics _analytics;
  
  // Track which experiments user has been exposed to
  final Set<String> _exposedExperiments = {};
  
  // Singleton
  static final ABTestService _instance = ABTestService._internal();
  factory ABTestService() => _instance;
  ABTestService._internal()
      : _remoteConfig = FirebaseRemoteConfig.instance,
        _analytics = FirebaseAnalytics.instance;
  
  /// Get variant for an experiment and log exposure
  Future<String> getVariant(String experimentKey) async {
    final variant = _remoteConfig.getString(experimentKey);
    
    // Log exposure only once per session
    if (!_exposedExperiments.contains(experimentKey)) {
      _exposedExperiments.add(experimentKey);
      await _logExposure(experimentKey, variant);
    }
    
    return variant;
  }
  
  Future<void> _logExposure(String experimentKey, String variant) async {
    await _analytics.logEvent(
      name: 'experiment_exposure',
      parameters: {
        'experiment_name': experimentKey,
        'variant': variant,
        'timestamp': DateTime.now().toIso8601String(),
      },
    );
  }
  
  /// Log a conversion event for experiment tracking
  Future<void> logConversion({
    required String experimentKey,
    required String eventName,
    Map<String, dynamic>? parameters,
  }) async {
    final variant = _remoteConfig.getString(experimentKey);
    
    await _analytics.logEvent(
      name: eventName,
      parameters: {
        'experiment_name': experimentKey,
        'variant': variant,
        ...?parameters,
      },
    );
  }
}

/// Experiment definitions
class Experiments {
  static const String checkoutFlow = 'experiment_checkout_flow';
  static const String onboardingStyle = 'experiment_onboarding_style';
  static const String pricingDisplay = 'experiment_pricing_display';
}

/// Experiment variants
class CheckoutVariants {
  static const String control = 'control';
  static const String singlePage = 'single_page';
  static const String express = 'express';
}

// Usage in checkout flow:
class CheckoutScreen extends StatefulWidget {
  const CheckoutScreen({super.key});
  
  @override
  State<CheckoutScreen> createState() => _CheckoutScreenState();
}

class _CheckoutScreenState extends State<CheckoutScreen> {
  final ABTestService _abTest = ABTestService();
  String _variant = CheckoutVariants.control;
  
  @override
  void initState() {
    super.initState();
    _loadVariant();
  }
  
  Future<void> _loadVariant() async {
    final variant = await _abTest.getVariant(Experiments.checkoutFlow);
    setState(() {
      _variant = variant;
    });
  }
  
  Future<void> _onPurchaseComplete() async {
    // Log conversion for the experiment
    await _abTest.logConversion(
      experimentKey: Experiments.checkoutFlow,
      eventName: 'purchase',
      parameters: {
        'value': 99.99,
        'currency': 'USD',
      },
    );
  }
  
  @override
  Widget build(BuildContext context) {
    switch (_variant) {
      case CheckoutVariants.singlePage:
        return SinglePageCheckout(onComplete: _onPurchaseComplete);
      case CheckoutVariants.express:
        return ExpressCheckout(onComplete: _onPurchaseComplete);
      default:
        return StandardCheckout(onComplete: _onPurchaseComplete);
    }
  }
}

// Reusable A/B test widget
class ABTestWidget extends StatefulWidget {
  final String experimentKey;
  final Map<String, Widget> variants;
  final Widget fallback;
  final void Function(String variant)? onVariantLoaded;
  
  const ABTestWidget({
    super.key,
    required this.experimentKey,
    required this.variants,
    required this.fallback,
    this.onVariantLoaded,
  });
  
  @override
  State<ABTestWidget> createState() => _ABTestWidgetState();
}

class _ABTestWidgetState extends State<ABTestWidget> {
  String? _variant;
  
  @override
  void initState() {
    super.initState();
    _loadVariant();
  }
  
  Future<void> _loadVariant() async {
    final variant = await ABTestService().getVariant(widget.experimentKey);
    setState(() {
      _variant = variant;
    });
    widget.onVariantLoaded?.call(variant);
  }
  
  @override
  Widget build(BuildContext context) {
    if (_variant == null) {
      return const CircularProgressIndicator();
    }
    return widget.variants[_variant] ?? widget.fallback;
  }
}

// Placeholder widgets
class StandardCheckout extends StatelessWidget {
  final VoidCallback onComplete;
  const StandardCheckout({super.key, required this.onComplete});
  @override
  Widget build(BuildContext context) => ElevatedButton(
    onPressed: onComplete,
    child: const Text('Standard Checkout'),
  );
}

class SinglePageCheckout extends StatelessWidget {
  final VoidCallback onComplete;
  const SinglePageCheckout({super.key, required this.onComplete});
  @override
  Widget build(BuildContext context) => ElevatedButton(
    onPressed: onComplete,
    child: const Text('Single Page Checkout'),
  );
}

class ExpressCheckout extends StatelessWidget {
  final VoidCallback onComplete;
  const ExpressCheckout({super.key, required this.onComplete});
  @override
  Widget build(BuildContext context) => ElevatedButton(
    onPressed: onComplete,
    child: const Text('Express Checkout'),
  );
}
```
