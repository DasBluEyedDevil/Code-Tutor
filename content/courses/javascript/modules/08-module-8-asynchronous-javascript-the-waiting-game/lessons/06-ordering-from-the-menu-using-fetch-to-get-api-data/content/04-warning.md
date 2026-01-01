---
type: "WARNING"
title: "Fetching Pitfalls"
---

### 1. Silent Failures (Status 404)
As mentioned before, `fetch` will not trigger your `catch` block for a 404 error. 
*   **Error:** Your code proceeds to `response.json()`, but the server sent back an HTML error page instead of JSON. 
*   **Result:** `SyntaxError: Unexpected token < in JSON at position 0`.
*   **Fix:** Always check `if (!response.ok)` before parsing the data.

### 2. CORS Errors
The most frustrating error for web developers! Browsers have a security feature called **CORS**. It prevents Website A from stealing data from Website B without permission. 
If the API you are trying to reach hasn't "allowed" your website, the browser will block the request. 
*   **Fix:** Usually requires server-side configuration or using a proxy.

### 3. Forgetting `JSON.stringify`
When sending data (`POST`), you cannot just pass a JavaScript object into the `body`. 
*   **Wrong:** `body: { name: "Alice" }`
*   **Right:** `body: JSON.stringify({ name: "Alice" })`

### 4. Protocol Matters
If you are on a secure site (`https://`), you cannot fetch data from an insecure site (`http://`). This is called a "Mixed Content" error and is blocked by all modern browsers.
