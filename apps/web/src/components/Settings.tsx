import { X, Settings as SettingsIcon } from 'lucide-react'
import { Button } from './Button'
import { useFocusTrap } from '../hooks/useFocusTrap'
import { useFocusReturn } from '../hooks/useFocusReturn'
import { useThemeStore } from '../stores/themeStore'
import { usePreferencesStore } from '../stores/preferencesStore'

interface SettingsProps {
  isOpen: boolean
  onClose: () => void
}

export function Settings({ isOpen, onClose }: SettingsProps) {
  const dialogRef = useFocusTrap<HTMLDivElement>(isOpen)
  useFocusReturn()

  const { theme, setTheme, motionPreference, setMotionPreference } = useThemeStore()
  const {
    editor,
    autoSave,
    autoSaveDelay,
    soundEffects,
    notifications,
    setEditorPreference,
    setAutoSave,
    setAutoSaveDelay,
    setSoundEffects,
    setNotifications,
    resetToDefaults,
  } = usePreferencesStore()

  if (!isOpen) return null

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
        aria-labelledby="settings-title"
        className="fixed inset-0 z-50 flex items-center justify-center p-4"
      >
        <div className="bg-background border border-border rounded-lg shadow-2xl max-w-3xl w-full max-h-[85vh] overflow-hidden animate-scale-in">
          {/* Header */}
          <div className="flex items-center justify-between p-6 border-b border-border bg-secondary/20">
            <div className="flex items-center gap-3">
              <div className="bg-gradient-to-br from-primary/20 to-primary/5 p-2 rounded-lg">
                <SettingsIcon className="w-5 h-5 text-primary" />
              </div>
              <h2 id="settings-title" className="text-2xl font-bold">
                Settings
              </h2>
            </div>
            <Button
              variant="ghost"
              size="sm"
              onClick={onClose}
              aria-label="Close settings"
              className="rounded-full"
            >
              <X className="w-5 h-5" />
            </Button>
          </div>

          {/* Content */}
          <div className="p-6 overflow-y-auto max-h-[calc(85vh-160px)] space-y-8">
            {/* Appearance Section */}
            <section>
              <h3 className="text-lg font-semibold mb-4 text-primary">Appearance</h3>
              <div className="space-y-4">
                {/* Theme */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Theme</label>
                    <p className="text-xs text-muted-foreground">Choose your color theme</p>
                  </div>
                  <select
                    value={theme}
                    onChange={(e) => setTheme(e.target.value as 'light' | 'dark')}
                    className="px-3 py-2 bg-secondary border border-border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-primary"
                  >
                    <option value="light">Light</option>
                    <option value="dark">Dark</option>
                  </select>
                </div>

                {/* Motion Preference */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Animations</label>
                    <p className="text-xs text-muted-foreground">Control animation behavior</p>
                  </div>
                  <select
                    value={motionPreference}
                    onChange={(e) =>
                      setMotionPreference(e.target.value as 'auto' | 'always' | 'reduced')
                    }
                    className="px-3 py-2 bg-secondary border border-border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-primary"
                  >
                    <option value="auto">Auto (respect system)</option>
                    <option value="always">Always animate</option>
                    <option value="reduced">Reduce motion</option>
                  </select>
                </div>
              </div>
            </section>

            {/* Editor Section */}
            <section>
              <h3 className="text-lg font-semibold mb-4 text-primary">Code Editor</h3>
              <div className="space-y-4">
                {/* Font Size */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Font Size</label>
                    <p className="text-xs text-muted-foreground">Editor font size in pixels</p>
                  </div>
                  <div className="flex items-center gap-2">
                    <input
                      type="number"
                      min="10"
                      max="24"
                      value={editor.fontSize}
                      onChange={(e) =>
                        setEditorPreference('fontSize', parseInt(e.target.value))
                      }
                      className="w-20 px-3 py-2 bg-secondary border border-border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-primary"
                    />
                    <span className="text-xs text-muted-foreground">px</span>
                  </div>
                </div>

                {/* Tab Size */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Tab Size</label>
                    <p className="text-xs text-muted-foreground">Number of spaces per tab</p>
                  </div>
                  <select
                    value={editor.tabSize}
                    onChange={(e) => setEditorPreference('tabSize', parseInt(e.target.value))}
                    className="px-3 py-2 bg-secondary border border-border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-primary"
                  >
                    <option value="2">2 spaces</option>
                    <option value="4">4 spaces</option>
                    <option value="8">8 spaces</option>
                  </select>
                </div>

                {/* Word Wrap */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Word Wrap</label>
                    <p className="text-xs text-muted-foreground">Wrap long lines</p>
                  </div>
                  <label className="relative inline-flex items-center cursor-pointer">
                    <input
                      type="checkbox"
                      checked={editor.wordWrap === 'on'}
                      onChange={(e) =>
                        setEditorPreference('wordWrap', e.target.checked ? 'on' : 'off')
                      }
                      className="sr-only peer"
                    />
                    <div className="w-11 h-6 bg-secondary peer-focus:outline-none peer-focus:ring-2 peer-focus:ring-primary rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                  </label>
                </div>

                {/* Minimap */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Minimap</label>
                    <p className="text-xs text-muted-foreground">Show code minimap</p>
                  </div>
                  <label className="relative inline-flex items-center cursor-pointer">
                    <input
                      type="checkbox"
                      checked={editor.minimap}
                      onChange={(e) => setEditorPreference('minimap', e.target.checked)}
                      className="sr-only peer"
                    />
                    <div className="w-11 h-6 bg-secondary peer-focus:outline-none peer-focus:ring-2 peer-focus:ring-primary rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                  </label>
                </div>

                {/* Format on Paste */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Format on Paste</label>
                    <p className="text-xs text-muted-foreground">Auto-format when pasting code</p>
                  </div>
                  <label className="relative inline-flex items-center cursor-pointer">
                    <input
                      type="checkbox"
                      checked={editor.formatOnPaste}
                      onChange={(e) => setEditorPreference('formatOnPaste', e.target.checked)}
                      className="sr-only peer"
                    />
                    <div className="w-11 h-6 bg-secondary peer-focus:outline-none peer-focus:ring-2 peer-focus:ring-primary rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                  </label>
                </div>

                {/* Format on Type */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Format on Type</label>
                    <p className="text-xs text-muted-foreground">Auto-format while typing</p>
                  </div>
                  <label className="relative inline-flex items-center cursor-pointer">
                    <input
                      type="checkbox"
                      checked={editor.formatOnType}
                      onChange={(e) => setEditorPreference('formatOnType', e.target.checked)}
                      className="sr-only peer"
                    />
                    <div className="w-11 h-6 bg-secondary peer-focus:outline-none peer-focus:ring-2 peer-focus:ring-primary rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                  </label>
                </div>
              </div>
            </section>

            {/* Learning Section */}
            <section>
              <h3 className="text-lg font-semibold mb-4 text-primary">Learning</h3>
              <div className="space-y-4">
                {/* Auto-save */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Auto-save Progress</label>
                    <p className="text-xs text-muted-foreground">
                      Automatically save your code and progress
                    </p>
                  </div>
                  <label className="relative inline-flex items-center cursor-pointer">
                    <input
                      type="checkbox"
                      checked={autoSave}
                      onChange={(e) => setAutoSave(e.target.checked)}
                      className="sr-only peer"
                    />
                    <div className="w-11 h-6 bg-secondary peer-focus:outline-none peer-focus:ring-2 peer-focus:ring-primary rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                  </label>
                </div>

                {/* Auto-save Delay */}
                {autoSave && (
                  <div className="flex items-center justify-between ml-4">
                    <div>
                      <label className="text-sm font-medium">Save Delay</label>
                      <p className="text-xs text-muted-foreground">
                        Time before auto-saving (seconds)
                      </p>
                    </div>
                    <div className="flex items-center gap-2">
                      <input
                        type="number"
                        min="1"
                        max="10"
                        step="0.5"
                        value={autoSaveDelay / 1000}
                        onChange={(e) => setAutoSaveDelay(parseFloat(e.target.value) * 1000)}
                        className="w-20 px-3 py-2 bg-secondary border border-border rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-primary"
                      />
                      <span className="text-xs text-muted-foreground">sec</span>
                    </div>
                  </div>
                )}

                {/* Sound Effects */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Sound Effects</label>
                    <p className="text-xs text-muted-foreground">
                      Play sounds for achievements and completions
                    </p>
                  </div>
                  <label className="relative inline-flex items-center cursor-pointer">
                    <input
                      type="checkbox"
                      checked={soundEffects}
                      onChange={(e) => setSoundEffects(e.target.checked)}
                      className="sr-only peer"
                    />
                    <div className="w-11 h-6 bg-secondary peer-focus:outline-none peer-focus:ring-2 peer-focus:ring-primary rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                  </label>
                </div>

                {/* Notifications */}
                <div className="flex items-center justify-between">
                  <div>
                    <label className="text-sm font-medium">Notifications</label>
                    <p className="text-xs text-muted-foreground">Show success and error messages</p>
                  </div>
                  <label className="relative inline-flex items-center cursor-pointer">
                    <input
                      type="checkbox"
                      checked={notifications}
                      onChange={(e) => setNotifications(e.target.checked)}
                      className="sr-only peer"
                    />
                    <div className="w-11 h-6 bg-secondary peer-focus:outline-none peer-focus:ring-2 peer-focus:ring-primary rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-primary"></div>
                  </label>
                </div>
              </div>
            </section>
          </div>

          {/* Footer */}
          <div className="p-4 border-t border-border bg-secondary/20 flex justify-between items-center">
            <Button variant="ghost" size="sm" onClick={resetToDefaults}>
              Reset to Defaults
            </Button>
            <Button onClick={onClose}>Done</Button>
          </div>
        </div>
      </div>
    </>
  )
}
