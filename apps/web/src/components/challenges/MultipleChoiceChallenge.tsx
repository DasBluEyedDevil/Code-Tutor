import { useState } from 'react'
import { CheckCircle2, XCircle } from 'lucide-react'
import { clsx } from 'clsx'
import ReactMarkdown from 'react-markdown'
import { MultipleChoiceChallenge as MultipleChoiceChallengeType } from '../../types/interactive'
import { Button } from '../Button'
import { HintsPanel } from './HintsPanel'

/**
 * MultipleChoiceChallenge Component
 *
 * Displays a multiple choice question with radio button options.
 * Shows visual feedback (correct/incorrect) and explanation after submission.
 *
 * @example
 * ```tsx
 * <MultipleChoiceChallenge
 *   challenge={{
 *     id: "mc-1",
 *     type: "MULTIPLE_CHOICE",
 *     title: "What is 2 + 2?",
 *     description: "Select the correct answer",
 *     options: ["3", "4", "5", "6"],
 *     correctAnswer: 1,
 *     explanation: "2 + 2 equals 4"
 *   }}
 *   onSubmit={(isCorrect, hintsUsed) => console.log("Submitted")}
 * />
 * ```
 */

interface MultipleChoiceChallengeProps {
  /** Challenge data */
  challenge: MultipleChoiceChallengeType
  /** Callback when challenge is submitted */
  onSubmit?: (isCorrect: boolean, hintsUsed: number) => void
  /** Custom className for styling */
  className?: string
}

export function MultipleChoiceChallenge({
  challenge,
  onSubmit,
  className,
}: MultipleChoiceChallengeProps) {
  const [selectedOption, setSelectedOption] = useState<number | null>(null)
  const [isSubmitted, setIsSubmitted] = useState(false)
  const [hintsUsed, setHintsUsed] = useState(0)

  // Normalize correctAnswer to index (0-based)
  const correctAnswerIndex =
    typeof challenge.correctAnswer === 'number'
      ? challenge.correctAnswer
      : challenge.correctAnswer.charCodeAt(0) - 'A'.charCodeAt(0)

  const isCorrect = selectedOption === correctAnswerIndex
  const hasHints = challenge.hints && challenge.hints.length > 0

  const handleSubmit = () => {
    if (selectedOption === null) return

    setIsSubmitted(true)
    onSubmit?.(isCorrect, hintsUsed)
  }

  const handleReset = () => {
    setSelectedOption(null)
    setIsSubmitted(false)
    setHintsUsed(0)
  }

  const getOptionLetter = (index: number) => String.fromCharCode(65 + index) // A, B, C, D...

  return (
    <div className={clsx('space-y-6', className)}>
      {/* Challenge header */}
      <div>
        <h2 className="text-2xl font-bold text-gray-900 dark:text-gray-100 mb-2">
          {challenge.title}
        </h2>
        {challenge.description && (
          <div className="prose prose-sm dark:prose-invert max-w-none text-gray-600 dark:text-gray-400">
            <ReactMarkdown>{challenge.description}</ReactMarkdown>
          </div>
        )}
      </div>

      {/* Options */}
      <div
        role="radiogroup"
        aria-label="Multiple choice options"
        className="space-y-3"
      >
        {challenge.options.map((option, index) => {
          const isSelected = selectedOption === index
          const showFeedback = isSubmitted
          const isCorrectOption = index === correctAnswerIndex

          return (
            <label
              key={index}
              className={clsx(
                'flex items-start gap-3 p-4 border-2 rounded-lg cursor-pointer transition-all',
                'focus-within:ring-2 focus-within:ring-primary focus-within:ring-offset-2',
                {
                  // Default state
                  'border-gray-300 dark:border-gray-700 hover:border-primary hover:bg-gray-50 dark:hover:bg-gray-800':
                    !isSelected && !showFeedback,
                  // Selected but not submitted
                  'border-primary bg-primary/5': isSelected && !showFeedback,
                  // Submitted - correct answer
                  'border-green-500 bg-green-50 dark:bg-green-900/20':
                    showFeedback && isCorrectOption,
                  // Submitted - wrong selection
                  'border-red-500 bg-red-50 dark:bg-red-900/20':
                    showFeedback && isSelected && !isCorrectOption,
                  // Submitted - unselected options
                  'border-gray-200 dark:border-gray-700 opacity-50':
                    showFeedback && !isSelected && !isCorrectOption,
                  // Disable interaction after submission
                  'cursor-not-allowed': isSubmitted,
                }
              )}
            >
              {/* Radio button */}
              <input
                type="radio"
                name="multiple-choice-option"
                value={index}
                checked={isSelected}
                onChange={() => !isSubmitted && setSelectedOption(index)}
                disabled={isSubmitted}
                className="mt-1 w-4 h-4 text-primary focus:ring-primary"
                aria-label={`Option ${getOptionLetter(index)}`}
              />

              {/* Option content */}
              <div className="flex-1 min-w-0">
                <div className="flex items-start gap-2">
                  <span className="font-semibold text-gray-700 dark:text-gray-300">
                    {getOptionLetter(index)}.
                  </span>
                  <div className="flex-1 prose prose-sm dark:prose-invert max-w-none">
                    <ReactMarkdown>{option}</ReactMarkdown>
                  </div>
                </div>
              </div>

              {/* Feedback icon */}
              {showFeedback && isCorrectOption && (
                <CheckCircle2
                  className="w-6 h-6 text-green-600 dark:text-green-400 flex-shrink-0"
                  aria-label="Correct answer"
                />
              )}
              {showFeedback && isSelected && !isCorrectOption && (
                <XCircle
                  className="w-6 h-6 text-red-600 dark:text-red-400 flex-shrink-0"
                  aria-label="Incorrect answer"
                />
              )}
            </label>
          )
        })}
      </div>

      {/* Hints panel */}
      {hasHints && !isSubmitted && (
        <HintsPanel hints={challenge.hints!} onHintUsed={setHintsUsed} />
      )}

      {/* Action buttons */}
      {!isSubmitted ? (
        <div className="flex gap-3">
          <Button
            onClick={handleSubmit}
            disabled={selectedOption === null}
            variant="primary"
            size="lg"
            className="flex-1"
          >
            Submit Answer
          </Button>
        </div>
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
