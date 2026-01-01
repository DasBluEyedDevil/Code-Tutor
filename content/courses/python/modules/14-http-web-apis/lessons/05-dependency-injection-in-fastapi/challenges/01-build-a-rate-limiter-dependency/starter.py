from fastapi import FastAPI, Depends, HTTPException
from datetime import datetime, timedelta
from typing import Dict

app = FastAPI()

# TODO: Create RateLimiter class
# - __init__(max_requests: int, time_window: int) - window in seconds
# - check_rate_limit(user_id: str) -> bool - returns True if allowed
# - get_request_count(user_id: str) -> int

class RateLimiter:
    def __init__(self, max_requests: int = 10, time_window: int = 60):
        pass  # Store config and create request tracking dict
    
    def check_rate_limit(self, user_id: str) -> bool:
        pass  # Check if user is within limit
    
    def get_request_count(self, user_id: str) -> int:
        pass  # Return current count for user

# Create rate limiter instance
rate_limiter = RateLimiter(max_requests=5, time_window=60)

# TODO: Create get_current_user dependency
# Returns: {"user_id": "user123", "name": "Test User"}

def get_current_user():
    pass

# TODO: Create rate_limit dependency
# - Depends on get_current_user
# - Checks rate limit, raises HTTPException(429) if exceeded
# - Returns user if allowed

def rate_limit(user = Depends(get_current_user)):
    pass

# TODO: Create endpoints
# GET /public - No rate limiting, returns {"message": "Public endpoint"}
# GET /api/data - Rate limited, returns {"data": "Secret data", "user": user["name"]}
# GET /api/status - Rate limited, returns request count for user

@app.get("/public")
async def public_endpoint():
    pass

@app.get("/api/data")
async def get_data(user = Depends(rate_limit)):
    pass

@app.get("/api/status")
async def get_status(user = Depends(rate_limit)):
    pass