/**
 * Challenge Components Utility Functions
 *
 * Helper functions and type guards for working with challenge components
 */

import {
  Challenge,
  MultipleChoiceChallenge,
  TrueFalseChallenge,
  CodeOutputChallenge,
  FreeCodingChallenge,
  CodeCompletionChallenge,
  ConceptualChallenge,
} from '../../types/interactive'

// ============================================================================
// Type Guards
// ============================================================================

/**
 * Check if challenge is a Multiple Choice challenge
 */
export function isMultipleChoiceChallenge(
  challenge: Challenge
): challenge is MultipleChoiceChallenge {
  return challenge.type === 'MULTIPLE_CHOICE'
}

/**
 * Check if challenge is a True/False challenge
 */
export function isTrueFalseChallenge(challenge: Challenge): challenge is TrueFalseChallenge {
  return challenge.type === 'TRUE_FALSE'
}

/**
 * Check if challenge is a Code Output challenge
 */
export function isCodeOutputChallenge(challenge: Challenge): challenge is CodeOutputChallenge {
  return challenge.type === 'CODE_OUTPUT'
}

/**
 * Check if challenge is a Free Coding challenge
 */
export function isFreeCodingChallenge(challenge: Challenge): challenge is FreeCodingChallenge {
  return challenge.type === 'FREE_CODING'
}

/**
 * Check if challenge is a Code Completion challenge
 */
export function isCodeCompletionChallenge(
  challenge: Challenge
): challenge is CodeCompletionChallenge {
  return challenge.type === 'CODE_COMPLETION'
}

/**
 * Check if challenge is a Conceptual challenge
 */
export function isConceptualChallenge(challenge: Challenge): challenge is ConceptualChallenge {
  return challenge.type === 'CONCEPTUAL'
}

/**
 * Check if challenge requires code editor
 */
export function requiresCodeEditor(challenge: Challenge): boolean {
  return (
    challenge.type === 'FREE_CODING' ||
    challenge.type === 'CODE_COMPLETION' ||
    challenge.type === 'CODE_OUTPUT'
  )
}

/**
 * Check if challenge has automated testing
 */
export function hasAutomatedTesting(challenge: Challenge): boolean {
  return challenge.type === 'FREE_CODING' || challenge.type === 'CODE_COMPLETION'
}

// ============================================================================
// Difficulty Helpers
// ============================================================================

/**
 * Normalize difficulty to numeric value
 */
export function normalizeDifficulty(
  difficulty?: 'beginner' | 'intermediate' | 'advanced' | 1 | 2 | 3 | 4 | 5
): number {
  if (!difficulty) return 3 // default to medium

  if (typeof difficulty === 'number') {
    return difficulty
  }

  const mapping = {
    beginner: 1,
    intermediate: 3,
    advanced: 5,
  }

  return mapping[difficulty]
}

/**
 * Get difficulty label
 */
export function getDifficultyLabel(
  difficulty?: 'beginner' | 'intermediate' | 'advanced' | 1 | 2 | 3 | 4 | 5
): string {
  if (!difficulty) return 'Medium'

  if (typeof difficulty === 'string') {
    return difficulty.charAt(0).toUpperCase() + difficulty.slice(1)
  }

  if (difficulty <= 2) return 'Easy'
  if (difficulty <= 3) return 'Medium'
  if (difficulty <= 4) return 'Hard'
  return 'Expert'
}

/**
 * Get difficulty color
 */
export function getDifficultyColor(
  difficulty?: 'beginner' | 'intermediate' | 'advanced' | 1 | 2 | 3 | 4 | 5
): string {
  const numeric = normalizeDifficulty(difficulty)

  if (numeric <= 2) return 'text-green-600 dark:text-green-400'
  if (numeric <= 3) return 'text-yellow-600 dark:text-yellow-400'
  if (numeric <= 4) return 'text-orange-600 dark:text-orange-400'
  return 'text-red-600 dark:text-red-400'
}

// ============================================================================
// Challenge Metadata
// ============================================================================

/**
 * Get challenge type display name
 */
export function getChallengeTypeLabel(challenge: Challenge): string {
  const labels = {
    MULTIPLE_CHOICE: 'Multiple Choice',
    TRUE_FALSE: 'True/False',
    CODE_OUTPUT: 'Code Output',
    FREE_CODING: 'Free Coding',
    CODE_COMPLETION: 'Code Completion',
    CONCEPTUAL: 'Conceptual',
  }

  return labels[challenge.type] || challenge.type
}

/**
 * Get challenge type icon
 */
export function getChallengeTypeIcon(challenge: Challenge): string {
  const icons = {
    MULTIPLE_CHOICE: '📝',
    TRUE_FALSE: '✅',
    CODE_OUTPUT: '💻',
    FREE_CODING: '🚀',
    CODE_COMPLETION: '✏️',
    CONCEPTUAL: '🤔',
  }

  return icons[challenge.type] || '❓'
}

/**
 * Get estimated time in human-readable format
 */
export function getEstimatedTimeLabel(minutes?: number): string {
  if (!minutes) return 'Unknown'

  if (minutes < 60) {
    return `${minutes} ${minutes === 1 ? 'minute' : 'minutes'}`
  }

  const hours = Math.floor(minutes / 60)
  const remainingMinutes = minutes % 60

  if (remainingMinutes === 0) {
    return `${hours} ${hours === 1 ? 'hour' : 'hours'}`
  }

  return `${hours}h ${remainingMinutes}m`
}

// ============================================================================
// Validation Helpers
// ============================================================================

/**
 * Validate challenge structure
 */
export function validateChallenge(challenge: Challenge): {
  valid: boolean
  errors: string[]
} {
  const errors: string[] = []

  // Basic validation
  if (!challenge.id) errors.push('Challenge must have an id')
  if (!challenge.title) errors.push('Challenge must have a title')
  if (!challenge.type) errors.push('Challenge must have a type')

  // Type-specific validation
  if (isMultipleChoiceChallenge(challenge)) {
    if (!challenge.options || challenge.options.length < 2) {
      errors.push('Multiple choice must have at least 2 options')
    }
    if (challenge.correctAnswer === undefined) {
      errors.push('Multiple choice must have a correctAnswer')
    }
    if (!challenge.explanation) {
      errors.push('Multiple choice should have an explanation')
    }
  }

  if (isTrueFalseChallenge(challenge)) {
    if (typeof challenge.correctAnswer !== 'boolean') {
      errors.push('True/False must have a boolean correctAnswer')
    }
    if (!challenge.question) {
      errors.push('True/False must have a question')
    }
  }

  if (isCodeOutputChallenge(challenge)) {
    if (!challenge.code) errors.push('Code output must have code')
    if (!challenge.language) errors.push('Code output must have a language')
    if (!challenge.correctOutput) errors.push('Code output must have correctOutput')
  }

  if (isFreeCodingChallenge(challenge)) {
    if (!challenge.instructions) errors.push('Free coding must have instructions')
    if (!challenge.starterCode) errors.push('Free coding must have starterCode')
    if (!challenge.solution) errors.push('Free coding must have a solution')
    if (!challenge.language) errors.push('Free coding must have a language')
    if (!challenge.testCases || challenge.testCases.length === 0) {
      errors.push('Free coding must have at least one test case')
    }
  }

  if (isCodeCompletionChallenge(challenge)) {
    if (!challenge.starterCode) errors.push('Code completion must have starterCode')
    if (!challenge.solution) errors.push('Code completion must have a solution')
    if (!challenge.language) errors.push('Code completion must have a language')
    if (!challenge.testCases || challenge.testCases.length === 0) {
      errors.push('Code completion must have at least one test case')
    }
  }

  return {
    valid: errors.length === 0,
    errors,
  }
}

// ============================================================================
// Score Calculation
// ============================================================================

/**
 * Calculate challenge score based on hints used and attempts
 */
export function calculateChallengeScore(options: {
  hintsUsed: number
  attempts: number
  totalHints: number
  passed: boolean
}): number {
  const { hintsUsed, attempts, totalHints, passed } = options

  if (!passed) return 0

  let score = 100

  // Deduct points for hints (up to 30 points)
  if (totalHints > 0) {
    const hintPenalty = (hintsUsed / totalHints) * 30
    score -= hintPenalty
  }

  // Deduct points for multiple attempts (up to 20 points)
  if (attempts > 1) {
    const attemptPenalty = Math.min((attempts - 1) * 5, 20)
    score -= attemptPenalty
  }

  return Math.max(0, Math.round(score))
}

// ============================================================================
// Test Result Helpers
// ============================================================================

/**
 * Calculate test pass rate
 */
export function calculatePassRate(results: { passed: boolean }[]): number {
  if (results.length === 0) return 0

  const passed = results.filter((r) => r.passed).length
  return Math.round((passed / results.length) * 100)
}

/**
 * Group tests by visibility
 */
export function groupTestsByVisibility<T extends { testCase: { isVisible: boolean } }>(
  results: T[]
): { visible: T[]; hidden: T[] } {
  return {
    visible: results.filter((r) => r.testCase.isVisible),
    hidden: results.filter((r) => !r.testCase.isVisible),
  }
}

// ============================================================================
// Code Analysis
// ============================================================================

/**
 * Count lines of code
 */
export function countLinesOfCode(code: string): number {
  return code.split('\n').filter((line) => line.trim().length > 0).length
}

/**
 * Check if code contains TODO markers
 */
export function hasTodoMarkers(code: string): boolean {
  return /TODO|FIXME|XXX/i.test(code)
}

/**
 * Estimate code complexity (simple heuristic)
 */
export function estimateComplexity(code: string): 'low' | 'medium' | 'high' {
  const lines = countLinesOfCode(code)
  const hasLoops = /for|while|forEach/.test(code)
  const hasConditions = /if|switch|case/.test(code)
  const hasFunctions = /function|=>/.test(code)

  let complexity = lines

  if (hasLoops) complexity += 10
  if (hasConditions) complexity += 5
  if (hasFunctions) complexity += 5

  if (complexity < 20) return 'low'
  if (complexity < 50) return 'medium'
  return 'high'
}
