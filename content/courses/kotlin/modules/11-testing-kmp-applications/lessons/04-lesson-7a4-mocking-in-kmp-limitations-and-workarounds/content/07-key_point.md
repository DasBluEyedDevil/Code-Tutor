---
type: "KEY_POINT"
title: "Key Takeaways"
---

**MockK and Mockito don't work in commonTest**—they rely on JVM reflection unavailable in KMP. Use manual fakes or test doubles instead of mocking frameworks.

**Fakes are hand-written test implementations** of interfaces: `class FakeUserRepository : UserRepository` with in-memory storage. They're more work than mocks but provide deterministic, cross-platform testing.

**Design for testability: depend on interfaces, not classes**. If your code depends on concrete classes, you can't substitute test implementations. Interfaces + dependency injection enable manual test doubles.
