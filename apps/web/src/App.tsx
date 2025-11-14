import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { useEffect, lazy, Suspense } from 'react'
import { useThemeStore } from './stores/themeStore'
import { useToastStore } from './stores/toastStore'
import { Toast, ToastContainer } from './components/Toast'
import { ErrorBoundary } from './components/ErrorBoundary'
import { LoadingSpinner } from './components/Loading'

// Lazy load route components for code splitting
const LandingPage = lazy(() => import('./pages/LandingPage'))
const CoursePage = lazy(() => import('./pages/CoursePage'))
const LessonPage = lazy(() => import('./pages/LessonPage'))

function App() {
  const { theme } = useThemeStore()
  const { toasts, removeToast } = useToastStore()

  useEffect(() => {
    if (theme === 'dark') {
      document.documentElement.classList.add('dark')
    } else {
      document.documentElement.classList.remove('dark')
    }
  }, [theme])

  return (
    <ErrorBoundary>
      <BrowserRouter>
        <Suspense
          fallback={
            <div className="min-h-screen flex items-center justify-center bg-background">
              <div className="text-center">
                <LoadingSpinner size="lg" />
                <p className="mt-4 text-muted-foreground">Loading...</p>
              </div>
            </div>
          }
        >
          <Routes>
            <Route path="/" element={<LandingPage />} />
            <Route path="/course/:language" element={<CoursePage />} />
            <Route path="/course/:language/module/:moduleId/lesson/:lessonId" element={<LessonPage />} />
          </Routes>
        </Suspense>
        <ToastContainer>
          {toasts.map((toast) => (
            <Toast
              key={toast.id}
              id={toast.id}
              message={toast.message}
              type={toast.type}
              duration={toast.duration}
              onClose={removeToast}
            />
          ))}
        </ToastContainer>
      </BrowserRouter>
    </ErrorBoundary>
  )
}

export default App
