---
type: "EXAMPLE"
title: "FastAPI Rate Limiter with Redis"
---

**Production-ready rate limiting using Redis for distributed state:**

**Installation:**
```bash
pip install redis fastapi slowapi
```

```python
import time
import redis
from typing import Optional, Tuple
from dataclasses import dataclass
from enum import Enum

class RateLimitAlgorithm(Enum):
    FIXED_WINDOW = "fixed_window"
    SLIDING_WINDOW = "sliding_window"
    TOKEN_BUCKET = "token_bucket"

@dataclass
class RateLimitResult:
    allowed: bool
    remaining: int
    reset_time: int
    retry_after: Optional[int] = None

class RedisRateLimiter:
    """
    Production-ready rate limiter using Redis.
    Supports multiple algorithms for different use cases.
    """
    
    def __init__(self, redis_client: redis.Redis, prefix: str = "ratelimit"):
        self.redis = redis_client
        self.prefix = prefix
    
    def _key(self, identifier: str, endpoint: str) -> str:
        """Generate Redis key for rate limit tracking."""
        return f"{self.prefix}:{endpoint}:{identifier}"
    
    def fixed_window(
        self,
        identifier: str,
        endpoint: str,
        max_requests: int,
        window_seconds: int
    ) -> RateLimitResult:
        """
        Fixed window rate limiting.
        Simple and efficient, but allows bursts at window boundaries.
        """
        key = self._key(identifier, endpoint)
        current_window = int(time.time() // window_seconds)
        window_key = f"{key}:{current_window}"
        
        # Atomic increment with expiry
        pipe = self.redis.pipeline()
        pipe.incr(window_key)
        pipe.expire(window_key, window_seconds)
        results = pipe.execute()
        
        current_count = results[0]
        remaining = max(0, max_requests - current_count)
        reset_time = (current_window + 1) * window_seconds
        
        if current_count > max_requests:
            return RateLimitResult(
                allowed=False,
                remaining=0,
                reset_time=reset_time,
                retry_after=reset_time - int(time.time())
            )
        
        return RateLimitResult(
            allowed=True,
            remaining=remaining,
            reset_time=reset_time
        )
    
    def sliding_window(
        self,
        identifier: str,
        endpoint: str,
        max_requests: int,
        window_seconds: int
    ) -> RateLimitResult:
        """
        Sliding window counter - weighted average of two windows.
        More accurate than fixed window with minimal overhead.
        """
        key = self._key(identifier, endpoint)
        now = time.time()
        current_window = int(now // window_seconds)
        previous_window = current_window - 1
        
        # Keys for current and previous windows
        current_key = f"{key}:{current_window}"
        previous_key = f"{key}:{previous_window}"
        
        # Get counts from both windows
        pipe = self.redis.pipeline()
        pipe.get(previous_key)
        pipe.incr(current_key)
        pipe.expire(current_key, window_seconds * 2)
        results = pipe.execute()
        
        previous_count = int(results[0] or 0)
        current_count = results[1]
        
        # Calculate weighted count
        elapsed_in_window = now - (current_window * window_seconds)
        weight = 1 - (elapsed_in_window / window_seconds)
        weighted_count = (previous_count * weight) + current_count
        
        remaining = max(0, int(max_requests - weighted_count))
        reset_time = int((current_window + 1) * window_seconds)
        
        if weighted_count > max_requests:
            return RateLimitResult(
                allowed=False,
                remaining=0,
                reset_time=reset_time,
                retry_after=int(window_seconds * weight) + 1
            )
        
        return RateLimitResult(
            allowed=True,
            remaining=remaining,
            reset_time=reset_time
        )
    
    def token_bucket(
        self,
        identifier: str,
        endpoint: str,
        bucket_size: int,
        refill_rate: float,  # tokens per second
        tokens_required: int = 1
    ) -> RateLimitResult:
        """
        Token bucket algorithm - allows controlled bursts.
        Ideal for APIs that can handle occasional traffic spikes.
        """
        key = self._key(identifier, endpoint)
        now = time.time()
        
        # Lua script for atomic token bucket operation
        lua_script = """
        local key = KEYS[1]
        local bucket_size = tonumber(ARGV[1])
        local refill_rate = tonumber(ARGV[2])
        local tokens_required = tonumber(ARGV[3])
        local now = tonumber(ARGV[4])
        
        local bucket = redis.call('HMGET', key, 'tokens', 'last_refill')
        local tokens = tonumber(bucket[1]) or bucket_size
        local last_refill = tonumber(bucket[2]) or now
        
        -- Calculate tokens to add based on time elapsed
        local elapsed = now - last_refill
        local tokens_to_add = elapsed * refill_rate
        tokens = math.min(bucket_size, tokens + tokens_to_add)
        
        local allowed = 0
        if tokens >= tokens_required then
            tokens = tokens - tokens_required
            allowed = 1
        end
        
        -- Save state
        redis.call('HMSET', key, 'tokens', tokens, 'last_refill', now)
        redis.call('EXPIRE', key, 3600)  -- Expire after 1 hour of inactivity
        
        return {allowed, math.floor(tokens)}
        """
        
        result = self.redis.eval(
            lua_script, 1, key,
            bucket_size, refill_rate, tokens_required, now
        )
        
        allowed = result[0] == 1
        remaining = result[1]
        
        # Calculate when bucket will have tokens again
        if not allowed:
            seconds_until_token = (tokens_required - remaining) / refill_rate
            retry_after = int(seconds_until_token) + 1
        else:
            retry_after = None
        
        return RateLimitResult(
            allowed=allowed,
            remaining=remaining,
            reset_time=int(now + (bucket_size - remaining) / refill_rate),
            retry_after=retry_after
        )

# Demonstration with mock Redis
class MockRedis:
    """Simple mock for demonstration purposes."""
    def __init__(self):
        self.data = {}
        self.expiry = {}
    
    def pipeline(self):
        return MockPipeline(self)
    
    def get(self, key):
        return self.data.get(key)
    
    def incr(self, key):
        self.data[key] = self.data.get(key, 0) + 1
        return self.data[key]
    
    def expire(self, key, seconds):
        self.expiry[key] = time.time() + seconds
        return True
    
    def hmget(self, key, *fields):
        if key not in self.data:
            return [None] * len(fields)
        return [self.data[key].get(f) for f in fields]
    
    def hmset(self, key, mapping):
        if key not in self.data:
            self.data[key] = {}
        self.data[key].update(mapping)
    
    def eval(self, script, numkeys, *args):
        # Simplified token bucket for demo
        key = args[0]
        bucket_size = float(args[1])
        tokens = self.data.get(key, {}).get('tokens', bucket_size)
        if tokens >= 1:
            tokens -= 1
            self.data[key] = {'tokens': tokens, 'last_refill': time.time()}
            return [1, int(tokens)]
        return [0, 0]

class MockPipeline:
    def __init__(self, redis):
        self.redis = redis
        self.commands = []
    
    def incr(self, key):
        self.commands.append(('incr', key))
        return self
    
    def expire(self, key, seconds):
        self.commands.append(('expire', key, seconds))
        return self
    
    def get(self, key):
        self.commands.append(('get', key))
        return self
    
    def execute(self):
        results = []
        for cmd in self.commands:
            if cmd[0] == 'incr':
                results.append(self.redis.incr(cmd[1]))
            elif cmd[0] == 'expire':
                results.append(self.redis.expire(cmd[1], cmd[2]))
            elif cmd[0] == 'get':
                results.append(self.redis.get(cmd[1]))
        return results

# Test the rate limiter
print("Rate Limiter Demonstration")
print("=" * 50)

mock_redis = MockRedis()
limiter = RedisRateLimiter(mock_redis)

# Test fixed window
print("\n1. Fixed Window (5 requests per window):")
for i in range(7):
    result = limiter.fixed_window("user_1", "/api/data", max_requests=5, window_seconds=60)
    status = "ALLOWED" if result.allowed else "BLOCKED"
    print(f"   Request {i+1}: {status} (remaining: {result.remaining})")

# Test token bucket
print("\n2. Token Bucket (10 tokens, refill 2/sec):")
mock_redis2 = MockRedis()
limiter2 = RedisRateLimiter(mock_redis2)
for i in range(12):
    result = limiter2.token_bucket("user_2", "/api/action", bucket_size=10, refill_rate=2.0)
    status = "ALLOWED" if result.allowed else "BLOCKED"
    print(f"   Request {i+1}: {status} (tokens remaining: {result.remaining})")

print("\nRate limiting protects your API from abuse!")
```
