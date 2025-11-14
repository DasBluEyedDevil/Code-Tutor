/**
 * Type definitions for Code Tutor Desktop App
 * Shared types for challenge validation and interactive content
 */

// ============================================================================
// Content Section Types
// ============================================================================

export type ContentSectionType =
  | 'THEORY'
  | 'ANALOGY'
  | 'EXAMPLE'
  | 'KEY_POINT'
  | 'WARNING'
  | 'EXPERIMENT';

export interface ContentSection {
  type: ContentSectionType;
  title: string;
  content: string;
  code?: string;
  language?: string;
}

// ============================================================================
// Challenge Types
// ============================================================================

export interface BaseChallenge {
  id: string;
  title: string;
  description: string;
  difficulty?: 'beginner' | 'intermediate' | 'advanced' | 1 | 2 | 3 | 4 | 5;
  hints?: Hint[];
  estimatedMinutes?: number;
}

export interface MultipleChoiceChallenge extends BaseChallenge {
  type: 'MULTIPLE_CHOICE';
  options: string[];
  correctAnswer: number | string;
  explanation: string;
}

export interface TrueFalseChallenge extends BaseChallenge {
  type: 'TRUE_FALSE';
  question: string;
  correctAnswer: boolean;
  explanation: string;
}

export interface CodeOutputChallenge extends BaseChallenge {
  type: 'CODE_OUTPUT';
  code: string;
  language: string;
  correctOutput: string;
  explanation: string;
}

export interface FreeCodingChallenge extends BaseChallenge {
  type: 'FREE_CODING';
  instructions: string;
  starterCode: string;
  solution: string;
  language: string;
  testCases: TestCase[];
  commonMistakes?: CommonMistake[];
  bonusChallenges?: BonusChallenge[];
}

export interface CodeCompletionChallenge extends BaseChallenge {
  type: 'CODE_COMPLETION';
  starterCode: string;
  solution: string;
  language: string;
  testCases: TestCase[];
  commonMistakes?: CommonMistake[];
}

export interface ConceptualChallenge extends BaseChallenge {
  type: 'CONCEPTUAL';
  question: string;
  sampleAnswers?: string[];
  explanation: string;
}

export type Challenge =
  | MultipleChoiceChallenge
  | TrueFalseChallenge
  | CodeOutputChallenge
  | FreeCodingChallenge
  | CodeCompletionChallenge
  | ConceptualChallenge;

// ============================================================================
// Test Cases
// ============================================================================

export type TestType = 'exact' | 'contains' | 'regex' | 'pattern';

export interface TestCase {
  id?: string;
  description: string;
  inputs?: any[];
  expectedOutput: any;
  isVisible: boolean;
  testType?: TestType;
  customMessage?: string;
}

export interface TestResult {
  testCase: TestCase;
  passed: boolean;
  actualOutput?: any;
  expectedOutput: any;
  errorMessage?: string;
}

// ============================================================================
// Validation
// ============================================================================

export type ValidationRuleType =
  | 'output_exact'
  | 'output_contains'
  | 'output_regex'
  | 'code_structure'
  | 'custom';

export interface ValidationRule {
  type: ValidationRuleType;
  value: string | RegExp;
  caseSensitive?: boolean;
  message?: string;
}

export interface ValidationResponse {
  success: boolean;
  passed: boolean;
  score?: number;
  message: string;
  testResults?: TestResult[];
  compilationError?: string;
  runtimeError?: string;
  output?: string;
  feedback?: string;
}

// ============================================================================
// Hints
// ============================================================================

export interface Hint {
  level: number;
  text: string;
  code?: string;
}

// ============================================================================
// Common Mistakes
// ============================================================================

export interface CommonMistake {
  mistake: string;
  consequence: string;
  correction: string;
  example?: {
    wrong: string;
    right: string;
  };
}

// ============================================================================
// Experiments
// ============================================================================

export interface Experiment {
  id: string;
  title: string;
  description: string;
  code: string;
  language: string;
  expectedBehavior: string;
  takeaway: string;
  isIntentionallyWrong?: boolean;
}

// ============================================================================
// Bonus Challenges
// ============================================================================

export interface BonusChallenge {
  id: string;
  title: string;
  description: string;
  difficulty: 1 | 2 | 3 | 4 | 5;
  hints?: string[];
  solution?: string;
}

// ============================================================================
// Challenge Submission
// ============================================================================

export interface ChallengeSubmission {
  challengeId: string;
  lessonId: string;
  userAnswer: any;
  hintsUsed: number;
}

// ============================================================================
// Code Execution
// ============================================================================

export interface CodeExecutionRequest {
  challengeId: string;
  lessonId: string;
  code: string;
  language: string;
  testCases: TestCase[];
}
