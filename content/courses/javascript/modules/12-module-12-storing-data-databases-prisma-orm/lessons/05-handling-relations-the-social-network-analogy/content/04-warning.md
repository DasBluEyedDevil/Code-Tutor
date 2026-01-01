---
type: "WARNING"
title: "Common Pitfalls"
---

Common relation mistakes:

1. **Forgetting foreign key field**:
   ```prisma
   // WRONG! Missing authorId
   model Post {
     id     Int  @id
     author User @relation(fields: [authorId], references: [id])
   }
   
   // CORRECT!
   model Post {
     id       Int  @id
     authorId Int  // Foreign key field
     author   User @relation(fields: [authorId], references: [id])
   }
   ```

2. **Wrong array syntax**:
   ```prisma
   // WRONG!
   model User {
     posts Post  // Missing brackets!
   }
   
   // CORRECT!
   model User {
     posts Post[]  // Array of posts
   }
   ```

3. **Missing @relation name for many-to-many**:
   ```prisma
   // WRONG! Ambiguous relation
   model User {
     posts Post[]
   }
   model Post {
     users User[]
   }
   
   // CORRECT!
   model User {
     posts Post[] @relation("UserPosts")
   }
   model Post {
     users User[] @relation("UserPosts")
   }
   ```

4. **Not using @unique for one-to-one**:
   ```prisma
   // WRONG! This is one-to-many without @unique
   model Profile {
     userId Int
     user   User @relation(fields: [userId], references: [id])
   }
   
   // CORRECT! @unique makes it one-to-one
   model Profile {
     userId Int  @unique
     user   User @relation(fields: [userId], references: [id])
   }
   ```

5. **Forgetting include in queries**:
   ```typescript
   // WRONG! This doesn't include posts
   const user = await prisma.user.findUnique({ where: { id: 1 } });
   console.log(user.posts);  // undefined!
   
   // CORRECT!
   const user = await prisma.user.findUnique({
     where: { id: 1 },
     include: { posts: true }
   });
   console.log(user.posts);  // Array of posts
   ```

6. **Cascade delete pitfalls**:
   ```prisma
   // DANGEROUS! Deleting user deletes all posts
   model Post {
     authorId Int
     author   User @relation(fields: [authorId], references: [id], onDelete: Cascade)
   }
   
   // Better: Prevent deletion if posts exist
   model Post {
     authorId Int
     author   User @relation(fields: [authorId], references: [id], onDelete: Restrict)
   }
   ```

7. **Creating without connecting**:
   ```typescript
   // WRONG! Post needs an author
   await prisma.post.create({
     data: {
       title: 'New Post'
       // Missing author connection!
     }
   });
   
   // CORRECT!
   await prisma.post.create({
     data: {
       title: 'New Post',
       author: { connect: { id: 1 } }
     }
   });
   ```

8. **Self-relation confusion**:
   ```prisma
   // WRONG! Ambiguous self-relation
   model User {
     followers User[]
     following User[]
   }
   
   // CORRECT! Named relation
   model User {
     followers User[] @relation("UserFollows")
     following User[] @relation("UserFollows")
   }
   ```

9. **Not understanding implicit vs explicit many-to-many**:
   ```prisma
   // Implicit: Prisma manages join table
   model User {
     posts Post[] @relation("Likes")
   }
   model Post {
     likedBy User[] @relation("Likes")
   }
   
   // Explicit: You control join table (when you need extra fields)
   model User {
     likes Like[]
   }
   model Post {
     likes Like[]
   }
   model Like {
     userId Int
     user   User @relation(fields: [userId], references: [id])
     postId Int
     post   Post @relation(fields: [postId], references: [id])
     likedAt DateTime @default(now())  // Extra field!
   }
   ```

10. **Relation filtering mistakes**:
    ```typescript
    // WRONG! Can't filter like this
    const users = await prisma.user.findMany({
      where: { posts.published: true }  // Error!
    });
    
    // CORRECT! Use relation filters
    const users = await prisma.user.findMany({
      where: {
        posts: {
          some: { published: true }  // Users with at least one published post
        }
      }
    });
    ```