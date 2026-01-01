---
type: "THEORY"
title: "Exercise 1: Safe User Profile Display"
---


**Goal**: Create a user profile system that handles missing data safely.

**Requirements**:
1. Create a `User` data class with nullable fields: name, email, phone, address
2. Create `displayProfile(user: User?)` function that:
   - Shows all available information
   - Shows "Not provided" for missing fields
   - Shows "No user data" if user is null
3. Test with different combinations of null/non-null values

---

