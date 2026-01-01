---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine calling a restaurant:

You: "Do you have a table for 4?"

GOOD responses:
✅ "Yes, table 12 is ready!" (200 OK with data)
✅ "Sorry, we're fully booked" (404 Not Found)
✅ "Invalid number, we don't have table for -1 people!" (400 Bad Request)

BAD response:
❌ "Umm... maybe? I dunno" (Unclear status)

HTTP STATUS CODES are how APIs communicate results:

2xx SUCCESS:
• 200 OK - Request succeeded, here's data
• 201 Created - New resource created
• 204 No Content - Success, but no data to return

4xx CLIENT ERROR (user's fault):
• 400 Bad Request - Invalid input
• 404 Not Found - Resource doesn't exist
• 401 Unauthorized - Need to log in

5xx SERVER ERROR (our fault):
• 500 Internal Server Error - Something broke

Think: Status codes = 'The universal language of HTTP. Speak it correctly!'