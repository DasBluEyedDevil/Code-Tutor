---
type: "EXAMPLE"
title: "Code Example: Implementing Layered Architecture"
---

**Layered architecture explained:**

**1. Models (Data structures):**
- Plain data classes
- No business logic
- Just hold data

**2. Repositories (Data access):**
- CRUD operations only
- Talks to database
- No validation or business rules

**3. Services (Business logic):**
- Validation
- Business rules
- Uses repositories
- Coordinates multiple operations

**4. API/Controllers (HTTP layer):**
- Handle requests/responses
- Call services
- No business logic

**Benefits:**
- Testable: Mock each layer
- Maintainable: Change one layer without affecting others
- Reusable: Services used by API, CLI, tests, etc.
- Clear responsibilities

```python
# Example: Implementing a simple blog with layered architecture

from typing import List, Optional, Dict
from dataclasses import dataclass
from datetime import datetime

print("=== Layer 1: Data Models ===")

@dataclass
class User:
    """User data model"""
    id: int
    username: str
    email: str
    created_at: datetime

@dataclass
class Post:
    """Post data model"""
    id: int
    title: str
    content: str
    author_id: int
    created_at: datetime
    
print("✓ Models defined")

print("\n=== Layer 2: Repository Layer (Data Access) ===")

class UserRepository:
    """Handles all database operations for users"""
    
    def __init__(self):
        # In real app, this would be database connection
        self._users: Dict[int, User] = {}
        self._next_id = 1
    
    def create(self, username: str, email: str) -> User:
        """Create new user"""
        user = User(
            id=self._next_id,
            username=username,
            email=email,
            created_at=datetime.now()
        )
        self._users[user.id] = user
        self._next_id += 1
        return user
    
    def find_by_id(self, user_id: int) -> Optional[User]:
        """Find user by ID"""
        return self._users.get(user_id)
    
    def find_by_username(self, username: str) -> Optional[User]:
        """Find user by username"""
        for user in self._users.values():
            if user.username == username:
                return user
        return None
    
    def list_all(self) -> List[User]:
        """List all users"""
        return list(self._users.values())

class PostRepository:
    """Handles all database operations for posts"""
    
    def __init__(self):
        self._posts: Dict[int, Post] = {}
        self._next_id = 1
    
    def create(self, title: str, content: str, author_id: int) -> Post:
        """Create new post"""
        post = Post(
            id=self._next_id,
            title=title,
            content=content,
            author_id=author_id,
            created_at=datetime.now()
        )
        self._posts[post.id] = post
        self._next_id += 1
        return post
    
    def find_by_id(self, post_id: int) -> Optional[Post]:
        """Find post by ID"""
        return self._posts.get(post_id)
    
    def find_by_author(self, author_id: int) -> List[Post]:
        """Find all posts by author"""
        return [
            post for post in self._posts.values()
            if post.author_id == author_id
        ]
    
    def list_all(self) -> List[Post]:
        """List all posts"""
        return list(self._posts.values())
    
    def delete(self, post_id: int) -> bool:
        """Delete post"""
        if post_id in self._posts:
            del self._posts[post_id]
            return True
        return False

print("✓ Repositories defined")

print("\n=== Layer 3: Service Layer (Business Logic) ===")

class UserService:
    """Business logic for user operations"""
    
    def __init__(self, user_repo: UserRepository):
        self.user_repo = user_repo
    
    def register_user(self, username: str, email: str) -> Dict:
        """Register new user with validation"""
        # Validation
        if len(username) < 3:
            return {"error": "Username must be at least 3 characters"}
        
        if '@' not in email:
            return {"error": "Invalid email format"}
        
        # Check if username exists
        if self.user_repo.find_by_username(username):
            return {"error": "Username already taken"}
        
        # Create user
        user = self.user_repo.create(username, email)
        
        return {
            "success": True,
            "user": {
                "id": user.id,
                "username": user.username,
                "email": user.email
            }
        }
    
    def get_user_profile(self, user_id: int) -> Optional[Dict]:
        """Get user profile"""
        user = self.user_repo.find_by_id(user_id)
        if not user:
            return None
        
        return {
            "id": user.id,
            "username": user.username,
            "email": user.email,
            "member_since": user.created_at.strftime("%Y-%m-%d")
        }

class PostService:
    """Business logic for post operations"""
    
    def __init__(self, post_repo: PostRepository, user_repo: UserRepository):
        self.post_repo = post_repo
        self.user_repo = user_repo
    
    def create_post(self, title: str, content: str, author_id: int) -> Dict:
        """Create new post with validation"""
        # Validation
        if len(title) < 5:
            return {"error": "Title must be at least 5 characters"}
        
        if len(content) < 10:
            return {"error": "Content must be at least 10 characters"}
        
        # Verify author exists
        if not self.user_repo.find_by_id(author_id):
            return {"error": "Author not found"}
        
        # Create post
        post = self.post_repo.create(title, content, author_id)
        
        return {
            "success": True,
            "post": {
                "id": post.id,
                "title": post.title,
                "content": post.content,
                "author_id": post.author_id
            }
        }
    
    def get_post_with_author(self, post_id: int) -> Optional[Dict]:
        """Get post with author information"""
        post = self.post_repo.find_by_id(post_id)
        if not post:
            return None
        
        author = self.user_repo.find_by_id(post.author_id)
        
        return {
            "id": post.id,
            "title": post.title,
            "content": post.content,
            "author": {
                "id": author.id,
                "username": author.username
            } if author else None,
            "created_at": post.created_at.strftime("%Y-%m-%d %H:%M")
        }
    
    def get_user_posts(self, user_id: int) -> List[Dict]:
        """Get all posts by a user"""
        posts = self.post_repo.find_by_author(user_id)
        return [
            {
                "id": post.id,
                "title": post.title,
                "content": post.content[:100] + "..."
            }
            for post in posts
        ]

print("✓ Services defined")

print("\n=== Testing the Architecture ===")

# Initialize layers
user_repo = UserRepository()
post_repo = PostRepository()

user_service = UserService(user_repo)
post_service = PostService(post_repo, user_repo)

# Test user registration
print("\n1. Registering users...")
result1 = user_service.register_user("alice", "alice@example.com")
print(f"   Alice: {result1}")

result2 = user_service.register_user("bob", "bob@example.com")
print(f"   Bob: {result2}")

# Test validation
result3 = user_service.register_user("ab", "invalid")
print(f"   Invalid: {result3}")

# Test duplicate username
result4 = user_service.register_user("alice", "alice2@example.com")
print(f"   Duplicate: {result4}")

# Create posts
print("\n2. Creating posts...")
post1 = post_service.create_post(
    "My First Post",
    "This is the content of my first blog post. Hello world!",
    1  # Alice's ID
)
print(f"   Post 1: {post1}")

post2 = post_service.create_post(
    "Python Tips",
    "Here are some great Python tips for beginners and experts alike.",
    1  # Alice's ID
)
print(f"   Post 2: {post2}")

post3 = post_service.create_post(
    "Hello from Bob",
    "Bob's first post on this awesome platform!",
    2  # Bob's ID
)
print(f"   Post 3: {post3}")

# Get post with author
print("\n3. Fetching post with author info...")
post_detail = post_service.get_post_with_author(1)
print(f"   Post: {post_detail['title']}")
print(f"   Author: {post_detail['author']['username']}")
print(f"   Created: {post_detail['created_at']}")

# Get user's posts
print("\n4. Getting Alice's posts...")
alice_posts = post_service.get_user_posts(1)
print(f"   Alice has {len(alice_posts)} posts:")
for post in alice_posts:
    print(f"     - {post['title']}")

# Get user profile
print("\n5. Getting user profile...")
profile = user_service.get_user_profile(1)
print(f"   Username: {profile['username']}")
print(f"   Email: {profile['email']}")
print(f"   Member since: {profile['member_since']}")

print("\n=== Architecture Benefits ===")
benefits = [
    "✓ Each layer has single responsibility",
    "✓ Business logic separated from data access",
    "✓ Easy to test each layer independently",
    "✓ Can swap repositories (e.g., in-memory -> database)",
    "✓ Validation centralized in services",
    "✓ Clear data flow: API → Service → Repository → Database"
]

for benefit in benefits:
    print(benefit)
```
