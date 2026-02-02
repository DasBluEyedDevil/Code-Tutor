---
type: "WARNING"
title: "Common Pitfalls"
---

Common query optimization mistakes:

1. **Fetching all fields when you need few**:
   ```typescript
   // BAD: Fetches password, bio, avatar, etc.
   const users = await prisma.user.findMany();
   const names = users.map(u => u.name);
   
   // GOOD: Only fetches what you need
   const users = await prisma.user.findMany({
     select: { name: true }
   });
   ```

2. **Using offset pagination for large datasets**:
   ```typescript
   // BAD: Slow for page 1000
   const users = await prisma.user.findMany({
     skip: 9990,  // DB must scan 9990 rows first!
     take: 10
   });
   
   // GOOD: Cursor pagination is O(1)
   const users = await prisma.user.findMany({
     cursor: { id: lastSeenId },
     take: 10
   });
   ```

3. **Counting in JavaScript instead of database**:
   ```typescript
   // BAD: Fetches ALL users just to count
   const users = await prisma.user.findMany();
   const count = users.length;
   
   // GOOD: Let database count
   const count = await prisma.user.count();
   ```

4. **Calculating aggregates in JavaScript**:
   ```typescript
   // BAD: Fetches all orders to calculate sum
   const orders = await prisma.order.findMany();
   const total = orders.reduce((sum, o) => sum + o.total, 0);
   
   // GOOD: Let database calculate
   const { _sum } = await prisma.order.aggregate({
     _sum: { total: true }
   });
   ```

5. **N+1 query problem**:
   ```typescript
   // BAD: 1 query for users + N queries for posts
   const users = await prisma.user.findMany();
   for (const user of users) {
     const posts = await prisma.post.findMany({
       where: { authorId: user.id }
     });
   }
   
   // GOOD: 1 query with include
   const users = await prisma.user.findMany({
     include: { posts: true }
   });
   ```

6. **String interpolation in raw SQL (SQL injection!)**:
   ```typescript
   // DANGEROUS! SQL Injection vulnerability
   const users = await prisma.$queryRawUnsafe(
     `SELECT * FROM users WHERE name = '${userInput}'`
   );
   
   // SAFE: Use tagged template
   const users = await prisma.$queryRaw`
     SELECT * FROM users WHERE name = ${userInput}
   `;
   ```

7. **Mixing select and include**:
   ```typescript
   // ERROR: Can't use both at same level
   const user = await prisma.user.findUnique({
     where: { id: 1 },
     select: { name: true },
     include: { posts: true }  // Error!
   });
   
   // CORRECT: Use select for everything
   const user = await prisma.user.findUnique({
     where: { id: 1 },
     select: {
       name: true,
       posts: true  // Include via select
     }
   });
   ```

8. **Not using database indexes**:
   ```prisma
   // In schema.prisma - add indexes for queried fields
   model User {
     id    Int    @id @default(autoincrement())
     email String @unique  // Automatically indexed
     name  String
     
     @@index([name])       // Add index for name queries
     @@index([createdAt])  // Add index for date sorting
   }
   ```

9. **Fetching relations you don't need**:
   ```typescript
   // BAD: Fetches ALL posts even if you just need user info
   const user = await prisma.user.findUnique({
     where: { id: 1 },
     include: { posts: true }  // Potentially thousands of posts!
   });
   
   // GOOD: Limit what you fetch
   const user = await prisma.user.findUnique({
     where: { id: 1 },
     include: {
       posts: {
         take: 5,
         orderBy: { createdAt: 'desc' }
       }
     }
   });
   ```

10. **Not batching bulk operations**:
    ```typescript
    // BAD: 100 individual queries
    for (const data of items) {
      await prisma.item.create({ data });
    }
    
    // GOOD: Single bulk query
    await prisma.item.createMany({
      data: items
    });
    ```