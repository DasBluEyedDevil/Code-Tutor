---
type: "WARNING"
title: "Common Pitfalls"
---

Common transaction mistakes:

1. **Using prisma instead of tx inside callback**:
   ```typescript
   // WRONG! Uses global prisma, not transactional
   await prisma.$transaction(async (tx) => {
     await prisma.user.create({ ... });  // NOT transactional!
   });
   
   // CORRECT! Uses tx parameter
   await prisma.$transaction(async (tx) => {
     await tx.user.create({ ... });  // Transactional!
   });
   ```

2. **Not catching transaction errors**:
   ```typescript
   // WRONG! Unhandled rejection
   prisma.$transaction(async (tx) => {
     throw new Error('Failed');
   });
   
   // CORRECT! Proper error handling
   try {
     await prisma.$transaction(async (tx) => {
       throw new Error('Failed');
     });
   } catch (error) {
     console.error('Transaction rolled back:', error.message);
   }
   ```

3. **Long-running transactions**:
   ```typescript
   // WRONG! Transaction holds locks too long
   await prisma.$transaction(async (tx) => {
     await tx.user.update({ ... });
     await sendEmail();           // Slow external call!
     await processPayment();      // Another slow call!
   });
   
   // CORRECT! Minimize transaction scope
   const user = await prisma.$transaction(async (tx) => {
     return tx.user.update({ ... });
   });
   await sendEmail();           // Outside transaction
   await processPayment();      // Outside transaction
   ```

4. **Forgetting transactions for related updates**:
   ```typescript
   // WRONG! Not atomic - partial failure possible
   await prisma.account.update({ where: { id: 1 }, data: { balance: { decrement: 100 } } });
   await prisma.account.update({ where: { id: 2 }, data: { balance: { increment: 100 } } });
   
   // CORRECT! Atomic transaction
   await prisma.$transaction([
     prisma.account.update({ where: { id: 1 }, data: { balance: { decrement: 100 } } }),
     prisma.account.update({ where: { id: 2 }, data: { balance: { increment: 100 } } })
   ]);
   ```

5. **Mixing array and callback syntax incorrectly**:
   ```typescript
   // WRONG! Can't mix syntaxes
   await prisma.$transaction([
     async (tx) => { ... }  // This won't work!
   ]);
   
   // CORRECT! Choose one syntax
   // Array syntax:
   await prisma.$transaction([prisma.user.create({ ... })]);
   
   // OR Callback syntax:
   await prisma.$transaction(async (tx) => {
     await tx.user.create({ ... });
   });
   ```

6. **Not setting appropriate timeouts**:
   ```typescript
   // WRONG! Default timeout may be too short for complex operations
   await prisma.$transaction(async (tx) => {
     // Complex, multi-step operation
   });
   
   // CORRECT! Set appropriate timeout
   await prisma.$transaction(
     async (tx) => { /* ... */ },
     { timeout: 30000 }  // 30 seconds
   );
   ```

7. **Returning non-serializable values**:
   ```typescript
   // WRONG! Functions can't be serialized
   await prisma.$transaction(async (tx) => {
     return () => console.log('Hi');  // Error!
   });
   
   // CORRECT! Return plain data
   await prisma.$transaction(async (tx) => {
     const user = await tx.user.create({ ... });
     return { id: user.id, name: user.name };
   });
   ```

8. **Ignoring isolation level for critical operations**:
   ```typescript
   // For financial operations, consider Serializable
   await prisma.$transaction(
     async (tx) => {
       // Critical financial operation
     },
     { isolationLevel: 'Serializable' }
   );
   ```