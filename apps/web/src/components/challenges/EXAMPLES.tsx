/**
 * Challenge Components - Usage Examples
 *
 * This file contains complete working examples for all challenge components.
 * Copy and adapt these examples for your own use cases.
 */

import { ChallengeContainer } from './ChallengeContainer'
import { MultipleChoiceChallenge } from './MultipleChoiceChallenge'
import { TrueFalseChallenge } from './TrueFalseChallenge'
import { CodeOutputChallenge } from './CodeOutputChallenge'
import { FreeCodingChallenge } from './FreeCodingChallenge'
import { CodeCompletionChallenge } from './CodeCompletionChallenge'
import {
  Challenge,
  MultipleChoiceChallenge as MCType,
  TrueFalseChallenge as TFType,
  CodeOutputChallenge as COType,
  FreeCodingChallenge as FCType,
  CodeCompletionChallenge as CCType,
} from '../../types/interactive'

// ============================================================================
// Example 1: Using ChallengeContainer (Recommended)
// ============================================================================

export function Example1_ChallengeContainer() {
  const challenge: Challenge = {
    id: 'mc-intro-1',
    type: 'MULTIPLE_CHOICE',
    title: 'What is a variable?',
    description: 'Select the best answer that describes what a variable is in programming.',
    options: [
      'A container for storing data values',
      'A type of function',
      'A programming language',
      'A syntax error',
    ],
    correctAnswer: 0,
    explanation:
      'A variable is a container for storing data values. It acts like a labeled box that can hold different types of information.',
    hints: [
      {
        level: 1,
        text: 'Think about what stores information in your code.',
      },
      {
        level: 2,
        text: 'Variables hold data that can change during program execution.',
      },
    ],
    estimatedMinutes: 2,
  }

  return (
    <div className="max-w-4xl mx-auto p-6">
      <ChallengeContainer
        challenge={challenge}
        lessonId="intro-to-programming"
        onComplete={(result) => {
          console.log('Challenge completed!', result)
          alert(`Challenge completed! Score: ${result.score}`)
        }}
        onError={(error) => {
          console.error('Submission error:', error)
        }}
      />
    </div>
  )
}

// ============================================================================
// Example 2: Multiple Choice Challenge
// ============================================================================

export function Example2_MultipleChoice() {
  const challenge: MCType = {
    id: 'mc-loops-1',
    type: 'MULTIPLE_CHOICE',
    title: 'Which loop is best for iterating over an array?',
    description: 'Consider performance and readability when making your choice.',
    options: [
      'while loop',
      'do-while loop',
      'for loop or forEach',
      'goto statement',
    ],
    correctAnswer: 2,
    explanation:
      'For loops and forEach methods are specifically designed for array iteration and provide the best combination of readability and performance.',
    hints: [
      {
        level: 1,
        text: 'Think about which loops are commonly used with arrays.',
      },
      {
        level: 2,
        text: 'Modern JavaScript provides both traditional and functional approaches.',
      },
    ],
    difficulty: 'beginner',
    estimatedMinutes: 3,
  }

  return (
    <div className="max-w-4xl mx-auto p-6">
      <MultipleChoiceChallenge
        challenge={challenge}
        onSubmit={(isCorrect, hintsUsed) => {
          console.log(`Correct: ${isCorrect}, Hints used: ${hintsUsed}`)
        }}
      />
    </div>
  )
}

// ============================================================================
// Example 3: True/False Challenge
// ============================================================================

export function Example3_TrueFalse() {
  const challenge: TFType = {
    id: 'tf-js-1',
    type: 'TRUE_FALSE',
    title: 'JavaScript Type System',
    description: 'Test your understanding of JavaScript types.',
    question: 'In JavaScript, `0 == false` evaluates to `true` due to type coercion.',
    correctAnswer: true,
    explanation:
      'JavaScript uses type coercion with the `==` operator. When comparing 0 and false, both are coerced to the same type, and the result is true. To avoid this, use strict equality (`===`).',
    hints: [
      {
        level: 1,
        text: 'Consider the difference between == and ===',
      },
      {
        level: 2,
        text: 'Think about what "falsy" values mean in JavaScript',
      },
    ],
    difficulty: 'intermediate',
    estimatedMinutes: 2,
  }

  return (
    <div className="max-w-4xl mx-auto p-6">
      <TrueFalseChallenge
        challenge={challenge}
        onSubmit={(isCorrect, hintsUsed) => {
          console.log(`Answer submitted: ${isCorrect}`)
        }}
      />
    </div>
  )
}

// ============================================================================
// Example 4: Code Output Challenge
// ============================================================================

export function Example4_CodeOutput() {
  const challenge: COType = {
    id: 'co-closure-1',
    type: 'CODE_OUTPUT',
    title: 'Predict the Output: Closure',
    description: 'What will this code print to the console?',
    code: `function makeCounter() {
  let count = 0;
  return function() {
    count++;
    return count;
  };
}

const counter = makeCounter();
console.log(counter());
console.log(counter());
console.log(counter());`,
    language: 'javascript',
    correctOutput: `1
2
3`,
    explanation:
      'This demonstrates a closure. The inner function maintains access to the `count` variable even after `makeCounter()` has finished executing. Each call increments and returns the count.',
    hints: [
      {
        level: 1,
        text: 'The inner function has access to the outer function\'s variables.',
      },
      {
        level: 2,
        text: 'Each call to counter() increments the same count variable.',
      },
      {
        level: 3,
        text: 'The output will be three consecutive numbers starting from 1.',
        code: '1\n2\n3',
      },
    ],
    difficulty: 'intermediate',
    estimatedMinutes: 5,
  }

  return (
    <div className="max-w-5xl mx-auto p-6">
      <CodeOutputChallenge
        challenge={challenge}
        onSubmit={(isCorrect, hintsUsed) => {
          console.log(`Output prediction: ${isCorrect}`)
        }}
      />
    </div>
  )
}

// ============================================================================
// Example 5: Free Coding Challenge
// ============================================================================

export function Example5_FreeCoding() {
  const challenge: FCType = {
    id: 'fc-fibonacci-1',
    type: 'FREE_CODING',
    title: 'Fibonacci Function',
    description: 'Write a function that calculates the nth Fibonacci number.',
    instructions: `Write a function called \`fibonacci\` that takes a number \`n\` as input and returns the nth Fibonacci number.

**Requirements:**
- The function should handle n = 0 (returns 0)
- The function should handle n = 1 (returns 1)
- For n > 1, use the formula: F(n) = F(n-1) + F(n-2)

**Example:**
\`\`\`javascript
fibonacci(0) // returns 0
fibonacci(1) // returns 1
fibonacci(5) // returns 5
fibonacci(10) // returns 55
\`\`\``,
    starterCode: `function fibonacci(n) {
  // Your code here

}`,
    solution: `function fibonacci(n) {
  if (n <= 1) {
    return n;
  }
  return fibonacci(n - 1) + fibonacci(n - 2);
}`,
    language: 'javascript',
    testCases: [
      {
        description: 'Returns 0 for n = 0',
        inputs: [0],
        expectedOutput: 0,
        isVisible: true,
      },
      {
        description: 'Returns 1 for n = 1',
        inputs: [1],
        expectedOutput: 1,
        isVisible: true,
      },
      {
        description: 'Calculates fibonacci(5) correctly',
        inputs: [5],
        expectedOutput: 5,
        isVisible: true,
      },
      {
        description: 'Calculates fibonacci(10) correctly',
        inputs: [10],
        expectedOutput: 55,
        isVisible: false,
      },
    ],
    hints: [
      {
        level: 1,
        text: 'Start by handling the base cases (n = 0 and n = 1).',
      },
      {
        level: 2,
        text: 'For other values, you need to add the previous two Fibonacci numbers.',
      },
      {
        level: 3,
        text: 'Use recursion to call fibonacci(n-1) and fibonacci(n-2).',
        code: 'return fibonacci(n - 1) + fibonacci(n - 2);',
      },
    ],
    commonMistakes: [
      {
        mistake: 'Not handling base cases',
        consequence: 'Function will recurse infinitely and cause stack overflow',
        correction: 'Always check if n <= 1 first',
        example: {
          wrong: 'function fibonacci(n) {\n  return fibonacci(n-1) + fibonacci(n-2);\n}',
          right: 'function fibonacci(n) {\n  if (n <= 1) return n;\n  return fibonacci(n-1) + fibonacci(n-2);\n}',
        },
      },
    ],
    bonusChallenges: [
      {
        id: 'bonus-fib-iterative',
        title: 'Iterative Solution',
        description: 'Rewrite the function using iteration instead of recursion for better performance.',
        difficulty: 3,
      },
      {
        id: 'bonus-fib-memoization',
        title: 'Memoization',
        description: 'Add memoization to cache results and improve recursive performance.',
        difficulty: 4,
      },
    ],
    difficulty: 'intermediate',
    estimatedMinutes: 15,
  }

  return (
    <div className="max-w-7xl mx-auto p-6">
      <FreeCodingChallenge
        challenge={challenge}
        onTestRun={(results, code) => {
          console.log('Test results:', results)
        }}
        onComplete={(code, hintsUsed, testResults) => {
          console.log('Challenge completed!')
          console.log('Final code:', code)
          console.log('Hints used:', hintsUsed)
        }}
      />
    </div>
  )
}

// ============================================================================
// Example 6: Code Completion Challenge
// ============================================================================

export function Example6_CodeCompletion() {
  const challenge: CCType = {
    id: 'cc-array-filter-1',
    type: 'CODE_COMPLETION',
    title: 'Complete the Array Filter',
    description: 'Fill in the missing parts to create a working filter function.',
    starterCode: `function filterEvenNumbers(numbers) {
  // TODO: Use the filter method to return only even numbers
  return numbers.filter(function(num) {
    // TODO: Add condition to check if number is even

  });
}`,
    solution: `function filterEvenNumbers(numbers) {
  return numbers.filter(function(num) {
    return num % 2 === 0;
  });
}`,
    language: 'javascript',
    testCases: [
      {
        description: 'Filters even numbers from [1,2,3,4,5,6]',
        inputs: [[1, 2, 3, 4, 5, 6]],
        expectedOutput: [2, 4, 6],
        isVisible: true,
      },
      {
        description: 'Returns empty array for all odd numbers',
        inputs: [[1, 3, 5, 7]],
        expectedOutput: [],
        isVisible: true,
      },
      {
        description: 'Handles negative numbers',
        inputs: [[-2, -1, 0, 1, 2]],
        expectedOutput: [-2, 0, 2],
        isVisible: false,
      },
    ],
    hints: [
      {
        level: 1,
        text: 'The filter method takes a callback function that returns true/false.',
      },
      {
        level: 2,
        text: 'Use the modulo operator (%) to check if a number is even.',
      },
      {
        level: 3,
        text: 'A number is even if num % 2 === 0',
        code: 'return num % 2 === 0;',
      },
    ],
    commonMistakes: [
      {
        mistake: 'Using num % 2 === 1 for even numbers',
        consequence: 'This checks for odd numbers instead of even',
        correction: 'Use num % 2 === 0 to check if even',
        example: {
          wrong: 'return num % 2 === 1;',
          right: 'return num % 2 === 0;',
        },
      },
    ],
    difficulty: 'beginner',
    estimatedMinutes: 10,
  }

  return (
    <div className="max-w-7xl mx-auto p-6">
      <CodeCompletionChallenge
        challenge={challenge}
        onTestRun={(results, code) => {
          console.log('Test results:', results)
        }}
        onComplete={(code, hintsUsed, testResults) => {
          console.log('Completion successful!')
        }}
      />
    </div>
  )
}

// ============================================================================
// Example 7: Complete Lesson with Multiple Challenges
// ============================================================================

export function Example7_LessonWithChallenges() {
  const challenges: Challenge[] = [
    {
      id: 'lesson1-mc1',
      type: 'MULTIPLE_CHOICE',
      title: 'Question 1: Variables',
      description: 'What keyword is used to declare a constant in JavaScript?',
      options: ['var', 'let', 'const', 'static'],
      correctAnswer: 2,
      explanation: 'The `const` keyword is used to declare constants that cannot be reassigned.',
    },
    {
      id: 'lesson1-tf1',
      type: 'TRUE_FALSE',
      title: 'Question 2: Scope',
      question: '`let` and `const` have block scope in JavaScript.',
      correctAnswer: true,
      explanation: 'Both `let` and `const` are block-scoped, unlike `var` which is function-scoped.',
    },
    {
      id: 'lesson1-fc1',
      type: 'FREE_CODING',
      title: 'Exercise: Variable Declaration',
      instructions: 'Create three variables: name (const), age (let), and isStudent (const).',
      starterCode: '// Declare your variables here\n',
      solution: "const name = 'John';\nlet age = 25;\nconst isStudent = true;",
      language: 'javascript',
      testCases: [
        {
          description: 'Variables are declared',
          expectedOutput: true,
          isVisible: true,
        },
      ],
    },
  ]

  const [currentChallengeIndex, setCurrentChallengeIndex] = React.useState(0)

  const handleChallengeComplete = (result: any) => {
    console.log('Challenge completed:', result)

    // Move to next challenge
    if (currentChallengeIndex < challenges.length - 1) {
      setCurrentChallengeIndex(currentChallengeIndex + 1)
    } else {
      alert('Lesson completed! 🎉')
    }
  }

  return (
    <div className="max-w-6xl mx-auto p-6">
      {/* Progress indicator */}
      <div className="mb-8">
        <div className="flex items-center justify-between mb-2">
          <h2 className="text-xl font-bold">Lesson 1: JavaScript Variables</h2>
          <span className="text-sm text-gray-600">
            Challenge {currentChallengeIndex + 1} of {challenges.length}
          </span>
        </div>
        <div className="w-full bg-gray-200 rounded-full h-2">
          <div
            className="bg-primary h-2 rounded-full transition-all duration-300"
            style={{ width: `${((currentChallengeIndex + 1) / challenges.length) * 100}%` }}
          />
        </div>
      </div>

      {/* Current challenge */}
      <ChallengeContainer
        challenge={challenges[currentChallengeIndex]}
        lessonId="lesson-1-variables"
        onComplete={handleChallengeComplete}
      />
    </div>
  )
}

// Note: Import React at the top of the file
import React from 'react'
