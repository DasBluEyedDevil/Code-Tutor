from pydantic import BaseModel, Field, field_validator, model_validator, ValidationError
from typing import Self
import re

class UserProfile(BaseModel):
    """User profile with comprehensive Pydantic v2 validation."""
    
    username: str = Field(..., min_length=3, max_length=20)
    email: str
    age: int | None = Field(default=None, ge=13, le=120)
    bio: str | None = Field(default=None, max_length=500)
    website: str | None = None
    
    @field_validator("username")
    @classmethod
    def validate_username(cls, v: str) -> str:
        """Username must be alphanumeric and lowercase."""
        if not v.isalnum():
            raise ValueError("Username must be alphanumeric only")
        return v.lower()
    
    @field_validator("email")
    @classmethod
    def validate_email(cls, v: str) -> str:
        """Basic email validation."""
        if "@" not in v:
            raise ValueError("Email must contain @")
        parts = v.split("@")
        if len(parts) != 2 or not parts[1] or "." not in parts[1]:
            raise ValueError("Invalid email format")
        return v.lower()
    
    @field_validator("website")
    @classmethod
    def validate_website(cls, v: str | None) -> str | None:
        """Website must use HTTPS if provided."""
        if v is None:
            return v
        if not v.startswith("https://"):
            raise ValueError("Website must start with https://")
        return v
    
    @model_validator(mode="after")
    def check_age_restrictions(self) -> Self:
        """If user is under 18, bio cannot contain adult content markers."""
        if self.age is not None and self.age < 18 and self.bio:
            restricted_words = ["adult", "18+", "nsfw"]
            bio_lower = self.bio.lower()
            for word in restricted_words:
                if word in bio_lower:
                    raise ValueError(
                        f"Bio cannot contain '{word}' for users under 18"
                    )
        return self

# Test valid profiles
print("=== Testing UserProfile Validation ===")

print("\n1. Valid adult user:")
user1 = UserProfile(
    username="Alice123",
    email="alice@example.com",
    age=25,
    bio="Python developer and coffee enthusiast",
    website="https://alice.dev"
)
print(f"   Username: {user1.username}")
print(f"   Email: {user1.email}")
print(f"   Age: {user1.age}")

print("\n2. Valid teen user:")
user2 = UserProfile(
    username="TeenCoder",
    email="TEEN@School.EDU",
    age=15,
    bio="Learning to code!"
)
print(f"   Username: {user2.username}")
print(f"   Email: {user2.email}")

print("\n3. Minimal valid user:")
user3 = UserProfile(username="minimal", email="min@test.com")
print(f"   Username: {user3.username}")
print(f"   Age: {user3.age} (optional)")

# Test validation errors
print("\n4. Invalid cases:")

test_cases = [
    ({"username": "ab", "email": "test@test.com"}, "Username too short"),
    ({"username": "valid", "email": "invalid"}, "Invalid email"),
    ({"username": "valid", "email": "a@b.com", "age": 10}, "Age too young"),
    ({"username": "valid", "email": "a@b.com", "website": "http://insecure.com"}, "HTTP not HTTPS"),
    ({"username": "teen", "email": "a@b.com", "age": 15, "bio": "I am 18+ years"}, "Restricted content for minor"),
]

for data, expected_error in test_cases:
    try:
        UserProfile.model_validate(data)
        print(f"   Unexpected success for: {expected_error}")
    except ValidationError as e:
        print(f"   Caught: {expected_error}")
        print(f"      -> {e.errors()[0]['msg']}")

print("\n5. Export to JSON:")
json_output = user1.model_dump_json(indent=2)
print(json_output)