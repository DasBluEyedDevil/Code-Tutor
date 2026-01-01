---
type: "THEORY"
title: "NOT @CrossOrigin(\"*\")"
---

Avoid using @CrossOrigin("*") which allows all origins. Instead, configure CORS to only accept requests from your frontend domain and set reasonable rate limits to prevent abuse.