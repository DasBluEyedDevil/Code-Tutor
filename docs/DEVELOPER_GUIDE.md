# Code Tutor - Developer Guide

Welcome to the Code Tutor development guide! This document will help you understand the codebase architecture, development workflow, and best practices.

## Table of Contents

1. [Project Overview](#project-overview)
2. [Architecture](#architecture)
3. [Development Setup](#development-setup)
4. [Project Structure](#project-structure)
5. [Core Concepts](#core-concepts)
6. [State Management](#state-management)
7. [Component Guidelines](#component-guidelines)
8. [Testing](#testing)
9. [Performance](#performance)
10. [Error Handling](#error-handling)
11. [Deployment](#deployment)

---

## Project Overview

Code Tutor is a unified learning platform supporting 7 programming languages:
- Python
- Java
- JavaScript/TypeScript
- Kotlin
- Rust
- C#
- Flutter/Dart

**Tech Stack:**
- **Frontend**: React 18 + TypeScript + Vite
- **Styling**: Tailwind CSS
- **State**: Zustand + localStorage persistence
- **Code Editor**: Monaco Editor
- **Testing**: Vitest + React Testing Library
- **Backend**: Node.js + Express
- **Code Execution**: Language-specific Docker containers

---

## Architecture

### Monorepo Structure

```
Code-Tutor/
├── apps/
│   ├── web/           # React frontend application
│   └── api/           # Express backend API
├── packages/          # Shared packages (future)
├── content/           # Course content (source)
├── scripts/           # Build and import scripts
└── docs/              # Documentation
```

### Frontend Architecture

**Component Hierarchy:**
```
App
├── ErrorBoundary
│   ├── Router
│   │   ├── LandingPage
│   │   ├── CoursePage
│   │   └── LessonPage
│   │       ├── CodeEditor (Monaco)
│   │       ├── LessonContent (Markdown)
│   │       ├── ExercisePanel
│   │       └── OutputPanel
```

**State Flow:**
```
User Action → Component → Store → localStorage → UI Update
                    ↓
               API Call → Backend → Executor → Response
```

---

## Development Setup

### Prerequisites

- Node.js 18+
- npm or yarn
- Docker (for code execution)
- Git

### Installation

```bash
# Clone the repository
git clone https://github.com/yourusername/Code-Tutor.git
cd Code-Tutor

# Install dependencies
npm install

# Set up environment variables
cp apps/api/.env.example apps/api/.env
cp apps/web/.env.example apps/web/.env

# Start development servers
npm run dev
```

### Development Commands

```bash
# Run frontend only
npm run dev:web

# Run API only
npm run dev:api

# Run tests
npm test

# Run tests with UI
npm run test:ui

# Run tests with coverage
npm run test:coverage

# Build for production
npm run build

# Type check
npm run type-check

# Lint code
npm run lint
```

---

## Project Structure

### Frontend (`apps/web/`)

```
src/
├── components/        # Reusable UI components
│   ├── Button.tsx
│   ├── Card.tsx
│   ├── CodeEditor.tsx
│   ├── CommandPalette.tsx
│   ├── ErrorBoundary.tsx
│   └── Settings.tsx
│
├── pages/             # Page-level components
│   ├── LandingPage.tsx
│   ├── CoursePage.tsx
│   └── LessonPage.tsx
│
├── stores/            # Zustand state stores
│   ├── achievementsStore.ts
│   ├── preferencesStore.ts
│   ├── progressStore.ts
│   └── themeStore.ts
│
├── hooks/             # Custom React hooks
│   ├── useKeyboardShortcuts.ts
│   ├── useFocusTrap.ts
│   ├── useReducedMotion.ts
│   ├── useMonacoSetup.ts
│   └── useAutoSave.ts
│
├── utils/             # Utility functions
│   ├── monacoConfig.ts
│   ├── errorLogging.ts
│   └── api.ts
│
└── types/             # TypeScript type definitions
    └── course.ts
```

### Backend (`apps/api/`)

```
src/
├── routes/            # API route handlers
│   ├── courses.ts     # Course content endpoints
│   ├── execute.ts     # Code execution endpoints
│   ├── progress.ts    # User progress endpoints
│   └── auth.ts        # Authentication endpoints
│
├── middleware/        # Express middleware
│   └── rateLimit.ts   # Rate limiting
│
├── executors/         # Language execution engines
│   ├── python.ts
│   ├── java.ts
│   ├── javascript.ts
│   └── ...
│
└── index.ts           # Main server file
```

---

## Core Concepts

### 1. Course Structure

```typescript
interface Course {
  courseMetadata: {
    id: string
    language: string
    displayName: string
    description: string
    // ...
  }
  modules: Module[]
  languageConfig: LanguageConfig
}

interface Module {
  id: string
  title: string
  lessons: Lesson[]
}

interface Lesson {
  id: string
  title: string
  content: {
    body: string           // Markdown content
    codeExamples: CodeExample[]
  }
  exercises: Exercise[]
  quiz?: Quiz
}
```

### 2. Code Execution Flow

```
1. User writes code in Monaco Editor
2. Code sent to /api/execute with language parameter
3. Backend routes to appropriate executor
4. Executor runs code in sandboxed environment
5. Results (stdout, stderr, test results) returned
6. Frontend displays output and updates progress
```

### 3. State Persistence

All user data is persisted to localStorage:
- Progress (completed lessons, test scores)
- Preferences (editor settings, theme)
- Achievements (unlocked badges, streak)
- Code (auto-saved work)

---

## State Management

### Zustand Stores

**progressStore** - User learning progress
```typescript
{
  completedLessons: string[]
  currentLesson: string
  testResults: Record<string, TestResult>
  markLessonComplete: (lessonId: string) => void
}
```

**preferencesStore** - User preferences
```typescript
{
  editor: EditorPreferences
  autoSave: boolean
  soundEffects: boolean
  setEditorPreference: (key, value) => void
}
```

**achievementsStore** - Gamification
```typescript
{
  achievements: Achievement[]
  stats: { lessons, streak, tests, codeRuns }
  bookmarks: string[]
  incrementCodeRuns: () => void
}
```

**themeStore** - UI theme
```typescript
{
  theme: 'light' | 'dark'
  motionPreference: 'auto' | 'always' | 'reduced'
  setTheme: (theme) => void
}
```

### Store Pattern

```typescript
import { create } from 'zustand'
import { persist } from 'zustand/middleware'

export const useMyStore = create(
  persist(
    (set, get) => ({
      // State
      count: 0,

      // Actions
      increment: () => set((state) => ({ count: state.count + 1 })),

      // Computed values
      doubleCount: () => get().count * 2,
    }),
    {
      name: 'my-store', // localStorage key
    }
  )
)
```

---

## Component Guidelines

### Component Structure

```typescript
// 1. Imports
import React, { useState, useEffect } from 'react'
import { SomeIcon } from 'lucide-react'
import { Button } from './Button'

// 2. Types
interface MyComponentProps {
  title: string
  onAction?: () => void
}

// 3. Component
export function MyComponent({ title, onAction }: MyComponentProps) {
  // Hooks (always at the top)
  const [state, setState] = useState(false)
  const store = useMyStore()

  useEffect(() => {
    // Side effects
  }, [])

  // Event handlers
  const handleClick = () => {
    setState(true)
    onAction?.()
  }

  // Render
  return (
    <div className="p-4">
      <h2>{title}</h2>
      <Button onClick={handleClick}>Action</Button>
    </div>
  )
}
```

### Performance Optimization

Use `React.memo()` for expensive components:

```typescript
export const ExpensiveComponent = memo(({ data }: Props) => {
  // Component logic
}, (prevProps, nextProps) => {
  // Custom comparison function (optional)
  return prevProps.data === nextProps.data
})
```

Use `useMemo()` for expensive computations:

```typescript
const sortedData = useMemo(() => {
  return data.sort((a, b) => a.value - b.value)
}, [data])
```

Use `useCallback()` for stable function references:

```typescript
const handleClick = useCallback(() => {
  doSomething(value)
}, [value])
```

### Accessibility

- Always include ARIA labels
- Support keyboard navigation
- Manage focus properly
- Respect reduced motion preferences

```typescript
<button
  aria-label="Close dialog"
  onClick={onClose}
  onKeyDown={(e) => e.key === 'Escape' && onClose()}
>
  <X className="w-4 h-4" />
</button>
```

---

## Testing

### Unit Tests

```typescript
import { render, screen, fireEvent } from '@testing-library/react'
import { describe, it, expect, vi } from 'vitest'
import { Button } from './Button'

describe('Button', () => {
  it('renders button text', () => {
    render(<Button>Click me</Button>)
    expect(screen.getByText('Click me')).toBeInTheDocument()
  })

  it('calls onClick handler', () => {
    const handleClick = vi.fn()
    render(<Button onClick={handleClick}>Click</Button>)
    fireEvent.click(screen.getByText('Click'))
    expect(handleClick).toHaveBeenCalledTimes(1)
  })
})
```

### Testing Hooks

```typescript
import { renderHook, act } from '@testing-library/react'
import { useMyStore } from './myStore'

describe('useMyStore', () => {
  it('increments count', () => {
    const { result } = renderHook(() => useMyStore())

    act(() => {
      result.current.increment()
    })

    expect(result.current.count).toBe(1)
  })
})
```

### Test Coverage Goals

- Critical paths: 90%+
- Components: 80%+
- Utilities: 90%+
- Overall: 75%+

---

## Performance

### Bundle Optimization

**Code Splitting:**
```typescript
const HeavyComponent = lazy(() => import('./HeavyComponent'))

<Suspense fallback={<Loading />}>
  <HeavyComponent />
</Suspense>
```

**Manual Chunks (vite.config.ts):**
```typescript
build: {
  rollupOptions: {
    output: {
      manualChunks: {
        'react-vendor': ['react', 'react-dom'],
        'monaco': ['@monaco-editor/react'],
      }
    }
  }
}
```

### Performance Monitoring

```typescript
import { performanceMonitor } from '@/utils/errorLogging'

performanceMonitor.startTiming('heavy-operation')
await doHeavyWork()
performanceMonitor.endTiming('heavy-operation')
```

---

## Error Handling

### Error Boundary

Wrap components that might throw:

```typescript
<ErrorBoundary
  onError={(error, errorInfo) => {
    errorLogger.logError(error, { component: 'MyComponent' })
  }}
>
  <MyComponent />
</ErrorBoundary>
```

### Error Logging

```typescript
import { errorLogger } from '@/utils/errorLogging'

try {
  await riskyOperation()
} catch (error) {
  errorLogger.logError(error as Error, {
    component: 'MyComponent',
    action: 'riskyOperation',
    metadata: { userId: user.id }
  })
}
```

### API Error Handling

```typescript
const response = await fetch('/api/courses/python')

if (!response.ok) {
  if (response.status === 429) {
    // Rate limit exceeded
    const retryAfter = response.headers.get('Retry-After')
    throw new Error(`Rate limited. Retry after ${retryAfter}s`)
  }
  throw new Error(`API error: ${response.statusText}`)
}
```

---

## Deployment

### Build Production Bundle

```bash
npm run build
```

### Environment Variables

**Frontend (.env):**
```
VITE_API_URL=https://api.codetutor.com
VITE_ENABLE_ANALYTICS=true
```

**Backend (.env):**
```
PORT=3001
NODE_ENV=production
CORS_ORIGIN=https://codetutor.com
```

### Docker Deployment

```bash
# Build images
docker build -t code-tutor-web ./apps/web
docker build -t code-tutor-api ./apps/api

# Run containers
docker-compose up -d
```

---

## Best Practices

### TypeScript

- Always type function parameters and return values
- Avoid `any` - use `unknown` or proper types
- Use strict mode
- Leverage type inference where appropriate

### Git Workflow

- Feature branches: `feature/my-feature`
- Fix branches: `fix/bug-description`
- Commit message format: `type: description`
  - `feat:` new feature
  - `fix:` bug fix
  - `docs:` documentation
  - `test:` tests
  - `refactor:` code refactoring
  - `perf:` performance improvement

### Code Style

- Use ESLint and Prettier
- 2-space indentation
- Single quotes for strings
- Trailing commas
- Semicolons required

---

## Troubleshooting

### Common Issues

**Monaco Editor not loading:**
- Check CDN availability
- Verify Monaco webpack plugin configuration
- Clear browser cache

**Rate limiting in development:**
- API rate limits are generous but can be hit during intensive testing
- Use `resetAll()` in rate limiter to clear limits
- Consider disabling rate limiting in development

**Tests failing:**
- Ensure mocks are set up correctly (localStorage, IntersectionObserver, etc.)
- Check that test environment is `jsdom`
- Clear test cache: `npm test -- --clearCache`

---

## Resources

- [React Documentation](https://react.dev)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/)
- [Zustand Documentation](https://github.com/pmndrs/zustand)
- [Monaco Editor API](https://microsoft.github.io/monaco-editor/)
- [Vitest Documentation](https://vitest.dev/)
- [Tailwind CSS](https://tailwindcss.com/)

---

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes with tests
4. Submit a pull request with clear description

For major changes, please open an issue first to discuss proposed changes.

---

## License

[License information here]

---

## Support

- Issues: [GitHub Issues](https://github.com/yourusername/Code-Tutor/issues)
- Discussions: [GitHub Discussions](https://github.com/yourusername/Code-Tutor/discussions)
- Email: support@codetutor.com
