// Assume Version class from previous challenge is available

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
    // TODO: In real app, use PackageInfo.fromPlatform()
    // For this challenge, return a simulated version
    return UpdateChecker('2.0.0');
  }
  
  /// Check if update is needed based on version requirements
  UpdateResult checkUpdate({
    required String? minVersion,
    String? recommendedVersion,
    required String latestVersion,
  }) {
    // TODO: Implement update check logic
    // Rules:
    // 1. If current < minVersion -> required
    // 2. If current < recommendedVersion -> recommended
    // 3. If current < latestVersion -> optional
    // 4. Otherwise -> upToDate
    // Handle null/invalid versions gracefully
    throw UnimplementedError();
  }
  
  /// Helper to safely compare versions
  bool _isVersionLessThan(String current, String target) {
    // TODO: Use Version.parse and comparison
    throw UnimplementedError();
  }
}

void main() {
  final checker = UpdateChecker('2.0.0');
  
  // Test cases
  final result1 = checker.checkUpdate(
    minVersion: '1.0.0',
    recommendedVersion: '2.0.0',
    latestVersion: '2.1.0',
  );
  print('Result 1: ${result1.status}'); // optional
  
  final result2 = checker.checkUpdate(
    minVersion: '2.5.0',
    latestVersion: '3.0.0',
  );
  print('Result 2: ${result2.status}'); // required
}