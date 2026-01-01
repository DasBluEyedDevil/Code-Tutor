---
type: "EXAMPLE"
title: "Defense in Depth for User Registration"
---

**Multiple security layers working together:**

```python
import re
import secrets
from datetime import datetime, timedelta
from typing import Optional

# Layer 1: Input Validation
def validate_registration_input(email: str, password: str) -> tuple[bool, str]:
    """First line of defense: Validate all input"""
    # Email format validation
    email_pattern = r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$'
    if not re.match(email_pattern, email):
        return False, "Invalid email format"
    
    # Password strength validation
    if len(password) < 12:
        return False, "Password must be at least 12 characters"
    if not re.search(r'[A-Z]', password):
        return False, "Password must contain uppercase letter"
    if not re.search(r'[a-z]', password):
        return False, "Password must contain lowercase letter"
    if not re.search(r'\d', password):
        return False, "Password must contain a digit"
    
    return True, "Valid"

# Layer 2: Rate Limiting (pseudo-code representation)
class RateLimiter:
    """Second line of defense: Prevent brute force attacks"""
    def __init__(self):
        self.attempts = {}  # In production: use Redis
    
    def check_rate_limit(self, ip_address: str, max_attempts: int = 5, 
                         window_minutes: int = 15) -> bool:
        """Returns True if request is allowed"""
        now = datetime.now()
        key = f"register:{ip_address}"
        
        if key not in self.attempts:
            self.attempts[key] = []
        
        # Remove old attempts
        cutoff = now - timedelta(minutes=window_minutes)
        self.attempts[key] = [t for t in self.attempts[key] if t > cutoff]
        
        if len(self.attempts[key]) >= max_attempts:
            return False
        
        self.attempts[key].append(now)
        return True

# Layer 3: Secure Token Generation
def generate_verification_token() -> str:
    """Third line of defense: Cryptographically secure tokens"""
    return secrets.token_urlsafe(32)

# Layer 4: Audit Logging
def log_security_event(event_type: str, details: dict) -> None:
    """Fourth line of defense: Track all security events"""
    timestamp = datetime.now().isoformat()
    print(f"[SECURITY] {timestamp} | {event_type} | {details}")

# Complete registration with all layers
def register_user_secure(email: str, password: str, ip_address: str):
    """Registration with defense in depth"""
    rate_limiter = RateLimiter()
    
    # Layer 2: Check rate limit first (cheap operation)
    if not rate_limiter.check_rate_limit(ip_address):
        log_security_event("RATE_LIMIT_EXCEEDED", {"ip": ip_address})
        return {"error": "Too many attempts. Try again later."}
    
    # Layer 1: Validate input
    is_valid, message = validate_registration_input(email, password)
    if not is_valid:
        log_security_event("VALIDATION_FAILED", {"email": email, "reason": message})
        return {"error": message}
    
    # Layer 3: Generate secure verification token
    token = generate_verification_token()
    
    # Layer 4: Log successful registration attempt
    log_security_event("REGISTRATION_INITIATED", {"email": email})
    
    print(f"Registration successful for {email}")
    print(f"Verification token: {token[:16]}...")
    return {"success": True, "message": "Check your email to verify"}

# Test the secure registration
print("Testing Defense in Depth:")
print("="*50)
register_user_secure("alice@example.com", "SecurePass123!", "192.168.1.1")
```
