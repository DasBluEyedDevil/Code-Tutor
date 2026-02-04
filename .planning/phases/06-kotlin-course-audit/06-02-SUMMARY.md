---
phase: 06-kotlin-course-audit
plan: 02
subsystem: content
tags: [kotlin, accuracy, coroutines, null-safety, oop, collections, flows]
requires:
  - phase: 06-kotlin-course-audit
    plan: 01
    provides: Kotlin 2.3 version targets and structural fixes
provides:
  - M01-M05 verified accurate against Kotlin 2.3 and kotlinx.coroutines 1.10.x
  - M04/M05 coroutines overlap assessed and documented with transition note
  - Zero deprecated patterns across 44 lessons
affects: [06-03, 06-04, 06-05, 06-06, 06-07, 06-09, 06-10]
tech-stack:
  added: []
  patterns: []
key-files:
  created: []
  modified:
    - content/courses/kotlin/modules/01-the-absolute-basics/lessons/10-lesson-110-whats-new-in-kotlin-20-k2-compiler/content/04-example.md
    - content/courses/kotlin/modules/01-the-absolute-basics/lessons/10-lesson-110-whats-new-in-kotlin-20-k2-compiler/content/06-example.md
    - content/courses/kotlin/modules/04-advanced-kotlin/lessons/08-lesson-48-coroutines-fundamentals/content/15-theory.md
  deleted: []
key-decisions:
  - "M04/M05 coroutines overlap: KEPT both -- M04 L08-L09 are preview, M05 is deep-dive (transition note added)"
  - "GlobalScope references in M05 L02 kept as anti-pattern warnings (pedagogically correct)"
  - "Kotlin version references in M03 L04 (since 1.0), M03 L05 (since 1.5), M05 L07 (pre-1.7.20 memory model) kept as historical context"
  - "M01 L10 version-tagged framing cleaned: 'Kotlin 2.0 in Practice' -> 'Modern Kotlin in Practice'"
duration: 7min
completed: 2026-02-04
---

# Phase 6 Plan 02: Accuracy Pass M01-M05 Summary

44 lessons across M01-M05 (~684 content files) verified against Kotlin 2.3 and kotlinx.coroutines 1.10.x with only 3 minor version-framing fixes; M04/M05 coroutines overlap kept with transition note.

## What Was Done

### Task 1: Accuracy Pass M01-M03 (24 lessons)

**M01 - The Absolute Basics (10 lessons)**:
- L01-L09: All content accurate -- val/var, type inference, null safety (?., ?:, !!), string templates, when expressions, collections, functions, extension functions, varargs, scope, capstone calculator all use current patterns
- L10 (K2 Compiler): 3 version-framing fixes applied:
  - Title "Kotlin 2.0 in Practice" -> "Modern Kotlin in Practice"
  - Code comment "Kotlin 2.0 code examples" -> "Modern Kotlin code examples"
  - Code comment "Kotlin 2.0 has smarter smart casts" -> "The K2 compiler has smarter smart casts"
  - String literal "Hello Kotlin 2.0" -> "Hello Kotlin"
- L10 Kotlin 2.0/2.3 historical references in prose kept (accurate: K2 was introduced in 2.0)

**M02 - Controlling the Flow (7 lessons)**:
- 100% accurate, zero corrections needed
- if/else, logical operators, when expressions, for/while/do-while loops, break/continue all current
- Deep, well-structured lessons with exercises, quizzes, and solutions

**M03 - Object-Oriented Programming (7 lessons)**:
- 100% accurate, zero corrections needed
- Classes, constructors, properties (custom getters/setters, lateinit, lazy, backing fields, delegation)
- Inheritance, polymorphism, interfaces, abstract classes, data classes, sealed classes, companion objects
- "since Kotlin 1.0" (interface defaults) and "since Kotlin 1.5" (sealed class same-package) kept as factual historical context
- Sealed class uses `data object` pattern (correct modern Kotlin)

### Task 2: Accuracy Pass M04-M05 (20 lessons) + Overlap Assessment

**M04 - Advanced Kotlin (13 lessons)**:
- L01-L07: FP, lambdas, collection operations, scope functions, function composition, generics, capstone -- all current
- L08-L09: Coroutines fundamentals + advanced coroutines -- accurate against kotlinx.coroutines 1.10.x
- L10-L13: Delegation/lazy, annotations/reflection, DSLs/type-safe builders, capstone -- all current
- Zero deprecated patterns

**M05 - Coroutines and Flows (7 lessons)**:
- L01: Coroutine introduction with runBlocking, launch, async, delay -- current
- L02: CoroutineScope, structured concurrency, GlobalScope anti-pattern warnings -- correct
- L03: Dispatchers (Default, IO, Main, Unconfined), withContext -- current
- L04: Exception handling (try-catch, CoroutineExceptionHandler, SupervisorJob) -- current
- L05: Flow reactive streams, operators (map, filter, combine), flowOn, buffer -- current
- L06: StateFlow/SharedFlow with MutableStateFlow.update(), data object pattern -- current
- L07: Flows across platforms (KMP dispatchers, iOS memory model note) -- current
- Zero deprecated patterns (no ConflatedBroadcastChannel, no BroadcastChannel)

**M04/M05 Coroutines Overlap Assessment**:
- **Verdict: KEPT both, transition note added**
- M04 L08 covers: suspend functions, launch/async/runBlocking, scope, context, dispatchers, Job/Deferred, common patterns (75 min)
- M04 L09 covers: structured concurrency, exception handling, Flows, Channels, StateFlow/SharedFlow, context switching (75 min)
- M05 covers same topics across 7 dedicated lessons (~60 min each) with much deeper treatment
- M04 serves as self-contained preview within the Advanced Kotlin module context
- M05 is the production-ready deep-dive module
- Transition note added to M04 L08 summary directing learners to M05 for mastery

## Verification Results

| Check | Result |
|-------|--------|
| All 44 lessons reviewed for Kotlin 2.3 accuracy | PASS (44/44) |
| Zero deprecated patterns (experimentalStdlibApi, JvmDefault, header/impl) | PASS |
| Zero deprecated stdlib (capitalize(), toUpperCase, toLowerCase) | PASS |
| Zero deprecated coroutines (ConflatedBroadcastChannel, BroadcastChannel) | PASS |
| GlobalScope references are anti-pattern warnings only | PASS (6 refs, all in M05 L02 warnings) |
| M04/M05 coroutines overlap assessed and documented | PASS |
| No knowledge cliffs in M01-M05 progression | PASS |

## Deviations from Plan

None -- plan executed exactly as written.

## Commits

| Task | Commit | Description |
|------|--------|-------------|
| 1 | 0b2ffca7 | feat(06-02): accuracy pass M01-M03 (24 lessons verified for Kotlin 2.3) |
| 2 | 0201eacf | feat(06-02): accuracy pass M04-M05 (20 lessons verified) + coroutines overlap assessed |

## Next Phase Readiness

Plan 06-02 confirms M01-M05 are accurate and current for Kotlin 2.3. The coroutines overlap between M04 and M05 is documented and handled with a transition note. Plans 06-03 through 06-05 can now proceed with backend/UI/ecosystem accuracy passes knowing the foundation modules are clean.
