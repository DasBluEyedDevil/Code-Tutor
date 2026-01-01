---
type: "ANALOGY"
title: "Understanding the Concept"
---

You've learned to CREATE collections (arrays, lists, dictionaries). Now learn to WORK with them efficiently!

The FOREACH loop is your best friend for collections:

❌ OLD WAY (with for loop):
for (int i = 0; i < names.Length; i++)
{
    Console.WriteLine(names[i]);
}

✅ NEW WAY (with foreach):
foreach (string name in names)
{
    Console.WriteLine(name);
}

Foreach is:
✅ Simpler - no index variable needed
✅ Safer - can't go out of bounds
✅ Cleaner - less code to write
✅ Works with ALL collections (arrays, lists, dictionaries)

Think: foreach = "for each item in this collection, do something"