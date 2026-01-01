---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you're a manager delegating tasks:

'Hey team, START working on this report (don't block me!)'
'I'll go do other things'
'When the report is READY, I'll come back and review it'

That's async/await!

ASYNC keyword:
• Marks a method as asynchronous
• Enables 'await' keyword inside
• Method can be 'paused' and 'resumed'
• Returns Task or Task<T>

AWAIT keyword:
• 'Pause here until this completes'
• Doesn't block the thread! (Thread is released for other work)
• When done, execution continues from where it paused
• Can only use inside 'async' methods

Think of await like a bookmark: 'I'll pause here, do other things, and come back when this is ready.'

RULE: If a method is async, you should await it! (Unless you intentionally want fire-and-forget)