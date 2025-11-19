# Code Tutor - Native Desktop Application

> Master multiple programming languages with an interactive, cross-platform native desktop application.

## 🖥️ Native C#/Avalonia Desktop App

Code Tutor is a **production-ready native desktop application** built with C# and Avalonia UI for superior performance and a true native experience.

- ✅ **True Native App** - C#/.NET 8.0 with Avalonia UI (not Electron)
- ✅ **Cross-Platform** - Windows, macOS, and Linux
- ✅ **Local Code Execution** - uses your installed language runtimes
- ✅ **Works Offline** - everything runs locally
- ✅ **Low Memory Footprint** - native performance
- ✅ **Modern UI** - Beautiful dark/light themes with smooth animations

## 🌟 Features

### Learning Platform
- **5 Programming Languages** - Python, JavaScript, Java, C#, Rust
- **6 Challenge Types** - Multiple Choice, True/False, Free Coding, Code Output, Code Completion, Conceptual
- **Interactive Code Editor** - Syntax highlighting with TextMate grammar support
- **Real-time Code Execution** - Execute code locally with resource limits for safety
- **Educational Guidance** - Hints, common mistakes, solutions, and bonus challenges

### User Experience
- **Progress Tracking** - SQLite database persists your learning journey
- **Achievement System** - Earn achievements as you complete challenges
- **Streak Tracking** - Build daily coding streaks
- **Dark/Light Themes** - Comfortable coding in any lighting
- **Responsive UI** - Smooth animations and intuitive navigation

### Security & Safety
- **Resource Limits** - Memory limited to 512 MB, output capped at 100 KB
- **Execution Timeout** - 10-second timeout with process tree termination
- **Environment Restrictions** - Whitelisted environment variables only
- **CPU Priority Control** - Runs user code at below-normal priority

## 🏗️ Architecture

```
Code-Tutor/
├── native-app/                # Native C#/Avalonia desktop app (PRODUCTION)
│   ├── Services/              # 15 production services
│   │   ├── CodeExecutor.cs           - Secure code execution
│   │   ├── ChallengeValidationService.cs  - Multi-language validation
│   │   ├── ProgressService.cs        - Progress tracking
│   │   ├── AchievementService.cs     - Gamification
│   │   └── ...                       - 11 more services
│   ├── ViewModels/            # MVVM ViewModels with ReactiveUI
│   ├── Views/                 # Avalonia XAML views
│   ├── Models/                # Data models
│   ├── Controls/              # Custom controls
│   ├── Themes/                # Dark/Light themes
│   └── Data/                  # SQLite database context
│
├── native-app.Tests/          # Unit tests (102 tests, 90% coverage)
│   ├── Unit/                  # Service tests
│   ├── ViewModels/            # ViewModel tests
│   └── Integration/           # Integration tests
│
├── content/courses/           # Course content (JSON)
│   ├── python/
│   ├── javascript/
│   ├── java/
│   ├── csharp/
│   └── rust/
│
└── docs/                      # Documentation
    ├── INTERACTIVE_CONTENT_SCHEMA.md  - Content structure
    └── UX_UI_IMPROVEMENTS.md          - UX guidelines
```

## 🚀 Quick Start

### Prerequisites

1. **Install .NET 8.0 SDK**
   - Download from: https://dotnet.microsoft.com/download/dotnet/8.0

2. **Install Language Runtimes** (for the languages you want to learn):
   - **Python 3.x** - https://www.python.org/downloads/
   - **Java 17+** - https://adoptium.net/
   - **.NET 8.0** - https://dotnet.microsoft.com/download
   - **Node.js 18+** - https://nodejs.org
   - **Rust** - https://www.rust-lang.org/tools/install

### Running the App

```bash
# Clone the repository
git clone https://github.com/DasBluEyedDevil/Code-Tutor.git
cd Code-Tutor

# Restore NuGet packages
dotnet restore native-app/CodeTutor.Native.csproj

# Run the application
dotnet run --project native-app/CodeTutor.Native.csproj
```

### Building for Distribution

```bash
# Build release version
dotnet build native-app/CodeTutor.Native.csproj -c Release

# Publish self-contained executable (Windows)
dotnet publish native-app/CodeTutor.Native.csproj -c Release -r win-x64 --self-contained

# Publish self-contained executable (macOS)
dotnet publish native-app/CodeTutor.Native.csproj -c Release -r osx-x64 --self-contained

# Publish self-contained executable (Linux)
dotnet publish native-app/CodeTutor.Native.csproj -c Release -r linux-x64 --self-contained
```

## 🧪 Running Tests

```bash
# Run all tests
dotnet test native-app.Tests/CodeTutor.Native.Tests.csproj

# Run with coverage
dotnet test native-app.Tests/CodeTutor.Native.Tests.csproj --collect:"XPlat Code Coverage"

# Run specific test category
dotnet test native-app.Tests/CodeTutor.Native.Tests.csproj --filter Category=Services
```

**Test Coverage**: 102 tests, 90% service coverage

## 📚 Supported Languages & Features

| Language   | Code Execution | Syntax Highlighting | Test Cases | Input Mocking |
|------------|----------------|---------------------|------------|---------------|
| Python     | ✅ python3      | ✅ TextMate          | ✅ Yes      | ✅ Yes         |
| JavaScript | ✅ node         | ✅ TextMate          | ✅ Yes      | ✅ Yes         |
| Java       | ✅ javac + java | ✅ TextMate          | ✅ Yes      | ✅ Yes         |
| C#         | ✅ dotnet       | ✅ TextMate          | ✅ Yes      | ✅ Yes         |
| Rust       | ✅ rustc        | ✅ TextMate          | ✅ Yes      | ✅ Yes         |

## 🎯 Challenge Types

1. **Multiple Choice** - Select the correct answer from options
2. **True/False** - Determine if statements are true or false
3. **Free Coding** - Write code from scratch with test validation
4. **Code Output** - Predict what code will output
5. **Code Completion** - Fill in missing code segments
6. **Conceptual** - Short-answer conceptual questions

## 🛡️ Security Features

The Code Tutor app implements multiple layers of security for safe code execution:

- **Memory Limits**: 512 MB maximum per execution
- **Output Limits**: 100 KB maximum output (prevents log spam)
- **Execution Timeout**: 10-second limit with process tree termination
- **Environment Restrictions**: Whitelisted environment variables only (PATH, HOME, TEMP)
- **Network Restrictions**: NO_PROXY set to limit network access
- **CPU Priority**: Below-normal priority to prevent system impact
- **Process Isolation**: Spawned processes run in isolated context

## 📖 Technology Stack

- **Framework**: .NET 8.0
- **UI**: Avalonia UI 11.1.0 (cross-platform XAML)
- **MVVM**: ReactiveUI 19.5.31
- **Database**: Entity Framework Core 8.0 + SQLite
- **Syntax Highlighting**: TextMateSharp 1.0.56
- **Testing**: xUnit 2.6.2 + Moq 4.20.70 + FluentAssertions 6.12.0

## 📝 Development

### Architecture Pattern: MVVM

The app follows the **Model-View-ViewModel** pattern:

- **Models** - Data entities (Challenge, Course, UserProgress, Achievement)
- **Views** - Avalonia XAML UI (CoursePage, LessonPage, Challenge Views)
- **ViewModels** - Presentation logic with ReactiveUI (CoursePageViewModel, etc.)
- **Services** - Business logic layer (15 services with dependency injection)

### Dependency Injection

All services are registered in `App.axaml.cs` with appropriate lifetimes:

- **Singleton**: Database, Settings, Navigation, Error Handler
- **Scoped**: Progress, Achievement, Streak tracking
- **Transient**: Code execution, validation, factories

### Database Schema

SQLite database with Entity Framework Core:

- **Users** - User accounts
- **UserProgress** - Lesson completion tracking
- **Achievements** - Achievement definitions and unlocks
- **Streaks** - Daily streak tracking
- **CodeHistory** - Code submission history
- **Statistics** - Learning analytics

## 🤝 Contributing

Contributions are welcome! Please see `native-app/SETUP.md` for development setup instructions.

## 📄 License

MIT License - see LICENSE file for details

## 🎓 Educational Philosophy

Code Tutor follows a **concept-first pedagogy**:

1. **Conceptual Understanding** - Learn the "why" before the "how"
2. **Hands-On Practice** - Write real code immediately
3. **Immediate Feedback** - Instant validation with detailed explanations
4. **Progressive Complexity** - Build skills incrementally
5. **Common Mistakes** - Learn from typical errors
6. **Bonus Challenges** - Push beyond basics for mastery

---

**Production Version**: v1.0
**Status**: ✅ Production Ready
**Platform**: Native C#/Avalonia Desktop Application
