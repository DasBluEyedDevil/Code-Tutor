---
type: "THEORY"
title: "Installing Dart Frog CLI"
---


Dart Frog comes with a command-line interface (CLI) that handles project creation, development, and building. Let's install it.

> **Community Maintenance Notice:** Dart Frog transitioned to community maintenance in July 2025. Previously maintained by Very Good Ventures (VGV), it is now under the [dart-frog-dev](https://github.com/dart-frog-dev) GitHub organization. The core API is unchanged and the same maintainers continue contributing. All APIs taught in this module work with dart_frog 1.2.x.

**Prerequisites**:
- Dart SDK installed (you already have this from Flutter!)
- A terminal/command prompt

**Installation Command**:

Open your terminal and run:

```bash
dart pub global activate dart_frog_cli
```

**What This Does**:
- `dart pub global activate` installs a Dart package globally on your system
- `dart_frog_cli` is the Dart Frog command-line tool
- After installation, you can use `dart_frog` commands from anywhere

**Verify Installation**:

```bash
dart_frog --version
```

You should see something like:
```
dart_frog_cli 1.x.x
```

**Troubleshooting**:

If you see "command not found" or "not recognized":

1. **Check your PATH**: The Dart pub cache bin directory needs to be in your PATH
   - Windows: `%USERPROFILE%\AppData\Local\Pub\Cache\bin`
   - Mac/Linux: `~/.pub-cache/bin`

2. **Restart your terminal**: PATH changes require a new terminal session

3. **Reinstall**: Try the activation command again

