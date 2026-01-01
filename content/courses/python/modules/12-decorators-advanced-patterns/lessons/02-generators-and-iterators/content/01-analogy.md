---
type: "ANALOGY"
title: "The Concept: One Item at a Time"
---

**Generators = Lazy iterators**

**Think of a streaming service:**
- **List approach:** Download entire movie first, then watch
  - Uses lots of memory
  - Have to wait for full download

- **Generator approach:** Stream one frame at a time
  - Minimal memory
  - Start watching immediately
  - Only load what you need

**Why use generators?**

1. **Memory Efficient** 💾
   - Don't store all values
   - Generate on-demand
   - Perfect for large datasets

2. **Lazy Evaluation** 😴
   - Only compute when needed
   - Can represent infinite sequences

3. **Pipeline Processing** ⚡
   - Chain operations efficiently
   - Process streams of data

**Key difference:**
```python
# List - all at once
def get_numbers():
    return [1, 2, 3, 4, 5]  # All in memory

# Generator - one at a time
def get_numbers():
    yield 1
    yield 2
    yield 3
    yield 4
    yield 5  # Generated on demand
```