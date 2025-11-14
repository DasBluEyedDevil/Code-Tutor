import { useState } from 'react'
import { Lightbulb, ChevronDown, ChevronUp, AlertTriangle } from 'lucide-react'
import { clsx } from 'clsx'
import ReactMarkdown from 'react-markdown'
import { Hint } from '../../types/interactive'
import { Button } from '../Button'

/**
 * HintsPanel Component
 *
 * Progressive hints reveal system that shows hints one at a time.
 * Tracks how many hints have been used and warns about potential score impact.
 *
 * @example
 * ```tsx
 * <HintsPanel
 *   hints={[
 *     { level: 1, text: "Think about the loop condition" },
 *     { level: 2, text: "You need to check if i < length", code: "for(int i=0; i<arr.length; i++)" }
 *   ]}
 *   onHintUsed={(count) => console.log(`${count} hints used`)}
 * />
 * ```
 */

interface HintsPanelProps {
  /** Array of hints ordered by difficulty level */
  hints: Hint[]
  /** Callback when a new hint is revealed */
  onHintUsed?: (hintsUsedCount: number) => void
  /** Custom className for styling */
  className?: string
}

export function HintsPanel({ hints, onHintUsed, className }: HintsPanelProps) {
  const [revealedHintsCount, setRevealedHintsCount] = useState(0)
  const [isExpanded, setIsExpanded] = useState(false)

  // Sort hints by level to ensure progressive reveal
  const sortedHints = [...hints].sort((a, b) => a.level - b.level)

  const handleRevealNextHint = () => {
    if (revealedHintsCount < sortedHints.length) {
      const newCount = revealedHintsCount + 1
      setRevealedHintsCount(newCount)
      setIsExpanded(true)
      onHintUsed?.(newCount)
    }
  }

  const hasMoreHints = revealedHintsCount < sortedHints.length
  const allHintsRevealed = revealedHintsCount === sortedHints.length && sortedHints.length > 0

  if (sortedHints.length === 0) {
    return null
  }

  return (
    <div className={clsx('border rounded-lg bg-white dark:bg-gray-800 shadow-sm', className)}>
      {/* Header */}
      <button
        onClick={() => setIsExpanded(!isExpanded)}
        className={clsx(
          'w-full flex items-center justify-between px-4 py-3',
          'hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors',
          'focus:outline-none focus:ring-2 focus:ring-primary focus:ring-inset rounded-t-lg'
        )}
        aria-expanded={isExpanded}
        aria-controls="hints-content"
      >
        <div className="flex items-center gap-2">
          <Lightbulb className="w-5 h-5 text-yellow-500" aria-hidden="true" />
          <h3 className="font-semibold text-gray-900 dark:text-gray-100">
            Hints {revealedHintsCount > 0 && `(${revealedHintsCount}/${sortedHints.length} revealed)`}
          </h3>
        </div>
        {isExpanded ? (
          <ChevronUp className="w-5 h-5 text-gray-500" aria-hidden="true" />
        ) : (
          <ChevronDown className="w-5 h-5 text-gray-500" aria-hidden="true" />
        )}
      </button>

      {/* Content */}
      {isExpanded && (
        <div id="hints-content" className="px-4 pb-4 space-y-4">
          {/* Warning about score impact */}
          {revealedHintsCount === 0 && (
            <div className="flex items-start gap-2 p-3 bg-yellow-50 dark:bg-yellow-900/20 border border-yellow-200 dark:border-yellow-800 rounded-lg">
              <AlertTriangle className="w-5 h-5 text-yellow-600 dark:text-yellow-500 flex-shrink-0 mt-0.5" aria-hidden="true" />
              <p className="text-sm text-yellow-800 dark:text-yellow-200">
                Using hints may affect your score. Try to solve the challenge on your own first!
              </p>
            </div>
          )}

          {/* Revealed hints */}
          {sortedHints.slice(0, revealedHintsCount).map((hint, index) => (
            <div
              key={index}
              className={clsx(
                'p-4 rounded-lg border animate-in fade-in slide-in-from-top-2 duration-300',
                {
                  'bg-blue-50 dark:bg-blue-900/20 border-blue-200 dark:border-blue-800': hint.level <= 2,
                  'bg-orange-50 dark:bg-orange-900/20 border-orange-200 dark:border-orange-800':
                    hint.level >= 3 && hint.level <= 4,
                  'bg-red-50 dark:bg-red-900/20 border-red-200 dark:border-red-800': hint.level >= 5,
                }
              )}
            >
              {/* Hint level badge */}
              <div className="flex items-center gap-2 mb-2">
                <span
                  className={clsx('text-xs font-semibold px-2 py-1 rounded', {
                    'bg-blue-100 dark:bg-blue-900 text-blue-700 dark:text-blue-300': hint.level <= 2,
                    'bg-orange-100 dark:bg-orange-900 text-orange-700 dark:text-orange-300':
                      hint.level >= 3 && hint.level <= 4,
                    'bg-red-100 dark:bg-red-900 text-red-700 dark:text-red-300': hint.level >= 5,
                  })}
                >
                  Level {hint.level}
                  {hint.level <= 2 && ' - Gentle Nudge'}
                  {hint.level >= 3 && hint.level <= 4 && ' - Strong Hint'}
                  {hint.level >= 5 && ' - Almost Solution'}
                </span>
              </div>

              {/* Hint text */}
              <div className="prose prose-sm dark:prose-invert max-w-none">
                <ReactMarkdown>{hint.text}</ReactMarkdown>
              </div>

              {/* Code snippet if provided */}
              {hint.code && (
                <div className="mt-3">
                  <pre className="bg-gray-900 dark:bg-gray-950 text-gray-100 p-3 rounded-lg overflow-x-auto">
                    <code>{hint.code}</code>
                  </pre>
                </div>
              )}
            </div>
          ))}

          {/* Reveal next hint button */}
          {hasMoreHints && (
            <Button
              onClick={handleRevealNextHint}
              variant="outline"
              className="w-full"
              aria-label={`Reveal hint ${revealedHintsCount + 1} of ${sortedHints.length}`}
            >
              <Lightbulb className="w-4 h-4" aria-hidden="true" />
              Reveal Next Hint ({revealedHintsCount + 1}/{sortedHints.length})
            </Button>
          )}

          {/* All hints revealed message */}
          {allHintsRevealed && (
            <div className="text-center p-3 bg-gray-50 dark:bg-gray-700 rounded-lg">
              <p className="text-sm text-gray-600 dark:text-gray-300">
                All hints revealed. You've got this! 💪
              </p>
            </div>
          )}
        </div>
      )}
    </div>
  )
}
