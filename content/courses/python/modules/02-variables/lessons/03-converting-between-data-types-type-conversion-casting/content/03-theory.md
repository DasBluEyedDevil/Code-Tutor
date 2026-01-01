---
type: "THEORY"
title: "Syntax Breakdown"
---

Let's understand the three main conversion functions:

### 1. int() - Convert to Integer

<li>**What it does:** Converts text or float to a whole number

```
int("42")    # "42" (string) → 42 (int)
int(3.9)     # 3.9 (float) → 3 (int)  ⚠️ Loses .9!
int(3.1)     # 3.1 (float) → 3 (int)  ⚠️ Loses .1!
```
</li><li>**Important:** int() doesn't round—it **truncates** (cuts off) decimals

```
int(9.9)  # Result: 9 (not 10!)
int(9.1)  # Result: 9
```
Think of it like cutting pizza: int(3.9) gives you 3 <em>whole</em> slices. The 0.9 piece is thrown away!

</li><li>**Common error:** Converting invalid strings

```
int("hello")    # ❌ ValueError: invalid literal
int("12.5")     # ❌ ValueError: can't convert "12.5" directly
int(float("12.5"))  # ✅ Works! Convert to float first, then int
```
</li>
### 2. float() - Convert to Decimal Number

<li>**What it does:** Converts text or integer to a decimal number

```
float("3.14")    # "3.14" (string) → 3.14 (float)
float("10")      # "10" (string) → 10.0 (float)
float(5)         # 5 (int) → 5.0 (float)
```
</li><li>**Always adds a decimal point:**

<pre>`float(100)  # Result: 100.0 (not 100)`</pre></li><li>**Can handle string decimals:**

```
price = float("19.99")  # ✅ Works perfectly
temp = float("98.6")    # ✅ Works perfectly
```
</li>
### 3. str() - Convert to String (Text)

<li>**What it does:** Converts anything to text

```
str(42)      # 42 (int) → "42" (string)
str(3.14)    # 3.14 (float) → "3.14" (string)
str(True)    # True (bool) → "True" (string)
```
</li><li>**Why you need it:** To combine numbers with text

```
# ❌ Doesn't work:
print("Age: " + 25)  # TypeError

# ✅ Works:
print("Age: " + str(25))  # "Age: 25"

# ✅ Better way (f-string):
print(f"Age: {25}")  # "Age: 25"
```
</li><li>**str() works on everything:**

```
str(100)      # "100"
str(3.14)     # "3.14"
str(True)     # "True"
str([1,2,3])  # "[1, 2, 3]"
```
</li>
### 4. Implicit Conversion (Python's Autopilot)

<li>**When Python auto-converts:**

```
result = 5 + 2.5  # Python converts 5 → 5.0, then adds
# 5.0 + 2.5 = 7.5
```
Python always promotes to the "higher" type (float is higher than int)

</li><li>**Rule:** int + float = float

```
10 + 5.0   # 15.0 (float)
3 * 2.5    # 7.5 (float)
100 / 2    # 50.0 (float) ← Division ALWAYS gives float!
```
</li>
### 5. Common Conversion Patterns
<table><thead><tr><th>From</th><th>To</th><th>Function</th><th>Example</th></tr></thead><tbody><tr><td>String</td><td>Integer</td><td>`int()`</td><td>`int("42")` → `42`</td></tr><tr><td>String</td><td>Float</td><td>`float()`</td><td>`float("3.14")` → `3.14`</td></tr><tr><td>Integer</td><td>String</td><td>`str()`</td><td>`str(42)` → `"42"`</td></tr><tr><td>Float</td><td>String</td><td>`str()`</td><td>`str(3.14)` → `"3.14"`</td></tr><tr><td>Float</td><td>Integer</td><td>`int()`</td><td>`int(3.9)` → `3` ⚠️</td></tr><tr><td>Integer</td><td>Float</td><td>`float()`</td><td>`float(5)` → `5.0`</td></tr></tbody></table>### 6. The Golden Rule
**If you're not sure what type something is, check it:**

```
mystery_value = "100"
print(type(mystery_value))  # <class 'str'>

converted = int(mystery_value)
print(type(converted))      # <class 'int'>
```
Always use `type()` when debugging conversion issues!