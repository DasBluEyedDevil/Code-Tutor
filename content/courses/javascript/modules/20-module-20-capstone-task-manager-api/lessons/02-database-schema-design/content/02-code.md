---
type: "EXAMPLE"
title: "Complete Prisma Schema"
---

Create your database schema with proper relationships and constraints:

```prisma
// prisma/schema.prisma
generator client {
  provider = "prisma-client-js"
}

datasource db {
  provider = "sqlite"
  url      = env("DATABASE_URL")
}

model User {
  id           String     @id @default(cuid())
  email        String     @unique
  passwordHash String
  name         String?
  createdAt    DateTime   @default(now())
  updatedAt    DateTime   @updatedAt

  tasks        Task[]
  categories   Category[]

  @@map("users")
}

model Task {
  id          String    @id @default(cuid())
  title       String
  description String?
  status      String    @default("pending") // pending, in_progress, completed
  priority    String    @default("medium")  // low, medium, high
  dueDate     DateTime?
  createdAt   DateTime  @default(now())
  updatedAt   DateTime  @updatedAt

  // Relations
  userId      String
  user        User      @relation(fields: [userId], references: [id], onDelete: Cascade)
  
  categoryId  String?
  category    Category? @relation(fields: [categoryId], references: [id], onDelete: SetNull)

  @@index([userId])
  @@index([categoryId])
  @@index([status])
  @@index([dueDate])
  @@map("tasks")
}

model Category {
  id        String   @id @default(cuid())
  name      String
  color     String   @default("#3B82F6") // Default blue
  createdAt DateTime @default(now())
  updatedAt DateTime @updatedAt

  // Relations
  userId    String
  user      User     @relation(fields: [userId], references: [id], onDelete: Cascade)
  
  tasks     Task[]

  @@unique([userId, name]) // User can't have duplicate category names
  @@index([userId])
  @@map("categories")
}
```
