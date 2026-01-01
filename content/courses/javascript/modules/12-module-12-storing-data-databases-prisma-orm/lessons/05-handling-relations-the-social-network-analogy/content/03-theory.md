---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Prisma Relations Syntax Guide:

1. **ONE-TO-MANY Relationship**:
   ```prisma
   model User {
     id    Int    @id @default(autoincrement())
     posts Post[] // Relation field (no column in database)
   }
   
   model Post {
     id       Int  @id @default(autoincrement())
     authorId Int  // Foreign key (actual column)
     author   User @relation(fields: [authorId], references: [id])
   }
   ```
   
   Key points:
   - "Many" side: array type (Post[])
   - "One" side: singular type + @relation
   - Foreign key: actual database column
   - Relation field: virtual, not in database

2. **ONE-TO-ONE Relationship**:
   ```prisma
   model User {
     id      Int      @id
     profile Profile? // Optional (user might not have profile)
   }
   
   model Profile {
     id     Int  @id
     userId Int  @unique // UNIQUE makes it one-to-one!
     user   User @relation(fields: [userId], references: [id])
   }
   ```
   
   Key points:
   - Foreign key must be @unique
   - One side usually optional (?)

3. **MANY-TO-MANY (Implicit)**:
   ```prisma
   model User {
     id    Int    @id
     posts Post[] @relation("UserLikes")
   }
   
   model Post {
     id     Int    @id
     likedBy User[] @relation("UserLikes")
   }
   ```
   
   Key points:
   - Both sides are arrays
   - Named relation ("UserLikes")
   - Prisma auto-creates join table (_UserLikes)
   - No foreign keys needed!

4. **MANY-TO-MANY (Explicit)**:
   ```prisma
   model User {
     id          Int          @id
     enrollments Enrollment[]
   }
   
   model Course {
     id          Int          @id
     enrollments Enrollment[]
   }
   
   model Enrollment {
     id       Int      @id
     userId   Int
     user     User     @relation(fields: [userId], references: [id])
     courseId Int
     course   Course   @relation(fields: [courseId], references: [id])
     
     enrolledAt DateTime @default(now())
     grade      String?
     
     @@unique([userId, courseId])
   }
   ```
   
   Use when you need:
   - Extra fields (enrolledAt, grade)
   - Composite unique constraints
   - More control over join table

5. **Relation Attributes**:
   ```prisma
   @relation(fields: [authorId], references: [id])
   ```
   
   - `fields`: Foreign key in this model
   - `references`: Primary key in related model
   - Both are arrays (can be composite keys)

6. **Cascade Delete** (be careful!):
   ```prisma
   model User {
     id    Int    @id
     posts Post[]
   }
   
   model Post {
     id       Int  @id
     authorId Int
     author   User @relation(fields: [authorId], references: [id], onDelete: Cascade)
   }
   ```
   
   Options:
   - `Cascade`: Delete posts when user deleted
   - `SetNull`: Set authorId to null
   - `Restrict`: Prevent deletion if posts exist
   - `NoAction`: Database default

7. **Self-Relations** (like followers):
   ```prisma
   model User {
     id         Int    @id
     followers  User[] @relation("UserFollows")
     following  User[] @relation("UserFollows")
   }
   ```

8. **Querying Relations**:
   ```typescript
   // Include related data
   const user = await prisma.user.findUnique({
     where: { id: 1 },
     include: {
       posts: true,
       profile: true
     }
   });
   
   // Select specific fields
   const user = await prisma.user.findUnique({
     where: { id: 1 },
     select: {
       name: true,
       posts: {
         select: {
           title: true,
           createdAt: true
         }
       }
     }
   });
   
   // Filter relations
   const user = await prisma.user.findUnique({
     where: { id: 1 },
     include: {
       posts: {
         where: { published: true },
         orderBy: { createdAt: 'desc' },
         take: 5
       }
     }
   });
   ```

9. **Creating with Relations**:
   ```typescript
   // Connect to existing
   await prisma.post.create({
     data: {
       title: 'New Post',
       author: { connect: { id: 1 } }
     }
   });
   
   // Create related
   await prisma.user.create({
     data: {
       email: 'bob@example.com',
       posts: {
         create: [
           { title: 'First Post' },
           { title: 'Second Post' }
         ]
       }
     }
   });
   
   // Nested writes
   await prisma.user.update({
     where: { id: 1 },
     data: {
       posts: {
         create: { title: 'Another Post' },
         delete: { id: 5 },
         update: {
           where: { id: 3 },
           data: { title: 'Updated Title' }
         }
       }
     }
   });
   ```