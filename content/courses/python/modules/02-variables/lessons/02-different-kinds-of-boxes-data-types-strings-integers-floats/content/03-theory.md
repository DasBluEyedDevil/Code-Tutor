---
type: "THEORY"
title: "Syntax Breakdown"
---

Let's break down each data type and understand the rules:

### 1. Strings (str) - The Text Type

<li>**What they are:** Any text wrapped in quotes (single `'` or double `"`)

```
name = "Alice"     # Double quotes
greeting = 'Hi!'   # Single quotes (works the same)
message = "123"    # Even numbers in quotes are strings!
```
</li><li>**Key rule:** MUST have matching quotes at start and end

```
"Hello"  ✅ Correct
'Hello'  ✅ Also correct
"Hello'  ❌ Error - quotes don't match!
```
</li><li>**The `type()` function:** Tells you what type a value is

<pre>`print(type("Alice"))  # Shows: <class 'str'>`</pre>Think of `type()` as asking "What kind of container is this?"

</li>
### 2. Integers (int) - The Whole Number Type

<li>**What they are:** Whole numbers with NO decimal point and NO quotes

```
age = 25        # Positive integer
count = 0       # Zero is an integer
debt = -500     # Negative integer
```
</li><li>**Key rule:** NO quotes, NO decimal point

```
age = 25       ✅ Integer
age = "25"     ❌ String (has quotes)
age = 25.0     ❌ Float (has decimal point)
```
</li><li>**You can do math:** +, -, *, /, //, %, **

```
students = 20 + 5   # Addition: 25
groups = 20 // 5    # Division (whole number): 4
squared = 5 ** 2    # Exponent: 25
```
</li>
### 3. Floats (float) - The Decimal Number Type

<li>**What they are:** Numbers with a decimal point

```
price = 19.99
pi = 3.14159
temperature = -2.5
exact_value = 5.0  # Even whole numbers with .0 are floats!
```
</li><li>**Key rule:** MUST have a decimal point (even if it's .0)

```
price = 19.99  ✅ Float
price = 20     ❌ Integer (no decimal point)
```
</li><li>**Mixing int and float:** Result is always a float

```
result = 5 + 2.5   # 5 (int) + 2.5 (float) = 7.5 (float)
print(type(result))  # <class 'float'>
```
</li><li>**IEEE 754 Standard:** Python uses 64-bit precision for floats (about 15-17 decimal digits of accuracy)

</li>
### 4. Booleans (bool) - The True/False Type

<li>**What they are:** Only TWO possible values: `True` or `False`

```
is_raining = True
has_license = False
```
</li><li>**Key rules:**

- Must be capitalized: `True` and `False` (not `true` or `false`)
- NO quotes (with quotes they become strings)

```
is_student = True    ✅ Boolean
is_student = true    ❌ Error (not capitalized)
is_student = "True"  ❌ String (has quotes)
```
</li>
### 5. Mixing Types - What Works and What Doesn't
<table><thead><tr><th>Operation</th><th>Example</th><th>Result</th></tr></thead><tbody><tr><td>String + String</td><td>`"Hello" + " World"`</td><td>✅ `"Hello World"`</td></tr><tr><td>Integer + Integer</td><td>`5 + 3`</td><td>✅ `8`</td></tr><tr><td>Float + Integer</td><td>`5.5 + 2`</td><td>✅ `7.5`</td></tr><tr><td>String * Integer</td><td>`"Ha" * 3`</td><td>✅ `"HaHaHa"`</td></tr><tr><td>String + Integer</td><td>`"Age: " + 25`</td><td>❌ TypeError!</td></tr></tbody></table>**The golden rule:** You can't directly combine strings and numbers with `+`. You need to convert them first (we'll learn this next lesson!).