# Challenge Components

React UI components for the Code Tutor platform's interactive learning challenges.

## Overview

This directory contains all the React components needed to display and interact with different types of coding challenges. The components are built with TypeScript, Tailwind CSS, and follow accessibility best practices.

## Components

### 🎯 ChallengeContainer

Main wrapper component that determines challenge type and renders the appropriate component.

```tsx
import { ChallengeContainer } from '@/components/challenges'

function LessonPage() {
  return (
    <ChallengeContainer
      challenge={challenge}
      lessonId="lesson-1"
      onComplete={(result) => {
        console.log('Challenge completed!', result)
      }}
      onError={(error) => {
        console.error('Submission failed:', error)
      }}
    />
  )
}
```

**Props:**
- `challenge` - Challenge data (any Challenge type)
- `lessonId` - Lesson ID for tracking
- `onComplete?` - Callback when challenge is completed successfully
- `onError?` - Callback when submission fails
- `apiEndpoint?` - Custom API endpoint (default: `/api/challenges/submit`)
- `className?` - Custom styling

---

### 📝 MultipleChoiceChallenge

Displays a multiple choice question with radio button options.

```tsx
import { MultipleChoiceChallenge } from '@/components/challenges'

const challenge = {
  id: 'mc-1',
  type: 'MULTIPLE_CHOICE',
  title: 'What is a variable?',
  description: 'Select the best answer',
  options: [
    'A container for storing data',
    'A type of function',
    'A programming language',
    'A syntax error'
  ],
  correctAnswer: 0, // Can also be 'A', 'B', etc.
  explanation: 'A variable is a container for storing data values.',
  hints: [
    { level: 1, text: 'Think about what stores information in code' }
  ]
}

<MultipleChoiceChallenge
  challenge={challenge}
  onSubmit={(isCorrect, hintsUsed) => {
    console.log(`Correct: ${isCorrect}, Hints: ${hintsUsed}`)
  }}
/>
```

**Features:**
- Radio button options
- Visual feedback (green/red borders)
- Explanation after submission
- Optional hints
- Keyboard navigation
- ARIA labels

---

### ✅ TrueFalseChallenge

Displays a true/false question with two button options.

```tsx
import { TrueFalseChallenge } from '@/components/challenges'

const challenge = {
  id: 'tf-1',
  type: 'TRUE_FALSE',
  title: 'True or False',
  question: 'JavaScript is a compiled language',
  correctAnswer: false,
  explanation: 'JavaScript is an interpreted language, not compiled.',
  hints: [
    { level: 1, text: 'Consider how JavaScript code is executed' }
  ]
}

<TrueFalseChallenge
  challenge={challenge}
  onSubmit={(isCorrect, hintsUsed) => {
    console.log(`Correct: ${isCorrect}`)
  }}
/>
```

**Features:**
- Large True/False buttons with icons
- Visual feedback
- Explanation after submission
- Optional hints
- Accessible button controls

---

### 💻 CodeOutputChallenge

Displays a code snippet and asks users to predict the output.

```tsx
import { CodeOutputChallenge } from '@/components/challenges'

const challenge = {
  id: 'co-1',
  type: 'CODE_OUTPUT',
  title: 'Predict the Output',
  description: 'What will this code print?',
  code: `function greet(name) {
  return "Hello, " + name + "!";
}
console.log(greet("World"));`,
  language: 'javascript',
  correctOutput: 'Hello, World!',
  explanation: 'The function concatenates strings and returns the greeting.',
  hints: [
    { level: 1, text: 'Look at what the function returns' }
  ]
}

<CodeOutputChallenge
  challenge={challenge}
  onSubmit={(isCorrect, hintsUsed) => {
    console.log(`Correct: ${isCorrect}`)
  }}
/>
```

**Features:**
- Monaco editor with syntax highlighting (read-only)
- Textarea for user output
- Side-by-side comparison after submission
- Normalizes whitespace for comparison
- Optional hints

---

### 🚀 FreeCodingChallenge

Full-featured coding challenge with Monaco editor and test runner.

```tsx
import { FreeCodingChallenge } from '@/components/challenges'

const challenge = {
  id: 'fc-1',
  type: 'FREE_CODING',
  title: 'Sum Function',
  instructions: 'Write a function that adds two numbers and returns the result.',
  starterCode: 'function sum(a, b) {\n  // Your code here\n}',
  solution: 'function sum(a, b) {\n  return a + b;\n}',
  language: 'javascript',
  testCases: [
    {
      description: 'Adds positive numbers',
      inputs: [2, 3],
      expectedOutput: 5,
      isVisible: true
    },
    {
      description: 'Adds negative numbers',
      inputs: [-1, -2],
      expectedOutput: -3,
      isVisible: true
    },
    {
      description: 'Hidden test',
      inputs: [100, 200],
      expectedOutput: 300,
      isVisible: false
    }
  ],
  hints: [
    { level: 1, text: 'Use the + operator' },
    { level: 2, text: 'Return the sum of a and b', code: 'return a + b;' }
  ],
  commonMistakes: [
    {
      mistake: 'Forgetting to return',
      consequence: 'Function returns undefined',
      correction: 'Add a return statement',
      example: {
        wrong: 'function sum(a, b) { a + b; }',
        right: 'function sum(a, b) { return a + b; }'
      }
    }
  ],
  bonusChallenges: [
    {
      id: 'bonus-1',
      title: 'Sum Multiple Numbers',
      description: 'Modify the function to accept any number of arguments',
      difficulty: 3
    }
  ]
}

<FreeCodingChallenge
  challenge={challenge}
  onTestRun={(results, code) => {
    console.log('Tests run:', results)
  }}
  onComplete={(code, hintsUsed, testResults) => {
    console.log('Challenge completed!')
  }}
/>
```

**Features:**
- Monaco code editor with syntax highlighting
- Run tests button
- Test results panel with pass/fail indicators
- Progressive hints system
- Show/hide solution toggle
- Common mistakes panel (expandable)
- Bonus challenges (shown after completion)
- Reset code functionality
- Hidden tests (revealed after visible tests pass)

---

### ✏️ CodeCompletionChallenge

Similar to FreeCodingChallenge but emphasizes completing partial code.

```tsx
import { CodeCompletionChallenge } from '@/components/challenges'

const challenge = {
  id: 'cc-1',
  type: 'CODE_COMPLETION',
  title: 'Complete the Function',
  description: 'Fill in the missing parts to make this function work',
  starterCode: `function multiply(a, b) {
  // TODO: Return the product of a and b
}`,
  solution: `function multiply(a, b) {
  return a * b;
}`,
  language: 'javascript',
  testCases: [
    {
      description: 'Multiplies positive numbers',
      inputs: [2, 3],
      expectedOutput: 6,
      isVisible: true
    }
  ],
  hints: [
    { level: 1, text: 'Use the * operator for multiplication' }
  ]
}

<CodeCompletionChallenge
  challenge={challenge}
  onTestRun={(results, code) => {
    console.log('Tests run:', results)
  }}
  onComplete={(code, hintsUsed, testResults) => {
    console.log('Challenge completed!')
  }}
/>
```

**Features:**
- Same as FreeCodingChallenge but with completion-focused UI
- Auto-highlights TODO markers
- Purple-themed instructions panel
- Emphasizes "filling in the blanks"

---

### 💡 HintsPanel

Progressive hints reveal system.

```tsx
import { HintsPanel } from '@/components/challenges'

const hints = [
  { level: 1, text: 'Think about the loop condition' },
  { level: 2, text: 'You need to check if i < length' },
  { level: 3, text: 'Use a for loop', code: 'for(int i=0; i<arr.length; i++)' }
]

<HintsPanel
  hints={hints}
  onHintUsed={(count) => {
    console.log(`${count} hints used`)
  }}
/>
```

**Features:**
- Progressive reveal (one hint at a time)
- Color-coded by difficulty (blue, orange, red)
- Warning about score impact
- Code snippets in hints
- Expandable/collapsible

---

### ✔️ TestResultsPanel

Displays test results with pass/fail indicators.

```tsx
import { TestResultsPanel } from '@/components/challenges'

const results = [
  {
    testCase: {
      description: 'Test basic addition',
      expectedOutput: 5,
      isVisible: true
    },
    passed: true,
    actualOutput: 5,
    expectedOutput: 5
  },
  {
    testCase: {
      description: 'Test negative numbers',
      expectedOutput: -3,
      isVisible: true
    },
    passed: false,
    actualOutput: -1,
    expectedOutput: -3,
    errorMessage: 'Expected -3 but got -1'
  }
]

<TestResultsPanel
  results={results}
  isLoading={false}
/>
```

**Features:**
- Pass/fail indicators (checkmark/X)
- Progress bar
- Expandable details for failed tests
- Side-by-side expected vs actual output
- Hidden tests (revealed after visible tests pass)
- Error messages

---

### ⚠️ CommonMistakesPanel

Displays common mistakes in expandable format.

```tsx
import { CommonMistakesPanel } from '@/components/challenges'

const mistakes = [
  {
    mistake: 'Using = instead of ==',
    consequence: 'Assignment instead of comparison',
    correction: 'Use == or === for comparison',
    example: {
      wrong: 'if (x = 5) { }',
      right: 'if (x === 5) { }'
    }
  }
]

<CommonMistakesPanel
  mistakes={mistakes}
  defaultExpanded={false}
/>
```

**Features:**
- Expandable panel
- Table format (Mistake | Consequence | Correction)
- Code examples (wrong vs right)
- Side-by-side comparison with color coding

---

## Usage Patterns

### Simple Usage (Recommended)

Use `ChallengeContainer` for automatic routing:

```tsx
import { ChallengeContainer } from '@/components/challenges'

function ChallengePage({ challenge, lessonId }) {
  return (
    <ChallengeContainer
      challenge={challenge}
      lessonId={lessonId}
      onComplete={(result) => {
        // Handle completion
        if (result.passed) {
          router.push('/next-lesson')
        }
      }}
    />
  )
}
```

### Direct Component Usage

Use specific components for custom layouts:

```tsx
import {
  MultipleChoiceChallenge,
  HintsPanel
} from '@/components/challenges'

function CustomChallenge({ challenge }) {
  return (
    <div className="grid grid-cols-2 gap-4">
      <MultipleChoiceChallenge challenge={challenge} />
      <HintsPanel hints={challenge.hints} />
    </div>
  )
}
```

## Styling

All components use Tailwind CSS and support dark mode. They accept a `className` prop for custom styling:

```tsx
<ChallengeContainer
  challenge={challenge}
  lessonId="lesson-1"
  className="max-w-6xl mx-auto p-8"
/>
```

## Accessibility

All components follow accessibility best practices:

- ✅ Keyboard navigation
- ✅ ARIA labels and roles
- ✅ Screen reader support
- ✅ Focus management
- ✅ Semantic HTML

## Animation

Components use Tailwind CSS animations:

- `animate-in fade-in slide-in-from-top-2` - Smooth entrance
- `animate-spin` - Loading spinners
- `transition-all` - Smooth state changes

## API Integration

The `ChallengeContainer` automatically handles API submissions. Expected API response format:

```typescript
interface ValidationResponse {
  success: boolean
  passed: boolean
  score?: number
  message: string
  testResults?: TestResult[]
  compilationError?: string
  runtimeError?: string
  output?: string
  feedback?: string
}
```

Example API endpoint:

```typescript
// POST /api/challenges/submit
{
  challengeId: "fc-1",
  lessonId: "lesson-1",
  userAnswer: "function sum(a, b) { return a + b; }",
  hintsUsed: 2
}
```

## Testing

Components can be tested with React Testing Library:

```tsx
import { render, screen, fireEvent } from '@testing-library/react'
import { MultipleChoiceChallenge } from '@/components/challenges'

test('renders multiple choice challenge', () => {
  render(<MultipleChoiceChallenge challenge={mockChallenge} />)

  expect(screen.getByText(mockChallenge.title)).toBeInTheDocument()
  expect(screen.getByText('Submit Answer')).toBeInTheDocument()
})
```

## Browser Support

- Chrome/Edge: ✅ Full support
- Firefox: ✅ Full support
- Safari: ✅ Full support
- Mobile browsers: ✅ Responsive design

## Dependencies

- `react` - React library
- `@monaco-editor/react` - Code editor
- `react-markdown` - Markdown rendering
- `lucide-react` - Icons
- `clsx` - Conditional classnames
- `tailwindcss` - Styling

## Performance

- Monaco editor uses lazy loading
- Components use React.memo where appropriate
- Efficient re-rendering with proper state management
- Code splitting supported

## Future Enhancements

- [ ] Real-time collaboration
- [ ] AI-powered hints
- [ ] Video explanations
- [ ] Peer review system
- [ ] Gamification elements
- [ ] Mobile app support

## Contributing

When adding new challenge types:

1. Create component in `/components/challenges/`
2. Add to `ChallengeContainer` switch statement
3. Export from `index.ts`
4. Update this README
5. Add tests
6. Update TypeScript types in `/types/interactive.ts`

## Support

For issues or questions, contact the Code Tutor development team.
