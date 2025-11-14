import { CheckCircle2, XCircle, Eye, EyeOff, ChevronDown, ChevronUp } from 'lucide-react'
import { clsx } from 'clsx'
import { TestResult } from '../../types/interactive'
import { useState } from 'react'

/**
 * TestResultsPanel Component
 *
 * Displays test results with visual indicators for passed/failed tests.
 * Hides hidden test details until all visible tests pass.
 * Shows actual vs expected output for failed tests.
 *
 * @example
 * ```tsx
 * <TestResultsPanel
 *   results={[
 *     { testCase: { description: "Test 1", isVisible: true, expectedOutput: "5" }, passed: true, actualOutput: "5" }
 *   ]}
 *   isLoading={false}
 * />
 * ```
 */

interface TestResultsPanelProps {
  /** Array of test results */
  results: TestResult[]
  /** Loading state while tests are running */
  isLoading?: boolean
  /** Custom className for styling */
  className?: string
}

export function TestResultsPanel({ results, isLoading, className }: TestResultsPanelProps) {
  const [expandedTests, setExpandedTests] = useState<Set<number>>(new Set())

  if (results.length === 0 && !isLoading) {
    return null
  }

  // Separate visible and hidden tests
  const visibleTests = results.filter((result) => result.testCase.isVisible)
  const hiddenTests = results.filter((result) => !result.testCase.isVisible)

  // Check if all visible tests passed
  const allVisibleTestsPassed = visibleTests.length > 0 && visibleTests.every((result) => result.passed)

  // Calculate statistics
  const totalTests = results.length
  const passedTests = results.filter((result) => result.passed).length
  const failedTests = totalTests - passedTests

  const toggleTestExpanded = (index: number) => {
    const newExpanded = new Set(expandedTests)
    if (newExpanded.has(index)) {
      newExpanded.delete(index)
    } else {
      newExpanded.add(index)
    }
    setExpandedTests(newExpanded)
  }

  return (
    <div className={clsx('border rounded-lg bg-white dark:bg-gray-800 shadow-sm', className)}>
      {/* Header */}
      <div className="px-4 py-3 border-b dark:border-gray-700 bg-gray-50 dark:bg-gray-750">
        <div className="flex items-center justify-between">
          <h3 className="font-semibold text-gray-900 dark:text-gray-100">Test Results</h3>
          {!isLoading && (
            <div className="flex items-center gap-3 text-sm">
              <span className="flex items-center gap-1 text-green-600 dark:text-green-400">
                <CheckCircle2 className="w-4 h-4" aria-hidden="true" />
                {passedTests} passed
              </span>
              {failedTests > 0 && (
                <span className="flex items-center gap-1 text-red-600 dark:text-red-400">
                  <XCircle className="w-4 h-4" aria-hidden="true" />
                  {failedTests} failed
                </span>
              )}
            </div>
          )}
        </div>

        {/* Progress bar */}
        {!isLoading && totalTests > 0 && (
          <div className="mt-2">
            <div className="w-full bg-gray-200 dark:bg-gray-700 rounded-full h-2 overflow-hidden">
              <div
                className={clsx('h-full transition-all duration-500', {
                  'bg-green-500': passedTests === totalTests,
                  'bg-yellow-500': passedTests > 0 && passedTests < totalTests,
                  'bg-red-500': passedTests === 0,
                })}
                style={{ width: `${(passedTests / totalTests) * 100}%` }}
                role="progressbar"
                aria-valuenow={passedTests}
                aria-valuemin={0}
                aria-valuemax={totalTests}
                aria-label={`${passedTests} of ${totalTests} tests passed`}
              />
            </div>
          </div>
        )}
      </div>

      {/* Loading state */}
      {isLoading && (
        <div className="px-4 py-8 flex items-center justify-center">
          <div className="flex items-center gap-3">
            <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-primary" />
            <span className="text-gray-600 dark:text-gray-400">Running tests...</span>
          </div>
        </div>
      )}

      {/* Test results */}
      {!isLoading && (
        <div className="divide-y dark:divide-gray-700">
          {/* Visible tests */}
          {visibleTests.map((result, index) => (
            <TestResultItem
              key={`visible-${index}`}
              result={result}
              index={index}
              isExpanded={expandedTests.has(index)}
              onToggleExpand={() => toggleTestExpanded(index)}
            />
          ))}

          {/* Hidden tests section */}
          {hiddenTests.length > 0 && (
            <div className="p-4">
              <div className="flex items-start gap-2 p-3 bg-gray-50 dark:bg-gray-700 rounded-lg">
                {allVisibleTestsPassed ? (
                  <Eye className="w-5 h-5 text-blue-600 dark:text-blue-400 flex-shrink-0 mt-0.5" aria-hidden="true" />
                ) : (
                  <EyeOff className="w-5 h-5 text-gray-600 dark:text-gray-400 flex-shrink-0 mt-0.5" aria-hidden="true" />
                )}
                <div className="flex-1">
                  <p className="text-sm font-medium text-gray-900 dark:text-gray-100 mb-1">
                    Hidden Tests ({hiddenTests.length})
                  </p>
                  {allVisibleTestsPassed ? (
                    <div className="space-y-2">
                      <p className="text-sm text-gray-600 dark:text-gray-400">
                        All visible tests passed! Here are the hidden test results:
                      </p>
                      <div className="space-y-2 mt-3">
                        {hiddenTests.map((result, index) => (
                          <div
                            key={`hidden-${index}`}
                            className={clsx('flex items-center gap-2 text-sm', {
                              'text-green-600 dark:text-green-400': result.passed,
                              'text-red-600 dark:text-red-400': !result.passed,
                            })}
                          >
                            {result.passed ? (
                              <CheckCircle2 className="w-4 h-4" aria-hidden="true" />
                            ) : (
                              <XCircle className="w-4 h-4" aria-hidden="true" />
                            )}
                            <span>Hidden Test {index + 1}</span>
                            {result.testCase.customMessage && (
                              <span className="text-gray-600 dark:text-gray-400">
                                - {result.testCase.customMessage}
                              </span>
                            )}
                          </div>
                        ))}
                      </div>
                    </div>
                  ) : (
                    <p className="text-sm text-gray-600 dark:text-gray-400">
                      Pass all visible tests to see hidden test results.
                    </p>
                  )}
                </div>
              </div>
            </div>
          )}
        </div>
      )}
    </div>
  )
}

/**
 * Individual test result item component
 */
interface TestResultItemProps {
  result: TestResult
  index: number
  isExpanded: boolean
  onToggleExpand: () => void
}

function TestResultItem({ result, index, isExpanded, onToggleExpand }: TestResultItemProps) {
  const { testCase, passed, actualOutput, expectedOutput, errorMessage } = result
  const hasDetails = !passed && (actualOutput !== undefined || errorMessage)

  return (
    <div
      className={clsx('transition-colors', {
        'bg-green-50 dark:bg-green-900/10': passed,
        'bg-red-50 dark:bg-red-900/10': !passed,
      })}
    >
      <button
        onClick={hasDetails ? onToggleExpand : undefined}
        disabled={!hasDetails}
        className={clsx('w-full px-4 py-3 flex items-start gap-3 text-left', {
          'hover:bg-gray-50 dark:hover:bg-gray-700/50 cursor-pointer': hasDetails,
          'cursor-default': !hasDetails,
        })}
        aria-expanded={hasDetails ? isExpanded : undefined}
      >
        {/* Status icon */}
        {passed ? (
          <CheckCircle2
            className="w-5 h-5 text-green-600 dark:text-green-400 flex-shrink-0 mt-0.5"
            aria-label="Test passed"
          />
        ) : (
          <XCircle
            className="w-5 h-5 text-red-600 dark:text-red-400 flex-shrink-0 mt-0.5"
            aria-label="Test failed"
          />
        )}

        {/* Test description */}
        <div className="flex-1 min-w-0">
          <p className="font-medium text-gray-900 dark:text-gray-100">{testCase.description}</p>
          {testCase.customMessage && (
            <p className="text-sm text-gray-600 dark:text-gray-400 mt-1">{testCase.customMessage}</p>
          )}
        </div>

        {/* Expand icon */}
        {hasDetails && (
          <div className="flex-shrink-0">
            {isExpanded ? (
              <ChevronUp className="w-5 h-5 text-gray-500" aria-hidden="true" />
            ) : (
              <ChevronDown className="w-5 h-5 text-gray-500" aria-hidden="true" />
            )}
          </div>
        )}
      </button>

      {/* Expanded details */}
      {hasDetails && isExpanded && (
        <div className="px-4 pb-4 pl-12 space-y-3 animate-in fade-in slide-in-from-top-1 duration-200">
          {/* Error message */}
          {errorMessage && (
            <div className="p-3 bg-red-100 dark:bg-red-900/30 border border-red-300 dark:border-red-800 rounded-lg">
              <p className="text-sm font-medium text-red-900 dark:text-red-200 mb-1">Error:</p>
              <pre className="text-xs text-red-800 dark:text-red-300 whitespace-pre-wrap font-mono">
                {errorMessage}
              </pre>
            </div>
          )}

          {/* Expected vs Actual output */}
          {actualOutput !== undefined && (
            <div className="grid grid-cols-2 gap-3">
              <div className="space-y-1">
                <p className="text-xs font-medium text-gray-700 dark:text-gray-300">Expected Output:</p>
                <pre className="text-xs bg-white dark:bg-gray-900 border border-gray-300 dark:border-gray-700 rounded p-2 overflow-x-auto">
                  <code>{JSON.stringify(expectedOutput, null, 2)}</code>
                </pre>
              </div>
              <div className="space-y-1">
                <p className="text-xs font-medium text-gray-700 dark:text-gray-300">Actual Output:</p>
                <pre className="text-xs bg-white dark:bg-gray-900 border border-red-300 dark:border-red-700 rounded p-2 overflow-x-auto">
                  <code>{JSON.stringify(actualOutput, null, 2)}</code>
                </pre>
              </div>
            </div>
          )}
        </div>
      )}
    </div>
  )
}
