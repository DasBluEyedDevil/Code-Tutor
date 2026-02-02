---
type: "EXAMPLE"
title: "Authorization Code Flow - Step by Step"
---

**The most secure OAuth2 flow for web applications:**

```python
# OAuth2 Authorization Code Flow - Conceptual Implementation
# This demonstrates the flow; production apps use libraries like Authlib

import secrets
import hashlib
import base64
from urllib.parse import urlencode, parse_qs
from dataclasses import dataclass
from typing import Optional

@dataclass
class OAuthConfig:
    """OAuth2 configuration for a provider (e.g., Google)"""
    client_id: str
    client_secret: str
    authorize_url: str
    token_url: str
    redirect_uri: str
    scopes: list

# Example configuration for Google
google_config = OAuthConfig(
    client_id="your-client-id.apps.googleusercontent.com",
    client_secret="your-client-secret",  # Keep secret!
    authorize_url="https://accounts.google.com/o/oauth2/v2/auth",
    token_url="https://oauth2.googleapis.com/token",
    redirect_uri="http://localhost:8000/auth/google/callback",
    scopes=["openid", "email", "profile"]
)

class OAuth2Client:
    """Handles OAuth2 Authorization Code Flow"""
    
    def __init__(self, config: OAuthConfig):
        self.config = config
        # Store state tokens to prevent CSRF
        self.pending_states = {}  # state -> created_at
    
    def get_authorization_url(self) -> tuple[str, str]:
        """
        STEP 1: Generate URL to redirect user for authorization.
        Returns (url, state) - store state for verification.
        """
        # Generate random state to prevent CSRF attacks
        state = secrets.token_urlsafe(32)
        self.pending_states[state] = True
        
        params = {
            "client_id": self.config.client_id,
            "redirect_uri": self.config.redirect_uri,
            "response_type": "code",  # Request authorization code
            "scope": " ".join(self.config.scopes),
            "state": state,
            "access_type": "offline",  # Get refresh token
            "prompt": "consent"  # Always show consent screen
        }
        
        url = f"{self.config.authorize_url}?{urlencode(params)}"
        return url, state
    
    def handle_callback(self, code: str, state: str) -> Optional[dict]:
        """
        STEP 2: Handle callback after user authorizes.
        Exchange authorization code for tokens.
        """
        # Verify state to prevent CSRF
        if state not in self.pending_states:
            print("ERROR: Invalid state - possible CSRF attack!")
            return None
        
        # Remove used state
        del self.pending_states[state]
        
        # Exchange code for tokens (in real app, make HTTP POST)
        token_request = {
            "grant_type": "authorization_code",
            "code": code,
            "redirect_uri": self.config.redirect_uri,
            "client_id": self.config.client_id,
            "client_secret": self.config.client_secret
        }
        
        print(f"Would POST to {self.config.token_url}")
        print(f"Request body: {token_request}")
        
        # Simulated response (in real app, parse JSON response)
        return {
            "access_token": "ya29.example-access-token",
            "token_type": "Bearer",
            "expires_in": 3600,
            "refresh_token": "1//example-refresh-token",
            "id_token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9..."
        }

# Demonstration of the flow
print("OAuth2 Authorization Code Flow")
print("=" * 50)

client = OAuth2Client(google_config)

# Step 1: Get authorization URL
print("\nSTEP 1: Generate Authorization URL")
auth_url, state = client.get_authorization_url()
print(f"Redirect user to:\n{auth_url[:80]}...")
print(f"\nStored state: {state[:20]}...")

# Step 2: User authorizes, provider redirects back with code
print("\n" + "="*50)
print("STEP 2: User Authorizes & Provider Redirects Back")
print("User sees: 'Finance Tracker wants to access your email and profile'")
print("User clicks: 'Allow'")
print("\nProvider redirects to:")
print(f"{google_config.redirect_uri}?code=AUTH_CODE_HERE&state={state}")

# Step 3: Exchange code for tokens
print("\n" + "="*50)
print("STEP 3: Exchange Code for Tokens")
tokens = client.handle_callback(code="AUTH_CODE_HERE", state=state)
if tokens:
    print(f"\nReceived tokens:")
    for key, value in tokens.items():
        if isinstance(value, str) and len(value) > 30:
            print(f"  {key}: {value[:30]}...")
        else:
            print(f"  {key}: {value}")

print("\n" + "="*50)
print("STEP 4: Use access_token to call APIs")
print("GET https://www.googleapis.com/oauth2/v2/userinfo")
print("Authorization: Bearer ya29.example-access-token")
```
