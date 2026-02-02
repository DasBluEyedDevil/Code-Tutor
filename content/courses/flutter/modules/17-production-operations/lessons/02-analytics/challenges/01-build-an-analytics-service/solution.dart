import 'package:firebase_analytics/firebase_analytics.dart';

class AnalyticsService {
  // Singleton pattern
  static final AnalyticsService _instance = AnalyticsService._internal();
  factory AnalyticsService() => _instance;
  AnalyticsService._internal();
  
  final FirebaseAnalytics _analytics = FirebaseAnalytics.instance;
  
  /// Log when user views a screen
  Future<void> logScreenView({
    required String screenName,
    String? screenClass,
  }) async {
    await _analytics.logScreenView(
      screenName: screenName,
      screenClass: screenClass ?? screenName,
    );
  }
  
  /// Log when user taps a button
  Future<void> logButtonTap({
    required String buttonName,
    String? screenName,
  }) async {
    await _analytics.logEvent(
      name: 'button_tap',
      parameters: {
        'button_name': buttonName,
        if (screenName != null) 'screen_name': screenName,
      },
    );
  }
  
  /// Log when user performs a search
  Future<void> logSearch({
    required String searchTerm,
    required int resultsCount,
  }) async {
    await _analytics.logEvent(
      name: 'search',
      parameters: {
        'search_term': searchTerm,
        'results_count': resultsCount,
      },
    );
  }
  
  /// Set user tier for segmentation
  Future<void> setUserTier(String tier) async {
    await _analytics.setUserProperty(
      name: 'user_tier',
      value: tier,
    );
  }
  
  /// Log when user uses a feature
  Future<void> logFeatureUsed({
    required String featureName,
    int? durationSeconds,
  }) async {
    await _analytics.logEvent(
      name: 'feature_used',
      parameters: {
        'feature_name': featureName,
        if (durationSeconds != null) 'duration_seconds': durationSeconds,
      },
    );
  }
}

void main() {
  print('AnalyticsService created');
  final service = AnalyticsService();
  print('Singleton works: ${identical(service, AnalyticsService())}');
}