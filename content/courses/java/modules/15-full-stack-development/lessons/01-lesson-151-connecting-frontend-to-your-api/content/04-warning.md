---
type: "WARNING"
title: "Common Frontend-Backend Connection Mistakes"
---

CORS ERRORS:
Browser blocks requests from different origins by default.
Fix: Add @CrossOrigin annotation to your Spring controllers.

HARDCODED URLS:
Don't hardcode 'localhost:8080' in production code!
Fix: Use environment variables or config files.

NO ERROR HANDLING:
Fetch doesn't throw on HTTP errors (404, 500).
Fix: Always check response.ok before processing.

MIXED CONTENT:
HTTPS pages can't call HTTP APIs.
Fix: Use HTTPS everywhere in production.

JSON PARSING ERRORS:
Forgetting Content-Type header on POST requests.
Fix: Always set headers: { 'Content-Type': 'application/json' }