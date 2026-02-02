---
type: "THEORY"
title: "Testing Strategy"
---

**Test Pyramid for Finance Tracker:**

```
                    /\
                   /  \           E2E Tests
                  / UI \          (Few, slow)
                 /------\
                /        \        Integration Tests
               /   API    \       (Some, medium)
              /------------\
             /              \     Unit Tests
            /    Models &    \    (Many, fast)
           /    Repositories  \
          /--------------------\
```

**What We'll Test:**
1. **Unit Tests**: Domain models, validation, calculations
2. **Repository Tests**: Database operations with test database
3. **API Tests**: HTTP endpoints with TestClient
4. **Integration Tests**: Full flows (register → login → create transaction)