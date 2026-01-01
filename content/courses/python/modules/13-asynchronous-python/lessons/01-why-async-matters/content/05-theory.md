---
type: "THEORY"
title: "Python 3.14+ Free-Threaded Mode (Experimental)"
---

**The Future: True Parallelism in Python**

Python 3.14 introduces an experimental **free-threaded mode** that removes the Global Interpreter Lock (GIL). This is a major change for Python's concurrency model.

**What is the GIL?**
- The Global Interpreter Lock prevents multiple threads from executing Python bytecode simultaneously
- This means threads in Python take turns, not truly run in parallel
- Async works around this by using cooperative multitasking for I/O

**Free-Threaded Mode (Python 3.14+):**
```python
# To run Python 3.14 in free-threaded mode:
# python3.14t script.py  # Note the 't' suffix

# Check if running free-threaded:
import sys
print(sys._is_gil_enabled())  # False = free-threaded mode
```

**What This Means:**
- True parallelism for CPU-bound tasks using threads
- Better performance for multi-threaded applications
- Async still best for I/O-bound tasks (simpler, lower overhead)

**Current Status:**
- Still experimental, opt-in only
- Use async for I/O-bound tasks (proven, stable)
- Test free-threaded mode for CPU-bound parallelism

**Rule of Thumb (Updated):**
- **I/O-bound** -> Use `async/await` (best for waiting)
- **CPU-bound (Python 3.13-)** -> Use `multiprocessing`
- **CPU-bound (Python 3.14+)** -> Test free-threaded threads OR multiprocessing