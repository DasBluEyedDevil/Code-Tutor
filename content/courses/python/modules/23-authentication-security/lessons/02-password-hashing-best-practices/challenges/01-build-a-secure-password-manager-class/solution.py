from passlib.context import CryptContext
import secrets
import string
import re
from typing import Tuple, Optional

class PasswordManager:
    def __init__(self, rounds: int = 12):
        self.pwd_context = CryptContext(
            schemes=["bcrypt"],
            deprecated="auto",
            bcrypt__rounds=rounds
        )
    
    def hash_password(self, password: str) -> str:
        """Hash a password securely"""
        return self.pwd_context.hash(password)
    
    def verify_password(self, plain: str, hashed: str) -> Tuple[bool, bool]:
        """
        Verify password and check if rehash needed.
        Returns: (is_valid, needs_rehash)
        """
        is_valid = self.pwd_context.verify(plain, hashed)
        needs_rehash = self.pwd_context.needs_update(hashed) if is_valid else False
        return (is_valid, needs_rehash)
    
    def validate_strength(self, password: str) -> Tuple[bool, list]:
        """
        Validate password strength.
        Returns: (is_valid, list_of_failures)
        """
        failures = []
        
        if len(password) < 12:
            failures.append("Must be at least 12 characters")
        if not re.search(r'[A-Z]', password):
            failures.append("Must contain uppercase letter")
        if not re.search(r'[a-z]', password):
            failures.append("Must contain lowercase letter")
        if not re.search(r'\d', password):
            failures.append("Must contain digit")
        if not re.search(r'[!@#$%^&*(),.?":{}|<>]', password):
            failures.append("Must contain special character")
        
        return (len(failures) == 0, failures)
    
    def generate_temporary_password(self, length: int = 16) -> str:
        """
        Generate a secure temporary password.
        Must include upper, lower, digit, and special char.
        """
        # Ensure at least one of each required type
        password = [
            secrets.choice(string.ascii_uppercase),
            secrets.choice(string.ascii_lowercase),
            secrets.choice(string.digits),
            secrets.choice("!@#$%^&*(),.?\"")
        ]
        
        # Fill the rest with random characters from all types
        all_chars = string.ascii_letters + string.digits + "!@#$%^&*(),.?\""
        password += [secrets.choice(all_chars) for _ in range(length - 4)]
        
        # Shuffle to avoid predictable positions
        secrets.SystemRandom().shuffle(password)
        
        return ''.join(password)

# Test the password manager
pm = PasswordManager(rounds=12)

# Test password strength validation
print("Password Strength Tests:")
for pwd in ["short", "longenoughbutweakpassword", "SecureP@ss123!"]:
    is_valid, failures = pm.validate_strength(pwd)
    status = "PASS" if is_valid else f"FAIL: {failures}"
    print(f"  '{pwd}': {status}")

# Test hashing and verification
password = "MySecureP@ssword123!"
hashed = pm.hash_password(password)
print(f"\nHashed: {hashed[:50]}...")

is_valid, needs_rehash = pm.verify_password(password, hashed)
print(f"Verification: valid={is_valid}, needs_rehash={needs_rehash}")

is_valid, _ = pm.verify_password("wrong", hashed)
print(f"Wrong password: valid={is_valid}")

# Test temporary password generation
temp = pm.generate_temporary_password()
print(f"\nTemporary password: {temp}")
temp_valid, _ = pm.validate_strength(temp)
print(f"Temporary password strength check: {temp_valid}")