---
type: "THEORY"
title: "Understanding the Concept"
---

Imagine you're applying for a special membership program at a gym. The bouncer at the door makes decisions in layers:

1. **First check:** "Are you 18 or older?"
   - If NO → "Sorry, adults only" (stop here)
   - If YES → (proceed to next check)

2. **Second check (only if first passed):** "Do you have a membership card?"
   - If YES → "Welcome! Enter"
   - If NO → "Please sign up at the desk"

Notice how the second check **only happens IF** the first check passed. This is **nested decision-making** - decisions within decisions!

### In Code:

```python
if age >= 18:              # First decision (outer)
    if has_membership:     # Second decision (inner, only checked if outer True)
        print("Welcome!") 
    else:
        print("Please sign up")
else:
    print("Adults only")  # First check failed, never reached inner check
```

### Real-World Nested Decision Examples:

- **Online shopping discounts**:
  - IF in cart >= $50:
    - IF is_member:
      - Apply 20% discount
    - ELSE:
      - Apply 10% discount

- **Access control system**:
  - IF is_logged_in:
    - IF is_admin:
      - Show admin dashboard
    - ELIF is_moderator:
      - Show moderator panel
    - ELSE:
      - Show user dashboard
  - ELSE:
    - Show login page

- **Ride safety check**:
  - IF tall_enough:
    - IF has_ticket:
      - IF not_pregnant:
        - Allow on ride

Each layer adds another condition that must be checked only if previous conditions passed.
