---
type: "EXAMPLE"
title: "Mocking Time"
---

Fake timers let you control setTimeout, setInterval, Date.now(), and new Date(). This is essential for testing time-dependent logic like debounce, cache expiration, and scheduled tasks without waiting real time.

```javascript
import { describe, it, expect, beforeEach, afterEach, setSystemTime, mock } from 'bun:test';
// Vitest: import { vi } from 'vitest'; then use vi.useFakeTimers()

// Functions that depend on time
function debounce(fn, delay) {
  let timeoutId;
  return function (...args) {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(() => fn.apply(this, args), delay);
  };
}

function throttle(fn, limit) {
  let inThrottle = false;
  return function (...args) {
    if (!inThrottle) {
      fn.apply(this, args);
      inThrottle = true;
      setTimeout(() => inThrottle = false, limit);
    }
  };
}

class CacheWithExpiry {
  constructor(ttlMs) {
    this.ttl = ttlMs;
    this.cache = new Map();
  }

  set(key, value) {
    this.cache.set(key, {
      value,
      expiry: Date.now() + this.ttl
    });
  }

  get(key) {
    const entry = this.cache.get(key);
    if (!entry) return undefined;
    if (Date.now() > entry.expiry) {
      this.cache.delete(key);
      return undefined;
    }
    return entry.value;
  }
}

function formatRelativeTime(date) {
  const now = Date.now();
  const diffMs = now - date.getTime();
  const diffMins = Math.floor(diffMs / 60000);
  
  if (diffMins < 1) return 'just now';
  if (diffMins < 60) return `${diffMins} minutes ago`;
  if (diffMins < 1440) return `${Math.floor(diffMins / 60)} hours ago`;
  return `${Math.floor(diffMins / 1440)} days ago`;
}

describe('Testing with Fake Timers', () => {
  beforeEach(() => {
    // Enable fake timers - all timer functions are now mocked
    mock.timers();
  });

  afterEach(() => {
    // Restore real timers
    mock.restore();
  });

  describe('Debounce', () => {
    it('delays function execution', () => {
      const callback = mock();
      const debounced = debounce(callback, 300);

      debounced('call 1');
      debounced('call 2');
      debounced('call 3');

      // Function hasn't been called yet
      expect(callback).not.toHaveBeenCalled();

      // Advance time by 300ms
      mock.advance(300);

      // Now it's called once with the last argument
      expect(callback).toHaveBeenCalledTimes(1);
      expect(callback).toHaveBeenCalledWith('call 3');
    });

    it('resets timer on each call', () => {
      const callback = mock();
      const debounced = debounce(callback, 300);

      debounced('call 1');
      mock.advance(200);  // 200ms passed
      
      debounced('call 2');  // Reset timer
      mock.advance(200);  // Another 200ms (400ms total, but only 200ms since last call)
      
      expect(callback).not.toHaveBeenCalled();  // Still waiting
      
      mock.advance(100);  // Now 300ms since last call
      expect(callback).toHaveBeenCalledWith('call 2');
    });
  });

  describe('Throttle', () => {
    it('limits call frequency', () => {
      const callback = mock();
      const throttled = throttle(callback, 1000);

      throttled('call 1');  // Executes immediately
      throttled('call 2');  // Ignored (in throttle)
      throttled('call 3');  // Ignored

      expect(callback).toHaveBeenCalledTimes(1);
      expect(callback).toHaveBeenCalledWith('call 1');

      mock.advance(1000);  // Throttle period ends

      throttled('call 4');  // Executes
      expect(callback).toHaveBeenCalledTimes(2);
      expect(callback).toHaveBeenCalledWith('call 4');
    });
  });

  describe('Cache with Expiry', () => {
    it('returns cached value before expiry', () => {
      const cache = new CacheWithExpiry(5000);  // 5 second TTL
      
      cache.set('user', { name: 'Alice' });
      
      mock.advance(4000);  // 4 seconds later
      expect(cache.get('user')).toEqual({ name: 'Alice' });
    });

    it('returns undefined after expiry', () => {
      const cache = new CacheWithExpiry(5000);
      
      cache.set('user', { name: 'Alice' });
      
      mock.advance(6000);  // 6 seconds later (past 5s TTL)
      expect(cache.get('user')).toBeUndefined();
    });
  });
});

describe('Testing Date-Dependent Code', () => {
  it('formats relative time correctly', () => {
    // Set a fixed "current" time
    const fixedNow = new Date('2024-01-15T12:00:00Z');
    setSystemTime(fixedNow);

    // Test various relative times
    expect(formatRelativeTime(new Date('2024-01-15T11:59:30Z'))).toBe('just now');
    expect(formatRelativeTime(new Date('2024-01-15T11:30:00Z'))).toBe('30 minutes ago');
    expect(formatRelativeTime(new Date('2024-01-15T09:00:00Z'))).toBe('3 hours ago');
    expect(formatRelativeTime(new Date('2024-01-13T12:00:00Z'))).toBe('2 days ago');

    // Restore real time
    setSystemTime();
  });

  it('tests time-sensitive business logic', () => {
    // Monday morning
    setSystemTime(new Date('2024-01-15T09:00:00Z'));
    
    function isBusinessHours() {
      const now = new Date();
      const hour = now.getUTCHours();
      const day = now.getUTCDay();
      return day >= 1 && day <= 5 && hour >= 9 && hour < 17;
    }

    expect(isBusinessHours()).toBe(true);

    // Saturday
    setSystemTime(new Date('2024-01-20T12:00:00Z'));
    expect(isBusinessHours()).toBe(false);

    // Monday evening
    setSystemTime(new Date('2024-01-15T18:00:00Z'));
    expect(isBusinessHours()).toBe(false);

    setSystemTime();  // Restore
  });
});
```
