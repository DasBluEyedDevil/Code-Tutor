---
type: "EXAMPLE"
title: "Implementing PKCE"
---

**Adding PKCE to the Authorization Code Flow:**

```python
import secrets
import hashlib
import base64
from urllib.parse import urlencode

class PKCEHelper:
    """
    PKCE (Proof Key for Code Exchange) implementation.
    Required for OAuth2 with public clients (mobile, SPA).
    """
    
    @staticmethod
    def generate_code_verifier(length: int = 64) -> str:
        """
        Generate a random code verifier.
        Must be 43-128 characters, using unreserved URI characters.
        """
        # Use URL-safe random bytes
        return secrets.token_urlsafe(length)[:length]
    
    @staticmethod
    def generate_code_challenge(verifier: str) -> str:
        """
        Generate code challenge from verifier using S256 method.
        challenge = BASE64URL(SHA256(verifier))
        """
        # SHA256 hash of the verifier
        digest = hashlib.sha256(verifier.encode('ascii')).digest()
        
        # Base64 URL encode (no padding)
        challenge = base64.urlsafe_b64encode(digest).decode('ascii')
        return challenge.rstrip('=')
    
    @staticmethod
    def verify_challenge(verifier: str, challenge: str) -> bool:
        """
        Verify that verifier matches the challenge.
        Used by authorization server (shown for understanding).
        """
        expected = PKCEHelper.generate_code_challenge(verifier)
        return secrets.compare_digest(expected, challenge)

class OAuth2PKCEClient:
    """OAuth2 client with PKCE support for public clients."""
    
    def __init__(self, client_id: str, authorize_url: str, 
                 token_url: str, redirect_uri: str):
        self.client_id = client_id
        self.authorize_url = authorize_url
        self.token_url = token_url
        self.redirect_uri = redirect_uri
        
        # Store verifiers for pending authorizations
        self.pending_auth = {}  # state -> code_verifier
    
    def start_authorization(self, scopes: list) -> tuple[str, str]:
        """
        Start OAuth2 flow with PKCE.
        Returns (authorization_url, state)
        """
        # Generate PKCE values
        code_verifier = PKCEHelper.generate_code_verifier()
        code_challenge = PKCEHelper.generate_code_challenge(code_verifier)
        
        # Generate state for CSRF protection
        state = secrets.token_urlsafe(32)
        
        # Store verifier for later use
        self.pending_auth[state] = code_verifier
        
        # Build authorization URL
        params = {
            "client_id": self.client_id,
            "redirect_uri": self.redirect_uri,
            "response_type": "code",
            "scope": " ".join(scopes),
            "state": state,
            "code_challenge": code_challenge,
            "code_challenge_method": "S256"
        }
        
        url = f"{self.authorize_url}?{urlencode(params)}"
        return url, state
    
    def complete_authorization(self, code: str, state: str) -> dict:
        """
        Complete OAuth2 flow by exchanging code for tokens.
        Uses stored code_verifier for PKCE.
        """
        # Retrieve and remove stored verifier
        if state not in self.pending_auth:
            raise ValueError("Invalid state - possible CSRF attack")
        
        code_verifier = self.pending_auth.pop(state)
        
        # Token request includes code_verifier (NOT challenge)
        token_request = {
            "grant_type": "authorization_code",
            "code": code,
            "redirect_uri": self.redirect_uri,
            "client_id": self.client_id,
            "code_verifier": code_verifier  # PKCE verifier
            # Note: No client_secret needed for public clients!
        }
        
        print(f"Token request body:")
        for key, value in token_request.items():
            if len(str(value)) > 40:
                print(f"  {key}: {str(value)[:40]}...")
            else:
                print(f"  {key}: {value}")
        
        return token_request

# Demonstration
print("PKCE (Proof Key for Code Exchange)")
print("=" * 50)

# Show PKCE generation
print("\n1. Generate PKCE Values:")
verifier = PKCEHelper.generate_code_verifier()
challenge = PKCEHelper.generate_code_challenge(verifier)
print(f"   Code Verifier: {verifier[:50]}...")
print(f"   Code Challenge: {challenge}")
print(f"   Challenge Length: {len(challenge)} chars")

# Verify the math works
print("\n2. Verify Challenge Matches Verifier:")
is_valid = PKCEHelper.verify_challenge(verifier, challenge)
print(f"   Valid: {is_valid}")

# Wrong verifier should fail
wrong_verifier = PKCEHelper.generate_code_verifier()
is_valid_wrong = PKCEHelper.verify_challenge(wrong_verifier, challenge)
print(f"   Wrong verifier: {is_valid_wrong}")

# Full flow demonstration
print("\n" + "=" * 50)
print("3. Complete PKCE Flow:")

client = OAuth2PKCEClient(
    client_id="finance-tracker-mobile",
    authorize_url="https://accounts.google.com/o/oauth2/v2/auth",
    token_url="https://oauth2.googleapis.com/token",
    redirect_uri="com.financetracker://callback"
)

# Start flow
auth_url, state = client.start_authorization(["openid", "email"])
print(f"\nAuthorization URL (partial):")
print(f"   {auth_url[:80]}...")
print(f"   Contains code_challenge and code_challenge_method=S256")

# Complete flow (simulated callback)
print("\nToken exchange:")
client.complete_authorization(code="AUTH_CODE", state=state)
```
