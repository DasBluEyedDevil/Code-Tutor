---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you're running a restaurant that needs to update its menu:

NO VERSIONING (Breaking Changes):
- Change the pasta recipe completely
- Regular customers order 'pasta' expecting the old dish
- They get something totally different!
- Angry customers, bad reviews

URL VERSIONING (/v1/, /v2/):
- Two separate menus: 'Classic Menu' and 'New Menu'
- Customers explicitly choose which one
- 'I'll order from the Classic Menu'
- Clear separation, no surprises

HEADER VERSIONING (X-API-Version):
- Same menu card, but waiter asks 'Which style?'
- Customer says 'Traditional style' or 'Modern style'
- Menu looks the same, behavior differs
- Cleaner URLs, but hidden complexity

QUERY VERSIONING (?api-version=1.0):
- Add a note to your order: 'Pasta (original recipe)'
- Works with bookmarks and sharing
- Version visible in URL
- Easy to test different versions

WHY VERSION?
- Clients break when APIs change
- Mobile apps can't update instantly
- Partners need migration time
- Multiple versions can coexist

VERSION STRATEGIES:
1. URL Path: /api/v1/users (most common, very clear)
2. Query String: /api/users?version=1.0 (easy to add)
3. Header: X-API-Version: 1 (clean URLs)
4. Media Type: Accept: application/vnd.api.v1+json (RESTful)

Think: 'API versioning is like having multiple menus - old customers keep their favorites, new customers get improvements!'