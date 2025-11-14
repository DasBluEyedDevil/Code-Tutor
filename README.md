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

Available as both a **desktop application** and **web-based platform**.

## 🌟 Features

- **7 Programming Languages** in one platform
- **Interactive Code Editor** powered by Monaco Editor (VS Code's engine)
- **Real-time Code Execution** with sandboxed Docker containers
- **Progress Tracking** with localStorage and backend sync
- **Markdown Lessons** with syntax highlighting
- **Dark/Light Theme** support
- **Concept-First Pedagogy** - understand concepts before jargon
- **Offline-Capable** Progressive Web App (planned)

## 🏗️ Architecture

```
Code-Tutor/
├── apps/
│   ├── web/              # React + TypeScript frontend
│   ├── api/              # Node.js/Express backend
│   └── executors/
│       ├── python/       # Python executor (Flask)
│       ├── javascript/   # JavaScript/TS executor (Node.js)
│       └── java/         # Java executor (Spark Java)
├── content/
│   └── courses/
│       └── python/       # Python course content
├── packages/             # Shared packages
└── tools/
    ├── content-migrator/ # Migration CLI tool
    └── content-validator/# Validation script
```

## 🚀 Quick Start

> **Important:** Docker Desktop is **required** for Code-Tutor to work properly.  
> **Having issues?** See [TROUBLESHOOTING.md](./TROUBLESHOOTING.md)

### Prerequisites

- **Node.js** 18+ and npm 9+ ([Download](https://nodejs.org))
- **Docker Desktop** ([Download](https://www.docker.com/products/docker-desktop)) - **Required**
- **Git**

### Quick Setup (3 Steps)

1. **Install Docker Desktop:**
   - Download and install from: https://www.docker.com/products/docker-desktop
   - Start Docker Desktop and wait for it to fully start
   - You should see the whale icon in your system tray

2. **Clone and install:**
   ```bash
   git clone https://github.com/DasBluEyedDevil/Code-Tutor.git
   cd Code-Tutor
   npm install
   ```

3. **Start everything:**
   ```bash
   # Start Docker containers
   docker-compose up -d
   
   # Start the application
   npm run dev
   ```

4. **Open browser:** http://localhost:3000

### Windows Users: One-Click Start

Use the included startup script (handles Docker check automatically):

```powershell
.\start.ps1
```

Or double-click: **`START.bat`**

## 📚 Development

### Project Structure

#### Frontend (`apps/web/`)
- **Framework:** React 18 + TypeScript + Vite
- **Styling:** Tailwind CSS
- **State:** Zustand
- **Editor:** Monaco Editor
- **Routing:** React Router
- **Markdown:** react-markdown with syntax highlighting

#### Backend (`apps/api/`)
- **Framework:** Express + TypeScript
- **Database:** PostgreSQL (planned) / JSON files (current)
- **Auth:** JWT-based authentication
- **APIs:**
  - `/api/courses` - Course content
  - `/api/execute` - Code execution dispatcher
  - `/api/progress` - Progress tracking
  - `/api/auth` - Authentication

#### Executors (`apps/executors/`)
Each language has its own sandboxed executor service:
- **Python:** Flask + Docker ✅ (port 4000)
- **Java:** Spark Java + JDK 17 + Docker ✅ (port 4001)
- **Kotlin:** Kotlin Compiler + JVM + Docker ✅ (port 4002)
- **Rust:** Actix-web + rustc + Docker ✅ (port 4003)
- **C#:** ASP.NET Core + Roslyn + Docker ✅ (port 4004)
- **JavaScript/TypeScript:** Node.js + VM2 + Docker ✅ (port 4005)
- **Dart/Flutter:** Dart SDK + Shelf + Docker ✅ (port 4007)

### Running Development Services

#### All services at once:
```bash
npm run dev
```

#### Individual services:
```bash
# Frontend only
npm run dev:web

# Backend only
npm run dev:api

# Python executor only
docker-compose up python-executor
```

### Building for Production

```bash
# Build all workspaces
npm run build

# Build frontend
cd apps/web && npm run build

# Build backend
cd apps/api && npm run build
```

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

## 🐳 Docker Services

### Python Executor

Build and run:
```bash
cd apps/executors/python
docker build -t code-tutor-python-executor .
docker run -p 4000:4000 code-tutor-python-executor
```

Or use docker-compose:
```bash
docker-compose up python-executor
```

### Testing the Executor

```bash
curl -X POST http://localhost:4000/execute \
  -H "Content-Type: application/json" \
  -d '{
    "code": "print(\"Hello, World!\")"
  }'
```

Expected response:
```json
{
  "success": true,
  "output": "Hello, World!\n",
  "error": null
}
```

## 🧪 Testing

```bash
# Run all tests
npm test

# Run frontend tests
cd apps/web && npm test

# Run backend tests
cd apps/api && npm test
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

Want to share Code-Tutor as a standalone app? See [PACKAGING_GUIDE.md](./PACKAGING_GUIDE.md) for options:

- **Electron Desktop App** - Single `.exe` installer, no Node.js required (~150 MB)
- **PKG Binary** - Standalone executable (~50 MB)  
- **Portable ZIP** - Just extract and run (~200 MB, requires Node.js)
- **Docker Container** - Fully containerized app

The easiest option is Electron, which packages everything into a professional desktop application.

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