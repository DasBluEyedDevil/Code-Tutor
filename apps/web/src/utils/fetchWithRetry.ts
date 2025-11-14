/**
 * Fetch with Retry Utility
 *
 * Provides robust network request handling with exponential backoff,
 * timeout handling, and detailed error reporting.
 */

export interface RetryOptions {
  /** Maximum number of retry attempts (default: 3) */
  maxRetries?: number
  /** Initial delay in milliseconds (default: 1000) */
  initialDelay?: number
  /** Maximum delay in milliseconds (default: 10000) */
  maxDelay?: number
  /** Backoff multiplier (default: 2) */
  backoffMultiplier?: number
  /** Request timeout in milliseconds (default: 30000) */
  timeout?: number
  /** Function to determine if error is retryable (default: retries on network errors) */
  shouldRetry?: (error: Error, attempt: number) => boolean
  /** Callback for retry attempts */
  onRetry?: (attempt: number, delay: number, error: Error) => void
}

export interface FetchError extends Error {
  status?: number
  statusText?: string
  response?: Response
  isRetryable: boolean
}

const DEFAULT_OPTIONS: Required<RetryOptions> = {
  maxRetries: 3,
  initialDelay: 1000,
  maxDelay: 10000,
  backoffMultiplier: 2,
  timeout: 30000,
  shouldRetry: (error) => {
    // Retry on network errors, timeouts, and 5xx server errors
    const retryableStatuses = [408, 429, 500, 502, 503, 504]
    const fetchError = error as FetchError

    // Network error (no status)
    if (!fetchError.status) {
      return true
    }

    // Retryable HTTP status codes
    return retryableStatuses.includes(fetchError.status)
  },
  onRetry: () => {},
}

/**
 * Calculate delay for exponential backoff
 */
function calculateDelay(
  attempt: number,
  initialDelay: number,
  backoffMultiplier: number,
  maxDelay: number
): number {
  const delay = initialDelay * Math.pow(backoffMultiplier, attempt - 1)
  // Add jitter to prevent thundering herd
  const jitter = delay * 0.1 * Math.random()
  return Math.min(delay + jitter, maxDelay)
}

/**
 * Sleep for specified milliseconds
 */
function sleep(ms: number): Promise<void> {
  return new Promise((resolve) => setTimeout(resolve, ms))
}

/**
 * Create fetch error with additional metadata
 */
function createFetchError(
  message: string,
  status?: number,
  statusText?: string,
  response?: Response
): FetchError {
  const error = new Error(message) as FetchError
  error.name = 'FetchError'
  error.status = status
  error.statusText = statusText
  error.response = response
  error.isRetryable = DEFAULT_OPTIONS.shouldRetry(error, 0)
  return error
}

/**
 * Fetch with timeout
 */
async function fetchWithTimeout(
  url: string,
  options: RequestInit,
  timeout: number
): Promise<Response> {
  const controller = new AbortController()
  const timeoutId = setTimeout(() => controller.abort(), timeout)

  try {
    const response = await fetch(url, {
      ...options,
      signal: controller.signal,
    })
    clearTimeout(timeoutId)
    return response
  } catch (error) {
    clearTimeout(timeoutId)
    if (error instanceof Error && error.name === 'AbortError') {
      throw createFetchError('Request timeout', 408, 'Request Timeout')
    }
    throw error
  }
}

/**
 * Fetch with automatic retry on failure
 *
 * @example
 * ```ts
 * try {
 *   const response = await fetchWithRetry('/api/data', {
 *     method: 'POST',
 *     body: JSON.stringify({ key: 'value' })
 *   })
 *   const data = await response.json()
 * } catch (error) {
 *   console.error('Request failed after retries:', error)
 * }
 * ```
 */
export async function fetchWithRetry(
  url: string,
  options: RequestInit = {},
  retryOptions: RetryOptions = {}
): Promise<Response> {
  const config = { ...DEFAULT_OPTIONS, ...retryOptions }
  let lastError: FetchError | null = null

  for (let attempt = 0; attempt <= config.maxRetries; attempt++) {
    try {
      // Attempt the request
      const response = await fetchWithTimeout(url, options, config.timeout)

      // Check if response is ok
      if (!response.ok) {
        const error = createFetchError(
          `HTTP ${response.status}: ${response.statusText}`,
          response.status,
          response.statusText,
          response
        )

        // Determine if we should retry
        if (attempt < config.maxRetries && config.shouldRetry(error, attempt + 1)) {
          lastError = error
          const delay = calculateDelay(
            attempt + 1,
            config.initialDelay,
            config.backoffMultiplier,
            config.maxDelay
          )
          config.onRetry(attempt + 1, delay, error)
          await sleep(delay)
          continue
        }

        // Not retryable or out of retries
        throw error
      }

      // Success!
      return response
    } catch (error) {
      const fetchError =
        error instanceof Error
          ? createFetchError(error.message)
          : createFetchError('Unknown error')

      // Determine if we should retry
      if (attempt < config.maxRetries && config.shouldRetry(fetchError, attempt + 1)) {
        lastError = fetchError
        const delay = calculateDelay(
          attempt + 1,
          config.initialDelay,
          config.backoffMultiplier,
          config.maxDelay
        )
        config.onRetry(attempt + 1, delay, fetchError)
        await sleep(delay)
        continue
      }

      // Not retryable or out of retries
      throw fetchError
    }
  }

  // Should never reach here, but TypeScript needs it
  throw lastError || createFetchError('Maximum retries exceeded')
}

/**
 * Convenience method for JSON API calls
 *
 * @example
 * ```ts
 * const data = await fetchJSON<{ users: User[] }>('/api/users')
 * ```
 */
export async function fetchJSON<T = any>(
  url: string,
  options: RequestInit = {},
  retryOptions: RetryOptions = {}
): Promise<T> {
  const response = await fetchWithRetry(
    url,
    {
      ...options,
      headers: {
        'Content-Type': 'application/json',
        ...options.headers,
      },
    },
    retryOptions
  )

  return response.json()
}

/**
 * Check if user is online
 */
export function isOnline(): boolean {
  return navigator.onLine
}

/**
 * Wait for connection to be restored
 */
export function waitForConnection(timeoutMs: number = 30000): Promise<boolean> {
  return new Promise((resolve) => {
    if (isOnline()) {
      resolve(true)
      return
    }

    const timeout = setTimeout(() => {
      window.removeEventListener('online', handleOnline)
      resolve(false)
    }, timeoutMs)

    function handleOnline() {
      clearTimeout(timeout)
      window.removeEventListener('online', handleOnline)
      resolve(true)
    }

    window.addEventListener('online', handleOnline)
  })
}
