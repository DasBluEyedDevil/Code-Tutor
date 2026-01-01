---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Prisma Schema Syntax Guide:

1. **File Structure** (schema.prisma):
   ```prisma
   // Database connection
   datasource db {
     provider = "postgresql"
     url      = env("DATABASE_URL")
   }
   
   // TypeScript client generator
   generator client {
     provider = "prisma-client-js"
   }
   
   // Your data models
   model ModelName {
     // fields here
   }
   ```

2. **Field Syntax**:
   ```prisma
   model User {
     fieldName  FieldType  @attribute
   }
   ```

3. **Common Field Types**:
   - `String` - Text (VARCHAR)
   - `Int` - Integer
   - `Float` - Decimal number
   - `Boolean` - true/false
   - `DateTime` - Timestamp
   - `Json` - JSON object
   - `Bytes` - Binary data

4. **Field Attributes**:
   ```prisma
   id        Int      @id @default(autoincrement())
   email     String   @unique
   name      String   @default("Anonymous")
   createdAt DateTime @default(now())
   updatedAt DateTime @updatedAt
   bio       String?  // ? makes it optional
   tags      String[] // [] makes it an array
   ```

5. **Primary Key Options**:
   ```prisma
   // Auto-incrementing integer
   id Int @id @default(autoincrement())
   
   // UUID (random unique string)
   id String @id @default(uuid())
   
   // CUID (shorter unique string)
   id String @id @default(cuid())
   ```

6. **Unique Constraints**:
   ```prisma
   email String @unique  // Single unique field
   
   // Compound unique (combination must be unique)
   @@unique([email, username])
   ```

7. **Indexes for Performance**:
   ```prisma
   email String @unique  // Automatically indexed
   
   // Manual index
   @@index([email])
   
   // Compound index
   @@index([lastName, firstName])
   ```

8. **Database Providers**:
   ```prisma
   provider = "postgresql"  // Recommended for production
   provider = "mysql"
   provider = "sqlite"      // Good for development
   provider = "sqlserver"
   provider = "mongodb"
   provider = "cockroachdb"
   ```

9. **Environment Variables**:
   ```prisma
   url = env("DATABASE_URL")
   ```
   
   In your .env file:
   ```
   DATABASE_URL="postgresql://user:password@localhost:5432/mydb"
   ```

10. **Complete Example**:
    ```prisma
    datasource db {
      provider = "postgresql"
      url      = env("DATABASE_URL")
    }
    
    generator client {
      provider = "prisma-client-js"
    }
    
    model User {
      id        Int      @id @default(autoincrement())
      email     String   @unique
      name      String
      age       Int?
      isActive  Boolean  @default(true)
      createdAt DateTime @default(now())
      updatedAt DateTime @updatedAt
    }
    ```