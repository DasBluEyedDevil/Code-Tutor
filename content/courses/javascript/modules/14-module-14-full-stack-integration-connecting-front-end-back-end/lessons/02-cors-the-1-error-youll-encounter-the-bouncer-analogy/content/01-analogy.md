---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a nightclub with a strict bouncer:

Club Rules:
- Only people from the guest list can enter
- The bouncer checks your ID and origin
- If you're not on the list → DENIED!

Web Browser:
- Your React app runs on http://localhost:3000 (origin A)
- Your Hono API runs on http://localhost:4000 (origin B)
- Browser: 'These are DIFFERENT addresses!'
- Browser acts as bouncer: 'Can origin A talk to origin B?'

CORS = Cross-Origin Resource Sharing:
- Security feature built into ALL browsers
- Prevents malicious websites from stealing data
- By default: BLOCKS all cross-origin requests
- You must EXPLICITLY allow your frontend to talk to your backend

The Fix:
- Backend says: 'Yes, localhost:3000 is on the guest list'
- Uses CORS headers to tell browser: 'This is allowed'
- One line of code: app.use('*', cors())

Without CORS configuration → Every API call FAILS!