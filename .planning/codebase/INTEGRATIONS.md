# External Integrations

**Analysis Date:** 2026-02-02

## APIs & External Services

**Code Execution:**
- Piston API (optional, fallback)
  - Default endpoint: `http://localhost:2000`
  - What it's used for: Execute code in languages other than C# when local runtimes unavailable
  - SDK/Client: `PistonExecutor` in `native-app-wpf/Services/Executors/PistonExecutor.cs`
  - Supported languages: Python, JavaScript, Java, Kotlin, Rust, Dart
  - Timeout: 35 seconds per request
  - Availability detection: Checks `/api/v2/runtimes` endpoint on startup
  - Response handling: Parses stdout/stderr/exit code from `/api/v2/execute` POST endpoint

**Model Distribution:**
- Hugging Face Hub (for Phi-4 model downloads)
  - Base URL: `https://huggingface.co`
  - What it's used for: Host and distribute Phi-4 mini ONNX model files
  - Model: `microsoft/Phi-4-mini-instruct-onnx`
  - Path: `gpu/gpu-int4-rtn-block-32`
  - Client: `ModelDownloadService` in `native-app-wpf/Services/ModelDownloadService.cs`
  - Required files:
    - `model.onnx` (main model weights)
    - `model.onnx.data` (model data)
    - `genai_config.json` (generator config)
    - `tokenizer.json` (tokenizer file)
    - `tokenizer_config.json` (tokenizer config)
    - `special_tokens_map.json` (special tokens)
    - `added_tokens.json` (additional tokens)
  - Download method: Direct HTTP downloads with progress tracking
  - User-Agent: `CodeTutor/1.0`

## Data Storage

**Databases:**
- None detected - application uses file-based storage only

**File Storage:**
- Local filesystem only
- User progress: `%LOCALAPPDATA%\CodeTutor\progress.json`
  - Contains: Completed lesson IDs, last updated timestamp
  - Format: JSON serialized `UserProgress` model
  - Implementation: `ProgressService` in `native-app-wpf/Services/ProgressService.cs`

- Course content: Embedded in application distribution
  - Location: `{AppDir}/Content/courses/`
  - Format: JSON course metadata with embedded markdown lesson content
  - Loaded via: `CourseService` in `native-app-wpf/Services/CourseService.cs`

- AI Model: Local disk storage (requires ~4-6 GB for Phi-4)
  - Location: `{AppDir}/models/phi4/gpu/gpu-int4-rtn-block-32/`
  - Lazy downloaded on first AI tutor use

**Caching:**
- In-memory course cache: `ConcurrentDictionary<string, Course>` in `CourseService`
- Runtime detection cache: `ConcurrentDictionary<string, RuntimeInfo>` in `RuntimeDetectionService`
- User progress cache: Single `UserProgress` instance in `ProgressService`

## Authentication & Identity

**Auth Provider:**
- Custom/None - Application does not implement user authentication
- No login system detected
- No user accounts required
- Progress stored locally only (single-user per machine)

## Monitoring & Observability

**Error Tracking:**
- Not implemented - No external error tracking service integrated

**Logs:**
- File-based only
- Code execution logs: `%APPDATA%\CodeTutor\logs\` (referenced in `BUILD.md`, not found in src)
- No structured logging framework detected

## CI/CD & Deployment

**Hosting:**
- Desktop application (no server)
- Windows installer distribution via GitHub Releases (intended)
- Self-contained executable: `publish/CodeTutor.exe` (~80-120 MB)

**CI Pipeline:**
- Not implemented in codebase
- Manual build via PowerShell script (`build-installer.ps1`)
- Local distribution via Inno Setup installer

## Environment Configuration

**Required env vars:**
- None detected - Application uses hardcoded paths and defaults

**Secrets location:**
- No secrets management detected
- No API keys, tokens, or credentials in configuration

## Webhooks & Callbacks

**Incoming:**
- None detected

**Outgoing:**
- None detected - Application does not send webhooks or callbacks

## Local Runtime Requirements (Optional)

The application can execute student code in multiple languages. These are detected at runtime via environment PATH checks:

**Language Runtimes (Optional for Code Execution):**
- Python: Detected via `python --version` command
  - Installation hint: `https://python.org`

- JavaScript (Node.js): Detected via `node --version` command
  - Installation hint: `https://nodejs.org`

- Java: Detected via `java --version` command
  - Installation hint: `https://adoptium.net`

- Kotlin: Detected via `kotlinc -version` command
  - Installation hint: `https://kotlinlang.org/docs/command-line.html`

- Rust: Detected via `rustc --version` command
  - Installation hint: `https://rustup.rs`

- Dart/Flutter: Detected via `dart --version` command
  - Installation hint: `https://dart.dev/get-dart`

- C#: Always available via embedded Roslyn compiler
  - No external runtime required
  - Executes in-process via `RoslynCSharpExecutor`

**Detection Implementation:**
- File: `native-app-wpf/Services/RuntimeDetectionService.cs`
- Process: Spawns each language command with version flag, caches result for session
- Timeout: 5 seconds per detection

**Execution Implementation:**
- File: `native-app-wpf/Services/CodeExecutionService.cs`
- Method: Writes code to temp file, spawns process, captures stdout/stderr/exit code
- Interactive sessions: Supports stdin redirection for Python, JavaScript, Dart
- Java: Compiles first (temp directory), then runs compiled class
- Timeouts: 30 second execution limit

---

*Integration audit: 2026-02-02*
