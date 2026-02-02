---
type: "WARNING"
title: "Common Pitfalls"
---

Common Prisma mistakes:

1. **Forgetting async/await**:
   ```typescript
   // Wrong!
   let user = prisma.user.findUnique({ where: { id: 1 } });
   console.log(user.name); // ERROR: user is a Promise!
   
   // Correct!
   let user = await prisma.user.findUnique({ where: { id: 1 } });
   console.log(user.name); // Works!
   ```

2. **Not running migrations**:
   - After changing schema.prisma, run: `npx prisma migrate dev`
   - Then: `npx prisma generate` to update TypeScript types
   - Without this, your code won't match the database!

3. **Prisma Client not initialized**:
   ```typescript
   // Create once, reuse everywhere
   // prisma/client.ts
   import { PrismaClient } from '@prisma/client';
   export const prisma = new PrismaClient();
   
   // other files
   import { prisma } from './prisma/client';
   ```

4. **Not handling null results**:
   ```typescript
   let user = await prisma.user.findUnique({ where: { id: 999 } });
   console.log(user.name); // ERROR if user is null!
   
   // Better:
   if (!user) {
     throw new Error('User not found');
   }
   console.log(user.name);
   ```

5. **Forgetting to connect/disconnect**:
   ```typescript
   // Usually not needed in serverless
   // But for long-running servers:
   await prisma.$connect();
   // ... use prisma ...
   await prisma.$disconnect();
   ```

6. **Schema syntax errors**:
   ```prisma
   // Wrong!
   model User {
     id Int @id
     name String
     // Missing newline before }
   }
   
   // Correct!
   model User {
     id   Int    @id @default(autoincrement())
     name String
   }
   ```

7. **Not using Prisma Studio**:
   - Run `npx prisma studio` to see your data visually
   - Great for debugging and understanding your database
   - Much easier than writing SELECT queries!