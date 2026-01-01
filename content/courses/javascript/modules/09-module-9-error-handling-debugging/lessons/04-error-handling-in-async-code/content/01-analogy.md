---
type: "ANALOGY"
title: "The Delayed Letter"
---

Imagine you're sending a letter through the post.

1.  **Synchronous Error:** You try to put the letter in the mailbox, but the mailbox is locked. You know **immediately** that it failed. You're still standing right there to fix it.
2.  **Asynchronous Error:** You successfully put the letter in the mailbox and walk away. Three days later, the letter is returned to you because the address was wrong. You weren't standing at the mailbox when the "error" happened.

In JavaScript, asynchronous errors happen "later," after the main program has already moved on. If you use a simple `try/catch` around an async task without the right keywords, you'll be long gone by the time the crash happens, and the "blast shield" won't be there to protect you.

Async error handling is about making sure someone is still standing by the mailbox to catch the "Returned to Sender" notice.
