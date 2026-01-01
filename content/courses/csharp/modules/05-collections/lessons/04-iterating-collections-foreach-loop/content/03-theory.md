---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`foreach (type item in collection)`**: Iterates through each element. 'type' matches collection type (int for int[], string for string[]). 'item' is current element. 'collection' is what to iterate.

**`.Count vs .Length`**: Lists use .Count property. Arrays use .Length property. Both tell you number of elements. No parentheses - they're properties not methods!

**`.Add(item)`**: Adds item to end of List. list.Add(5) appends 5. Can't use with arrays - they're fixed size!

**`.Contains(item)`**: Returns true if item exists in collection, false otherwise. Works with Lists, arrays (using Array methods), dictionaries. list.Contains("Bob") checks if "Bob" is in list.

**`Array.Sort() / list.Sort()`**: Sorts collection in place. Array.Sort(array) for arrays. list.Sort() for lists. Ascending order (smallest to largest, A to Z).