---
type: "WARNING"
title: "Common Pitfalls"
---

Common database misconceptions:

1. **"Databases are too complex for beginners"**:
   - Start simple with SQLite or Prisma
   - You don't need to be a DBA to use databases
   - ORMs (like Prisma) make it much easier

2. **"I can just use variables/files instead"**:
   ```javascript
   // This seems easier but...
   let users = [];  // Lost on restart!
   
   // Or
   fs.writeFileSync('users.json', JSON.stringify(users));
   // File corruption? Race conditions? Concurrent access?
   ```
   Databases solve these problems professionally

3. **"All databases are the same"**:
   - SQL databases: Structured, relations, ACID guarantees
   - NoSQL databases: Flexible, scalable, eventual consistency
   - Choose based on your needs!

4. **"I need to learn raw SQL first"**:
   - Modern ORMs (Prisma, TypeORM) abstract SQL
   - You can learn SQL gradually
   - Start with ORM, understand SQL over time

5. **"Databases are slow"**:
   - Modern databases are EXTREMELY fast
   - Proper indexes make queries lightning quick
   - Can handle millions of queries per second

6. **"I don't need relationships"**:
   - Even simple apps benefit from relationships
   - User → Posts → Comments (natural hierarchy)
   - Avoids data duplication

7. **"Development database vs Production database"**:
   - Use SQLite for development (simple, file-based)
   - Use PostgreSQL for production (robust, scalable)
   - Prisma makes switching databases easy!