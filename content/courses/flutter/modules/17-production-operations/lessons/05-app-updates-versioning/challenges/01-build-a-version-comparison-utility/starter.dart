class Version implements Comparable<Version> {
  final int major;
  final int minor;
  final int patch;
  final String? preRelease;
  final String? buildNumber;
  
  Version({
    required this.major,
    required this.minor,
    required this.patch,
    this.preRelease,
    this.buildNumber,
  });
  
  /// Parse a version string like '2.4.1', '2.4.1-beta.1', or '2.4.1+45'
  factory Version.parse(String versionString) {
    // TODO: Parse the version string
    // Handle formats:
    // - '2.4.1' (basic)
    // - '2.4.1-beta.1' (with pre-release)
    // - '2.4.1+45' (with build number)
    // - '2.4.1-beta.1+45' (with both)
    throw UnimplementedError();
  }
  
  @override
  int compareTo(Version other) {
    // TODO: Compare two versions
    // Return: negative (this < other), 0 (equal), positive (this > other)
    // Rules:
    // - Compare major, then minor, then patch
    // - Pre-release versions are less than release (2.0.0-beta < 2.0.0)
    // - Build numbers are ignored for comparison
    throw UnimplementedError();
  }
  
  bool operator <(Version other) => compareTo(other) < 0;
  bool operator >(Version other) => compareTo(other) > 0;
  bool operator <=(Version other) => compareTo(other) <= 0;
  bool operator >=(Version other) => compareTo(other) >= 0;
  
  @override
  bool operator ==(Object other) {
    // TODO: Check equality (ignore build number)
    throw UnimplementedError();
  }
  
  @override
  int get hashCode => Object.hash(major, minor, patch, preRelease);
  
  bool isNewerThan(Version other) => this > other;
  bool isOlderThan(Version other) => this < other;
  
  @override
  String toString() {
    var result = '$major.$minor.$patch';
    if (preRelease != null) result += '-$preRelease';
    if (buildNumber != null) result += '+$buildNumber';
    return result;
  }
}

void main() {
  // Test your implementation
  final v1 = Version.parse('2.4.1');
  final v2 = Version.parse('2.5.0');
  final v3 = Version.parse('2.5.0-beta.1');
  
  print('v1: $v1');
  print('v2: $v2');
  print('v3: $v3');
  print('v1 < v2: ${v1 < v2}');
  print('v3 < v2: ${v3 < v2}');
}