import React, { useState } from 'react'
import { ConceptualChallenge as ConceptualChallengeType } from '@/types/interactive'
import { CheckCircle2, XCircle, Lightbulb, BookOpen } from 'lucide-react'
import ReactMarkdown from 'react-markdown'

interface ConceptualChallengeProps {
  challenge: ConceptualChallengeType
  onComplete?: (passed: boolean) => void
  className?: string
}

export function ConceptualChallenge({
  challenge,
  onComplete,
  className = ''
}: ConceptualChallengeProps) {
  const [userAnswer, setUserAnswer] = useState('')
  const [isSubmitted, setIsSubmitted] = useState(false)
  const [showSampleAnswers, setShowSampleAnswers] = useState(false)
  const [hintsUsed, setHintsUsed] = useState(0)

  const handleSubmit = () => {
    if (!userAnswer.trim()) {
      return
    }

    setIsSubmitted(true)
    // Conceptual challenges are not auto-graded
    // Mark as completed when user submits an answer
    onComplete?.(true)
  }

  const handleReset = () => {
    setUserAnswer('')
    setIsSubmitted(false)
    setShowSampleAnswers(false)
    setHintsUsed(0)
  }

  const revealHint = () => {
    if (challenge.hints && hintsUsed < challenge.hints.length) {
      setHintsUsed(hintsUsed + 1)
    }
  }

  return (
    <div className={`space-y-6 ${className}`}>
      {/* Challenge Header */}
      <div className="bg-purple-50 dark:bg-purple-900/20 border border-purple-200 dark:border-purple-800 rounded-lg p-6">
        <div className="flex items-start gap-3">
          <BookOpen className="w-6 h-6 text-purple-600 dark:text-purple-400 flex-shrink-0 mt-1" />
          <div className="flex-1">
            <h3 className="text-lg font-semibold text-gray-900 dark:text-gray-100 mb-2">
              {challenge.title}
            </h3>
            <div className="text-sm text-gray-600 dark:text-gray-300 space-y-2">
              <ReactMarkdown>{challenge.question}</ReactMarkdown>
            </div>
          </div>
        </div>
      </div>

      {/* Hints Section */}
      {challenge.hints && challenge.hints.length > 0 && (
        <div className="border border-yellow-200 dark:border-yellow-800 rounded-lg overflow-hidden">
          <button
            onClick={revealHint}
            disabled={hintsUsed >= challenge.hints.length}
            className="w-full flex items-center justify-between p-4 bg-yellow-50 dark:bg-yellow-900/20 hover:bg-yellow-100 dark:hover:bg-yellow-900/30 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
            aria-label="Reveal hint"
          >
            <div className="flex items-center gap-2">
              <Lightbulb className="w-5 h-5 text-yellow-600 dark:text-yellow-400" />
              <span className="font-medium text-gray-900 dark:text-gray-100">
                Hints ({hintsUsed}/{challenge.hints.length})
              </span>
            </div>
            {hintsUsed < challenge.hints.length && (
              <span className="text-sm text-gray-600 dark:text-gray-400">
                Click to reveal next hint
              </span>
            )}
          </button>

          {hintsUsed > 0 && (
            <div className="p-4 space-y-3 bg-white dark:bg-gray-800">
              {challenge.hints.slice(0, hintsUsed).map((hint, index) => (
                <div
                  key={index}
                  className="flex gap-3 p-3 bg-yellow-50 dark:bg-yellow-900/10 rounded border-l-4 border-yellow-400 dark:border-yellow-600"
                >
                  <Lightbulb className="w-4 h-4 text-yellow-600 dark:text-yellow-400 flex-shrink-0 mt-1" />
                  <div className="text-sm text-gray-700 dark:text-gray-300">
                    <ReactMarkdown>{hint.text}</ReactMarkdown>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      )}

      {/* Answer Input */}
      {!isSubmitted ? (
        <div className="space-y-3">
          <label
            htmlFor="conceptual-answer"
            className="block text-sm font-medium text-gray-700 dark:text-gray-300"
          >
            Your Answer
            <span className="ml-2 text-xs text-gray-500 dark:text-gray-400">
              (This will not be auto-graded - answer thoughtfully)
            </span>
          </label>
          <textarea
            id="conceptual-answer"
            value={userAnswer}
            onChange={(e) => setUserAnswer(e.target.value)}
            rows={8}
            className="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-purple-500 focus:border-transparent bg-white dark:bg-gray-800 text-gray-900 dark:text-gray-100 font-mono text-sm resize-y"
            placeholder="Type your answer here... Be specific and explain your reasoning."
            aria-describedby="answer-help"
          />
          <p
            id="answer-help"
            className="text-xs text-gray-500 dark:text-gray-400"
          >
            Take your time to think through the question. Consider multiple perspectives and provide examples if relevant.
          </p>

          <button
            onClick={handleSubmit}
            disabled={!userAnswer.trim()}
            className="px-6 py-3 bg-purple-600 hover:bg-purple-700 disabled:bg-gray-300 dark:disabled:bg-gray-700 text-white rounded-lg font-medium transition-colors disabled:cursor-not-allowed"
            aria-label="Submit answer"
          >
            Submit Answer
          </button>
        </div>
      ) : (
        <div className="space-y-4">
          {/* Submitted Answer */}
          <div className="border border-gray-300 dark:border-gray-600 rounded-lg overflow-hidden">
            <div className="bg-gray-50 dark:bg-gray-800 px-4 py-2 border-b border-gray-300 dark:border-gray-600">
              <h4 className="text-sm font-medium text-gray-700 dark:text-gray-300">
                Your Answer
              </h4>
            </div>
            <div className="p-4 bg-white dark:bg-gray-900">
              <div className="text-sm text-gray-700 dark:text-gray-300 whitespace-pre-wrap font-mono">
                {userAnswer}
              </div>
            </div>
          </div>

          {/* Feedback */}
          <div className="bg-green-50 dark:bg-green-900/20 border border-green-200 dark:border-green-800 rounded-lg p-4">
            <div className="flex items-start gap-3">
              <CheckCircle2 className="w-5 h-5 text-green-600 dark:text-green-400 flex-shrink-0 mt-0.5" />
              <div className="flex-1">
                <h4 className="font-medium text-green-900 dark:text-green-100 mb-2">
                  Answer Submitted
                </h4>
                <p className="text-sm text-green-800 dark:text-green-200">
                  Great job thinking through this conceptual question! Review the explanation below and compare it with your answer.
                </p>
              </div>
            </div>
          </div>

          {/* Explanation */}
          <div className="border border-blue-200 dark:border-blue-800 rounded-lg overflow-hidden">
            <div className="bg-blue-50 dark:bg-blue-900/20 px-4 py-2 border-b border-blue-200 dark:border-blue-800">
              <h4 className="text-sm font-medium text-blue-900 dark:text-blue-100">
                Explanation
              </h4>
            </div>
            <div className="p-4 bg-white dark:bg-gray-900">
              <div className="text-sm text-gray-700 dark:text-gray-300 prose dark:prose-invert max-w-none">
                <ReactMarkdown>{challenge.explanation}</ReactMarkdown>
              </div>
            </div>
          </div>

          {/* Sample Answers */}
          {challenge.sampleAnswers && challenge.sampleAnswers.length > 0 && (
            <div className="border border-gray-300 dark:border-gray-600 rounded-lg overflow-hidden">
              <button
                onClick={() => setShowSampleAnswers(!showSampleAnswers)}
                className="w-full flex items-center justify-between p-4 bg-gray-50 dark:bg-gray-800 hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors"
                aria-expanded={showSampleAnswers}
                aria-label="Toggle sample answers"
              >
                <span className="font-medium text-gray-900 dark:text-gray-100">
                  Sample Good Answers
                </span>
                <span className="text-sm text-gray-600 dark:text-gray-400">
                  {showSampleAnswers ? 'Hide' : 'Show'} ({challenge.sampleAnswers.length})
                </span>
              </button>

              {showSampleAnswers && (
                <div className="p-4 space-y-4 bg-white dark:bg-gray-900 border-t border-gray-300 dark:border-gray-600">
                  {challenge.sampleAnswers.map((answer, index) => (
                    <div
                      key={index}
                      className="p-4 bg-gray-50 dark:bg-gray-800 rounded border border-gray-200 dark:border-gray-700"
                    >
                      <div className="flex items-center gap-2 mb-2">
                        <CheckCircle2 className="w-4 h-4 text-green-600 dark:text-green-400" />
                        <span className="text-sm font-medium text-gray-700 dark:text-gray-300">
                          Sample Answer {index + 1}
                        </span>
                      </div>
                      <div className="text-sm text-gray-700 dark:text-gray-300 whitespace-pre-wrap">
                        {answer}
                      </div>
                    </div>
                  ))}
                </div>
              )}
            </div>
          )}

          {/* Reset Button */}
          <button
            onClick={handleReset}
            className="px-6 py-3 bg-gray-600 hover:bg-gray-700 text-white rounded-lg font-medium transition-colors"
            aria-label="Try another answer"
          >
            Try Another Answer
          </button>
        </div>
      )}

      {/* Description/Additional Context */}
      {challenge.description && challenge.description !== challenge.question && (
        <div className="border border-gray-300 dark:border-gray-600 rounded-lg p-4 bg-gray-50 dark:bg-gray-800">
          <h4 className="text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
            Additional Context
          </h4>
          <div className="text-sm text-gray-600 dark:text-gray-400 prose dark:prose-invert max-w-none">
            <ReactMarkdown>{challenge.description}</ReactMarkdown>
          </div>
        </div>
      )}
    </div>
  )
}
