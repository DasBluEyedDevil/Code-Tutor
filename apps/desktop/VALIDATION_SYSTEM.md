# Code Validation System - Implementation Guide

This document describes the comprehensive code validation system implemented for the Code Tutor desktop app backend.

## Overview

The validation system provides automated grading for all interactive challenge types:
- **Free Coding**: Execute user code and validate against test cases
- **Code Completion**: Fill-in-the-blank validation
- **Multiple Choice**: Answer selection validation
- **True/False**: Boolean question validation
- **Code Output**: Output prediction validation

## Architecture

### Files Created

1. **`/apps/desktop/src/challenge-validator.ts`** (650+ lines)
   - Core validation logic
   - Test case comparison functions
   - Challenge-type specific validators
   - Helper utilities

2. **`/apps/desktop/src/types.ts`** (250+ lines)
   - TypeScript type definitions
   - Shared interfaces for challenges, test cases, and validation responses

### Files Modified

3. **`/apps/desktop/src/api-server.ts`**
   - Added `POST /api/challenges/validate` endpoint
   - Added `POST /api/challenges/validate-visible` endpoint

## API Endpoints

### 1. Validate Challenge Submission

**Endpoint**: `POST /api/challenges/validate`

**Request Body**:
```json
{
  "challenge": {
    "id": "java-lesson1-challenge1",
    "type": "FREE_CODING",
    "title": "Print Hello World",
    "language": "java",
    "testCases": [
      {
        "description": "Should print 'Hello, World!'",
        "expectedOutput": "Hello, World!",
        "isVisible": true,
        "testType": "exact"
      }
    ]
  },
  "userSubmission": {
    "challengeId": "java-lesson1-challenge1",
    "lessonId": "java-lesson1",
    "userAnswer": "public class Main {\n  public static void main(String[] args) {\n    System.out.println(\"Hello, World!\");\n  }\n}",
    "hintsUsed": 0
  }
}
```

**Response**:
```json
{
  "success": true,
  "passed": true,
  "score": 100,
  "message": "Excellent! All 1 test case passed!",
  "testResults": [
    {
      "testCase": {
        "description": "Should print 'Hello, World!'",
        "expectedOutput": "Hello, World!",
        "isVisible": true,
        "testType": "exact"
      },
      "passed": true,
      "actualOutput": "Hello, World!",
      "expectedOutput": "Hello, World!"
    }
  ],
  "output": "Hello, World!"
}
```

### 2. Validate Visible Test Cases Only

**Endpoint**: `POST /api/challenges/validate-visible`

**Request Body**:
```json
{
  "code": "print('Hello')",
  "language": "python",
  "testCases": [
    {
      "description": "Visible test",
      "expectedOutput": "Hello",
      "isVisible": true,
      "testType": "exact"
    },
    {
      "description": "Hidden test",
      "expectedOutput": "Hello, World!",
      "isVisible": false,
      "testType": "exact"
    }
  ]
}
```

**Response**: Same format as full validation, but only tests visible test cases.

## Test Types

The system supports 4 test validation types:

### 1. Exact Match (`'exact'`)
Outputs must match exactly (after trimming whitespace).

```typescript
{
  testType: 'exact',
  expectedOutput: "42",
  // Passes if output is exactly "42"
}
```

### 2. Contains (`'contains'`)
Output must contain the expected string.

```typescript
{
  testType: 'contains',
  expectedOutput: "Error",
  // Passes if output contains "Error" anywhere
}
```

### 3. Regex Match (`'regex'`)
Output must match a regular expression pattern.

```typescript
{
  testType: 'regex',
  expectedOutput: "\\d+\\.\\d{2}",
  // Passes if output matches number with 2 decimal places
}
```

### 4. Pattern Match (`'pattern'`)
Flexible matching with placeholders:
- `{number}` - matches any number (int or float)
- `{string}` - matches any non-empty string
- `{any}` - matches any content
- `*` - wildcard

```typescript
{
  testType: 'pattern',
  expectedOutput: "Total: ${number}",
  // Passes for "Total: $42.99", "Total: $100", etc.
}
```

## Challenge-Specific Validation

### Free Coding & Code Completion

```typescript
import { validateFreeCoding } from './challenge-validator';

const result = await validateFreeCoding(
  userCode,
  'python',
  testCases
);
```

**Features**:
- Executes code using existing `executeCode` from `executors.ts`
- Validates against all test cases
- Distinguishes compilation errors from runtime errors
- Calculates score as percentage of passed tests
- Provides detailed feedback for failed tests
- Handles timeouts (10 second limit)

### Multiple Choice

```typescript
import { validateMultipleChoice } from './challenge-validator';

const result = validateMultipleChoice(
  userAnswer, // 0, 1, 2, or "A", "B", "C"
  correctAnswer // 2 or "C"
);
```

**Features**:
- Normalizes numeric and letter answers
- Case-insensitive comparison
- Binary pass/fail (100 or 0 score)

### True/False

```typescript
import { validateTrueFalse } from './challenge-validator';

const result = validateTrueFalse(
  userAnswer, // true or false
  correctAnswer // true or false
);
```

### Code Output Prediction

```typescript
import { validateCodeOutput } from './challenge-validator';

const result = validateCodeOutput(
  userPrediction, // "Hello, World!"
  correctOutput   // "Hello, World!"
);
```

**Features**:
- Case-insensitive comparison
- Helpful feedback for incorrect predictions

## Main Validation Function

The `validateChallenge` function routes to the appropriate validator:

```typescript
import { validateChallenge } from './challenge-validator';

const result = await validateChallenge(challenge, userSubmission);
```

**Supported Challenge Types**:
- `FREE_CODING`
- `CODE_COMPLETION`
- `MULTIPLE_CHOICE`
- `TRUE_FALSE`
- `CODE_OUTPUT`
- `CONCEPTUAL` (auto-passes, not graded)

## Validation Response Format

All validators return a consistent `ValidationResponse`:

```typescript
interface ValidationResponse {
  success: boolean;        // Was validation successful (not same as passed)
  passed: boolean;         // Did user pass the challenge
  score?: number;          // 0-100 percentage
  message: string;         // Human-friendly message
  testResults?: TestResult[]; // Detailed test results (coding challenges)
  compilationError?: string;  // Compilation error message
  runtimeError?: string;      // Runtime error message
  output?: string;            // Actual code output
  feedback?: string;          // Additional feedback
}
```

## Error Handling

### Compilation Errors
Detected by keywords in error output:
- `error:`, `Error:`, `SyntaxError`
- `cannot find symbol`, `expected`

```json
{
  "success": false,
  "passed": false,
  "score": 0,
  "message": "Code failed to compile. Please fix the errors and try again.",
  "compilationError": "Main.java:3: error: ';' expected\n    System.out.println(\"Hello\")\n                                ^"
}
```

### Runtime Errors
Any other execution failures:

```json
{
  "success": false,
  "passed": false,
  "score": 0,
  "message": "Code execution failed. Please check for runtime errors.",
  "runtimeError": "ZeroDivisionError: division by zero"
}
```

### Timeouts
Execution timeout after 10 seconds:

```json
{
  "success": false,
  "passed": false,
  "score": 0,
  "runtimeError": "Execution timeout (10 seconds)"
}
```

## Helper Functions

### Validate Only Visible Tests

```typescript
import { validateVisibleTestCases } from './challenge-validator';

// For "Run" button during development
const result = await validateVisibleTestCases(code, language, testCases);
```

### Get Hints from Failed Tests

```typescript
import { getHintsFromFailedTests } from './challenge-validator';

const hints = getHintsFromFailedTests(testResults);
// Returns array of error messages from failed tests
```

### Check Common Syntax Issues

```typescript
import { checkCommonSyntaxIssues } from './challenge-validator';

const { hasIssues, issues } = checkCommonSyntaxIssues(code, 'java');
// Returns basic static analysis warnings
```

## Usage Examples

### Example 1: Free Coding Challenge (Python)

```javascript
// Frontend sends:
fetch('http://localhost:3001/api/challenges/validate', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    challenge: {
      id: 'python-lesson2-fizzbuzz',
      type: 'FREE_CODING',
      title: 'FizzBuzz',
      language: 'python',
      testCases: [
        {
          description: 'Test with 3',
          expectedOutput: 'Fizz',
          isVisible: true,
          testType: 'exact'
        },
        {
          description: 'Test with 5',
          expectedOutput: 'Buzz',
          isVisible: false,
          testType: 'exact'
        },
        {
          description: 'Test with 15',
          expectedOutput: 'FizzBuzz',
          isVisible: false,
          testType: 'exact'
        }
      ]
    },
    userSubmission: {
      challengeId: 'python-lesson2-fizzbuzz',
      lessonId: 'python-lesson2',
      userAnswer: `
n = int(input())
if n % 15 == 0:
    print('FizzBuzz')
elif n % 3 == 0:
    print('Fizz')
elif n % 5 == 0:
    print('Buzz')
else:
    print(n)
      `,
      hintsUsed: 1
    }
  })
});
```

### Example 2: Multiple Choice

```javascript
fetch('http://localhost:3001/api/challenges/validate', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    challenge: {
      id: 'java-lesson1-quiz1',
      type: 'MULTIPLE_CHOICE',
      title: 'What is a variable?',
      options: ['A storage location', 'A function', 'A class', 'A loop'],
      correctAnswer: 0, // or "A"
      explanation: 'A variable is a storage location in memory.'
    },
    userSubmission: {
      challengeId: 'java-lesson1-quiz1',
      lessonId: 'java-lesson1',
      userAnswer: 0, // or "A"
      hintsUsed: 0
    }
  })
});
```

### Example 3: Pattern Matching

```javascript
{
  testType: 'pattern',
  expectedOutput: 'Balance: ${number}',
  // Matches: "Balance: $100.00", "Balance: $42.50", etc.
}

{
  testType: 'pattern',
  expectedOutput: 'Hello, {string}!',
  // Matches: "Hello, Alice!", "Hello, Bob!", etc.
}

{
  testType: 'pattern',
  expectedOutput: 'Array: [*]',
  // Matches: "Array: [1, 2, 3]", "Array: []", etc.
}
```

## Integration with Frontend

### React Example

```typescript
import { useState } from 'react';

function ChallengePage({ challenge }) {
  const [code, setCode] = useState(challenge.starterCode);
  const [result, setResult] = useState(null);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async () => {
    setLoading(true);
    try {
      const response = await fetch('http://localhost:3001/api/challenges/validate', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          challenge,
          userSubmission: {
            challengeId: challenge.id,
            lessonId: challenge.lessonId,
            userAnswer: code,
            hintsUsed: 0
          }
        })
      });

      const validationResult = await response.json();
      setResult(validationResult);

      if (validationResult.passed) {
        // Save progress, show success message, unlock next challenge
      } else {
        // Show feedback, offer hints
      }
    } catch (error) {
      console.error('Validation failed:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1>{challenge.title}</h1>
      <CodeEditor value={code} onChange={setCode} />
      <button onClick={handleSubmit} disabled={loading}>
        {loading ? 'Validating...' : 'Submit'}
      </button>

      {result && (
        <div className={result.passed ? 'success' : 'error'}>
          <h3>{result.message}</h3>
          <p>Score: {result.score}%</p>

          {result.testResults && (
            <ul>
              {result.testResults.map((test, i) => (
                <li key={i} className={test.passed ? 'pass' : 'fail'}>
                  {test.testCase.description}
                  {!test.passed && test.errorMessage && (
                    <p className="error">{test.errorMessage}</p>
                  )}
                </li>
              ))}
            </ul>
          )}

          {result.compilationError && (
            <pre className="error">{result.compilationError}</pre>
          )}

          {result.runtimeError && (
            <pre className="error">{result.runtimeError}</pre>
          )}
        </div>
      )}
    </div>
  );
}
```

## Testing

### Test the API Endpoint

```bash
# Start the desktop app
cd apps/desktop
npm run dev
```

```bash
# Test validation (in another terminal)
curl -X POST http://localhost:3001/api/challenges/validate \
  -H "Content-Type: application/json" \
  -d '{
    "challenge": {
      "id": "test-1",
      "type": "FREE_CODING",
      "title": "Print Hello",
      "language": "python",
      "testCases": [{
        "description": "Should print Hello",
        "expectedOutput": "Hello",
        "isVisible": true,
        "testType": "exact"
      }]
    },
    "userSubmission": {
      "challengeId": "test-1",
      "lessonId": "lesson-1",
      "userAnswer": "print(\"Hello\")",
      "hintsUsed": 0
    }
  }'
```

## Performance Considerations

1. **Execution Timeout**: 10 seconds max per code execution
2. **Parallel Testing**: Test cases run sequentially against same output
3. **Temp File Cleanup**: All temporary files are cleaned up automatically
4. **Error Recovery**: Graceful handling of missing language runtimes

## Security Considerations

1. **Sandboxing**: Code execution runs in isolated processes
2. **Timeouts**: Prevents infinite loops from hanging the system
3. **Resource Limits**: Process spawning includes timeout controls
4. **Input Validation**: All API inputs are validated before processing

## Future Enhancements

Potential improvements:
- [ ] Custom test case inputs (function parameter testing)
- [ ] Memory/performance profiling
- [ ] Code quality metrics (style checking)
- [ ] Plagiarism detection
- [ ] Real-time collaboration
- [ ] Advanced pattern matching with AST analysis

## Troubleshooting

### Language Runtime Not Found

If you see errors like "Python is not installed":
1. Install the required language runtime
2. Ensure it's in system PATH
3. Restart the desktop app

### Compilation Errors Not Showing

Check that error detection keywords are present:
- Java: `error:`, `cannot find symbol`
- Python: `SyntaxError`, `IndentationError`
- C#: `error CS`

### Timeout Issues

If code consistently times out:
1. Check for infinite loops in user code
2. Increase timeout in `executors.ts` if needed (default: 10s)
3. Optimize test case complexity

## Support

For issues or questions:
- Check the interactive content schema: `/docs/INTERACTIVE_CONTENT_SCHEMA.md`
- Review type definitions: `/apps/desktop/src/types.ts`
- Examine executor implementation: `/apps/desktop/src/executors.ts`
