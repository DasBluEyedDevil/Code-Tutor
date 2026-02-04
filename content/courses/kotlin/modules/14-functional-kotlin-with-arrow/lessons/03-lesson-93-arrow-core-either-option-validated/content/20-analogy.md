---
type: "ANALOGY"
title: "Either as Railway Switches"
---

`Either<E, A>` is like a railway track with switches that route trains to different destinations.

**Success track (`Right`) is the main line**—trains (data) move smoothly toward the destination (function completion) as long as nothing goes wrong. Each station (function) processes the cargo and sends it forward.

**Failure track (`Left`) is the detour line**—when something goes wrong (validation fails, network timeout), a switch routes the train onto the detour track. Once on the detour, the train bypasses all main-line stations and goes straight to the error terminal.

**`map` operations are stations on the main track**—they only process trains on the success track. If a train is already on the failure track, map stations don't touch it; it rolls past to the error terminal.

**`flatMap` operations can create new switches**—they process success-track trains but might route them onto the failure track if the next operation fails. Each `flatMap` is a decision point: continue on success or switch to failure.

**Unlike exceptions (emergency brakes), Either switches are controlled**—you see in the types whether a track has switches (`Either`) or not (`A`). Exceptions are hidden brakes that stop everything; Either switches are visible route changes you can plan for.

At the end of the line, you handle both destinations: `fold({ handleError(it) }, { handleSuccess(it) })` processes trains regardless of which track they arrived on.
