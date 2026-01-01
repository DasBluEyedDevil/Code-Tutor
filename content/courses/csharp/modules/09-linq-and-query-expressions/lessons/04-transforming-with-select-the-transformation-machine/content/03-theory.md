---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`collection.Select(x => transformation)`**: Select() transforms each item. Returns IEnumerable with transformed items. EVERY item is transformed (unlike Where which filters).

**`Same type transform`**: numbers.Select(n => n * 2) takes int, returns int. But creates NEW collection - original unchanged!

**`Type change transform`**: people.Select(p => p.Name) takes Person, returns string. Output type changes! IEnumerable<Person> → IEnumerable<string>.

**`Anonymous object: new { }`**: Create unnamed object on-the-fly: 'new { Prop1 = value, Prop2 = value }'. Great for reshaping data! Type inferred by compiler.