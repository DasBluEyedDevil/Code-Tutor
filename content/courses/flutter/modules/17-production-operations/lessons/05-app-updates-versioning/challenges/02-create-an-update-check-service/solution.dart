// Version class (simplified for this solution)
class Version implements Comparable<Version> {
  final int major;
  final int minor;
  final int patch;
  final String? preRelease;
  
  Version({required this.major, required this.minor, required this.patch, this.preRelease});
  
  factory Version.parse(String versionString) {
    String remaining = versionString;
    String? preRelease;
    
    if (remaining.contains('+')) {
      remaining = remaining.split('+')[0];
    }
    if (remaining.contains('-')) {
      final parts = remaining.split('-');
      remaining = parts[0];
      preRelease = parts.sublist(1).join('-');
    }
    
    final versionParts = remaining.split('.');
    return Version(
      major: versionParts.isNotEmpty ? int.tryParse(versionParts[0]) ?? 0 : 0,
      minor: versionParts.length > 1 ? int.tryParse(versionParts[1]) ?? 0 : 0,
      patch: versionParts.length > 2 ? int.tryParse(versionParts[2]) ?? 0 : 0,
      preRelease: preRelease,
    );
  }
  
  @override
  int compareTo(Version other) {
    if (major != other.major) return major.compareTo(other.major);
    if (minor != other.minor) return minor.compareTo(other.minor);
    if (patch != other.patch) return patch.compareTo(other.patch);
    if (preRelease == null && other.preRelease != null) return 1;
    if (preRelease != null && other.preRelease == null) return -1;
    if (preRelease != null && other.preRelease != null) {
      return preRelease!.compareTo(other.preRelease!);
    }
    return 0;
  }
  
  bool operator <(Version other) => compareTo(other) < 0;
}

enum UpdateStatus {
  upToDate,
  optional,
  recommended,
  required,
}

class UpdateResult {
  final UpdateStatus status;
  final String currentVersion;
  final String? targetVersion;
  final String? message;
  
  UpdateResult({
    required this.status,
    required this.currentVersion,
    this.targetVersion,
    this.message,
  });
  
  bool get needsUpdate => status != UpdateStatus.upToDate;
  bool get isBlocking => status == UpdateStatus.required;
}

class UpdateChecker {
  final String currentVersion;
  
  UpdateChecker(this.currentVersion);
  
  /// Create from package info (simulated for this challenge)
  static Future<UpdateChecker> fromPackageInfo() async {
    // In real app: final info = await PackageInfo.fromPlatform();
    // return UpdateChecker(info.version);
    return UpdateChecker('2.0.0');
  }
  
  /// Check if update is needed based on version requirements
  UpdateResult checkUpdate({
    required String? minVersion,
    String? recommendedVersion,
    required String latestVersion,
  }) {
    try {
      // Check required update first (highest priority)
      if (minVersion != null && _isVersionLessThan(currentVersion, minVersion)) {
        return UpdateResult(
          status: UpdateStatus.required,
          currentVersion: currentVersion,
          targetVersion: minVersion,
          message: 'A critical update is required. Please update to continue.',
        );
      }
      
      // Check recommended update
      if (recommendedVersion != null && 
          _isVersionLessThan(currentVersion, recommendedVersion)) {
        return UpdateResult(
          status: UpdateStatus.recommended,
          currentVersion: currentVersion,
          targetVersion: latestVersion,
          message: 'An important update is available. We recommend updating.',
        );
      }
      
      // Check optional update
      if (_isVersionLessThan(currentVersion, latestVersion)) {
        return UpdateResult(
          status: UpdateStatus.optional,
          currentVersion: currentVersion,
          targetVersion: latestVersion,
          message: 'A new version is available.',
        );
      }
      
      // Up to date
      return UpdateResult(
        status: UpdateStatus.upToDate,
        currentVersion: currentVersion,
        message: 'You have the latest version.',
      );
    } catch (e) {
      // Handle parsing errors gracefully - assume up to date
      return UpdateResult(
        status: UpdateStatus.upToDate,
        currentVersion: currentVersion,
        message: 'Unable to check for updates.',
      );
    }
  }
  
  /// Helper to safely compare versions
  bool _isVersionLessThan(String current, String target) {
    try {
      final currentVer = Version.parse(current);
      final targetVer = Version.parse(target);
      return currentVer < targetVer;
    } catch (e) {
      return false; // On error, assume not less than
    }
  }
}

void main() {
  final checker = UpdateChecker('2.0.0');
  
  // Test case 1: Optional update available
  final result1 = checker.checkUpdate(
    minVersion: '1.0.0',
    recommendedVersion: '2.0.0',
    latestVersion: '2.1.0',
  );
  print('Test 1 (current 2.0.0, latest 2.1.0):');
  print('  Status: ${result1.status}'); // optional
  print('  Message: ${result1.message}');
  print('  Needs update: ${result1.needsUpdate}');
  
  // Test case 2: Required update
  final result2 = checker.checkUpdate(
    minVersion: '2.5.0',
    latestVersion: '3.0.0',
  );
  print('\nTest 2 (current 2.0.0, min 2.5.0):');
  print('  Status: ${result2.status}'); // required
  print('  Is blocking: ${result2.isBlocking}');
  
  // Test case 3: Recommended update
  final result3 = checker.checkUpdate(
    minVersion: '1.5.0',
    recommendedVersion: '2.5.0',
    latestVersion: '3.0.0',
  );
  print('\nTest 3 (current 2.0.0, recommended 2.5.0):');
  print('  Status: ${result3.status}'); // recommended
  
  // Test case 4: Up to date
  final result4 = checker.checkUpdate(
    minVersion: '1.0.0',
    recommendedVersion: '1.5.0',
    latestVersion: '2.0.0',
  );
  print('\nTest 4 (current 2.0.0, latest 2.0.0):');
  print('  Status: ${result4.status}'); // upToDate
}