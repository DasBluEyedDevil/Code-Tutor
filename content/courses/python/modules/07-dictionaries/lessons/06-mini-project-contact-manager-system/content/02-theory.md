---
type: "THEORY"
title: "Building the Contact Manager"
---

**Core functions we need:**

1. **`add_contact()`** - Add a new contact
2. **`get_contact()`** - Look up a contact by name
3. **`update_contact()`** - Modify contact details
4. **`delete_contact()`** - Remove a contact
5. **`search_contacts()`** - Find contacts by name pattern
6. **`filter_by_tag()`** - Find contacts with a specific tag
7. **`list_all_tags()`** - Get all unique tags
8. **`display_contact()`** - Format contact for display

**Design principles:**

```python
# Use lowercase keys for case-insensitive lookup
def normalize_name(name):
    return name.lower().strip()

# Return meaningful values (not just print)
def get_contact(contacts, name):
    key = normalize_name(name)
    return contacts.get(key)  # Returns None if not found

# Handle edge cases
def add_contact(contacts, name, phone, email, tags=None):
    if not name or not phone:
        return False  # Invalid input
    key = normalize_name(name)
    if key in contacts:
        return False  # Already exists
    # ... add the contact
    return True
```

This structure makes the code testable and maintainable!