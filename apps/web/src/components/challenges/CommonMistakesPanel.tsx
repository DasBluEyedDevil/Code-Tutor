import { useState } from 'react'
import { AlertCircle, ChevronDown, ChevronUp } from 'lucide-react'
import { clsx } from 'clsx'
import ReactMarkdown from 'react-markdown'
import { CommonMistake } from '../../types/interactive'

/**
 * CommonMistakesPanel Component
 *
 * Displays common mistakes in an expandable panel with table format.
 * Shows mistake, consequence, correction, and code examples (wrong vs right).
 *
 * @example
 * ```tsx
 * <CommonMistakesPanel
 *   mistakes={[
 *     {
 *       mistake: "Using = instead of ==",
 *       consequence: "Assignment instead of comparison",
 *       correction: "Use == or === for comparison",
 *       example: {
 *         wrong: "if (x = 5) { }",
 *         right: "if (x === 5) { }"
 *       }
 *     }
 *   ]}
 * />
 * ```
 */

interface CommonMistakesPanelProps {
  /** Array of common mistakes */
  mistakes: CommonMistake[]
  /** Custom className for styling */
  className?: string
  /** Start expanded (default: false) */
  defaultExpanded?: boolean
}

export function CommonMistakesPanel({
  mistakes,
  className,
  defaultExpanded = false,
}: CommonMistakesPanelProps) {
  const [isExpanded, setIsExpanded] = useState(defaultExpanded)
  const [expandedMistakes, setExpandedMistakes] = useState<Set<number>>(new Set())

  if (mistakes.length === 0) {
    return null
  }

  const toggleMistakeExpanded = (index: number) => {
    const newExpanded = new Set(expandedMistakes)
    if (newExpanded.has(index)) {
      newExpanded.delete(index)
    } else {
      newExpanded.add(index)
    }
    setExpandedMistakes(newExpanded)
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
        aria-controls="mistakes-content"
      >
        <div className="flex items-center gap-2">
          <AlertCircle className="w-5 h-5 text-orange-500" aria-hidden="true" />
          <h3 className="font-semibold text-gray-900 dark:text-gray-100">
            Common Mistakes ({mistakes.length})
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
        <div id="mistakes-content" className="p-4">
          {/* Info message */}
          <div className="mb-4 p-3 bg-blue-50 dark:bg-blue-900/20 border border-blue-200 dark:border-blue-800 rounded-lg">
            <p className="text-sm text-blue-800 dark:text-blue-200">
              Review these common mistakes to avoid them in your solution.
            </p>
          </div>

          {/* Mistakes list */}
          <div className="space-y-3">
            {mistakes.map((mistake, index) => (
              <MistakeItem
                key={index}
                mistake={mistake}
                index={index}
                isExpanded={expandedMistakes.has(index)}
                onToggleExpand={() => toggleMistakeExpanded(index)}
              />
            ))}
          </div>
        </div>
      )}
    </div>
  )
}

/**
 * Individual mistake item component
 */
interface MistakeItemProps {
  mistake: CommonMistake
  index: number
  isExpanded: boolean
  onToggleExpand: () => void
}

function MistakeItem({ mistake, index, isExpanded, onToggleExpand }: MistakeItemProps) {
  const hasExample = mistake.example && (mistake.example.wrong || mistake.example.right)

  return (
    <div className="border border-gray-200 dark:border-gray-700 rounded-lg overflow-hidden">
      {/* Mistake header */}
      <button
        onClick={hasExample ? onToggleExpand : undefined}
        disabled={!hasExample}
        className={clsx(
          'w-full px-4 py-3 text-left bg-gray-50 dark:bg-gray-750 transition-colors',
          {
            'hover:bg-gray-100 dark:hover:bg-gray-700 cursor-pointer': hasExample,
            'cursor-default': !hasExample,
          }
        )}
        aria-expanded={hasExample ? isExpanded : undefined}
      >
        <div className="flex items-start justify-between gap-3">
          <div className="flex-1 min-w-0 space-y-3">
            {/* Table-like layout for desktop */}
            <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
              {/* Mistake */}
              <div>
                <p className="text-xs font-semibold text-gray-500 dark:text-gray-400 uppercase mb-1">
                  Mistake
                </p>
                <div className="prose prose-sm dark:prose-invert max-w-none">
                  <ReactMarkdown>{mistake.mistake}</ReactMarkdown>
                </div>
              </div>

              {/* Consequence */}
              <div>
                <p className="text-xs font-semibold text-gray-500 dark:text-gray-400 uppercase mb-1">
                  Consequence
                </p>
                <div className="prose prose-sm dark:prose-invert max-w-none">
                  <ReactMarkdown>{mistake.consequence}</ReactMarkdown>
                </div>
              </div>

              {/* Correction */}
              <div>
                <p className="text-xs font-semibold text-gray-500 dark:text-gray-400 uppercase mb-1">
                  Correction
                </p>
                <div className="prose prose-sm dark:prose-invert max-w-none text-green-700 dark:text-green-400">
                  <ReactMarkdown>{mistake.correction}</ReactMarkdown>
                </div>
              </div>
            </div>

            {/* Show examples indicator */}
            {hasExample && (
              <p className="text-xs text-blue-600 dark:text-blue-400 font-medium">
                {isExpanded ? '▼' : '▶'} {isExpanded ? 'Hide' : 'Show'} code examples
              </p>
            )}
          </div>

          {/* Expand icon */}
          {hasExample && (
            <div className="flex-shrink-0">
              {isExpanded ? (
                <ChevronUp className="w-5 h-5 text-gray-500" aria-hidden="true" />
              ) : (
                <ChevronDown className="w-5 h-5 text-gray-500" aria-hidden="true" />
              )}
            </div>
          )}
        </div>
      </button>

      {/* Code examples */}
      {hasExample && isExpanded && (
        <div className="px-4 py-4 bg-white dark:bg-gray-800 animate-in fade-in slide-in-from-top-1 duration-200">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            {/* Wrong example */}
            {mistake.example?.wrong && (
              <div>
                <div className="flex items-center gap-2 mb-2">
                  <div className="w-3 h-3 rounded-full bg-red-500" aria-hidden="true" />
                  <p className="text-sm font-semibold text-red-700 dark:text-red-400">
                    Wrong
                  </p>
                </div>
                <pre className="bg-red-50 dark:bg-red-900/20 border-2 border-red-300 dark:border-red-800 rounded-lg p-3 overflow-x-auto">
                  <code className="text-sm text-gray-900 dark:text-gray-100">
                    {mistake.example.wrong}
                  </code>
                </pre>
              </div>
            )}

            {/* Right example */}
            {mistake.example?.right && (
              <div>
                <div className="flex items-center gap-2 mb-2">
                  <div className="w-3 h-3 rounded-full bg-green-500" aria-hidden="true" />
                  <p className="text-sm font-semibold text-green-700 dark:text-green-400">
                    Correct
                  </p>
                </div>
                <pre className="bg-green-50 dark:bg-green-900/20 border-2 border-green-300 dark:border-green-800 rounded-lg p-3 overflow-x-auto">
                  <code className="text-sm text-gray-900 dark:text-gray-100">
                    {mistake.example.right}
                  </code>
                </pre>
              </div>
            )}
          </div>
        </div>
      )}
    </div>
  )
}
