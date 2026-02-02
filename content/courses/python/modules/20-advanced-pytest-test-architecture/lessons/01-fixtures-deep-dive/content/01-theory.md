---
type: "THEORY"
title: "Beyond Basic Fixtures"
---

You've used simple fixtures before. Now let's master advanced patterns:

**Fixture Scopes:**
- `function` (default) - New fixture for each test
- `class` - Shared within a test class
- `module` - Shared within a test file
- `package` - Shared within a package
- `session` - Shared across all tests

**Why scope matters:**
- Database connections - `session` scope (expensive to create)
- Test data - `function` scope (isolated between tests)
- Temp directories - `module` scope (shared within file)

**Rule of thumb:** Use the narrowest scope that still performs well.