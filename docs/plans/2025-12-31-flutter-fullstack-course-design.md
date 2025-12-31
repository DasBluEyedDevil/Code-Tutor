# Flutter Full-Stack Dart Developer Course Design

**Date:** 2025-12-31
**Goal:** Transform the Flutter course into a comprehensive Full-Stack Dart Developer program
**Outcome:** Learners can build Flutter frontends AND Dart backends, shipping complete production apps

---

## Executive Summary

This design restructures the existing Flutter course (15+ modules) into a cohesive full-stack journey using Dart for both frontend and backend. Key additions include Dart Frog and Serverpod backend modules, MVVM architecture patterns, comprehensive testing (frontend + backend), production operations, and a Social/Chat App capstone.

### Key Decisions

| Decision | Choice | Rationale |
|----------|--------|-----------|
| Backend Language | Dart (not Java/Python) | One language, shared models, reduced context-switching |
| Backend Frameworks | Dart Frog + Serverpod | Frog for fundamentals, Serverpod for production |
| Architecture | MVVM with Riverpod | Pragmatic, testable, builds on existing Riverpod content |
| Capstone | Social/Chat App | Exercises real-time, media, auth, offline — all critical patterns |
| Testing | Backend + Frontend sections | Full-stack developers must test both sides |
| Deployment | Full production ops | Store submission, CI/CD, monitoring, crash reporting |

---

## Course Structure

### Phase 1: Foundations (Existing + Updates)

#### Module 0: Environment Setup (5 lessons — UPDATE)

| Lesson | Updates Needed |
|--------|----------------|
| 0.1 Installing Flutter | Version-agnostic language, link to `CURRENT_VERSIONS.md` |
| 0.2 Editor Setup | Minor refresh only |
| 0.3 Hello World | No changes |
| 0.4 Emulator vs Device | Generic SDK instructions |
| 0.5 Troubleshooting | **Major update**: Add Impeller troubleshooting section |

**New Impeller Content for 0.5:**

```markdown
## Impeller Rendering Issues

Impeller is Flutter's default renderer (iOS since 3.29, Android API 29+ since 3.38).
Most devices work flawlessly, but some Android devices have driver issues.

**Symptoms:**
- Visual glitches or jank on specific Android devices
- Blank screens or rendering artifacts
- Performance worse than expected

**Quick Fix — Disable Impeller temporarily:**
flutter run --no-enable-impeller

**Permanent Fix — In AndroidManifest.xml:**
<meta-data
    android:name="io.flutter.embedding.android.EnableImpeller"
    android:value="false" />

**Note:** Some Exynos chips are auto-blocklisted and fall back to OpenGL automatically.
```

#### Module 1: Dart Fundamentals (8 lessons — NO CHANGES)

Existing content is comprehensive.

#### Modules 2-4: UI & Interactivity (NO STRUCTURAL CHANGES)

Existing content is solid.

---

### Phase 2: Architecture & State (Consolidate)

#### Module 5: MVVM Architecture with Riverpod (10 lessons — RESTRUCTURE)

Consolidates existing Modules 5 + 13 into unified architecture-first approach.

| Lesson | Title | Content |
|--------|-------|---------|
| 5.1 | Why Architecture Matters | Spaghetti code problems, maintainability, testability |
| 5.2 | MVVM Pattern Explained | Model-View-ViewModel roles, data flow diagrams |
| 5.3 | Project Structure | Feature-first folders: `features/auth/`, `features/chat/` |
| 5.4 | Riverpod Fundamentals | Providers, ref, watch vs read |
| 5.5 | ViewModels with Notifier | `Notifier` and `NotifierProvider` |
| 5.6 | AsyncValue & Loading States | Handling async data elegantly |
| 5.7 | Riverpod Generator | Code generation with `@riverpod` |
| 5.8 | Dependency Injection | Using `ref` for DI, overriding for tests |
| 5.9 | Flutter Hooks (Optional) | `useState`, `useEffect`, `useMemoized` |
| 5.10 | Mini-Project: Refactor Notes App | Refactor Module 4's Notes App to MVVM |

#### Module 6: Navigation (9 lessons — MINOR UPDATE)

Keep existing content. Update Lesson 6.3 (GoRouter) to show Riverpod auth-guarded routes.

---

### Phase 3: Dart Backend (NEW CONTENT)

#### Module 7: Dart Frog Fundamentals (8 lessons — NEW)

| Lesson | Title | Content |
|--------|-------|---------|
| 7.1 | Why Dart on the Backend? | Full-stack Dart benefits, comparison to Node/Python |
| 7.2 | Dart Frog Setup | Install CLI, create project, file structure, hot reload |
| 7.3 | File-Based Routing | Routes from file paths, dynamic routes `[id].dart` |
| 7.4 | Request & Response | Reading body, headers, returning JSON, status codes |
| 7.5 | Middleware | Auth checks, logging, CORS, chaining |
| 7.6 | Database Integration | PostgreSQL connection, raw SQL, environment configs |
| 7.7 | Authentication | JWT creation/validation, password hashing, protected routes |
| 7.8 | Mini-Project: REST API | Complete CRUD API, deploy to Railway/Fly.io |

#### Module 8: Serverpod Production Backend (10 lessons — NEW)

| Lesson | Title | Content |
|--------|-------|---------|
| 8.1 | Dart Frog vs Serverpod | When to use each, batteries-included approach |
| 8.2 | Serverpod Setup | CLI, project creation, Docker/PostgreSQL, VS Code extension |
| 8.3 | Models & Code Generation | YAML models, `serverpod generate`, type-safe client |
| 8.4 | Endpoints & Methods | Creating endpoints, automatic client generation |
| 8.5 | Database & ORM | ORM, relations, migrations, transactions |
| 8.6 | Authentication Module | Built-in auth with Google/Apple/Email |
| 8.7 | Real-Time Streams | Server-to-client streaming, auto-reconnection |
| 8.8 | File Storage | Upload/download, cloud storage integration |
| 8.9 | Background Tasks | Scheduled jobs, future calls, task queues |
| 8.10 | Mini-Project: Chat Backend | Complete chat server with auth, messages, real-time |

#### Module 9: Backend Testing (5 lessons — NEW)

| Lesson | Title | Content |
|--------|-------|---------|
| 9.1 | Testing Philosophy | Test pyramid for backends |
| 9.2 | Unit Testing Dart Code | Pure function tests, mocking with Mocktail |
| 9.3 | Testing Dart Frog Routes | Request/response testing, middleware tests |
| 9.4 | Testing Serverpod Endpoints | Integration tests with test database |
| 9.5 | API Contract Testing | Ensuring frontend/backend stay in sync |

---

### Phase 4: Full-Stack Integration (REVISED)

#### Module 10: API Integration & Auth Flows (8 lessons — REWRITE)

| Lesson | Title | Content |
|--------|-------|---------|
| 10.1 | Connecting Flutter to Dart Backend | Serverpod client, type-safe API calls |
| 10.2 | HTTP Fundamentals (External APIs) | Dio package, interceptors, error handling |
| 10.3 | JSON Serialization | `json_serializable`, `freezed` |
| 10.4 | Auth Flow: Registration | Sign-up → backend → token storage |
| 10.5 | Auth Flow: Login & Sessions | Login, refresh tokens, session persistence |
| 10.6 | Auth Flow: OAuth | Social login with Serverpod auth |
| 10.7 | Auth-Guarded Navigation | Riverpod + GoRouter auth state |
| 10.8 | Mini-Project: Auth System | Complete registration/login/logout |

#### Module 11: Real-Time Features (6 lessons — NEW)

| Lesson | Title | Content |
|--------|-------|---------|
| 11.1 | Real-Time Patterns | Polling vs WebSockets vs Streams |
| 11.2 | Serverpod Streams | Subscribe to server streams |
| 11.3 | Building a Chat UI | Message list, input, real-time send/receive |
| 11.4 | Presence & Typing Indicators | "User is typing...", online status |
| 11.5 | Push Notifications | FCM setup, foreground/background handling |
| 11.6 | Mini-Project: Live Chat | Real-time messaging with typing indicators |

#### Module 12: Offline-First & Persistence (7 lessons — CONSOLIDATE)

| Lesson | Title | Content |
|--------|-------|---------|
| 12.1 | Offline-First Principles | Why offline matters |
| 12.2 | Local Storage Options | SharedPreferences, Hive, Drift, Isar |
| 12.3 | Drift Setup & Queries | Type-safe SQL, DAOs, reactive queries |
| 12.4 | Drift Migrations | Schema versioning, data migration |
| 12.5 | Sync Engine Design | Local-first writes, background sync, conflicts |
| 12.6 | Optimistic UI Updates | Update UI immediately, sync in background |
| 12.7 | Mini-Project: Offline Notes | Works offline, syncs when online |

---

### Phase 5: Production Readiness (NEW/EXPANDED)

#### Module 13: Frontend Testing (7 lessons — NEW)

| Lesson | Title | Content |
|--------|-------|---------|
| 13.1 | Testing Pyramid for Flutter | Unit vs Widget vs Integration |
| 13.2 | Unit Testing Business Logic | Testing ViewModels, mocking with Mocktail |
| 13.3 | Widget Testing Fundamentals | `testWidgets`, `WidgetTester`, finders |
| 13.4 | Widget Testing with Riverpod | Overriding providers, testing state |
| 13.5 | Golden Tests | Visual regression testing |
| 13.6 | Integration Tests | `integration_test` package, full user flows |
| 13.7 | TDD Workflow | Red-green-refactor |

#### Module 14: Advanced UI (9 lessons — NEW)

| Lesson | Title | Content |
|--------|-------|---------|
| 14.1 | Implicit Animations | `AnimatedContainer`, `AnimatedOpacity` |
| 14.2 | Explicit Animations | `AnimationController`, `Tween` |
| 14.3 | Hero & Page Transitions | Shared element transitions |
| 14.4 | Rive & Lottie | Complex animations from design tools |
| 14.5 | Responsive Layouts | `LayoutBuilder`, `MediaQuery`, breakpoints |
| 14.6 | Adaptive Platform UI | Material vs Cupertino |
| 14.7 | Accessibility Fundamentals | Semantics, screen readers |
| 14.8 | Accessibility Implementation | Focus, contrast, touch targets |
| 14.9 | Internationalization (i18n) | `intl` package, ARB files |

#### Module 15: Deployment & DevOps (8 lessons — NEW)

| Lesson | Title | Content |
|--------|-------|---------|
| 15.1 | Build Flavors & Environments | dev/staging/prod configs |
| 15.2 | Android Release Build | Signing, keystore, APK/AAB |
| 15.3 | iOS Release Build | Certificates, provisioning, archive |
| 15.4 | Play Store Submission | Store listing, screenshots, rollout |
| 15.5 | App Store Submission | App Store Connect, TestFlight |
| 15.6 | Backend Deployment | Serverpod to Railway/Fly.io |
| 15.7 | CI/CD with GitHub Actions | Automated testing, builds |
| 15.8 | Web & Desktop Builds | Vercel/Netlify, desktop packaging |

#### Module 16: Production Operations (6 lessons — NEW)

| Lesson | Title | Content |
|--------|-------|---------|
| 16.1 | Crash Reporting | Sentry, Firebase Crashlytics |
| 16.2 | Analytics | Firebase Analytics, custom events |
| 16.3 | Performance Monitoring | Firebase Performance, traces |
| 16.4 | Feature Flags | Remote config, gradual rollouts |
| 16.5 | App Updates & Versioning | Semantic versioning, forced updates |
| 16.6 | Serverpod Insights | Logs, health metrics, debugging |

---

### Phase 6: Capstone

#### Module 17: Social/Chat App Capstone (12 lessons — NEW)

| Lesson | Title | What Learners Build |
|--------|-------|---------------------|
| 17.1 | Project Setup & Architecture | Monorepo: `app/`, `server/`, `shared/` |
| 17.2 | Backend: Models & Database | User, Post, Message, Comment models |
| 17.3 | Backend: Auth Endpoints | Registration, login, OAuth |
| 17.4 | Backend: Posts & Comments API | CRUD, pagination |
| 17.5 | Backend: Real-Time Chat | WebSocket streams, message persistence |
| 17.6 | Backend: Media Upload | Image/video upload, thumbnails |
| 17.7 | Frontend: Auth Screens | Login, register, form validation |
| 17.8 | Frontend: Feed & Posts | Infinite scroll, create post, interactions |
| 17.9 | Frontend: Chat UI | Conversations, real-time messages |
| 17.10 | Frontend: Profile & Settings | Edit profile, themes |
| 17.11 | Offline & Sync | Cache locally, sync when online |
| 17.12 | Deploy & Launch | Backend deploy, store submission, Crashlytics |

**Capstone Features Checklist:**

- User authentication (Serverpod auth, OAuth)
- Posts with images (file upload, feed UI)
- Real-time chat (WebSockets, typing indicators)
- Push notifications (FCM)
- Offline support (Drift, sync engine)
- Responsive UI (phone/tablet, accessibility)
- Full test coverage (unit, widget, integration, backend)
- Production deployment (CI/CD, store submission, monitoring)

---

## Supporting Content

### Living Version Reference: `CURRENT_VERSIONS.md`

```markdown
# Current Recommended Versions (Updated: December 2025)

## Flutter & Dart
- Flutter SDK: 3.38.x (stable channel)
- Dart SDK: 3.10.x

## Android
- Android SDK: API 35 (latest stable)
- Build Tools: 35.0.0
- NDK: r28 (required for 16KB page size)
- Java: 17 (minimum required)
- Gradle: 8.14+

## iOS
- Xcode: 16.x
- iOS Deployment Target: 12.0+
- CocoaPods: 1.15+

## Backend
- Dart Frog CLI: 1.x
- Serverpod: 3.x ("Industrial")
- PostgreSQL: 16.x
- Docker: 24.x+

## Key Packages
- riverpod: 2.6+
- go_router: 14+
- dio: 5.x
- drift: 2.x
- freezed: 2.x
```

### Troubleshooting Appendix

| Section | Content |
|---------|---------|
| A.1 Impeller Issues | Android device-specific problems, disable flags, blocklisted chips |
| A.2 Android Build Issues | Gradle versions, SDK updates, 16KB page size, Java 17 |
| A.3 iOS Build Issues | CocoaPods, Xcode versions, signing problems |
| A.4 Serverpod Troubleshooting | Docker issues, database connections, code generation |
| A.5 Common Runtime Errors | Null safety, provider scope, async gaps |

---

## Implementation Priority

| Priority | Modules | Effort | Rationale |
|----------|---------|--------|-----------|
| HIGH | 7, 8, 9 (Dart Backend) | New content | Core differentiator |
| HIGH | 17 (Capstone) | New content | Proves job-readiness |
| MEDIUM | 5 (MVVM consolidation) | Restructure | Foundation for all modules |
| MEDIUM | 10, 11, 12 (Integration) | Partial rewrite | Connect to new backend |
| LOWER | 13, 14, 15, 16 (Production) | New content | Important but follows core |
| LOWER | 0 (Setup updates) | Minor edits | Quick wins |

---

## Research Sources

- [Dart Frog Official](https://dart-frog.dev/) — Minimalist Dart backend framework
- [Serverpod Official](https://serverpod.dev/) — Full-featured Dart backend
- [Serverpod 3 "Industrial" Release](https://medium.com/serverpod/serverpod-3-industrial-robust-authentication-and-a-new-web-server-5b1152863beb) — Latest features
- [Flutter 3.38 Release Notes](https://docs.flutter.dev/release/release-notes/release-notes-3.38.0) — Impeller, Android updates
- [Flutter 3.38 Blog Post](https://blog.flutter.dev/whats-new-in-flutter-3-38-3f7b258f7228) — Feature overview
- [Flutter Testing Documentation](https://docs.flutter.dev/testing/overview) — Testing best practices
- [roadmap.sh/flutter](https://roadmap.sh/flutter) — Community Flutter roadmap

---

## Primary Rule Compliance

This design ensures every lesson:
- **Completely conveys the topic** — No stubs, placeholders, or TODOs
- **Guides newbie to full-stack developer** — Progressive skill building
- **All text is complete and thorough** — Each lesson stands alone
- **Clearly understood** — Beginner-friendly language with technical depth
