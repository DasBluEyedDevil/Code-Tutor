---
type: "EXAMPLE"
title: "Social Login Service Implementation"
---

**Complete social login handler for Finance Tracker:**

```python
from dataclasses import dataclass, field
from typing import Optional, Dict, Any
from datetime import datetime
import secrets

@dataclass
class User:
    """User model for Finance Tracker"""
    id: int
    email: str
    name: str
    picture_url: Optional[str] = None
    created_at: datetime = field(default_factory=datetime.now)

@dataclass
class SocialAccount:
    """Links user to social provider"""
    user_id: int
    provider: str  # 'google', 'github'
    provider_user_id: str

class UserDatabase:
    """Simulated database for users and social accounts"""
    
    def __init__(self):
        self.users: Dict[int, User] = {}
        self.social_accounts: Dict[str, SocialAccount] = {}  # provider:id -> account
        self.users_by_email: Dict[str, User] = {}
        self.user_counter = 0
    
    def find_by_social(self, provider: str, provider_user_id: str) -> Optional[User]:
        """Find user by social account"""
        key = f"{provider}:{provider_user_id}"
        account = self.social_accounts.get(key)
        if account:
            return self.users.get(account.user_id)
        return None
    
    def find_by_email(self, email: str) -> Optional[User]:
        """Find user by email"""
        return self.users_by_email.get(email.lower())
    
    def create_user(self, email: str, name: str, 
                    picture_url: Optional[str] = None) -> User:
        """Create a new user"""
        self.user_counter += 1
        user = User(
            id=self.user_counter,
            email=email.lower(),
            name=name,
            picture_url=picture_url
        )
        self.users[user.id] = user
        self.users_by_email[user.email] = user
        return user
    
    def link_social_account(self, user_id: int, provider: str, 
                            provider_user_id: str):
        """Link a social account to a user"""
        key = f"{provider}:{provider_user_id}"
        self.social_accounts[key] = SocialAccount(
            user_id=user_id,
            provider=provider,
            provider_user_id=provider_user_id
        )

class SocialLoginService:
    """
    Handles social login flow for Finance Tracker.
    Supports Google and GitHub.
    """
    
    def __init__(self, db: UserDatabase):
        self.db = db
    
    def process_social_login(self, provider: str, 
                             id_token_claims: Dict[str, Any]) -> Dict[str, Any]:
        """
        Process validated ID token from OAuth provider.
        Returns user info and session token.
        
        Args:
            provider: 'google' or 'github'
            id_token_claims: Validated claims from ID token
                - sub: Provider's user ID
                - email: User's email
                - name: User's display name
                - picture: Profile picture URL (optional)
        """
        provider_user_id = id_token_claims["sub"]
        email = id_token_claims["email"]
        name = id_token_claims.get("name", email.split("@")[0])
        picture = id_token_claims.get("picture")
        
        # Step 1: Check if social account already linked
        user = self.db.find_by_social(provider, provider_user_id)
        
        if user:
            print(f"Found existing user via {provider} account")
            return self._create_session(user, is_new=False)
        
        # Step 2: Check if email already exists (link accounts)
        user = self.db.find_by_email(email)
        
        if user:
            print(f"Linking {provider} to existing account with email {email}")
            self.db.link_social_account(user.id, provider, provider_user_id)
            return self._create_session(user, is_new=False)
        
        # Step 3: Create new user
        print(f"Creating new user from {provider} login")
        user = self.db.create_user(
            email=email,
            name=name,
            picture_url=picture
        )
        self.db.link_social_account(user.id, provider, provider_user_id)
        
        return self._create_session(user, is_new=True)
    
    def _create_session(self, user: User, is_new: bool) -> Dict[str, Any]:
        """Create session for authenticated user"""
        # In real app: Create JWT tokens
        session_token = secrets.token_urlsafe(32)
        
        return {
            "success": True,
            "is_new_user": is_new,
            "user": {
                "id": user.id,
                "email": user.email,
                "name": user.name,
                "picture": user.picture_url
            },
            "session_token": session_token
        }

# Demonstration
print("Social Login Flow - Finance Tracker")
print("=" * 50)

db = UserDatabase()
login_service = SocialLoginService(db)

# Scenario 1: New user signs up with Google
print("\n1. New User - Google Sign Up:")
google_claims = {
    "sub": "google-user-12345",
    "email": "alice@gmail.com",
    "name": "Alice Smith",
    "picture": "https://lh3.googleusercontent.com/...",
    "email_verified": True
}
result = login_service.process_social_login("google", google_claims)
print(f"   Is new user: {result['is_new_user']}")
print(f"   User ID: {result['user']['id']}")
print(f"   Name: {result['user']['name']}")

# Scenario 2: Same user logs in again with Google
print("\n2. Returning User - Google Login:")
result = login_service.process_social_login("google", google_claims)
print(f"   Is new user: {result['is_new_user']}")
print(f"   User ID: {result['user']['id']} (same as before)")

# Scenario 3: Same user links GitHub account
print("\n3. Link GitHub to Existing Account:")
github_claims = {
    "sub": "github-user-67890",
    "email": "alice@gmail.com",  # Same email
    "name": "alice-github"
}
result = login_service.process_social_login("github", github_claims)
print(f"   Is new user: {result['is_new_user']}")
print(f"   User ID: {result['user']['id']} (linked to same user!)")

# Scenario 4: User logs in via GitHub (now linked)
print("\n4. Login via Linked GitHub:")
result = login_service.process_social_login("github", github_claims)
print(f"   Is new user: {result['is_new_user']}")
print(f"   User ID: {result['user']['id']}")

# Scenario 5: Different user with different email
print("\n5. New User - Different Email:")
bob_claims = {
    "sub": "google-user-99999",
    "email": "bob@gmail.com",
    "name": "Bob Jones"
}
result = login_service.process_social_login("google", bob_claims)
print(f"   Is new user: {result['is_new_user']}")
print(f"   User ID: {result['user']['id']} (new user, different ID)")
```
