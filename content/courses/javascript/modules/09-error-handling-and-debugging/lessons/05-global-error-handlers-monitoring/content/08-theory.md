---
type: "THEORY"
title: "The Ultimate Safety Net"
---

Global error handlers are events triggered by the JavaScript runtime when an error "bubbles up" to the very top level of the program without being caught.

### 1. The Browser Events
*   **`onerror`**: The oldest handler. It captures basic errors like variable typos or `null` property access.
*   **`unhandledrejection`**: Crucial for modern apps. It catches `await` calls or `.then` chains that fail without a `catch` block.

### 2. The Node.js Events
*   **`uncaughtException`**: Triggered when a synchronous error isn't caught. Node.js documentation warns that continuing after this event is dangerous, because the system is now in an "undefined state."
*   **`unhandledRejection`**: The server-side equivalent of the browser's rejection event.

### 3. Error Monitoring Services
In professional production apps (like Facebook, Netflix, or Code Tutor), developers don't just log errors to the console. They use tools like **Sentry**, **Bugsnag**, or **Datadog**. 
1.  The Global Handler catches the error.
2.  The Handler sends the error details (stack trace, user's browser version, operating system) to the monitoring service via an API call.
3.  The service alerts the developers (via Slack or Email) so they can fix the bug before more users see it.

### 4. Why restart Node.js?
If an error reaches `uncaughtException`, it means your code crashed in a way you didn't expect. Memory could be corrupted, or database connections could be stuck open. The safest practice is to log the error and let the process die. A "Process Manager" (like PM2 or Docker) will automatically restart a fresh, clean version of your server.
