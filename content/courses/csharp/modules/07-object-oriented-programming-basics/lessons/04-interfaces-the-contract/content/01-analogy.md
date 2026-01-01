---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a job posting: 'Looking for employee who can: Drive, Operate forklift, Read inventory systems.' This is a CONTRACT - if you want the job, you MUST have these skills!

An INTERFACE is a contract for classes. It says: 'If you implement me, you MUST provide these methods.' Unlike abstract classes:
• NO implementation (just method signatures)
• A class can implement MULTIPLE interfaces (but only inherit from ONE class!)
• All members are public and abstract by default

Naming: Interfaces start with 'I' by convention: IDrawable, IPlayable, IComparable.

Think: IDrawable = 'anything that can be drawn'. Button, Image, Shape all implement IDrawable. They're completely different, but share the ability to be drawn!