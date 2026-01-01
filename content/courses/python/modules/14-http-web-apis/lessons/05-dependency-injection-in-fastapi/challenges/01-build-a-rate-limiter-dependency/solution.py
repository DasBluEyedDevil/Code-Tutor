from fastapi import FastAPI, Depends, HTTPException
from datetime import datetime, timedelta
from typing import Dict, List

app = FastAPI()

class RateLimiter:
    """Rate limiter using sliding window."""
    
    def __init__(self, max_requests: int = 10, time_window: int = 60):
        self.max_requests = max_requests
        self.time_window = time_window  # seconds
        self.requests: Dict[str, List[datetime]] = {}
    
    def _clean_old_requests(self, user_id: str) -> None:
        """Remove requests outside the time window."""
        if user_id not in self.requests:
            return
        
        cutoff = datetime.now() - timedelta(seconds=self.time_window)
        self.requests[user_id] = [
            req_time for req_time in self.requests[user_id]
            if req_time > cutoff
        ]
    
    def check_rate_limit(self, user_id: str) -> bool:
        """Check if user is within rate limit. Records request if allowed."""
        self._clean_old_requests(user_id)
        
        if user_id not in self.requests:
            self.requests[user_id] = []
        
        if len(self.requests[user_id]) >= self.max_requests:
            return False
        
        # Record this request
        self.requests[user_id].append(datetime.now())
        return True
    
    def get_request_count(self, user_id: str) -> int:
        """Get current request count for user."""
        self._clean_old_requests(user_id)
        return len(self.requests.get(user_id, []))

# Create rate limiter instance
rate_limiter = RateLimiter(max_requests=5, time_window=60)

def get_current_user():
    """Simulated user authentication."""
    # In real app, would extract from JWT token or session
    return {"user_id": "user123", "name": "Test User"}

def rate_limit(user = Depends(get_current_user)):
    """Rate limiting dependency."""
    if not rate_limiter.check_rate_limit(user["user_id"]):
        raise HTTPException(
            status_code=429,
            detail=f"Rate limit exceeded. Max {rate_limiter.max_requests} requests per {rate_limiter.time_window} seconds."
        )
    return user

@app.get("/public")
async def public_endpoint():
    """Public endpoint - no rate limiting."""
    return {"message": "Public endpoint"}

@app.get("/api/data")
async def get_data(user = Depends(rate_limit)):
    """Rate limited endpoint."""
    return {
        "data": "Secret data",
        "user": user["name"]
    }

@app.get("/api/status")
async def get_status(user = Depends(rate_limit)):
    """Get rate limit status."""
    return {
        "user": user["name"],
        "requests_made": rate_limiter.get_request_count(user["user_id"]),
        "max_requests": rate_limiter.max_requests,
        "time_window_seconds": rate_limiter.time_window
    }

# Demonstration
if __name__ == "__main__":
    print("=== Rate Limiter Dependency Demo ===")
    
    print("\n1. Testing rate limiter:")
    user = get_current_user()
    print(f"   User: {user['name']}")
    
    print("\n2. Making requests within limit:")
    for i in range(5):
        allowed = rate_limiter.check_rate_limit(user["user_id"])
        count = rate_limiter.get_request_count(user["user_id"])
        print(f"   Request {i+1}: {'Allowed' if allowed else 'Blocked'} (count: {count})")
    
    print("\n3. Request exceeding limit:")
    allowed = rate_limiter.check_rate_limit(user["user_id"])
    print(f"   Request 6: {'Allowed' if allowed else 'Blocked - 429 Too Many Requests'}")
    
    print("\nRun with: uvicorn solution:app --reload")