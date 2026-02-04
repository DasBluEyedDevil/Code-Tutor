---
type: "KEY_POINT"
title: "✏️ Quiz Answer Key"
---


**Question 1**: **B) Path parameter: `/profile/42`**

Explanation: User ID is a required identifier that specifies *which* user's profile. Path parameters are perfect for required resource identifiers. Query parameters would make it seem optional.

---

**Question 2**: **B) Use all query parameters**

Explanation: Query parameters are ideal for optional filters. Users can provide as many or as few as they want. Path parameters would be unwieldy, and POST body would be overkill for a simple read operation (GET).

---

**Question 3**: **B) Gets the page parameter, converts to Int, or returns 1 if null/invalid**

Explanation: `?.` safely accesses the parameter (returns null if not present), `toIntOrNull()` converts to Int (returns null if invalid), and `?: 1` provides a default value of 1.

---

**Congratulations!** You now understand all three ways to receive data in Ktor and when to use each! 🎉

