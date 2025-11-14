# Code Tutor Desktop - Distribution Guide

**Version:** 1.0.0
**Release Date:** November 14, 2025

---

## Overview

Code Tutor Desktop is a standalone application for interactive coding tutorials across 7 programming languages. This guide explains how to build, distribute, and install the application on Windows, macOS, and Linux.

---

## Table of Contents

1. [Quick Start](#quick-start)
2. [System Requirements](#system-requirements)
3. [Installation Instructions](#installation-instructions)
4. [Building from Source](#building-from-source)
5. [Distribution Files](#distribution-files)
6. [Troubleshooting](#troubleshooting)
7. [Technical Details](#technical-details)

---

## Quick Start

### For End Users

1. Download the appropriate installer for your platform
2. Run the installer and follow the prompts
3. Launch "Code Tutor" from your applications menu
4. Select a programming language and start learning!

### For Developers

```bash
# Clone the repository
git clone https://github.com/DasBluEyedDevil/Code-Tutor.git
cd Code-Tutor

# Install dependencies
npm install

# Build and create installers
./build-installers.sh  # Linux/macOS
build-installers.bat   # Windows
```

---

## System Requirements

### Minimum Requirements

- **CPU:** Dual-core processor (2.0 GHz+)
- **RAM:** 4 GB
- **Storage:** 500 MB free space
- **Display:** 1280x720 resolution

### Recommended Requirements

- **CPU:** Quad-core processor (2.5 GHz+)
- **RAM:** 8 GB+
- **Storage:** 1 GB free space
- **Display:** 1920x1080 resolution

### Operating Systems

- **Windows:** Windows 10/11 (64-bit)
- **macOS:** macOS 10.15 (Catalina) or later
- **Linux:** Ubuntu 20.04+, Debian 10+, Fedora 34+, or equivalent

### Optional Runtime Dependencies

Code Tutor can run code locally if you have the programming languages installed:

- **JavaScript/TypeScript:** Node.js 18+ (included with app)
- **Python:** Python 3.8+
- **Java:** JDK 11+
- **C#:** .NET 6.0+
- **Kotlin:** Kotlin 1.8+
- **Rust:** Rust 1.70+
- **Flutter/Dart:** Flutter 3.0+

**Note:** The app will detect and help you install missing runtimes on first launch.

---

## Installation Instructions

### Windows Installation

#### Option 1: Full Installer (Recommended)

1. Download `Code-Tutor-Setup-1.0.0.exe`
2. Double-click the installer
3. Choose installation directory (default: `C:\Program Files\Code Tutor`)
4. Select additional options:
   - ☑ Create desktop shortcut
   - ☑ Add to Start Menu
5. Click "Install" and wait for completion
6. Launch from Start Menu or desktop shortcut

#### Option 2: Portable Version

1. Download `Code-Tutor-1.0.0-win-portable.exe`
2. Run directly from any location (no installation required)
3. App data stored in the same directory

**Uninstall (Windows):**
- Settings > Apps > Code Tutor > Uninstall
- Or use Control Panel > Programs and Features

---

### macOS Installation

1. Download `Code-Tutor-1.0.0.dmg`
2. Double-click the DMG file
3. Drag "Code Tutor" to the Applications folder
4. Eject the DMG
5. Launch from Applications or Spotlight

**First Launch Note:**
If you see a security warning:
1. System Preferences > Security & Privacy
2. Click "Open Anyway" next to Code Tutor warning
3. Confirm to open the application

**Alternative:** Download `Code-Tutor-1.0.0-mac.zip` for a zip archive

**Uninstall (macOS):**
- Drag Code Tutor from Applications to Trash
- Delete `~/Library/Application Support/Code Tutor` (optional)

---

### Linux Installation

#### Option 1: AppImage (Universal)

1. Download `Code-Tutor-1.0.0.AppImage`
2. Make it executable:
   ```bash
   chmod +x Code-Tutor-1.0.0.AppImage
   ```
3. Run directly:
   ```bash
   ./Code-Tutor-1.0.0.AppImage
   ```

**Optional:** Integrate with desktop environment:
```bash
# Create desktop entry
mkdir -p ~/.local/share/applications
cat > ~/.local/share/applications/code-tutor.desktop <<'EOF'
[Desktop Entry]
Name=Code Tutor
Exec=/path/to/Code-Tutor-1.0.0.AppImage
Icon=code-tutor
Type=Application
Categories=Education;Development;
EOF
```

#### Option 2: Debian Package (.deb)

For Ubuntu, Debian, Linux Mint, Pop!_OS:
```bash
# Download the .deb file
sudo dpkg -i code-tutor-desktop_1.0.0_amd64.deb

# Install dependencies if needed
sudo apt-get install -f

# Launch
code-tutor-desktop
```

**Uninstall (Linux):**
```bash
# AppImage - just delete the file
rm Code-Tutor-1.0.0.AppImage

# Debian package
sudo apt-get remove code-tutor-desktop
```

---

## Building from Source

### Prerequisites

1. **Node.js 18+** and **npm 9+**
2. **Git**
3. Platform-specific build tools:
   - **Windows:** Visual Studio Build Tools
   - **macOS:** Xcode Command Line Tools
   - **Linux:** `build-essential`, `libfuse2` (for AppImage)

### Build Process

```bash
# 1. Clone repository
git clone https://github.com/DasBluEyedDevil/Code-Tutor.git
cd Code-Tutor

# 2. Install dependencies
npm install

# 3. Build all components
npm run build
cd apps/web && npm run build:prod && cd ../..
cd apps/desktop && npm run build:electron && cd ../..

# 4. Create installers
cd apps/desktop
npm run dist -- --linux   # Linux
npm run dist -- --mac     # macOS
npm run dist -- --win     # Windows

# Or use the build script (recommended)
cd ../..
./build-installers.sh  # Linux/macOS
build-installers.bat   # Windows (PowerShell)
```

### Build Scripts

**Linux/macOS:**
```bash
./build-installers.sh
```

**Windows (Command Prompt):**
```cmd
build-installers.bat
```

**Windows (PowerShell):**
```powershell
.\build-installers.ps1
```

---

## Distribution Files

### Windows

| File | Size | Description |
|------|------|-------------|
| `Code-Tutor-Setup-1.0.0.exe` | ~150 MB | Full installer with uninstaller |
| `Code-Tutor-1.0.0-win-portable.exe` | ~150 MB | Portable version (no installation) |
| `Code-Tutor-1.0.0-win.zip` | ~145 MB | Zip archive |

### macOS

| File | Size | Description |
|------|------|-------------|
| `Code-Tutor-1.0.0.dmg` | ~155 MB | Disk image installer |
| `Code-Tutor-1.0.0-mac.zip` | ~150 MB | Zip archive |

### Linux

| File | Size | Description |
|------|------|-------------|
| `Code-Tutor-1.0.0.AppImage` | ~102 MB | Universal Linux package (recommended) |
| `code-tutor-desktop_1.0.0_amd64.deb` | ~71 MB | Debian/Ubuntu package |

---

## Troubleshooting

### App Won't Launch

**Windows:**
- Install Visual C++ Redistributable if missing
- Check Windows Defender isn't blocking the app
- Run as Administrator if needed

**macOS:**
- Right-click > Open (first time) to bypass Gatekeeper
- Check System Preferences > Security & Privacy

**Linux:**
- For AppImage: Install `libfuse2` if needed
  ```bash
  # Ubuntu/Debian
  sudo apt install libfuse2

  # Fedora
  sudo dnf install fuse-libs
  ```
- Ensure execute permissions: `chmod +x Code-Tutor-1.0.0.AppImage`

### Code Execution Fails

1. Check runtime detection on first launch
2. Install missing programming languages:
   - **Python:** [python.org](https://python.org)
   - **Java:** [adoptium.net](https://adoptium.net)
   - **Node.js:** [nodejs.org](https://nodejs.org)
   - **.NET:** [dot.net](https://dot.net)
   - **Rust:** [rustup.rs](https://rustup.rs)

### Network Issues

- The app works offline for most features
- Course content loads from local files
- Only code execution uses internet (optional)

### Performance Issues

- Close other applications to free memory
- Reduce Monaco editor features in settings
- Disable animations (Settings > Accessibility)

### Port Already in Use

If you see "Port 3000 already in use":
1. Close other applications using port 3000
2. Or change the port in `apps/desktop/src/api-server.ts`

---

## Technical Details

### Architecture

```
Code Tutor Desktop
├── Electron (Main Process)
│   ├── Window Management
│   ├── Menu & Shortcuts
│   └── Runtime Detection
├── Express API Server (Embedded)
│   ├── Course Content API
│   ├── Code Execution Engine
│   ├── Challenge Validation
│   └── Progress Tracking
└── React Frontend (Renderer Process)
    ├── Vite Build System
    ├── Monaco Editor
    ├── Challenge Components
    └── Progress UI
```

### Technology Stack

- **Framework:** Electron 28.0.0
- **Frontend:** React 18, TypeScript, Tailwind CSS
- **Backend:** Node.js, Express 4
- **Editor:** Monaco Editor (VS Code engine)
- **Build:** Vite, electron-builder
- **Languages:** TypeScript, JavaScript, CSS

### File Structure

```
Code Tutor/
├── apps/
│   ├── desktop/           # Electron main process
│   │   ├── dist/          # Compiled backend
│   │   └── dist-electron/ # Installers output
│   └── web/               # React frontend
│       └── dist/          # Compiled frontend
├── content/
│   └── courses/           # Course JSON files
├── build-installers.sh    # Build script
└── package.json           # Root dependencies
```

### Build Configuration

**Electron Builder Config** (`apps/desktop/package.json`):
```json
{
  "build": {
    "appId": "com.codetutor.app",
    "productName": "Code Tutor",
    "directories": {
      "output": "dist-electron"
    },
    "files": [
      "dist/**/*",
      "../web/dist/**/*",
      "../../content/**/*"
    ]
  }
}
```

### Code Signing

**Current Status:** Not code-signed (for development)

**For Production:**
- **Windows:** Requires code signing certificate
- **macOS:** Requires Apple Developer certificate
- **Linux:** No signing required

### Auto-Updates

**Current Status:** Not implemented

**Future Enhancement:**
- Integrate electron-updater
- Set up update server
- Configure app update channels

---

## Security & Privacy

### Data Collection

**Code Tutor does NOT:**
- Collect personal information
- Send telemetry data
- Track usage statistics
- Require internet connection for most features

**Code Tutor DOES:**
- Store progress locally on your device
- Execute code locally (sandboxed when possible)
- Load course content from local files

### Permissions

**Required:**
- **File System:** To save progress and preferences
- **Network:** Optional, for downloading course updates (future)

**NOT Required:**
- Camera, microphone, location, contacts

### Code Execution Safety

- Code runs in isolated processes
- Sandboxed execution where possible
- No file system access from user code
- Network access restricted

---

## Support & Contributing

### Getting Help

- **Issues:** [GitHub Issues](https://github.com/DasBluEyedDevil/Code-Tutor/issues)
- **Discussions:** [GitHub Discussions](https://github.com/DasBluEyedDevil/Code-Tutor/discussions)
- **Email:** support@codetutor.dev

### Contributing

Contributions welcome! See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

### Reporting Bugs

Please include:
- Operating system and version
- Code Tutor version
- Steps to reproduce
- Expected vs actual behavior
- Screenshots if applicable

---

## License

**Code Tutor Desktop**
Copyright © 2025 Code Tutor Team

This application is provided as-is for educational purposes.

For full license information, see [LICENSE.md](LICENSE.md).

---

## Changelog

### Version 1.0.0 (2025-11-14)

**Initial Release**

- ✨ Interactive lessons for 7 programming languages
- ✨ 340+ lessons across Java, C#, Python, JavaScript, Kotlin, Flutter, Rust
- ✨ Real-time code execution with test validation
- ✨ Progressive hint system
- ✨ Challenge types: Multiple choice, True/False, Code output, Free coding, Code completion, Conceptual
- ✨ Monaco editor with syntax highlighting
- ✨ Progress tracking and achievements
- ✨ Dark/light theme support
- ✨ Keyboard shortcuts and accessibility features
- ✨ Mobile-responsive lesson interface
- ✨ Automatic runtime detection
- ✨ Offline-first architecture
- 🐛 Fixed 20+ UX/UI issues identified in Context7 analysis
- 📱 Mobile-responsive lesson page with tab interface
- ♿ WCAG 2.1 Level A accessibility compliance
- 🚀 Network retry logic with exponential backoff
- 🎨 Professional 404 error page
- 📚 Comprehensive documentation

---

## Roadmap

### Version 1.1 (Planned)

- 🔍 Global search for lessons
- 📊 Advanced progress analytics
- 🌐 Cloud sync for progress (optional)
- 🎨 Custom code themes
- 🔧 More configuration options

### Version 1.2 (Planned)

- 📝 Code snippet library
- 🤝 Share solutions with friends
- 🏆 More achievements and gamification
- 🔄 Auto-update system
- 🌍 Internationalization (i18n)

### Version 2.0 (Future)

- 🤖 AI-powered code hints
- 👥 Collaborative coding sessions
- 🎮 Coding challenges with leaderboards
- 🎓 Certificate generation
- 📱 Mobile companion app

---

**Thank you for using Code Tutor Desktop!**

Happy coding! 🚀
