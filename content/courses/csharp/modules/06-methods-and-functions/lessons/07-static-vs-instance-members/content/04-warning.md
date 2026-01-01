---
type: "WARNING"
title: "Common Pitfalls"
---

**Static methods can't access instance members!** Inside `static void Method()`, you cannot use `this.field` or any instance field/method. Static exists at the CLASS level, not object level.

**Accessing static through instances is bad practice!** While `player1.TotalPlayers` might compile, it's confusing! Always use `Player.TotalPlayers` for static members.

**Static state persists!** Static fields keep their values across all object creations. If `TotalPlayers = 5` and you create a new Player, it becomes 6 - not reset to 1!

**Thread safety warning:** In multi-threaded apps, static fields are shared across threads. Modifying them without locks can cause race conditions. For beginners, this is advanced - just be aware!