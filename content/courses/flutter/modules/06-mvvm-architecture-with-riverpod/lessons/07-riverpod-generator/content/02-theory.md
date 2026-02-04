---
type: "THEORY"
title: "Setting Up Code Generation"
---

Setting up Riverpod code generation requires adding a few packages and configuring your project correctly. Here is the complete setup process.

### Step 1: Add Dependencies

You need four packages:
- **flutter_riverpod**: The core Riverpod package you already have
- **riverpod_annotation**: Provides the @riverpod annotation
- **riverpod_generator**: The code generator (dev dependency)
- **build_runner**: Dart's code generation runner (dev dependency)

### Step 2: Update pubspec.yaml

Add these to your pubspec.yaml file. Note that riverpod_generator and build_runner go under dev_dependencies because they are only used during development, not at runtime:

```yaml
dependencies:
  flutter:
    sdk: flutter
  flutter_riverpod: ^2.6.1
  riverpod_annotation: ^2.6.1

dev_dependencies:
  flutter_test:
    sdk: flutter
  riverpod_generator: ^2.6.1
  build_runner: ^2.4.0
```

### Step 3: Run the Generator

After adding dependencies, run `flutter pub get` to install them. Then you have two options to run the generator:

**Option A: One-time generation**
```bash
dart run build_runner build
```
This runs once and generates all files. Use this for CI/CD or when you are done coding.

**Option B: Watch mode (recommended for development)**
```bash
dart run build_runner watch
```
This watches your files and regenerates automatically whenever you save changes. Keep this running in a terminal while you code.

### Step 4: Clean Build (if needed)

If you get errors or stale generated files, clean and rebuild:
```bash
dart run build_runner clean
dart run build_runner build --delete-conflicting-outputs
```

The `--delete-conflicting-outputs` flag removes old generated files that might conflict with new ones.