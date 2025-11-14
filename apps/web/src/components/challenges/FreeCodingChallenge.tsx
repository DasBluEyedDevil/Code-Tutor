import { useState, useRef } from 'react'
import { Play, Eye, EyeOff, RotateCcw } from 'lucide-react'
import { clsx } from 'clsx'
import ReactMarkdown from 'react-markdown'
import Editor from '@monaco-editor/react'
import type { editor as MonacoEditor } from 'monaco-editor'
import { FreeCodingChallenge as FreeCodingChallengeType, TestResult } from '../../types/interactive'
import { Button } from '../Button'
import { HintsPanel } from './HintsPanel'
import { TestResultsPanel } from './TestResultsPanel'
import { CommonMistakesPanel } from './CommonMistakesPanel'
import { getEditorOptions } from '../../utils/monacoConfig'

/**
 * FreeCodingChallenge Component
 *
 * Full-featured coding challenge with Monaco editor, test runner, hints, and solution toggle.
 * Provides an interactive environment for students to write and test their code.
 *
 * @example
 * ```tsx
 * <FreeCodingChallenge
 *   challenge={{
 *     id: "fc-1",
 *     type: "FREE_CODING",
 *     title: "Sum Function",
 *     instructions: "Write a function that adds two numbers",
 *     starterCode: "function sum(a, b) {\n  // Your code here\n}",
 *     solution: "function sum(a, b) {\n  return a + b;\n}",
 *     language: "javascript",
 *     testCases: [...]
 *   }}
 *   onTestRun={(results) => console.log(results)}
 * />
 * ```
 */

interface FreeCodingChallengeProps {
  /** Challenge data */
  challenge: FreeCodingChallengeType
  /** Callback when tests are run */
  onTestRun?: (results: TestResult[], code: string) => void
  /** Callback when challenge is completed */
  onComplete?: (code: string, hintsUsed: number, testResults: TestResult[]) => void
  /** Custom className for styling */
  className?: string
}

export function FreeCodingChallenge({
  challenge,
  onTestRun,
  onComplete,
  className,
}: FreeCodingChallengeProps) {
  const [code, setCode] = useState(challenge.starterCode)
  const [testResults, setTestResults] = useState<TestResult[]>([])
  const [isRunningTests, setIsRunningTests] = useState(false)
  const [showSolution, setShowSolution] = useState(false)
  const [hintsUsed, setHintsUsed] = useState(0)
  const [hasRunTests, setHasRunTests] = useState(false)
  const editorRef = useRef<MonacoEditor.IStandaloneCodeEditor | null>(null)

  const hasHints = challenge.hints && challenge.hints.length > 0
  const hasCommonMistakes = challenge.commonMistakes && challenge.commonMistakes.length > 0
  const hasBonusChallenges = challenge.bonusChallenges && challenge.bonusChallenges.length > 0

  // Calculate progress
  const allTestsPassed = testResults.length > 0 && testResults.every((r) => r.passed)
  const passedTestsCount = testResults.filter((r) => r.passed).length

  const handleEditorDidMount = (editor: MonacoEditor.IStandaloneCodeEditor) => {
    editorRef.current = editor
    // Focus the editor on mount
    editor.focus()
  }

  const handleRunTests = async () => {
    setIsRunningTests(true)
    setHasRunTests(true)

    // Simulate test execution (in real app, this would call the backend API)
    // For now, we'll create mock results
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

      {/* Instructions */}
      <div className="bg-blue-50 dark:bg-blue-900/20 border border-blue-200 dark:border-blue-800 rounded-lg p-4">
        <h3 className="font-semibold text-blue-900 dark:text-blue-200 mb-2">📋 Instructions</h3>
        <div className="prose prose-sm dark:prose-invert max-w-none text-blue-900 dark:text-blue-100">
          <ReactMarkdown>{challenge.instructions}</ReactMarkdown>
        </div>
      </div>

      {/* Main editor area */}
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        {/* Left column: Editor */}
        <div className="lg:col-span-2 space-y-4">
          {/* Editor */}
          <div className="border rounded-lg overflow-hidden shadow-sm">
            <div className="bg-gray-800 px-4 py-2 flex items-center justify-between">
              <span className="text-sm text-gray-300 font-mono">{challenge.language}</span>
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
              className={clsx('p-4 rounded-lg border-2', {
                'bg-green-50 dark:bg-green-900/20 border-green-500': allTestsPassed,
                'bg-yellow-50 dark:bg-yellow-900/20 border-yellow-500':
                  !allTestsPassed && passedTestsCount > 0,
                'bg-red-50 dark:bg-red-900/20 border-red-500': passedTestsCount === 0,
              })}
            >
              <p className="font-semibold text-gray-900 dark:text-gray-100">
                {allTestsPassed
                  ? '🎉 All tests passed! Great job!'
                  : `${passedTestsCount} of ${testResults.length} tests passed. Keep trying!`}
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

          {/* Bonus challenges */}
          {hasBonusChallenges && allTestsPassed && (
            <div className="border rounded-lg bg-gradient-to-br from-purple-50 to-pink-50 dark:from-purple-900/20 dark:to-pink-900/20 shadow-sm">
              <div className="px-4 py-3 border-b dark:border-gray-700">
                <h3 className="font-semibold text-gray-900 dark:text-gray-100">
                  🌟 Bonus Challenges
                </h3>
              </div>
              <div className="p-4 space-y-3">
                {challenge.bonusChallenges!.map((bonus, index) => (
                  <div
                    key={bonus.id || index}
                    className="p-3 bg-white dark:bg-gray-800 rounded-lg border border-gray-200 dark:border-gray-700"
                  >
                    <h4 className="font-semibold text-sm text-gray-900 dark:text-gray-100 mb-1">
                      {bonus.title}
                    </h4>
                    <p className="text-sm text-gray-600 dark:text-gray-400 mb-2">
                      {bonus.description}
                    </p>
                    <span className="text-xs px-2 py-1 bg-purple-100 dark:bg-purple-900 text-purple-700 dark:text-purple-300 rounded">
                      Difficulty: {bonus.difficulty}/5
                    </span>
                  </div>
                ))}
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
