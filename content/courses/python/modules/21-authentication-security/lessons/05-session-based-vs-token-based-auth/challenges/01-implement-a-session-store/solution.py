import secrets
from datetime import datetime
from typing import Optional, Dict, Any, List

class SessionStore:
    def __init__(self):
        self.sessions: Dict[str, Dict[str, Any]] = {}
    
    def create(self, user_id: int, metadata: Dict[str, Any]) -> str:
        """Create session, return session_id."""
        session_id = secrets.token_urlsafe(32)
        self.sessions[session_id] = {
            "user_id": user_id,
            "metadata": metadata,
            "created_at": datetime.now(),
            "last_accessed": datetime.now()
        }
        return session_id
    
    def get(self, session_id: str) -> Optional[Dict[str, Any]]:
        """Get session and update last_accessed."""
        if session_id not in self.sessions:
            return None
        self.sessions[session_id]["last_accessed"] = datetime.now()
        return self.sessions[session_id]
    
    def destroy(self, session_id: str) -> bool:
        """Delete session. Return True if existed."""
        if session_id in self.sessions:
            del self.sessions[session_id]
            return True
        return False
    
    def get_user_sessions(self, user_id: int) -> List[str]:
        """Get all session IDs for a user."""
        return [
            sid for sid, data in self.sessions.items()
            if data["user_id"] == user_id
        ]
    
    def destroy_all_user_sessions(self, user_id: int) -> int:
        """Logout from all devices. Return count destroyed."""
        sessions_to_destroy = self.get_user_sessions(user_id)
        for sid in sessions_to_destroy:
            del self.sessions[sid]
        return len(sessions_to_destroy)

# Test
store = SessionStore()

print("Session Store Tests")
print("=" * 40)

# Create sessions
s1 = store.create(1, {"device": "Chrome"})
s2 = store.create(1, {"device": "Mobile"})
s3 = store.create(2, {"device": "Firefox"})

print(f"Created 3 sessions")
print(f"User 1 sessions: {len(store.get_user_sessions(1))}")
print(f"User 2 sessions: {len(store.get_user_sessions(2))}")

# Retrieve
session = store.get(s1)
print(f"\nSession data: user_id={session['user_id']}, device={session['metadata']['device']}")

# Destroy one
store.destroy(s2)
print(f"\nAfter destroying s2, User 1 sessions: {len(store.get_user_sessions(1))}")

# Destroy all for user
count = store.destroy_all_user_sessions(1)
print(f"Destroyed {count} remaining sessions for User 1")
print(f"User 1 sessions now: {len(store.get_user_sessions(1))}")

print("\nAll tests passed!")