from dataclasses import dataclass
from typing import Dict, List, Optional
import time

@dataclass
class RateLimitResponse:
    allowed: bool
    limit: int
    remaining: int
    reset_timestamp: int
    retry_after: Optional[int] = None

class SlidingWindowRateLimiter:
    """
    Sliding window rate limiter for API protection.
    Uses in-memory storage (use Redis in production).
    """
    
    def __init__(self):
        self.windows: Dict[str, Dict[int, int]] = {}
    
    def _cleanup_old_windows(self, identifier: str, current_window: int, keep_windows: int = 2):
        """Remove windows older than needed."""
        if identifier not in self.windows:
            return
        old_windows = [w for w in self.windows[identifier] if w < current_window - keep_windows]
        for w in old_windows:
            del self.windows[identifier][w]
    
    def check_rate_limit(
        self,
        identifier: str,
        max_requests: int,
        window_seconds: int
    ) -> RateLimitResponse:
        """
        Check if request is allowed using sliding window algorithm.
        """
        now = time.time()
        current_window = int(now // window_seconds)
        previous_window = current_window - 1
        
        if identifier not in self.windows:
            self.windows[identifier] = {}
        
        # Get counts from both windows
        previous_count = self.windows[identifier].get(previous_window, 0)
        current_count = self.windows[identifier].get(current_window, 0)
        
        # Calculate weight for previous window
        elapsed_in_current = now - (current_window * window_seconds)
        weight = 1 - (elapsed_in_current / window_seconds)
        
        # Calculate weighted count
        weighted_count = (previous_count * weight) + current_count
        
        # Determine if request is allowed
        allowed = weighted_count < max_requests
        
        # If allowed, increment current window count
        if allowed:
            self.windows[identifier][current_window] = current_count + 1
            weighted_count += 1
        
        # Calculate remaining requests
        remaining = max(0, int(max_requests - weighted_count))
        
        # Calculate reset timestamp
        reset_timestamp = int((current_window + 1) * window_seconds)
        
        # Calculate retry_after if blocked
        retry_after = None
        if not allowed:
            # Wait until enough of the previous window has passed
            retry_after = max(1, int(weight * window_seconds) + 1)
        
        self._cleanup_old_windows(identifier, current_window)
        
        return RateLimitResponse(
            allowed=allowed,
            limit=max_requests,
            remaining=remaining,
            reset_timestamp=reset_timestamp,
            retry_after=retry_after
        )
    
    def get_headers(self, response: RateLimitResponse) -> Dict[str, str]:
        """Generate standard rate limit headers."""
        headers = {
            "X-RateLimit-Limit": str(response.limit),
            "X-RateLimit-Remaining": str(response.remaining),
            "X-RateLimit-Reset": str(response.reset_timestamp)
        }
        if response.retry_after:
            headers["Retry-After"] = str(response.retry_after)
        return headers


# Test the rate limiter
print("Sliding Window Rate Limiter Test")
print("=" * 50)

limiter = SlidingWindowRateLimiter()

print("\nSimulating 12 requests (limit: 10 per 60 seconds)")
for i in range(12):
    result = limiter.check_rate_limit(
        identifier="192.168.1.100",
        max_requests=10,
        window_seconds=60
    )
    status = "ALLOWED" if result.allowed else "BLOCKED"
    headers = limiter.get_headers(result)
    print(f"Request {i+1:2d}: {status} | Remaining: {result.remaining} | Reset: {result.reset_timestamp}")
    if result.retry_after:
        print(f"           Retry-After: {result.retry_after} seconds")

print("\nDifferent user (should have fresh limit):")
result = limiter.check_rate_limit("192.168.1.200", 10, 60)
print(f"New user request: {'ALLOWED' if result.allowed else 'BLOCKED'} | Remaining: {result.remaining}")

print("\nRate limiter implementation complete!")