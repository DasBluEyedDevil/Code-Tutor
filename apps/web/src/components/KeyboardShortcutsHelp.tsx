import { X } from 'lucide-react'
import { Button } from './Button'
import { formatShortcut, type KeyboardShortcut } from '../hooks/useKeyboardShortcuts'
import { useFocusTrap } from '../hooks/useFocusTrap'
import { useFocusReturn } from '../hooks/useFocusReturn'
import { clsx } from 'clsx'

interface KeyboardShortcutsHelpProps {
  isOpen: boolean
  onClose: () => void
  shortcuts: KeyboardShortcut[]
}

export function KeyboardShortcutsHelp({ isOpen, onClose, shortcuts }: KeyboardShortcutsHelpProps) {
  const dialogRef = useFocusTrap<HTMLDivElement>(isOpen)
  useFocusReturn()

  if (!isOpen) return null

  // Group shortcuts by category (inferred from description)
  const groupedShortcuts = shortcuts.reduce((acc, shortcut) => {
    const category = inferCategory(shortcut.description)
    if (!acc[category]) {
      acc[category] = []
    }
    acc[category].push(shortcut)
    return acc
  }, {} as Record<string, KeyboardShortcut[]>)

  return (
    <>
      {/* Backdrop */}
      <div
        className="fixed inset-0 bg-black/50 backdrop-blur-sm z-50 animate-fade-in"
        onClick={onClose}
        aria-hidden="true"
      />

      {/* Modal */}
      <div
        ref={dialogRef}
        role="dialog"
        aria-modal="true"
        aria-labelledby="shortcuts-title"
        className="fixed inset-0 z-50 flex items-center justify-center p-4"
      >
        <div className="bg-background border border-border rounded-lg shadow-2xl max-w-2xl w-full max-h-[80vh] overflow-hidden animate-scale-in">
          {/* Header */}
          <div className="flex items-center justify-between p-6 border-b border-border">
            <h2 id="shortcuts-title" className="text-2xl font-bold">
              Keyboard Shortcuts
            </h2>
            <Button
              variant="ghost"
              size="sm"
              onClick={onClose}
              aria-label="Close keyboard shortcuts help"
              className="rounded-full"
            >
              <X className="w-5 h-5" />
            </Button>
          </div>

          {/* Content */}
          <div className="p-6 overflow-y-auto max-h-[calc(80vh-88px)]">
            {Object.entries(groupedShortcuts).map(([category, categoryShortcuts]) => (
              <div key={category} className="mb-6 last:mb-0">
                <h3 className="text-lg font-semibold mb-3 text-muted-foreground">
                  {category}
                </h3>
                <div className="space-y-2">
                  {categoryShortcuts.map((shortcut, index) => (
                    <div
                      key={index}
                      className="flex items-center justify-between py-2 px-3 rounded hover:bg-secondary/50 transition-colors"
                    >
                      <span className="text-sm">{shortcut.description}</span>
                      <kbd
                        className={clsx(
                          'px-3 py-1.5 text-xs font-mono font-semibold',
                          'bg-secondary border border-border rounded',
                          'shadow-sm'
                        )}
                      >
                        {formatShortcut(shortcut)}
                      </kbd>
                    </div>
                  ))}
                </div>
              </div>
            ))}
          </div>

          {/* Footer */}
          <div className="p-4 border-t border-border bg-secondary/20 text-center text-sm text-muted-foreground">
            Press <kbd className="px-2 py-1 text-xs font-mono bg-secondary border border-border rounded">?</kbd> to
            toggle this help dialog
          </div>
        </div>
      </div>
    </>
  )
}

// Helper function to infer category from description
function inferCategory(description: string): string {
  const lower = description.toLowerCase()

  if (lower.includes('navigation') || lower.includes('go to') || lower.includes('navigate')) {
    return 'Navigation'
  }
  if (lower.includes('search') || lower.includes('find') || lower.includes('command')) {
    return 'Search & Commands'
  }
  if (lower.includes('code') || lower.includes('editor') || lower.includes('run') || lower.includes('format')) {
    return 'Code Editor'
  }
  if (lower.includes('help') || lower.includes('shortcut')) {
    return 'Help'
  }
  if (lower.includes('theme') || lower.includes('settings') || lower.includes('preference')) {
    return 'Settings'
  }

  return 'General'
}
