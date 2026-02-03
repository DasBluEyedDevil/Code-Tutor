---
type: "EXAMPLE"
title: "Service Layer Pattern"
---

The service layer is where your core business logic lives. Services are classes or modules that encapsulate operations related to a specific domain concept like users, orders, or payments. Services are independent of HTTP concerns - they do not know about requests, responses, or status codes. This independence makes services highly testable and reusable. A handler calls a service method, and the service returns data or throws an error. The handler then decides how to format the response. Services typically call the database layer for persistence and may call other services for complex operations. This layered approach creates clean boundaries between concerns.

```typescript
// src/services/user.service.ts
// Service layer contains business logic - no HTTP concerns!

import { UsersDB } from '../db/users.db';
import { EmailService } from './email.service';
import { hashPassword, verifyPassword } from '../utils/hash';
import { CreateUserInput, UpdateUserInput, User } from '../types/user.types';

export class UserService {
  private db: UsersDB;
  private emailService: EmailService;

  constructor() {
    this.db = new UsersDB();
    this.emailService = new EmailService();
  }

  // List users with pagination
  async listUsers(page: number, limit: number): Promise<{ users: User[]; total: number }> {
    const offset = (page - 1) * limit;
    
    const [users, total] = await Promise.all([
      this.db.findMany({ offset, limit }),
      this.db.count()
    ]);
    
    // Remove sensitive fields before returning
    const sanitized = users.map(this.sanitizeUser);
    
    return { users: sanitized, total };
  }

  // Get single user by ID
  async getUserById(id: string): Promise<User | null> {
    const user = await this.db.findById(id);
    return user ? this.sanitizeUser(user) : null;
  }

  // Get user by email (for auth and uniqueness checks)
  async getUserByEmail(email: string): Promise<User | null> {
    return this.db.findByEmail(email.toLowerCase());
  }

  // Create new user with business rules
  async createUser(input: CreateUserInput): Promise<User> {
    // Business rule: normalize email
    const normalizedEmail = input.email.toLowerCase().trim();
    
    // Business rule: hash password before storage
    const hashedPassword = await hashPassword(input.password);
    
    // Create user in database
    const user = await this.db.create({
      ...input,
      email: normalizedEmail,
      password: hashedPassword,
      createdAt: new Date(),
      updatedAt: new Date()
    });
    
    // Business rule: send welcome email
    await this.emailService.sendWelcomeEmail(user.email, user.name);
    
    return this.sanitizeUser(user);
  }

  // Update user with partial data
  async updateUser(id: string, input: UpdateUserInput): Promise<User | null> {
    const existing = await this.db.findById(id);
    if (!existing) {
      return null;
    }
    
    // Build update object with only provided fields
    const updates: Partial<User> = {
      ...input,
      updatedAt: new Date()
    };
    
    // If password is being changed, hash it
    if (input.password) {
      updates.password = await hashPassword(input.password);
    }
    
    // If email is being changed, normalize it
    if (input.email) {
      updates.email = input.email.toLowerCase().trim();
    }
    
    const updated = await this.db.update(id, updates);
    return updated ? this.sanitizeUser(updated) : null;
  }

  // Delete user
  async deleteUser(id: string): Promise<boolean> {
    const existing = await this.db.findById(id);
    if (!existing) {
      return false;
    }
    
    // Business rule: soft delete or hard delete based on requirements
    await this.db.delete(id);
    
    // Business rule: send account deletion confirmation
    await this.emailService.sendAccountDeletedEmail(existing.email);
    
    return true;
  }

  // Authenticate user (called by auth service)
  async authenticateUser(email: string, password: string): Promise<User | null> {
    const user = await this.db.findByEmail(email.toLowerCase());
    
    if (!user) {
      return null;
    }
    
    const isValid = await verifyPassword(password, user.password);
    
    if (!isValid) {
      // Business rule: track failed login attempts
      await this.db.incrementFailedLogins(user.id);
      return null;
    }
    
    // Reset failed login counter on success
    await this.db.resetFailedLogins(user.id);
    
    return this.sanitizeUser(user);
  }

  // Private helper to remove sensitive fields
  private sanitizeUser(user: User): User {
    const { password, failedLoginAttempts, ...safe } = user as any;
    return safe;
  }
}

// ============================================================
// src/db/users.db.ts
// Database layer - only handles data persistence
// ============================================================

import { db } from './index';
import { User } from '../types/user.types';

export class UsersDB {
  async findMany(options: { offset: number; limit: number }): Promise<User[]> {
    // Using Drizzle ORM as an example
    return db.select()
      .from(users)
      .limit(options.limit)
      .offset(options.offset);
  }

  async findById(id: string): Promise<User | null> {
    const result = await db.select()
      .from(users)
      .where(eq(users.id, id))
      .limit(1);
    return result[0] || null;
  }

  async findByEmail(email: string): Promise<User | null> {
    const result = await db.select()
      .from(users)
      .where(eq(users.email, email))
      .limit(1);
    return result[0] || null;
  }

  async create(data: Omit<User, 'id'>): Promise<User> {
    const id = crypto.randomUUID();
    await db.insert(users).values({ id, ...data });
    return { id, ...data } as User;
  }

  async update(id: string, data: Partial<User>): Promise<User | null> {
    await db.update(users).set(data).where(eq(users.id, id));
    return this.findById(id);
  }

  async delete(id: string): Promise<void> {
    await db.delete(users).where(eq(users.id, id));
  }

  async count(): Promise<number> {
    const result = await db.select({ count: sql`count(*)` }).from(users);
    return Number(result[0].count);
  }

  async incrementFailedLogins(id: string): Promise<void> {
    await db.update(users)
      .set({ failedLoginAttempts: sql`failed_login_attempts + 1` })
      .where(eq(users.id, id));
  }

  async resetFailedLogins(id: string): Promise<void> {
    await db.update(users)
      .set({ failedLoginAttempts: 0 })
      .where(eq(users.id, id));
  }
}

// ============================================================
// Why this layering matters:
// ============================================================
//
// 1. TESTABILITY: You can test UserService without HTTP or database
//    - Mock UsersDB to test business logic in isolation
//    - Mock EmailService to test without sending real emails
//    - Test handlers with mocked services
//
// 2. REUSABILITY: Services can be called from anywhere
//    - REST API handlers
//    - GraphQL resolvers
//    - CLI scripts
//    - Background jobs
//
// 3. MAINTAINABILITY: Changes are localized
//    - Switch from PostgreSQL to MongoDB? Only change db/ files
//    - Change email provider? Only change EmailService
//    - Add caching? Add it in the service layer
//
// 4. CLARITY: Each layer has one job
//    - Routes: URL mapping
//    - Handlers: HTTP request/response
//    - Services: Business logic
//    - DB: Data persistence
```
