import 'package:firebase_analytics/firebase_analytics.dart';

class AnalyticsService {
  // TODO: Implement singleton pattern with FirebaseAnalytics.instance
  
  /// Log when user views a screen
  Future<void> logScreenView({
    required String screenName,
    String? screenClass,
  }) async {
    // TODO: Call logScreenView on analytics instance
  }
  
  /// Log when user taps a button
  Future<void> logButtonTap({
    required String buttonName,
    String? screenName,
  }) async {
    // TODO: Log custom event 'button_tap' with parameters
  }
  
  /// Log when user performs a search
  Future<void> logSearch({
    required String searchTerm,
    required int resultsCount,
  }) async {
    // TODO: Log custom event 'search' with search_term and results_count
  }
  
  /// Set user tier for segmentation
  Future<void> setUserTier(String tier) async {
    // TODO: Set 'user_tier' user property
  }
  
  /// Log when user uses a feature
  Future<void> logFeatureUsed({
    required String featureName,
    int? durationSeconds,
  }) async {
    // TODO: Log custom event 'feature_used' with feature_name and optional duration
  }
}

void main() {
  print('AnalyticsService created');
}