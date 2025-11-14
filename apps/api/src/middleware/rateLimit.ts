import { Request, Response, NextFunction } from 'express'

/**
 * Rate Limiting Middleware
 *
 * Implements token bucket algorithm for rate limiting.
 * Prevents abuse while allowing burst traffic.
 */

interface RateLimitConfig {
  windowMs: number // Time window in milliseconds
  maxRequests: number // Maximum requests per window
  message?: string // Custom error message
  skipSuccessfulRequests?: boolean // Don't count successful requests
  skipFailedRequests?: boolean // Don't count failed requests
  keyGenerator?: (req: Request) => string // Custom key generator
}

interface RateLimitEntry {
  count: number
  resetTime: number
}

class RateLimiter {
  private store = new Map<string, RateLimitEntry>()
  private config: Required<RateLimitConfig>

  constructor(config: RateLimitConfig) {
    this.config = {
      windowMs: config.windowMs,
      maxRequests: config.maxRequests,
      message: config.message || 'Too many requests, please try again later.',
      skipSuccessfulRequests: config.skipSuccessfulRequests || false,
      skipFailedRequests: config.skipFailedRequests || false,
      keyGenerator: config.keyGenerator || this.defaultKeyGenerator,
    }

    // Clean up expired entries every minute
    setInterval(() => this.cleanup(), 60000)
  }

  private defaultKeyGenerator(req: Request): string {
    // Use IP address as default key
    return req.ip || req.socket.remoteAddress || 'unknown'
  }

  private cleanup(): void {
    const now = Date.now()
    for (const [key, entry] of this.store.entries()) {
      if (entry.resetTime < now) {
        this.store.delete(key)
      }
    }
  }

  middleware() {
    return (req: Request, res: Response, next: NextFunction) => {
      const key = this.config.keyGenerator(req)
      const now = Date.now()

      let entry = this.store.get(key)

      // Initialize or reset if window expired
      if (!entry || entry.resetTime < now) {
        entry = {
          count: 0,
          resetTime: now + this.config.windowMs,
        }
        this.store.set(key, entry)
      }

      // Check if limit exceeded
      if (entry.count >= this.config.maxRequests) {
        const retryAfter = Math.ceil((entry.resetTime - now) / 1000)

        res.set({
          'X-RateLimit-Limit': this.config.maxRequests.toString(),
          'X-RateLimit-Remaining': '0',
          'X-RateLimit-Reset': new Date(entry.resetTime).toISOString(),
          'Retry-After': retryAfter.toString(),
        })

        return res.status(429).json({
          error: this.config.message,
          retryAfter,
        })
      }

      // Increment counter
      entry.count++

      // Set rate limit headers
      res.set({
        'X-RateLimit-Limit': this.config.maxRequests.toString(),
        'X-RateLimit-Remaining': (this.config.maxRequests - entry.count).toString(),
        'X-RateLimit-Reset': new Date(entry.resetTime).toISOString(),
      })

      // Handle skipSuccessfulRequests and skipFailedRequests
      if (this.config.skipSuccessfulRequests || this.config.skipFailedRequests) {
        const originalSend = res.send

        res.send = function (body) {
          const statusCode = res.statusCode
          const isSuccessful = statusCode >= 200 && statusCode < 300
          const isFailed = statusCode >= 400

          if (
            (isSuccessful && this.config.skipSuccessfulRequests) ||
            (isFailed && this.config.skipFailedRequests)
          ) {
            entry!.count--
          }

          return originalSend.call(this, body)
        }.bind(res)
      }

      next()
    }
  }

  /**
   * Get current rate limit status for a key
   */
  getStatus(key: string): { remaining: number; resetTime: number } | null {
    const entry = this.store.get(key)
    if (!entry) {
      return null
    }

    return {
      remaining: Math.max(0, this.config.maxRequests - entry.count),
      resetTime: entry.resetTime,
    }
  }

  /**
   * Reset rate limit for a specific key
   */
  reset(key: string): void {
    this.store.delete(key)
  }

  /**
   * Reset all rate limits
   */
  resetAll(): void {
    this.store.clear()
  }
}

/**
 * Create a rate limiter with default config
 */
export function createRateLimiter(config: RateLimitConfig) {
  const limiter = new RateLimiter(config)
  return limiter.middleware()
}

/**
 * Preset rate limiters for common use cases
 */
export const rateLimiters = {
  // General API rate limit - 100 requests per minute
  general: createRateLimiter({
    windowMs: 60 * 1000,
    maxRequests: 100,
    message: 'Too many requests from this IP, please try again after a minute.',
  }),

  // Code execution rate limit - 30 executions per minute
  codeExecution: createRateLimiter({
    windowMs: 60 * 1000,
    maxRequests: 30,
    message: 'Too many code execution requests, please wait before running more code.',
  }),

  // Strict rate limit for sensitive operations - 10 requests per minute
  strict: createRateLimiter({
    windowMs: 60 * 1000,
    maxRequests: 10,
    message: 'Rate limit exceeded for this operation.',
  }),

  // Course content access - 200 requests per minute (generous for browsing)
  courseAccess: createRateLimiter({
    windowMs: 60 * 1000,
    maxRequests: 200,
    skipSuccessfulRequests: true, // Only count failed requests
  }),
}

/**
 * Create a custom rate limiter with IP + user-based key
 */
export function createUserBasedRateLimiter(config: Omit<RateLimitConfig, 'keyGenerator'>) {
  return createRateLimiter({
    ...config,
    keyGenerator: (req: Request) => {
      // Combine IP and user ID if available
      const ip = req.ip || req.socket.remoteAddress || 'unknown'
      const userId = (req as any).user?.id || 'anonymous'
      return `${ip}:${userId}`
    },
  })
}

export default createRateLimiter
