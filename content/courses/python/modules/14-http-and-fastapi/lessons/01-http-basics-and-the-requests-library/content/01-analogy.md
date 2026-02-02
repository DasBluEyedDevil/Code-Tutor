---
type: "ANALOGY"
title: "The Concept: How the Web Works"
---

**HTTP = How computers talk on the web**

**Think of it like a restaurant:**

**Client (You) → Server (Kitchen)**
- You: "Can I have a burger?" (REQUEST)
- Kitchen: "Here's your burger!" (RESPONSE)

**HTTP Request Methods:**

1. **GET** 📖 - Read/retrieve data
   - Like asking to see the menu
   - "Show me the user with ID 5"

2. **POST** ➕ - Create new data
   - Like placing an order
   - "Create a new user account"

3. **PUT** 📝 - Update existing data
   - Like changing your order
   - "Update user profile"

4. **DELETE** 🗑️ - Remove data
   - Like canceling your order
   - "Delete this post"

**HTTP Status Codes:**
- **2xx (Success)** ✅
  - 200: OK - Request succeeded
  - 201: Created - New resource created

- **3xx (Redirect)** ↪️
  - 301: Moved permanently
  - 302: Temporary redirect

- **4xx (Client Error)** ❌
  - 400: Bad request
  - 401: Unauthorized
  - 404: Not found

- **5xx (Server Error)** 💥
  - 500: Internal server error
  - 503: Service unavailable

**Headers:** Metadata about the request/response
- Content-Type: application/json
- Authorization: Bearer token
- User-Agent: Browser info