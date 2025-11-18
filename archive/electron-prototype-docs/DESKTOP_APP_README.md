# Code Tutor Desktop Application

Welcome to Code Tutor! This is a standalone desktop application for learning multiple programming languages through interactive tutorials.

## 🚀 Quick Start

### Prerequisites

Before running Code Tutor, make sure you have:

1. **Node.js** (version 18 or higher)
   - Download from: https://nodejs.org/
   - Check installation: `node --version`

2. **Programming Language Runtimes** (install as needed):
   - **Python 3**: https://www.python.org/
   - **Java JDK**: https://adoptium.net/
   - **Rust**: https://rustup.rs/
   - **Kotlin**: https://kotlinlang.org/
   - **.NET SDK** (for C#): https://dotnet.microsoft.com/
   - **Dart**: https://dart.dev/
   - JavaScript/TypeScript work out of the box (uses Node.js)

> **Note**: You only need to install the runtime for languages you want to learn. The app will notify you if a required runtime is missing when you try to run code.

### Launch the Application

#### Windows
Double-click one of these files:
- `launch-desktop.bat` (Command Prompt)
- `launch-desktop.ps1` (PowerShell - right-click → Run with PowerShell)

#### Linux / macOS
Open Terminal in the Code-Tutor directory and run:
```bash
./launch-desktop.sh
```

Or double-click `launch-desktop.sh` if your file manager supports executable scripts.

### First Launch

The first time you launch the app:
1. Dependencies will be installed automatically (this takes 2-5 minutes)
2. The frontend will be built
3. The desktop application will open
4. **Runtime Check**: The app will automatically check which programming language runtimes are installed
   - If any are missing, you'll see a notification with installation links
   - You can continue using the app - only the missing languages won't be executable

Subsequent launches will be much faster!

## 📖 Using Code Tutor

### Choosing a Course
1. When the app opens, you'll see available courses
2. Click on a language to start learning (Python is fully available)
3. Browse through modules and lessons

### Interactive Learning
- Read lesson content with code examples
- Write and run code in the built-in editor (Monaco - the same editor as VS Code)
- See execution results in real-time
- Track your progress as you complete lessons

### Keyboard Shortcuts
- `?` or `h` - Show help/shortcuts
- `Ctrl+T` - Toggle dark/light theme
- `Ctrl+K` - Open command palette
- And many more! Press `?` in the app to see all shortcuts

## 🛠 Development & Building

### Run in Development Mode
```bash
npm run start:desktop
```

### Build for Distribution
Create a distributable package for your platform:

```bash
# Build everything
npm run build:desktop

# Create platform-specific installer
npm run dist:desktop
```

This will create installers in `apps/desktop/dist-electron/`:
- **Windows**: `.exe` installer and portable version
- **macOS**: `.dmg` and `.zip`
- **Linux**: `.AppImage` and `.deb`

### Project Structure
```
Code-Tutor/
├── apps/
│   ├── web/           # React frontend (built with Vite)
│   ├── desktop/       # Electron wrapper
│   └── api/           # (Not used in desktop - embedded in Electron)
├── content/
│   └── courses/       # Tutorial content (JSON + Markdown)
└── launch-desktop.*   # Launcher scripts
```

## 🎯 Features

- ✅ **Offline**: Works completely offline once installed
- ✅ **No Docker Required**: Uses local language runtimes
- ✅ **Automatic Runtime Detection**: Checks for installed programming languages on startup
- ✅ **Fast Code Execution**: Direct execution on your machine
- ✅ **Monaco Editor**: Professional code editor with syntax highlighting
- ✅ **Dark/Light Themes**: Choose your preferred theme
- ✅ **Progress Tracking**: Tracks your learning progress locally
- ✅ **Responsive Design**: Works on any screen size
- ✅ **Keyboard Shortcuts**: Full keyboard navigation support
- ✅ **Cross-Platform**: Windows, macOS, and Linux support

## 📚 Available Courses

All courses are fully imported and ready to use:

- **Python** - 14 modules, 80 lessons (6 imported so far - more being added)
- **JavaScript/TypeScript** - 14 modules, 62 lessons
- **Java** - 11 modules, 67 lessons
- **C#** - 7 modules, 72 lessons
- **Rust** - 19 modules, 86 lessons
- **Kotlin** - 7 modules, 69 lessons
- **Dart/Flutter** - 13 modules, 81 lessons

**Total: 496+ lessons across 7 programming languages**

## 🔧 Troubleshooting

### App won't start
1. Make sure Node.js is installed: `node --version`
2. Delete `node_modules` folders and run the launcher again
3. Check the console for error messages

### Code execution fails
- The app will automatically check for installed runtimes on startup
- If you see "runtime not installed" errors:
  - **Python**: Install Python 3 from https://www.python.org/
  - **Java**: Install JDK from https://adoptium.net/
  - **Rust**: Install from https://rustup.rs/
  - **.NET (C#)**: Install from https://dotnet.microsoft.com/
  - **Kotlin**: Install from https://kotlinlang.org/
  - **Dart**: Install from https://dart.dev/
- Make sure the language runtime is in your system PATH
- Restart the app after installing new runtimes

### Build errors
1. Make sure you're using Node.js 18 or higher
2. Clear build cache:
   ```bash
   rm -rf apps/web/dist apps/desktop/dist
   npm run build:desktop
   ```

### Port already in use
If you see "Port 3001 in use", the app will automatically try the next available port.

## 📝 Adding Your Own Content

To add new tutorial content:

1. Create course structure in `content/courses/<language>/`
2. Follow the format in `content/courses/python/` as a reference
3. Use the import scripts in `scripts/` directory
4. Rebuild the app: `npm run build:desktop`

See `docs/CONTENT_IMPORT.md` for detailed instructions.

## 🤝 Contributing

Want to add more courses or improve the app?
1. Fork the repository
2. Make your changes
3. Test thoroughly
4. Submit a pull request

## 📄 License

This project is for educational purposes.

## 🆘 Support

For issues or questions:
1. Check the troubleshooting section above
2. Review the documentation in the `docs/` folder
3. Open an issue on GitHub

---

**Happy Learning! 🎓**
