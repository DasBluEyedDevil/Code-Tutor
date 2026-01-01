---
type: "WARNING"
title: "Common Testing Mistakes"
---

Avoid these pitfalls when writing tests:

**1. Not Cleaning Up Between Tests**
```typescript
// WRONG - Test pollution: data from one test affects another
describe('Tasks', () => {
  it('should create a task', async () => {
    await createTask('Test 1');
  });
  
  it('should list tasks', async () => {
    const tasks = await getTasks();
    // This test passes because previous test created Task
  });
});

// RIGHT - Use beforeEach to reset state
describe('Tasks', () => {
  beforeEach(async () => {
    await cleanupDatabase(prisma);
  });
  
  it('should create a task', async () => {
    await createTask('Test 1');
  });
  
  it('should list tasks (empty)', async () => {
    const tasks = await getTasks();
    expect(tasks).toEqual([]);
  });
});
```

**2. Testing Implementation Instead of Behavior**
```typescript
// WRONG - Testing how, not what
it('should hash password using bcrypt', () => {
  const hash = hashPassword('password');
  expect(hash).toContain('$2b$'); // Implementation detail!
});

// RIGHT - Testing behavior
it('should hash password securely', async () => {
  const hash = await hashPassword('password');
  const isValid = await verifyPassword('password', hash);
  expect(isValid).toBe(true);
});
```

**3. Async/Await Issues**
```typescript
// WRONG - Missing await
it('should create user', () => {
  createUser('test@example.com'); // Not awaited!
  expect(userExists('test@example.com')).toBe(true); // May fail randomly
});

// RIGHT - Await async operations
it('should create user', async () => {
  await createUser('test@example.com');
  expect(await userExists('test@example.com')).toBe(true);
});
```

**4. Testing Too Much in One Test**
```typescript
// WRONG - Too many assertions, hard to debug
it('should handle complete auth flow', async () => {
  const register = await register(...);
  expect(register.status).toBe(201);
  const login = await login(...);
  expect(login.status).toBe(200);
  const me = await getMe(...);
  expect(me.status).toBe(200);
  // 5 more assertions...
});

// RIGHT - One assertion per test (roughly)
it('should register successfully', async () => {
  const response = await register(...);
  expect(response.status).toBe(201);
});

it('should login successfully', async () => {
  await register(...);
  const response = await login(...);
  expect(response.status).toBe(200);
});
```

**5. Not Testing Error Cases**
```typescript
// WRONG - Only happy path
it('should create task', () => {
  const task = createTask('My Task');
  expect(task).toBeDefined();
});

// RIGHT - Test success and failures
it('should create valid task', () => {
  const task = createTask('My Task');
  expect(task).toBeDefined();
});

it('should reject task with empty title', () => {
  expect(() => createTask('')).toThrow();
});
```