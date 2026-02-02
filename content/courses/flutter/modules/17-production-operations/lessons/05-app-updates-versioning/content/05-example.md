---
type: "EXAMPLE"
title: "Implementing Version Checking"
---


Create a comprehensive update checking service:



```dart
// lib/services/update_service.dart
import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:package_info_plus/package_info_plus.dart';
import 'package:url_launcher/url_launcher.dart';

/// Represents the result of an update check
enum UpdateStatus {
  upToDate,
  optionalUpdate,
  recommendedUpdate,
  requiredUpdate,
  error,
}

class UpdateInfo {
  final UpdateStatus status;
  final String? latestVersion;
  final String? currentVersion;
  final String? releaseNotes;
  final String? storeUrl;
  final String? errorMessage;
  
  UpdateInfo({
    required this.status,
    this.latestVersion,
    this.currentVersion,
    this.releaseNotes,
    this.storeUrl,
    this.errorMessage,
  });
  
  bool get hasUpdate => status == UpdateStatus.optionalUpdate ||
      status == UpdateStatus.recommendedUpdate ||
      status == UpdateStatus.requiredUpdate;
  
  bool get isRequired => status == UpdateStatus.requiredUpdate;
}

/// Service for checking and handling app updates
class UpdateService {
  static const String _iosAppId = 'YOUR_APP_STORE_ID';
  static const String _androidPackage = 'com.yourcompany.yourapp';
  
  /// Check for updates using your backend
  static Future<UpdateInfo> checkForUpdates() async {
    try {
      final packageInfo = await PackageInfo.fromPlatform();
      final currentVersion = packageInfo.version;
      
      // Option 1: Check your own backend
      final updateInfo = await _checkBackend(currentVersion);
      return updateInfo;
      
      // Option 2: Check app stores directly
      // return await _checkAppStore(currentVersion);
    } catch (e) {
      return UpdateInfo(
        status: UpdateStatus.error,
        errorMessage: e.toString(),
      );
    }
  }
  
  /// Check your backend for version requirements
  static Future<UpdateInfo> _checkBackend(String currentVersion) async {
    final response = await http.get(
      Uri.parse('https://api.yourapp.com/v1/app-version'),
      headers: {
        'X-Platform': Platform.isIOS ? 'ios' : 'android',
        'X-App-Version': currentVersion,
      },
    ).timeout(const Duration(seconds: 10));
    
    if (response.statusCode != 200) {
      throw Exception('Failed to check for updates');
    }
    
    final data = json.decode(response.body);
    // Expected response format:
    // {
    //   "latest_version": "2.5.0",
    //   "minimum_version": "2.0.0",
    //   "recommended_version": "2.4.0",
    //   "release_notes": "Bug fixes and improvements",
    //   "store_url": "https://..."
    // }
    
    final latestVersion = data['latest_version'] as String;
    final minimumVersion = data['minimum_version'] as String;
    final recommendedVersion = data['recommended_version'] as String?;
    
    UpdateStatus status;
    if (_isVersionLessThan(currentVersion, minimumVersion)) {
      status = UpdateStatus.requiredUpdate;
    } else if (recommendedVersion != null && 
               _isVersionLessThan(currentVersion, recommendedVersion)) {
      status = UpdateStatus.recommendedUpdate;
    } else if (_isVersionLessThan(currentVersion, latestVersion)) {
      status = UpdateStatus.optionalUpdate;
    } else {
      status = UpdateStatus.upToDate;
    }
    
    return UpdateInfo(
      status: status,
      latestVersion: latestVersion,
      currentVersion: currentVersion,
      releaseNotes: data['release_notes'] as String?,
      storeUrl: data['store_url'] as String?,
    );
  }
  
  /// Check app stores directly for latest version
  static Future<UpdateInfo> _checkAppStore(String currentVersion) async {
    if (Platform.isIOS) {
      return await _checkIOSAppStore(currentVersion);
    } else if (Platform.isAndroid) {
      return await _checkPlayStore(currentVersion);
    }
    return UpdateInfo(status: UpdateStatus.error);
  }
  
  /// Check iOS App Store using iTunes Lookup API
  static Future<UpdateInfo> _checkIOSAppStore(String currentVersion) async {
    final response = await http.get(
      Uri.parse('https://itunes.apple.com/lookup?id=$_iosAppId'),
    );
    
    if (response.statusCode != 200) {
      throw Exception('Failed to check App Store');
    }
    
    final data = json.decode(response.body);
    final results = data['results'] as List;
    
    if (results.isEmpty) {
      return UpdateInfo(status: UpdateStatus.upToDate);
    }
    
    final appInfo = results.first;
    final storeVersion = appInfo['version'] as String;
    final releaseNotes = appInfo['releaseNotes'] as String?;
    final storeUrl = appInfo['trackViewUrl'] as String?;
    
    final hasUpdate = _isVersionLessThan(currentVersion, storeVersion);
    
    return UpdateInfo(
      status: hasUpdate ? UpdateStatus.optionalUpdate : UpdateStatus.upToDate,
      latestVersion: storeVersion,
      currentVersion: currentVersion,
      releaseNotes: releaseNotes,
      storeUrl: storeUrl,
    );
  }
  
  /// Check Play Store (requires web scraping or Play Developer API)
  static Future<UpdateInfo> _checkPlayStore(String currentVersion) async {
    // Play Store doesn't have a public API like iTunes
    // Options:
    // 1. Use your own backend
    // 2. Use Firebase Remote Config
    // 3. Use third-party packages like 'new_version'
    
    // For this example, return up-to-date
    return UpdateInfo(
      status: UpdateStatus.upToDate,
      currentVersion: currentVersion,
      storeUrl: 'https://play.google.com/store/apps/details?id=$_androidPackage',
    );
  }
  
  /// Compare two semantic versions
  static bool _isVersionLessThan(String current, String target) {
    final currentParts = current.split('.').map((s) => int.tryParse(s) ?? 0).toList();
    final targetParts = target.split('.').map((s) => int.tryParse(s) ?? 0).toList();
    
    for (int i = 0; i < 3; i++) {
      final c = i < currentParts.length ? currentParts[i] : 0;
      final t = i < targetParts.length ? targetParts[i] : 0;
      if (c < t) return true;
      if (c > t) return false;
    }
    return false; // Equal versions
  }
  
  /// Open the appropriate app store
  static Future<void> openStore() async {
    final url = Platform.isIOS
        ? 'https://apps.apple.com/app/id$_iosAppId'
        : 'https://play.google.com/store/apps/details?id=$_androidPackage';
    
    final uri = Uri.parse(url);
    if (await canLaunchUrl(uri)) {
      await launchUrl(uri, mode: LaunchMode.externalApplication);
    }
  }
}
```
