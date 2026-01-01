from pydantic import BaseModel, EmailStr
from passlib.context import CryptContext
from typing import Optional, Dict

# Password hashing context with bcrypt
pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

def hash_password(password: str) -> str:
    """Hash a password for secure storage.
    
    Uses bcrypt with automatic salt generation.
    Each call produces a different hash for the same password.
    """
    return pwd_context.hash(password)

def verify_password(plain_password: str, hashed_password: str) -> bool:
    """Verify a password against its hash.
    
    Uses timing-safe comparison to prevent timing attacks.
    Returns True if password matches, False otherwise.
    """
    return pwd_context.verify(plain_password, hashed_password)

class UserCreate(BaseModel):
    """User registration request model."""
    email: EmailStr  # Automatic email format validation
    password: str

class UserResponse(BaseModel):
    """User response model (never includes password)."""
    id: int
    email: str

# Mock database
users_db: Dict[str, dict] = {}
next_id = 1

def register_user(user_data: UserCreate) -> Optional[UserResponse]:
    """Register a new user with hashed password.
    
    Returns UserResponse on success, None if email exists.
    """
    global next_id
    
    # Check if email already exists
    if user_data.email in users_db:
        return None
    
    # Hash password before storing
    hashed = hash_password(user_data.password)
    
    # Create user record
    user_record = {
        "id": next_id,
        "email": user_data.email,
        "hashed_password": hashed  # Store hash, never plain password
    }
    
    users_db[user_data.email] = user_record
    next_id += 1
    
    # Return response without password info
    return UserResponse(id=user_record["id"], email=user_record["email"])

def authenticate_user(email: str, password: str) -> Optional[UserResponse]:
    """Authenticate user with email and password.
    
    Returns UserResponse on success, None if authentication fails.
    """
    # Look up user
    user = users_db.get(email)
    if not user:
        return None
    
    # Verify password
    if not verify_password(password, user["hashed_password"]):
        return None
    
    return UserResponse(id=user["id"], email=user["email"])

# Test the implementation
if __name__ == "__main__":
    print("=== Password Hashing Challenge Solution ===")
    print()
    
    # Test registration
    user = UserCreate(email="alice@example.com", password="secret123")
    result = register_user(user)
    print(f"1. Registered: {result}")
    
    # Test duplicate registration
    result2 = register_user(user)
    print(f"2. Duplicate attempt: {result2}")
    
    # Test successful authentication
    auth = authenticate_user("alice@example.com", "secret123")
    print(f"3. Auth with correct password: {auth}")
    
    # Test wrong password
    auth_fail = authenticate_user("alice@example.com", "wrongpassword")
    print(f"4. Auth with wrong password: {auth_fail}")
    
    # Test non-existent user
    auth_none = authenticate_user("nobody@example.com", "secret123")
    print(f"5. Auth for non-existent user: {auth_none}")
    
    print()
    print("Database contents (for debugging only):")
    for email, data in users_db.items():
        print(f"  {email}: hash={data['hashed_password'][:30]}...")