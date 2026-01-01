---
type: "EXAMPLE"
title: "Code Example: Contact Manager System"
---

**Expected Output:**
```
=== Contact Manager System ===

--- Adding Contacts ---
Added: Alice Smith
Added: Bob Jones
Added: Charlie Brown
Alice Smith already exists!

--- Viewing Contacts ---

All Contacts:
  1. Alice Smith
     Phone: 555-1234
     Email: alice@example.com
     Tags: friend, work
  2. Bob Jones
     Phone: 555-5678
     Email: bob@example.com
     Tags: family
  3. Charlie Brown
     Phone: 555-9999
     Email: charlie@example.com
     Tags: friend, school

--- Searching ---
Contacts matching 'ali': ['Alice Smith']

Contacts with tag 'friend':
  - Alice Smith
  - Charlie Brown

--- All Tags ---
Unique tags: family, friend, school, work

--- Updating Contact ---
Updated Alice Smith's phone to 555-4321

--- Contact Details ---
Alice Smith
  Phone: 555-4321
  Email: alice@example.com
  Tags: friend, work
```

```python
# Contact Manager System
# A practical application using dictionaries, sets, and functions

# ============ HELPER FUNCTIONS ============

def normalize_name(name):
    """Convert name to lowercase for consistent lookup."""
    return name.lower().strip()

def display_contact(contact, indent=2):
    """Format a contact for display."""
    spaces = " " * indent
    tags_str = ", ".join(sorted(contact["tags"])) if contact["tags"] else "none"
    print(f"{spaces}Phone: {contact['phone']}")
    print(f"{spaces}Email: {contact['email']}")
    print(f"{spaces}Tags: {tags_str}")

# ============ CORE FUNCTIONS ============

def add_contact(contacts, name, phone, email, tags=None):
    """Add a new contact. Returns True if successful."""
    key = normalize_name(name)
    if key in contacts:
        return False  # Already exists
    
    contacts[key] = {
        "name": name,  # Keep original case
        "phone": phone,
        "email": email,
        "tags": set(tags) if tags else set()
    }
    return True

def get_contact(contacts, name):
    """Get a contact by name. Returns None if not found."""
    key = normalize_name(name)
    return contacts.get(key)

def update_contact(contacts, name, field, value):
    """Update a contact field. Returns True if successful."""
    contact = get_contact(contacts, name)
    if not contact:
        return False
    
    if field == "tags":
        contact["tags"] = set(value) if isinstance(value, list) else {value}
    else:
        contact[field] = value
    return True

def search_contacts(contacts, query):
    """Find contacts whose name contains the query."""
    query = query.lower()
    return [c["name"] for c in contacts.values() if query in c["name"].lower()]

def filter_by_tag(contacts, tag):
    """Find all contacts with a specific tag."""
    return [c["name"] for c in contacts.values() if tag in c["tags"]]

def get_all_tags(contacts):
    """Get all unique tags across all contacts."""
    all_tags = set()
    for contact in contacts.values():
        all_tags.update(contact["tags"])
    return all_tags

# ============ DEMO ============

print("=== Contact Manager System ===")

# Initialize empty contacts dictionary
contacts = {}

print("\n--- Adding Contacts ---")
if add_contact(contacts, "Alice Smith", "555-1234", "alice@example.com", ["friend", "work"]):
    print("Added: Alice Smith")
if add_contact(contacts, "Bob Jones", "555-5678", "bob@example.com", ["family"]):
    print("Added: Bob Jones")
if add_contact(contacts, "Charlie Brown", "555-9999", "charlie@example.com", ["friend", "school"]):
    print("Added: Charlie Brown")
if not add_contact(contacts, "Alice Smith", "555-0000", "alice2@example.com"):
    print("Alice Smith already exists!")

print("\n--- Viewing Contacts ---")
print("\nAll Contacts:")
for i, contact in enumerate(contacts.values(), 1):
    print(f"  {i}. {contact['name']}")
    display_contact(contact, indent=5)

print("\n--- Searching ---")
results = search_contacts(contacts, "ali")
print(f"Contacts matching 'ali': {results}")

print("\nContacts with tag 'friend':")
for name in filter_by_tag(contacts, "friend"):
    print(f"  - {name}")

print("\n--- All Tags ---")
all_tags = get_all_tags(contacts)
print(f"Unique tags: {', '.join(sorted(all_tags))}")

print("\n--- Updating Contact ---")
if update_contact(contacts, "Alice Smith", "phone", "555-4321"):
    print("Updated Alice Smith's phone to 555-4321")

print("\n--- Contact Details ---")
alice = get_contact(contacts, "alice smith")  # Case insensitive!
if alice:
    print(alice["name"])
    display_contact(alice)
```
