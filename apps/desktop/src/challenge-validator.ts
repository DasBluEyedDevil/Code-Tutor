/**
 * Challenge Validation System for Code Tutor Desktop App
 *
 * Provides comprehensive validation for all interactive challenge types:
 * - Free Coding: Execute code and validate against test cases
 * - Code Completion: Same as free coding
 * - Multiple Choice: Compare user selection with correct answer
 * - True/False: Boolean comparison
 * - Code Output: Predict output validation
 *
 * Supports 4 test types: exact, contains, regex, pattern
 */

import { executeCode } from './executors';
import type {
  Challenge,
  ValidationResponse,
  TestCase,
  TestResult,
  FreeCodingChallenge,
  CodeCompletionChallenge,
  MultipleChoiceChallenge,
  TrueFalseChallenge,
  CodeOutputChallenge,
  ChallengeSubmission,
} from './types';

// ============================================================================
// Test Case Validation Functions
// ============================================================================

/**
 * Compare actual output with expected output based on test type
 *
 * @param actualOutput - The actual output from code execution
 * @param expectedOutput - The expected output from test case
 * @param testType - Type of comparison: 'exact', 'contains', 'regex', 'pattern'
 * @param caseSensitive - Whether comparison should be case-sensitive (default: true)
 * @returns Object with passed status and optional error message
 */
function compareOutput(
  actualOutput: any,
  expectedOutput: any,
  testType: 'exact' | 'contains' | 'regex' | 'pattern' = 'exact',
  caseSensitive: boolean = true
): { passed: boolean; errorMessage?: string } {
  try {
    // Normalize outputs to strings for comparison
    const actual = String(actualOutput).trim();
    const expected = String(expectedOutput).trim();

    switch (testType) {
      case 'exact': {
        // Exact match - outputs must be identical
        const actualCompare = caseSensitive ? actual : actual.toLowerCase();
        const expectedCompare = caseSensitive ? expected : expected.toLowerCase();

        const passed = actualCompare === expectedCompare;
        return {
          passed,
          errorMessage: passed
            ? undefined
            : `Expected exact match: "${expected}", but got: "${actual}"`
        };
      }

      case 'contains': {
        // Contains - actual output must contain expected string
        const actualCompare = caseSensitive ? actual : actual.toLowerCase();
        const expectedCompare = caseSensitive ? expected : expected.toLowerCase();

        const passed = actualCompare.includes(expectedCompare);
        return {
          passed,
          errorMessage: passed
            ? undefined
            : `Expected output to contain: "${expected}", but got: "${actual}"`
        };
      }

      case 'regex': {
        // Regex - actual output must match regex pattern
        try {
          const flags = caseSensitive ? '' : 'i';
          const regex = new RegExp(expected, flags);
          const passed = regex.test(actual);

          return {
            passed,
            errorMessage: passed
              ? undefined
              : `Output did not match pattern: ${expected}. Got: "${actual}"`
          };
        } catch (regexError: any) {
          return {
            passed: false,
            errorMessage: `Invalid regex pattern: ${regexError.message}`
          };
        }
      }

      case 'pattern': {
        // Pattern - flexible matching with wildcards and placeholders
        // Supports: {number}, {string}, {any}, * (wildcard)
        let pattern = expected;

        // Escape special regex characters except our placeholders
        pattern = pattern.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');

        // Replace placeholders with regex patterns
        pattern = pattern.replace(/\\\{number\\\}/g, '\\d+(\\.\\d+)?');
        pattern = pattern.replace(/\\\{string\\\}/g, '.+?');
        pattern = pattern.replace(/\\\{any\\\}/g, '.*?');
        pattern = pattern.replace(/\\\*/g, '.*?');

        try {
          const flags = caseSensitive ? '' : 'i';
          const regex = new RegExp(`^${pattern}$`, flags);
          const passed = regex.test(actual);

          return {
            passed,
            errorMessage: passed
              ? undefined
              : `Output did not match expected pattern. Got: "${actual}"`
          };
        } catch (patternError: any) {
          return {
            passed: false,
            errorMessage: `Invalid pattern: ${patternError.message}`
          };
        }
      }

      default:
        return {
          passed: false,
          errorMessage: `Unknown test type: ${testType}`
        };
    }
  } catch (error: any) {
    return {
      passed: false,
      errorMessage: `Comparison error: ${error.message}`
    };
  }
}

/**
 * Validate a single test case against execution output
 *
 * @param testCase - The test case to validate
 * @param actualOutput - The actual output from code execution
 * @returns TestResult with pass/fail status and details
 */
function validateTestCase(
  testCase: TestCase,
  actualOutput: string
): TestResult {
  const testType = testCase.testType || 'exact';
  const { passed, errorMessage } = compareOutput(
    actualOutput,
    testCase.expectedOutput,
    testType,
    true // caseSensitive - could be made configurable per test case
  );

  return {
    testCase,
    passed,
    actualOutput,
    expectedOutput: testCase.expectedOutput,
    errorMessage: passed ? undefined : (testCase.customMessage || errorMessage)
  };
}

// ============================================================================
// Challenge-Specific Validation Functions
// ============================================================================

/**
 * Validate Free Coding challenge
 * Executes user code and validates against all test cases
 *
 * @param code - User's code submission
 * @param language - Programming language
 * @param testCases - Array of test cases to validate against
 * @returns ValidationResponse with results and score
 */
export async function validateFreeCoding(
  code: string,
  language: string,
  testCases: TestCase[]
): Promise<ValidationResponse> {
  try {
    // Execute the code
    const executionResult = await executeCode(language, code);

    // Handle compilation or runtime errors
    if (!executionResult.success) {
      // Determine if it's a compilation or runtime error based on error message
      const isCompilationError =
        executionResult.error?.includes('error:') ||
        executionResult.error?.includes('Error:') ||
        executionResult.error?.includes('SyntaxError') ||
        executionResult.error?.includes('cannot find symbol') ||
        executionResult.error?.includes('expected');

      return {
        success: false,
        passed: false,
        score: 0,
        message: isCompilationError
          ? 'Code failed to compile. Please fix the errors and try again.'
          : 'Code execution failed. Please check for runtime errors.',
        compilationError: isCompilationError ? executionResult.error : undefined,
        runtimeError: !isCompilationError ? executionResult.error : undefined,
        output: executionResult.output
      };
    }

    // Validate against all test cases
    const testResults: TestResult[] = testCases.map(testCase =>
      validateTestCase(testCase, executionResult.output)
    );

    // Calculate score based on passed tests
    const passedTests = testResults.filter(result => result.passed).length;
    const totalTests = testResults.length;
    const score = totalTests > 0 ? Math.round((passedTests / totalTests) * 100) : 0;
    const allPassed = passedTests === totalTests;

    // Generate feedback message
    let message: string;
    if (allPassed) {
      message = `Excellent! All ${totalTests} test case${totalTests === 1 ? '' : 's'} passed!`;
    } else {
      const failedTests = totalTests - passedTests;
      message = `${passedTests}/${totalTests} test cases passed. ${failedTests} test${failedTests === 1 ? '' : 's'} failed. Keep trying!`;
    }

    // Generate detailed feedback
    const failedTestMessages = testResults
      .filter(result => !result.passed)
      .map(result => result.errorMessage)
      .filter(Boolean)
      .join('\n');

    return {
      success: true,
      passed: allPassed,
      score,
      message,
      testResults,
      output: executionResult.output,
      feedback: failedTestMessages || undefined
    };
  } catch (error: any) {
    return {
      success: false,
      passed: false,
      score: 0,
      message: 'Validation failed due to an unexpected error.',
      runtimeError: error.message || 'Unknown error occurred',
      output: ''
    };
  }
}

/**
 * Validate Code Completion challenge
 * Same as Free Coding - executes code and validates test cases
 *
 * @param code - User's completed code
 * @param language - Programming language
 * @param testCases - Array of test cases to validate against
 * @returns ValidationResponse with results and score
 */
export async function validateCodeCompletion(
  code: string,
  language: string,
  testCases: TestCase[]
): Promise<ValidationResponse> {
  // Code completion uses the same validation as free coding
  return validateFreeCoding(code, language, testCases);
}

/**
 * Validate Multiple Choice challenge
 * Compares user's answer with correct answer
 *
 * @param userAnswer - User's selected answer (index or letter)
 * @param correctAnswer - Correct answer (index or letter)
 * @returns ValidationResponse with pass/fail result
 */
export function validateMultipleChoice(
  userAnswer: number | string,
  correctAnswer: number | string
): ValidationResponse {
  try {
    // Normalize answers to comparable format
    const normalizeAnswer = (answer: number | string): string => {
      if (typeof answer === 'number') {
        return answer.toString();
      }
      // Remove any whitespace and convert to uppercase for letter answers
      return answer.toString().trim().toUpperCase();
    };

    const normalizedUser = normalizeAnswer(userAnswer);
    const normalizedCorrect = normalizeAnswer(correctAnswer);

    // Check if answers match
    const passed = normalizedUser === normalizedCorrect;

    return {
      success: true,
      passed,
      score: passed ? 100 : 0,
      message: passed
        ? 'Correct! Well done!'
        : 'Incorrect. Review the explanation and try again.',
      output: `Your answer: ${userAnswer}, Correct answer: ${correctAnswer}`
    };
  } catch (error: any) {
    return {
      success: false,
      passed: false,
      score: 0,
      message: 'Failed to validate answer.',
      runtimeError: error.message
    };
  }
}

/**
 * Validate True/False challenge
 * Compares user's boolean answer with correct answer
 *
 * @param userAnswer - User's answer (true/false)
 * @param correctAnswer - Correct answer (true/false)
 * @returns ValidationResponse with pass/fail result
 */
export function validateTrueFalse(
  userAnswer: boolean,
  correctAnswer: boolean
): ValidationResponse {
  try {
    const passed = userAnswer === correctAnswer;

    return {
      success: true,
      passed,
      score: passed ? 100 : 0,
      message: passed
        ? 'Correct! Well done!'
        : 'Incorrect. Review the explanation and try again.',
      output: `Your answer: ${userAnswer}, Correct answer: ${correctAnswer}`
    };
  } catch (error: any) {
    return {
      success: false,
      passed: false,
      score: 0,
      message: 'Failed to validate answer.',
      runtimeError: error.message
    };
  }
}

/**
 * Validate Code Output prediction challenge
 * Compares user's predicted output with correct output
 *
 * @param userAnswer - User's predicted output
 * @param correctOutput - Actual correct output
 * @returns ValidationResponse with pass/fail result
 */
export function validateCodeOutput(
  userAnswer: string,
  correctOutput: string
): ValidationResponse {
  try {
    const { passed, errorMessage } = compareOutput(
      userAnswer,
      correctOutput,
      'exact',
      false // Case insensitive for output predictions
    );

    return {
      success: true,
      passed,
      score: passed ? 100 : 0,
      message: passed
        ? 'Correct! You predicted the output accurately!'
        : 'Not quite. Try running the code mentally line by line.',
      output: userAnswer,
      feedback: errorMessage
    };
  } catch (error: any) {
    return {
      success: false,
      passed: false,
      score: 0,
      message: 'Failed to validate answer.',
      runtimeError: error.message
    };
  }
}

// ============================================================================
// Main Validation Function
// ============================================================================

/**
 * Main validation function that routes to appropriate validator based on challenge type
 *
 * @param challenge - The challenge being validated
 * @param userSubmission - User's submission data
 * @returns ValidationResponse with results
 */
export async function validateChallenge(
  challenge: Challenge,
  userSubmission: ChallengeSubmission
): Promise<ValidationResponse> {
  try {
    switch (challenge.type) {
      case 'FREE_CODING': {
        const freeCodingChallenge = challenge as FreeCodingChallenge;

        if (typeof userSubmission.userAnswer !== 'string') {
          return {
            success: false,
            passed: false,
            score: 0,
            message: 'Invalid submission: code must be a string',
            runtimeError: 'Invalid user submission format'
          };
        }

        return await validateFreeCoding(
          userSubmission.userAnswer,
          freeCodingChallenge.language,
          freeCodingChallenge.testCases
        );
      }

      case 'CODE_COMPLETION': {
        const codeCompletionChallenge = challenge as CodeCompletionChallenge;

        if (typeof userSubmission.userAnswer !== 'string') {
          return {
            success: false,
            passed: false,
            score: 0,
            message: 'Invalid submission: code must be a string',
            runtimeError: 'Invalid user submission format'
          };
        }

        return await validateCodeCompletion(
          userSubmission.userAnswer,
          codeCompletionChallenge.language,
          codeCompletionChallenge.testCases
        );
      }

      case 'MULTIPLE_CHOICE': {
        const multipleChoiceChallenge = challenge as MultipleChoiceChallenge;

        return validateMultipleChoice(
          userSubmission.userAnswer,
          multipleChoiceChallenge.correctAnswer
        );
      }

      case 'TRUE_FALSE': {
        const trueFalseChallenge = challenge as TrueFalseChallenge;

        if (typeof userSubmission.userAnswer !== 'boolean') {
          return {
            success: false,
            passed: false,
            score: 0,
            message: 'Invalid submission: answer must be true or false',
            runtimeError: 'Invalid user submission format'
          };
        }

        return validateTrueFalse(
          userSubmission.userAnswer,
          trueFalseChallenge.correctAnswer
        );
      }

      case 'CODE_OUTPUT': {
        const codeOutputChallenge = challenge as CodeOutputChallenge;

        if (typeof userSubmission.userAnswer !== 'string') {
          return {
            success: false,
            passed: false,
            score: 0,
            message: 'Invalid submission: answer must be a string',
            runtimeError: 'Invalid user submission format'
          };
        }

        return validateCodeOutput(
          userSubmission.userAnswer,
          codeOutputChallenge.correctOutput
        );
      }

      case 'CONCEPTUAL': {
        // Conceptual challenges are not auto-validated
        return {
          success: true,
          passed: true,
          score: 100,
          message: 'Conceptual questions are not automatically graded. Review the sample answers and explanation.',
          output: userSubmission.userAnswer
        };
      }

      default:
        return {
          success: false,
          passed: false,
          score: 0,
          message: `Unknown challenge type: ${(challenge as any).type}`,
          runtimeError: 'Invalid challenge type'
        };
    }
  } catch (error: any) {
    console.error('Challenge validation error:', error);
    return {
      success: false,
      passed: false,
      score: 0,
      message: 'An unexpected error occurred during validation.',
      runtimeError: error.message || 'Unknown error'
    };
  }
}

// ============================================================================
// Exported Helper Functions
// ============================================================================

/**
 * Validate only visible test cases (for immediate feedback during development)
 *
 * @param code - User's code
 * @param language - Programming language
 * @param testCases - All test cases
 * @returns ValidationResponse with results only for visible tests
 */
export async function validateVisibleTestCases(
  code: string,
  language: string,
  testCases: TestCase[]
): Promise<ValidationResponse> {
  const visibleTests = testCases.filter(tc => tc.isVisible);
  return validateFreeCoding(code, language, visibleTests);
}

/**
 * Get helpful hints based on failed test cases
 *
 * @param testResults - Array of test results
 * @returns Array of helpful hint messages
 */
export function getHintsFromFailedTests(testResults: TestResult[]): string[] {
  const hints: string[] = [];

  testResults.forEach(result => {
    if (!result.passed && result.errorMessage) {
      hints.push(result.errorMessage);
    }
  });

  return hints;
}

/**
 * Check if code has common syntax issues before execution
 * Basic static analysis to provide quick feedback
 *
 * @param code - User's code
 * @param language - Programming language
 * @returns Object with hasIssues flag and array of issue messages
 */
export function checkCommonSyntaxIssues(
  code: string,
  language: string
): { hasIssues: boolean; issues: string[] } {
  const issues: string[] = [];
  const lang = language.toLowerCase();

  // Common issues for specific languages
  switch (lang) {
    case 'java':
      if (!code.includes('public static void main')) {
        issues.push('Missing main method. Java programs need: public static void main(String[] args)');
      }
      if (!code.match(/class\s+\w+/)) {
        issues.push('Missing class declaration. Java code must be inside a class.');
      }
      break;

    case 'python':
      // Check for common Python issues
      const lines = code.split('\n');
      lines.forEach((line, index) => {
        if (line.trim() && line.match(/^\s/) && !lines[index - 1]?.includes(':')) {
          issues.push(`Unexpected indentation on line ${index + 1}`);
        }
      });
      break;

    case 'csharp':
    case 'c#':
      if (!code.includes('static void Main')) {
        issues.push('Missing Main method. C# programs need: static void Main(string[] args)');
      }
      break;
  }

  return {
    hasIssues: issues.length > 0,
    issues
  };
}
