from dataclasses import dataclass
from typing import Optional, Dict, Any, List
from datetime import datetime

@dataclass
class User:
    id: int
    email: str
    name: str
    created_at: datetime

@dataclass  
class SocialLink:
    user_id: int
    provider: str
    provider_id: str

class MockDatabase:
    """Simulated database"""
    def __init__(self):
        self.users: Dict[int, User] = {}
        self.social_links: List[SocialLink] = []
        self.next_id = 1
    
    def get_user_by_id(self, user_id: int) -> Optional[User]:
        return self.users.get(user_id)

class SocialAuthService:
    def __init__(self, db: MockDatabase):
        self.db = db
    
    def find_user_by_social(self, provider: str, provider_id: str) -> Optional[User]:
        """Find user by social provider and ID."""
        for link in self.db.social_links:
            if link.provider == provider and link.provider_id == provider_id:
                return self.db.get_user_by_id(link.user_id)
        return None
    
    def find_user_by_email(self, email: str) -> Optional[User]:
        """Find user by email (case-insensitive)."""
        email_lower = email.lower()
        for user in self.db.users.values():
            if user.email.lower() == email_lower:
                return user
        return None
    
    def create_user(self, email: str, name: str) -> User:
        """Create a new user."""
        user = User(
            id=self.db.next_id,
            email=email.lower(),
            name=name,
            created_at=datetime.now()
        )
        self.db.users[user.id] = user
        self.db.next_id += 1
        return user
    
    def link_social_account(self, user_id: int, provider: str, provider_id: str):
        """Link a social account to a user."""
        link = SocialLink(
            user_id=user_id,
            provider=provider,
            provider_id=provider_id
        )
        self.db.social_links.append(link)
    
    def handle_social_login(self, provider: str, 
                            social_data: Dict[str, Any]) -> Dict[str, Any]:
        """
        Process social login from OAuth callback.
        """
        provider_id = social_data["provider_id"]
        email = social_data["email"]
        name = social_data["name"]
        
        # Step 1: Check if social account exists
        user = self.find_user_by_social(provider, provider_id)
        if user:
            return {
                "success": True,
                "is_new_user": False,
                "user": {"id": user.id, "email": user.email, "name": user.name}
            }
        
        # Step 2: Check if email exists (link account)
        user = self.find_user_by_email(email)
        if user:
            self.link_social_account(user.id, provider, provider_id)
            return {
                "success": True,
                "is_new_user": False,
                "user": {"id": user.id, "email": user.email, "name": user.name}
            }
        
        # Step 3: Create new user
        user = self.create_user(email, name)
        self.link_social_account(user.id, provider, provider_id)
        
        return {
            "success": True,
            "is_new_user": True,
            "user": {"id": user.id, "email": user.email, "name": user.name}
        }

# Test the social auth service
db = MockDatabase()
auth = SocialAuthService(db)

print("Social Authentication Tests")
print("=" * 50)

# Test 1: New user signup
print("\n1. New User - Google Signup:")
result = auth.handle_social_login("google", {
    "provider_id": "g-12345",
    "email": "alice@example.com",
    "name": "Alice"
})
print(f"   Is new: {result['is_new_user']}, ID: {result['user']['id']}")

# Test 2: Same user returns
print("\n2. Returning User - Google:")
result = auth.handle_social_login("google", {
    "provider_id": "g-12345",
    "email": "alice@example.com",
    "name": "Alice"
})
print(f"   Is new: {result['is_new_user']}, ID: {result['user']['id']}")

# Test 3: Same email, different provider (link)
print("\n3. Link GitHub to Existing:")
result = auth.handle_social_login("github", {
    "provider_id": "gh-99999",
    "email": "alice@example.com",
    "name": "alice-gh"
})
print(f"   Is new: {result['is_new_user']}, ID: {result['user']['id']}")

# Test 4: Login via newly linked GitHub
print("\n4. Login via Linked GitHub:")
result = auth.handle_social_login("github", {
    "provider_id": "gh-99999",
    "email": "alice@example.com",
    "name": "alice-gh"
})
print(f"   Is new: {result['is_new_user']}, ID: {result['user']['id']}")

# Test 5: Completely new user
print("\n5. Different User - New:")
result = auth.handle_social_login("google", {
    "provider_id": "g-67890",
    "email": "bob@example.com",
    "name": "Bob"
})
print(f"   Is new: {result['is_new_user']}, ID: {result['user']['id']}")

print("\nAll tests completed!")