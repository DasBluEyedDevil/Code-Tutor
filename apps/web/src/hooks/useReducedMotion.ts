import { useEffect, useState } from 'react'

/**
 * Hook to detect if the user prefers reduced motion
 * Checks the prefers-reduced-motion media query
 */
export function useReducedMotion(): boolean {
  const [prefersReducedMotion, setPrefersReducedMotion] = useState(() => {
    if (typeof window === 'undefined') return false
    const mediaQuery = window.matchMedia('(prefers-reduced-motion: reduce)')
    return mediaQuery.matches
  })

  useEffect(() => {
    const mediaQuery = window.matchMedia('(prefers-reduced-motion: reduce)')

    const handleChange = (event: MediaQueryListEvent | MediaQueryList) => {
      setPrefersReducedMotion(event.matches)
    }

    // Set initial value
    handleChange(mediaQuery)

    // Listen for changes
    mediaQuery.addEventListener('change', handleChange)

    return () => {
      mediaQuery.removeEventListener('change', handleChange)
    }
  }, [])

  return prefersReducedMotion
}
