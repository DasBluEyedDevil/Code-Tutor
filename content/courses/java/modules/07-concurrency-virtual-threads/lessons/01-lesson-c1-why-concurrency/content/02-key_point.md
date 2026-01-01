---
type: "KEY_POINT"
title: "Concurrency vs Parallelism"
---

These terms are often confused:

CONCURRENCY:
- Managing multiple tasks that COULD run at the same time
- Like a chef juggling multiple dishes: start soup, while it simmers, chop vegetables
- Tasks interleave, not necessarily simultaneous
- About STRUCTURE

PARALLELISM:
- Actually running multiple tasks at the SAME time
- Like having 4 chefs working simultaneously
- Requires multiple CPU cores
- About EXECUTION

Concurrency enables parallelism, but they're different:
- Concurrency: Write code that CAN run simultaneously
- Parallelism: Actually RUN code simultaneously on multiple cores

Java gives you both: write concurrent code, and the JVM + OS handle parallel execution on your multi-core CPU.