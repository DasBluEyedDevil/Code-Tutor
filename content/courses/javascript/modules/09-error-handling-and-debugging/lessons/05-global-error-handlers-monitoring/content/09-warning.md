---
type: "WARNING"
title: "Monitoring Pitfalls"
---

### 1. Global is Not Local
Never use global handlers as a replacement for `try/catch`. 
*   **Problem:** By the time an error reaches the global handler, you've already lost the "Context." You don't know which function failed or what the variables were at that moment. You can't "fix" the error and keep going.

### 2. Don't hide errors from yourself
If you use `window.onerror` and return `true`, the browser will stop showing errors in the developer console. This makes local debugging much harder. Only return `true` in your production build.

### 3. The Node.js "Zombies"
If you catch an `uncaughtException` in Node but don't call `process.exit(1)`, your server might stay alive but stop responding to requests properly. This is called a "Zombie Process." Always crash and restart for safety.

### 4. Privacy Concerns
When sending errors to monitoring services like Sentry, be careful not to send **Sensitive Data**. If an error happens while a user is typing their password, and that password is part of the error message, you might accidentally send the password to your logging service! Always scrub sensitive data from your error logs.
