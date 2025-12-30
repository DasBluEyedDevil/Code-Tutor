# Flutter Course Content Quality Review Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Systematically review all 109 lessons in the Flutter & Dart course for accuracy, completeness, freshness, and pedagogical gaps.

**Architecture:** Process each lesson through an AI agent with web search capability. The agent verifies content against Flutter 3.x, Dart 3.x, and modern state management (Riverpod) and architecture patterns.

**Tech Stack:** PowerShell scripts, AI agents with web search, JSON course files, Markdown review reports

---

## Course Overview

- **Course ID:** flutter
- **Total Modules:** 16
- **Total Lessons:** 109
- **Topics Covered:** Dart Basics → Flutter Widgets → Layouts → State Management → Navigation → Networking → Firebase → Native Features → Testing → Advanced (Riverpod, Offline-First)

## Review Criteria

For each lesson, the AI reviewer must:

1. **Accuracy** - Verify code works with Flutter 3.x and Dart 3.x
2. **Completeness** - Ensure all concepts needed for understanding are present
3. **Freshness** - Check against latest Flutter features, patterns, and ecosystem
4. **Pedagogical Gaps** - Identify missing explanations or prerequisite knowledge

---

## Module 0: Environment Setup (7 lessons)

### Tasks 0.1-0.7: Review lessons 0.1 through 0.7

**Topics:** Flutter Installation, IDE Setup, First App, Troubleshooting

**AI Review Focus:**
- Flutter 3.x installation
- Android Studio vs VS Code
- FVM for version management
- Common setup issues

---

## Module 1: Dart Fundamentals (9 lessons)

### Tasks 1.1-1.9: Review lessons 1.1 through 1.9

**Topics:** Variables, Types, Functions, Null Safety, Collections

**AI Review Focus:**
- Dart 3.x features
- Sound null safety
- Pattern matching
- Records
- Sealed classes

---

## Module 2: Flutter Widgets Basics (9 lessons)

### Tasks 2.1-2.9: Review lessons 2.1 through 2.9

**Topics:** StatelessWidget, StatefulWidget, Text, Container, Row/Column

**AI Review Focus:**
- Widget composition patterns
- `const` constructors
- Key usage
- Widget lifecycle

---

## Module 3: Layouts & Lists (6 lessons)

### Tasks 3.1-3.6: Review lessons 3.1 through 3.6

**Topics:** ListView, GridView, Stack, Positioned, Responsive

**AI Review Focus:**
- ListView.builder efficiency
- Sliver widgets
- LayoutBuilder
- MediaQuery patterns

---

## Module 4: User Interaction (4 lessons)

### Tasks 4.1-4.4: Review lessons 4.1 through 4.4

**Topics:** Buttons, Forms, TextField, GestureDetector

**AI Review Focus:**
- Form validation patterns
- TextEditingController lifecycle
- Gesture handling
- Focus management

---

## Module 5: State Management Basics (4 lessons)

### Tasks 5.1-5.4: Review lessons 5.1 through 5.4

**Topics:** setState, Provider, InheritedWidget

**AI Review Focus:**
- Provider package current API
- When to use setState
- Context and providers
- State management decision tree

---

## Module 6: Navigation (9 lessons)

### Tasks 6.1-6.9: Review lessons 6.1 through 6.9

**Topics:** Navigator, Routes, GoRouter, Deep Linking, Tabs

**AI Review Focus:**
- GoRouter as standard
- Navigator 2.0 patterns
- Deep linking setup
- Bottom navigation patterns

---

## Module 7: Networking & APIs (8 lessons)

### Tasks 7.1-7.8: Review lessons 7.1 through 7.8

**Topics:** HTTP, Dio, REST APIs, Error Handling, Image Loading

**AI Review Focus:**
- Dio interceptors
- Retry logic
- Caching strategies
- Image caching (cached_network_image)

---

## Module 8: Firebase Integration (8 lessons)

### Tasks 8.1-8.8: Review lessons 8.1 through 8.8

**Topics:** Firebase Setup, Auth, Firestore, Storage, Messaging

**AI Review Focus:**
- FlutterFire latest
- Firebase Auth patterns
- Firestore queries
- Security rules
- FCM token handling

---

## Module 9: Native Features (8 lessons)

### Tasks 9.1-9.8: Review lessons 9.1 through 9.8

**Topics:** Camera, Location, Sensors, SQLite, Local Storage

**AI Review Focus:**
- Permission handling
- Platform channels
- Drift (formerly Moor) for SQLite
- SharedPreferences alternatives

---

## Module 10: Testing (5 lessons)

### Tasks 10.1-10.5: Review lessons 10.1 through 10.5

**Topics:** Unit Testing, Widget Testing, Integration Testing, Mocking

**AI Review Focus:**
- flutter_test patterns
- Mockito for Dart
- Golden tests
- Integration test setup

---

## Module 11: Animations (5 lessons)

### Tasks 11.1-11.5: Review lessons 11.1 through 11.5

**Topics:** Implicit Animations, Explicit Animations, Hero, Transitions

**AI Review Focus:**
- AnimatedBuilder patterns
- Animation curves
- Rive integration
- Performance considerations

---

## Module 12: Advanced Widgets (5 lessons)

### Tasks 12.1-12.5: Review lessons 12.1 through 12.5

**Topics:** CustomPainter, Slivers, Platform-specific, Accessibility

**AI Review Focus:**
- Custom painting performance
- Sliver protocols
- Semantics for accessibility
- Platform-specific UI

---

## Module 13: Riverpod & Hooks (8 lessons)

### Tasks 13.1-13.8: Review lessons 13.1 through 13.8

**Topics:** Riverpod 2.x, Providers, Notifiers, Hooks

**AI Review Focus:**
- Riverpod 2.x syntax
- `@riverpod` annotation
- AsyncValue handling
- flutter_hooks patterns

---

## Module 14: Multi-Platform (8 lessons)

### Tasks 14.1-14.8: Review lessons 14.1 through 14.8

**Topics:** Web, Desktop, Platform Detection, Responsive

**AI Review Focus:**
- Flutter Web current state
- Desktop support
- Conditional imports
- Responsive frameworks

---

## Module 15: Offline-First Architecture (8 lessons)

### Tasks 15.1-15.8: Review lessons 15.1 through 15.8

**Topics:** Sync Strategies, Drift, Conflict Resolution, Background Sync

**AI Review Focus:**
- Drift (SQLite) patterns
- Sync queue architecture
- Conflict resolution
- Connectivity detection

---

## Execution Steps

### Step 1: Generate all review prompts

```powershell
powershell -File scripts/batch-review-lessons.ps1 -Course flutter
```

### Step 2: Process each prompt with AI agent

For each `flutter-lesson-*-review-prompt.md`:
1. Load prompt
2. AI performs web searches for Flutter 3.x, Dart 3.x, Riverpod documentation
3. Save result as `flutter-lesson-*-review-result.json`

### Step 3: Aggregate results

```powershell
powershell -File scripts/aggregate-reviews.ps1
```

### Step 4: Fix high-priority issues

Review `docs/audits/content-review-summary.md` and apply fixes.

---

## Success Criteria

- [ ] All 109 lessons reviewed
- [ ] All code examples verified against Flutter 3.x / Dart 3.x
- [ ] Riverpod 2.x syntax confirmed
- [ ] All content sections > 50 characters
- [ ] All deprecated patterns flagged and updated
- [ ] All pedagogical gaps documented
