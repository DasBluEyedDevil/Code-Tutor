# Repository Guidelines

This repository is a monorepo for the Code Tutor platform (web + desktop). Follow these guidelines to keep contributions consistent and easy to review.

## Project Structure & Modules

- Core apps live under `apps/` (`apps/web`, `apps/desktop`).
- Shared content and curricula are in `content/`.
- Documentation lives in `docs/`; helper scripts in `scripts/` and `tools/`.
- Place new runtime code in the relevant `apps/*` package; keep docs and assets in `docs/` or `content/` as appropriate.

## Build, Test, and Development

- `npm install` – install root and workspace dependencies.
- `npm run build` – build all workspaces.
- `npm run build:desktop` – build web + desktop apps.
- `npm run start:desktop` – launch the desktop app in development.
- `npm test` – run tests across all workspaces.
- `npm run lint` – run linting across all workspaces.

## Coding Style & Naming

- Use existing stack conventions in each app (TypeScript/React patterns in `apps/web`, Electron conventions in `apps/desktop`).
- Prefer 2-space indentation and descriptive names (`CourseOverviewPage`, `content-import-service.ts`).
- Keep files small and focused; group features by domain under each app.

## Testing Guidelines

- Mirror existing test setups in each workspace (e.g., place tests alongside code or under a `__tests__` directory).
- Name tests after the unit under test (e.g., `course-import.spec.ts`).
- Ensure new features include unit tests and keep coverage comparable to surrounding modules.

## Commit & Pull Request Practices

- Write clear, imperative commit messages (e.g., `Add course import validation`).
- For PRs, include a short summary, testing notes (`npm test`, `npm run lint`), and screenshots or GIFs for UI changes.
- Link related issues and call out breaking changes explicitly.

## Cursor Cloud specific instructions

### Actual tech stack

Despite the npm-based instructions above (which are outdated/legacy), this codebase is a **.NET 8.0 C#/WPF desktop application**. There is no `apps/` directory, no `package.json`, and no npm/Node.js project. The active code lives in:

- `native-app-wpf/` — the main WPF desktop application (targets `net8.0-windows`)
- `native-app.Tests/` — xUnit test suite (targets `net8.0`, runs on Linux)
- `content/courses/` — educational content (JSON + Markdown, ~6,600 files across 6 languages)

### Prerequisites

- **.NET SDK 9.0** (needed because `CodeTutor.Wpf.csproj` uses `<LangVersion>13.0</LangVersion>`, which requires the .NET 9 compiler). The .NET 8.0 runtime is also installed for the `net8.0` test target.
- The SDKs are installed at `$HOME/.dotnet` and `PATH`/`DOTNET_ROOT` are configured in `~/.bashrc`.

### Build, test, and lint commands

```bash
# Restore NuGet packages (WPF app needs EnableWindowsTargeting on Linux)
dotnet restore native-app-wpf/CodeTutor.Wpf.csproj -p:EnableWindowsTargeting=true
dotnet restore native-app.Tests/native-app.Tests.csproj

# Build the WPF app (cross-compile on Linux)
dotnet build native-app-wpf/CodeTutor.Wpf.csproj -p:EnableWindowsTargeting=true

# Build and run tests (targets net8.0, works natively on Linux)
dotnet test native-app.Tests/native-app.Tests.csproj

# Lint check (C# compiler analysis with warnings-as-errors)
dotnet build native-app-wpf/CodeTutor.Wpf.csproj -p:EnableWindowsTargeting=true -warnaserror
```

### Key caveats

- **WPF GUI cannot run on Linux.** The app targets `net8.0-windows` with `<UseWPF>true</UseWPF>`. You can build and cross-compile but not launch the GUI. The test suite targets `net8.0` (no Windows dependency) and runs fully on Linux.
- **`-p:EnableWindowsTargeting=true`** must be passed to all `dotnet build`/`restore`/`publish` commands for the WPF project on Linux. Without it, you get `NETSDK1100`.
- **29 pre-existing test failures** exist in content validation and user journey tests (e.g., `Course_ShouldHaveValidModules`, `UserJourney_LessonViewing_CanViewLessonContent`). These are course-content data issues, not environment problems. The remaining 232/261 tests pass.
- **No solution file (.sln)** at the repo root. Build each `.csproj` individually.

