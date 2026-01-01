---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you have 1000 photos on your phone. You want to find:
• All photos from 2023
• Only photos of your dog
• Photos sorted by date

You could write a FOR LOOP to check each photo manually... OR you could use the built-in search feature! Much easier!

That's what LINQ is - a 'search engine' for collections in C#:
• LINQ = Language Integrated Query
• Works on any collection: arrays, Lists, Dictionaries
• Write SQL-like queries in C# code
• Much cleaner than manual loops!

Two syntaxes:
1. METHOD SYNTAX: list.Where(x => x > 5).OrderBy(x => x)
2. QUERY SYNTAX: from x in list where x > 5 orderby x select x

Most developers use METHOD syntax (we'll focus on that!).

Think: LINQ = 'Asking your collection smart questions instead of manually digging through it.'