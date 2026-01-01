import secrets
import html
from typing import Dict, Optional, Callable
from dataclasses import dataclass

@dataclass
class Request:
    method: str
    path: str
    headers: Dict[str, str]
    session: Dict[str, str]
    body: Optional[Dict] = None

@dataclass
class Response:
    status_code: int
    body: str
    headers: Dict[str, str]

class SecurityMiddleware:
    def __init__(self, app: Callable):
        self.app = app
        self.csrf_exempt_paths = ["/api/health", "/api/public"]
    
    def add_security_headers(self, response: Response) -> None:
        """Add security headers to response."""
        response.headers["X-Frame-Options"] = "DENY"
        response.headers["X-Content-Type-Options"] = "nosniff"
        response.headers["Content-Security-Policy"] = "default-src 'self'; script-src 'self'; frame-ancestors 'none'"
    
    def generate_csrf_token(self, request: Request) -> str:
        """Generate and store CSRF token in session."""
        token = secrets.token_urlsafe(32)
        request.session["csrf_token"] = token
        return token
    
    def validate_csrf_token(self, request: Request) -> bool:
        """Validate CSRF token for state-changing requests."""
        # Skip for safe methods
        if request.method in ("GET", "HEAD", "OPTIONS"):
            return True
        
        # Skip for exempt paths
        if request.path in self.csrf_exempt_paths:
            return True
        
        # Get session token
        session_token = request.session.get("csrf_token", "")
        if not session_token:
            return False
        
        # Get request token from header or body
        request_token = request.headers.get("X-CSRF-Token", "")
        if not request_token and request.body:
            request_token = request.body.get("csrf_token", "")
        
        # Constant-time comparison to prevent timing attacks
        return secrets.compare_digest(session_token, request_token)
    
    @staticmethod
    def sanitize_output(content: str) -> str:
        """Sanitize user content for safe HTML output."""
        return html.escape(content)
    
    def __call__(self, request: Request) -> Response:
        """Process request through security middleware."""
        # Validate CSRF for unsafe methods
        if not self.validate_csrf_token(request):
            return Response(403, '{"error": "CSRF validation failed"}', {})
        
        # Call the wrapped application
        response = self.app(request)
        
        # Add security headers
        self.add_security_headers(response)
        
        return response

# Simple app for testing
def sample_app(request: Request) -> Response:
    if request.path == "/api/transfer":
        return Response(200, '{"status": "transferred"}', {})
    return Response(200, '{"status": "ok"}', {})

# Tests
print("Security Middleware Tests")
print("=" * 40)

middleware = SecurityMiddleware(sample_app)

# Test 1: Security headers added
req = Request("GET", "/api/data", {}, {})
resp = middleware(req)
print(f"X-Frame-Options: {resp.headers.get('X-Frame-Options')}")
print(f"X-Content-Type-Options: {resp.headers.get('X-Content-Type-Options')}")
assert resp.headers.get("X-Frame-Options") == "DENY"
assert resp.headers.get("X-Content-Type-Options") == "nosniff"
print("Headers added correctly")

# Test 2: CSRF token generation
req = Request("GET", "/form", {}, {})
token = middleware.generate_csrf_token(req)
print(f"\nGenerated CSRF token: {token[:20]}...")
assert len(token) > 20
assert req.session.get("csrf_token") == token
print("CSRF token generated and stored")

# Test 3: CSRF validation passes with valid token
req = Request("POST", "/api/transfer", {"X-CSRF-Token": token}, {"csrf_token": token})
assert middleware.validate_csrf_token(req) == True
print("\nValid CSRF token accepted")

# Test 4: CSRF validation fails with invalid token
req = Request("POST", "/api/transfer", {"X-CSRF-Token": "wrong"}, {"csrf_token": token})
assert middleware.validate_csrf_token(req) == False
print("Invalid CSRF token rejected")

# Test 5: CSRF skipped for GET
req = Request("GET", "/api/data", {}, {})
assert middleware.validate_csrf_token(req) == True
print("GET requests skip CSRF validation")

# Test 6: Output sanitization
dangerous = '<script>alert("xss")</script>'
safe = SecurityMiddleware.sanitize_output(dangerous)
assert "<script>" not in safe
assert "&lt;script&gt;" in safe
print(f"\nSanitized: {safe}")

print("\nAll security tests passed!")