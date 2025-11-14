import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { useEffect } from 'react'
import LandingPage from './pages/LandingPage'
import CoursePage from './pages/CoursePage'
import LessonPage from './pages/LessonPage'
import { useThemeStore } from './stores/themeStore'
import { useToastStore } from './stores/toastStore'
import { Toast, ToastContainer } from './components/Toast'
import { ErrorBoundary } from './components/ErrorBoundary'

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
        <Routes>
          <Route path="/" element={<LandingPage />} />
          <Route path="/course/:language" element={<CoursePage />} />
          <Route path="/course/:language/module/:moduleId/lesson/:lessonId" element={<LessonPage />} />
        </Routes>
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
