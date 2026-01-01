---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`[1, 2, 3]`**: Square brackets with comma-separated values create a collection. The target type (int[], List<int>, etc.) determines what's created!

**`[]`**: Empty brackets create an empty collection. Much cleaner than new List<int>() or Array.Empty<int>()!

**`[..existing, newValue]`**: The SPREAD element '..' expands a collection inline. Perfect for combining collections or adding elements. Note: '..' is not an operator - it's part of the spread element syntax!

**`Target-typed`**: C# looks at what you're assigning to and creates the right type. 'int[] x = [1,2,3]' creates array, 'List<int> x = [1,2,3]' creates List!

**`Inline passing`**: You can pass collections directly to methods: DoSomething([1, 2, 3]) - no need to declare a variable first!