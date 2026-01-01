# Contact Manager - Starter Code

print("=== Contact Manager ===")
print()

# YOUR CODE: Create empty contact list
contacts = 

# YOUR CODE: Add 5 contacts using append()
contacts.  ("Alice Johnson")
contacts.  ("Bob Smith")
contacts.  ("Charlie Brown")
contacts.  ("Diana Ross")
contacts.  ("Eve Adams")

print(f"Added {len(contacts)} contacts")

# YOUR CODE: Insert emergency contact at beginning (index 0)
contacts.  (  , "Emergency Services")
print("Added emergency contact at top")

print()
print("All Contacts:")

# YOUR CODE: Print all contacts with 1-based numbering
for   in enumerate(contacts, start=  ):
    print(f"{  }. {  }")

print()

# YOUR CODE: Remove "Bob Smith" using remove()
contacts.  ("Bob Smith")
print("Removed: Bob Smith")

# YOUR CODE: Remove last contact using pop() and save it
last_contact = contacts.  ()
print(f"Last contact removed: {last_contact}")

print()

# YOUR CODE: Sort contacts alphabetically
contacts.  ()

print("Contacts sorted alphabetically:")
for position, name in enumerate(contacts, start=1):
    print(f"{position}. {name}")

print()

# YOUR CODE: Search for a contact
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

# YOUR CODE: Count contacts starting with a letter
letter = input("Count contacts starting with letter: ").upper()
count = 0

for name in contacts:
    if name.startswith(letter):  # Check if name starts with letter
        count = count + 1

print(f"Contacts starting with '{letter}': {count}")

print()
print(f"Final contact list: {contacts}")