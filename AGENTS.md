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

