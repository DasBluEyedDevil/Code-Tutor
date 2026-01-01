---
type: "THEORY"
title: "⚠️ Production Security Checklist"
---

Before deploying to production:

✓ HTTPS only (no HTTP)
server.ssl.enabled=true

✓ Hide actuator endpoints from public
management.endpoints.web.exposure.include=health