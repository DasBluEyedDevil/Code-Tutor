/**
 * Error Logging and Monitoring Utilities
 *
 * Provides centralized error logging for development and production.
 * In production, errors can be sent to external services (Sentry, LogRocket, etc.)
 */

export interface ErrorContext {
  component?: string
  action?: string
  userId?: string
  metadata?: Record<string, any>
}

export interface LoggedError {
  timestamp: string
  message: string
  stack?: string
  context?: ErrorContext
  userAgent: string
  url: string
}

class ErrorLogger {
  private errors: LoggedError[] = []
  private maxStoredErrors = 50
  private isProduction = import.meta.env.PROD

  /**
   * Log an error with optional context
   */
  logError(error: Error, context?: ErrorContext): void {
    const loggedError: LoggedError = {
      timestamp: new Date().toISOString(),
      message: error.message,
      stack: error.stack,
      context,
      userAgent: navigator.userAgent,
      url: window.location.href,
    }

    // Store in memory (limited to prevent memory leaks)
    this.errors.push(loggedError)
    if (this.errors.length > this.maxStoredErrors) {
      this.errors.shift()
    }

    // Log to console in development
    if (!this.isProduction) {
      console.error('[ErrorLogger]', loggedError)
    }

    // In production, send to error tracking service
    if (this.isProduction) {
      this.sendToErrorService(loggedError)
    }
  }

  /**
   * Log a warning (non-critical error)
   */
  logWarning(message: string, context?: ErrorContext): void {
    const warning = {
      timestamp: new Date().toISOString(),
      message,
      context,
      userAgent: navigator.userAgent,
      url: window.location.href,
    }

    if (!this.isProduction) {
      console.warn('[ErrorLogger]', warning)
    }
  }

  /**
   * Log an info message for debugging
   */
  logInfo(message: string, metadata?: Record<string, any>): void {
    if (!this.isProduction) {
      console.info('[ErrorLogger]', message, metadata)
    }
  }

  /**
   * Get recent errors (for debugging)
   */
  getRecentErrors(): LoggedError[] {
    return [...this.errors]
  }

  /**
   * Clear error history
   */
  clearErrors(): void {
    this.errors = []
  }

  /**
   * Send error to external service
   * Override this method to integrate with your error tracking service
   */
  private sendToErrorService(error: LoggedError): void {
    // Example integration points:
    // - Sentry: Sentry.captureException(error)
    // - LogRocket: LogRocket.captureException(error)
    // - Custom API: fetch('/api/errors', { method: 'POST', body: JSON.stringify(error) })

    // For now, just log that we would send it
    if (!this.isProduction) {
      console.info('[ErrorLogger] Would send to error service:', error)
    }
  }

  /**
   * Set user context for error tracking
   */
  setUserContext(userId: string, metadata?: Record<string, any>): void {
    // Example: Sentry.setUser({ id: userId, ...metadata })
    this.logInfo('User context set', { userId, ...metadata })
  }

  /**
   * Add breadcrumb for debugging
   */
  addBreadcrumb(category: string, message: string, data?: Record<string, any>): void {
    // Example: Sentry.addBreadcrumb({ category, message, data })
    this.logInfo(`[${category}] ${message}`, data)
  }
}

// Singleton instance
export const errorLogger = new ErrorLogger()

/**
 * Global error handler for unhandled errors and promise rejections
 */
export function initializeErrorHandling(): void {
  // Handle unhandled errors
  window.addEventListener('error', (event) => {
    errorLogger.logError(event.error || new Error(event.message), {
      component: 'window',
      action: 'unhandled_error',
      metadata: {
        filename: event.filename,
        lineno: event.lineno,
        colno: event.colno,
      },
    })
  })

  // Handle unhandled promise rejections
  window.addEventListener('unhandledrejection', (event) => {
    const error = event.reason instanceof Error ? event.reason : new Error(String(event.reason))
    errorLogger.logError(error, {
      component: 'window',
      action: 'unhandled_rejection',
    })
  })

  errorLogger.logInfo('Error handling initialized')
}

/**
 * Wrapper for async functions to catch and log errors
 */
export function withErrorLogging<T extends (...args: any[]) => Promise<any>>(
  fn: T,
  context?: ErrorContext
): T {
  return (async (...args: any[]) => {
    try {
      return await fn(...args)
    } catch (error) {
      errorLogger.logError(error as Error, context)
      throw error
    }
  }) as T
}

/**
 * Performance monitoring utilities
 */
export class PerformanceMonitor {
  private marks = new Map<string, number>()

  /**
   * Start timing an operation
   */
  startTiming(label: string): void {
    this.marks.set(label, performance.now())
  }

  /**
   * End timing and log the duration
   */
  endTiming(label: string): number | null {
    const startTime = this.marks.get(label)
    if (!startTime) {
      errorLogger.logWarning(`No start mark found for: ${label}`)
      return null
    }

    const duration = performance.now() - startTime
    this.marks.delete(label)

    if (import.meta.env.DEV) {
      console.info(`[Performance] ${label}: ${duration.toFixed(2)}ms`)
    }

    // Log slow operations
    if (duration > 1000) {
      errorLogger.logWarning(`Slow operation detected: ${label}`, {
        metadata: { duration },
      })
    }

    return duration
  }

  /**
   * Measure and log memory usage
   */
  logMemoryUsage(): void {
    if ('memory' in performance) {
      const memory = (performance as any).memory
      errorLogger.logInfo('Memory usage', {
        usedJSHeapSize: `${(memory.usedJSHeapSize / 1024 / 1024).toFixed(2)} MB`,
        totalJSHeapSize: `${(memory.totalJSHeapSize / 1024 / 1024).toFixed(2)} MB`,
        jsHeapSizeLimit: `${(memory.jsHeapSizeLimit / 1024 / 1024).toFixed(2)} MB`,
      })
    }
  }
}

export const performanceMonitor = new PerformanceMonitor()
