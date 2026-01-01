---
type: "THEORY"
title: "Understanding the Concept"
---

Now it's time to put everything together! We'll build a **Contact Manager** - a practical application that uses dictionaries, sets, and functions to manage a list of contacts.

**What we'll build:**

- Store contacts with names, phone numbers, emails, and tags
- Add, update, and delete contacts
- Search contacts by name or tag
- List all contacts or filter by criteria
- Track unique tags across all contacts

**Data structure design:**

```python
# Each contact is a dictionary
contact = {
    "name": "Alice Smith",
    "phone": "555-1234",
    "email": "alice@example.com",
    "tags": {"friend", "work"}
}

# All contacts stored in a dictionary, keyed by name
contacts = {
    "alice smith": {...},
    "bob jones": {...}
}
```

**Why this structure?**

- Dictionary keys (lowercase names) allow fast lookup
- Nested dictionaries store all contact details
- Sets for tags ensure no duplicate tags per contact
- Functions keep the code organized and reusable