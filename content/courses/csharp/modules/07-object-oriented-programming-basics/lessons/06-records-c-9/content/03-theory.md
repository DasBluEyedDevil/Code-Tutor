---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`public record Person(string Name, int Age);`**: This ONE line creates: a class with Name and Age properties, a constructor that sets them, ToString(), Equals(), GetHashCode(), and deconstructor!

**`person1 == person2`**: Unlike classes (which compare references), records compare VALUES. Two records with identical data are equal, even if they're different objects in memory.

**`person1 with { Age = 31 }`**: The 'with' expression creates a COPY with some properties changed. Original stays unchanged! This is how you 'modify' immutable data.

**`Positional parameters`**: The (string Name, int Age) are called positional parameters. They become init-only properties and constructor parameters automatically.

**`record vs record class vs record struct`**: 'record' and 'record class' are the same (reference types). 'record struct' (C# 10+) creates a value type record.