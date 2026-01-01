---
type: "EXAMPLE"
title: "Session Implementation with Redis"
---



```python
import redis
import secrets
import json
from datetime import datetime, timedelta
from typing import Optional, Dict, Any

class RedisSessionStore:
    def __init__(self, redis_url: str = "redis://localhost:6379"):
        self.redis = redis.from_url(redis_url)
        self.session_ttl = 3600 * 24  # 24 hours
    
    def create_session(self, user_id: int, user_data: Dict[str, Any]) -> str:
        """Create new session and return session ID."""
        session_id = secrets.token_urlsafe(32)
        session_data = {
            "user_id": user_id,
            "user_data": user_data,
            "created_at": datetime.now().isoformat(),
            "last_accessed": datetime.now().isoformat()
        }
        self.redis.setex(
            f"session:{session_id}",
            self.session_ttl,
            json.dumps(session_data)
        )
        return session_id
    
    def get_session(self, session_id: str) -> Optional[Dict[str, Any]]:
        """Retrieve and refresh session."""
        key = f"session:{session_id}"
        data = self.redis.get(key)
        if not data:
            return None
        
        session = json.loads(data)
        # Refresh TTL on access
        session["last_accessed"] = datetime.now().isoformat()
        self.redis.setex(key, self.session_ttl, json.dumps(session))
        return session
    
    def destroy_session(self, session_id: str) -> bool:
        """Delete session (logout)."""
        return self.redis.delete(f"session:{session_id}") > 0

# Usage in Flask-style pseudocode:
# @app.post("/login")
# def login():
#     user = authenticate(request.json)
#     session_id = store.create_session(user.id, {"email": user.email})
#     response.set_cookie("session_id", session_id, httponly=True, secure=True)
```
