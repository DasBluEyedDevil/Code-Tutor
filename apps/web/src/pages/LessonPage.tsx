import { useParams, Link, useNavigate } from 'react-router-dom'
import { useEffect, useState } from 'react'
import { ChevronRight, Home, Play, CheckCircle2, Lightbulb, ChevronLeft, AlertCircle, Terminal } from 'lucide-react'
import Editor from '@monaco-editor/react'
import ReactMarkdown from 'react-markdown'
import remarkGfm from 'remark-gfm'
import rehypeHighlight from 'rehype-highlight'
import { Course, Lesson, Module } from '../types/content'
import { useProgressStore } from '../stores/progressStore'
import { useThemeStore } from '../stores/themeStore'
import { useToastStore } from '../stores/toastStore'
import { fetchCourse, executeCode } from '../api/content'
import { Card, CardContent } from '../components/Card'
import { Badge } from '../components/Badge'
import { Button } from '../components/Button'
import { LoadingSpinner } from '../components/Loading'
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
  const { addToast } = useToastStore()

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

          if (failed === 0) {
            addToast({
              message: `All ${passed} tests passed! Great work!`,
              type: 'success',
              duration: 4000,
            })
          } else {
            addToast({
              message: `${passed} tests passed, ${failed} failed`,
              type: 'warning',
              duration: 4000,
            })
          }
        } else {
          addToast({
            message: 'Code executed successfully!',
            type: 'success',
            duration: 3000,
          })
        }
      } else {
        setOutput(`✗ Error:\n${result.error || 'Unknown error occurred'}`)
        addToast({
          message: 'Code execution failed',
          type: 'error',
          duration: 4000,
        })
      }
    } catch (error: any) {
      setOutput(`✗ Error:\n${error.message || 'Failed to execute code'}`)
      addToast({
        message: `Error: ${error.message || 'Failed to execute code'}`,
        type: 'error',
        duration: 4000,
      })
    } finally {
      setIsRunning(false)
    }
  }

  const handleMarkComplete = () => {
    if (language && moduleId && lessonId) {
      markLessonComplete(language, moduleId, lessonId)

      addToast({
        message: 'Lesson completed! Keep up the great work!',
        type: 'success',
        duration: 3000,
      })

      // Navigate to next lesson if available
      if (currentModule && currentLesson) {
        const currentLessonIndex = currentModule.lessons.findIndex((l) => l.id === lessonId)
        const nextLesson = currentModule.lessons[currentLessonIndex + 1]

        if (nextLesson) {
          setTimeout(() => {
            navigate(`/course/${language}/module/${moduleId}/lesson/${nextLesson.id}`)
          }, 500)
        } else {
          // Go back to course page if this is the last lesson
          setTimeout(() => {
            navigate(`/course/${language}`)
          }, 500)
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
        <div className="text-center">
          <LoadingSpinner size="lg" />
          <p className="mt-4 text-muted-foreground">Loading lesson...</p>
        </div>
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
            <div className="animate-fade-in">
              <h1 className="text-3xl font-bold mb-3">{currentLesson.title}</h1>
              <div className="flex items-center gap-2 flex-wrap">
                <Badge variant="info">{currentLesson.type}</Badge>
                <Badge
                  variant={
                    currentLesson.difficulty === 'beginner' ? 'success' :
                    currentLesson.difficulty === 'intermediate' ? 'warning' : 'error'
                  }
                >
                  {currentLesson.difficulty}
                </Badge>
                <Badge variant="default">~{currentLesson.estimatedMinutes} min</Badge>
                {isCompleted && (
                  <Badge variant="success" className="flex items-center gap-1">
                    <CheckCircle2 className="w-3 h-3" />
                    Completed
                  </Badge>
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
            {currentLesson.content.codeExamples.map((example, idx) => (
              <Card
                key={example.id}
                className="overflow-hidden animate-fade-in-up"
                style={{ animationDelay: `${idx * 100}ms` }}
              >
                <div className="bg-secondary/50 px-4 py-2.5 flex items-center gap-2">
                  <Terminal className="w-4 h-4 text-muted-foreground" />
                  <span className="text-sm font-medium">{example.explanation}</span>
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
              </Card>
            ))}

            {/* Exercise Instructions */}
            {currentExercise && (
              <Card className="animate-fade-in-up">
                <CardContent className="pt-6">
                  <div className="flex items-start gap-3 mb-4">
                    <div className="bg-primary/10 p-2 rounded-lg">
                      <AlertCircle className="w-5 h-5 text-primary" />
                    </div>
                    <div className="flex-1">
                      <h3 className="text-xl font-bold mb-2">{currentExercise.title}</h3>
                      <p className="text-muted-foreground">{currentExercise.instructions}</p>
                    </div>
                  </div>

                  {/* Hints */}
                  <Button
                    variant="ghost"
                    size="sm"
                    onClick={() => setShowHints(!showHints)}
                    className="flex items-center gap-2"
                  >
                    <Lightbulb className="w-4 h-4" />
                    {showHints ? 'Hide' : 'Show'} Hints ({currentExercise.hints.length})
                  </Button>

                  {showHints && (
                    <div className="mt-4 space-y-2 animate-fade-in">
                      {currentExercise.hints.map((hint, index) => (
                        <div key={index} className="text-sm bg-yellow-500/10 border border-yellow-500/20 p-3 rounded-lg flex items-start gap-2">
                          <Lightbulb className="w-4 h-4 text-yellow-500 flex-shrink-0 mt-0.5" />
                          <span>{hint}</span>
                        </div>
                      ))}
                    </div>
                  )}
                </CardContent>
              </Card>
            )}
          </div>

          {/* Right: Code Editor */}
          <div className="space-y-4">
            <Card className="overflow-hidden sticky top-24 animate-fade-in-up" style={{ animationDelay: '100ms' }}>
              <div className="bg-secondary/50 px-4 py-3 flex justify-between items-center border-b">
                <div className="flex items-center gap-2">
                  <Terminal className="w-4 h-4 text-muted-foreground" />
                  <span className="text-sm font-medium">Code Editor</span>
                </div>
                <Button
                  onClick={handleRunCode}
                  disabled={isRunning}
                  isLoading={isRunning}
                  size="sm"
                  className="flex items-center gap-2"
                >
                  {!isRunning && <Play className="w-4 h-4" />}
                  {isRunning ? 'Running...' : 'Run Code'}
                </Button>
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
                <div className="text-sm font-medium mb-3 flex items-center gap-2">
                  <Terminal className="w-4 h-4" />
                  Output:
                </div>
                <pre className={`text-sm font-mono whitespace-pre-wrap p-4 rounded-lg min-h-[120px] ${
                  output.startsWith('✓') ? 'bg-green-500/10 border border-green-500/20 text-green-700 dark:text-green-400' :
                  output.startsWith('✗') ? 'bg-red-500/10 border border-red-500/20 text-red-700 dark:text-red-400' :
                  'bg-background border border-border'
                }`}>
                  {output || 'Run your code to see the output here...'}
                </pre>
              </div>
            </Card>

            {/* Navigation Buttons */}
            <div className="flex gap-4 animate-fade-in">
              {currentModule.lessons.findIndex((l) => l.id === lessonId) > 0 && (
                <Button
                  variant="outline"
                  onClick={handlePreviousLesson}
                  className="flex items-center gap-2"
                >
                  <ChevronLeft className="w-4 h-4" />
                  Previous
                </Button>
              )}
              <Button
                variant="success"
                onClick={handleMarkComplete}
                className="flex-1 flex items-center justify-center gap-2"
              >
                <CheckCircle2 className="w-4 h-4" />
                {isCompleted ? 'Continue to Next' : 'Mark Complete'}
              </Button>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
