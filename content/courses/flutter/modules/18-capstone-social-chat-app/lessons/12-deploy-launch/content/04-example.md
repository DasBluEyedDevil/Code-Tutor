---
type: "EXAMPLE"
title: "Store Submission"
---


**Submitting to Google Play Store and Apple App Store**

This section covers the complete submission process for both stores, including metadata configuration, screenshot requirements, review guidelines compliance, and handling rejections.



```ruby
# ============================================================
# Fastlane Configuration for Automated Submissions
# ============================================================
# fastlane/Fastfile
default_platform(:ios)

before_all do
  ensure_git_status_clean
  git_pull
end

# ============================================================
# iOS Lanes
# ============================================================
platform :ios do
  desc "Push a new beta build to TestFlight"
  lane :beta do
    setup_ci if ENV['CI']
    
    # Match for code signing
    match(
      type: "appstore",
      readonly: is_ci,
      app_identifier: "com.yourcompany.yourapp"
    )
    
    # Increment build number
    increment_build_number(
      build_number: ENV['BUILD_NUMBER'] || Time.now.strftime("%Y%m%d%H%M")
    )
    
    # Build the app
    build_app(
      workspace: "ios/Runner.xcworkspace",
      scheme: "Runner",
      export_method: "app-store",
      output_directory: "./build/ios",
      output_name: "YourApp.ipa"
    )
    
    # Upload to TestFlight
    upload_to_testflight(
      skip_waiting_for_build_processing: true,
      changelog: File.read("../CHANGELOG.md").split("\n## ")[1]
    )
    
    # Notify team
    slack(
      message: "New iOS beta uploaded to TestFlight!",
      channel: "#releases",
      success: true
    )
  end

  desc "Push a new release to the App Store"
  lane :release do
    setup_ci if ENV['CI']
    
    match(
      type: "appstore",
      readonly: true,
      app_identifier: "com.yourcompany.yourapp"
    )
    
    build_app(
      workspace: "ios/Runner.xcworkspace",
      scheme: "Runner",
      export_method: "app-store"
    )
    
    # Upload to App Store Connect
    upload_to_app_store(
      submit_for_review: true,
      automatic_release: false,
      force: true,
      precheck_include_in_app_purchases: false,
      submission_information: {
        add_id_info_uses_idfa: false,
        content_rights_has_rights: true,
        content_rights_contains_third_party_content: true,
        export_compliance_uses_encryption: true,
        export_compliance_is_exempt: true
      }
    )
  end

  desc "Download dSYMs and upload to Crashlytics"
  lane :refresh_dsyms do
    download_dsyms(
      version: "latest",
      app_identifier: "com.yourcompany.yourapp"
    )
    upload_symbols_to_crashlytics(gsp_path: "ios/Runner/GoogleService-Info.plist")
    clean_build_artifacts
  end
end

# ============================================================
# Android Lanes
# ============================================================
platform :android do
  desc "Push a new beta build to Internal Testing"
  lane :beta do
    # Build the app bundle
    sh("cd .. && flutter build appbundle --release")
    
    # Upload to Play Store Internal Testing
    upload_to_play_store(
      track: "internal",
      aab: "../build/app/outputs/bundle/release/app-release.aab",
      skip_upload_metadata: true,
      skip_upload_images: true,
      skip_upload_screenshots: true
    )
    
    slack(
      message: "New Android beta uploaded to Internal Testing!",
      channel: "#releases",
      success: true
    )
  end

  desc "Promote beta to production with staged rollout"
  lane :release do |options|
    rollout = (options[:rollout] || 10).to_f / 100
    
    upload_to_play_store(
      track: "production",
      track_promote_to: "production",
      rollout: rollout.to_s,
      aab: "../build/app/outputs/bundle/release/app-release.aab",
      skip_upload_changelogs: false
    )
    
    slack(
      message: "Android release promoted to production (#{(rollout * 100).to_i}% rollout)",
      channel: "#releases"
    )
  end

  desc "Increase rollout percentage"
  lane :increase_rollout do |options|
    rollout = (options[:rollout] || 100).to_f / 100
    
    upload_to_play_store(
      track: "production",
      rollout: rollout.to_s,
      skip_upload_aab: true,
      skip_upload_metadata: true
    )
  end
end

---

# ============================================================
# App Store Metadata (fastlane/metadata/en-US)
# ============================================================
# fastlane/metadata/en-US/name.txt
YourApp - Social Platform

# fastlane/metadata/en-US/subtitle.txt
Connect, Share, Discover

# fastlane/metadata/en-US/description.txt
YourApp is a modern social platform that helps you connect with friends, share moments, and discover new communities.

KEY FEATURES:

• Share Posts & Stories
Capture and share your favorite moments with photos, videos, and text posts. Express yourself with creative tools and filters.

• Real-time Messaging
Stay connected with instant messaging. Send text, photos, voice messages, and more in private or group conversations.

• Discover Communities
Find and join communities that match your interests. Connect with like-minded people from around the world.

• Privacy First
Your data is yours. Control who sees your content with granular privacy settings. End-to-end encryption for messages.

• Works Offline
Never miss a beat. Create and view content even without an internet connection. Everything syncs when you're back online.

Download YourApp today and start connecting!

Questions or feedback? Contact us at support@yourapp.com

# fastlane/metadata/en-US/keywords.txt
social,messaging,photos,community,friends,chat,share,discover,connect,stories

# fastlane/metadata/en-US/promotional_text.txt
New: Dark mode, improved performance, and bug fixes!

# fastlane/metadata/en-US/privacy_url.txt
https://yourapp.com/privacy

# fastlane/metadata/en-US/support_url.txt
https://yourapp.com/support

---

# ============================================================
# Play Store Metadata (fastlane/metadata/android/en-US)
# ============================================================
# fastlane/metadata/android/en-US/full_description.txt
# (Same content as iOS description.txt)

# fastlane/metadata/android/en-US/short_description.txt
Connect with friends, share moments, discover communities.

# fastlane/metadata/android/en-US/title.txt
YourApp - Social Platform

# fastlane/metadata/android/en-US/changelogs/default.txt
• Performance improvements for faster loading
• Dark mode now available in settings
• Bug fixes and stability improvements
• Improved offline sync reliability

---

# ============================================================
# Review Guidelines Compliance Checklist
# ============================================================
// lib/core/compliance/review_guidelines.dart

/// Checklist for App Store Review Guidelines Compliance
/// https://developer.apple.com/app-store/review/guidelines/
///
/// Common rejection reasons and how to avoid them:
///
/// 1. GUIDELINE 2.1 - App Completeness
///    - All features must be functional
///    - No placeholder content or "coming soon" features
///    - Demo accounts must work without requiring real data
///
/// 2. GUIDELINE 2.3 - Accurate Metadata
///    - Screenshots must reflect current app UI
///    - Description must accurately represent functionality
///    - No misleading claims about features
///
/// 3. GUIDELINE 3.1.1 - In-App Purchase
///    - All paid features must use Apple's IAP
///    - No links to external payment methods
///    - Clearly display IAP prices
///
/// 4. GUIDELINE 4.2 - Minimum Functionality
///    - App must provide sufficient value
///    - Not just a web wrapper
///    - Must have native features
///
/// 5. GUIDELINE 5.1.1 - Data Collection
///    - Privacy policy required
///    - Explain all data collection
///    - App Tracking Transparency if tracking

class ReviewComplianceChecker {
  /// Verify app meets review guidelines before submission
  Future<ComplianceReport> checkCompliance() async {
    final issues = <ComplianceIssue>[];

    // Check for placeholder content
    if (await _hasPlaceholderContent()) {
      issues.add(ComplianceIssue(
        guideline: '2.1',
        severity: Severity.critical,
        description: 'Found placeholder content that must be removed',
        recommendation: 'Remove all TODO comments and placeholder images',
      ));
    }

    // Check for external payment links
    if (await _hasExternalPaymentLinks()) {
      issues.add(ComplianceIssue(
        guideline: '3.1.1',
        severity: Severity.critical,
        description: 'Found links to external payment methods',
        recommendation: 'Remove all external payment links, use Apple IAP',
      ));
    }

    // Check privacy policy
    if (!await _hasValidPrivacyPolicy()) {
      issues.add(ComplianceIssue(
        guideline: '5.1.1',
        severity: Severity.critical,
        description: 'Privacy policy URL is invalid or missing',
        recommendation: 'Ensure privacy policy is accessible at provided URL',
      ));
    }

    // Check ATT implementation
    if (await _usesTracking() && !await _hasATTImplementation()) {
      issues.add(ComplianceIssue(
        guideline: '5.1.2',
        severity: Severity.critical,
        description: 'App uses tracking but ATT not implemented',
        recommendation: 'Implement App Tracking Transparency framework',
      ));
    }

    return ComplianceReport(
      passed: issues.where((i) => i.severity == Severity.critical).isEmpty,
      issues: issues,
      checkedAt: DateTime.now(),
    );
  }

  Future<bool> _hasPlaceholderContent() async {
    // Check for TODO comments, placeholder images, etc.
    return false;
  }

  Future<bool> _hasExternalPaymentLinks() async {
    // Scan for URLs containing payment keywords
    return false;
  }

  Future<bool> _hasValidPrivacyPolicy() async {
    // Verify privacy policy URL is accessible
    return true;
  }

  Future<bool> _usesTracking() async {
    // Check if app uses any tracking SDKs
    return false;
  }

  Future<bool> _hasATTImplementation() async {
    // Verify ATT is properly implemented
    return true;
  }
}

enum Severity { warning, critical }

class ComplianceIssue {
  final String guideline;
  final Severity severity;
  final String description;
  final String recommendation;

  ComplianceIssue({
    required this.guideline,
    required this.severity,
    required this.description,
    required this.recommendation,
  });
}

class ComplianceReport {
  final bool passed;
  final List<ComplianceIssue> issues;
  final DateTime checkedAt;

  ComplianceReport({
    required this.passed,
    required this.issues,
    required this.checkedAt,
  });
}

---

# ============================================================
# Responding to App Store Rejections
# ============================================================
# Example rejection response template

# Subject: Re: App Review - YourApp (v1.0.0)

# Dear App Review Team,
#
# Thank you for reviewing YourApp. We have addressed the concerns raised:
#
# Issue: Guideline 4.2 - Minimum Functionality
# "The app appears to be a simple web wrapper."
#
# Resolution:
# We have added the following native features:
# 1. Offline mode with local data persistence
# 2. Push notifications for real-time updates
# 3. Native camera integration for photo capture
# 4. Biometric authentication (Face ID / Touch ID)
# 5. Widget support for home screen
#
# We have attached screenshots demonstrating these native features.
#
# Please let us know if you need any additional information.
#
# Best regards,
# Your Name
# Developer, YourCompany

# Tips for successful appeals:
# 1. Be respectful and professional
# 2. Address each issue specifically
# 3. Provide evidence (screenshots, videos)
# 4. Explain the user benefit
# 5. Offer to schedule a call if complex
```
