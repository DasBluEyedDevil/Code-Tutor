# Code Tutor - Unified Training Platform

> Master multiple programming languages in one unified platform with interactive lessons and real-time code execution.

## 🖥️ **Desktop App Available!**

**Want a simple double-click experience?** Code Tutor is now available as a **standalone desktop application**!

- ✅ **No Docker required** - uses your local language runtimes
- ✅ **One-click launch** - just double-click the launcher script
- ✅ **Works offline** - everything runs on your machine
- ✅ **Cross-platform** - Windows, macOS, and Linux

**Get Started:** See **[QUICK_START.md](./QUICK_START.md)** for the desktop app

---

A modern learning platform that teaches **Java, Python, Kotlin, Rust, C#, Flutter, and JavaScript/TypeScript** through interactive lessons, real-time code execution, and progress tracking.

Available as a **standalone desktop application** that works offline using your local language runtimes.

## 🌟 Features

- **7 Programming Languages** in one platform
- **Interactive Code Editor** powered by Monaco Editor (VS Code's engine)
- **Real-time Code Execution** using your local language runtimes
- **Progress Tracking** with automatic progress saving
- **Markdown Lessons** with syntax highlighting
- **Dark/Light Theme** support
- **Concept-First Pedagogy** - understand concepts before jargon
- **Fully Offline** - works completely offline once installed

## 🏗️ Architecture

```
Code-Tutor/
├── apps/
│   ├── desktop/          # Electron desktop application
│   │   ├── src/
│   │   │   ├── main.ts          # Electron main process
│   │   │   ├── executors.ts     # Local code execution
│   │   │   └── api-server.ts    # Embedded Express server
│   │   └── package.json
│   └── web/              # React + TypeScript frontend (bundled into desktop)
├── content/
│   └── courses/          # Course content for all 7 languages
│       ├── python/
│       ├── java/
│       ├── kotlin/
│       ├── rust/
│       ├── csharp/
│       ├── javascript/
│       └── dart/
└── scripts/              # Content management tools
```

## 🚀 Quick Start

> **Simple!** Just double-click the launcher script to start the desktop app.
> **Having issues?** See [TROUBLESHOOTING.md](./TROUBLESHOOTING.md)

### Prerequisites

- **Language Runtimes:** Install the programming languages you want to learn:
  - Python 3.x ([Download](https://www.python.org/downloads/))
  - Java 17+ ([Download](https://adoptium.net/))
  - Kotlin (included with Java)
  - Rust ([Download](https://www.rust-lang.org/tools/install))
  - .NET 6+ ([Download](https://dotnet.microsoft.com/download))
  - Node.js 18+ ([Download](https://nodejs.org))
  - Dart/Flutter ([Download](https://flutter.dev/docs/get-started/install))

### Quick Setup (2 Steps)

1. **Clone and install:**
   ```bash
   git clone https://github.com/DasBluEyedDevil/Code-Tutor.git
   cd Code-Tutor
   npm install
   ```

2. **Launch the app:**
   - **Windows:** Double-click `launch-desktop.bat` or `launch-desktop.ps1`
   - **macOS/Linux:** Run `./launch-desktop.sh`
   - **Or use npm:** `npm run start:desktop`

The desktop app will automatically open in a new window!

## 📚 Development

### Project Structure

#### Desktop App (`apps/desktop/`)
- **Framework:** Electron 28
- **Backend:** Embedded Express server
- **Execution:** Local language runtime spawning
- **Runtime Detection:** Automatic detection of installed languages
- **Builder:** electron-builder for cross-platform installers

#### Frontend (`apps/web/`)
- **Framework:** React 18 + TypeScript + Vite
- **Styling:** Tailwind CSS
- **State:** Zustand
- **Editor:** Monaco Editor
- **Routing:** React Router
- **Markdown:** react-markdown with syntax highlighting

### Running in Development

```bash
# Start the desktop app in development mode
npm run start:desktop

# Build the desktop app (no installers)
npm run build:desktop

# Build desktop app with installers (.exe, .dmg, .AppImage)
npm run dist:desktop
```

### Building Installers

```bash
# Build installers for your current platform
npm run dist:desktop

# Or use the helper scripts:
# Windows:
.\build-installers.bat

# macOS/Linux:
./build-installers.sh
```

This creates platform-specific installers in `apps/desktop/dist-electron/`

## 🎨 Adding New Content

### Creating a New Course

1. Create course directory:
   ```bash
   mkdir -p content/courses/your-language/modules/module-00/lessons
   ```

2. Create `course.json`:
   ```json
   {
     "courseMetadata": {
       "id": "your-language",
       "language": "YourLanguage",
       "displayName": "Your Language Course",
       "description": "Learn Your Language",
       ...
     },
     "modules": [...],
     "languageConfig": {...}
   }
   ```

3. Add lesson content as markdown files

4. Update language selection in `apps/web/src/pages/LandingPage.tsx`

### Creating a New Lesson

1. Add lesson object to module in `course.json`
2. Create markdown file: `lesson-XX-YY.md`
3. Add exercises with starter code and test cases
4. Add hints and solutions

See `content/courses/python/` for examples.

## 🧪 Testing

```bash
# Run all tests
npm test

# Run frontend tests
cd apps/web && npm test

# Test the desktop app
npm run start:desktop
```

## 📖 Course Content

### Current Status

| Language | Status | Lessons | Executors |
|----------|--------|---------|-----------|
| Python | 🟢 Started | 3/73 | ✅ Complete |
| Java | 🟡 Planned | 0/20 | ✅ Complete |
| Kotlin | 🟡 Planned | 0/29 | ✅ Complete |
| Rust | 🟡 Planned | 0/60 | ✅ Complete |
| C# | 🟡 Planned | 0/26 | ✅ Complete |
| JavaScript/TS | 🟡 Planned | 0/40 | ✅ Complete |
| Flutter/Dart | 🟡 Planned | 0/95 | ✅ Complete |

### Migration Status

Content is being migrated from individual repos:
- [Java Training Course](https://github.com/DasBluEyedDevil/Java-Training-Course)
- [Python Training Course](https://github.com/DasBluEyedDevil/Python-Training-Course)
- [Kotlin Training Course](https://github.com/DasBluEyedDevil/Kotlin-Training-Course)
- [Rust Training Course](https://github.com/DasBluEyedDevil/Rust-Training-Course)
- [C# Training Course](https://github.com/DasBluEyedDevil/CSharp-Training-Course)
- [Flutter Training Course](https://github.com/DasBluEyedDevil/Flutter-Training-Course)

## 🗺️ Roadmap

See [UNIFIED_PLATFORM_PLAN.md](./UNIFIED_PLATFORM_PLAN.md) for the comprehensive plan.

### Phase 1: Foundation ✅ (Complete)
- [x] React frontend with Monaco Editor
- [x] Node.js backend API
- [x] Python executor service
- [x] Sample Python course content
- [x] Progress tracking

### Phase 2: Migration Tools & Executors ✅ (Complete)
**Tools:**
- [x] Content migration CLI
- [x] Content validator script

**All 7 Language Executors:**
- [x] Python executor (Flask + Docker)
- [x] Java executor (Spark Java + JDK 17)
- [x] Kotlin executor (Kotlin Compiler + JVM)
- [x] Rust executor (Actix-web + rustc)
- [x] C# executor (ASP.NET Core + Roslyn)
- [x] JavaScript/TypeScript executor (Node.js + VM2)
- [x] Dart/Flutter executor (Dart SDK + Shelf)

### Phase 3: Content Migration 🚧 (Next)
**Content to Migrate:**
- [ ] Migrate Python course (70 remaining lessons)
- [ ] Migrate Kotlin course (29 lessons)
- [ ] Migrate Java course (20 lessons)
- [ ] Migrate C# course (26 lessons)
- [ ] Migrate Flutter course (95 lessons)
- [ ] Migrate Rust course (60 lessons)
- [ ] Create JavaScript/TypeScript course (40 lessons)

### Phase 4: Advanced Features (Weeks 11-14)
- [ ] User authentication (full implementation)
- [ ] PostgreSQL database
- [ ] Progress analytics dashboard
- [ ] Achievement/badge system
- [ ] Code sharing
- [ ] Search across courses
- [ ] Bookmarks and notes

### Phase 5: Polish & Launch (Weeks 15-18)
- [ ] Testing and QA
- [ ] Performance optimization
- [ ] Accessibility (WCAG 2.1 AA)
- [ ] Documentation
- [ ] Beta testing
- [ ] Public launch

## 📦 Packaging & Distribution

Want to create installable packages of Code-Tutor? See [PACKAGING_GUIDE.md](./PACKAGING_GUIDE.md) and [DISTRIBUTION.md](./DISTRIBUTION.md) for details.

The desktop app can be packaged as:

- **Windows:** `.exe` installer (NSIS) and portable `.exe`
- **macOS:** `.dmg` disk image and `.zip` archive
- **Linux:** `.AppImage`, `.deb`, and `.rpm` packages

**Create installers:**
```bash
npm run dist:desktop
# Or use: build-installers.bat (Windows) / build-installers.sh (Linux/macOS)
```

Installers are created in `apps/desktop/dist-electron/` and include everything needed to run Code-Tutor, including the course content.

## 🤝 Contributing

Contributions are welcome! Please read our [Contributing Guide](./CONTRIBUTING.md) (coming soon).

### Ways to Contribute

1. **Add course content** - Write lessons, exercises, and tutorials
2. **Build language executors** - Implement executors for new languages
3. **Improve UI/UX** - Enhance the user interface and experience
4. **Fix bugs** - Help squash bugs and improve stability
5. **Write documentation** - Improve guides and tutorials

## 📄 License

MIT License - see [LICENSE](./LICENSE) for details.

## 🙏 Acknowledgments

- **Monaco Editor** - Microsoft's VS Code editor component
- **React** - Facebook's UI library
- **Express** - Node.js web framework
- All the original course content from individual repositories

## 📞 Contact

- **GitHub:** [@DasBluEyedDevil](https://github.com/DasBluEyedDevil)
- **Issues:** [GitHub Issues](https://github.com/DasBluEyedDevil/Code-Tutor/issues)

---

Built with ❤️ for developers learning multiple programming languages.