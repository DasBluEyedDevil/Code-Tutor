# Code Tutor Desktop - Quick Start Guide

## 🚀 Launch the Application

### The Easy Way

**Double-click the launcher script for your platform:**

- **Windows**: `launch-desktop.bat` or `launch-desktop.ps1`
- **Linux/Mac**: `launch-desktop.sh`

That's it! The app will:
1. Install dependencies (first time only - takes 2-5 minutes)
2. Build the frontend (first time only)
3. Launch the desktop application

### From Command Line

```bash
# From the project root
npm run start:desktop
```

## 📋 Prerequisites

1. **Node.js 18+** (required)
   - Download: https://nodejs.org/

2. **Programming Language Runtimes** (optional - install only what you want to learn):
   - **Python 3**: https://www.python.org/ (for Python tutorials)
   - **Java JDK**: https://adoptium.net/ (for Java tutorials)
   - **Rust**: https://rustup.rs/ (for Rust tutorials)
   - **.NET SDK**: https://dotnet.microsoft.com/ (for C# tutorials)
   - **Kotlin**: https://kotlinlang.org/ (for Kotlin tutorials)
   - **Dart**: https://dart.dev/ (for Dart/Flutter tutorials)
   - JavaScript/TypeScript: Built-in with Node.js ✓

> **Note**: The app will notify you if a language runtime is missing when you try to execute code.

## 📚 Using the App

1. **Select a Course**: Choose from available programming languages
2. **Browse Lessons**: Navigate through modules and lessons
3. **Learn Interactively**: Read content, write code, and see results instantly
4. **Track Progress**: Your progress is saved automatically

## ⌨️ Keyboard Shortcuts

- `?` or `h` - Show all shortcuts
- `Ctrl+T` - Toggle dark/light theme
- `Ctrl+K` - Open command palette
- `F11` - Toggle fullscreen

## 🛠️ Building for Distribution

Create an installable package:

```bash
npm run dist:desktop
```

Installers will be created in `apps/desktop/dist-electron/`:
- **Windows**: `.exe` and portable
- **macOS**: `.dmg` and `.zip`
- **Linux**: `.AppImage` and `.deb`

## 🔧 Troubleshooting

**App won't start?**
- Make sure Node.js 18+ is installed: `node --version`
- Delete `node_modules` folders and run the launcher again

**Code won't execute?**
- Install the required language runtime (see prerequisites above)
- Make sure it's in your system PATH

**Build failed?**
```bash
# Clean and rebuild
rm -rf apps/web/dist apps/desktop/dist
npm run build:desktop
```

## 📖 More Documentation

See `DESKTOP_APP_README.md` for detailed documentation.

---

**Happy Learning! 🎓**
