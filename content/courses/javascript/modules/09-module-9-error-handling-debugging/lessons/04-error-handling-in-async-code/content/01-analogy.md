---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you take a new medication and the doctor warns you about potential side effects. Some side effects happen immediately - you take the pill and feel dizzy right away. But other side effects are delayed - you might not have a reaction until hours or days later when the medication has had time to work through your system.

Synchronous errors are like immediate side effects - they happen right when you run the code, and try-catch catches them instantly. But async errors are like delayed reactions. If you wrap an async operation in a regular try-catch and walk away, when the delayed error happens, there's no one there to catch it.

This is why async error handling requires special attention. You need to set up error handling that's still 'listening' when the async operation eventually fails. With async/await, you use try-catch that waits for the operation. With Promises, you use .catch() that stays attached to the promise. Either way, you need error handling that's patient enough to wait for delayed problems.