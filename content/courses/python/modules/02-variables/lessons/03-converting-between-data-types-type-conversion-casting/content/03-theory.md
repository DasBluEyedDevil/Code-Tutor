---
type: "THEORY"
title: "Syntax Breakdown"
---

Let's understand the three main conversion functions:

### 1. int() - Convert to Integer

- **What it does:** Converts text or float to a whole number

  ```python
  int("42")    # "42" (string) → 42 (int)
  int(3.9)     # 3.9 (float) → 3 (int)  ⚠️ Loses .9!
  int(3.1)     # 3.1 (float) → 3 (int)  ⚠️ Loses .1!
  ```

- **Important:** int() doesn't round—it **truncates** (cuts off) decimals

  ```python
  int(9.9)  # Result: 9 (not 10!)
  int(9.1)  # Result: 9
  ```
  
  Think of it like cutting pizza: int(3.9) gives you 3 *whole* slices. The 0.9 piece is thrown away!

- **Common error:** Converting invalid strings

  ```python
  int("hello")    # ❌ ValueError: invalid literal
  int("12.5")     # ❌ ValueError: can't convert "12.5" directly
  int(float("12.5"))  # ✅ Works! Convert to float first, then int
  ```

### 2. float() - Convert to Decimal Number

- **What it does:** Converts text or integer to a decimal number

  ```python
  float("3.14")    # "3.14" (string) → 3.14 (float)
  float("10")      # "10" (string) → 10.0 (float)
  float(5)         # 5 (int) → 5.0 (float)
  ```

- **Always adds a decimal point:**

  ```python
  float(100)  # Result: 100.0 (not 100)
  ```

- **Can handle string decimals:**

  ```python
  price = float("19.99")  # ✅ Works perfectly
  temp = float("98.6")    # ✅ Works perfectly
  ```

### 3. str() - Convert to String (Text)

- **What it does:** Converts anything to text

  ```python
  str(42)      # 42 (int) → "42" (string)
  str(3.14)    # 3.14 (float) → "3.14" (string)
  str(True)    # True (bool) → "True" (string)
  ```

- **Why you need it:** To combine numbers with text

  ```python
  # ❌ Doesn't work:
  print("Age: " + 25)  # TypeError

  # ✅ Works:
  print("Age: " + str(25))  # "Age: 25"

  # ✅ Better way (f-string):
  print(f"Age: {25}")  # "Age: 25"
  ```

- **str() works on everything:**

  ```python
  str(100)      # "100"
  str(3.14)     # "3.14"
  str(True)     # "True"
  str([1,2,3])  # "[1, 2, 3]"
  ```

### 4. Implicit Conversion (Python's Autopilot)

- **When Python auto-converts:**

  ```python
  result = 5 + 2.5  # Python converts 5 → 5.0, then adds
  # 5.0 + 2.5 = 7.5
  ```
  
  Python always promotes to the "higher" type (float is higher than int)

- **Rule:** int + float = float

  ```python
  10 + 5.0   # 15.0 (float)
  3 * 2.5    # 7.5 (float)
  100 / 2    # 50.0 (float) ← Division ALWAYS gives float!
  ```

### 5. Common Conversion Patterns

| From | To | Function | Example |
| :--- | :--- | :--- | :--- |
| String | Integer | `int()` | `int("42")` → `42` |
| String | Float | `float()` | `float("3.14")` → `3.14` |
| Integer | String | `str()` | `str(42)` → `"42"` |
| Float | String | `str()` | `str(3.14)` → `"3.14"` |
| Float | Integer | `int()` | `int(3.9)` → `3` ⚠️ |
| Integer | Float | `float()` | `float(5)` → `5.0` |

### 6. The Golden Rule
**If you're not sure what type something is, check it:**

```python
mystery_value = "100"
print(type(mystery_value))  # <class 'str'>

converted = int(mystery_value)
print(type(converted))      # <class 'int'>
```

Always use `type()` when debugging conversion issues!
