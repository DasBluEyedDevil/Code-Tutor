---
type: "WARNING"
title: "JPA Pitfalls to Avoid"
---

1. N+1 QUERY PROBLEM:
   - Loading a list, then accessing relationships = many queries
   - Fix: Use JOIN FETCH or @EntityGraph

2. LAZY LOADING EXCEPTIONS:
   - Accessing lazy relationships outside transaction = error
   - Fix: Use @Transactional or fetch eagerly when needed

3. ENTITY LIFECYCLE CONFUSION:
   - Detached entities don't auto-save
   - Use merge() to reattach

4. OVERFETCHING:
   - Don't load entire entities when you only need ID
   - Use projections or DTOs for read operations

5. MISSING @Transactional:
   - JPA operations need transaction context
   - Spring Boot: Add @Transactional to service methods

6. BIDIRECTIONAL RELATIONSHIP BUGS:
   - Must set both sides of relationship
   - Use helper methods: student.addEnrollment(enrollment)