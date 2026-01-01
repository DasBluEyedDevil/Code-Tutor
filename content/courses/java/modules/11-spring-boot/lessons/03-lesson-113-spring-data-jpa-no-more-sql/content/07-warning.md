---
type: "WARNING"
title: "JPA and Database Best Practices"
---

CRITICAL JPA CONSIDERATIONS:

1. NEVER USE ddl-auto IN PRODUCTION
   - Use: spring.jpa.hibernate.ddl-auto=validate
   - Use Flyway or Liquibase for migrations
   - ddl-auto=update can cause data loss

2. N+1 QUERY PROBLEM
   - Lazy loading can cause performance issues
   - Use @EntityGraph or JOIN FETCH
   - Monitor with spring.jpa.show-sql=true

3. TRANSACTION MANAGEMENT
   - Use @Transactional on service methods
   - Understand propagation and isolation
   - Avoid long transactions

4. ENTITY RELATIONSHIPS
   - Prefer @ManyToOne over @OneToMany
   - Avoid bidirectional if possible
   - Use cascade carefully (especially REMOVE)

5. CONNECTION POOLING
   - HikariCP is the default (and best)
   - Configure pool size appropriately
   - Monitor connection leaks