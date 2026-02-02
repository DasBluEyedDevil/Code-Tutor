---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Prisma Transaction Types and Usage:

1. **Sequential Transactions (Array Syntax)**:
   ```typescript
   // Pass an array of Prisma operations
   const [user, post, comment] = await prisma.$transaction([
     prisma.user.create({ data: { name: 'Alice' } }),
     prisma.post.create({ data: { title: 'Hello' } }),
     prisma.comment.create({ data: { text: 'Nice!' } })
   ]);
   
   // Best for:
   // - Independent operations that must all succeed
   // - Simple create/update operations
   // - When you don't need to read before writing
   ```

2. **Interactive Transactions (Callback Syntax)**:
   ```typescript
   const result = await prisma.$transaction(async (tx) => {
     // tx is a transactional Prisma client
     // Use tx instead of prisma inside the callback
     
     const user = await tx.user.findUnique({ where: { id: 1 } });
     
     if (!user) {
       throw new Error('User not found');
     }
     
     const updatedUser = await tx.user.update({
       where: { id: 1 },
       data: { credits: { decrement: 10 } }
     });
     
     return updatedUser;
   });
   
   // Best for:
   // - Read-then-write operations
   // - Business logic validation
   // - Conditional operations
   ```

3. **Nested Writes (Automatic Transactions)**:
   ```typescript
   // Prisma automatically wraps nested operations
   const user = await prisma.user.create({
     data: {
       email: 'alice@example.com',
       profile: { create: { bio: 'Hello' } },
       posts: {
         create: [
           { title: 'Post 1' },
           { title: 'Post 2' }
         ]
       }
     }
   });
   
   // No explicit transaction needed!
   // Prisma ensures atomic creation
   ```

4. **Transaction Configuration Options**:
   ```typescript
   await prisma.$transaction(
     async (tx) => { /* ... */ },
     {
       maxWait: 5000,   // Wait for transaction slot (ms)
       timeout: 10000,  // Max execution time (ms)
       isolationLevel: 'Serializable'
     }
   );
   ```
   
   **Isolation Levels**:
   - `ReadUncommitted`: Can see uncommitted changes (dirty reads)
   - `ReadCommitted`: Only see committed changes (PostgreSQL default)
   - `RepeatableRead`: Consistent snapshot for entire transaction
   - `Serializable`: Highest isolation, prevents all anomalies

5. **Error Handling Patterns**:
   ```typescript
   try {
     await prisma.$transaction(async (tx) => {
       // If any operation fails or throws...
       throw new Error('Something went wrong');
     });
   } catch (error) {
     // ...the entire transaction is rolled back
     console.error('Transaction failed:', error.message);
   }
   ```

6. **Common Transaction Patterns**:

   **Transfer Pattern**:
   ```typescript
   await prisma.$transaction(async (tx) => {
     await tx.account.update({
       where: { id: senderId },
       data: { balance: { decrement: amount } }
     });
     await tx.account.update({
       where: { id: receiverId },
       data: { balance: { increment: amount } }
     });
   });
   ```

   **Create-and-Connect Pattern**:
   ```typescript
   await prisma.$transaction(async (tx) => {
     const order = await tx.order.create({
       data: { userId: 1, total: 100 }
     });
     await tx.inventory.update({
       where: { productId: 1 },
       data: { quantity: { decrement: 1 } }
     });
     return order;
   });
   ```

   **Validate-then-Write Pattern**:
   ```typescript
   await prisma.$transaction(async (tx) => {
     const user = await tx.user.findUnique({ where: { id: 1 } });
     
     if (user.credits < 10) {
       throw new Error('Insufficient credits');
     }
     
     return tx.user.update({
       where: { id: 1 },
       data: { credits: { decrement: 10 } }
     });
   });
   ```