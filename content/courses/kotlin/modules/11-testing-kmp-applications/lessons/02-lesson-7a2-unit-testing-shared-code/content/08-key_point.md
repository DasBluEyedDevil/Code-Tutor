---
type: "KEY_POINT"
title: "Key Takeaways"
---

**kotlin.test provides multiplatform testing annotations** (`@Test`, `@BeforeTest`, `@AfterTest`) that compile to JUnit on JVM, XCTest on iOS. Write tests once in commonTest, run everywhere.

**Assertions use `kotlin.test` functions** like `assertEquals`, `assertTrue`, `assertFailsWith`. These work identically across platforms, abstracting platform-specific assertion libraries.

**Test only public APIs, not implementation details**—internal functions and private state shouldn't be tested directly. This keeps tests resilient to refactoring and focused on behavior contracts.
