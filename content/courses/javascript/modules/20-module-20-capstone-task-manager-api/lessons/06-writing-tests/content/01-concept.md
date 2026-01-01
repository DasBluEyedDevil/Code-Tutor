---
type: "CONCEPT"
title: "Testing Strategy - Unit vs Integration"
---

Professional APIs require comprehensive testing. Let's understand the testing pyramid and when to use each type.

**Testing Pyramid:**
```
        /\        
       /E2E\       End-to-End (few)
      /------\     
     /  Int   \    Integration (moderate)
    /---------\   
   /  Unit     \  Unit Tests (many)
  /__________*/
```

**Unit Tests** - Test individual functions in isolation
- Test utility functions (hashPassword, verifyPassword)
- Mock external dependencies
- Fast and focused
- Usually 80% of your tests

**Integration Tests** - Test how components work together
- Test API endpoints with real database
- Test authentication flows
- Test request/response cycles
- Usually 15-20% of your tests

**E2E Tests** - Test complete user flows (skip for this capstone)
- Browser-based testing
- Full application flow
- Usually 5% of your tests

**For This Capstone:**
We'll focus on unit and integration tests using Bun's built-in test runner, which is simple and fast:
```typescript
// Bun test pattern
describe('Auth Utils', () => {
  it('should hash passwords correctly', () => {
    // Test implementation
  });
});
```