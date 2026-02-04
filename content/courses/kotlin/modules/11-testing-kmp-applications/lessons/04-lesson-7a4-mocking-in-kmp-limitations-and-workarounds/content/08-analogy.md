---
type: "ANALOGY"
title: "Fakes as Stunt Doubles"
---

Test doubles (fakes) in KMP are like stunt doubles in movies.

**The real actor (production repository)** does complex, dangerous stunts—talks to real databases, makes network calls, handles real authentication. These are slow, expensive, and require elaborate setup.

**Mocking frameworks** are like CGI—they can perfectly fake anything with magic (reflection, bytecode manipulation). They're powerful but only work in certain studios (JVM). If you're filming on location (iOS, Native), CGI isn't available.

**Fakes are actual stunt doubles**—real people who look like the actor and can perform the stunts safely. A `FakeUserRepository` is a real implementation that stores data in memory instead of a database. It's not magic; it's a hand-written substitute that looks and acts like the real thing, but works anywhere you film.

**You train your stunt double (write the fake class)** to mimic the actor's moves. More work upfront than CGI (mocking), but the stunt double works on any location (all KMP platforms) and performs predictably in every scene (deterministic tests).

In KMP, you can't rely on CGI (mocking libraries), so you hire stunt doubles (write fakes) to test your action sequences (business logic) safely across all platforms.
