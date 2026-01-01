---
type: "THEORY"
title: "What to Test (and What Not to Test)"
---


Not all code needs the same level of testing. Understanding what to prioritize helps you write effective tests efficiently.

**High Priority (Always Test):**

1. **Business Logic**: Core algorithms, calculations, validation rules
   - Price calculations, discounts, taxes
   - User eligibility checks
   - Data transformation functions

2. **Edge Cases**: Boundary conditions and unusual inputs
   - Empty arrays, null values, maximum values
   - Invalid input handling
   - Race conditions in async code

3. **Security-Sensitive Code**: Authentication, authorization, data protection
   - Password validation and hashing
   - Token verification
   - Permission checks

4. **Complex Conditional Logic**: Code with multiple branches
   - State machines
   - Feature flags
   - Multi-step workflows

**Medium Priority:**

5. **Data Access Layers**: Repositories and database operations
   - CRUD operations
   - Query builders
   - Data mapping

6. **External Service Integration**: API clients and adapters
   - Request/response formatting
   - Error handling for external failures

**Low Priority (Often Skip):**

7. **Simple Getters/Setters**: Trivial property access
8. **Framework Code**: Trust that Dart Frog/Serverpod works
9. **Configuration Files**: Static data that rarely changes
10. **Generated Code**: Code from build_runner, etc.

**The 80/20 Rule**: 20% of your code causes 80% of bugs. Focus testing on that critical 20%.

