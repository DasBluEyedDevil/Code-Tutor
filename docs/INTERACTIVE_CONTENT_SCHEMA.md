# Interactive Content Schema

This document defines the unified schema for all interactive learning features in Code Tutor.

## Overview

The schema supports interactive features from all 7 programming language courses:
- **Java**: Test-case validation, multiple challenge types, Socratic content structure
- **C#**: Output pattern matching, common sticking points, hints
- **Python**: Quizzes with explanations, multiple question types
- **JavaScript/TypeScript**: Real-time execution, embedded challenges
- **Kotlin**: Progressive hints, difficulty levels, separate quiz files
- **Flutter**: Experiments, bonus challenges, self-validation
- **Rust**: Markdown content (no interactive features)

## Content Section Types

Lessons can include multiple content sections with different pedagogical purposes:

```typescript
type ContentSectionType =
  | 'THEORY'      // Main explanation of concepts
  | 'ANALOGY'     // Real-world analogies
  | 'EXAMPLE'     // Code examples with explanations
  | 'KEY_POINT'   // Important takeaways
  | 'WARNING'     // Common pitfalls
  | 'EXPERIMENT'  // Guided experimentation

interface ContentSection {
  type: ContentSectionType;
  title: string;
  content: string;  // Markdown or HTML
  code?: string;    // Optional code snippet
  language?: string; // Programming language for code
}
```

## Challenge Types

### 1. Multiple Choice

```typescript
interface MultipleChoiceChallenge {
  id: string;
  type: 'MULTIPLE_CHOICE';
  title: string;
  description: string;           // Question text (supports markdown/HTML)
  options: string[];             // Array of answer choices
  correctAnswer: number | string; // Index (0-based) or letter ('A', 'B', etc.)
  explanation: string;           // Explanation shown after answering
  difficulty?: 'beginner' | 'intermediate' | 'advanced';
  hints?: string[];              // Optional progressive hints
}
```

### 2. True/False

```typescript
interface TrueFalseChallenge {
  id: string;
  type: 'TRUE_FALSE';
  title: string;
  question: string;
  correctAnswer: boolean;
  explanation: string;
  hints?: string[];
}
```

### 3. Code Output Prediction

```typescript
interface CodeOutputChallenge {
  id: string;
  type: 'CODE_OUTPUT';
  title: string;
  description: string;
  code: string;                  // Code snippet to analyze
  language: string;              // Programming language
  correctOutput: string;         // Expected output
  explanation: string;
  hints?: string[];
}
```

### 4. Free Coding

Student writes code from scratch, validated by test cases.

```typescript
interface FreeCodingChallenge {
  id: string;
  type: 'FREE_CODING';
  title: string;
  description: string;           // Task description
  instructions: string;          // Step-by-step instructions
  starterCode: string;           // Template code
  solution: string;              // Complete solution
  language: string;              // Programming language
  testCases: TestCase[];         // Automated validation
  hints?: string[];              // Progressive hints
  commonMistakes?: string[];     // Common sticking points
  difficulty?: 1 | 2 | 3 | 4 | 5;
  estimatedMinutes?: number;
  bonusChallenges?: BonusChallenge[];
}
```

### 5. Code Completion

Student completes partial code (fill in the blanks).

```typescript
interface CodeCompletionChallenge {
  id: string;
  type: 'CODE_COMPLETION';
  title: string;
  description: string;
  starterCode: string;           // Partial code with blanks/TODOs
  solution: string;
  language: string;
  testCases: TestCase[];
  hints?: string[];
  commonMistakes?: string[];
  difficulty?: 1 | 2 | 3 | 4 | 5;
}
```

### 6. Conceptual Question

Text-based conceptual understanding (not auto-validated).

```typescript
interface ConceptualChallenge {
  id: string;
  type: 'CONCEPTUAL';
  title: string;
  question: string;
  sampleAnswers?: string[];      // Example good answers
  explanation: string;
  hints?: string[];
}
```

## Test Cases

Test cases validate coding challenges by comparing outputs.

```typescript
interface TestCase {
  id?: string;
  description: string;           // Human-readable description
  inputs?: any[];                // Input parameters (for function testing)
  expectedOutput: any;           // Expected result
  isVisible: boolean;            // If true, student sees this test before running
  testType?: 'exact' | 'contains' | 'regex' | 'pattern';
  customMessage?: string;        // Custom feedback message
}

// Example test cases:
const testCases: TestCase[] = [
  {
    description: "Should print 'Hello, World!'",
    inputs: [],
    expectedOutput: "Hello, World!",
    isVisible: true,
    testType: 'exact'
  },
  {
    description: "Output should contain student name",
    inputs: [],
    expectedOutput: "Alice",
    isVisible: true,
    testType: 'contains'
  },
  {
    description: "Should handle negative numbers",
    inputs: [-5, -3],
    expectedOutput: -15,
    isVisible: false  // Hidden test
  }
];
```

## Validation Rules

Multiple validation strategies supported:

```typescript
interface ValidationRule {
  type: 'output_exact' | 'output_contains' | 'output_regex' | 'code_structure' | 'custom';
  value: string | RegExp;
  caseSensitive?: boolean;
  message?: string;              // Feedback if validation fails
}

// Examples:
const validationRules: ValidationRule[] = [
  {
    type: 'output_contains',
    value: 'Balance',
    caseSensitive: false,
    message: "Your output should display the balance"
  },
  {
    type: 'output_regex',
    value: /\d+\.\d{2}/,  // Match currency format
    message: "Format price as decimal with 2 places"
  },
  {
    type: 'code_structure',
    value: 'foreach',
    message: "You must use a foreach loop for this exercise"
  }
];
```

## Hints System

Progressive hints that reveal more information:

```typescript
interface HintsSystem {
  hints: Hint[];
  maxHintsBeforePenalty?: number;  // Optional: reduce score after N hints
}

interface Hint {
  level: number;                   // 1 = gentle nudge, 5 = almost solution
  text: string;
  code?: string;                   // Optional code snippet
}

// Example:
const hints: Hint[] = [
  {
    level: 1,
    text: "Think about what data type should store decimal numbers."
  },
  {
    level: 2,
    text: "Use the 'double' type for variables that need decimal precision."
  },
  {
    level: 3,
    text: "Declare like this: double price = 19.99;"
  }
];
```

## Common Mistakes / Sticking Points

Proactive guidance on common beginner errors:

```typescript
interface CommonMistake {
  mistake: string;                 // What students do wrong
  consequence: string;             // What happens
  correction: string;              // How to fix it
  example?: {
    wrong: string;
    right: string;
  };
}

// Example:
const commonMistakes: CommonMistake[] = [
  {
    mistake: "Forgetting semicolons",
    consequence: "Error: 'Expected ;'",
    correction: "Every statement in C# needs a semicolon at the end",
    example: {
      wrong: "Console.WriteLine('Hi')",
      right: "Console.WriteLine('Hi');"
    }
  },
  {
    mistake: "Using 'Main' instead of 'main'",
    consequence: "Error: 'Expected 'main''",
    correction: "The entry point must be lowercase: main()"
  }
];
```

## Experiments

Guided experimentation sections:

```typescript
interface Experiment {
  id: string;
  title: string;
  description: string;
  code: string;
  language: string;
  expectedBehavior: string;       // What should happen
  takeaway: string;               // What to learn from this
  isIntentionallyWrong?: boolean; // If true, shows what NOT to do
}
```

## Bonus Challenges

Optional advanced challenges:

```typescript
interface BonusChallenge {
  id: string;
  title: string;
  description: string;
  difficulty: 1 | 2 | 3 | 4 | 5;
  hints?: string[];
  solution?: string;
}
```

## Complete Lesson Schema

Putting it all together:

```typescript
interface Lesson {
  id: string;
  title: string;
  moduleId: string;
  order: number;
  estimatedMinutes: number;
  difficulty: 'beginner' | 'intermediate' | 'advanced';

  // Content sections
  contentSections: ContentSection[];

  // Interactive elements
  challenges?: Challenge[];        // Can include multiple types
  experiments?: Experiment[];
  commonMistakes?: CommonMistake[];

  // Metadata
  learningObjectives?: string[];
  prerequisites?: string[];
  nextSteps?: string[];
  resources?: Resource[];
}

type Challenge =
  | MultipleChoiceChallenge
  | TrueFalseChallenge
  | CodeOutputChallenge
  | FreeCodingChallenge
  | CodeCompletionChallenge
  | ConceptualChallenge;
```

## Quiz Schema

Standalone quizzes (end of module):

```typescript
interface Quiz {
  id: string;
  title: string;
  description: string;
  moduleId: string;
  passingScore: number;            // Percentage (e.g., 70)
  estimatedMinutes: number;
  questions: QuizQuestion[];
  completionMessage?: string;
  retryMessage?: string;
}

type QuizQuestion =
  | MultipleChoiceChallenge
  | TrueFalseChallenge
  | CodeOutputChallenge;
```

## Progress Tracking

```typescript
interface LessonProgress {
  lessonId: string;
  completed: boolean;
  completedAt?: string;            // ISO timestamp
  attempts: number;
  challengesCompleted: number;
  challengesTotal: number;
  score?: number;                  // Percentage
  hintsUsed?: number;
  timeSpentMinutes?: number;
}

interface ChallengeProgress {
  challengeId: string;
  lessonId: string;
  completed: boolean;
  attempts: number;
  passed: boolean;
  score?: number;
  hintsUsed: number;
  lastAttemptAt?: string;
}

interface QuizProgress {
  quizId: string;
  completed: boolean;
  attempts: number;
  bestScore: number;
  passed: boolean;                 // >= passingScore
  lastAttempt?: {
    date: string;
    score: number;
    percentage: number;
    answers: {
      questionId: string;
      userAnswer: any;
      correct: boolean;
    }[];
  };
}
```

## Validation Response

What the backend returns after validating a challenge:

```typescript
interface ValidationResponse {
  success: boolean;
  passed: boolean;
  score?: number;                  // Percentage (0-100)
  message: string;
  testResults?: TestResult[];
  compilationError?: string;
  runtimeError?: string;
  output?: string;
  feedback?: string;
}

interface TestResult {
  testCase: TestCase;
  passed: boolean;
  actualOutput?: any;
  expectedOutput: any;
  errorMessage?: string;
}
```

## Example: Complete Lesson with All Features

```json
{
  "id": "java-epoch1-lesson1",
  "title": "Data Types in Depth",
  "moduleId": "java-epoch1",
  "order": 1,
  "estimatedMinutes": 30,
  "difficulty": "beginner",

  "contentSections": [
    {
      "type": "THEORY",
      "title": "The Problem: Different Kinds of Information",
      "content": "You've learned that variables store information. But not all information is the same..."
    },
    {
      "type": "ANALOGY",
      "title": "Data Types are Like Kitchen Containers",
      "content": "In a kitchen, you have different containers for different things..."
    },
    {
      "type": "KEY_POINT",
      "title": "Choosing the Right Type",
      "content": "How to decide which type to use:\n\n1. Is it a number? → int or double..."
    },
    {
      "type": "WARNING",
      "title": "Type Conversion and Common Mistakes",
      "content": "⚠️ Java is STRICT about types..."
    }
  ],

  "challenges": [
    {
      "id": "java-epoch1-lesson1-quiz1",
      "type": "MULTIPLE_CHOICE",
      "title": "Choosing Data Types",
      "description": "Which data type should you use to store someone's email address?",
      "options": [
        "A) int",
        "B) double",
        "C) String",
        "D) boolean"
      ],
      "correctAnswer": "C",
      "explanation": "Email addresses are text, so use String. int and double are for numbers, boolean is for true/false.",
      "hints": [
        "Think about what kind of data an email address is.",
        "Email addresses contain letters and symbols, not just numbers."
      ]
    },
    {
      "id": "java-epoch1-lesson1-variables",
      "type": "FREE_CODING",
      "title": "Creating Multiple Data Types",
      "description": "Create four variables with different data types and print one of them.",
      "instructions": "Create these variables:\n1. int age = 28\n2. double price = 15.99\n3. String name = \"Bob\"\n4. boolean isActive = true\n\nThen print the 'name' variable.",
      "starterCode": "public class DataTypes {\n    public static void main(String[] args) {\n        // Create your variables here\n        \n        // Print the name variable\n        \n    }\n}",
      "solution": "public class DataTypes {\n    public static void main(String[] args) {\n        int age = 28;\n        double price = 15.99;\n        String name = \"Bob\";\n        boolean isActive = true;\n        \n        System.out.println(name);\n    }\n}",
      "language": "java",
      "testCases": [
        {
          "description": "Should print 'Bob'",
          "inputs": [],
          "expectedOutput": "Bob",
          "isVisible": true,
          "testType": "exact"
        }
      ],
      "hints": [
        "Remember the syntax: type variableName = value;",
        "Strings need double quotes around them.",
        "Use System.out.println() to print."
      ],
      "commonMistakes": [
        {
          "mistake": "Forgetting semicolons",
          "consequence": "Compilation error",
          "correction": "Every statement needs a semicolon at the end"
        },
        {
          "mistake": "Using single quotes for Strings",
          "consequence": "Error: incompatible types",
          "correction": "Strings must use double quotes: \"text\""
        }
      ],
      "difficulty": 2
    }
  ],

  "learningObjectives": [
    "Understand the four essential data types (int, double, String, boolean)",
    "Choose the appropriate type for different kinds of data",
    "Declare and initialize variables with correct syntax"
  ],

  "nextSteps": [
    "Next lesson: Using variables in calculations and operations"
  ]
}
```

## Implementation Notes

1. **Validation**: Each language will have its own code execution engine, but all return the same `ValidationResponse` format.

2. **Test Cases**: Support both visible (shown to students before running) and hidden (used for grading) test cases.

3. **Hints**: Progressive hints start gentle and become more specific. Track how many hints used for scoring.

4. **Common Mistakes**: Displayed proactively to prevent errors before students encounter them.

5. **Content Sections**: Different visual styling based on type (THEORY, ANALOGY, etc.) to enhance learning.

6. **Experiments**: Encourage exploration and understanding through guided experimentation.

7. **Progress**: Track at lesson, challenge, and quiz levels for detailed analytics.

This schema supports ALL interactive features found across all 7 programming language courses while maintaining consistency and extensibility.
