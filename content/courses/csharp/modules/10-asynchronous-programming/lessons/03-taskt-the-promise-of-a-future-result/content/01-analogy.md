---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you order a package online:
• You get a TRACKING NUMBER immediately
• The package isn't here YET
• The tracking number is a PROMISE: 'Your package WILL arrive'
• You can check status, wait for it, or do other things
• When it arrives, you can open it and get the contents

That's Task<T>!

Task<T> represents:
• An ONGOING operation (might not be done yet)
• A PROMISE of a future result of type T
• You can:
  - Check if it's complete: task.IsCompleted
  - Wait for it: await task
  - Get the result: await task (returns T)
  - Run multiple: Task.WhenAll(), Task.WhenAny()

Task = async operation that returns nothing (void)
Task<T> = async operation that returns T

Think: Task<T> = 'I don't have the value NOW, but I WILL have it soon. Here's a promise!'