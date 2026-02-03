---
type: "WARNING"
title: "Common Pitfalls"
---

## Order Processing Pitfalls

**Database Migrations in Production Without a Plan**: Running `dotnet ef database update` directly against a production database is risky. A failed migration can leave the database in a partially migrated state. Generate SQL scripts with `dotnet ef migrations script`, review them, test against a staging copy, and apply through your CI/CD pipeline with a rollback plan.

**Missing Transaction Boundaries**: Creating an order involves multiple operations: deducting inventory, creating the order record, clearing the cart, and publishing events. If any step fails without a transaction, you get inconsistent state (inventory deducted but no order created). Wrap related operations in a single database transaction using `BeginTransactionAsync()`.

**Connection String Security**: Never commit database connection strings with real credentials to source control. Even in private repositories, credentials in commit history persist forever. Use `dotnet user-secrets` for development, environment variables for containers, and Azure Key Vault or AWS Secrets Manager for production.

**Not Handling Idempotency**: Network failures can cause a client to retry an order submission. Without idempotency keys, the retry creates a duplicate order. Accept a client-generated idempotency key, store it with the order, and return the existing order if the same key is submitted again.
