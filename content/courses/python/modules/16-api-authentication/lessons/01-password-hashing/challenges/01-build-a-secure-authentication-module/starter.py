from pydantic import BaseModel, EmailStr
from passlib.context import CryptContext
from typing import Optional, Dict

# TODO: Set up password hashing context with bcrypt
pwd_context = None

# TODO: Implement hash_password function
def hash_password(password: str) -> str:
    """Hash a password for secure storage."""
    pass

# TODO: Implement verify_password function
def verify_password(plain_password: str, hashed_password: str) -> bool:
    """Verify a password against its hash."""
    pass

# TODO: Create UserCreate model with email (EmailStr) and password (str)
class UserCreate(BaseModel):
    pass

# TODO: Create UserResponse model with id (int) and email (str)
class UserResponse(BaseModel):
    pass

# Mock database
users_db: Dict[str, dict] = {}
next_id = 1

# TODO: Implement register_user
def register_user(user_data: UserCreate) -> Optional[UserResponse]:
    """Register a new user with hashed password.
    
    Returns UserResponse on success, None if email exists.
    """
    pass

# TODO: Implement authenticate_user
def authenticate_user(email: str, password: str) -> Optional[UserResponse]:
    """Authenticate user with email and password.
    
    Returns UserResponse on success, None if authentication fails.
    """
    pass

# Test your implementation
if __name__ == "__main__":
    # Test registration
    user = UserCreate(email="alice@example.com", password="secret123")
    result = register_user(user)
    print(f"Registered: {result}")
    
    # Test duplicate registration
    result2 = register_user(user)
    print(f"Duplicate: {result2}")
    
    # Test authentication
    auth = authenticate_user("alice@example.com", "secret123")
    print(f"Auth success: {auth}")
    
    # Test wrong password
    auth_fail = authenticate_user("alice@example.com", "wrong")
    print(f"Auth fail: {auth_fail}")