---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These!

**IndexOutOfRangeException**: The most common array error! If your array has 5 items (indexes 0-4), accessing array[5] CRASHES your program. Always check that index < array.Length!

**Off-by-one errors**: Since arrays start at 0, the LAST valid index is Length - 1. An array of size 5 has valid indexes 0, 1, 2, 3, 4 - NOT 5!

**Arrays are fixed size**: Once created, you CANNOT resize an array. If you need to add more items, you must create a new, bigger array and copy everything. Use List<T> instead if you need dynamic sizing!

**Default values**: When you create an array without initializing, items have default values (0 for numbers, null for objects, false for bool). Don't assume the array is truly 'empty'!