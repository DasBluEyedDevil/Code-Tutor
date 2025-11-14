# Testing Guide

This directory contains the test setup and utilities for the Code Tutor web application.

## Running Tests

```bash
# Run all tests in watch mode
npm test

# Run tests with UI
npm run test:ui

# Run tests with coverage report
npm run test:coverage
```

## Test Structure

```
src/
├── components/
│   ├── Button.tsx
│   └── __tests__/
│       └── Button.test.tsx
├── hooks/
│   ├── useAutoSave.ts
│   └── __tests__/
│       └── useAutoSave.test.ts
├── stores/
│   ├── themeStore.ts
│   └── __tests__/
│       └── themeStore.test.ts
└── test/
    ├── setup.ts          # Global test setup
    └── README.md         # This file
```

## Writing Tests

### Component Tests

```typescript
import { describe, it, expect } from 'vitest'
import { render, screen } from '@testing-library/react'
import { MyComponent } from '../MyComponent'

describe('MyComponent', () => {
  it('renders correctly', () => {
    render(<MyComponent />)
    expect(screen.getByText('Hello')).toBeInTheDocument()
  })
})
```

### Hook Tests

```typescript
import { describe, it, expect } from 'vitest'
import { renderHook, act } from '@testing-library/react'
import { useMyHook } from '../useMyHook'

describe('useMyHook', () => {
  it('returns expected value', () => {
    const { result } = renderHook(() => useMyHook())
    expect(result.current.value).toBe('expected')
  })
})
```

### Store Tests

```typescript
import { describe, it, expect } from 'vitest'
import { renderHook, act } from '@testing-library/react'
import { useMyStore } from '../myStore'

describe('myStore', () => {
  it('updates state correctly', () => {
    const { result } = renderHook(() => useMyStore())

    act(() => {
      result.current.updateValue('new value')
    })

    expect(result.current.value).toBe('new value')
  })
})
```

## Test Coverage

Run `npm run test:coverage` to generate a coverage report. The report will be available in the `coverage` directory.

Coverage thresholds and exclusions are configured in `vitest.config.ts`.

## Best Practices

1. **Arrange-Act-Assert**: Structure tests with clear setup, action, and assertion phases
2. **Test Behavior, Not Implementation**: Focus on testing what components do, not how they do it
3. **Use Testing Library Queries**: Prefer `screen.getByRole` and `screen.getByLabelText` over `getByTestId`
4. **Mock External Dependencies**: Use `vi.fn()` and `vi.mock()` to isolate unit tests
5. **Clean Up**: The test setup automatically cleans up after each test
6. **Accessibility**: Test for proper ARIA attributes and semantic HTML

## Available Utilities

### From `@testing-library/react`:
- `render`: Render React components
- `screen`: Query rendered elements
- `fireEvent`: Trigger events
- `waitFor`: Wait for async operations
- `renderHook`: Test custom hooks
- `act`: Wrap state updates

### From `@testing-library/jest-dom`:
- `toBeInTheDocument()`
- `toBeVisible()`
- `toBeDisabled()`
- `toHaveClass()`
- `toHaveAttribute()`
- And many more matchers

### From `vitest`:
- `describe`: Group related tests
- `it`/`test`: Define individual tests
- `expect`: Make assertions
- `vi.fn()`: Create mock functions
- `vi.mock()`: Mock modules
- `beforeEach`/`afterEach`: Setup and teardown

## Mocked APIs

The test setup includes mocks for:
- `window.matchMedia` - For testing responsive behavior
- `localStorage` - For testing persistence
- `IntersectionObserver` - For testing visibility
