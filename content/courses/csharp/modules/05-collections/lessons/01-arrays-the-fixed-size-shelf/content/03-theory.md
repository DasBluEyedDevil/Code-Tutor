---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`int[] arrayName`**: Square brackets [] after the type means 'array of this type'. int[] means array of integers, string[] means array of strings.

**`new int[5]`**: Creates an array with 5 slots. The number in brackets is the SIZE. Once created, this size can NEVER change!

**`arrayName[index]`**: Access an item using square brackets and the index. Remember: indexes start at 0! An array of size 5 has indexes 0, 1, 2, 3, 4.

**`.Length`**: The Length property tells you how many items the array holds. arrayName.Length is VERY useful in loops!