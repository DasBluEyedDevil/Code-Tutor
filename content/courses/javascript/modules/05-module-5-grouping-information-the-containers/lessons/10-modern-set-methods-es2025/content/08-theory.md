---
type: "THEORY"
title: "Breaking Down the Syntax"
---

**Set Basics:**
```javascript
new Set()              // Empty set
new Set([1, 2, 3])     // Set from array
set.add(value)         // Add item (returns set)
set.delete(value)      // Remove item (returns boolean)
set.has(value)         // Check existence (returns boolean)
set.size               // Number of items
set.clear()            // Remove all items
[...set]               // Convert to array
```

**New ES2025 Methods:**

**set.union(otherSet)**
- Returns new Set with all items from both
- No duplicates
- Original sets unchanged

**set.intersection(otherSet)**
- Returns new Set with items in BOTH sets
- Only common elements

**set.difference(otherSet)**
- Returns items in set but NOT in otherSet
- Order matters! A.difference(B) !== B.difference(A)

**set.symmetricDifference(otherSet)**
- Returns items in EITHER set but not BOTH
- Same as (A union B) - (A intersection B)

**set.isSubsetOf(otherSet)**
- Returns true if ALL items are in otherSet
- Empty set is subset of everything

**set.isSupersetOf(otherSet)**
- Returns true if set CONTAINS all items of otherSet
- Opposite of isSubsetOf

**set.isDisjointFrom(otherSet)**
- Returns true if sets have NO common items
- intersection would be empty