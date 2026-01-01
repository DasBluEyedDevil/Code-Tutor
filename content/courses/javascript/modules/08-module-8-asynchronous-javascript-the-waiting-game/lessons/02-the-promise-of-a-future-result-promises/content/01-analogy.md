---
type: "ANALOGY"
title: "Understanding the Concept"
---

You order a pizza for delivery:

1. **You place the order** (create a Promise) - The restaurant promises to either deliver your pizza OR call you with a problem.

2. **You wait** (Promise is 'pending') - The pizza is being made. You don't have it yet, but you have a promise.

3. **Two possible outcomes**:
   - **Fulfilled (Resolved)**: Pizza arrives! You eat it (the 'then' callback runs)
   - **Rejected**: Restaurant calls - they're out of ingredients (the 'catch' callback runs)

A Promise is JavaScript's way of saying: 'I'll get you a result eventually. Here's a guarantee (promise) that I'll let you know when it's done, whether it succeeds or fails.'