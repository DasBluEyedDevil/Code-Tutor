---
type: "KEY_POINT"
title: "Error Handling is Like a Hospital Emergency Response"
---

PATIENT EMERGENCY (Error occurs):

1. FIRST RESPONDERS (Try-Catch at source):
   Paramedics identify the problem immediately
   Like: Validation catches bad input right away

2. EMERGENCY ROOM (Backend exception handler):
   Doctors diagnose and treat the condition
   Like: @RestControllerAdvice catches all exceptions
   Returns: Structured diagnosis (error code + message)

3. FAMILY NOTIFICATION (Frontend UI):
   Family gets clear, understandable information
   Like: User sees "Email already exists"
   NOT: "SQLException: Duplicate key violation on idx_email"

4. MEDICAL RECORDS (Logging):
   Everything documented for future analysis
   Like: Server logs stack traces for debugging
   User NEVER sees stack traces (security risk!)