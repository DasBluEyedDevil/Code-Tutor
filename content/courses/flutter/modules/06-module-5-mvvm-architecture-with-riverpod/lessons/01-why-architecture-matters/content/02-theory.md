---
type: "THEORY"
title: "Why Is This Code Problematic?"
---

That code above might seem fine for a small app. But as your app grows, these problems become nightmares:

### Problem 1: Impossible to Test
How do you test if the UI displays correctly? You cannot, because testing the UI means also calling the real API! You cannot test pieces in isolation.

### Problem 2: Changes Break Everything
If the API changes its URL or response format, you must hunt through every widget that makes that call. Miss one? Your app crashes.

### Problem 3: Code Duplication
Need user data on another screen? You copy-paste the same loading logic. Now you have the same code in 5 places, and a bug fix requires changing all 5.

### Problem 4: Hard to Understand
New team members cannot understand what the code does. Is this a UI file? An API file? A data file? It is all three, which makes it none of them properly.

### Problem 5: Team Collaboration Is Difficult
Two developers cannot work on the same screen. One is changing the UI, another is fixing an API bug. Their changes conflict because everything is in one file.

**The house analogy applies here:** If your plumber, electrician, and painter all work in the same room at the same time with no plan, chaos ensues. Architecture gives each person (or piece of code) a clear job and boundary.