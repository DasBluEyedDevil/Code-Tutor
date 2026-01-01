---
type: "WARNING"
title: "Database Test Best Practices"
---

Follow these guidelines for reliable database tests:

1. **Always Clean Up**: Use tearDown to truncate tables or roll back transactions

2. **Avoid Test Dependencies**: Each test should create its own data, never rely on data from other tests

3. **Use Transactions**: Wrap tests in transactions that auto-rollback for faster cleanup

4. **Separate Test Database**: Never run tests against production or development databases

5. **Reset Sequences**: After truncating, reset auto-increment sequences to avoid ID conflicts

6. **Parallel Test Safety**: If running tests in parallel, use unique identifiers to avoid conflicts