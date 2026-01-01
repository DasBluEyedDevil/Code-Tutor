---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting to Normalize Keys Consistently**
```python
# WRONG - Inconsistent key normalization
contacts = {}
contacts["Alice"] = {"phone": "555-1234"}
print(contacts.get("alice"))  # None! Keys don't match

# CORRECT - Always normalize keys the same way
def normalize(name):
    return name.lower().strip()

contacts = {}
contacts[normalize("Alice")] = {"phone": "555-1234"}
print(contacts.get(normalize("alice")))  # Works!
```

**2. Modifying Data While Looking It Up**
```python
# WRONG - get() returns a reference, changes affect the dict!
contacts = {"alice": {"phone": "555-1234", "tags": set()}}
data = contacts.get("alice")
data["tags"].add("friend")  # Modifies the original dict!

# CORRECT - Make a copy if you need to modify without affecting original
import copy
data = copy.deepcopy(contacts.get("alice"))
data["tags"].add("friend")  # Original unchanged
```

**3. Not Validating Input Before Adding to Dict**
```python
# WRONG - No validation leads to bad data
def add_contact(contacts, name, phone):
    contacts[name.lower()] = {"name": name, "phone": phone}

add_contact(contacts, "", "555-1234")  # Empty name added!
add_contact(contacts, "Bob", "")       # Empty phone added!

# CORRECT - Validate before adding
def add_contact(contacts, name, phone):
    if not name or not name.strip():
        return False
    if not phone:
        return False
    contacts[name.lower().strip()] = {"name": name, "phone": phone}
    return True
```

**4. Returning None When False Would Be Clearer**
```python
# WRONG - Ambiguous return values
def find_contact(contacts, name):
    return contacts.get(name.lower())  # Returns None if not found

# But None could mean "not found" OR "contact has no data"

# CORRECT - Be explicit about what's happening
def find_contact(contacts, name):
    key = name.lower()
    if key not in contacts:
        return None  # Explicitly not found
    return contacts[key]  # Return the actual data
```

**5. Not Handling Case Where Data Structure Changes**
```python
# WRONG - Assuming structure never changes
def get_email(contacts, name):
    return contacts[name.lower()]["email"]  # KeyError if missing!

# CORRECT - Use get() with defaults at each level
def get_email(contacts, name):
    contact = contacts.get(name.lower(), {})
    return contact.get("email", "No email on file")
```