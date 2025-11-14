import { useEffect, useRef } from 'react'

/**
 * Hook to auto-save data after a delay
 * Debounces the save function to avoid excessive saves
 */
export function useAutoSave<T>(
  data: T,
  saveFunction: (data: T) => void,
  options: {
    enabled: boolean
    delay: number
  }
) {
  const { enabled, delay } = options
  const timeoutRef = useRef<NodeJS.Timeout>()
  const previousDataRef = useRef<T>(data)

  useEffect(() => {
    // Skip if auto-save is disabled
    if (!enabled) return

    // Skip if data hasn't changed
    if (data === previousDataRef.current) return

    // Clear existing timeout
    if (timeoutRef.current) {
      clearTimeout(timeoutRef.current)
    }

    // Set new timeout for auto-save
    timeoutRef.current = setTimeout(() => {
      saveFunction(data)
      previousDataRef.current = data
    }, delay)

    // Cleanup timeout on unmount or data change
    return () => {
      if (timeoutRef.current) {
        clearTimeout(timeoutRef.current)
      }
    }
  }, [data, saveFunction, enabled, delay])
}
