# Architecture

**Analysis Date:** 2026-02-02

## Pattern Overview

**Overall:** Multi-layered client-server architecture with content-driven educational platform design.

**Key Characteristics:**
- Desktop WPF client application serving as primary user interface
- JSON-based content structure with hierarchical course/module/lesson organization
- Service-oriented architecture with dependency injection for loose coupling
- Multi-language code execution support via Roslyn (C#) and Piston API (other languages)
- AI-powered tutoring integration using local LLM inference (Phi-4)
- Offline-capable with embedded content distribution

## Layers

**Presentation Layer:**
- Purpose: User-facing UI rendering and interaction management
- Location: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Views\`, `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Controls\`
- Contains: XAML views (CoursePage, LessonPage, LandingPage), custom WPF controls (CodeExampleSection, CodingChallenge, TutorChat)
- Depends on: Service layer (NavigationService, CourseService, CodeExecutionService)
- Used by: MainWindow entry point

**Service Layer:**
- Purpose: Business logic, data management, and external service integration
- Location: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Services\`
- Contains:
  - `CourseService` - loads courses from JSON files, manages lesson hierarchy, caches data
  - `NavigationService` - manages view stack and navigation state
  - `CodeExecutionService` - coordinates code execution across multiple languages
  - `Phi4TutorService` - AI tutoring using local Phi-4 model inference
  - `ModelDownloadService` - manages ONNX model downloads and updates
  - Executor services: `RoslynCSharpExecutor`, `PistonExecutor`
- Depends on: Models layer, external APIs (Piston)
- Used by: Views and MainWindow

**Model Layer:**
- Purpose: Data structures representing domain concepts
- Location: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Models\`
- Contains:
  - `Course` - course metadata, module collection
  - `Module` - module metadata, lesson collection
  - `Lesson` - lesson metadata, content sections, challenges
  - `Challenge` - coding challenge with test cases, hints
  - `ContentSection` - theory, examples, code snippets, legacy content
  - `TestCase`, `Hint` - challenge metadata
- Depends on: System.Text.Json for deserialization

**Content Layer:**
- Purpose: Educational content storage and organization
- Location: `C:\Users\dasbl\Downloads\Code-Tutor\content\courses\`
- Contains: Hierarchical JSON-based course definitions with markdown content files
- Organization: `courses/{language}/{modules}/{module}/lessons/{lesson}/`
  - Each lesson has `lesson.json` metadata
  - Content in `content/` directory with numbered markdown files (01-theory.md, 02-key_point.md, etc.)
  - Challenges in `challenges/` with `challenge.json`, `starter.{ext}`, `solution.{ext}`
- Depends on: CourseService for loading

## Data Flow

**Course Loading & Display:**

1. User launches application → `MainWindow` initializes service collection (DI)
2. `NavigationService.NavigateTo(LandingPage)` → loads landing page view
3. `LandingPage` calls `CourseService.GetAllCoursesAsync()`
4. `CourseService` scans `content/courses/` directory
5. For each course: deserializes `course.json` → `Course` model
6. For each module: loads `modules/{id}/module.json` → `Module` model
7. For each lesson: loads `modules/{id}/lessons/{id}/lesson.json` → `Lesson` model
8. Lessons hydrated with content sections from `content/` markdown files
9. Challenges loaded from `challenges/` subdirectories with code files
10. Returns `List<Course>` to presentation layer
11. `LandingPage` renders course cards with metadata

**Code Execution Flow:**

1. User writes code in `CodingChallenge` control
2. User clicks "Run" button → calls `CodeExecutionService.ExecuteAsync(code, language)`
3. Service selects executor based on language:
   - C#/csharp → `RoslynCSharpExecutor` (in-process, no external dependency)
   - Other languages → attempts `PistonExecutor` (remote API, with fallback if unavailable)
4. Executor compiles/interprets code and captures output
5. Returns `ExecutionResult(Success, Output, Error)`
6. `CodingChallenge` compares output against `TestCase.ExpectedOutput`
7. Shows pass/fail feedback to user

**Tutoring Flow:**

1. User sends message in `TutorChat` control
2. `Phi4TutorService` receives user message
3. Service calls ONNX Runtime GenAI API with local Phi-4 model
4. Model generates contextual response based on current lesson/challenge
5. Response streamed back as `TutorMessage`
6. Chat bubbles rendered in `ChatMessageBubble` control

**State Management:**

- Navigation state: Stack-based history in `NavigationService._history`
- Course data: In-memory cache in `CourseService._courseCache` (ConcurrentDictionary)
- User progress: Tracked in `UserProgress` model (lesson completion, challenge attempts)
- Execution state: Maintained per language in service instances

## Key Abstractions

**INavigationService:**
- Purpose: Provides view navigation with back/forward stack
- Examples: `LandingPage`, `CoursePage`, `LessonPage` all use this to navigate
- Pattern: Service interface with Stack-based implementation for history

**ICourseService:**
- Purpose: Abstracts course content loading and caching
- Examples: Loads from JSON files in `content/courses/` directory
- Pattern: Service with ConcurrentDictionary caching for thread-safe access

**ICodeExecutionService:**
- Purpose: Abstracts multi-language code execution
- Examples: Routes C# to Roslyn, other languages to Piston or local runtimes
- Pattern: Strategy pattern with language-specific executors

**ITutorService:**
- Purpose: Abstracts AI tutoring backend (local vs future cloud)
- Examples: `Phi4TutorService` uses ONNX Runtime for local inference
- Pattern: Service abstraction for LLM interactions

## Entry Points

**MainWindow (Application Entry):**
- Location: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\MainWindow.xaml.cs`
- Triggers: Application startup via App.xaml
- Responsibilities:
  - Configures dependency injection (ServiceCollection)
  - Instantiates all services (CourseService, NavigationService, TutorService)
  - Sets up navigation event handling
  - Renders initial landing page
  - Maintains main content area and sidebar regions

**App (WPF Application Class):**
- Location: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\App.xaml.cs`
- Triggers: OS application launch
- Responsibilities:
  - Applies performance profile on startup
  - Initializes WPF application lifecycle

**LandingPage (Primary User Entry):**
- Location: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Views\LandingPage.xaml`
- Triggers: Called from MainWindow during initialization
- Responsibilities:
  - Loads and displays all available courses
  - Allows user to select a course
  - Navigates to CoursePage when course selected

**CoursePage & LessonPage (Content Display):**
- Locations: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Views\CoursePage.xaml`, `LessonPage.xaml`
- Triggers: Navigation from LandingPage or lesson selection
- Responsibilities:
  - Display course/module/lesson content from loaded models
  - Render content sections (theory, examples, key points)
  - Display coding challenges with code editor
  - Execute code and validate against test cases
  - Provide AI tutor chat interface

## Error Handling

**Strategy:** Defensive with graceful degradation

**Patterns:**

1. **Service Initialization Errors:** Try-catch blocks in CourseService when deserializing JSON
   - Logs error with file path
   - Skips invalid course, continues loading others
   - Example: `C:\Users\dasbl\Downloads\Code-Tutor\native-app-wpf\Services\CourseService.cs` line 54-58

2. **Code Execution Errors:** Separate Success/Error fields in ExecutionResult record
   - Contains compiler messages and runtime errors
   - Example: `CodeExecutionService.ExecuteAsync()` returns both output and error

3. **External Service Fallback:** PistonExecutor failures fall back to local execution
   - `CodeExecutionService` checks PistonAvailable before attempting remote execution
   - Falls through to local language runtimes if Piston unavailable
   - Example: Lines 72-76 in CodeExecutionService.cs

4. **UI Error Display:** Error messages rendered in content controls with visual distinction
   - CodingChallenge shows red error state
   - TutorChat displays service errors to user

## Cross-Cutting Concerns

**Logging:**
- Console/Debug output in services
- LogError method in CourseService for deserialization failures
- Integrated with performance profiling

**Validation:**
- Language validation in CodeExecutionService (non-null, lowercase conversion)
- JSON schema validation via .NET serialization (throws on mismatch)
- Test case visibility flags (isVisible) control which tests shown to user

**Authentication:**
- Not integrated at platform level (content is read-only, no user accounts in base app)
- Tutoring service may have implicit authentication for ONNX model access

**Concurrency:**
- ConcurrentDictionary for course/lesson caching (thread-safe)
- InteractiveProcessSession for managing long-running code execution
- Async/await throughout service layer for non-blocking operations

---

*Architecture analysis: 2026-02-02*
