/**
 * Interactive Content Type Definitions
 * Based on unified schema supporting all 7 programming language courses
 */

// ============================================================================
// Content Section Types
// ============================================================================

export type ContentSectionType =
  | 'THEORY'      // Main explanation of concepts
  | 'ANALOGY'     // Real-world analogies
  | 'EXAMPLE'     // Code examples with explanations
  | 'KEY_POINT'   // Important takeaways
  | 'WARNING'     // Common pitfalls
  | 'EXPERIMENT'; // Guided experimentation

export interface ContentSection {
  type: ContentSectionType;
  title: string;
  content: string;  // Markdown or HTML
  code?: string;    // Optional code snippet
  language?: string; // Programming language for code
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
  correctAnswer: number | string; // Index (0-based) or letter ('A', 'B', etc.)
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
  level: number; // 1 = gentle nudge, 5 = almost solution
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
// Lesson
// ============================================================================

export interface Resource {
  title: string;
  url: string;
  type: 'documentation' | 'video' | 'article' | 'tutorial';
}

export interface InteractiveLesson {
  id: string;
  title: string;
  moduleId: string;
  order: number;
  estimatedMinutes: number;
  difficulty: 'beginner' | 'intermediate' | 'advanced';

  // Content sections
  contentSections?: ContentSection[];

  // Interactive elements
  challenges?: Challenge[];
  experiments?: Experiment[];
  commonMistakes?: CommonMistake[];

  // Metadata
  learningObjectives?: string[];
  prerequisites?: string[];
  nextSteps?: string[];
  resources?: Resource[];
}

// ============================================================================
// Quiz
// ============================================================================

export type QuizQuestion =
  | MultipleChoiceChallenge
  | TrueFalseChallenge
  | CodeOutputChallenge;

export interface Quiz {
  id: string;
  title: string;
  description: string;
  moduleId: string;
  passingScore: number; // Percentage (e.g., 70)
  estimatedMinutes: number;
  questions: QuizQuestion[];
  completionMessage?: string;
  retryMessage?: string;
}

// ============================================================================
// Progress Tracking
// ============================================================================

export interface LessonProgress {
  lessonId: string;
  completed: boolean;
  completedAt?: string;
  attempts: number;
  challengesCompleted: number;
  challengesTotal: number;
  score?: number;
  hintsUsed?: number;
  timeSpentMinutes?: number;
}

export interface ChallengeProgress {
  challengeId: string;
  lessonId: string;
  completed: boolean;
  attempts: number;
  passed: boolean;
  score?: number;
  hintsUsed: number;
  lastAttemptAt?: string;
}

export interface QuizAttempt {
  date: string;
  score: number;
  total: number;
  percentage: number;
  answers: {
    questionId: string;
    userAnswer: any;
    correct: boolean;
  }[];
}

export interface QuizProgress {
  quizId: string;
  completed: boolean;
  attempts: number;
  bestScore: number;
  passed: boolean;
  lastAttempt?: QuizAttempt;
}

// ============================================================================
// Code Execution Request
// ============================================================================

export interface CodeExecutionRequest {
  challengeId: string;
  lessonId: string;
  code: string;
  language: string;
  testCases: TestCase[];
}

export interface ChallengeSubmission {
  challengeId: string;
  lessonId: string;
  userAnswer: any; // Different based on challenge type
  hintsUsed: number;
}
