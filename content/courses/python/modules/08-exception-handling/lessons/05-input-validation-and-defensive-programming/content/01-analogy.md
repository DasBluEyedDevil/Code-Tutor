---
type: "ANALOGY"
title: "The Concept: The Bouncer at the Door"
---

Imagine you're a bouncer at an exclusive club. Do you:

**A) Let everyone in and deal with problems later?** (No validation)
- Drunk people start fights → chaos
- Underage people drink → legal problems  
- Result: Club gets shut down

**B) Check EVERYTHING at the door?** (Input validation)
- ID check → Prevent underage entry
- Sobriety check → Prevent drunk people
- Dress code check → Maintain standards
- Result: Safe, enjoyable club

This is **defensive programming** - assume EVERYTHING can go wrong, and guard against it.

**Two Philosophies:**

**LBYL (Look Before You Leap):**
"Check ID before letting them in"
```python
if age >= 21:  # Check first
    serve_drink()  # Then act
else:
    deny_entry()
```

**EAFP (Easier to Ask Forgiveness than Permission):**
"Let them order, catch the error if they're underage"
```python
try:
    serve_drink()  # Try it
except UnderageError:  # Handle error
    deny_entry()
```

**Python prefers EAFP** for many situations (it's more Pythonic), but **validation is still crucial**.

**Real-world scenarios:**

1. **User registration:**
   - Validate email format, password strength, age
   - Don't just trust user input!

2. **API endpoints:**
   - Validate all request parameters
   - Check data types, ranges, required fields

3. **File processing:**
   - Validate file exists, correct format, not too large
   - Don't assume file is perfect

**Defensive programming checklist:**
- ✅ Validate ALL user input (never trust it)
- ✅ Check types (is it an int when you expect int?)
- ✅ Check ranges (is age between 0-120?)
- ✅ Check formats (is email valid? Is date parseable?)
- ✅ Provide helpful error messages (tell users what's wrong)
- ✅ Have fallback values when appropriate