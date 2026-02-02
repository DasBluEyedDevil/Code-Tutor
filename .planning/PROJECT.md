# Code Tutor

## What This Is

A desktop application that teaches programming from zero to "can build and deploy real apps" across six languages: Python, Java, C#, JavaScript, Kotlin, and Flutter/Dart. Built as a WPF app with local Phi-4 LLM tutoring, in-app code execution, and a hierarchical content system of courses, modules, lessons, and coding challenges. The course content was assembled by multiple AI tools across unrelated sessions and has never been systematically audited.

## Core Value

Every course teaches a coherent, progressive path from absolute beginner to independently building and deploying a real application — no gaps, no outdated practices, no dead ends.

## Requirements

### Validated

- ✓ WPF desktop application with course browser, lesson viewer, and code editor — existing
- ✓ Hierarchical content system (course > module > lesson > challenge) with JSON metadata and markdown content — existing
- ✓ C# code execution via Roslyn (in-process, no external dependency) — existing
- ✓ Multi-language code execution via local runtime detection (Python, Java, Node.js, Kotlin, Rust, Dart) — existing
- ✓ Piston API fallback for remote code execution — existing
- ✓ Local Phi-4 LLM integration via ONNX Runtime with DirectML GPU acceleration — existing
- ✓ AI tutor chat interface with streaming responses — existing
- ✓ Syntax highlighting via AvalonEdit — existing
- ✓ Navigation with back/forward history — existing
- ✓ Course content for 6 languages (Python, Java, C#, JavaScript, Kotlin, Flutter/Dart) — existing but unaudited
- ✓ Coding challenges with starter code, solution code, and test case validation — existing
- ✓ Windows installer via Inno Setup — existing
- ✓ Self-contained .NET 8.0 deployment (no runtime required on user machine) — existing

### Active

- [ ] Systematic content audit of all 6 courses for completeness, accuracy, freshness, consistency, and progressive pedagogy
- [ ] Every coding challenge executes and validates correctly in the app
- [ ] All content renders properly (markdown, code blocks, all section types)
- [ ] Content uses 2025-2026 best practices, current stable library/framework versions, no deprecated APIs
- [ ] Consistent voice, tone, and difficulty progression across all courses
- [ ] Each course has a deployable capstone project (create where missing)
- [ ] Each lesson builds on the previous — no knowledge gaps, no assumed context not yet taught
- [ ] UI enhanced beyond basic "read text, type text" to be more interactive and engaging
- [ ] Local LLM tutor provides contextual concept explanations for current lesson
- [ ] Local LLM tutor helps students debug their code when stuck on challenges
- [ ] Local LLM tutor gives progressive hints before revealing answers

### Out of Scope

- Rust course — deprecated, never built beyond stub
- Mobile companion app — desktop-first, mobile deferred
- Cloud-based code execution — local runtimes + Piston sufficient for now
- User accounts / authentication — offline desktop app, no login needed
- Multi-user collaboration / code sharing — single-user learning tool
- Real-time chat with human tutors — LLM handles tutoring

## Context

**Existing codebase:** Mature WPF application with working architecture. The app shell, services, and execution pipeline are functional. The primary risk is content quality, not application architecture.

**Content origin:** Course content was created by multiple AI tools (likely GPT, Claude, and others) across different sessions with no shared style guide, no curriculum review, and no consistency checks. Each session may have made different assumptions about prerequisite knowledge, code style, framework versions, and depth of explanation.

**Content structure:** Six languages, each with multiple modules containing multiple lessons. Each lesson has numbered markdown content sections (theory, key points, examples, analogies, warnings) and coding challenges with starter code, solutions, and test cases. Some courses have capstone projects; others don't.

**Target audience:** Complete beginners who want to become developers capable of building and deploying real applications. Not computer science students — practical, project-oriented learners.

**Tech stack:** WPF (.NET 8.0, C# 13), AvalonEdit for code editing, ONNX Runtime GenAI for local Phi-4 inference, Roslyn for C# execution, local language runtimes for other languages.

**Known issues from codebase analysis:**
- JSON backup files (.bak) scattered in content directories
- Compiled binaries checked into git (csharp capstone bin/obj)
- Course loading is eager (all courses on startup, no lazy loading)
- Test coverage gaps in service layer and ViewModels

## Constraints

- **Platform**: Windows desktop (WPF) — no cross-platform requirement for this milestone
- **LLM**: Local Phi-4 via ONNX Runtime — no cloud API dependency, must work offline
- **Content freshness**: 2025-2026 industry standards — current stable versions of all languages, frameworks, and tools
- **Languages**: Python, Java, C#, JavaScript, Kotlin, Flutter/Dart — six courses, no additions
- **Execution**: Code must run locally via installed runtimes or Roslyn — no Docker requirement for students

## Key Decisions

| Decision | Rationale | Outcome |
|----------|-----------|---------|
| Content quality is primary priority over UI and LLM work | Content is the product; app and LLM are delivery mechanisms | -- Pending |
| All 6 courses audited to same standard simultaneously | Consistency matters more than shipping one perfect course | -- Pending |
| Rust course deprecated | Never built beyond stub, not worth the investment | -- Pending |
| Local LLM (Phi-4) over cloud API | Offline capability, no API costs for students, privacy | -- Pending |
| WPF over web/Electron | Already built and working, no reason to rewrite | -- Pending |

---
*Last updated: 2026-02-02 after initialization*
