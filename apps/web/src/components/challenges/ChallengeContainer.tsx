import { useState } from 'react'
import { AlertCircle } from 'lucide-react'
import { clsx } from 'clsx'
import {
  Challenge,
  TestResult,
  ChallengeSubmission,
  ValidationResponse,
} from '../../types/interactive'
import { MultipleChoiceChallenge } from './MultipleChoiceChallenge'
import { TrueFalseChallenge } from './TrueFalseChallenge'
import { CodeOutputChallenge } from './CodeOutputChallenge'
import { FreeCodingChallenge } from './FreeCodingChallenge'
import { CodeCompletionChallenge } from './CodeCompletionChallenge'
import { ConceptualChallenge } from './ConceptualChallenge'

/**
 * ChallengeContainer Component
 *
 * Wrapper component that determines challenge type and renders the appropriate component.
 * Handles submission to backend API and manages challenge state.
 *
 * @example
 * ```tsx
 * <ChallengeContainer
 *   challenge={challenge}
 *   lessonId="lesson-1"
 *   onComplete={(result) => console.log("Challenge completed", result)}
 * />
 * ```
 */

interface ChallengeContainerProps {
  /** Challenge data */
  challenge: Challenge
  /** Lesson ID for tracking */
  lessonId: string
  /** Callback when challenge is completed successfully */
  onComplete?: (result: ValidationResponse) => void
  /** Callback when submission fails */
  onError?: (error: Error) => void
  /** Custom API endpoint for challenge submission (default: /api/challenges/submit) */
  apiEndpoint?: string
  /** Custom className for styling */
  className?: string
}

export function ChallengeContainer({
  challenge,
  lessonId,
  onComplete,
  onError,
  apiEndpoint = '/api/challenges/submit',
  className,
}: ChallengeContainerProps) {
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)

  /**
   * Submit challenge answer to backend API
   */
  const submitChallenge = async (submission: ChallengeSubmission): Promise<ValidationResponse> => {
    setIsSubmitting(true)
    setError(null)

    try {
      const response = await fetch(apiEndpoint, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(submission),
      })

      if (!response.ok) {
        throw new Error(`Submission failed: ${response.statusText}`)
      }

      const result: ValidationResponse = await response.json()

      if (result.passed) {
        onComplete?.(result)
      }

      return result
    } catch (err) {
      const error = err instanceof Error ? err : new Error('Unknown error occurred')
      setError(error.message)
      onError?.(error)
      throw error
    } finally {
      setIsSubmitting(false)
    }
  }

  /**
   * Handle Multiple Choice submission
   */
  const handleMultipleChoiceSubmit = async (isCorrect: boolean, hintsUsed: number) => {
    if (challenge.type !== 'MULTIPLE_CHOICE') return

    const submission: ChallengeSubmission = {
      challengeId: challenge.id,
      lessonId,
      userAnswer: isCorrect,
      hintsUsed,
    }

    try {
      await submitChallenge(submission)
    } catch (err) {
      console.error('Submission failed:', err)
    }
  }

  /**
   * Handle True/False submission
   */
  const handleTrueFalseSubmit = async (isCorrect: boolean, hintsUsed: number) => {
    if (challenge.type !== 'TRUE_FALSE') return

    const submission: ChallengeSubmission = {
      challengeId: challenge.id,
      lessonId,
      userAnswer: isCorrect,
      hintsUsed,
    }

    try {
      await submitChallenge(submission)
    } catch (err) {
      console.error('Submission failed:', err)
    }
  }

  /**
   * Handle Code Output submission
   */
  const handleCodeOutputSubmit = async (isCorrect: boolean, hintsUsed: number) => {
    if (challenge.type !== 'CODE_OUTPUT') return

    const submission: ChallengeSubmission = {
      challengeId: challenge.id,
      lessonId,
      userAnswer: isCorrect,
      hintsUsed,
    }

    try {
      await submitChallenge(submission)
    } catch (err) {
      console.error('Submission failed:', err)
    }
  }

  /**
   * Handle Free Coding test run
   */
  const handleFreeCodingTestRun = async (results: TestResult[], code: string) => {
    if (challenge.type !== 'FREE_CODING') return

    // Test run doesn't submit to backend, just logs locally
    console.log('Test run completed:', { results, code })
  }

  /**
   * Handle Free Coding completion (all tests passed)
   */
  const handleFreeCodingComplete = async (
    code: string,
    hintsUsed: number,
    _testResults: TestResult[]
  ) => {
    if (challenge.type !== 'FREE_CODING') return

    const submission: ChallengeSubmission = {
      challengeId: challenge.id,
      lessonId,
      userAnswer: code,
      hintsUsed,
    }

    try {
      await submitChallenge(submission)
    } catch (err) {
      console.error('Submission failed:', err)
    }
  }

  /**
   * Handle Code Completion test run
   */
  const handleCodeCompletionTestRun = async (results: TestResult[], code: string) => {
    if (challenge.type !== 'CODE_COMPLETION') return

    // Test run doesn't submit to backend, just logs locally
    console.log('Test run completed:', { results, code })
  }

  /**
   * Handle Code Completion completion (all tests passed)
   */
  const handleCodeCompletionComplete = async (
    code: string,
    hintsUsed: number,
    _testResults: TestResult[]
  ) => {
    if (challenge.type !== 'CODE_COMPLETION') return

    const submission: ChallengeSubmission = {
      challengeId: challenge.id,
      lessonId,
      userAnswer: code,
      hintsUsed,
    }

    try {
      await submitChallenge(submission)
    } catch (err) {
      console.error('Submission failed:', err)
    }
  }

  /**
   * Handle Conceptual challenge completion
   */
  const handleConceptualComplete = async (passed: boolean) => {
    if (challenge.type !== 'CONCEPTUAL') return

    const submission: ChallengeSubmission = {
      challengeId: challenge.id,
      lessonId,
      userAnswer: passed,
      hintsUsed: 0,
    }

    try {
      await submitChallenge(submission)
    } catch (err) {
      console.error('Submission failed:', err)
    }
  }

  /**
   * Render the appropriate challenge component based on type
   */
  const renderChallenge = () => {
    switch (challenge.type) {
      case 'MULTIPLE_CHOICE':
        return (
          <MultipleChoiceChallenge
            challenge={challenge}
            onSubmit={handleMultipleChoiceSubmit}
          />
        )

      case 'TRUE_FALSE':
        return (
          <TrueFalseChallenge
            challenge={challenge}
            onSubmit={handleTrueFalseSubmit}
          />
        )

      case 'CODE_OUTPUT':
        return (
          <CodeOutputChallenge
            challenge={challenge}
            onSubmit={handleCodeOutputSubmit}
          />
        )

      case 'FREE_CODING':
        return (
          <FreeCodingChallenge
            challenge={challenge}
            onTestRun={handleFreeCodingTestRun}
            onComplete={handleFreeCodingComplete}
          />
        )

      case 'CODE_COMPLETION':
        return (
          <CodeCompletionChallenge
            challenge={challenge}
            onTestRun={handleCodeCompletionTestRun}
            onComplete={handleCodeCompletionComplete}
          />
        )

      case 'CONCEPTUAL':
        return (
          <ConceptualChallenge
            challenge={challenge}
            onComplete={handleConceptualComplete}
          />
        )

      default:
        return (
          <div className="p-8 text-center border rounded-lg bg-red-50 dark:bg-red-900/20">
            <AlertCircle className="w-12 h-12 text-red-600 dark:text-red-400 mx-auto mb-3" />
            <p className="text-red-600 dark:text-red-400 font-semibold">
              Unknown challenge type: {(challenge as any).type}
            </p>
          </div>
        )
    }
  }

  return (
    <div className={clsx('relative', className)}>
      {/* Loading overlay */}
      {isSubmitting && (
        <div className="absolute inset-0 bg-white/80 dark:bg-gray-900/80 backdrop-blur-sm z-50 flex items-center justify-center rounded-lg">
          <div className="flex items-center gap-3 bg-white dark:bg-gray-800 px-6 py-4 rounded-lg shadow-lg border border-gray-200 dark:border-gray-700">
            <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-primary" />
            <span className="text-gray-900 dark:text-gray-100 font-medium">
              Submitting...
            </span>
          </div>
        </div>
      )}

      {/* Error message */}
      {error && (
        <div
          className="mb-6 p-4 bg-red-50 dark:bg-red-900/20 border border-red-300 dark:border-red-800 rounded-lg animate-in fade-in slide-in-from-top-2 duration-300"
          role="alert"
        >
          <div className="flex items-start gap-3">
            <AlertCircle className="w-5 h-5 text-red-600 dark:text-red-400 flex-shrink-0 mt-0.5" />
            <div className="flex-1">
              <p className="font-semibold text-red-900 dark:text-red-200 mb-1">
                Submission Error
              </p>
              <p className="text-sm text-red-800 dark:text-red-300">{error}</p>
            </div>
            <button
              onClick={() => setError(null)}
              className="text-red-600 dark:text-red-400 hover:text-red-800 dark:hover:text-red-200"
              aria-label="Dismiss error"
            >
              ✕
            </button>
          </div>
        </div>
      )}

      {/* Challenge component */}
      {renderChallenge()}
    </div>
  )
}
