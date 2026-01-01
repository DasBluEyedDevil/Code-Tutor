---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`List<string>`**: List<T> where T is the type. List<string> = list of strings, List<int> = list of integers. The angle brackets specify what type is stored!

**`.Add(item)`**: Adds an item to the END of the list. The list automatically expands to fit! add() is THE most common list operation.

**`.Remove(item)`**: Removes the FIRST occurrence of this item from the list. If the item isn't found, nothing happens (no error).

**`.Count`**: Like array's .Length but for lists! Use .Count to get the number of items. NOTE: It's Count not Length!

**`.Contains(item)`**: Returns true if the item exists in the list, false otherwise. Great for checking before adding or removing!