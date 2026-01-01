---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine borrowing a book from the library. Whether you finish reading it OR give up halfway, you MUST return the book! That's a 'finally' action - it happens NO MATTER WHAT.

The FINALLY BLOCK runs whether the try succeeds OR an exception is caught:
• Try succeeds → finally runs
• Exception caught → finally runs
• Exception NOT caught → finally runs (then exception propagates)

Use finally for CLEANUP: close files, release resources, disconnect from databases.

CUSTOM EXCEPTIONS: Sometimes built-in exceptions aren't specific enough. Create your own!
• InvalidAgeException for age validation
• InsufficientFundsException for banking
• GameOverException for game logic

Think: finally = 'Always do this, no matter what!' Custom exceptions = 'Create your own error types for your domain.'