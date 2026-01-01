# Contact Manager - COMPLETE SOLUTION

print("=== Contact Manager ===")
print()

# Create empty contact list
contacts = []

# Add 5 contacts using append()
contacts.append("Alice Johnson")
contacts.append("Bob Smith")
contacts.append("Charlie Brown")
contacts.append("Diana Ross")
contacts.append("Eve Adams")

print(f"Added {len(contacts)} contacts")

# Insert emergency contact at beginning (index 0)
contacts.insert(0, "Emergency Services")
print("Added emergency contact at top")

print()
print("All Contacts:")

# Print all contacts with 1-based numbering
for position, name in enumerate(contacts, start=1):
    print(f"{position}. {name}")

print()

# Remove "Bob Smith" using remove()
contacts.remove("Bob Smith")
print("Removed: Bob Smith")

# Remove last contact using pop() and save it
last_contact = contacts.pop()
print(f"Last contact removed: {last_contact}")

print()

# Sort contacts alphabetically
contacts.sort()

print("Contacts sorted alphabetically:")
for position, name in enumerate(contacts, start=1):
    print(f"{position}. {name}")

print()

# Search for a contact
search_term = input("Search for contact: ")

# Check if any contact contains the search term
found = False
for position, name in enumerate(contacts, start=1):
    if search_term.lower() in name.lower():  # Case-insensitive search
        print(f"✅ '{name}' found at position {position}")
        found = True
        break

if not found:
    print(f"❌ No contact found matching '{search_term}'")

print()

# Count contacts starting with a letter
letter = input("Count contacts starting with letter: ").upper()
count = 0

for name in contacts:
    if name.startswith(letter):  # Check if name starts with letter
        count = count + 1

print(f"Contacts starting with '{letter}': {count}")

print()
print(f"Final contact list: {contacts}")