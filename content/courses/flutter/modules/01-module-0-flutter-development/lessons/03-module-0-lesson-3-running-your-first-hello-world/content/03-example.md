---
type: "EXAMPLE"
title: "Creating Your First Project"
---


### Step 1: Open VS Code

Launch Visual Studio Code (the editor we installed in the previous lesson).

### Step 2: Create a New Flutter Project

1. Press `Ctrl/Cmd + Shift + P` to open the command palette
2. Type: `Flutter: New Project`
3. Select **Application**
4. Choose a location on your computer (like a folder called "FlutterProjects")
5. Name your project: `hello_world` (must be lowercase with underscores, no spaces!)
6. Press Enter

VS Code will now create your project. This takes 30-60 seconds. You'll see a progress indicator.

### Step 3: Explore What Was Created

Look at the **Explorer** panel (left sidebar). You'll see a folder structure:


**The only file you need to know about right now is `lib/main.dart`**. This is where your app's code lives.



```dart
hello_world/
├── lib/
│   └── main.dart        ← This is YOUR code file
├── android/             ← Android-specific files
├── ios/                 ← iOS-specific files
├── web/                 ← Web-specific files
├── test/                ← Testing files
└── pubspec.yaml         ← Project configuration
```
