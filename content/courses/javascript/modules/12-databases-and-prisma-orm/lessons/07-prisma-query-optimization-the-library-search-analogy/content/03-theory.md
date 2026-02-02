---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Prisma Query Optimization Techniques:

1. **Select Specific Fields**:
   ```typescript
   // Only fetch what you need
   const users = await prisma.user.findMany({
     select: {
       id: true,
       name: true,
       email: true
       // Excludes: password, bio, avatar, etc.
     }
   });
   
   // Select with relations
   const users = await prisma.user.findMany({
     select: {
       name: true,
       posts: {
         select: { title: true },
         take: 5
       }
     }
   });
   ```

2. **Offset-Based Pagination** (skip/take):
   ```typescript
   // Simple page-based pagination
   const pageSize = 10;
   const page = 3;
   
   const users = await prisma.user.findMany({
     skip: (page - 1) * pageSize,
     take: pageSize,
     orderBy: { createdAt: 'desc' }
   });
   
   // Get total for pagination UI
   const total = await prisma.user.count();
   const totalPages = Math.ceil(total / pageSize);
   ```

3. **Cursor-Based Pagination** (more efficient):
   ```typescript
   // First page
   const firstPage = await prisma.user.findMany({
     take: 10,
     orderBy: { id: 'asc' }
   });
   
   // Next page using cursor
   const lastId = firstPage[firstPage.length - 1].id;
   const nextPage = await prisma.user.findMany({
     take: 10,
     skip: 1,  // Skip the cursor
     cursor: { id: lastId },
     orderBy: { id: 'asc' }
   });
   ```

4. **Counting Records**:
   ```typescript
   // Total count
   const total = await prisma.user.count();
   
   // Conditional count
   const activeUsers = await prisma.user.count({
     where: { isActive: true }
   });
   
   // Count with relation
   const usersWithPosts = await prisma.user.count({
     where: {
       posts: { some: {} }
     }
   });
   ```

5. **Aggregations**:
   ```typescript
   const stats = await prisma.order.aggregate({
     _count: true,             // Count rows
     _sum: { total: true },    // Sum of totals
     _avg: { total: true },    // Average
     _min: { total: true },    // Minimum
     _max: { total: true },    // Maximum
     where: { status: 'completed' }  // Optional filter
   });
   
   // Result: { _count: 150, _sum: { total: 15000 }, ... }
   ```

6. **Group By**:
   ```typescript
   // Group by single field
   const byCategory = await prisma.product.groupBy({
     by: ['category'],
     _count: true,
     _sum: { price: true },
     orderBy: { _count: { id: 'desc' } }
   });
   
   // Group by multiple fields
   const stats = await prisma.order.groupBy({
     by: ['status', 'paymentMethod'],
     _sum: { total: true },
     _count: true,
     having: {
       total: { _sum: { gt: 1000 } }  // Filter groups
     }
   });
   ```

7. **Raw SQL Queries**:
   ```typescript
   // Tagged template (safe, parameterized)
   const users = await prisma.$queryRaw`
     SELECT * FROM users 
     WHERE name ILIKE ${pattern}
     LIMIT ${limit}
   `;
   
   // Execute raw (returns affected row count)
   const affected = await prisma.$executeRaw`
     UPDATE products SET price = price * 1.1
     WHERE category = ${category}
   `;
   
   // With Prisma.sql helper
   import { Prisma } from '@prisma/client';
   const sql = Prisma.sql`SELECT * FROM users WHERE age > ${age}`;
   const result = await prisma.$queryRaw(sql);
   ```

8. **Performance Tips**:
   - Use `select` to limit fields (reduces data transfer)
   - Use cursor pagination for large datasets
   - Use aggregations instead of fetching + calculating
   - Add database indexes for frequently queried fields
   - Use `findFirst` instead of `findMany()[0]`
   - Batch operations with `createMany`, `updateMany`, `deleteMany`