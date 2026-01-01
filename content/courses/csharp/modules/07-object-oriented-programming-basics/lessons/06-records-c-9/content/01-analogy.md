---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you're filling out a government form. Once you write your name, address, and birthdate, you don't want anyone changing it - it's your official record!

That's what RECORDS are in C# - special classes designed to hold DATA that shouldn't change. They're perfect for:

- Data transfer objects (DTOs)
- Configuration values
- API responses
- Anything that represents 'facts' rather than 'things that do stuff'

Records give you:
- Immutability by default (data can't change after creation)
- Value-based equality (two records with same data are 'equal')
- Automatic ToString(), Equals(), and GetHashCode()
- Compact one-line syntax!

Think of a class as a person (can change clothes, mood, etc.) vs a record as a passport (fixed facts about a person).