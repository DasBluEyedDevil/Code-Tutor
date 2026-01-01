---
type: "THEORY"
title: "⚠️ Common Full-Stack Mistakes"
---

❌ MISTAKE 1: Not handling errors
Frontend sends request, backend fails, UI shows nothing
FIX: try-catch everywhere, display errors to user

❌ MISTAKE 2: No loading states
User clicks button, waits... did it work?
FIX: Show 'Loading...' or disable buttons during requests

❌ MISTAKE 3: Not escaping user input
User enters: <script>alert('hacked')</script>
FIX: Use escapeHtml() or textContent instead of innerHTML

❌ MISTAKE 4: Missing CORS configuration
Frontend: http://localhost:3000
Backend: http://localhost:8080
Browser blocks request!
FIX: @CrossOrigin on controller

❌ MISTAKE 5: Not validating on backend
Trust frontend validation? Attacker bypasses it!
FIX: ALWAYS validate on backend with @Valid

❌ MISTAKE 6: Exposing IDs without authorization
User can delete task ID 123, not theirs!
FIX: Check task.userId === currentUserId

❌ MISTAKE 7: N+1 query problem
Loading 100 tasks → 101 database queries!
FIX: Use @EntityGraph or JOIN FETCH