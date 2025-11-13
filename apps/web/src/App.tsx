import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { useEffect } from 'react'
import LandingPage from './pages/LandingPage'
import CoursePage from './pages/CoursePage'
import LessonPage from './pages/LessonPage'
import { useThemeStore } from './stores/themeStore'

function App() {
  const { theme } = useThemeStore()

  useEffect(() => {
    if (theme === 'dark') {
      document.documentElement.classList.add('dark')
    } else {
      document.documentElement.classList.remove('dark')
    }
  }, [theme])

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/course/:language" element={<CoursePage />} />
        <Route path="/course/:language/module/:moduleId/lesson/:lessonId" element={<LessonPage />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
