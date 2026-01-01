---
type: "WARNING"
title: "Common Pitfalls"
---

Common Prisma schema mistakes:

1. **Forgetting semicolons or using wrong syntax**:
   ```prisma
   // Wrong! Prisma doesn't use semicolons
   model User {
     id Int @id;
   }
   
   // Correct!
   model User {
     id Int @id
   }
   ```

2. **Wrong field type capitalization**:
   ```prisma
   // Wrong!
   name string  // lowercase
   
   // Correct!
   name String  // Pascal case
   ```

3. **Forgetting @id attribute**:
   ```prisma
   // Wrong! Every model needs an @id
   model User {
     email String @unique
     name  String
   }
   
   // Correct!
   model User {
     id    Int    @id @default(autoincrement())
     email String @unique
     name  String
   }
   ```

4. **Using JavaScript syntax in schema**:
   ```prisma
   // Wrong! This is not JavaScript
   model User {
     isActive: Boolean = true
   }
   
   // Correct! This is Prisma schema language
   model User {
     isActive Boolean @default(true)
   }
   ```

5. **Wrong default value syntax**:
   ```prisma
   // Wrong!
   createdAt DateTime default(now())
   
   // Correct!
   createdAt DateTime @default(now())
   ```

6. **Not running migrate after schema changes**:
   - Changed schema.prisma?
   - Run: `npx prisma migrate dev`
   - Otherwise database won't match your schema!

7. **Incorrect optional syntax**:
   ```prisma
   // Wrong!
   bio String | null
   
   // Correct!
   bio String?  // Question mark makes it optional
   ```

8. **Forgetting environment variables**:
   - Schema uses: `url = env("DATABASE_URL")`
   - Must create .env file:
   ```
   DATABASE_URL="postgresql://user:pass@localhost:5432/db"
   ```

9. **Using relations without foreign keys**:
   ```prisma
   // Incomplete! Missing authorId field
   model Post {
     id     Int  @id @default(autoincrement())
     author User @relation(fields: [authorId], references: [id])
   }
   
   // Correct!
   model Post {
     id       Int  @id @default(autoincrement())
     authorId Int  // Foreign key field
     author   User @relation(fields: [authorId], references: [id])
   }
   ```