import { describe, it, expect, beforeEach } from 'vitest'
import { renderHook, act } from '@testing-library/react'
import { useThemeStore } from '../themeStore'

describe('themeStore', () => {
  beforeEach(() => {
    // Reset store before each test
    const { result } = renderHook(() => useThemeStore())
    act(() => {
      result.current.setTheme('light')
      result.current.setMotionPreference('auto')
    })
  })

  describe('theme', () => {
    it('has default theme of light', () => {
      const { result } = renderHook(() => useThemeStore())
      expect(result.current.theme).toBe('light')
    })

    it('can set theme to dark', () => {
      const { result } = renderHook(() => useThemeStore())

      act(() => {
        result.current.setTheme('dark')
      })

      expect(result.current.theme).toBe('dark')
    })

    it('toggles theme from light to dark', () => {
      const { result } = renderHook(() => useThemeStore())

      act(() => {
        result.current.setTheme('light')
      })
      expect(result.current.theme).toBe('light')

      act(() => {
        result.current.toggleTheme()
      })
      expect(result.current.theme).toBe('dark')
    })

    it('toggles theme from dark to light', () => {
      const { result } = renderHook(() => useThemeStore())

      act(() => {
        result.current.setTheme('dark')
      })
      expect(result.current.theme).toBe('dark')

      act(() => {
        result.current.toggleTheme()
      })
      expect(result.current.theme).toBe('light')
    })
  })

  describe('motion preference', () => {
    it('has default motion preference of auto', () => {
      const { result } = renderHook(() => useThemeStore())
      expect(result.current.motionPreference).toBe('auto')
    })

    it('can set motion preference to always', () => {
      const { result } = renderHook(() => useThemeStore())

      act(() => {
        result.current.setMotionPreference('always')
      })

      expect(result.current.motionPreference).toBe('always')
    })

    it('can set motion preference to reduced', () => {
      const { result } = renderHook(() => useThemeStore())

      act(() => {
        result.current.setMotionPreference('reduced')
      })

      expect(result.current.motionPreference).toBe('reduced')
    })
  })

  describe('getEffectiveMotionPreference', () => {
    it('returns false when preference is always, regardless of system', () => {
      const { result } = renderHook(() => useThemeStore())

      act(() => {
        result.current.setMotionPreference('always')
      })

      expect(result.current.getEffectiveMotionPreference(true)).toBe(false)
      expect(result.current.getEffectiveMotionPreference(false)).toBe(false)
    })

    it('returns true when preference is reduced, regardless of system', () => {
      const { result } = renderHook(() => useThemeStore())

      act(() => {
        result.current.setMotionPreference('reduced')
      })

      expect(result.current.getEffectiveMotionPreference(true)).toBe(true)
      expect(result.current.getEffectiveMotionPreference(false)).toBe(true)
    })

    it('returns system preference when preference is auto', () => {
      const { result } = renderHook(() => useThemeStore())

      act(() => {
        result.current.setMotionPreference('auto')
      })

      expect(result.current.getEffectiveMotionPreference(true)).toBe(true)
      expect(result.current.getEffectiveMotionPreference(false)).toBe(false)
    })
  })
})
