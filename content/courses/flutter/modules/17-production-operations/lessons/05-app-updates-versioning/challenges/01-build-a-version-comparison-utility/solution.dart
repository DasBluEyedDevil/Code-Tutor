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
    String remaining = versionString;
    String? buildNumber;
    String? preRelease;
    
    // Extract build number (after +)
    if (remaining.contains('+')) {
      final parts = remaining.split('+');
      remaining = parts[0];
      buildNumber = parts[1];
    }
    
    // Extract pre-release (after -)
    if (remaining.contains('-')) {
      final parts = remaining.split('-');
      remaining = parts[0];
      preRelease = parts.sublist(1).join('-');
    }
    
    // Parse version numbers
    final versionParts = remaining.split('.');
    final major = versionParts.isNotEmpty 
        ? int.tryParse(versionParts[0]) ?? 0 : 0;
    final minor = versionParts.length > 1 
        ? int.tryParse(versionParts[1]) ?? 0 : 0;
    final patch = versionParts.length > 2 
        ? int.tryParse(versionParts[2]) ?? 0 : 0;
    
    return Version(
      major: major,
      minor: minor,
      patch: patch,
      preRelease: preRelease,
      buildNumber: buildNumber,
    );
  }
  
  @override
  int compareTo(Version other) {
    // Compare major
    if (major != other.major) return major.compareTo(other.major);
    
    // Compare minor
    if (minor != other.minor) return minor.compareTo(other.minor);
    
    // Compare patch
    if (patch != other.patch) return patch.compareTo(other.patch);
    
    // Pre-release versions are less than release versions
    // 2.0.0-beta < 2.0.0
    if (preRelease == null && other.preRelease != null) return 1;
    if (preRelease != null && other.preRelease == null) return -1;
    
    // Compare pre-release strings alphabetically
    if (preRelease != null && other.preRelease != null) {
      return preRelease!.compareTo(other.preRelease!);
    }
    
    // Build numbers are ignored for comparison
    return 0;
  }
  
  bool operator <(Version other) => compareTo(other) < 0;
  bool operator >(Version other) => compareTo(other) > 0;
  bool operator <=(Version other) => compareTo(other) <= 0;
  bool operator >=(Version other) => compareTo(other) >= 0;
  
  @override
  bool operator ==(Object other) {
    if (identical(this, other)) return true;
    if (other is! Version) return false;
    return major == other.major &&
           minor == other.minor &&
           patch == other.patch &&
           preRelease == other.preRelease;
    // Build number is ignored for equality
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
  // Test parsing
  final v1 = Version.parse('2.4.1');
  print('v1: $v1 (major=${v1.major}, minor=${v1.minor}, patch=${v1.patch})');
  
  final v2 = Version.parse('2.5.0');
  print('v2: $v2');
  
  final v3 = Version.parse('2.5.0-beta.1');
  print('v3: $v3 (preRelease=${v3.preRelease})');
  
  final v4 = Version.parse('2.5.0+45');
  print('v4: $v4 (buildNumber=${v4.buildNumber})');
  
  final v5 = Version.parse('2.5.0-rc.1+100');
  print('v5: $v5');
  
  // Test comparisons
  print('\nComparisons:');
  print('v1 < v2: ${v1 < v2}'); // true
  print('v3 < v2: ${v3 < v2}'); // true (pre-release < release)
  print('v2 == v4: ${v2 == v4}'); // true (build numbers ignored)
  print('v1.isNewerThan(v3): ${v1.isNewerThan(v3)}'); // false
  print('v2.isNewerThan(v3): ${v2.isNewerThan(v3)}'); // true
}