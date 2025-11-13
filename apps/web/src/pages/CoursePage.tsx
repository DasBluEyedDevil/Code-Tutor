import { useParams, Link } from 'react-router-dom'
import { useEffect, useState } from 'react'
import { ChevronRight, Home, CheckCircle2, Circle } from 'lucide-react'
import { Course } from '../types/content'
import { useProgressStore } from '../stores/progressStore'
import { fetchCourse } from '../api/content'

export default function CoursePage() {
  const { language } = useParams<{ language: string }>()
  const [course, setCourse] = useState<Course | null>(null)
  const [loading, setLoading] = useState(true)
  const getCourseProgress = useProgressStore((state) => state.getCourseProgress)
  const getLessonProgress = useProgressStore((state) => state.getLessonProgress)

  useEffect(() => {
    if (language) {
      fetchCourse(language)
        .then(setCourse)
        .catch((error) => console.error('Failed to load course:', error))
        .finally(() => setLoading(false))
    }
  }, [language])

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-lg">Loading course...</div>
      </div>
    )
  }

  if (!course || !language) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-center">
          <h2 className="text-2xl font-bold mb-4">Course not found</h2>
          <Link to="/" className="text-primary hover:underline">
            Return to home
          </Link>
        </div>
      </div>
    )
  }

  const overallProgress = getCourseProgress(language)

  return (
    <div className="min-h-screen bg-background">
      {/* Header */}
      <header className="border-b bg-card sticky top-0 z-10">
        <div className="container mx-auto px-4 py-4">
          <div className="flex items-center gap-2 text-sm text-muted-foreground mb-2">
            <Link to="/" className="hover:text-foreground flex items-center gap-1">
              <Home className="w-4 h-4" />
              Home
            </Link>
            <ChevronRight className="w-4 h-4" />
            <span className="text-foreground">{course.courseMetadata.displayName}</span>
          </div>
          <div className="flex justify-between items-center">
            <div>
              <h1 className="text-3xl font-bold">{course.courseMetadata.displayName}</h1>
              <p className="text-muted-foreground mt-1">{course.courseMetadata.description}</p>
            </div>
            <div className="text-right">
              <div className="text-sm text-muted-foreground">Overall Progress</div>
              <div className="text-3xl font-bold">{overallProgress}%</div>
            </div>
          </div>
        </div>
      </header>

      {/* Course Info */}
      <div className="container mx-auto px-4 py-8">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
          <div className="p-4 rounded-lg bg-card border">
            <div className="text-sm text-muted-foreground">Total Modules</div>
            <div className="text-2xl font-bold">{course.modules.length}</div>
          </div>
          <div className="p-4 rounded-lg bg-card border">
            <div className="text-sm text-muted-foreground">Estimated Time</div>
            <div className="text-2xl font-bold">{course.courseMetadata.estimatedHours}h</div>
          </div>
          <div className="p-4 rounded-lg bg-card border">
            <div className="text-sm text-muted-foreground">Difficulty</div>
            <div className="text-2xl font-bold capitalize">{course.courseMetadata.difficulty}</div>
          </div>
        </div>

        {/* Modules */}
        <div className="space-y-6">
          {course.modules.map((module) => {
            const completedLessons = module.lessons.filter((lesson) =>
              getLessonProgress(language, module.id, lesson.id)?.completed
            ).length
            const moduleProgress = Math.round((completedLessons / module.lessons.length) * 100)

            return (
              <div key={module.id} className="border rounded-lg bg-card overflow-hidden">
                <div className="p-6 bg-secondary/50">
                  <div className="flex justify-between items-start mb-2">
                    <div>
                      <h2 className="text-2xl font-bold">{module.title}</h2>
                      <p className="text-muted-foreground mt-1">{module.description}</p>
                    </div>
                    <div className="text-right">
                      <div className="text-sm text-muted-foreground">Progress</div>
                      <div className="text-xl font-bold">{moduleProgress}%</div>
                    </div>
                  </div>
                  <div className="text-sm text-muted-foreground">
                    {completedLessons} of {module.lessons.length} lessons completed · ~
                    {module.estimatedHours}h
                  </div>
                </div>

                <div className="divide-y">
                  {module.lessons.map((lesson) => {
                    const progress = getLessonProgress(language, module.id, lesson.id)
                    const isCompleted = progress?.completed || false

                    return (
                      <Link
                        key={lesson.id}
                        to={`/course/${language}/module/${module.id}/lesson/${lesson.id}`}
                        className="flex items-center gap-4 p-4 hover:bg-secondary/50 transition-colors"
                      >
                        {isCompleted ? (
                          <CheckCircle2 className="w-5 h-5 text-green-500 flex-shrink-0" />
                        ) : (
                          <Circle className="w-5 h-5 text-muted-foreground flex-shrink-0" />
                        )}
                        <div className="flex-1">
                          <h3 className="font-semibold">{lesson.title}</h3>
                          <div className="text-sm text-muted-foreground">
                            {lesson.type} · ~{lesson.estimatedMinutes} min · {lesson.difficulty}
                          </div>
                        </div>
                        <ChevronRight className="w-5 h-5 text-muted-foreground flex-shrink-0" />
                      </Link>
                    )
                  })}
                </div>
              </div>
            )
          })}
        </div>
      </div>
    </div>
  )
}
