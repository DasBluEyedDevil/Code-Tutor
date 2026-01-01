# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Flutter Complete Development Course (dart)
- **Module:** Module 0: Flutter Development
- **Lesson:** Module 0, Lesson 5: Troubleshooting Common Setup Issues (ID: 0.5)
- **Difficulty:** beginner
- **Estimated Time:** 60 minutes

## Current Lesson Content

{
    "id":  "0.5",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "When Things Go Wrong",
                                "content":  "\nEven experienced developers encounter setup issues. The good news? Most problems have simple solutions, and you\u0027re not alone!\n\nThink of troubleshooting like being a detective:\n- **The crime**: Your app won\u0027t run\n- **The clues**: Error messages\n- **The solution**: Following the evidence\n\nThis lesson teaches you how to solve the most common problems you\u0027ll face.\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "The #1 Troubleshooting Tool: flutter doctor",
                                "content":  "\nThis command checks your entire setup and tells you what\u0027s wrong.\n\n\n**What it checks**:\n- ✅ Is Flutter installed?\n- ✅ Is Dart available?\n- ✅ Are Android tools installed?\n- ✅ Is Xcode available? (Mac)\n- ✅ Are there any missing dependencies?\n\n**How to read the output**:\n\n\n- **[✓]**: Working perfectly\n- **[!]**: Working but with warnings\n- **[✗]**: Not working, needs fixing\n\n",
                                "code":  "[✓] Flutter (Channel stable, 3.24.0)\n[✗] Android toolchain - develop for Android devices\n    ✗ Android SDK not found\n[!] Xcode - develop for iOS and macOS (Xcode 15.0)\n    ✗ CocoaPods not installed\n[✓] Chrome - develop for the web\n[✓] VS Code (version 1.85.0)",
                                "language":  "dart"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Problem 1: \"flutter: command not found\"",
                                "content":  "\n### What it means:\nYour computer doesn\u0027t know where Flutter is installed.\n\n### Solution (Windows):\n\n\n### Solution (Mac/Linux):\n\n\n",
                                "code":  "# Find where you installed Flutter\nls ~/flutter/bin/flutter\n\n# Add to PATH (Mac with zsh)\necho \u0027export PATH=\"$PATH:$HOME/flutter/bin\"\u0027 \u003e\u003e ~/.zshrc\nsource ~/.zshrc\n\n# Or for bash\necho \u0027export PATH=\"$PATH:$HOME/flutter/bin\"\u0027 \u003e\u003e ~/.bashrc\nsource ~/.bashrc\n\n# Test\nflutter --version",
                                "language":  "bash"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Problem 2: Android License Not Accepted",
                                "content":  "\n### Error message:\n\n### Solution:\n\n\nIf this doesn\u0027t work:\n\n1. Open Android Studio\n2. Go to **Settings** → **Appearance \u0026 Behavior** → **System Settings** → **Android SDK**\n3. Click **SDK Tools** tab\n4. Check **Android SDK Command-line Tools**\n5. Click **Apply**\n\nThen run `flutter doctor --android-licenses` again.\n\n",
                                "code":  "# Accept all Android licenses\nflutter doctor --android-licenses\n\n# Type \u0027y\u0027 and press Enter for each license",
                                "language":  "bash"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Problem 3: \"Waiting for another flutter command...\"",
                                "content":  "\n### Error message:\n\n### What happened:\nA previous Flutter command didn\u0027t finish properly and left a lock file.\n\n### Solution:\n\n\n",
                                "code":  "# Kill the lock file\ncd \u003cyour-flutter-installation\u003e\nrm -f bin/cache/lockfile\n\n# Windows PowerShell:\nRemove-Item -Force bin/cache/lockfile\n\n# Or just restart your computer (easiest)",
                                "language":  "bash"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Problem 4: Emulator Won\u0027t Start",
                                "content":  "\n### Symptom:\nEmulator starts but shows a black screen or crashes.\n\n### Solution 1: Enable Hardware Acceleration\n\n**Windows**:\n1. Open **Task Manager** → **Performance**\n2. Check if \"Virtualization\" is enabled\n3. If not, enable Intel VT-x or AMD-V in BIOS\n\n**Mac**:\nHardware acceleration is enabled by default.\n\n**Linux**:\n\n### Solution 2: Allocate More RAM\n\n1. Open Android Studio\n2. **Tools** → **Device Manager**\n3. Click the pencil icon (Edit) on your emulator\n4. Click **Show Advanced Settings**\n5. Increase RAM to at least 2048 MB\n6. Click **Finish**\n\n### Solution 3: Use a Different System Image\n\nSome system images work better than others:\n- Try **API 33** (Android 13) instead of the latest\n- Use **x86_64** images (faster than ARM)\n\n",
                                "code":  "# Install KVM\nsudo apt-get install qemu-kvm libvirt-daemon-system libvirt-clients bridge-utils\n\n# Add yourself to the kvm group\nsudo adduser $USER kvm",
                                "language":  "bash"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Problem 5: App Builds But Crashes Immediately",
                                "content":  "\n### Check 1: Clean and Rebuild\n\n\n### Check 2: Check for Errors in Code\n\nLook at the terminal output. Common errors:\n\n↳ Missing semicolon\n\n↳ Missing import: `import \u0027package:flutter/material.dart\u0027;`\n\n↳ Type mismatch - check your variables\n\n",
                                "code":  "Error: The argument type \u0027int\u0027 can\u0027t be assigned to \u0027String\u0027",
                                "language":  "dart"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Problem 6: Hot Reload Doesn\u0027t Work",
                                "content":  "\n### Symptoms:\n- You save changes but nothing updates\n- App needs full restart every time\n\n### Solutions:\n\n**1. Make sure you\u0027re editing the right file**\n- Are you editing `lib/main.dart`?\n- Not a file in `android/` or `ios/`?\n\n**2. Try Hot Restart instead**\n- Press `Ctrl/Cmd + Shift + F5`\n- Or click the circular arrow icon\n\n**3. Check for errors**\n- Look at the terminal for error messages\n- Fix any syntax errors\n\n**4. Full restart**\n\n",
                                "code":  "# Stop the app\nq (in terminal)\n\n# Clean\nflutter clean\n\n# Run again\nflutter run",
                                "language":  "bash"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Problem 7: VS Code Not Finding Flutter",
                                "content":  "\n### Symptoms:\n- \"Dart\" or \"Flutter\" commands not available\n- No syntax highlighting\n- Can\u0027t run apps from VS Code\n\n### Solution:\n\n1. **Install Flutter Extension**:\n   - Press `Ctrl/Cmd + Shift + X`\n   - Search \"Flutter\"\n   - Install the official Flutter extension\n\n2. **Set Flutter SDK Path**:\n   - Press `Ctrl/Cmd + Shift + P`\n   - Type \"Flutter: Change SDK\"\n   - Select your Flutter installation path\n\n3. **Restart VS Code**:\n   - Close and reopen VS Code\n   - Check if Flutter commands work\n\n"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Problem 8: Gradle Build Fails (Android)",
                                "content":  "\n### Error message:\n\n### Solution 1: Update Gradle\n\nEdit `android/build.gradle`:\n\n\n### Solution 2: Clear Gradle Cache\n\n\n### Solution 3: Update Java Version\n\nFlutter requires Java 11 or higher:\n\n\n",
                                "code":  "# Check Java version\njava -version\n\n# If it\u0027s older than 11, download from:\n# https://adoptium.net/",
                                "language":  "bash"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Problem 9: Pod Install Fails (iOS/Mac)",
                                "content":  "\n### Error message:\n\n### Solution:\n\n\n",
                                "code":  "# Install CocoaPods\nsudo gem install cocoapods\n\n# Set up pods\npod setup\n\n# Then from your project:\ncd ios\npod install\ncd ..\n\n# Try running again\nflutter run",
                                "language":  "bash"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Problem 10: \"Version Solving Failed\"",
                                "content":  "\n### Error message:\n\n### Solution:\n\n\n",
                                "code":  "# Update all packages\nflutter pub upgrade\n\n# If that doesn\u0027t work, delete lock file\nrm pubspec.lock\n\n# Get fresh dependencies\nflutter pub get",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "The Nuclear Option: Complete Reset",
                                "content":  "\nIf nothing else works, start fresh:\n\n\n",
                                "code":  "# 1. Clean everything\nflutter clean\n\n# 2. Delete build files\nrm -rf build/\nrm -rf .dart_tool/\n\n# 3. Reset pub cache\nflutter pub cache repair\n\n# 4. Get dependencies\nflutter pub get\n\n# 5. Run\nflutter run",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Getting Help When Stuck",
                                "content":  "\n### Official Resources:\n- **Flutter Docs**: https://docs.flutter.dev\n- **Flutter GitHub Issues**: https://github.com/flutter/flutter/issues\n- **Stack Overflow**: Tag your question with `[flutter]`\n\n### Search Strategy:\n1. Copy the exact error message\n2. Google: \"flutter [your error message]\"\n3. Look for recent results (last 1-2 years)\n4. Try the top 3 solutions\n\n### Ask for Help:\nWhen asking questions, include:\n- Exact error message (full output)\n- Output of `flutter doctor -v`\n- What you\u0027ve already tried\n- Code snippet (if relevant)\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Common Error Patterns",
                                "content":  "\n| If you see... | It usually means... |\n|---------------|---------------------|\n| `command not found` | PATH not set correctly |\n| `licenses not accepted` | Run `flutter doctor --android-licenses` |\n| `version solving failed` | Package conflict - run `flutter pub upgrade` |\n| `gradle build failed` | Android build issue - clean and rebuild |\n| `pod install failed` | iOS/Mac dependency issue - reinstall CocoaPods |\n| `waiting for lock` | Previous Flutter command stuck - restart |\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What Did We Learn?",
                                "content":  "\nLet\u0027s recap:\n- ✅ `flutter doctor` is your best friend\n- ✅ Most errors have simple solutions\n- ✅ Clean and rebuild fixes many issues\n- ✅ Error messages tell you what\u0027s wrong\n- ✅ Google is your ally\n- ✅ The Flutter community is helpful!\n\n"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "🛠️ Flutter DevTools - Your Advanced Debugging Suite",
                                "content":  "\nFlutter comes with powerful debugging tools called **Flutter DevTools**. Think of it as X-ray vision for your app!\n\n### What is DevTools?\n\nDevTools is a browser-based suite of debugging and profiling tools built specifically for Flutter and Dart.\n\n### Launching DevTools\n\n**Option 1: From VS Code**\n1. Run your app (`F5` or `flutter run`)\n2. Open Command Palette (`Ctrl/Cmd + Shift + P`)\n3. Type \"Dart: Open DevTools\"\n4. Choose which tool to open\n\n**Option 2: From Terminal**\n```bash\n# Run app and get observatory URL\nflutter run\n\n# In another terminal:\nflutter pub global activate devtools\nflutter pub global run devtools\n```\n\n**Option 3: From Browser**\nWhen running `flutter run`, look for:\n```\nFlutter DevTools: http://127.0.0.1:9100?uri=...\n```\n\n### DevTools Tabs Explained\n\n#### 1. 🔍 Widget Inspector (Most Important!)\n\nSee your entire widget tree visually:\n\n```\n🌳 Widget Tree View:\n\nMaterialApp\n └─ Scaffold\n     ├─ AppBar\n     │   └─ Text: \"My App\"\n     └─ Center\n         └─ Column\n             ├─ Text: \"Hello\"\n             └─ ElevatedButton\n```\n\n**What you can do:**\n- Click any widget to see its properties\n- See padding, margins, and constraints\n- Find layout issues (overflow, wrong sizes)\n- Select widgets directly on the device/emulator\n\n**Pro Tip**: Click the \"Select Widget Mode\" button, then tap any widget in your app to jump directly to it in the tree!\n\n#### 2. 📊 Performance Overlay\n\nMonitor your app\u0027s performance in real-time:\n\n```\n┌─────────────────────────────────┐\n│ UI Thread:  ████░░░░  16ms    │ ← Should be under 16ms\n│ GPU Thread: ██░░░░░░  8ms     │ ← Should be under 16ms\n└─────────────────────────────────┘\n```\n\n**Enable from code:**\n```dart\nMaterialApp(\n  showPerformanceOverlay: true,  // 👈 Add this!\n  home: MyHomePage(),\n)\n```\n\n**What to look for:**\n- Green bars = Good (under 16ms = 60 FPS)\n- Red bars = Bad (frame dropped, causes jank)\n- Spikes = Potential performance issue\n\n#### 3. 🧠 Memory Tab\n\nTrack memory usage and find leaks:\n\n- See live memory graph\n- Detect memory leaks\n- Take heap snapshots\n- Compare allocations\n\n**Warning signs:**\n- Memory constantly increasing = potential leak\n- Spikes during specific actions = heavy operations\n\n#### 4. 🌐 Network Tab\n\nMonitor all HTTP requests:\n\n```\nMethod | URL                    | Status | Time\n-------+------------------------+--------+------\nGET    | api.example.com/users  | 200    | 120ms\nPOST   | api.example.com/login  | 401    | 85ms ❌\nGET    | api.example.com/posts  | 200    | 95ms\n```\n\n**What you can inspect:**\n- Request headers and body\n- Response data\n- Timing (how long each request takes)\n- Failed requests\n\n#### 5. 🐛 Debugger\n\nSet breakpoints and step through code:\n- Pause at any line\n- Inspect variables\n- Step in/out/over functions\n- Watch expressions\n\n#### 6. 📝 Logging\n\nView all logs from your app:\n\n```dart\nimport \u0027dart:developer\u0027 as developer;\n\n// These show in DevTools Logging tab\ndeveloper.log(\u0027User logged in\u0027, name: \u0027Auth\u0027);\ndeveloper.log(\u0027Fetched 10 items\u0027, name: \u0027API\u0027);\n```\n\n### Quick Debugging Shortcuts (VS Code)\n\n| Shortcut | Action |\n|----------|--------|\n| `F5` | Start debugging |\n| `F9` | Toggle breakpoint |\n| `F10` | Step over |\n| `F11` | Step into |\n| `Shift+F11` | Step out |\n| `Ctrl+Shift+D` | Open Debug panel |\n\n### Debugging Layout Issues with Inspector\n\n**Problem**: Your widget is in the wrong place or the wrong size.\n\n**Solution**:\n1. Open Widget Inspector\n2. Click \"Select Widget Mode\" (crosshair icon)\n3. Tap the problematic widget in your app\n4. In DevTools, look at:\n   - **Constraints**: What size was it told it can be?\n   - **Size**: What size did it actually choose?\n   - **Parent**: Who gave it those constraints?\n\n```\nConstraints: BoxConstraints(0.0\u003c=w\u003c=400.0, 0.0\u003c=h\u003c=600.0)\nActual Size: Size(200.0, 50.0)\nParent: Center → gives tight constraints from parent\n```\n\n### Performance Debugging Workflow\n\n1. **Enable Performance Overlay**\n2. **Use your app normally**\n3. **Watch for red bars** (dropped frames)\n4. **Open Timeline View** in DevTools\n5. **Record** the problematic action\n6. **Analyze** what\u0027s taking too long:\n   - Build? → Too many widgets rebuilding\n   - Layout? → Expensive layout calculations\n   - Paint? → Complex graphics\n\n**Common fixes:**\n- Add `const` constructors\n- Use `ListView.builder` instead of `ListView`\n- Cache expensive calculations\n- Use `RepaintBoundary` for complex animations\n\n### DevTools Cheat Sheet\n\n| I want to... | Use... |\n|--------------|--------|\n| Find why layout is wrong | Widget Inspector |\n| Fix slow animations | Performance tab |\n| Find memory leaks | Memory tab |\n| Debug API calls | Network tab |\n| Set breakpoints | Debugger tab |\n| View logs | Logging tab |\n| Profile CPU usage | CPU Profiler tab |\n\n**Bookmark this**: https://docs.flutter.dev/tools/devtools\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What\u0027s Next?",
                                "content":  "\n**Congratulations!** You\u0027ve completed Module 0! Your development environment is set up, and you know how to troubleshoot problems.\n\nIn **Module 1**, we\u0027ll dive into the Dart programming language. You\u0027ll learn:\n- How to store information (variables)\n- How to make decisions (if/else)\n- How to repeat actions (loops)\n- How to organize code (functions)\n\nAll taught interactively with lots of hands-on practice!\n\nReady to start coding? Let\u0027s go! 🚀\n\n"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "0.5-challenge-0",
                           "title":  "Practice Challenge",
                           "description":  "1. **Run flutter doctor**:    ```bash    flutter doctor -v    ```    The `-v` flag shows more details. Take a screenshot! 2. **Check Flutter version**:    ```bash    flutter --version    ``` 3. **List connected devices**:    ```bash    flutter devices    ``` 4. **Intentionally break something**:    - Open your hello_world app    - Delete a semicolon `;` somewhere    - Try to run it    - Read the error message    - Fix it! ---",
                           "instructions":  "1. **Run flutter doctor**:    ```bash    flutter doctor -v    ```    The `-v` flag shows more details. Take a screenshot! 2. **Check Flutter version**:    ```bash    flutter --version    ``` 3. **List connected devices**:    ```bash    flutter devices    ``` 4. **Intentionally break something**:    - Open your hello_world app    - Delete a semicolon `;` somewhere    - Try to run it    - Read the error message    - Fix it! ---",
                           "starterCode":  "// Practice Challenge\n// Write your code below\n\nvoid main() {\n    \n}",
                           "solution":  "// Solution: Troubleshooting and Debugging\n// This challenge teaches you to use diagnostic commands and read errors.\n//\n// Terminal commands covered:\n//\n// 1. Detailed doctor output:\n//    flutter doctor -v\n//\n// 2. Version check:\n//    flutter --version\n//\n// 3. List devices:\n//    flutter devices\n//\n// 4. Clean rebuild (useful for fixing issues):\n//    flutter clean \u0026\u0026 flutter pub get\n//\n// Example of code WITH an error (missing semicolon):\n\n// BROKEN CODE (will cause error):\n// void main() {\n//   print(\u0027Hello\u0027)  // \u003c- Missing semicolon!\n// }\n//\n// Error message you\u0027ll see:\n// Error: Expected \u0027;\u0027 after this.\n//   print(\u0027Hello\u0027)\n//                 ^\n\n// FIXED CODE:\nvoid main() {\n  print(\u0027Hello\u0027);  // \u003c- Semicolon added!\n  \n  // Common syntax errors and fixes:\n  \n  // 1. Missing semicolon\n  // Error: Expected \u0027;\u0027 after this\n  // Fix: Add ; at end of statement\n  \n  // 2. Mismatched brackets\n  // Error: Expected \u0027)\u0027 or \u0027identifier\u0027\n  // Fix: Check all opening brackets have closing ones\n  \n  // 3. Undefined variable\n  // Error: Undefined name \u0027variableName\u0027\n  // Fix: Declare the variable before using it\n  \n  // 4. Type mismatch\n  // Error: A value of type \u0027X\u0027 can\u0027t be assigned to \u0027Y\u0027\n  // Fix: Use correct types or add type conversion\n  \n  print(\u0027Troubleshooting complete!\u0027);\n  print(\u0027Remember: Error messages tell you WHAT is wrong and WHERE!\u0027);\n}",
                           "language":  "dart",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Program prints hello message",
                                                 "expectedOutput":  "Hello",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Program confirms troubleshooting complete",
                                                 "expectedOutput":  "Troubleshooting complete!",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-3",
                                                 "description":  "Program explains error messages",
                                                 "expectedOutput":  "Remember: Error messages tell you WHAT is wrong and WHERE!",
                                                 "isVisible":  false
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  2,
                                             "text":  "Use an if statement to check the condition."
                                         },
                                         {
                                             "level":  3,
                                             "text":  "If stuck, try writing out the solution in plain English first, then convert to dart code."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting semicolons",
                                                      "consequence":  "Syntax error",
                                                      "correction":  "Add ; at end of statements"
                                                  },
                                                  {
                                                      "mistake":  "Not handling null safety",
                                                      "consequence":  "Null check operator errors",
                                                      "correction":  "Use ? for nullable types, ! for assertion"
                                                  },
                                                  {
                                                      "mistake":  "Forgetting async/await",
                                                      "consequence":  "Future not awaited",
                                                      "correction":  "Add async to function, await before Future"
                                                  }
                                              ],
                           "difficulty":  "beginner"
                       }
                   ],
    "difficulty":  "beginner",
    "title":  "Module 0, Lesson 5: Troubleshooting Common Setup Issues",
    "estimatedMinutes":  60
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current dart documentation
- Search the web for the latest dart version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "dart Module 0, Lesson 5: Troubleshooting Common Setup Issues 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "0.5",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

