import { BrowserRouter, Routes, Route, useNavigate } from 'react-router-dom'
import { useEffect, lazy, Suspense, useState } from 'react'
import { useThemeStore } from './stores/themeStore'
import { useToastStore } from './stores/toastStore'
import { Toast, ToastContainer } from './components/Toast'
import { ErrorBoundary } from './components/ErrorBoundary'
import { LoadingSpinner } from './components/Loading'
import { useKeyboardShortcuts } from './hooks/useKeyboardShortcuts'
import { useKeyboardStore } from './stores/keyboardStore'
import { KeyboardShortcutsHelp } from './components/KeyboardShortcutsHelp'
import { Settings } from './components/Settings'
import { Achievements } from './components/Achievements'
import { CommandPalette, useCommandActions } from './components/CommandPalette'
import { SkipToContent } from './components/SkipToContent'
import { useReducedMotion } from './hooks/useReducedMotion'
import { useMonacoSetup } from './hooks/useMonacoSetup'

// Lazy load route components for code splitting
const LandingPage = lazy(() => import('./pages/LandingPage'))
const CoursePage = lazy(() => import('./pages/CoursePage'))
const LessonPage = lazy(() => import('./pages/LessonPage'))
const NotFoundPage = lazy(() => import('./pages/NotFoundPage'))

// Inner component with router hooks
function AppContent() {
  const navigate = useNavigate()
  const { toggleTheme } = useThemeStore()
  const { toasts, removeToast } = useToastStore()
  const { shortcuts, isHelpOpen, toggleHelp, setHelpOpen, registerShortcuts } = useKeyboardStore()
  const [isSettingsOpen, setIsSettingsOpen] = useState(false)
  const [isCommandPaletteOpen, setIsCommandPaletteOpen] = useState(false)
  const [isAchievementsOpen, setIsAchievementsOpen] = useState(false)

  // Get default command actions
  const commandActions = useCommandActions({
    onOpenSettings: () => setIsSettingsOpen(true),
    onOpenShortcuts: () => setHelpOpen(true),
  })

  // Register global keyboard shortcuts
  useEffect(() => {
    registerShortcuts([
      {
        key: '?',
        description: 'Show keyboard shortcuts',
        action: toggleHelp,
      },
      {
        key: 'h',
        description: 'Go to home page',
        action: () => navigate('/'),
      },
      {
        key: 't',
        ctrl: true,
        description: 'Toggle theme',
        action: toggleTheme,
      },
      {
        key: ',',
        ctrl: true,
        description: 'Open settings',
        action: () => setIsSettingsOpen(true),
      },
      {
        key: 'k',
        ctrl: true,
        description: 'Open command palette',
        action: () => setIsCommandPaletteOpen(true),
      },
      {
        key: 'a',
        ctrl: true,
        description: 'Open achievements',
        action: () => setIsAchievementsOpen(true),
      },
      {
        key: 'Escape',
        description: 'Close dialogs',
        action: () => {
          setHelpOpen(false)
          setIsSettingsOpen(false)
          setIsCommandPaletteOpen(false)
          setIsAchievementsOpen(false)
        },
      },
    ])
  }, [navigate, toggleTheme, toggleHelp, setHelpOpen, registerShortcuts])

  // Apply keyboard shortcuts
  useKeyboardShortcuts(shortcuts, { enabled: true })

  return (
    <>
      <SkipToContent />
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
          <Route path="*" element={<NotFoundPage />} />
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
      <KeyboardShortcutsHelp
        isOpen={isHelpOpen}
        onClose={() => setHelpOpen(false)}
        shortcuts={shortcuts}
      />
      <Settings isOpen={isSettingsOpen} onClose={() => setIsSettingsOpen(false)} />
      <CommandPalette
        isOpen={isCommandPaletteOpen}
        onClose={() => setIsCommandPaletteOpen(false)}
        actions={commandActions}
      />
      <Achievements isOpen={isAchievementsOpen} onClose={() => setIsAchievementsOpen(false)} />
    </>
  )
}

function App() {
  const { theme, getEffectiveMotionPreference } = useThemeStore()
  const systemPrefersReducedMotion = useReducedMotion()

  // Set up Monaco editor with custom themes and configurations
  useMonacoSetup()

  useEffect(() => {
    if (theme === 'dark') {
      document.documentElement.classList.add('dark')
    } else {
      document.documentElement.classList.remove('dark')
    }
  }, [theme])

  useEffect(() => {
    const shouldReduceMotion = getEffectiveMotionPreference(systemPrefersReducedMotion)
    if (shouldReduceMotion) {
      document.documentElement.classList.add('reduce-motion')
    } else {
      document.documentElement.classList.remove('reduce-motion')
    }
  }, [systemPrefersReducedMotion, getEffectiveMotionPreference])

  return (
    <ErrorBoundary>
      <BrowserRouter>
        <AppContent />
      </BrowserRouter>
    </ErrorBoundary>
  )
}

export default App
