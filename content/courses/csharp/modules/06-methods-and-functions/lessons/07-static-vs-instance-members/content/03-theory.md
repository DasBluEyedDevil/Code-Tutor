---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`public static int Count`**: STATIC field - ONE copy shared by all instances. Access via ClassName.FieldName, not objectName.FieldName!

**`public string Name`**: INSTANCE field (no 'static') - EACH object has its own copy. Access via objectName.FieldName.

**`public static void Method()`**: STATIC method - called through class name: ClassName.Method(). Can only access static members! Can't use 'this' or instance fields.

**`Player.TotalPlayers vs p1.Score`**: Static members use CLASS NAME (Player.TotalPlayers). Instance members use OBJECT (p1.Score). This is how you know which is which!

**`Math.PI, Console.WriteLine`**: These are static! Math.PI (static field), Console.WriteLine (static method). You don't create Math or Console objects - you use the class directly!