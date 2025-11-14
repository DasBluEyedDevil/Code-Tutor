import { create } from 'zustand'
import { persist } from 'zustand/middleware'

type Theme = 'light' | 'dark'
type MotionPreference = 'auto' | 'always' | 'reduced'

interface ThemeStore {
  theme: Theme
  toggleTheme: () => void
  setTheme: (theme: Theme) => void
  motionPreference: MotionPreference
  setMotionPreference: (preference: MotionPreference) => void
  getEffectiveMotionPreference: (systemPrefersReduced: boolean) => boolean
}

export const useThemeStore = create<ThemeStore>()(
  persist(
    (set, get) => ({
      theme: 'light',
      toggleTheme: () => set((state) => ({ theme: state.theme === 'light' ? 'dark' : 'light' })),
      setTheme: (theme) => set({ theme }),
      motionPreference: 'auto',
      setMotionPreference: (preference) => set({ motionPreference: preference }),
      getEffectiveMotionPreference: (systemPrefersReduced) => {
        const { motionPreference } = get()
        if (motionPreference === 'always') return false
        if (motionPreference === 'reduced') return true
        return systemPrefersReduced // auto - respect system preference
      },
    }),
    {
      name: 'theme-storage',
    }
  )
)
