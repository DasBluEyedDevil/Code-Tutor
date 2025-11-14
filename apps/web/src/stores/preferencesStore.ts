import { create } from 'zustand'
import { persist } from 'zustand/middleware'

export interface EditorPreferences {
  fontSize: number
  fontFamily: string
  tabSize: number
  wordWrap: 'on' | 'off'
  minimap: boolean
  lineNumbers: 'on' | 'off' | 'relative'
  formatOnPaste: boolean
  formatOnType: boolean
}

export interface UserPreferences {
  editor: EditorPreferences
  autoSave: boolean
  autoSaveDelay: number // milliseconds
  soundEffects: boolean
  notifications: boolean
}

interface PreferencesStore extends UserPreferences {
  setEditorPreference: <K extends keyof EditorPreferences>(
    key: K,
    value: EditorPreferences[K]
  ) => void
  setAutoSave: (enabled: boolean) => void
  setAutoSaveDelay: (delay: number) => void
  setSoundEffects: (enabled: boolean) => void
  setNotifications: (enabled: boolean) => void
  resetToDefaults: () => void
}

const defaultPreferences: UserPreferences = {
  editor: {
    fontSize: 14,
    fontFamily: 'Fira Code, Consolas, Monaco, "Courier New", monospace',
    tabSize: 2,
    wordWrap: 'on',
    minimap: false,
    lineNumbers: 'on',
    formatOnPaste: true,
    formatOnType: true,
  },
  autoSave: true,
  autoSaveDelay: 2000,
  soundEffects: true,
  notifications: true,
}

export const usePreferencesStore = create<PreferencesStore>()(
  persist(
    (set) => ({
      ...defaultPreferences,

      setEditorPreference: (key, value) =>
        set((state) => ({
          editor: {
            ...state.editor,
            [key]: value,
          },
        })),

      setAutoSave: (enabled) => set({ autoSave: enabled }),

      setAutoSaveDelay: (delay) => set({ autoSaveDelay: delay }),

      setSoundEffects: (enabled) => set({ soundEffects: enabled }),

      setNotifications: (enabled) => set({ notifications: enabled }),

      resetToDefaults: () => set(defaultPreferences),
    }),
    {
      name: 'user-preferences',
    }
  )
)
