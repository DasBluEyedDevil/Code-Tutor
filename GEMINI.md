# Code Tutor - Project Context

## 1. Project Overview
**Code Tutor** is a production-ready native desktop application designed for interactive programming education. It allows users to learn multiple languages (Python, JavaScript, Java, C#, Rust) through hands-on challenges with real-time local code execution.

*   **Type:** Native Desktop Application
*   **Primary Language:** C# (.NET 8.0)
*   **UI Framework:** Avalonia UI 11.1.0 (Cross-platform XAML)
*   **Architecture:** MVVM (Model-View-ViewModel) with ReactiveUI
*   **Status:** Production Ready (v1.0)

## 2. Technology Stack
*   **Core:** .NET 8.0
*   **UI:** Avalonia UI (XAML-based, similar to WPF/UWP but cross-platform)
*   **MVVM Framework:** ReactiveUI 19.5.31
*   **Database:** Entity Framework Core 8.0 + SQLite
*   **Code Execution:** Local process spawning with resource limits and sandboxing
*   **Syntax Highlighting:** TextMateSharp
*   **Testing:** xUnit, Moq, FluentAssertions

## 3. Architecture & Structure
The project follows a strict **MVVM** pattern with a **Service-Oriented** architecture.

### Key Directories (`native-app/`)
*   `CodeTutor.Native.csproj`: Main project file.
*   `App.axaml`: Application entry and DI container configuration.
*   `Program.cs`: Application entry point.
*   `Models/`: Plain Data Objects (POCOs) (e.g., `Course.cs`, `User.cs`).
*   `ViewModels/`: Presentation logic (e.g., `LessonPageViewModel.cs`). Inherits from `ViewModelBase`.
*   `Views/`: XAML UI definitions (e.g., `LessonPage.axaml`).
*   `Services/`: Business logic (e.g., `CodeExecutor.cs`, `CourseService.cs`).
*   `Data/`: Database context (`CodeTutorDbContext`) and repositories.
*   `Controls/`: Custom UI controls (e.g., `CodeEditor.axaml`).

### Key Files
*   `native-app/NATIVE_ARCHITECTURE.md`: Detailed architectural documentation.
*   `BUILD.md`: Comprehensive build instructions.
*   `content/courses/**/*.json`: Course content data.

## 4. Development Conventions
*   **MVVM:** Logic goes in ViewModels, UI in Views. Views bind to ViewModels.
*   **Dependency Injection:** All services are registered in `App.axaml.cs`. Use constructor injection.
*   **Services:** Stateless where possible. Use the `IServiceName` interface pattern.
*   **Async/Await:** Use for all I/O and heavy operations to keep the UI responsive.
*   **Naming:** PascalCase for public members, camelCase for private fields (prefixed with `_`).
*   **Testing:** Unit tests in `native-app.Tests/`. Focus on testing Services and ViewModels.

## 5. Building & Running

### Prerequisites
*   .NET 8.0 SDK
*   (Optional) Language runtimes (Python, Node.js, Java, etc.) for execution features.

### Key Commands
| Action | Command |
| :--- | :--- |
| **Run App** | `dotnet run --project native-app/CodeTutor.Native.csproj` |
| **Build (Dev)** | `dotnet build native-app/CodeTutor.Native.csproj` |
| **Run Tests** | `dotnet test native-app.Tests/native-app.Tests.csproj` |
| **Build Installer (Win)** | `.\build-installer.ps1` (Requires Inno Setup for .exe) |
| **Publish (Portable)** | `dotnet publish native-app/CodeTutor.Native.csproj -c Release -r win-x64 --self-contained` |

## 6. Content System
Course content is defined in JSON files located in `content/courses/{language}/course.json`.
*   **Structure:** Course -> Modules -> Lessons -> Challenges.
*   **Editing:** Edit the JSON files directly to modify course content.
