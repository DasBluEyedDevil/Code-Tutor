from pydantic import BaseModel, Field, field_validator, model_validator, ValidationError
from typing import Self

class UserProfile(BaseModel):
    """User profile with comprehensive validation."""
    
    username: str
    email: str
    age: int | None = None
    bio: str | None = None
    website: str | None = None
    
    # TODO: Add field validator for username
    # - Must be 3-20 characters
    # - Only alphanumeric
    # - Convert to lowercase
    
    # TODO: Add field validator for email
    # - Must contain @
    # - Must have domain after @
    
    # TODO: Add field validator for age
    # - If provided, must be 13-120
    
    # TODO: Add field validator for website
    # - If provided, must start with https://
    
    # TODO: Add model validator
    # - If age < 18, bio cannot contain "adult" or "18+"
    
    pass

# Test your implementation
test_cases = [
    {"username": "Alice123", "email": "alice@test.com", "age": 25},
    {"username": "ab", "email": "invalid"},  # Should fail
    {"username": "teen_user", "email": "teen@test.com", "age": 15, "bio": "I love coding"},
]

for data in test_cases:
    try:
        user = UserProfile.model_validate(data)
        print(f"Valid: {user.username}")
    except ValidationError as e:
        print(f"Invalid: {e.error_count()} errors")