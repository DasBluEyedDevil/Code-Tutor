import { useNavigate } from 'react-router-dom'
import { Home, ArrowLeft, Search } from 'lucide-react'
import { Button } from '../components/Button'
import { Card, CardContent } from '../components/Card'

/**
 * NotFoundPage Component
 *
 * Displayed when users navigate to a non-existent route.
 * Provides helpful navigation options and search functionality.
 */
export default function NotFoundPage() {
  const navigate = useNavigate()

  const popularCourses = [
    { id: 'javascript', name: 'JavaScript', icon: '🟨' },
    { id: 'python', name: 'Python', icon: '🐍' },
    { id: 'java', name: 'Java', icon: '☕' },
    { id: 'csharp', name: 'C#', icon: '💜' },
  ]

  return (
    <div className="min-h-screen bg-gradient-to-br from-background via-background to-primary/5 flex items-center justify-center p-4">
      <div className="max-w-2xl w-full">
        <Card className="shadow-2xl border-2">
          <CardContent className="p-8 md:p-12 text-center">
            {/* 404 Animation */}
            <div className="relative mb-8">
              <div className="text-9xl font-bold bg-gradient-to-r from-primary via-purple-500 to-primary bg-clip-text text-transparent animate-gradient">
                404
              </div>
              <div className="absolute inset-0 bg-gradient-to-r from-primary/20 via-purple-500/20 to-primary/20 blur-3xl animate-pulse-glow"></div>
            </div>

            {/* Error Message */}
            <h1 className="text-3xl md:text-4xl font-bold mb-4 text-foreground">
              Page Not Found
            </h1>
            <p className="text-lg text-muted-foreground mb-8 max-w-md mx-auto">
              Oops! The page you're looking for doesn't exist. It might have been moved or deleted.
            </p>

            {/* Action Buttons */}
            <div className="flex flex-col sm:flex-row gap-4 justify-center mb-12">
              <Button
                onClick={() => navigate(-1)}
                variant="outline"
                size="lg"
                className="group"
              >
                <ArrowLeft className="w-5 h-5 mr-2 group-hover:-translate-x-1 transition-transform" />
                Go Back
              </Button>
              <Button
                onClick={() => navigate('/')}
                variant="primary"
                size="lg"
                className="group"
              >
                <Home className="w-5 h-5 mr-2 group-hover:scale-110 transition-transform" />
                Go Home
              </Button>
            </div>

            {/* Popular Courses */}
            <div className="border-t border-border/50 pt-8">
              <div className="flex items-center justify-center gap-2 text-sm text-muted-foreground mb-4">
                <Search className="w-4 h-4" />
                <span>Or explore our popular courses</span>
              </div>
              <div className="grid grid-cols-2 sm:grid-cols-4 gap-3">
                {popularCourses.map((course) => (
                  <button
                    key={course.id}
                    onClick={() => navigate(`/course/${course.id}`)}
                    className="flex flex-col items-center gap-2 p-4 rounded-lg border border-border/50 hover:border-primary hover:bg-primary/5 transition-all group"
                  >
                    <span className="text-3xl group-hover:scale-110 transition-transform">
                      {course.icon}
                    </span>
                    <span className="text-sm font-medium text-foreground">
                      {course.name}
                    </span>
                  </button>
                ))}
              </div>
            </div>

            {/* Help Text */}
            <div className="mt-8 text-sm text-muted-foreground">
              <p>
                Need help?{' '}
                <button
                  onClick={() => navigate('/')}
                  className="text-primary hover:underline font-medium"
                >
                  Contact support
                </button>
              </p>
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  )
}
