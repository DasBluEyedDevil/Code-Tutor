import { useState } from 'react'
import { CheckCircle2, XCircle, Code } from 'lucide-react'
import { clsx } from 'clsx'
import ReactMarkdown from 'react-markdown'
import Editor from '@monaco-editor/react'
import { CodeOutputChallenge as CodeOutputChallengeType } from '../../types/interactive'
import { Button } from '../Button'
import { HintsPanel } from './HintsPanel'
import { getEditorOptions } from '../../utils/monacoConfig'

/**
 * CodeOutputChallenge Component
 *
 * Displays a code snippet with syntax highlighting and asks users to predict the output.
 * Shows explanation and correct output after submission.
 *
 * @example
 * ```tsx
 * <CodeOutputChallenge
 *   challenge={{
 *     id: "co-1",
 *     type: "CODE_OUTPUT",
 *     title: "Predict the Output",
 *     description: "What will this code print?",
 *     code: "console.log(2 + 2)",
 *     language: "javascript",
 *     correctOutput: "4",
 *     explanation: "The code adds 2 and 2, printing 4"
 *   }}
 *   onSubmit={(isCorrect, hintsUsed) => console.log("Submitted")}
 * />
 * ```
 */

interface CodeOutputChallengeProps {
  /** Challenge data */
  challenge: CodeOutputChallengeType
  /** Callback when challenge is submitted */
  onSubmit?: (isCorrect: boolean, hintsUsed: number) => void
  /** Custom className for styling */
  className?: string
}

export function CodeOutputChallenge({ challenge, onSubmit, className }: CodeOutputChallengeProps) {
  const [userOutput, setUserOutput] = useState('')
  const [isSubmitted, setIsSubmitted] = useState(false)
  const [hintsUsed, setHintsUsed] = useState(0)

  // Normalize outputs for comparison (trim whitespace, handle newlines)
  const normalizeOutput = (output: string) => {
    return output.trim().replace(/\r\n/g, '\n')
  }

  const isCorrect = normalizeOutput(userOutput) === normalizeOutput(challenge.correctOutput)
  const hasHints = challenge.hints && challenge.hints.length > 0

  const handleSubmit = () => {
    if (!userOutput.trim()) return

    setIsSubmitted(true)
    onSubmit?.(isCorrect, hintsUsed)
  }

  const handleReset = () => {
    setUserOutput('')
    setIsSubmitted(false)
    setHintsUsed(0)
  }

  // Get Monaco editor options
  const editorOptions = getEditorOptions({
    language: challenge.language,
    readOnly: true,
    compact: true,
  })

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

      {/* Code display */}
      <div className="border rounded-lg overflow-hidden shadow-sm">
        <div className="bg-gray-800 px-4 py-2 flex items-center justify-between">
          <div className="flex items-center gap-2">
            <Code className="w-4 h-4 text-gray-400" aria-hidden="true" />
            <span className="text-sm text-gray-300 font-mono">{challenge.language}</span>
          </div>
        </div>
        <div className="h-64">
          <Editor
            language={challenge.language}
            value={challenge.code}
            options={editorOptions}
            theme="vs-dark"
          />
        </div>
      </div>

      {/* Output input */}
      {!isSubmitted ? (
        <div className="space-y-3">
          <label
            htmlFor="output-input"
            className="block text-sm font-semibold text-gray-700 dark:text-gray-300"
          >
            What will this code output? 🤔
          </label>
          <textarea
            id="output-input"
            value={userOutput}
            onChange={(e) => setUserOutput(e.target.value)}
            placeholder="Enter the expected output..."
            rows={4}
            className={clsx(
              'w-full px-4 py-3 rounded-lg border-2 font-mono text-sm',
              'bg-white dark:bg-gray-800',
              'border-gray-300 dark:border-gray-700',
              'focus:outline-none focus:ring-2 focus:ring-primary focus:border-primary',
              'placeholder:text-gray-400 dark:placeholder:text-gray-500',
              'resize-y'
            )}
            aria-describedby="output-hint"
          />
          <p id="output-hint" className="text-xs text-gray-500 dark:text-gray-400">
            Enter exactly what you think will be printed to the console, including any newlines or spaces.
          </p>
        </div>
      ) : (
        /* Output comparison after submission */
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          {/* Expected output */}
          <div className="space-y-2">
            <p className="text-sm font-semibold text-gray-700 dark:text-gray-300">Expected Output:</p>
            <div className="p-4 bg-green-50 dark:bg-green-900/20 border-2 border-green-300 dark:border-green-800 rounded-lg">
              <pre className="text-sm font-mono whitespace-pre-wrap text-gray-900 dark:text-gray-100">
                {challenge.correctOutput}
              </pre>
            </div>
          </div>

          {/* User's output */}
          <div className="space-y-2">
            <p className="text-sm font-semibold text-gray-700 dark:text-gray-300">Your Output:</p>
            <div
              className={clsx('p-4 border-2 rounded-lg', {
                'bg-green-50 dark:bg-green-900/20 border-green-300 dark:border-green-800': isCorrect,
                'bg-red-50 dark:bg-red-900/20 border-red-300 dark:border-red-800': !isCorrect,
              })}
            >
              <pre className="text-sm font-mono whitespace-pre-wrap text-gray-900 dark:text-gray-100">
                {userOutput}
              </pre>
            </div>
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
          disabled={!userOutput.trim()}
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
                  {isCorrect ? '🎉 Correct!' : '❌ Not Quite Right'}
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
