---
type: "THEORY"
title: "Understanding the Concept"
---

Imagine you're applying for a special membership program at a gym. The bouncer at the door makes decisions in layers:

<ol style='background-color: #f0f0f0; padding: 15px; margin: 10px 0;'><li style='margin: 8px 0;'>**First check:** "Are you 18 or older?"<ul style='margin-left: 20px;'>- If NO → "Sorry, adults only" (stop here)
- If YES → (proceed to next check)

</li><li style='margin: 8px 0;'>**Second check (only if first passed):** "Do you have a membership card?"<ul style='margin-left: 20px;'>- If YES → "Welcome! Enter"
- If NO → "Please sign up at the desk"

</li>
Notice how the second check **only happens IF** the first check passed. This is **nested decision-making** - decisions within decisions!

### In Code:
```
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
IF in cart >= $50:
&nbsp;&nbsp;&nbsp;&nbsp;IF is_member:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Apply 20% discount
&nbsp;&nbsp;&nbsp;&nbsp;ELSE:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Apply 10% discount
- **Access control system**:
IF is_logged_in:
&nbsp;&nbsp;&nbsp;&nbsp;IF is_admin:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Show admin dashboard
&nbsp;&nbsp;&nbsp;&nbsp;ELIF is_moderator:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Show moderator panel
&nbsp;&nbsp;&nbsp;&nbsp;ELSE:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Show user dashboard
ELSE:
&nbsp;&nbsp;&nbsp;&nbsp;Show login page
- **Ride safety check**:
IF tall_enough:
&nbsp;&nbsp;&nbsp;&nbsp;IF has_ticket:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IF not_pregnant:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Allow on ride

Each layer adds another condition that must be checked only if previous conditions passed.