---
type: "KEY_POINT"
title: "Sound Null Safety: String vs String? Are DIFFERENT Types!"
---


Dart 3 has **sound null safety**, which prevents crashes from null values. Here's the key insight:

### `String` and `String?` Are Completely Different Types!

```dart
String name = 'Alice';    // MUST have a value, NEVER null
String? nickname = null;  // CAN be null (the ? makes it optional)
```

**Think of it like boxes:**
- `String` = A box that MUST contain text (never empty)
- `String?` = A box that MIGHT contain text OR be empty (null)

### Why This Matters

```dart
void main() {
  String name = 'Bob';     // ✅ OK - has a value
  // String name2 = null;  // ❌ ERROR! String can't be null
  
  String? nickname = null; // ✅ OK - String? allows null
  nickname = 'Bobby';      // ✅ Can also hold a value
}
```

### Working With Nullable Types

```dart
void main() {
  String? userName = getUserName(); // Might be null
  
  // ❌ This WON'T work:
  // print(userName.length);  // Error! userName might be null
  
  // ✅ Option 1: Null check
  if (userName != null) {
    print(userName.length);  // Safe - we checked!
  }
  
  // ✅ Option 2: Elvis operator (provide default)
  print(userName ?? 'Guest');  // If null, use 'Guest'
  
  // ✅ Option 3: Safe navigation
  print(userName?.length);  // Returns null if userName is null
  
  // ⚠️ Option 4: Force unwrap (DANGEROUS!)
  // print(userName!.length);  // Crashes if null!
}
```

### Quick Reference

| Type | Can Be Null? | Example |
|------|--------------|--------|
| `String` | ❌ No | Must have text |
| `String?` | ✅ Yes | Can be null |
| `int` | ❌ No | Must have number |
| `int?` | ✅ Yes | Can be null |
| `List<String>` | ❌ No | Must have list |
| `List<String>?` | ✅ Yes | List or null |

**Rule of Thumb**: Only use `?` when something truly might not exist. Prefer non-nullable types!

---

