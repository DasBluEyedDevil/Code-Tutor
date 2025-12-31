# Flutter Full-Stack Dart Developer Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Transform the Flutter course into a comprehensive Full-Stack Dart Developer program with 17 modules, ~100 lessons, covering Flutter frontend + Dart backend development.

**Architecture:** Single `course.json` file contains all modules, lessons, and content. Existing scripts (`apply-review-fix.ps1`, `extract-lesson.ps1`) support content modification. New lessons follow existing structure with `contentSections` (THEORY, EXAMPLE, KEY_POINT, WARNING) and `challenges` arrays.

**Tech Stack:** JSON content files, PowerShell scripts for content management, Dart/Flutter for code examples.

---

## Content Structure Reference

```json
{
  "modules": [{
    "id": "module-XX",
    "title": "Module X: Title",
    "description": "Description",
    "difficulty": "beginner|intermediate|advanced",
    "estimatedHours": N,
    "lessons": [{
      "id": "X.Y",
      "title": "Module X, Lesson Y: Title",
      "moduleId": "module-XX",
      "order": Y,
      "estimatedMinutes": N,
      "difficulty": "beginner|intermediate|advanced",
      "contentSections": [
        { "type": "THEORY", "title": "...", "content": "..." },
        { "type": "EXAMPLE", "title": "...", "content": "...", "code": "...", "language": "dart" },
        { "type": "KEY_POINT", "title": "...", "content": "..." },
        { "type": "WARNING", "title": "...", "content": "..." }
      ],
      "challenges": [{
        "type": "FREE_CODING",
        "id": "X.Y-challenge-N",
        "title": "...",
        "description": "...",
        "instructions": "...",
        "starterCode": "...",
        "solution": "...",
        "language": "dart",
        "testCases": [...],
        "hints": [...],
        "commonMistakes": [...],
        "difficulty": "beginner|intermediate|advanced"
      }]
    }]
  }]
}
```

---

## File Paths

- **Course content:** `content/courses/flutter/course.json`
- **Version reference:** `content/courses/flutter/CURRENT_VERSIONS.md` (NEW)
- **Troubleshooting:** `content/courses/flutter/TROUBLESHOOTING.md` (NEW)
- **Scripts:** `scripts/apply-review-fix.ps1`, `scripts/extract-lesson.ps1`

---

# PHASE 1: FOUNDATION UPDATES

## Task 1.1: Create CURRENT_VERSIONS.md Reference File

**Files:**
- Create: `content/courses/flutter/CURRENT_VERSIONS.md`

**Step 1: Create the versions reference file**

```markdown
# Current Recommended Versions

*Last Updated: December 2025*

Use this reference when following along with the course. Version numbers in lessons use "latest stable" language - check here for current specifics.

## Flutter & Dart

| Component | Version | Notes |
|-----------|---------|-------|
| Flutter SDK | 3.38.x | Stable channel |
| Dart SDK | 3.10.x | Included with Flutter |

## Android Development

| Component | Version | Notes |
|-----------|---------|-------|
| Android SDK | API 35 | Latest stable platform |
| Build Tools | 35.0.0 | Match API level |
| NDK | r28 | Required for 16KB page size (Android 15+) |
| Java | 17+ | Minimum required by Gradle 8.14+ |
| Gradle | 8.14+ | Bundled with Flutter |

## iOS Development

| Component | Version | Notes |
|-----------|---------|-------|
| Xcode | 16.x | Latest stable |
| iOS Deployment Target | 12.0+ | Minimum supported |
| CocoaPods | 1.15+ | Dependency manager |

## Backend (Dart)

| Component | Version | Notes |
|-----------|---------|-------|
| Dart Frog CLI | 1.x | `dart pub global activate dart_frog_cli` |
| Serverpod | 3.x | "Industrial" release |
| PostgreSQL | 16.x | Database |
| Docker | 24.x+ | Container runtime |

## Key Packages

| Package | Version | Purpose |
|---------|---------|---------|
| riverpod | 2.6+ | State management |
| flutter_riverpod | 2.6+ | Flutter bindings |
| riverpod_annotation | 2.6+ | Code generation |
| go_router | 14+ | Navigation |
| dio | 5.x | HTTP client |
| drift | 2.x | Local database |
| freezed | 2.x | Immutable models |
| json_serializable | 6.x | JSON parsing |
| mocktail | 1.x | Testing mocks |

## How to Check Your Versions

```bash
# Flutter and Dart
flutter --version

# Android SDK (in Android Studio)
# Tools > SDK Manager > SDK Platforms

# Xcode
xcodebuild -version

# Dart Frog
dart_frog --version

# Serverpod
serverpod version
```

## Updating Flutter

```bash
flutter upgrade
flutter doctor
```
```

**Step 2: Verify file created**

Run: `Get-Content "content/courses/flutter/CURRENT_VERSIONS.md" | Select-Object -First 10`
Expected: First 10 lines of the file

**Step 3: Commit**

```bash
git add content/courses/flutter/CURRENT_VERSIONS.md
git commit -m "docs(flutter): add CURRENT_VERSIONS.md reference file"
```

---

## Task 1.2: Create TROUBLESHOOTING.md Appendix

**Files:**
- Create: `content/courses/flutter/TROUBLESHOOTING.md`

**Step 1: Create the troubleshooting appendix**

```markdown
# Flutter Troubleshooting Guide

*Quick solutions to common issues encountered during the course.*

---

## A.1 Impeller Rendering Issues

Impeller is Flutter's default rendering engine (iOS since 3.29, Android API 29+ since 3.38).

### Symptoms
- Visual glitches or artifacts on specific Android devices
- Unexpected jank or stuttering
- Blank screens during rendering
- Performance worse than expected on certain hardware

### Quick Diagnosis

```bash
# Check if Impeller is active
flutter run --verbose 2>&1 | grep -i impeller
```

### Solutions

**Temporary disable (for testing):**
```bash
flutter run --no-enable-impeller
```

**Permanent disable in AndroidManifest.xml:**
```xml
<application ...>
    <meta-data
        android:name="io.flutter.embedding.android.EnableImpeller"
        android:value="false" />
</application>
```

**Known problematic hardware:**
- Some Exynos chips (auto-blocklisted, falls back to OpenGL)
- Older Mali GPUs without proper Vulkan support
- Devices with buggy Vulkan drivers

### When to Re-enable
After Flutter updates, test with Impeller enabled - driver issues often get fixed.

---

## A.2 Android Build Issues

### Gradle Version Mismatch

**Symptom:** `Could not determine the dependencies of task ':app:compileDebugJavaWithJavac'`

**Solution:**
```bash
cd android
./gradlew wrapper --gradle-version=8.14
cd ..
flutter clean
flutter pub get
```

### Java Version Issues

**Symptom:** `Unsupported class file major version` or Gradle compatibility errors

**Solution:** Flutter 3.38+ requires Java 17+

```bash
# Check Java version
java -version

# Set JAVA_HOME (Windows PowerShell)
$env:JAVA_HOME = "C:\Program Files\Eclipse Adoptium\jdk-17..."

# Set JAVA_HOME (Mac/Linux)
export JAVA_HOME=$(/usr/libexec/java_home -v 17)
```

### 16KB Page Size (Android 15+)

**Symptom:** App crashes on Android 15 devices

**Solution:** Ensure NDK r28+ is installed
```bash
flutter doctor -v  # Check NDK version
```

In `android/app/build.gradle`:
```gradle
android {
    ndkVersion = flutter.ndkVersion  // Uses Flutter's bundled NDK
}
```

### SDK/Build Tools Missing

**Symptom:** `Failed to find Build Tools revision X.X.X`

**Solution:**
```bash
# List available
sdkmanager --list | grep build-tools

# Install latest
sdkmanager "build-tools;35.0.0"
```

---

## A.3 iOS Build Issues

### CocoaPods Issues

**Symptom:** `Error running pod install`

**Solution:**
```bash
cd ios
rm -rf Pods Podfile.lock
pod cache clean --all
pod install --repo-update
cd ..
flutter clean
flutter pub get
```

### Xcode Version Mismatch

**Symptom:** `The iOS deployment target is set to X.X, but the range of supported deployment target versions is Y.Y to Z.Z`

**Solution:**
1. Open `ios/Podfile`
2. Update: `platform :ios, '12.0'` (or higher)
3. Run `pod install` again

### Signing Issues

**Symptom:** `No signing certificate` or `Provisioning profile doesn't match`

**Solution:**
1. Open `ios/Runner.xcworkspace` in Xcode
2. Select Runner target > Signing & Capabilities
3. Select your Team
4. Let Xcode manage signing automatically

---

## A.4 Serverpod Issues

### Docker Not Running

**Symptom:** `Cannot connect to the Docker daemon`

**Solution:**
- Start Docker Desktop
- Wait for it to fully initialize (check system tray icon)
- Retry: `docker compose up -d`

### Database Connection Failed

**Symptom:** `Connection refused` to PostgreSQL

**Solution:**
```bash
# Check containers are running
docker ps

# If not running
cd server
docker compose down
docker compose up -d

# Check logs
docker compose logs postgres
```

### Code Generation Errors

**Symptom:** `serverpod generate` fails

**Solution:**
```bash
# Clean and regenerate
dart pub get
serverpod generate --force

# If still failing, check model YAML syntax
# Common: missing fields, invalid types, improper indentation
```

---

## A.5 Common Runtime Errors

### Null Safety Violations

**Symptom:** `Null check operator used on a null value`

**Cause:** Using `!` on a nullable value that is actually null

**Solution:**
```dart
// Instead of
final value = maybeNull!;

// Use null-aware operators
final value = maybeNull ?? defaultValue;

// Or check first
if (maybeNull != null) {
  final value = maybeNull;
}
```

### Provider Scope Errors

**Symptom:** `ProviderNotFoundException` or `Bad state: No ProviderScope found`

**Solution:** Ensure `ProviderScope` wraps your app:
```dart
void main() {
  runApp(
    ProviderScope(
      child: MyApp(),
    ),
  );
}
```

### Async Gap Issues

**Symptom:** Widget state errors after async operations

**Solution:**
```dart
// Check mounted before setState
Future<void> fetchData() async {
  final result = await api.getData();
  if (!mounted) return;  // Important!
  setState(() {
    data = result;
  });
}
```

---

## Getting Help

1. **Flutter Doctor:** Always run `flutter doctor -v` first
2. **Clean Build:** `flutter clean && flutter pub get`
3. **Search Issues:** [github.com/flutter/flutter/issues](https://github.com/flutter/flutter/issues)
4. **Stack Overflow:** Tag with `flutter` and relevant keywords
5. **Discord:** Flutter Community Discord server
```

**Step 2: Verify file created**

Run: `Get-Content "content/courses/flutter/TROUBLESHOOTING.md" | Select-Object -First 10`
Expected: First 10 lines of the file

**Step 3: Commit**

```bash
git add content/courses/flutter/TROUBLESHOOTING.md
git commit -m "docs(flutter): add TROUBLESHOOTING.md appendix"
```

---

## Task 1.3: Update Lesson 0.5 - Add Impeller Troubleshooting

**Files:**
- Modify: `content/courses/flutter/course.json` (Lesson 0.5)

**Step 1: Extract current lesson content**

Run: `pwsh scripts/extract-lesson.ps1 -Course flutter -LessonId "0.5"`
Review the current content sections.

**Step 2: Add new WARNING section for Impeller**

Use the `apply-review-fix.ps1` script to add a new section:

```powershell
$impellerWarning = @{
    type = "WARNING"
    title = "Impeller Rendering Issues on Android"
    content = @"

**What is Impeller?**
Impeller is Flutter's modern rendering engine, replacing the older Skia renderer. It's enabled by default on iOS (since Flutter 3.29) and Android API 29+ (since Flutter 3.38).

**Why You Might See Issues:**
Most devices work perfectly, but some Android devices have GPU driver bugs that cause:
- Visual glitches or artifacts
- Unexpected stuttering
- Blank screens

**Quick Fix - Disable Impeller Temporarily:**
```bash
flutter run --no-enable-impeller
```

**If Issues Persist:**
See the full troubleshooting guide in `TROUBLESHOOTING.md` section A.1.

**Good News:** Flutter automatically falls back to OpenGL on devices with known issues (like some Exynos chips). Most users never encounter problems.

"@
} | ConvertTo-Json -Depth 5

pwsh scripts/apply-review-fix.ps1 -Course flutter -LessonId "0.5" -FixType addSection -Content $impellerWarning
```

**Step 3: Verify the update**

Run: `pwsh scripts/extract-lesson.ps1 -Course flutter -LessonId "0.5" | Select-String "Impeller"`
Expected: Should find the new Impeller warning content

**Step 4: Commit**

```bash
git add content/courses/flutter/course.json
git commit -m "fix(flutter): add Impeller troubleshooting to lesson 0.5"
```

---

## Task 1.4: Update Lesson 0.1 - Version-Agnostic SDK Instructions

**Files:**
- Modify: `content/courses/flutter/course.json` (Lesson 0.1)

**Step 1: Extract current lesson**

Run: `pwsh scripts/extract-lesson.ps1 -Course flutter -LessonId "0.1"`

**Step 2: Identify hardcoded versions**

Look for specific version numbers like `3.x.x`, `android-34`, `build-tools;34.0.0`

**Step 3: Update content to be version-agnostic**

Add a KEY_POINT section referencing CURRENT_VERSIONS.md:

```powershell
$versionNote = @{
    type = "KEY_POINT"
    title = "About Version Numbers"
    content = @"

Throughout this course, we use phrases like "latest stable" instead of specific version numbers. This keeps the course current as Flutter evolves.

**To find current recommended versions:**
See `CURRENT_VERSIONS.md` in the course materials.

**Quick Check - Your Installed Versions:**
```bash
flutter --version
```

**General Rule:** Use the latest stable Flutter SDK. The course is tested with Flutter 3.38+ and Dart 3.10+, but newer versions should work fine.

"@
} | ConvertTo-Json -Depth 5

pwsh scripts/apply-review-fix.ps1 -Course flutter -LessonId "0.1" -FixType addSection -Content $versionNote
```

**Step 4: Commit**

```bash
git add content/courses/flutter/course.json
git commit -m "fix(flutter): add version-agnostic guidance to lesson 0.1"
```

---

## Task 1.5: Update Course Metadata

**Files:**
- Modify: `content/courses/flutter/course.json` (root object)

**Step 1: Read current metadata**

```powershell
$course = Get-Content "content/courses/flutter/course.json" -Raw | ConvertFrom-Json
Write-Host "Title: $($course.title)"
Write-Host "Description: $($course.description)"
Write-Host "Estimated Hours: $($course.estimatedHours)"
```

**Step 2: Update course title and description**

```powershell
$course = Get-Content "content/courses/flutter/course.json" -Raw | ConvertFrom-Json

$course.title = "Flutter & Dart Full-Stack Development Course"
$course.description = "Master Flutter frontend development AND Dart backend programming. Build complete, production-ready applications with 100+ interactive lessons covering UI, state management, Dart Frog, Serverpod, testing, deployment, and a social app capstone."
$course.estimatedHours = 150  # Updated for expanded content

# Create backup
Copy-Item "content/courses/flutter/course.json" "content/courses/flutter/course.json.bak"

# Save updated course
$course | ConvertTo-Json -Depth 30 | Out-File "content/courses/flutter/course.json" -Encoding UTF8
```

**Step 3: Verify update**

```powershell
$course = Get-Content "content/courses/flutter/course.json" -Raw | ConvertFrom-Json
Write-Host "New Title: $($course.title)"
```

**Step 4: Commit**

```bash
git add content/courses/flutter/course.json
git commit -m "feat(flutter): update course title and description for full-stack focus"
```

---

# PHASE 2: ARCHITECTURE & STATE (Module 5 Consolidation)

## Task 2.1: Create Module 5 Structure - MVVM with Riverpod

**Files:**
- Modify: `content/courses/flutter/course.json`

**Step 1: Define the new module structure**

Module 5 consolidates existing state management content (old Modules 5 + 13) into a unified MVVM architecture module.

```powershell
$module5 = @{
    id = "module-05"
    title = "Module 5: MVVM Architecture with Riverpod"
    description = "Learn professional app architecture with the MVVM pattern and Riverpod state management. Structure your code for maintainability, testability, and team collaboration."
    difficulty = "intermediate"
    estimatedHours = 12
    lessons = @()  # Will be populated in subsequent tasks
}
```

**Step 2: Create Lesson 5.1 - Why Architecture Matters**

```json
{
  "id": "5.1",
  "title": "Module 5, Lesson 1: Why Architecture Matters",
  "moduleId": "module-05",
  "order": 1,
  "estimatedMinutes": 35,
  "difficulty": "intermediate",
  "contentSections": [
    {
      "type": "THEORY",
      "title": "The Spaghetti Code Problem",
      "content": "\nImagine you're building a house. Would you start nailing boards together randomly, or would you follow a blueprint?\n\nMost beginner apps are like houses built without blueprints. They work at first, but as they grow:\n- Adding new features breaks existing ones\n- Fixing one bug creates two more\n- Nobody (including you) can understand the code after a month\n- Testing becomes impossible\n\nThis chaotic code is called **spaghetti code** - everything is tangled together like a bowl of pasta.\n\n**Real Example - The Messy Way:**\n```dart\n// Everything in one widget - UI, logic, data, state\nclass ProfileScreen extends StatefulWidget {\n  @override\n  _ProfileScreenState createState() => _ProfileScreenState();\n}\n\nclass _ProfileScreenState extends State<ProfileScreen> {\n  User? user;\n  bool isLoading = true;\n  String? error;\n\n  @override\n  void initState() {\n    super.initState();\n    // API call directly in widget\n    http.get(Uri.parse('https://api.example.com/user'))\n      .then((response) {\n        setState(() {\n          user = User.fromJson(jsonDecode(response.body));\n          isLoading = false;\n        });\n      })\n      .catchError((e) {\n        setState(() {\n          error = e.toString();\n          isLoading = false;\n        });\n      });\n  }\n\n  void updateProfile() {\n    // More API calls mixed with UI\n    setState(() { isLoading = true; });\n    http.put(...).then(...);\n  }\n\n  @override\n  Widget build(BuildContext context) {\n    // 200 lines of UI mixed with logic\n  }\n}\n```\n\nThis widget does everything: fetches data, handles errors, manages state, and renders UI. Now imagine 50 screens like this.\n\n"
    },
    {
      "type": "THEORY",
      "title": "What is Architecture?",
      "content": "\n**Architecture** is how you organize your code into layers and components with clear responsibilities.\n\nGood architecture means:\n1. **Separation of Concerns** - Each piece does one thing well\n2. **Testability** - You can test logic without running the whole app\n3. **Maintainability** - Changes in one area don't break others\n4. **Readability** - New developers can understand the codebase\n\n**The Same Example - The Organized Way:**\n```dart\n// Model - just data\nclass User {\n  final String id;\n  final String name;\n  final String email;\n  // ...\n}\n\n// Repository - handles API calls\nclass UserRepository {\n  Future<User> getUser() async {\n    final response = await http.get(...);\n    return User.fromJson(jsonDecode(response.body));\n  }\n}\n\n// ViewModel - manages state and logic\nclass ProfileViewModel extends Notifier<AsyncValue<User>> {\n  @override\n  AsyncValue<User> build() {\n    _loadUser();\n    return AsyncValue.loading();\n  }\n\n  Future<void> _loadUser() async {\n    state = AsyncValue.loading();\n    state = await AsyncValue.guard(() => \n      ref.read(userRepositoryProvider).getUser()\n    );\n  }\n}\n\n// View - just UI\nclass ProfileScreen extends ConsumerWidget {\n  @override\n  Widget build(BuildContext context, WidgetRef ref) {\n    final userState = ref.watch(profileViewModelProvider);\n    return userState.when(\n      data: (user) => ProfileContent(user: user),\n      loading: () => LoadingSpinner(),\n      error: (e, _) => ErrorDisplay(message: e.toString()),\n    );\n  }\n}\n```\n\nNow each piece has one job. The View just displays. The ViewModel manages state. The Repository talks to APIs. The Model holds data.\n\n"
    },
    {
      "type": "KEY_POINT",
      "title": "Why This Matters For Your Career",
      "content": "\n**Professional teams expect architecture.** No company with more than one developer writes spaghetti code.\n\nWhen you interview:\n- \"How do you structure Flutter apps?\" is a common question\n- Code challenges are evaluated on architecture, not just functionality\n- Senior roles require demonstrating architectural decisions\n\n**Architecture also makes YOU faster:**\n- Finding bugs is easier when code is organized\n- Adding features doesn't require understanding everything\n- You can reuse patterns across projects\n\n"
    },
    {
      "type": "THEORY",
      "title": "Architecture Patterns Overview",
      "content": "\nSeveral patterns exist for organizing Flutter apps:\n\n| Pattern | Complexity | Best For |\n|---------|------------|----------|\n| No architecture | Low | Tiny apps, experiments |\n| MVVM | Medium | Most apps |\n| Clean Architecture | High | Large enterprise apps |\n| BLoC | Medium | Event-driven apps |\n\n**This course teaches MVVM** (Model-View-ViewModel) because:\n- It's the sweet spot of simplicity vs structure\n- It works perfectly with Riverpod\n- It scales from small to large apps\n- It's widely used in production Flutter apps\n\nOnce you master MVVM, learning other patterns is easy - they're variations on the same principles.\n\n"
    }
  ],
  "challenges": [
    {
      "type": "QUIZ",
      "id": "5.1-challenge-0",
      "title": "Architecture Concepts Quiz",
      "description": "Test your understanding of why architecture matters",
      "questions": [
        {
          "question": "What is the main problem with 'spaghetti code'?",
          "options": [
            "It runs too slowly",
            "Everything is tangled together, making changes risky",
            "It uses too much memory",
            "It doesn't compile"
          ],
          "correctAnswer": 1,
          "explanation": "Spaghetti code mixes concerns together, so changing one thing can break unrelated features."
        },
        {
          "question": "What does 'separation of concerns' mean?",
          "options": [
            "Keeping secrets in separate files",
            "Each piece of code has one clear responsibility",
            "Separating frontend from backend",
            "Using multiple programming languages"
          ],
          "correctAnswer": 1,
          "explanation": "Separation of concerns means each component focuses on one job - UI code handles display, logic code handles business rules, etc."
        }
      ],
      "difficulty": "beginner"
    }
  ]
}
```

**Step 3: Continue with remaining Module 5 lessons (5.2-5.10)**

Each lesson follows the same structure. Key lessons to create:

- 5.2: MVVM Pattern Explained
- 5.3: Project Structure (Feature-First Folders)
- 5.4: Riverpod Fundamentals
- 5.5: ViewModels with Notifier
- 5.6: AsyncValue & Loading States
- 5.7: Riverpod Generator
- 5.8: Dependency Injection
- 5.9: Flutter Hooks (Optional)
- 5.10: Mini-Project - Refactor Notes App

**Step 4: Commit after each lesson or batch of lessons**

```bash
git add content/courses/flutter/course.json
git commit -m "feat(flutter): add Module 5 MVVM architecture lessons"
```

---

# PHASE 3: DART BACKEND (NEW MODULES 7-9)

## Task 3.1: Create Module 7 - Dart Frog Fundamentals

**Files:**
- Modify: `content/courses/flutter/course.json`

**Step 1: Define Module 7 structure**

```json
{
  "id": "module-07",
  "title": "Module 7: Dart Frog Backend Fundamentals",
  "description": "Build your first Dart backend! Learn REST APIs, routing, middleware, authentication, and database integration using Dart Frog - a lightweight framework perfect for Flutter developers.",
  "difficulty": "intermediate",
  "estimatedHours": 10,
  "lessons": []
}
```

**Step 2: Create Lesson 7.1 - Why Dart on the Backend?**

This lesson must explain the full-stack Dart value proposition:

Key content sections:
1. THEORY: "The Full-Stack Dart Advantage" - One language, shared models, no context switching
2. THEORY: "Dart Backend Options" - Compare Dart Frog, Serverpod, Shelf
3. EXAMPLE: "Shared Code Between Frontend and Backend" - Show a model used in both
4. KEY_POINT: "When to Use Dart vs Other Backends" - Be honest about tradeoffs
5. Challenge: Quiz on full-stack concepts

**Step 3: Create Lesson 7.2 - Dart Frog Setup**

Key content sections:
1. THEORY: "Installing Dart Frog CLI"
2. THEORY: "Creating Your First Project" - `dart_frog create`
3. EXAMPLE: "Project Structure Tour" - Explain routes/, middleware/, main.dart
4. THEORY: "Hot Reload Development" - `dart_frog dev`
5. Challenge: Create and run a hello world Dart Frog app

**Step 4: Create Lessons 7.3-7.8**

Follow the same pattern for:
- 7.3: File-Based Routing
- 7.4: Request & Response
- 7.5: Middleware
- 7.6: Database Integration
- 7.7: Authentication (JWT)
- 7.8: Mini-Project - REST API

**Step 5: Commit**

```bash
git add content/courses/flutter/course.json
git commit -m "feat(flutter): add Module 7 Dart Frog fundamentals"
```

---

## Task 3.2: Create Module 8 - Serverpod Production Backend

**Files:**
- Modify: `content/courses/flutter/course.json`

**Step 1: Define Module 8 structure**

```json
{
  "id": "module-08",
  "title": "Module 8: Serverpod Production Backend",
  "description": "Level up to production-ready backends with Serverpod. Built-in ORM, authentication, real-time streams, file storage, and automatic Flutter client generation.",
  "difficulty": "intermediate",
  "estimatedHours": 14,
  "lessons": []
}
```

**Step 2: Create all 10 lessons (8.1-8.10)**

Key lessons requiring substantial content:
- 8.3: Models & Code Generation - YAML syntax, `serverpod generate`
- 8.5: Database & ORM - Relations, migrations, transactions
- 8.6: Authentication Module - Built-in providers, session management
- 8.7: Real-Time Streams - Server-to-client streaming
- 8.10: Mini-Project - Chat Backend

**Step 3: Commit**

```bash
git add content/courses/flutter/course.json
git commit -m "feat(flutter): add Module 8 Serverpod backend"
```

---

## Task 3.3: Create Module 9 - Backend Testing

**Files:**
- Modify: `content/courses/flutter/course.json`

**Step 1: Define Module 9 structure**

```json
{
  "id": "module-09",
  "title": "Module 9: Backend Testing",
  "description": "Write reliable backend code with comprehensive testing. Unit tests, route tests, database tests, and API contract testing.",
  "difficulty": "intermediate",
  "estimatedHours": 6,
  "lessons": []
}
```

**Step 2: Create all 5 lessons (9.1-9.5)**

- 9.1: Testing Philosophy (test pyramid)
- 9.2: Unit Testing Dart Code
- 9.3: Testing Dart Frog Routes
- 9.4: Testing Serverpod Endpoints
- 9.5: API Contract Testing

**Step 3: Commit**

```bash
git add content/courses/flutter/course.json
git commit -m "feat(flutter): add Module 9 backend testing"
```

---

# PHASE 4: FULL-STACK INTEGRATION (MODULES 10-12)

## Task 4.1: Rewrite Module 10 - API Integration & Auth Flows

**Files:**
- Modify: `content/courses/flutter/course.json`

This replaces the existing networking module with full-stack Dart focus.

**Step 1: Define Module 10 structure**

```json
{
  "id": "module-10",
  "title": "Module 10: API Integration & Auth Flows",
  "description": "Connect your Flutter frontend to your Dart backend. Type-safe API calls, authentication flows, secure token storage, and auth-guarded navigation.",
  "difficulty": "intermediate",
  "estimatedHours": 10,
  "lessons": []
}
```

**Step 2: Create all 8 lessons (10.1-10.8)**

Key lessons:
- 10.1: Connecting Flutter to Dart Backend (Serverpod client)
- 10.4-10.6: Auth flow lessons (registration, login, OAuth)
- 10.7: Auth-Guarded Navigation (Riverpod + GoRouter)

**Step 3: Commit**

```bash
git add content/courses/flutter/course.json
git commit -m "feat(flutter): rewrite Module 10 for full-stack integration"
```

---

## Task 4.2: Create Module 11 - Real-Time Features

## Task 4.3: Consolidate Module 12 - Offline-First & Persistence

(Follow same pattern as above)

---

# PHASE 5: PRODUCTION READINESS (MODULES 13-16)

## Task 5.1: Create Module 13 - Frontend Testing
## Task 5.2: Create Module 14 - Advanced UI
## Task 5.3: Create Module 15 - Deployment & DevOps
## Task 5.4: Create Module 16 - Production Operations

(Each follows the same lesson creation pattern)

---

# PHASE 6: CAPSTONE (MODULE 17)

## Task 6.1: Create Module 17 - Social/Chat App Capstone

**Files:**
- Modify: `content/courses/flutter/course.json`

**Step 1: Define Module 17 structure**

```json
{
  "id": "module-17",
  "title": "Module 17: Capstone - Social Chat Application",
  "description": "Build a complete social chat application from scratch. This capstone project integrates everything you've learned: authentication, real-time messaging, media uploads, offline support, and production deployment.",
  "difficulty": "advanced",
  "estimatedHours": 20,
  "lessons": []
}
```

**Step 2: Create all 12 capstone lessons (17.1-17.12)**

Each lesson builds on the previous:
- 17.1: Project Setup & Architecture (monorepo structure)
- 17.2: Backend Models & Database
- 17.3: Backend Auth Endpoints
- 17.4: Backend Posts & Comments API
- 17.5: Backend Real-Time Chat
- 17.6: Backend Media Upload
- 17.7: Frontend Auth Screens
- 17.8: Frontend Feed & Posts
- 17.9: Frontend Chat UI
- 17.10: Frontend Profile & Settings
- 17.11: Offline & Sync
- 17.12: Deploy & Launch

**Step 3: Commit**

```bash
git add content/courses/flutter/course.json
git commit -m "feat(flutter): add Module 17 social chat capstone"
```

---

# VALIDATION & FINAL STEPS

## Task 7.1: Validate JSON Structure

**Step 1: Parse and validate course.json**

```powershell
try {
    $course = Get-Content "content/courses/flutter/course.json" -Raw | ConvertFrom-Json
    Write-Host "Modules: $($course.modules.Count)"
    foreach ($module in $course.modules) {
        Write-Host "  $($module.id): $($module.lessons.Count) lessons"
    }
    Write-Host "JSON is valid!"
} catch {
    Write-Error "JSON validation failed: $_"
}
```

**Step 2: Check for required fields**

```powershell
$course = Get-Content "content/courses/flutter/course.json" -Raw | ConvertFrom-Json
$errors = @()

foreach ($module in $course.modules) {
    foreach ($lesson in $module.lessons) {
        if (-not $lesson.contentSections -or $lesson.contentSections.Count -eq 0) {
            $errors += "Lesson $($lesson.id) has no content sections"
        }
        if (-not $lesson.challenges -or $lesson.challenges.Count -eq 0) {
            $errors += "Lesson $($lesson.id) has no challenges"
        }
    }
}

if ($errors.Count -gt 0) {
    Write-Host "Validation errors:"
    $errors | ForEach-Object { Write-Host "  - $_" }
} else {
    Write-Host "All lessons have required content!"
}
```

## Task 7.2: Update Module Ordering

Ensure modules are in correct order (0-17) and lesson IDs are sequential.

## Task 7.3: Final Commit & Tag

```bash
git add -A
git commit -m "feat(flutter): complete full-stack Dart developer course restructure

- 17 modules covering frontend and backend
- 100+ lessons with complete content
- Dart Frog and Serverpod backend coverage
- Social/Chat app capstone
- Comprehensive testing coverage
- Production deployment and operations"

git tag -a v2.0.0-flutter-fullstack -m "Flutter Full-Stack Dart Developer Course"
```

---

# LESSON CONTENT TEMPLATE

Use this template for creating new lessons:

```json
{
  "id": "X.Y",
  "title": "Module X, Lesson Y: [Title]",
  "moduleId": "module-XX",
  "order": Y,
  "estimatedMinutes": 45,
  "difficulty": "beginner|intermediate|advanced",
  "contentSections": [
    {
      "type": "THEORY",
      "title": "[Opening Hook]",
      "content": "[Analogy or real-world connection. 2-3 paragraphs explaining the concept in plain language. Include a simple example.]"
    },
    {
      "type": "THEORY",
      "title": "[Technical Deep Dive]",
      "content": "[Detailed technical explanation. Show code examples. Explain WHY not just HOW.]"
    },
    {
      "type": "EXAMPLE",
      "title": "[Practical Example]",
      "content": "[Description of what this code does]",
      "code": "[Complete, runnable code example]",
      "language": "dart"
    },
    {
      "type": "KEY_POINT",
      "title": "[Key Takeaways]",
      "content": "[Bullet points of most important concepts to remember]"
    },
    {
      "type": "WARNING",
      "title": "[Common Mistakes]",
      "content": "[What to avoid and why]"
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "X.Y-challenge-0",
      "title": "[Challenge Title]",
      "description": "[What the student should build]",
      "instructions": "[Step-by-step instructions]",
      "starterCode": "[Minimal starter code with TODO comments]",
      "solution": "[Complete solution with comments explaining each part]",
      "language": "dart",
      "testCases": [
        {
          "id": "test-1",
          "description": "[What this test verifies]",
          "expectedOutput": "[Expected output string]",
          "isVisible": true
        }
      ],
      "hints": [
        { "level": 1, "text": "[Gentle nudge]" },
        { "level": 2, "text": "[More specific hint]" },
        { "level": 3, "text": "[Almost the answer]" }
      ],
      "commonMistakes": [
        {
          "mistake": "[What students often do wrong]",
          "consequence": "[What happens]",
          "correction": "[How to fix it]"
        }
      ],
      "difficulty": "beginner|intermediate|advanced"
    }
  ]
}
```

---

# ESTIMATED EFFORT

| Phase | Modules | New Lessons | Estimated Hours |
|-------|---------|-------------|-----------------|
| 1: Foundation Updates | 0 | 0 (updates only) | 4 |
| 2: Architecture | 5 | 10 | 15 |
| 3: Dart Backend | 7-9 | 23 | 35 |
| 4: Integration | 10-12 | 21 | 30 |
| 5: Production | 13-16 | 30 | 45 |
| 6: Capstone | 17 | 12 | 20 |
| **Total** | | **~96 new lessons** | **~150 hours** |

---

# EXECUTION NOTES

**Primary Rule Compliance:**
- Every lesson must completely convey the topic
- No stubs, placeholders, or TODOs
- All text must be complete and thorough
- Guide absolute beginners to full understanding

**Quality Checks:**
- Each lesson has 4-6 content sections minimum
- Each lesson has at least 1 challenge
- Code examples are complete and runnable
- Explanations use analogies for complex concepts
- Common mistakes are documented
