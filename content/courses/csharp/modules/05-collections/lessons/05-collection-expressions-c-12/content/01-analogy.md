---
type: "ANALOGY"
title: "Understanding the Concept"
---

Remember how creating collections used to be confusing? Arrays use { }, Lists need new List<T>(), and each has different syntax. It's like every store having different payment systems!

C# 12 introduces COLLECTION EXPRESSIONS - one universal syntax for ALL collections! Think of it like a universal remote that works with any TV.

Before: Different syntax everywhere!
- int[] arr = new int[] { 1, 2, 3 };
- List<int> list = new List<int> { 1, 2, 3 };
- Span<int> span = stackalloc int[] { 1, 2, 3 };

After: One syntax to rule them all!
- int[] arr = [1, 2, 3];
- List<int> list = [1, 2, 3];
- Span<int> span = [1, 2, 3];

The square brackets [ ] work for arrays, lists, spans, and more! Plus you get the SPREAD operator (..) to combine collections!