---
type: "WARNING"
title: "Generator Gotchas"
---

While the Riverpod generator is powerful, there are several common issues developers encounter. Understanding these will save you debugging time.

### Gotcha 1: Must Run build_runner

After writing `@riverpod` annotations, you MUST run the generator:
```bash
dart run build_runner watch  # Recommended during development
```

If you see errors like "Undefined name 'counterProvider'" or "Class _$Counter not found", the generator has not run yet.

### Gotcha 2: Part Directive is Required

Every file with `@riverpod` annotations needs the part directive:
```dart
part 'your_file.g.dart';  // MUST match your file name!
```

If your file is `counter_provider.dart`, the part must be `part 'counter_provider.g.dart';`

### Gotcha 3: Class Naming Convention

Your class MUST extend the generated base class:
```dart
// CORRECT:
class Counter extends _$Counter { ... }

// WRONG - will not compile:
class CounterNotifier extends Notifier<int> { ... }
```

The base class is always `_$YourClassName` (underscore, dollar sign, your exact class name).

### Gotcha 4: Provider Naming is Automatic

You do NOT name the provider. It is derived from the class name:
- `Counter` -> `counterProvider`
- `UserList` -> `userListProvider`
- `HTTPClient` -> `hTTPClientProvider` (careful with acronyms!)

If you want a specific provider name, name your class accordingly.

### Gotcha 5: Never Edit .g.dart Files

The `.g.dart` files are completely regenerated every time you run build_runner. Any manual changes will be lost. If you need to customize behavior, do it in your source file.

### Gotcha 6: Build Errors Can Be Cryptic

If the generator fails, error messages may be confusing. Common fixes:
```bash
# Clean and rebuild
dart run build_runner clean
dart run build_runner build --delete-conflicting-outputs

# Check for syntax errors in your Dart files first!
flutter analyze
```

### Gotcha 7: Hot Reload Limitations

Changes to `@riverpod` classes may not hot reload properly. You might need to:
- Stop the generator, restart it
- Perform a hot restart instead of hot reload
- In rare cases, stop and restart the entire app

### Gotcha 8: Ref Parameter Changed in Riverpod 2.6+

In older tutorials, you might see `SomeProviderRef ref`. The modern syntax is just `Ref ref`:
```dart
// Modern (2.6+):
@riverpod
String greeting(Ref ref) => 'Hello!';

// Old style (pre-2.6) - still works but deprecated:
@riverpod
String greeting(GreetingRef ref) => 'Hello!';
```

```dart
// COMMON MISTAKES AND FIXES

// =====================================================
// MISTAKE 1: Missing part directive
// =====================================================

// WRONG:
import 'package:riverpod_annotation/riverpod_annotation.dart';

@riverpod
class Counter extends _$Counter {  // ERROR: _$Counter not found!
  @override
  int build() => 0;
}

// CORRECT:
import 'package:riverpod_annotation/riverpod_annotation.dart';

part 'counter.g.dart';  // Add this line!

@riverpod
class Counter extends _$Counter {
  @override
  int build() => 0;
}

// =====================================================
// MISTAKE 2: Wrong base class
// =====================================================

// WRONG:
@riverpod
class Counter extends Notifier<int> {  // Should extend _$Counter!
  @override
  int build() => 0;
}

// CORRECT:
@riverpod
class Counter extends _$Counter {  // Always _$YourClassName
  @override
  int build() => 0;
}

// =====================================================
// MISTAKE 3: Trying to name the provider
// =====================================================

// WRONG - this syntax does not exist:
@riverpod(name: 'myCounterProvider')  // Not a valid option!
class Counter extends _$Counter { }

// CORRECT - provider name comes from class name:
@riverpod
class MyCounter extends _$MyCounter {  // Becomes myCounterProvider
  @override
  int build() => 0;
}

// =====================================================
// MISTAKE 4: Editing the .g.dart file
// =====================================================

// counter.g.dart - NEVER EDIT THIS FILE!
// Any changes will be lost on next build_runner run.

// If you need custom behavior, modify counter.dart instead.

// =====================================================
// MISTAKE 5: Forgetting to run build_runner
// =====================================================

// After writing @riverpod annotations, always run:
// dart run build_runner build
// 
// Or better, keep this running during development:
// dart run build_runner watch

// =====================================================
// MISTAKE 6: Part directive filename mismatch
// =====================================================

// If your file is: lib/providers/user_provider.dart
// WRONG:
part 'user.g.dart';  // Filename mismatch!

// CORRECT:
part 'user_provider.g.dart';  // Matches your file name
```
