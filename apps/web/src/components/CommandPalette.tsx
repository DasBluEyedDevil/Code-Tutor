import { useState, useEffect, useRef } from 'react'
import { Search, Home, BookOpen, Code, Settings as SettingsIcon, Keyboard } from 'lucide-react'
import { useNavigate } from 'react-router-dom'
import { useFocusTrap } from '../hooks/useFocusTrap'
import { useFocusReturn } from '../hooks/useFocusReturn'
import { clsx } from 'clsx'

export interface CommandAction {
  id: string
  title: string
  description?: string
  icon?: React.ReactNode
  action: () => void
  category: 'navigation' | 'settings' | 'help' | 'lesson'
  keywords?: string[]
}

interface CommandPaletteProps {
  isOpen: boolean
  onClose: () => void
  actions: CommandAction[]
}

export function CommandPalette({ isOpen, onClose, actions }: CommandPaletteProps) {
  const [query, setQuery] = useState('')
  const [selectedIndex, setSelectedIndex] = useState(0)
  const dialogRef = useFocusTrap<HTMLDivElement>(isOpen)
  const inputRef = useRef<HTMLInputElement>(null)
  useFocusReturn()

  // Filter actions based on query
  const filteredActions = query
    ? actions.filter((action) => {
        const searchText = `${action.title} ${action.description || ''} ${action.keywords?.join(' ') || ''}`.toLowerCase()
        return searchText.includes(query.toLowerCase())
      })
    : actions

  // Group actions by category
  const groupedActions = filteredActions.reduce((acc, action) => {
    if (!acc[action.category]) {
      acc[action.category] = []
    }
    acc[action.category].push(action)
    return acc
  }, {} as Record<string, CommandAction[]>)

  // Reset selected index when query changes
  useEffect(() => {
    setSelectedIndex(0)
  }, [query])

  // Reset query when opening
  useEffect(() => {
    if (isOpen) {
      setQuery('')
      setSelectedIndex(0)
      // Focus input after a small delay to ensure it's mounted
      setTimeout(() => inputRef.current?.focus(), 10)
    }
  }, [isOpen])

  // Handle keyboard navigation
  useEffect(() => {
    if (!isOpen) return

    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.key === 'ArrowDown') {
        e.preventDefault()
        setSelectedIndex((prev) => Math.min(prev + 1, filteredActions.length - 1))
      } else if (e.key === 'ArrowUp') {
        e.preventDefault()
        setSelectedIndex((prev) => Math.max(prev - 1, 0))
      } else if (e.key === 'Enter') {
        e.preventDefault()
        if (filteredActions[selectedIndex]) {
          filteredActions[selectedIndex].action()
          onClose()
        }
      }
    }

    window.addEventListener('keydown', handleKeyDown)
    return () => window.removeEventListener('keydown', handleKeyDown)
  }, [isOpen, filteredActions, selectedIndex, onClose])

  if (!isOpen) return null

  const categoryLabels = {
    navigation: 'Navigation',
    settings: 'Settings',
    help: 'Help',
    lesson: 'Lessons',
  }

  const categoryIcons = {
    navigation: <Home className="w-4 h-4" />,
    settings: <SettingsIcon className="w-4 h-4" />,
    help: <Keyboard className="w-4 h-4" />,
    lesson: <BookOpen className="w-4 h-4" />,
  }

  let currentIndex = 0

  return (
    <>
      {/* Backdrop */}
      <div
        className="fixed inset-0 bg-black/50 backdrop-blur-sm z-50 animate-fade-in"
        onClick={onClose}
        aria-hidden="true"
      />

      {/* Command Palette */}
      <div
        ref={dialogRef}
        role="dialog"
        aria-modal="true"
        aria-labelledby="command-palette-title"
        className="fixed top-[20%] left-1/2 -translate-x-1/2 z-50 w-full max-w-2xl"
      >
        <div className="bg-background border border-border rounded-lg shadow-2xl overflow-hidden animate-scale-in mx-4">
          {/* Search Input */}
          <div className="flex items-center gap-3 px-4 py-3 border-b border-border">
            <Search className="w-5 h-5 text-muted-foreground flex-shrink-0" />
            <input
              ref={inputRef}
              type="text"
              value={query}
              onChange={(e) => setQuery(e.target.value)}
              placeholder="Type a command or search..."
              className="flex-1 bg-transparent outline-none text-foreground placeholder:text-muted-foreground"
              aria-label="Search commands"
            />
            <kbd className="hidden sm:inline-block px-2 py-1 text-xs font-mono bg-secondary border border-border rounded">
              ESC
            </kbd>
          </div>

          {/* Results */}
          <div className="max-h-[60vh] overflow-y-auto">
            {filteredActions.length === 0 ? (
              <div className="px-4 py-8 text-center text-muted-foreground">
                <p>No results found</p>
                <p className="text-sm mt-1">Try a different search term</p>
              </div>
            ) : (
              Object.entries(groupedActions).map(([category, categoryActions]) => (
                <div key={category} className="py-2">
                  <div className="px-4 py-2 text-xs font-semibold text-muted-foreground flex items-center gap-2">
                    {categoryIcons[category as keyof typeof categoryIcons]}
                    {categoryLabels[category as keyof typeof categoryLabels]}
                  </div>
                  {categoryActions.map((action) => {
                    const isSelected = currentIndex === selectedIndex
                    const index = currentIndex++

                    return (
                      <button
                        key={action.id}
                        onClick={() => {
                          action.action()
                          onClose()
                        }}
                        onMouseEnter={() => setSelectedIndex(index)}
                        className={clsx(
                          'w-full px-4 py-3 flex items-center gap-3 text-left transition-colors',
                          isSelected
                            ? 'bg-primary/10 border-l-2 border-primary'
                            : 'hover:bg-secondary/50 border-l-2 border-transparent'
                        )}
                      >
                        {action.icon && (
                          <div
                            className={clsx(
                              'flex-shrink-0',
                              isSelected ? 'text-primary' : 'text-muted-foreground'
                            )}
                          >
                            {action.icon}
                          </div>
                        )}
                        <div className="flex-1 min-w-0">
                          <div className={clsx('text-sm font-medium', isSelected && 'text-primary')}>
                            {action.title}
                          </div>
                          {action.description && (
                            <div className="text-xs text-muted-foreground truncate">
                              {action.description}
                            </div>
                          )}
                        </div>
                      </button>
                    )
                  })}
                </div>
              ))
            )}
          </div>

          {/* Footer */}
          <div className="px-4 py-2 border-t border-border bg-secondary/20 flex items-center justify-between text-xs text-muted-foreground">
            <div className="flex items-center gap-4">
              <span className="flex items-center gap-1">
                <kbd className="px-1.5 py-0.5 bg-background border border-border rounded font-mono">↑</kbd>
                <kbd className="px-1.5 py-0.5 bg-background border border-border rounded font-mono">↓</kbd>
                to navigate
              </span>
              <span className="flex items-center gap-1">
                <kbd className="px-1.5 py-0.5 bg-background border border-border rounded font-mono">↵</kbd>
                to select
              </span>
            </div>
            <span className="hidden sm:block">
              <kbd className="px-1.5 py-0.5 bg-background border border-border rounded font-mono">Ctrl</kbd> +{' '}
              <kbd className="px-1.5 py-0.5 bg-background border border-border rounded font-mono">K</kbd>
            </span>
          </div>
        </div>
      </div>
    </>
  )
}

/**
 * Hook to generate default command actions
 */
export function useCommandActions(options: {
  onOpenSettings?: () => void
  onOpenShortcuts?: () => void
}) {
  const navigate = useNavigate()
  const { onOpenSettings, onOpenShortcuts } = options

  const defaultActions: CommandAction[] = [
    {
      id: 'home',
      title: 'Go to Home',
      description: 'Return to the main landing page',
      icon: <Home className="w-4 h-4" />,
      action: () => navigate('/'),
      category: 'navigation',
      keywords: ['home', 'landing', 'main'],
    },
  ]

  if (onOpenSettings) {
    defaultActions.push({
      id: 'settings',
      title: 'Open Settings',
      description: 'Configure your preferences',
      icon: <SettingsIcon className="w-4 h-4" />,
      action: onOpenSettings,
      category: 'settings',
      keywords: ['settings', 'preferences', 'config'],
    })
  }

  if (onOpenShortcuts) {
    defaultActions.push({
      id: 'shortcuts',
      title: 'Show Keyboard Shortcuts',
      description: 'View all available keyboard shortcuts',
      icon: <Keyboard className="w-4 h-4" />,
      action: onOpenShortcuts,
      category: 'help',
      keywords: ['shortcuts', 'keyboard', 'keys', 'hotkeys'],
    })
  }

  return defaultActions
}
