---
type: "KEY_POINT"
title: "Virtual Threads: The Game Changer"
---

Virtual threads are lightweight threads managed by the JVM, and the standard approach for concurrent Java applications:

VIRTUAL THREADS:
- Minimal memory (~1KB vs 1MB)
- JVM scheduling (not OS)
- Millions possible on a single machine
- Automatic unmounting during I/O

The magic:
Thread blocks on I/O -> JVM unmounts it from carrier thread
Carrier thread runs OTHER virtual threads
I/O completes -> Virtual thread remounts and continues

10,000 virtual threads on I/O:
- Only ~10 carrier threads actively running
- Memory: ~10MB instead of 10GB
- Same simple blocking code
- Massive scalability!

WRITE SIMPLE CODE, GET MASSIVE SCALE.