---
type: "WARNING"
title: "Full-Stack Development Pitfalls"
---

N+1 QUERY PROBLEM:
Loading 100 users with their orders = 101 database queries!
Fix: Use @EntityGraph or JOIN FETCH for related entities.

CIRCULAR JSON REFERENCES:
User has Orders, Order has User = infinite JSON loop!
Fix: Use @JsonIgnore or DTOs to break the cycle.

MISSING @TRANSACTIONAL:
Lazy-loaded entities fail outside of session.
Fix: Add @Transactional to service methods that load relationships.

FRONTEND STATE OUT OF SYNC:
User creates item but list doesn't update.
Fix: Always refresh data after mutations or use optimistic updates.