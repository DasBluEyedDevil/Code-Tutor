---
type: "WARNING"
title: "Common Architecture Mistakes Recap"
---

### Mistakes That Kill Projects

**1. Starting with Over-Architecture**
- Build the simplest thing first
- Add layers when pain points emerge

**2. Ignoring Platform Conventions**
- iOS users expect different navigation patterns
- Don't fight the platform

**3. Coupling to Frameworks**
- Keep domain layer pure
- Frameworks change; business logic shouldn't

**4. Forgetting Error Handling**
- Every async operation can fail
- Always have loading/error/success states

**5. No Testing Strategy**
- Architecture enables testing
- If you can't test it, refactor it

### The Golden Rule

> **Make it work, make it right, make it fast** - in that order.

Don't architect for problems you don't have yet.