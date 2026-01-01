import json

# User Profile Manager
# This solution demonstrates JSON file operations for data persistence

FILENAME = "profiles.json"

def save_profiles(profiles):
    """Save profiles list to JSON file."""
    # Open file in write mode and save with pretty formatting
    with open(FILENAME, 'w') as file:
        json.dump(profiles, file, indent=2)

def load_profiles():
    """Load profiles from JSON file."""
    try:
        with open(FILENAME, 'r') as file:
            return json.load(file)
    except FileNotFoundError:
        # File doesn't exist yet, return empty list
        return []

def add_profile(username, email, age, premium=False):
    """Add a new user profile."""
    # Step 1: Load existing profiles
    profiles = load_profiles()
    
    # Step 2: Create new profile dictionary
    new_profile = {
        'username': username,
        'email': email,
        'age': age,
        'premium': premium
    }
    
    # Step 3: Append to profiles list
    profiles.append(new_profile)
    
    # Step 4: Save profiles
    save_profiles(profiles)
    print(f"Added profile for '{username}'")

def update_profile(username, **updates):
    """Update an existing profile."""
    # Step 1: Load profiles
    profiles = load_profiles()
    
    # Step 2: Find profile with matching username
    for profile in profiles:
        if profile['username'] == username:
            # Step 3: Update fields from **updates
            for key, value in updates.items():
                if key in profile:
                    profile[key] = value
            
            # Step 4: Save profiles
            save_profiles(profiles)
            print(f"Updated profile for '{username}'")
            return True
    
    print(f"User '{username}' not found")
    return False

def display_profiles():
    """Display all profiles."""
    profiles = load_profiles()
    
    if not profiles:
        print("No profiles found.")
        return
    
    for profile in profiles:
        status = "Premium" if profile['premium'] else "Free"
        print(f"  {profile['username']}: {profile['email']}, age {profile['age']} ({status})")

# Test the functions
print("=== User Profile Manager ===")

print("\n1. Adding profiles...")
add_profile("alice", "alice@example.com", 25, True)
add_profile("bob", "bob@example.com", 30)
add_profile("carol", "carol@example.com", 28, True)

print("\n2. All profiles:")
display_profiles()

print("\n3. Updating Bob's profile...")
update_profile("bob", premium=True, age=31)

print("\n4. Updated profiles:")
display_profiles()