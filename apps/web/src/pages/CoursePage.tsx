import { useParams, Link, useNavigate } from 'react-router-dom'
import { useEffect, useState } from 'react'
import { ChevronRight, Home, CheckCircle2, Circle, BookOpen, Clock, Award, AlertCircle } from 'lucide-react'
import { Course } from '../types/content'
import { useProgressStore } from '../stores/progressStore'
import { fetchCourse } from '../api/content'
import { Card, CardContent } from '../components/Card'
import { Badge } from '../components/Badge'
import { ProgressBar } from '../components/ProgressBar'
import { LoadingSpinner, SkeletonCard } from '../components/Loading'
import { EmptyState } from '../components/EmptyState'

export default function CoursePage() {
  const { language } = useParams<{ language: string }>()
  const navigate = useNavigate()
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
      <div className="min-h-screen bg-background">
        <header className="border-b bg-card sticky top-0 z-10">
          <div className="container mx-auto px-4 py-4">
            <div className="h-20 animate-pulse bg-secondary/50 rounded"></div>
          </div>
        </header>
        <div className="container mx-auto px-4 py-8">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
            <SkeletonCard />
            <SkeletonCard />
            <SkeletonCard />
          </div>
          <div className="space-y-6">
            <SkeletonCard />
            <SkeletonCard />
          </div>
        </div>
      </div>
    )
  }

  if (!course || !language) {
    return (
      <div className="min-h-screen bg-background">
        <EmptyState
          icon={AlertCircle}
          title="Course Not Found"
          description="The course you're looking for doesn't exist or hasn't been created yet. Please check the URL or return to the home page."
          actionLabel="Return to Home"
          onAction={() => navigate('/')}
        />
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
          <div className="flex justify-between items-start gap-8">
            <div className="flex-1">
              <h1 className="text-3xl font-bold">{course.courseMetadata.displayName}</h1>
              <p className="text-muted-foreground mt-1">{course.courseMetadata.description}</p>
            </div>
            <div className="min-w-[200px]">
              <div className="text-sm text-muted-foreground mb-2">Overall Progress</div>
              <div className="text-3xl font-bold mb-2">{overallProgress}%</div>
              <ProgressBar value={overallProgress} variant={overallProgress === 100 ? 'success' : 'default'} />
            </div>
          </div>
        </div>
      </header>

      {/* Course Info */}
      <div className="container mx-auto px-4 py-8">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
          <Card hover className="animate-fade-in-up" style={{ animationDelay: '0ms' }}>
            <CardContent className="pt-6">
              <div className="flex items-center gap-4">
                <div className="bg-blue-500/10 p-3 rounded-lg">
                  <BookOpen className="w-6 h-6 text-blue-500" />
                </div>
                <div>
                  <div className="text-sm text-muted-foreground">Total Modules</div>
                  <div className="text-2xl font-bold">{course.modules.length}</div>
                </div>
              </div>
            </CardContent>
          </Card>
          <Card hover className="animate-fade-in-up" style={{ animationDelay: '50ms' }}>
            <CardContent className="pt-6">
              <div className="flex items-center gap-4">
                <div className="bg-green-500/10 p-3 rounded-lg">
                  <Clock className="w-6 h-6 text-green-500" />
                </div>
                <div>
                  <div className="text-sm text-muted-foreground">Estimated Time</div>
                  <div className="text-2xl font-bold">{course.courseMetadata.estimatedHours}h</div>
                </div>
              </div>
            </CardContent>
          </Card>
          <Card hover className="animate-fade-in-up" style={{ animationDelay: '100ms' }}>
            <CardContent className="pt-6">
              <div className="flex items-center gap-4">
                <div className="bg-purple-500/10 p-3 rounded-lg">
                  <Award className="w-6 h-6 text-purple-500" />
                </div>
                <div>
                  <div className="text-sm text-muted-foreground">Difficulty</div>
                  <Badge
                    variant={
                      course.courseMetadata.difficulty === 'beginner' ? 'success' :
                      course.courseMetadata.difficulty === 'intermediate' ? 'warning' : 'error'
                    }
                    className="text-sm px-3 py-1 mt-1"
                  >
                    {course.courseMetadata.difficulty}
                  </Badge>
                </div>
              </div>
            </CardContent>
          </Card>
        </div>

        {/* Modules */}
        <div className="space-y-6">
          {course.modules.map((module, index) => {
            const completedLessons = module.lessons.filter((lesson) =>
              getLessonProgress(language, module.id, lesson.id)?.completed
            ).length
            const moduleProgress = Math.round((completedLessons / module.lessons.length) * 100)

            return (
              <Card
                key={module.id}
                className="overflow-hidden animate-fade-in-up"
                style={{ animationDelay: `${index * 100}ms` }}
              >
                <CardContent className="p-6 bg-secondary/30">
                  <div className="flex flex-col md:flex-row md:justify-between md:items-start gap-4 mb-4">
                    <div className="flex-1">
                      <h2 className="text-2xl font-bold mb-2">{module.title}</h2>
                      <p className="text-muted-foreground">{module.description}</p>
                    </div>
                    <div className="min-w-[160px]">
                      <div className="text-sm text-muted-foreground mb-2">Module Progress</div>
                      <div className="text-2xl font-bold mb-2">{moduleProgress}%</div>
                      <ProgressBar
                        value={moduleProgress}
                        variant={moduleProgress === 100 ? 'success' : 'default'}
                        size="sm"
                      />
                    </div>
                  </div>
                  <div className="flex items-center gap-4 text-sm text-muted-foreground">
                    <span className="flex items-center gap-1">
                      <CheckCircle2 className="w-4 h-4" />
                      {completedLessons} / {module.lessons.length} lessons
                    </span>
                    <span>·</span>
                    <span className="flex items-center gap-1">
                      <Clock className="w-4 h-4" />
                      ~{module.estimatedHours}h
                    </span>
                  </div>
                </CardContent>

                <div className="divide-y">
                  {module.lessons.map((lesson) => {
                    const progress = getLessonProgress(language, module.id, lesson.id)
                    const isCompleted = progress?.completed || false

                    return (
                      <Link
                        key={lesson.id}
                        to={`/course/${language}/module/${module.id}/lesson/${lesson.id}`}
                        className="group flex items-center gap-4 p-4 hover:bg-secondary/50 transition-all duration-200"
                      >
                        {isCompleted ? (
                          <CheckCircle2 className="w-5 h-5 text-green-500 flex-shrink-0" />
                        ) : (
                          <Circle className="w-5 h-5 text-muted-foreground flex-shrink-0 group-hover:text-primary transition-colors" />
                        )}
                        <div className="flex-1 min-w-0">
                          <h3 className="font-semibold group-hover:text-primary transition-colors">
                            {lesson.title}
                          </h3>
                          <div className="flex items-center gap-2 mt-1 flex-wrap">
                            <Badge variant="info" className="text-xs">
                              {lesson.type}
                            </Badge>
                            <Badge
                              variant={
                                lesson.difficulty === 'beginner' ? 'success' :
                                lesson.difficulty === 'intermediate' ? 'warning' : 'error'
                              }
                              className="text-xs"
                            >
                              {lesson.difficulty}
                            </Badge>
                            <span className="text-xs text-muted-foreground flex items-center gap-1">
                              <Clock className="w-3 h-3" />
                              {lesson.estimatedMinutes} min
                            </span>
                          </div>
                        </div>
                        <ChevronRight className="w-5 h-5 text-muted-foreground flex-shrink-0 group-hover:text-primary group-hover:translate-x-1 transition-all" />
                      </Link>
                    )
                  })}
                </div>
              </Card>
            )
          })}
        </div>
      </div>
    </div>
  )
}
