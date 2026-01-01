---
type: "THEORY"
title: "Understanding StatefulWidget"
---


**Two classes work together:**

1. **Widget class** (`Counter`) - Immutable configuration
2. **State class** (`_CounterState`) - Mutable state

**Why?** Widgets rebuild often. State persists across rebuilds.

