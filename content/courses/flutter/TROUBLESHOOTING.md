# Flutter Troubleshooting Guide

This appendix covers common issues students encounter when working through the Flutter course. Use this reference when you hit roadblocks - most problems have straightforward solutions.

---

## A.1 Impeller Rendering Issues

### What is Impeller?

Impeller is Flutter's modern rendering engine, replacing Skia as the default renderer. It provides better performance through pre-compiled shaders and improved GPU utilization.

**Default Status:**
- iOS: Default since Flutter 3.29
- Android: Default since Flutter 3.38 (API 29+)

### Symptoms

- Visual glitches or artifacts during animations
- Unexpected jank on first frame of animations
- Blank or white screens on app launch
- Custom shader effects not rendering correctly
- Text rendering issues on certain devices

### Quick Diagnosis

Run your app with verbose logging to identify Impeller-related issues:

```bash
flutter run --verbose 2>&1 | grep -i impeller
```

### Solutions

**Temporary Disable (for testing):**

```bash
flutter run --no-enable-impeller
```

**Permanent Disable on Android:**

Add to `android/app/src/main/AndroidManifest.xml` inside the `<application>` tag:

```xml
<meta-data
    android:name="io.flutter.embedding.android.EnableImpeller"
    android:value="false" />
```

**Permanent Disable on iOS:**

Add to `ios/Runner/Info.plist`:

```xml
<key>FLTEnableImpeller</key>
<false/>
```

### Known Problematic Hardware

Some hardware configurations have documented issues with Impeller:

- **Samsung Exynos chips** (especially Exynos 990, 2100): Occasional shader compilation failures
- **Mali GPUs** (G52, G72 series): Visual artifacts in complex animations
- **Older Adreno GPUs** (6xx series on Android 10): Performance regressions

### When to Re-enable

Re-enable Impeller after:
- Updating to a newer Flutter version (fixes are released regularly)
- Testing on different hardware
- The specific visual issue is resolved in Flutter releases

Check the [Flutter GitHub issues](https://github.com/flutter/flutter/labels/e%3A%20impeller) for Impeller-related fixes in each release.

---

## A.2 Android Build Issues

### Gradle Version Mismatch

**Symptom:**
```
Could not determine the dependencies of task ':app:compileDebugJavaWithJavac'
```
or
```
Minimum supported Gradle version is X.Y
```

**Solution:**

Update the Gradle wrapper to the required version:

```bash
cd android
./gradlew wrapper --gradle-version=8.10
```

Then clean and rebuild:

```bash
cd ..
flutter clean
flutter pub get
flutter build apk
```

### Java Version Issues

Flutter 3.38+ requires Java 17 or higher.

**Check your Java version:**

```bash
java -version
```

**Check JAVA_HOME:**

```bash
# Windows (PowerShell)
echo $env:JAVA_HOME

# macOS/Linux
echo $JAVA_HOME
```

**Set JAVA_HOME (Windows):**

```powershell
# Find Java installation
where java

# Set permanently (run as Administrator)
[Environment]::SetEnvironmentVariable("JAVA_HOME", "C:\Program Files\Java\jdk-17", "Machine")
```

**Set JAVA_HOME (macOS/Linux):**

```bash
# Add to ~/.bashrc or ~/.zshrc
export JAVA_HOME=$(/usr/libexec/java_home -v 17)
```

### 16KB Page Size (Android 15+)

Android 15 introduces 16KB page size support. Apps targeting Android 15+ need NDK r28 or later.

**Symptom:**
```
FATAL EXCEPTION: 16KB page size not supported by native library
```

**Solution:**

Update `android/app/build.gradle`:

```gradle
android {
    ndkVersion = "28.0.0"  // Or higher
}
```

If you don't have NDK r28+, install it:

```bash
sdkmanager "ndk;28.0.12674087"
```

### SDK/Build Tools Missing

**Symptom:**
```
Failed to find Build Tools revision X.Y.Z
```
or
```
Android SDK is missing
```

**Solution:**

Install missing components:

```bash
# List available packages
sdkmanager --list

# Install build tools
sdkmanager "build-tools;35.0.0"

# Install platform
sdkmanager "platforms;android-35"

# Install command-line tools
sdkmanager "cmdline-tools;latest"
```

Accept licenses:

```bash
flutter doctor --android-licenses
```

---

## A.3 iOS Build Issues

### CocoaPods Issues

**Symptom:**
```
Error running pod install
```
or
```
CDN: trunk URL couldn't be downloaded
```

**Solution:**

Complete CocoaPods reset:

```bash
cd ios

# Remove existing pods
rm -rf Pods
rm -rf .symlinks
rm Podfile.lock

# Clear CocoaPods cache
pod cache clean --all

# Reinstall
pod install --repo-update

cd ..
flutter clean
flutter pub get
```

If issues persist, reinstall CocoaPods:

```bash
sudo gem uninstall cocoapods
sudo gem install cocoapods
pod setup
```

### Xcode Version Mismatch

**Symptom:**
```
The iOS deployment target 'IPHONEOS_DEPLOYMENT_TARGET' is set to X.0, but the range of supported deployment target versions is Y.0 to Z.0
```

**Solution:**

Update the platform version in `ios/Podfile`:

```ruby
# Change from:
platform :ios, '12.0'

# To (match your Xcode's minimum):
platform :ios, '13.0'
```

Then rebuild:

```bash
cd ios
pod install
cd ..
flutter clean
flutter build ios
```

### Signing Issues

**Symptom:**
```
No signing certificate "iOS Development" found
```
or
```
Signing for "Runner" requires a development team
```

**Solution:**

1. Open the project in Xcode:
   ```bash
   open ios/Runner.xcworkspace
   ```

2. Select the **Runner** target in the project navigator

3. Go to **Signing & Capabilities** tab

4. Check **Automatically manage signing**

5. Select your **Team** from the dropdown (requires Apple Developer account)

6. If you don't have a team, create a free Apple ID and select "Personal Team"

---

## A.4 Serverpod Issues

### Docker Not Running

**Symptom:**
```
Cannot connect to the Docker daemon
```
or
```
docker: command not found
```

**Solution:**

1. Verify Docker Desktop is installed and running
2. Check Docker status:
   ```bash
   docker info
   ```
3. On Windows/macOS, open Docker Desktop application
4. On Linux, start the Docker service:
   ```bash
   sudo systemctl start docker
   ```

### Database Connection Failed

**Symptom:**
```
Connection refused on port 5432
```
or
```
FATAL: password authentication failed
```

**Solution:**

Check running containers:

```bash
docker ps
```

If Serverpod containers aren't running, start them:

```bash
cd your_server_directory
docker compose up -d
```

Check container logs for errors:

```bash
docker compose logs postgres
```

Reset the database if needed:

```bash
docker compose down -v  # Warning: destroys data
docker compose up -d
```

### Code Generation Errors

**Symptom:**
```
Could not find generated protocol files
```
or
```
Endpoint methods not found
```

**Solution:**

Force regenerate Serverpod code:

```bash
cd your_server_directory
serverpod generate --force
```

If issues persist, clean and regenerate:

```bash
# In the server directory
dart pub get
serverpod generate --force

# In the client directory
dart pub get

# In the Flutter app directory
flutter pub get
```

---

## A.5 Common Runtime Errors

### Null Safety Violations

**Symptom:**
```
Null check operator used on a null value
```

**Problem code:**

```dart
String? name;
print(name!);  // Crashes if name is null
```

**Solution:**

Use null-aware operators instead of force-unwrapping:

```dart
String? name;

// Option 1: Provide default value
print(name ?? 'Unknown');

// Option 2: Conditional access
print(name?.toUpperCase() ?? 'UNKNOWN');

// Option 3: Check before use
if (name != null) {
  print(name);
}
```

### Provider Scope Errors

**Symptom:**
```
ProviderNotFoundException: Could not find a Provider
```
or
```
Bad state: No ProviderScope found
```

**Solution:**

Ensure `ProviderScope` wraps your app in `main.dart`:

```dart
void main() {
  runApp(
    const ProviderScope(
      child: MyApp(),
    ),
  );
}
```

For testing, wrap your widget:

```dart
testWidgets('my test', (tester) async {
  await tester.pumpWidget(
    const ProviderScope(
      child: MyWidget(),
    ),
  );
});
```

### Async Gap Issues

**Symptom:**
```
setState() called after dispose()
```
or app crashes after navigating away during async operation.

**Problem code:**

```dart
Future<void> loadData() async {
  final data = await fetchData();
  setState(() {  // Widget might be disposed!
    _data = data;
  });
}
```

**Solution:**

Check `mounted` before calling `setState`:

```dart
Future<void> loadData() async {
  final data = await fetchData();

  // Check if widget is still in the tree
  if (!mounted) return;

  setState(() {
    _data = data;
  });
}
```

For more complex scenarios, use a `CancelableOperation` or manage state with Riverpod/Bloc.

---

## Getting Help

When the solutions above don't resolve your issue, try these steps:

### 1. Run Flutter Doctor

Get a complete health check of your Flutter installation:

```bash
flutter doctor -v
```

Address any issues marked with `[!]` or `[X]`.

### 2. Clean Build

Reset all build artifacts:

```bash
flutter clean
flutter pub get
flutter build apk  # or flutter build ios
```

### 3. Check GitHub Issues

Search for your error message in the Flutter repository:
- [Flutter Issues](https://github.com/flutter/flutter/issues)
- [Serverpod Issues](https://github.com/serverpod/serverpod/issues)

### 4. Community Resources

- **Stack Overflow**: Tag questions with `flutter` and relevant tags
- **Flutter Discord**: [discord.gg/flutter](https://discord.gg/flutter) - Active community with help channels
- **Reddit**: r/FlutterDev for discussions and troubleshooting
- **Serverpod Discord**: For Serverpod-specific questions

### 5. Create a Minimal Reproduction

If filing a bug report:
1. Create a minimal project that reproduces the issue
2. Include `flutter doctor -v` output
3. List exact steps to reproduce
4. Include error messages and stack traces
