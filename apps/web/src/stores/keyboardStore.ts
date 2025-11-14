import { create } from 'zustand'
import type { KeyboardShortcut } from '../hooks/useKeyboardShortcuts'

interface KeyboardState {
  shortcuts: KeyboardShortcut[]
  isHelpOpen: boolean
  isEnabled: boolean
  registerShortcuts: (shortcuts: KeyboardShortcut[]) => void
  unregisterShortcuts: (keys: string[]) => void
  toggleHelp: () => void
  setHelpOpen: (isOpen: boolean) => void
  setEnabled: (enabled: boolean) => void
}

export const useKeyboardStore = create<KeyboardState>((set) => ({
  shortcuts: [],
  isHelpOpen: false,
  isEnabled: true,

  registerShortcuts: (newShortcuts) =>
    set((state) => {
      // Prevent duplicate shortcuts
      const existingKeys = new Set(
        state.shortcuts.map((s) => `${s.ctrl ? 'ctrl+' : ''}${s.meta ? 'meta+' : ''}${s.shift ? 'shift+' : ''}${s.alt ? 'alt+' : ''}${s.key}`)
      )

      const uniqueShortcuts = newShortcuts.filter((shortcut) => {
        const key = `${shortcut.ctrl ? 'ctrl+' : ''}${shortcut.meta ? 'meta+' : ''}${shortcut.shift ? 'shift+' : ''}${shortcut.alt ? 'alt+' : ''}${shortcut.key}`
        return !existingKeys.has(key)
      })

      return {
        shortcuts: [...state.shortcuts, ...uniqueShortcuts],
      }
    }),

  unregisterShortcuts: (keys) =>
    set((state) => ({
      shortcuts: state.shortcuts.filter((s) => !keys.includes(s.key)),
    })),

  toggleHelp: () => set((state) => ({ isHelpOpen: !state.isHelpOpen })),

  setHelpOpen: (isOpen) => set({ isHelpOpen: isOpen }),

  setEnabled: (enabled) => set({ isEnabled: enabled }),
}))
