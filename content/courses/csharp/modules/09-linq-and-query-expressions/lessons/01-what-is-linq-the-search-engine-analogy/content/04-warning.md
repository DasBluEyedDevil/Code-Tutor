---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Forgetting 'using System.Linq'**: Without this namespace import, you'll get 'Where does not exist' errors! LINQ methods are extension methods that require the System.Linq namespace.

**Deferred Execution Gotcha**: The query doesn't run when you create it! `var result = list.Where(...)` just defines the query. The actual work happens when you iterate (foreach) or call .ToList(), .Count(), etc. If the source collection changes between query creation and execution, you'll get the NEW values!

**Modifying source during iteration**: Never modify the source collection while iterating over LINQ results! You'll get 'Collection was modified' exception.

**Multiple enumeration performance**: Each time you iterate over an IEnumerable, the query re-executes. If you need to use results multiple times, call .ToList() once to materialize the results.