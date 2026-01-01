# Password Strength Checker - SOLUTION
# Validate password against security criteria

print("=== Password Strength Checker ===")
print()

password = input("Enter your password: ")

print("\nChecking password strength...")

# Counter for requirements met
requirements_met = 0

# Check 1: Length requirement (>= 8 characters)
if len(password) >= 8:
    print(f"✓ Password length is sufficient ({len(password)} characters)")
    requirements_met = requirements_met + 1

# Check 2: Contains at least one number
has_number = any(char.isdigit() for char in password)
if has_number:
    print("✓ Password contains at least one number")
    requirements_met = requirements_met + 1

# Check 3: Not a common password
common_passwords = ["password", "12345678", "qwerty"]
if password not in common_passwords:
    print("✓ Password is not commonly used")
    requirements_met = requirements_met + 1

# Display final result
print(f"\nPassword strength: {requirements_met}/3 requirements met")

if requirements_met == 3:
    print("Your password is strong!")

if requirements_met < 3:
    print("Your password needs improvement.")