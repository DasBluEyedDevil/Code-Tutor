---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine testing a car's engine. You don't need the ACTUAL wheels, fuel tank, and exhaust - you just need to verify the engine works!

Same with code:
• Your class (Engine) depends on other classes (Database, EmailService, PaymentAPI)
• In tests, you don't want REAL database calls or emails sent!
• Use MOCKS - fake versions that simulate dependencies

MOCKING FRAMEWORKS:
• Moq - Most popular, fluent API, Setup().Returns()
• NSubstitute - Clean syntax, Substitute.For<T>()
• FakeItEasy - Easy to read, A.Fake<T>()

WHY MOCK?
• Isolate unit under test
• Control dependency behavior
• Avoid side effects (real DB, emails, payments)
• Test edge cases (simulate errors, timeouts)
• Fast tests (no network/disk I/O)

Think: 'Mocks are stunt doubles - they look like the real thing but are safe for testing!'