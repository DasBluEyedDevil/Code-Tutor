---
type: "ANALOGY"
title: "The Concept: Automatic Cleanup"
---

**Context Managers = Guaranteed cleanup**

**Think of borrowing a library book:**
- ❌ **Without context manager:**
  ```
  1. Check out book
  2. Read it
  3. Forget to return it!
  4. Get fined
  ```

- ✅ **With context manager:**
  ```
  1. Auto check-out when you enter library
  2. Read
  3. Auto return when you leave
  4. No fines!
  ```

**The problem they solve:**
```python
# Easy to forget cleanup!
file = open('data.txt')
data = file.read()
# Oops, forgot file.close()!
# File handle stays open
```

```python
# Context manager guarantees cleanup
with open('data.txt') as file:
    data = file.read()
# File automatically closed, even if error!
```

**Common use cases:**
1. **File handling** 📁 - Auto close files
2. **Database connections** 🗄️ - Auto close connections
3. **Locks** 🔒 - Auto release locks
4. **Transactions** 💳 - Auto commit/rollback
5. **Temporary state** ⏱️ - Auto restore state

**Key benefit:** Cleanup happens EVEN IF ERRORS OCCUR!