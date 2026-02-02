---
type: "THEORY"
title: "The Fetch API"
---

`fetch` is the built-in JavaScript method for making network requests. It is much more powerful and flexible than the older `XMLHttpRequest` method used in the past.

### 1. The Two-Step Process
A standard `fetch` call requires two `await` statements:
1.  **Wait for the headers:** `const response = await fetch(url);` 
    *   This gets the "metadata" (status codes, content type).
2.  **Wait for the body:** `const data = await response.json();`
    *   This downloads the actual content and converts it into a JavaScript Object.

### 2. HTTP Status Codes
The "metadata" returned in Step 1 includes a status code:
*   **200-299:** Success!
*   **400-499:** Client Error (You did something wrong, like a 404 "Not Found").
*   **500-599:** Server Error (The other computer crashed).

**Crucially**, `fetch` does NOT throw an error for a 404 or 500. It only fails if the request never finished at all (e.g., you're offline). You must check `response.ok` or `response.status` yourself.

### 3. HTTP Methods
*   **GET:** To retrieve data (default).
*   **POST:** To create new data.
*   **PUT/PATCH:** To update existing data.
*   **DELETE:** To remove data.

### 4. JSON: The Language of the Web
JSON (**J**ava**S**cript **O**bject **N**otation) is the standard format for sending data across the internet. It looks almost exactly like a JavaScript Object, but it's just a long string of text. `JSON.stringify()` turns an Object into text, and `response.json()` turns text back into an Object.
