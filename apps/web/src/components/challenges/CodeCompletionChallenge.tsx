import { useState, useRef } from 'react'
import { Play, Eye, EyeOff, RotateCcw, AlertCircle } from 'lucide-react'
import { clsx } from 'clsx'
import ReactMarkdown from 'react-markdown'
import Editor from '@monaco-editor/react'
import type { editor as MonacoEditor } from 'monaco-editor'
import { CodeCompletionChallenge as CodeCompletionChallengeType, TestResult } from '../../types/interactive'
import { Button } from '../Button'
import { HintsPanel } from './HintsPanel'
import { TestResultsPanel } from './TestResultsPanel'
import { CommonMistakesPanel } from './CommonMistakesPanel'
import { getEditorOptions } from '../../utils/monacoConfig'

/**
 * CodeCompletionChallenge Component
 *
 * Similar to FreeCodingChallenge but emphasizes completing partial code.
 * Students fill in missing parts of pre-written code to make it work.
 *
 * @example
 * ```tsx
 * <CodeCompletionChallenge
 *   challenge={{
 *     id: "cc-1",
 *     type: "CODE_COMPLETION",
 *     title: "Complete the Function",
 *     description: "Fill in the missing parts",
 *     starterCode: "function sum(a, b) {\n  // TODO: Complete this\n}",
 *     solution: "function sum(a, b) {\n  return a + b;\n}",
 *     language: "javascript",
 *     testCases: [...]
 *   }}
 *   onTestRun={(results) => console.log(results)}
 * />
 * ```
 */

interface CodeCompletionChallengeProps {
  /** Challenge data */
  challenge: CodeCompletionChallengeType
  /** Callback when tests are run */
  onTestRun?: (results: TestResult[], code: string) => void
  /** Callback when challenge is completed */
  onComplete?: (code: string, hintsUsed: number, testResults: TestResult[]) => void
  /** Custom className for styling */
  className?: string
}

export function CodeCompletionChallenge({
  challenge,
  onTestRun,
  onComplete,
  className,
}: CodeCompletionChallengeProps) {
  const [code, setCode] = useState(challenge.starterCode)
  const [testResults, setTestResults] = useState<TestResult[]>([])
  const [isRunningTests, setIsRunningTests] = useState(false)
  const [showSolution, setShowSolution] = useState(false)
  const [hintsUsed, setHintsUsed] = useState(0)
  const [hasRunTests, setHasRunTests] = useState(false)
  const editorRef = useRef<MonacoEditor.IStandaloneCodeEditor | null>(null)

  const hasHints = challenge.hints && challenge.hints.length > 0
  const hasCommonMistakes = challenge.commonMistakes && challenge.commonMistakes.length > 0

  // Calculate progress
  const allTestsPassed = testResults.length > 0 && testResults.every((r) => r.passed)
  const passedTestsCount = testResults.filter((r) => r.passed).length

  // Find TODO comments or blank areas in starter code
  const hasTodoMarkers = challenge.starterCode.includes('TODO') || challenge.starterCode.includes('// ...')

  const handleEditorDidMount = (editor: MonacoEditor.IStandaloneCodeEditor) => {
    editorRef.current = editor
    // Focus the editor on mount
    editor.focus()

    // Highlight TODO areas if present
    if (hasTodoMarkers) {
      // Find and select first TODO
      const model = editor.getModel()
      if (model) {
        const matches = model.findMatches('TODO', false, false, false, null, false)
        if (matches.length > 0) {
          editor.setSelection(matches[0].range)
          editor.revealLineInCenter(matches[0].range.startLineNumber)
        }
      }
    }
  }

  const handleRunTests = async () => {
    setIsRunningTests(true)
    setHasRunTests(true)

    // Simulate test execution (in real app, this would call the backend API)
    await new Promise((resolve) => setTimeout(resolve, 1000))

    // Mock test results - in production, this would come from the backend
    const mockResults: TestResult[] = challenge.testCases.map((testCase) => ({
      testCase,
      passed: Math.random() > 0.3, // Mock random pass/fail for demo
      actualOutput: 'Mock output',
      expectedOutput: testCase.expectedOutput,
    }))

    setTestResults(mockResults)
    setIsRunningTests(false)
    onTestRun?.(mockResults, code)

    // Check if all tests passed
    if (mockResults.every((r) => r.passed)) {
      onComplete?.(code, hintsUsed, mockResults)
    }
  }

  const handleReset = () => {
    setCode(challenge.starterCode)
    setTestResults([])
    setShowSolution(false)
    setHasRunTests(false)
    editorRef.current?.focus()
  }

  const handleToggleSolution = () => {
    if (!showSolution) {
      setCode(challenge.solution)
      setShowSolution(true)
    } else {
      setCode(challenge.starterCode)
      setShowSolution(false)
    }
    editorRef.current?.focus()
  }

  // Get Monaco editor options
  const editorOptions = getEditorOptions({
    language: challenge.language,
    readOnly: showSolution,
  })

  return (
    <div className={clsx('space-y-6', className)}>
      {/* Challenge header */}
      <div>
        <h2 className="text-2xl font-bold text-gray-900 dark:text-gray-100 mb-2">
          {challenge.title}
        </h2>
        {challenge.description && (
          <p className="text-gray-600 dark:text-gray-400 mb-4">{challenge.description}</p>
        )}
      </div>

      {/* Instructions with completion emphasis */}
      <div className="bg-purple-50 dark:bg-purple-900/20 border border-purple-200 dark:border-purple-800 rounded-lg p-4">
        <div className="flex items-start gap-3">
          <AlertCircle className="w-5 h-5 text-purple-600 dark:text-purple-400 flex-shrink-0 mt-0.5" />
          <div>
            <h3 className="font-semibold text-purple-900 dark:text-purple-200 mb-2">
              ✏️ Complete the Code
            </h3>
            <p className="text-sm text-purple-800 dark:text-purple-300 mb-3">
              This challenge provides partial code. Your task is to fill in the missing parts to make it work correctly.
              {hasTodoMarkers && ' Look for TODO comments to guide you.'}
            </p>
            {challenge.description && (
              <div className="prose prose-sm dark:prose-invert max-w-none text-purple-900 dark:text-purple-100">
                <ReactMarkdown>{challenge.description}</ReactMarkdown>
              </div>
            )}
          </div>
        </div>
      </div>

      {/* Main editor area */}
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        {/* Left column: Editor */}
        <div className="lg:col-span-2 space-y-4">
          {/* Editor */}
          <div className="border rounded-lg overflow-hidden shadow-sm">
            <div className="bg-gray-800 px-4 py-2 flex items-center justify-between">
              <div className="flex items-center gap-3">
                <span className="text-sm text-gray-300 font-mono">{challenge.language}</span>
                {hasTodoMarkers && (
                  <span className="text-xs bg-purple-500 text-white px-2 py-1 rounded">
                    Fill in TODOs
                  </span>
                )}
              </div>
              {showSolution && (
                <span className="text-xs bg-yellow-500 text-yellow-900 px-2 py-1 rounded font-semibold">
                  SOLUTION
                </span>
              )}
            </div>
            <div className="h-96">
              <Editor
                language={challenge.language}
                value={code}
                onChange={(value) => setCode(value || '')}
                onMount={handleEditorDidMount}
                options={editorOptions}
                theme="vs-dark"
              />
            </div>
          </div>

          {/* Action buttons */}
          <div className="flex flex-wrap gap-3">
            <Button
              onClick={handleRunTests}
              disabled={isRunningTests}
              isLoading={isRunningTests}
              variant="success"
              size="lg"
              className="flex-1 min-w-[200px]"
            >
              <Play className="w-5 h-5" aria-hidden="true" />
              {isRunningTests ? 'Running Tests...' : 'Run Tests'}
            </Button>

            <Button onClick={handleReset} variant="outline" size="lg">
              <RotateCcw className="w-4 h-4" aria-hidden="true" />
              Reset Code
            </Button>

            <Button
              onClick={handleToggleSolution}
              variant={showSolution ? 'danger' : 'ghost'}
              size="lg"
            >
              {showSolution ? (
                <>
                  <EyeOff className="w-4 h-4" aria-hidden="true" />
                  Hide Solution
                </>
              ) : (
                <>
                  <Eye className="w-4 h-4" aria-hidden="true" />
                  Show Solution
                </>
              )}
            </Button>
          </div>

          {/* Progress indicator */}
          {hasRunTests && (
            <div
              className={clsx('p-4 rounded-lg border-2 animate-in fade-in slide-in-from-top-2 duration-300', {
                'bg-green-50 dark:bg-green-900/20 border-green-500': allTestsPassed,
                'bg-yellow-50 dark:bg-yellow-900/20 border-yellow-500':
                  !allTestsPassed && passedTestsCount > 0,
                'bg-red-50 dark:bg-red-900/20 border-red-500': passedTestsCount === 0,
              })}
            >
              <p className="font-semibold text-gray-900 dark:text-gray-100">
                {allTestsPassed
                  ? '🎉 Perfect! You completed the code correctly!'
                  : `${passedTestsCount} of ${testResults.length} tests passed. Review your completion and try again!`}
              </p>
            </div>
          )}

          {/* Test results */}
          {hasRunTests && (
            <TestResultsPanel results={testResults} isLoading={isRunningTests} />
          )}
        </div>

        {/* Right column: Hints and Resources */}
        <div className="space-y-4">
          {/* Hints */}
          {hasHints && !showSolution && (
            <HintsPanel hints={challenge.hints!} onHintUsed={setHintsUsed} />
          )}

          {/* Common mistakes */}
          {hasCommonMistakes && (
            <CommonMistakesPanel mistakes={challenge.commonMistakes!} />
          )}

          {/* Success message */}
          {allTestsPassed && (
            <div className="border rounded-lg bg-gradient-to-br from-green-50 to-emerald-50 dark:from-green-900/20 dark:to-emerald-900/20 shadow-sm p-4">
              <div className="flex items-start gap-3">
                <div className="text-3xl">🎯</div>
                <div>
                  <h3 className="font-semibold text-gray-900 dark:text-gray-100 mb-1">
                    Code Completed!
                  </h3>
                  <p className="text-sm text-gray-600 dark:text-gray-400">
                    Excellent work! You've successfully completed the code. The implementation passes all tests.
                  </p>
                </div>
              </div>
            </div>
          )}

          {/* Metadata */}
          <div className="text-sm text-gray-500 dark:text-gray-400 space-y-1">
            {challenge.estimatedMinutes && (
              <p>⏱️ Estimated time: {challenge.estimatedMinutes} minutes</p>
            )}
            {challenge.difficulty && (
              <p>
                📊 Difficulty:{' '}
                <span className="capitalize">{challenge.difficulty.toString()}</span>
              </p>
            )}
          </div>
        </div>
      </div>
    </div>
  )
}
