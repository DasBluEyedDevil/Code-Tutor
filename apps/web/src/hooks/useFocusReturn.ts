import { useEffect, useRef } from 'react'

/**
 * Hook to return focus to the previously focused element when component unmounts
 * Useful for modals, popups, and other temporary UI elements
 */
export function useFocusReturn() {
  const previouslyFocusedElement = useRef<HTMLElement | null>(null)

  useEffect(() => {
    // Store the currently focused element when component mounts
    previouslyFocusedElement.current = document.activeElement as HTMLElement

    return () => {
      // Return focus to the previously focused element when component unmounts
      if (previouslyFocusedElement.current && typeof previouslyFocusedElement.current.focus === 'function') {
        previouslyFocusedElement.current.focus()
      }
    }
  }, [])
}
