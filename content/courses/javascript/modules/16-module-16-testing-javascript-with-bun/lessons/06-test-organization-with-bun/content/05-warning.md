---
type: "WARNING"
title: "Test Organization Pitfalls"
---

Common organization mistakes:

1. **Wrong hook usage**:
   ```javascript
   // WRONG - expensive setup runs before EVERY test
   beforeEach(() => {
     db = await connectToDatabase();  // Slow!
   });
   
   // CORRECT - connect once, reset data each test
   beforeAll(() => db = await connectToDatabase());
   beforeEach(() => await db.clear());
   afterAll(() => await db.disconnect());
   ```

2. **Missing afterAll cleanup**:
   Open connections, temp files, and running servers leak between test files.

3. **Tests that rely on order**:
   ```javascript
   // WRONG - test 2 depends on test 1
   it('creates user', () => { db.users.push({id: 1}); });
   it('finds user', () => { expect(db.users[0]).toBeDefined(); });
   
   // CORRECT - each test is independent
   it('finds user', () => {
     db.users.push({id: 1});  // Own setup
     expect(db.users[0]).toBeDefined();
   });
   ```

4. **Too many files in one directory**:
   Co-locate tests with source files, not in a giant /tests folder.