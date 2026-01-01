---
type: "EXAMPLE"
title: "User Properties for Segmentation"
---


User properties let you segment users into audiences for analysis and targeting:



```dart
// lib/services/analytics_service.dart

extension UserPropertyAnalytics on AnalyticsService {
  /// Set subscription tier for segmentation
  Future<void> setSubscriptionTier(String tier) async {
    await setUserProperty(
      name: 'subscription_tier',
      value: tier, // 'free', 'premium', 'enterprise'
    );
  }
  
  /// Set user's preferred language
  Future<void> setPreferredLanguage(String language) async {
    await setUserProperty(
      name: 'preferred_language',
      value: language,
    );
  }
  
  /// Set user's experience level
  Future<void> setExperienceLevel(String level) async {
    await setUserProperty(
      name: 'experience_level',
      value: level, // 'beginner', 'intermediate', 'expert'
    );
  }
  
  /// Set account age bucket
  Future<void> setAccountAgeBucket(int daysOld) async {
    String bucket;
    if (daysOld < 7) {
      bucket = 'new_user';
    } else if (daysOld < 30) {
      bucket = 'week_old';
    } else if (daysOld < 90) {
      bucket = 'month_old';
    } else {
      bucket = 'veteran';
    }
    
    await setUserProperty(
      name: 'account_age_bucket',
      value: bucket,
    );
  }
  
  /// Track feature adoption
  Future<void> setFeatureAdoption(List<String> features) async {
    // User properties can't hold arrays, so we encode as string
    await setUserProperty(
      name: 'features_used',
      value: features.join(','),
    );
  }
}

// Call after user signs in:
class AuthService {
  final AnalyticsService _analytics = AnalyticsService();
  
  Future<void> onUserSignedIn(User user) async {
    // Set user ID for cross-device tracking
    await _analytics.setUserId(user.id);
    
    // Set user properties for segmentation
    await _analytics.setSubscriptionTier(user.subscriptionTier);
    await _analytics.setPreferredLanguage(user.language);
    await _analytics.setAccountAgeBucket(
      DateTime.now().difference(user.createdAt).inDays,
    );
  }
  
  Future<void> onUserSignedOut() async {
    // Clear user ID
    await _analytics.setUserId(null);
  }
}
```
