# Codebase Structure

**Analysis Date:** 2026-02-02

## Directory Layout

```
Code-Tutor/
├── native-app-wpf/          # Main WPF desktop application (C#)
│   ├── Views/               # XAML view pages and navigation targets
│   ├── Controls/            # Custom WPF controls and visual components
│   ├── Services/            # Business logic, data loading, execution
│   ├── Models/              # Data models for deserialization
│   ├── Behaviors/           # WPF attached behaviors
│   ├── Converters/          # WPF value converters
│   ├── Themes/              # XAML styling and theming
│   ├── Resources/           # Embedded resources and assets
│   ├── Assets/              # Icons, images, fonts
│   ├── App.xaml             # WPF application root
│   ├── MainWindow.xaml      # Main application window
│   └── CodeTutor.Wpf.csproj # Project file (Net8.0-windows, WPF, C# 13.0)
│
├── content/                 # Educational content (static, embedded)
│   └── courses/             # Hierarchical course structure
│       ├── java/            # Java course
│       ├── csharp/          # C# course
│       ├── flutter/         # Flutter course
│       ├── javascript/      # JavaScript course
│       ├── kotlin/          # Kotlin course
│       ├── python/          # Python course (minimal)
│       └── rust/            # Rust course (stub)
│
├── apps/                    # Legacy/archive application builds
│   ├── desktop/             # Electron desktop build (archived)
│   └── web/                 # Web version (archived)
│
├── native-app.Tests/        # Test project (C#, xUnit/Specflow)
│   ├── Models/              # Test data models
│   ├── E2E/                 # End-to-end test scenarios
│   └── bin/, obj/           # Build outputs (excluded)
│
├── docs/                    # Documentation
│   └── plans/               # Implementation plans from GSD orchestrator
│
├── course_structure/        # Course metadata snapshots
│   ├── CSharp/              # C# course outline
│   ├── Flutter/             # Flutter course outline
│   ├── Java/                # Java course outline
│   ├── Javascript/          # JavaScript course outline
│   ├── Kotlin/              # Kotlin course outline
│   └── Python/              # Python course outline
│
├── .planning/               # GSD planning artifacts
│   └── codebase/            # Architecture documentation (this location)
│
├── scripts/                 # Build and utility scripts
│   └── review-templates/    # Content review templates
│
├── publish/                 # Build output directory (portable executable)
│   └── CodeTutor.Native.exe # Self-contained Windows executable
│
├── dist/                    # Distribution builds
│   └── CodeTutor-Setup-*.exe # Windows installer (Inno Setup)
│
├── BUILD.md                 # Build and distribution guide
├── AGENTS.md                # Agent/automation documentation
├── GEMINI.md                # AI integration notes
└── LICENSE                  # MIT License

```

## Directory Purposes

**native-app-wpf:**
- Purpose: Main WPF desktop application - the primary deliverable
- Contains: Complete C# application with UI, services, and models
- Key files: `MainWindow.xaml.cs` (entry point), `App.xaml.cs` (startup)
- Built as: Self-contained .NET 8.0 Windows executable

**content/courses:**
- Purpose: Educational course content repository (static, versioned)
- Contains: Language-specific courses with modules, lessons, challenges
- Organization: One directory per language (java/, csharp/, flutter/, etc.)
- Structure per course:
  ```
  {language}/
  ├── course.json          # Course metadata (title, description, modules count)
  ├── modules/             # Module directories
  │   └── {number}-{name}/
  │       ├── module.json  # Module metadata (title, description, order)
  │       └── lessons/     # Lesson directories
  │           └── {number}-{name}/
  │               ├── lesson.json              # Lesson metadata
  │               ├── content/                 # Content sections
  │               │   ├── 01-theory.md
  │               │   ├── 02-key_point.md
  │               │   ├── 03-example.md
  │               │   ├── 04-theory.md
  │               │   ├── 05-analogy.md
  │               │   ├── 06-warning.md
  │               │   └── ...
  │               └── challenges/              # Coding challenges
  │                   └── {challenge-number}/
  │                       ├── challenge.json   # Challenge metadata, test cases
  │                       ├── starter.{lang}  # Starter code
  │                       └── solution.{lang} # Solution code
  └── capstone/           # (optional) Capstone project
      └── {project-files}
  ```

**native-app-wpf/Views:**
- Purpose: XAML pages for user-facing screens
- Contains: Routed through NavigationService
- Key files:
  - `LandingPage.xaml` - course selection screen
  - `CoursePage.xaml` - module/lesson browser
  - `LessonPage.xaml` - lesson content + challenges
  - `CourseSidebar.xaml` - navigation sidebar

**native-app-wpf/Controls:**
- Purpose: Reusable WPF controls and visual components
- Contains: Theory display, code editors, chat, challenges, animations
- Key files:
  - `CodingChallenge.xaml` - complete challenge UI with editor
  - `CodeExampleSection.xaml` - syntax-highlighted code display
  - `TutorChat.xaml` - AI tutor chat interface
  - `AnimatedContentControl.cs` - custom navigation animations
  - `KeyPointSection.xaml`, `TheorySection.xaml` - content types
  - Special effects: `ConfettiOverlay`, `FloatingParticles`, `RippleEffect`, `ShimmerEffect`

**native-app-wpf/Services:**
- Purpose: Business logic, data access, and external integrations
- Contains: Service interfaces and implementations
- Key files:
  - `CourseService.cs` - loads courses from JSON, manages cache
  - `NavigationService.cs` - manages view navigation stack
  - `CodeExecutionService.cs` - routes code to appropriate executor
  - `Phi4TutorService.cs` - LLM-based tutoring using ONNX Runtime
  - `ModelDownloadService.cs` - manages LLM model lifecycle
  - `Executors/RoslynCSharpExecutor.cs` - C# compilation via Roslyn
  - `Executors/PistonExecutor.cs` - Multi-language via Piston API
  - `InteractiveProcessSession.cs` - Long-running process management

**native-app-wpf/Models:**
- Purpose: Data structures for JSON deserialization
- Contains: Immutable models with JsonPropertyName attributes
- Key files:
  - `Course.cs` - Course, Module, Lesson, ContentSection, Challenge classes
  - `TutorMessage.cs` - Chat message model
  - `UserProgress.cs` - Lesson completion and challenge tracking

**native-app-wpf/Behaviors:**
- Purpose: WPF attached behaviors for reusable interactivity
- Key files:
  - `AnimationBehaviors.cs` - Storyboard triggering
  - `TypewriterBehavior.cs` - Text animation effect

**native-app-wpf/Converters:**
- Purpose: WPF value converters for data binding
- Patterns: Boolean to visibility, enum to string, difficulty to color

**native-app-wpf/Themes:**
- Purpose: XAML styling, brushes, and theming
- Contains: Resource dictionaries, styles for controls

**native-app.Tests:**
- Purpose: Automated test suite (unit and end-to-end)
- Contains: xUnit/Specflow tests
- Key areas:
  - `Models/` - Test data factories and fixtures
  - `E2E/` - User workflow scenarios

**docs/plans:**
- Purpose: GSD-generated implementation plans
- Generated by: `/gsd:plan-phase` command
- Contains: Phase breakdowns, subtask lists, guidance

**.planning/codebase:**
- Purpose: Codebase analysis documents (this location)
- Contains: ARCHITECTURE.md, STRUCTURE.md, CONVENTIONS.md, TESTING.md, STACK.md, INTEGRATIONS.md, CONCERNS.md
- Used by: `/gsd:plan-phase` and `/gsd:execute-phase` commands

## Key File Locations

**Entry Points:**
- `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\MainWindow.xaml.cs` - Application entry point, DI setup
- `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\App.xaml.cs` - WPF startup
- `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Views\LandingPage.xaml` - User-facing entry

**Configuration:**
- `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\CodeTutor.Wpf.csproj` - Project settings, dependencies
- `C:\Users\dasbl\Downloads\Code-Tutor\BUILD.md` - Build instructions
- `C:\Users\dasbl\Downloads\Code-Tutor\installer.iss` - Inno Setup installer config

**Core Logic:**
- `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Services\CourseService.cs` - Content loading
- `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Services\CodeExecutionService.cs` - Code execution routing
- `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Services\NavigationService.cs` - Navigation state

**Testing:**
- `C:\Users\dasbl\Downloads\Code-Tutor\native-app.Tests\` - Test project root
- Various test files for services and models

**Content:**
- `C:\Users\dasbl\Downloads\Code-Tutor\content\courses\java\course.json` - Example course metadata
- `C:\Users\dasbl\Downloads\Code-Tutor\content\courses\java\modules\01-java-fundamentals\module.json` - Module metadata
- `C:\Users\dasbl\Downloads\Code-Tutor\content\courses\java\modules\01-java-fundamentals\lessons\02-lesson-12-your-first-java-program\lesson.json` - Lesson metadata
- `C:\Users\dasbl\Downloads\Code-Tutor\content\courses\java\modules\01-java-fundamentals\lessons\02-lesson-12-your-first-java-program\challenges\01-your-first-hello-world\challenge.json` - Challenge metadata

## Naming Conventions

**Files:**

- **View files:** PascalCase + .xaml (e.g., `LandingPage.xaml`, `CoursePage.xaml`)
- **Code-behind:** Same name as XAML + .xaml.cs (e.g., `LandingPage.xaml.cs`)
- **Service interfaces:** IPascalCase.cs (e.g., `ICourseService.cs`, `INavigationService.cs`)
- **Service implementations:** PascalCase.cs (e.g., `CourseService.cs`, `NavigationService.cs`)
- **Models:** PascalCase.cs (e.g., `Course.cs`, `Lesson.cs`)
- **Controls:** PascalCase + .xaml for XAML, .cs for code-behind (e.g., `CodingChallenge.xaml`)
- **Content files:** Two-digit prefix + kebab-case + .md or .json
  - `01-theory.md`, `02-key_point.md`, `03-example.md`
  - `challenge.json`, `lesson.json`, `module.json`
- **Code files in challenges:** `starter.{lang}`, `solution.{lang}`
  - Examples: `starter.java`, `solution.cs`, `starter.dart`

**Directories:**

- **Features:** PascalCase (e.g., `Views/`, `Controls/`, `Services/`, `Models/`)
- **Courses:** kebab-case language names (e.g., `java/`, `csharp/`, `flutter/`)
- **Modules:** Number prefix + kebab-case (e.g., `01-java-fundamentals/`, `02-data-types-loops-and-methods/`)
- **Lessons:** Number prefix + kebab-case (e.g., `01-lesson-11-what-is-a-computer-program/`)
- **Challenges:** Number prefix + kebab-case (e.g., `01-your-first-hello-world/`, `02-print-your-own-message/`)

## Where to Add New Code

**New Feature (UI + Logic):**
- Primary code: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Views\` (new View file)
- Service layer: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Services\` (service interface + implementation)
- Tests: `C:\Users\dasbl\Downloads\Code-Tutor\native-app.Tests\` (corresponding test file)

**New Custom Control:**
- Implementation: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Controls\` (PascalCase.xaml + .xaml.cs)
- Follow existing patterns: Use XAML for layout, code-behind for interaction logic
- Register in App.xaml resources if globally used

**New Service:**
- Interface: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Services\I{ServiceName}.cs`
- Implementation: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Services\{ServiceName}.cs`
- Register in MainWindow ServiceCollection (line 21-26 in MainWindow.xaml.cs)

**New Data Model:**
- Location: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Models\` (PascalCase.cs)
- Pattern: Use JsonPropertyName attributes for JSON deserialization
- Example: See Course.cs for complete model pattern

**New Course Content:**
- Location: `C:\Users\dasbl\Downloads\Code-Tutor\content\courses\{language}/modules/`
- Structure: Follow existing module/lesson/challenge hierarchy
- Metadata: Create lesson.json with id, title, moduleId, order, estimatedMinutes, difficulty
- Content: Create numbered markdown files (01-theory.md, 02-key_point.md, etc.)
- Challenges: Create challenge.json with type, testCases, hints, followed by starter and solution files

**Utilities & Helpers:**
- Shared helpers: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Services\` or new Utilities folder
- Converters: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Converters\`
- Behaviors: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Behaviors\`

## Special Directories

**obj/ and bin/:**
- Purpose: Build outputs and compiled assemblies
- Generated: Yes (build artifacts)
- Committed: No (in .gitignore)
- Note: Can safely delete, will be regenerated on next build

**dist/ and publish/:**
- Purpose: Packaged executables and installers
- Generated: Yes (build output)
- Committed: No (in .gitignore)
- Contents:
  - `publish/` - Self-contained portable executable (CodeTutor.Native.exe)
  - `dist/` - Windows installer (.exe created by Inno Setup)

**dist-electron/ (in apps/desktop/):**
- Purpose: Legacy Electron desktop build artifacts
- Status: Archived, not actively maintained
- Note: Included for reference only

**apps/web/dist/:**
- Purpose: Legacy web application build
- Status: Archived, not actively maintained
- Note: Included for reference only

**Content Embedding:**
- The `content/` directory is embedded in the WPF executable at build time
- Configured in CodeTutor.Wpf.csproj (lines 22-28):
  ```xml
  <None Include="..\content\**\*">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    <Link>Content\%(RecursiveDir)%(Filename)%(Extension)</Link>
  </None>
  ```
- Loaded at runtime from: `Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "courses")`

---

*Structure analysis: 2026-02-02*
