---
type: "WARNING"
title: "Debugging and Logging Pitfalls"
---

AVOID THESE MISTAKES:

1. LEAVING DEBUG PRINTLNS IN CODE
   Remove System.out.println() before committing. Use proper logging instead.

2. LOGGING SENSITIVE DATA
   NEVER log passwords, credit cards, or personal data. Logs can be accessed by many people.

3. NOT LOGGING ENOUGH CONTEXT
   Bad: logger.error("Failed")
   Good: logger.error("Failed to process order {} for user {}", orderId, userId)

4. IGNORING EXCEPTION STACK TRACES
   Always include the exception: logger.error("Error processing", exception)

5. DEBUGGING IN PRODUCTION
   Use logging for production issues. Debuggers are for development only.