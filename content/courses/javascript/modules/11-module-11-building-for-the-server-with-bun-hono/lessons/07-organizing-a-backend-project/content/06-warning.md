---
type: "WARNING"
title: "Common Pitfalls"
---

Common project structure mistakes:

1. **Circular dependencies**:
   ```typescript
   // userService.ts imports orderService
   // orderService.ts imports userService
   // This creates a circular dependency!
   ```
   Solution: Extract shared logic into a separate module, or use dependency injection.

2. **Business logic in route handlers**:
   ```typescript
   // BAD: Logic mixed with HTTP handling
   app.post('/users', async (c) => {
     const hashedPassword = await bcrypt.hash(password, 10);
     const user = await db.insert(users).values({...});
     await sendEmail(user.email, 'Welcome!');
     return c.json(user);
   });
   ```
   Solution: Move all logic to services. Handlers should only handle HTTP.

3. **Hardcoded configuration**:
   ```typescript
   // BAD: Hardcoded values
   const db = connect('postgresql://localhost:5432/mydb');
   ```
   Solution: Always use environment variables for anything that varies by environment.

4. **Giant service files**:
   Services should be focused. If UserService has 2000 lines, consider splitting into UserAuthService, UserProfileService, and UserPreferencesService.

5. **Skipping the database layer**:
   ```typescript
   // BAD: Direct DB calls in service
   class UserService {
     async getUser(id) {
       return db.select().from(users).where(...);
     }
   }
   ```
   Solution: Create a separate DB layer. This makes it easier to add caching, switch databases, or mock for tests.

6. **Committing .env files**:
   Even once is dangerous - secrets remain in git history forever. Use pre-commit hooks to prevent this.

7. **Not validating environment at startup**:
   Missing config causes runtime errors. Validate everything when the application starts and fail fast with clear error messages.