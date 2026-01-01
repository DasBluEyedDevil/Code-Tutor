---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine a school:

• **INSTANCE (each student)**: Each student has their OWN name, grade, age. John's grade is different from Mary's grade.

• **STATIC (shared by all)**: The school bell rings for EVERYONE. The school's name is the SAME for all students. These are SHARED, not personal.

In C#:
• **Instance members**: Each object has its own copy. player1.score is different from player2.score.
• **Static members**: Shared by ALL instances of the class. ONE copy for the whole class.

When to use static?
• Counters: How many Player objects exist? Player.Count (shared)
• Utility methods: Math.Sqrt(), Console.WriteLine() - don't need a specific object!
• Constants: Math.PI - same for everyone

Access static members through the CLASS NAME (Player.Count), not an object (player1.Count won't work)!