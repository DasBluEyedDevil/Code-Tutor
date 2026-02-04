---
type: "ANALOGY"
title: "The Post Office"
---

Think of HTTP requests and responses like sending and receiving mail at a post office. When your Flutter app sends a request, it is writing a letter with specific instructions: "Dear server, please send me a list of all users" (a GET request) or "Dear server, here is a new user to add" (a POST request with a body).

The server reads the letter, does the work, and sends back a response envelope. That envelope always has a status code stamped on the outside -- like a tracking sticker that says "200: Delivered successfully" or "404: Address not found" or "500: Our sorting machine broke." The actual data you asked for is inside the envelope as the response body, typically formatted as JSON.

**Middleware is like the mailroom staff** who inspect every letter before it reaches the intended recipient. They might check for a return address (authentication), log when each letter arrives (logging), or reject letters that are too large (rate limiting). The letter itself never changes, but the mailroom decides whether it gets through.
