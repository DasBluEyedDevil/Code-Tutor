---
type: "THEORY"
title: "Exercise: Multi-Tenant Application"
---


Build a multi-tenant blog platform where each tenant has isolated data.

### Requirements

1. **Tenant Context**:
   - Extract tenant ID from request header: `X-Tenant-ID`
   - Store in request-scoped object

2. **Tenant-Specific Repositories**:
   - Each tenant has separate database schema
   - Repositories filter by tenant ID automatically

3. **Koin Scopes**:
   - Create request scope for tenant context
   - Inject tenant-aware repositories

4. **Implementation**:
   ```kotlin
   // Tenant context
   data class TenantContext(val tenantId: String)

   // Tenant-aware repository
   class TenantUserRepository(private val tenantContext: TenantContext) : UserRepository {
       override fun getAll(): List<User> {
           // Filter by tenantContext.tenantId
       }
   }
   ```

### Starter Code


---



```kotlin
val tenantModule = module {
    // TODO: Define request scope
    // TODO: Provide TenantContext from request header
    // TODO: Provide tenant-aware repositories
}

// TODO: Create middleware to extract tenant ID
// TODO: Inject tenant-aware repositories in routes
```
