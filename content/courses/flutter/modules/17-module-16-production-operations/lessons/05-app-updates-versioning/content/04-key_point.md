---
type: "KEY_POINT"
title: "Update Checking Strategies"
---


**Why Check for Updates?**

App stores do not immediately push updates to all users. Users may:
- Have auto-updates disabled
- Be on limited data and skip downloads
- Ignore update notifications
- Use old devices that update slowly

**Update Checking Approaches**

1. **Store API Check**
   - Query App Store / Play Store for latest version
   - Compare with installed version
   - Pros: Accurate, real-time
   - Cons: Rate limits, API changes

2. **Backend Version Check**
   - Your server returns current/minimum versions
   - Full control over update logic
   - Pros: Flexible, can target specific users
   - Cons: Requires backend infrastructure

3. **Firebase Remote Config**
   - Store version requirements as remote config
   - Combine with feature flags
   - Pros: No backend needed, instant updates
   - Cons: Limited targeting options

**Version Comparison Logic**

```dart
// Compare semantic versions
int compareVersions(String v1, String v2) {
  final parts1 = v1.split('.').map(int.parse).toList();
  final parts2 = v2.split('.').map(int.parse).toList();
  
  for (int i = 0; i < 3; i++) {
    final p1 = i < parts1.length ? parts1[i] : 0;
    final p2 = i < parts2.length ? parts2[i] : 0;
    if (p1 != p2) return p1.compareTo(p2);
  }
  return 0; // Equal
}

// Returns: negative (v1 < v2), 0 (equal), positive (v1 > v2)
compareVersions('1.0.0', '2.0.0'); // -1 (older)
compareVersions('2.0.0', '2.0.0'); // 0 (same)
compareVersions('2.1.0', '2.0.0'); // 1 (newer)
```

**Update Types**

| Type | Behavior | Use Case |
|------|----------|----------|
| Optional | Dismissible prompt | New features, minor fixes |
| Recommended | Persistent but skippable | Important improvements |
| Required | Blocking, must update | Security fixes, breaking API |

