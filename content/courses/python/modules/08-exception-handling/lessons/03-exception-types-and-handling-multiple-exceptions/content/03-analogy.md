---
type: "ANALOGY"
title: "Syntax Breakdown: Exception Handling Patterns"
---

**Common Built-in Exception Types:**

1. **ValueError:** Correct type, wrong value
   - int("abc") - can't convert to integer
   - float("not_a_number")
   
2. **TypeError:** Wrong type for operation
   - "hello" + 5 - can't add string and int
   - len(42) - len() expects iterable, not int
   
3. **IndexError:** Sequence index out of range
   - my_list[100] - index doesn't exist
   - Works with lists, tuples, strings
   
4. **KeyError:** Dictionary key doesn't exist
   - person["email"] - key not in dict
   - Use .get() to avoid this
   
5. **ZeroDivisionError:** Division or modulo by zero
   - 10 / 0
   - 10 % 0
   
6. **FileNotFoundError:** File doesn't exist
   - open("missing.txt")
   
7. **AttributeError:** Object lacks attribute/method
   - "hello".non_existent_method()
   
8. **NameError:** Variable not defined
   - print(undefined_variable)

**Handling Multiple Exceptions - Three Patterns:**

**Pattern 1: Separate except blocks (different handling)**
```python
try:
    risky_code()
except ValueError:
    handle_value_error()
except TypeError:
    handle_type_error()
except IndexError:
    handle_index_error()
```

**Pattern 2: Multiple exceptions, same handling**
```python
try:
    risky_code()
except (ValueError, TypeError, IndexError):
    # Handle all three the same way
    handle_error()
```

**Pattern 3: Specific first, general fallback**
```python
try:
    risky_code()
except ValueError:
    handle_value_error()  # Specific
except TypeError:
    handle_type_error()   # Specific
except Exception as e:
    handle_unknown_error(e)  # General fallback
```

**Exception Hierarchy (simplified):**
```
BaseException
├── Exception (catch most errors)
│   ├── ValueError
│   ├── TypeError
│   ├── IndexError
│   ├── KeyError
│   ├── ZeroDivisionError
│   ├── FileNotFoundError
│   ├── AttributeError
│   └── ... many more
├── KeyboardInterrupt (Ctrl+C)
└── SystemExit (sys.exit())
```

**Best Practices:**
- Catch SPECIFIC exceptions you expect (ValueError, FileNotFoundError)
- Order except blocks from SPECIFIC to GENERAL
- Avoid bare except: (catches everything, even Ctrl+C!)
- Use Exception as a last-resort fallback, not the primary catch