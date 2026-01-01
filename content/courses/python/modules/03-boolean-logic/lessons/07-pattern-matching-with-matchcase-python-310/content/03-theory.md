---
type: "THEORY"
title: "Syntax Breakdown"
---

### Basic match/case Syntax:

```python
match subject:
    case pattern1:
        # Code for pattern1
    case pattern2:
        # Code for pattern2
    case _:
        # Default case (matches anything)
```

### Pattern Types:

**1. Literal Patterns - Match exact values:**
```python
match status:
    case 200:
        print("OK")
    case 404:
        print("Not Found")
    case 500:
        print("Server Error")
```

**2. OR Patterns - Match multiple values:**
```python
match day:
    case "Saturday" | "Sunday":
        print("Weekend!")
    case "Monday" | "Tuesday" | "Wednesday" | "Thursday" | "Friday":
        print("Weekday")
```

**3. Wildcard Pattern - Match anything:**
```python
match value:
    case "specific":
        print("Matched specific")
    case _:  # Underscore matches ANYTHING
        print("Matched something else")
```

**4. Capture Patterns - Match and capture:**
```python
match point:
    case (x, y):  # Captures x and y variables
        print(f"Point at {x}, {y}")
```

**5. Guard Patterns - Add conditions:**
```python
match number:
    case n if n < 0:
        print("Negative")
    case n if n == 0:
        print("Zero")
    case n if n > 0:
        print("Positive")
```

**6. Sequence Patterns - Match lists/tuples:**
```python
match data:
    case []:  # Empty
        print("Empty")
    case [x]:  # Single element
        print(f"One item: {x}")
    case [x, y]:  # Exactly two
        print(f"Two items: {x}, {y}")
    case [first, *rest]:  # First + remaining
        print(f"First: {first}, others: {rest}")
```

**7. Dictionary Patterns - Match dicts:**
```python
match event:
    case {"type": "click", "x": x, "y": y}:
        print(f"Click at ({x}, {y})")
    case {"type": "keypress", "key": key}:
        print(f"Key pressed: {key}")
```

### Key Rules:

1. **Patterns are checked in order** - First match wins
2. **`case _:` should be last** - It catches everything
3. **Guards use `if`** - `case x if x > 0:` adds conditions
4. **Variables in patterns capture values** - `case (x, y):` creates x and y
5. **`|` means OR** - `case "a" | "b":` matches either

### Common Mistakes:

```python
# WRONG: Using = instead of match/case syntax
match value:
    case x = 5:  # ERROR! No = in patterns
        ...

# CORRECT:
match value:
    case 5:  # Literal match
        ...
    case x if x == 5:  # Guard condition
        ...
```

```python
# WRONG: Forgetting _ catches all
match status:
    case _:  # This catches EVERYTHING!
        print("Matched")
    case 200:  # Never reached!
        print("OK")

# CORRECT: Put _ last
match status:
    case 200:
        print("OK")
    case _:  # Catches everything else
        print("Unknown")
```