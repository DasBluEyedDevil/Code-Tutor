# Flutter Course - Current Versions Reference

> **Last Updated:** December 2025
>
> This file serves as a living reference for version numbers used throughout the course.
> Lessons should remain version-agnostic where possible, referencing this document for specifics.

---

## Flutter & Dart

| Component | Version | Notes |
|-----------|---------|-------|
| Flutter SDK | 3.38.x | Stable channel |
| Dart SDK | 3.10.x | Included with Flutter |

---

## Android Development

| Component | Version | Notes |
|-----------|---------|-------|
| Android SDK (API Level) | 35 | Android 15 |
| Build Tools | 35.0.0 | Required for API 35 |
| NDK | r28 | Native Development Kit |
| Java | 17+ | JDK required for Gradle |
| Gradle | 8.14+ | Build automation |

---

## iOS Development

| Component | Version | Notes |
|-----------|---------|-------|
| Xcode | 16.x | Required for iOS builds |
| Minimum iOS Target | 12.0+ | Deployment target |
| CocoaPods | 1.15+ | Dependency manager |

---

## Backend - Dart

| Component | Version | Notes |
|-----------|---------|-------|
| Dart Frog | 1.x | Lightweight backend framework |
| Serverpod | 3.x | Full-featured backend framework |
| PostgreSQL | 16.x | Database |
| Docker | 24.x+ | Containerization |

---

## Key Packages

| Package | Version | Purpose |
|---------|---------|---------|
| riverpod | 2.6+ | State management |
| go_router | 14+ | Declarative routing |
| dio | 5.x | HTTP client |
| drift | 2.x | SQLite database |
| freezed | 2.x | Code generation for immutable classes |
| json_serializable | 6.x | JSON serialization |
| mocktail | 1.x | Mocking for tests |

---

## How to Check Versions

```bash
# Flutter and Dart versions
flutter --version

# Detailed environment info
flutter doctor -v

# Check specific package versions in your project
flutter pub deps

# Android SDK location and versions
flutter doctor -v | grep -A 10 "Android toolchain"

# iOS toolchain (macOS only)
flutter doctor -v | grep -A 10 "Xcode"

# Check installed Dart packages globally
dart pub global list
```

---

## How to Update Flutter

```bash
# Update Flutter to latest stable
flutter upgrade

# Switch to a specific channel
flutter channel stable
flutter upgrade

# Clean and rebuild after major updates
flutter clean
flutter pub get

# Verify installation after update
flutter doctor
```

---

## Version Compatibility Notes

- Always run `flutter doctor` after updating to check for compatibility issues
- When updating Flutter, also update your IDE plugins (VS Code Flutter extension, Android Studio Flutter plugin)
- Check package compatibility on [pub.dev](https://pub.dev) before updating dependencies
- For production apps, pin exact versions in `pubspec.yaml` using `dependency: 1.2.3` instead of `dependency: ^1.2.3`
