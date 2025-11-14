import { useState } from 'react'
import { CheckCircle2, XCircle, ThumbsUp, ThumbsDown } from 'lucide-react'
import { clsx } from 'clsx'
import ReactMarkdown from 'react-markdown'
import { TrueFalseChallenge as TrueFalseChallengeType } from '../../types/interactive'
import { Button } from '../Button'
import { HintsPanel } from './HintsPanel'

/**
 * TrueFalseChallenge Component
 *
 * Displays a true/false question with two button options.
 * Shows visual feedback (correct/incorrect) and explanation after submission.
 *
 * @example
 * ```tsx
 * <TrueFalseChallenge
 *   challenge={{
 *     id: "tf-1",
 *     type: "TRUE_FALSE",
 *     title: "True or False",
 *     question: "JavaScript is a compiled language",
 *     correctAnswer: false,
 *     explanation: "JavaScript is an interpreted language"
 *   }}
 *   onSubmit={(isCorrect, hintsUsed) => console.log("Submitted")}
 * />
 * ```
 */

interface TrueFalseChallengeProps {
  /** Challenge data */
  challenge: TrueFalseChallengeType
  /** Callback when challenge is submitted */
  onSubmit?: (isCorrect: boolean, hintsUsed: number) => void
  /** Custom className for styling */
  className?: string
}

export function TrueFalseChallenge({ challenge, onSubmit, className }: TrueFalseChallengeProps) {
  const [selectedAnswer, setSelectedAnswer] = useState<boolean | null>(null)
  const [isSubmitted, setIsSubmitted] = useState(false)
  const [hintsUsed, setHintsUsed] = useState(0)

  const isCorrect = selectedAnswer === challenge.correctAnswer
  const hasHints = challenge.hints && challenge.hints.length > 0

  const handleSubmit = () => {
    if (selectedAnswer === null) return

    setIsSubmitted(true)
    onSubmit?.(isCorrect, hintsUsed)
  }

  const handleReset = () => {
    setSelectedAnswer(null)
    setIsSubmitted(false)
    setHintsUsed(0)
  }

  return (
    <div className={clsx('space-y-6', className)}>
      {/* Challenge header */}
      <div>
        <h2 className="text-2xl font-bold text-gray-900 dark:text-gray-100 mb-2">
          {challenge.title}
        </h2>
        {challenge.description && (
          <div className="prose prose-sm dark:prose-invert max-w-none text-gray-600 dark:text-gray-400 mb-4">
            <ReactMarkdown>{challenge.description}</ReactMarkdown>
          </div>
        )}
      </div>

      {/* Question */}
      <div className="p-6 bg-gray-50 dark:bg-gray-800 rounded-lg border border-gray-200 dark:border-gray-700">
        <div className="prose dark:prose-invert max-w-none">
          <ReactMarkdown>{challenge.question}</ReactMarkdown>
        </div>
      </div>

      {/* True/False buttons */}
      {!isSubmitted ? (
        <div className="grid grid-cols-2 gap-4" role="radiogroup" aria-label="True or False options">
          {/* True button */}
          <button
            onClick={() => setSelectedAnswer(true)}
            className={clsx(
              'flex flex-col items-center justify-center gap-3 p-6 rounded-lg border-2 transition-all',
              'focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2',
              {
                'border-gray-300 dark:border-gray-700 hover:border-green-500 hover:bg-green-50 dark:hover:bg-green-900/20':
                  selectedAnswer !== true,
                'border-green-500 bg-green-50 dark:bg-green-900/20 shadow-lg': selectedAnswer === true,
              }
            )}
            role="radio"
            aria-checked={selectedAnswer === true}
            aria-label="True"
          >
            <ThumbsUp
              className={clsx('w-12 h-12 transition-colors', {
                'text-gray-400': selectedAnswer !== true,
                'text-green-600 dark:text-green-400': selectedAnswer === true,
              })}
              aria-hidden="true"
            />
            <span
              className={clsx('text-2xl font-bold transition-colors', {
                'text-gray-600 dark:text-gray-400': selectedAnswer !== true,
                'text-green-600 dark:text-green-400': selectedAnswer === true,
              })}
            >
              TRUE
            </span>
          </button>

          {/* False button */}
          <button
            onClick={() => setSelectedAnswer(false)}
            className={clsx(
              'flex flex-col items-center justify-center gap-3 p-6 rounded-lg border-2 transition-all',
              'focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2',
              {
                'border-gray-300 dark:border-gray-700 hover:border-red-500 hover:bg-red-50 dark:hover:bg-red-900/20':
                  selectedAnswer !== false,
                'border-red-500 bg-red-50 dark:bg-red-900/20 shadow-lg': selectedAnswer === false,
              }
            )}
            role="radio"
            aria-checked={selectedAnswer === false}
            aria-label="False"
          >
            <ThumbsDown
              className={clsx('w-12 h-12 transition-colors', {
                'text-gray-400': selectedAnswer !== false,
                'text-red-600 dark:text-red-400': selectedAnswer === false,
              })}
              aria-hidden="true"
            />
            <span
              className={clsx('text-2xl font-bold transition-colors', {
                'text-gray-600 dark:text-gray-400': selectedAnswer !== false,
                'text-red-600 dark:text-red-400': selectedAnswer === false,
              })}
            >
              FALSE
            </span>
          </button>
        </div>
      ) : (
        /* Result display after submission */
        <div className="grid grid-cols-2 gap-4">
          {/* Show which was correct */}
          <div
            className={clsx('flex flex-col items-center justify-center gap-3 p-6 rounded-lg border-2', {
              'border-green-500 bg-green-50 dark:bg-green-900/20': challenge.correctAnswer === true,
              'border-gray-300 dark:border-gray-700 opacity-50': challenge.correctAnswer !== true,
            })}
          >
            <ThumbsUp
              className={clsx('w-12 h-12', {
                'text-green-600 dark:text-green-400': challenge.correctAnswer === true,
                'text-gray-400': challenge.correctAnswer !== true,
              })}
              aria-hidden="true"
            />
            <span
              className={clsx('text-2xl font-bold', {
                'text-green-600 dark:text-green-400': challenge.correctAnswer === true,
                'text-gray-600 dark:text-gray-400': challenge.correctAnswer !== true,
              })}
            >
              TRUE
            </span>
            {challenge.correctAnswer === true && (
              <CheckCircle2 className="w-6 h-6 text-green-600 dark:text-green-400" aria-label="Correct answer" />
            )}
          </div>

          <div
            className={clsx('flex flex-col items-center justify-center gap-3 p-6 rounded-lg border-2', {
              'border-green-500 bg-green-50 dark:bg-green-900/20': challenge.correctAnswer === false,
              'border-gray-300 dark:border-gray-700 opacity-50': challenge.correctAnswer !== false,
            })}
          >
            <ThumbsDown
              className={clsx('w-12 h-12', {
                'text-green-600 dark:text-green-400': challenge.correctAnswer === false,
                'text-gray-400': challenge.correctAnswer !== false,
              })}
              aria-hidden="true"
            />
            <span
              className={clsx('text-2xl font-bold', {
                'text-green-600 dark:text-green-400': challenge.correctAnswer === false,
                'text-gray-600 dark:text-gray-400': challenge.correctAnswer !== false,
              })}
            >
              FALSE
            </span>
            {challenge.correctAnswer === false && (
              <CheckCircle2 className="w-6 h-6 text-green-600 dark:text-green-400" aria-label="Correct answer" />
            )}
          </div>
        </div>
      )}

      {/* Hints panel */}
      {hasHints && !isSubmitted && (
        <HintsPanel hints={challenge.hints!} onHintUsed={setHintsUsed} />
      )}

      {/* Action buttons */}
      {!isSubmitted ? (
        <Button
          onClick={handleSubmit}
          disabled={selectedAnswer === null}
          variant="primary"
          size="lg"
          className="w-full"
        >
          Submit Answer
        </Button>
      ) : (
        <div className="space-y-4">
          {/* Result feedback */}
          <div
            className={clsx('p-4 rounded-lg border-2 animate-in fade-in slide-in-from-top-2 duration-300', {
              'bg-green-50 dark:bg-green-900/20 border-green-500': isCorrect,
              'bg-red-50 dark:bg-red-900/20 border-red-500': !isCorrect,
            })}
            role="alert"
            aria-live="polite"
          >
            <div className="flex items-start gap-3">
              {isCorrect ? (
                <CheckCircle2 className="w-6 h-6 text-green-600 dark:text-green-400 flex-shrink-0" />
              ) : (
                <XCircle className="w-6 h-6 text-red-600 dark:text-red-400 flex-shrink-0" />
              )}
              <div className="flex-1">
                <p className="font-semibold text-lg mb-2">
                  {isCorrect ? '🎉 Correct!' : '❌ Incorrect'}
                </p>
                <div className="prose prose-sm dark:prose-invert max-w-none">
                  <ReactMarkdown>{challenge.explanation}</ReactMarkdown>
                </div>
              </div>
            </div>
          </div>

          {/* Try again button */}
          <Button onClick={handleReset} variant="outline" size="lg" className="w-full">
            Try Another Question
          </Button>
        </div>
      )}

      {/* Challenge metadata */}
      {challenge.estimatedMinutes && (
        <div className="text-sm text-gray-500 dark:text-gray-400">
          Estimated time: {challenge.estimatedMinutes} {challenge.estimatedMinutes === 1 ? 'minute' : 'minutes'}
        </div>
      )}
    </div>
  )
}
