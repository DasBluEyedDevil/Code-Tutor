# JavaScript Course Content Quality Review Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Systematically review all 95 lessons in the JavaScript & TypeScript course for accuracy, completeness, freshness, and pedagogical gaps.

**Architecture:** Process each lesson through an AI agent with web search capability. The agent verifies content against ES2024/ES2025, TypeScript 5.x, and modern tooling (Bun, Node 22, etc.).

**Tech Stack:** PowerShell scripts, AI agents with web search, JSON course files, Markdown review reports

---

## Course Overview

- **Course ID:** javascript
- **Total Modules:** 18
- **Total Lessons:** 95
- **Topics Covered:** JS Basics → Variables → Control Flow → Functions → DOM → Async → TypeScript → Backend (Bun/Hono) → Databases → React → Deployment → Testing

## Review Criteria

For each lesson, the AI reviewer must:

1. **Accuracy** - Verify code works with ES2024/ES2025 and TypeScript 5.x
2. **Completeness** - Ensure all concepts needed for understanding are present
3. **Freshness** - Check against latest JS features, especially ES2024/2025 additions
4. **Pedagogical Gaps** - Identify missing explanations or prerequisite knowledge

---

## Module 1: The Absolute Basics (9 lessons)

### Tasks 1.1-1.9: Review lessons 1.1 through 1.9

**Topics:** What is Programming, Variables, Strings, Numbers, Booleans, Console, Comments

**AI Review Focus:**
- Search: "JavaScript basics 2024 ES2024"
- Modern `let`/`const` guidance (no `var`)
- Template literals as default string approach
- BigInt for large numbers
- Latest console methods

---

## Module 2: Variables & Types (6 lessons)

### Tasks 2.1-2.6: Review lessons 2.1 through 2.6

**Topics:** let/const, Naming, Type Coercion, typeof, null vs undefined

**AI Review Focus:**
- Temporal Dead Zone explanation
- Type coercion edge cases
- Optional chaining (`?.`)
- Nullish coalescing (`??`)

---

## Module 3: Control Flow (5 lessons)

### Tasks 3.1-3.5: Review lessons 3.1 through 3.5

**Topics:** If/Else, Switch, Ternary, Logical Operators, Short-circuit Evaluation

**AI Review Focus:**
- Pattern matching proposals (TC39)
- Switch with fall-through warnings
- Object-based switch alternatives

---

## Module 4: Functions (7 lessons)

### Tasks 4.1-4.7: Review lessons 4.1 through 4.7

**Topics:** Function Declarations, Expressions, Arrow Functions, Parameters, Return, Scope, Closures

**AI Review Focus:**
- Arrow function `this` binding
- Default parameters
- Rest parameters
- Named vs anonymous functions for debugging

---

## Module 5: Arrays & Objects (9 lessons)

### Tasks 5.1-5.9: Review lessons 5.1 through 5.9

**Topics:** Array Methods, Object Basics, Destructuring, Spread, Object Methods, JSON

**AI Review Focus:**
- `Array.at()` for negative indexing
- `Object.hasOwn()` vs `hasOwnProperty`
- `structuredClone()` for deep copy
- Array grouping methods (ES2024)
- `Object.groupBy()` / `Map.groupBy()`

---

## Module 6: Loops & Iteration (5 lessons)

### Tasks 6.1-6.5: Review lessons 6.1 through 6.5

**Topics:** for, while, for...of, for...in, Array iteration methods

**AI Review Focus:**
- `for...of` as preferred iteration
- Iterator helpers (ES2024)
- When to use `forEach` vs `for...of`
- Breaking out of `forEach` alternatives

---

## Module 7: The DOM (6 lessons)

### Tasks 7.1-7.6: Review lessons 7.1 through 7.6

**Topics:** Document Object Model, Selecting Elements, Events, DOM Manipulation

**AI Review Focus:**
- `querySelector`/`querySelectorAll` as standard
- Event delegation patterns
- Intersection Observer
- Mutation Observer
- Modern DOM APIs

---

## Module 8: Modules & Organization (5 lessons)

### Tasks 8.1-8.5: Review lessons 8.1 through 8.5

**Topics:** ES Modules, import/export, Module Patterns, Package Management

**AI Review Focus:**
- ES Modules vs CommonJS current status
- Import attributes (ES2025)
- Dynamic imports
- Top-level await
- Package.json `type: module`

---

## Module 9: Async JavaScript (7 lessons)

### Tasks 9.1-9.7: Review lessons 9.1 through 9.7

**Topics:** Callbacks, Promises, async/await, Error Handling, Fetch API

**AI Review Focus:**
- `Promise.withResolvers()` (ES2024)
- Fetch with streaming
- AbortController patterns
- Error cause chains
- Top-level await in modules

---

## Module 10: TypeScript Fundamentals (6 lessons)

### Tasks 10.1-10.6: Review lessons 10.1 through 10.6

**Topics:** Types, Interfaces, Type Inference, Generics, Utility Types

**AI Review Focus:**
- TypeScript 5.x features
- `satisfies` operator
- Const type parameters
- Template literal types
- `NoInfer` utility type

---

## Module 11: TypeScript Advanced (5 lessons)

### Tasks 11.1-11.5: Review lessons 11.1 through 11.5

**Topics:** Advanced Types, Type Guards, Mapped Types, Conditional Types

**AI Review Focus:**
- TypeScript 5.4/5.5 features
- `using` for resource management
- Decorator metadata
- Type-safe branded types

---

## Module 12: Backend with Bun & Hono (6 lessons)

### Tasks 12.1-12.6: Review lessons 12.1 through 12.6

**Topics:** Bun Runtime, Hono Framework, REST APIs, Middleware

**AI Review Focus:**
- Bun 1.x current features
- Hono latest API
- Comparison with Node.js/Express
- Bun's built-in SQLite, test runner

---

## Module 13: Database Access (5 lessons)

### Tasks 13.1-13.5: Review lessons 13.1 through 13.5

**Topics:** SQLite, Drizzle ORM, Queries, Migrations

**AI Review Focus:**
- Drizzle ORM latest syntax
- Bun SQLite driver
- Transaction patterns
- Type-safe queries

---

## Module 14: React Fundamentals (6 lessons)

### Tasks 14.1-14.6: Review lessons 14.1 through 14.6

**Topics:** Components, JSX, Props, State, Hooks

**AI Review Focus:**
- React 18/19 features
- Server Components awareness
- `use` hook
- `useOptimistic`
- React Compiler (React 19)

---

## Module 15: Testing (7 lessons)

### Tasks 15.1-15.7: Review lessons 15.1 through 15.7

**Topics:** Unit Testing, Jest/Vitest, Mocking, Integration Testing

**AI Review Focus:**
- Vitest as modern choice
- Bun test runner
- Testing Library patterns
- MSW for API mocking

---

## Module 16: TypeScript + React (5 lessons)

### Tasks 16.1-16.5: Review lessons 16.1 through 16.5

**Topics:** Typing React Components, Props, Events, Hooks with Types

**AI Review Focus:**
- React TypeScript patterns 2024
- Generic components
- Discriminated unions for props
- Typing context

---

## Module 17: Modern JavaScript (ES2024+) (5 lessons)

### Tasks 17.1-17.5: Review lessons 17.1 through 17.5

**Topics:** Latest ECMAScript Features, Import Attributes, Decorators

**AI Review Focus:**
- ES2024 finalized features
- ES2025 proposals (Stage 3+)
- Import attributes for JSON/CSS
- Decorators current status
- Temporal API (if Stage 3)

---

## Module 18: Bun & Modern Tooling (6 lessons)

### Tasks 18.1-18.6: Review lessons 18.1 through 18.6

**Topics:** Bun Deep Dive, Bundling, Shell, FFI, Deployment

**AI Review Focus:**
- Bun latest features
- Bun.build()
- Bun.$ shell
- Cross-compilation
- Docker with Bun

---

## Execution Steps

### Step 1: Generate all review prompts

```powershell
powershell -File scripts/batch-review-lessons.ps1 -Course javascript
```

### Step 2: Process each prompt with AI agent

For each `javascript-lesson-*-review-prompt.md`:
1. Load prompt
2. AI performs web searches for ES2024/ES2025/TypeScript 5.x documentation
3. Save result as `javascript-lesson-*-review-result.json`

### Step 3: Aggregate results

```powershell
powershell -File scripts/aggregate-reviews.ps1
```

### Step 4: Fix high-priority issues

Review `docs/audits/content-review-summary.md` and apply fixes.

---

## Success Criteria

- [ ] All 95 lessons reviewed
- [ ] All code examples verified against ES2024/ES2025
- [ ] All TypeScript examples verified against TS 5.x
- [ ] All content sections > 50 characters
- [ ] All deprecated patterns flagged and updated
- [ ] All pedagogical gaps documented
