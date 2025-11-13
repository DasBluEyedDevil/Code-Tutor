import { useParams, Link, useNavigate } from 'react-router-dom'
import { useEffect, useState } from 'react'
import { ChevronRight, Home, Play, CheckCircle2, Lightbulb, ChevronLeft } from 'lucide-react'
import Editor from '@monaco-editor/react'
import ReactMarkdown from 'react-markdown'
import remarkGfm from 'remark-gfm'
import rehypeHighlight from 'rehype-highlight'
import { Course, Lesson, Module } from '../types/content'
import { useProgressStore } from '../stores/progressStore'
import { useThemeStore } from '../stores/themeStore'
import { fetchCourse, executeCode } from '../api/content'
import 'highlight.js/styles/github-dark.css'

export default function LessonPage() {
  const { language, moduleId, lessonId } = useParams<{
    language: string
    moduleId: string
    lessonId: string
  }>()
  const navigate = useNavigate()

  const [course, setCourse] = useState<Course | null>(null)
  const [currentModule, setCurrentModule] = useState<Module | null>(null)
  const [currentLesson, setCurrentLesson] = useState<Lesson | null>(null)
  const [code, setCode] = useState('')
  const [output, setOutput] = useState('')
  const [isRunning, setIsRunning] = useState(false)
  const [showHints, setShowHints] = useState(false)
  const [currentExerciseIndex, setCurrentExerciseIndex] = useState(0)

  const { theme } = useThemeStore()
  const { markLessonComplete, updateLessonProgress, getLessonProgress } = useProgressStore()

  useEffect(() => {
    if (language) {
      fetchCourse(language)
        .then((courseData) => {
          setCourse(courseData)
          const module = courseData.modules.find((m) => m.id === moduleId)
          const lesson = module?.lessons.find((l) => l.id === lessonId)

          if (module && lesson) {
            setCurrentModule(module)
            setCurrentLesson(lesson)

            // Set initial code from first exercise if available
            if (lesson.exercises && lesson.exercises.length > 0) {
              setCode(lesson.exercises[0].starterCode)
            } else if (courseData.languageConfig.editorSettings.defaultTemplate) {
              setCode(courseData.languageConfig.editorSettings.defaultTemplate)
            }

            // Update last accessed
            updateLessonProgress(language, moduleId, lessonId, {
              lessonId,
              completed: false,
              lastAccessed: new Date().toISOString(),
              codeSubmissions: 0,
              timeSpent: 0,
            })
          }
        })
        .catch((error) => console.error('Failed to load course:', error))
    }
  }, [language, moduleId, lessonId])

  const handleRunCode = async () => {
    if (!language || !code) return

    setIsRunning(true)
    setOutput('')

    try {
      const currentExercise = currentLesson?.exercises[currentExerciseIndex]
      const result = await executeCode(language, code, currentExercise?.testCases)

      if (result.success) {
        setOutput(`✓ Success!\n\nOutput:\n${result.output}`)

        if (result.testResults) {
          const { passed, failed } = result.testResults
          setOutput(
            (prev) =>
              `${prev}\n\nTest Results: ${passed} passed, ${failed} failed\nExecution time: ${result.executionTime}ms`
          )
        }
      } else {
        setOutput(`✗ Error:\n${result.error || 'Unknown error occurred'}`)
      }
    } catch (error: any) {
      setOutput(`✗ Error:\n${error.message || 'Failed to execute code'}`)
    } finally {
      setIsRunning(false)
    }
  }

  const handleMarkComplete = () => {
    if (language && moduleId && lessonId) {
      markLessonComplete(language, moduleId, lessonId)

      // Navigate to next lesson if available
      if (currentModule && currentLesson) {
        const currentLessonIndex = currentModule.lessons.findIndex((l) => l.id === lessonId)
        const nextLesson = currentModule.lessons[currentLessonIndex + 1]

        if (nextLesson) {
          navigate(`/course/${language}/module/${moduleId}/lesson/${nextLesson.id}`)
        } else {
          // Go back to course page if this is the last lesson
          navigate(`/course/${language}`)
        }
      }
    }
  }

  const handlePreviousLesson = () => {
    if (currentModule && currentLesson) {
      const currentLessonIndex = currentModule.lessons.findIndex((l) => l.id === lessonId)
      const prevLesson = currentModule.lessons[currentLessonIndex - 1]

      if (prevLesson) {
        navigate(`/course/${language}/module/${moduleId}/lesson/${prevLesson.id}`)
      }
    }
  }

  if (!course || !currentModule || !currentLesson || !language) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-lg">Loading lesson...</div>
      </div>
    )
  }

  const progress = getLessonProgress(language, moduleId, lessonId)
  const isCompleted = progress?.completed || false
  const currentExercise = currentLesson.exercises[currentExerciseIndex]

  return (
    <div className="min-h-screen bg-background">
      {/* Header */}
      <header className="border-b bg-card sticky top-0 z-10">
        <div className="container mx-auto px-4 py-3">
          <div className="flex items-center gap-2 text-sm text-muted-foreground">
            <Link to="/" className="hover:text-foreground">
              <Home className="w-4 h-4" />
            </Link>
            <ChevronRight className="w-4 h-4" />
            <Link to={`/course/${language}`} className="hover:text-foreground">
              {course.courseMetadata.displayName}
            </Link>
            <ChevronRight className="w-4 h-4" />
            <span className="text-foreground">{currentLesson.title}</span>
          </div>
        </div>
      </header>

      <div className="container mx-auto px-4 py-8">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          {/* Left: Lesson Content */}
          <div className="space-y-6">
            <div>
              <h1 className="text-3xl font-bold mb-2">{currentLesson.title}</h1>
              <div className="flex items-center gap-4 text-sm text-muted-foreground">
                <span className="capitalize">{currentLesson.type}</span>
                <span>·</span>
                <span>~{currentLesson.estimatedMinutes} min</span>
                <span>·</span>
                <span className="capitalize">{currentLesson.difficulty}</span>
                {isCompleted && (
                  <>
                    <span>·</span>
                    <span className="flex items-center gap-1 text-green-500">
                      <CheckCircle2 className="w-4 h-4" />
                      Completed
                    </span>
                  </>
                )}
              </div>
            </div>

            {/* Lesson Content */}
            <div className="prose prose-sm dark:prose-invert max-w-none">
              <ReactMarkdown remarkPlugins={[remarkGfm]} rehypePlugins={[rehypeHighlight]}>
                {currentLesson.content.body}
              </ReactMarkdown>
            </div>

            {/* Code Examples */}
            {currentLesson.content.codeExamples.map((example) => (
              <div key={example.id} className="border rounded-lg overflow-hidden">
                <div className="bg-secondary/50 px-4 py-2 text-sm font-medium">
                  {example.explanation}
                </div>
                <Editor
                  height="200px"
                  language={example.language}
                  value={example.code}
                  theme={theme === 'dark' ? 'vs-dark' : 'light'}
                  options={{
                    readOnly: true,
                    minimap: { enabled: false },
                    lineNumbers: 'on',
                  }}
                />
              </div>
            ))}

            {/* Exercise Instructions */}
            {currentExercise && (
              <div className="border rounded-lg p-6 bg-card">
                <h3 className="text-xl font-bold mb-2">{currentExercise.title}</h3>
                <p className="text-muted-foreground mb-4">{currentExercise.instructions}</p>

                {/* Hints */}
                <button
                  onClick={() => setShowHints(!showHints)}
                  className="flex items-center gap-2 text-sm text-primary hover:underline"
                >
                  <Lightbulb className="w-4 h-4" />
                  {showHints ? 'Hide' : 'Show'} Hints
                </button>

                {showHints && (
                  <div className="mt-4 space-y-2">
                    {currentExercise.hints.map((hint, index) => (
                      <div key={index} className="text-sm bg-secondary/50 p-3 rounded">
                        💡 {hint}
                      </div>
                    ))}
                  </div>
                )}
              </div>
            )}
          </div>

          {/* Right: Code Editor */}
          <div className="space-y-4">
            <div className="border rounded-lg overflow-hidden bg-card sticky top-24">
              <div className="bg-secondary/50 px-4 py-2 flex justify-between items-center">
                <span className="text-sm font-medium">Code Editor</span>
                <button
                  onClick={handleRunCode}
                  disabled={isRunning}
                  className="flex items-center gap-2 px-4 py-2 bg-primary text-primary-foreground rounded-md hover:opacity-90 disabled:opacity-50 transition-opacity"
                >
                  <Play className="w-4 h-4" />
                  {isRunning ? 'Running...' : 'Run Code'}
                </button>
              </div>

              <Editor
                height="400px"
                language={course.languageConfig.editorSettings.monacoLanguageId}
                value={code}
                onChange={(value) => setCode(value || '')}
                theme={theme === 'dark' ? 'vs-dark' : 'light'}
                options={{
                  minimap: { enabled: false },
                  fontSize: 14,
                  lineNumbers: 'on',
                  scrollBeyondLastLine: false,
                  automaticLayout: true,
                }}
              />

              {/* Output */}
              <div className="border-t bg-secondary/20 p-4">
                <div className="text-sm font-medium mb-2">Output:</div>
                <pre className="text-sm font-mono whitespace-pre-wrap bg-background p-3 rounded border min-h-[100px]">
                  {output || 'Run your code to see the output here...'}
                </pre>
              </div>
            </div>

            {/* Navigation Buttons */}
            <div className="flex gap-4">
              {currentModule.lessons.findIndex((l) => l.id === lessonId) > 0 && (
                <button
                  onClick={handlePreviousLesson}
                  className="flex items-center gap-2 px-6 py-3 border rounded-lg hover:bg-secondary transition-colors"
                >
                  <ChevronLeft className="w-4 h-4" />
                  Previous Lesson
                </button>
              )}
              <button
                onClick={handleMarkComplete}
                className="flex-1 flex items-center justify-center gap-2 px-6 py-3 bg-green-600 text-white rounded-lg hover:bg-green-700 transition-colors"
              >
                <CheckCircle2 className="w-4 h-4" />
                {isCompleted ? 'Continue to Next Lesson' : 'Mark as Complete'}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
