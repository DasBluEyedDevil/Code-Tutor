---
type: "THEORY"
title: "Syntax Breakdown"
---

### Creating Tuples:
```
# With parentheses (recommended)
my_tuple = (1, 2, 3)

# Without parentheses (tuple packing)
my_tuple = 1, 2, 3  # Same result

# Single-item tuple (COMMA REQUIRED!)
single = (42,)   # Tuple
notuple = (42)   # Integer (parens ignored)

# Empty tuple
empty = ()

# Mixed types
mixed = ("Alice", 25, True, 3.14)

```
### Accessing Elements:
```
my_tuple = ('A', 'B', 'C', 'D', 'E')

# Positive indexing
first = my_tuple[0]   # 'A'
second = my_tuple[1]  # 'B'

# Negative indexing
last = my_tuple[-1]   # 'E'
second_last = my_tuple[-2]  # 'D'

# Slicing (works like lists)
slice1 = my_tuple[1:4]   # ('B', 'C', 'D')
slice2 = my_tuple[:3]    # ('A', 'B', 'C')
slice3 = my_tuple[::2]   # ('A', 'C', 'E')
reversed_tuple = my_tuple[::-1]  # ('E', 'D', 'C', 'B', 'A')

```
### Tuple Unpacking:
```
# Basic unpacking
point = (10, 20)
x, y = point  # x = 10, y = 20

# Multiple values
person = ("Alice", 25, "Engineer")
name, age, job = person

# Swap variables (Pythonic!)
a, b = b, a  # Swaps a and b

# Partial unpacking with * (Python 3+)
first, *middle, last = (1, 2, 3, 4, 5)
# first = 1, middle = [2, 3, 4], last = 5

# Ignore values with _
x, _, z = (10, 20, 30)  # x = 10, z = 30 (ignore middle)

```
### Tuple Methods (Only 2!):
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Method</th><th>Description</th><th>Example</th></tr><tr><td>count(item)</td><td>Count occurrences</td><td>t.count(5) → 3</td></tr><tr><td>index(item)</td><td>Find first position</td><td>t.index(5) → 2</td></tr></table>```
numbers = (1, 2, 3, 2, 4, 2, 5)

numbers.count(2)  # 3 (appears 3 times)
numbers.index(3)  # 2 (at index 2)

```
### Operations on Tuples:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Operation</th><th>Syntax</th><th>Example</th><th>Result</th></tr><tr><td>Concatenation</td><td>+</td><td>(1,2) + (3,4)</td><td>(1,2,3,4)</td></tr><tr><td>Repetition</td><td>*</td><td>(1,2) * 3</td><td>(1,2,1,2,1,2)</td></tr><tr><td>Membership</td><td>in</td><td>5 in (1,5,9)</td><td>True</td></tr><tr><td>Length</td><td>len()</td><td>len((1,2,3))</td><td>3</td></tr><tr><td>Min/Max</td><td>min(), max()</td><td>max((5,2,8))</td><td>8</td></tr><tr><td>Sum</td><td>sum()</td><td>sum((1,2,3))</td><td>6</td></tr></table>### Immutability Rules:
```
my_tuple = (1, 2, 3)

# ❌ CANNOT modify items
my_tuple[0] = 10  # TypeError!

# ❌ CANNOT append
my_tuple.append(4)  # AttributeError! (no append method)

# ❌ CANNOT remove
my_tuple.remove(2)  # AttributeError! (no remove method)

# ❌ CANNOT sort in place
my_tuple.sort()  # AttributeError! (no sort method)

# ✅ CAN create new tuple
my_tuple = my_tuple + (4, 5)  # Creates NEW tuple

# ✅ CAN use sorted() (returns list)
sorted_list = sorted(my_tuple)  # Returns sorted list
sorted_tuple = tuple(sorted(my_tuple))  # Convert back to tuple

```
### Converting Between Lists and Tuples:
```
# List → Tuple
my_list = [1, 2, 3, 4, 5]
my_tuple = tuple(my_list)  # (1, 2, 3, 4, 5)

# Tuple → List
my_tuple = (1, 2, 3, 4, 5)
my_list = list(my_tuple)  # [1, 2, 3, 4, 5]

# String → Tuple
letters = tuple("hello")  # ('h', 'e', 'l', 'l', 'o')

# Range → Tuple
numbers = tuple(range(5))  # (0, 1, 2, 3, 4)

```
### Lists vs Tuples - When to Use:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Scenario</th><th>Use</th><th>Reason</th></tr><tr><td>Shopping cart</td><td>List</td><td>Items added/removed</td></tr><tr><td>RGB color</td><td>Tuple</td><td>Fixed 3 values</td></tr><tr><td>Database record</td><td>Tuple</td><td>Fixed structure</td></tr><tr><td>Student grades</td><td>List</td><td>Grades added/changed</td></tr><tr><td>GPS coordinates</td><td>Tuple</td><td>Fixed lat/lon pair</td></tr><tr><td>Todo items</td><td>List</td><td>Items added/removed</td></tr><tr><td>Function return (multiple)</td><td>Tuple</td><td>Natural for returns</td></tr><tr><td>Dictionary key</td><td>Tuple</td><td>Must be immutable</td></tr></table>### Tuples as Dictionary Keys:
```
# ✅ Tuples work (immutable)
locations = {
    (40.7, -74.0): "New York",
    (34.0, -118.2): "Los Angeles"
}

print(locations[(40.7, -74.0)])  # "New York"

# ❌ Lists don't work (mutable)
try:
    bad_dict = {
        [40.7, -74.0]: "New York"  # TypeError!
    }
except TypeError as e:
    print(f"Error: {e}")

```
### Function Returns with Tuples:
```
# Single return value (normal)
def get_name():
    return "Alice"

# Multiple return values (tuple)
def get_user():
    return "Alice", 25, "alice@example.com"
    # Equivalent to: return ("Alice", 25, "alice@example.com")

# Unpack when calling
name, age, email = get_user()

# Or get as tuple
user_data = get_user()  # ('Alice', 25, 'alice@example.com')

```
### Common Patterns:
```
# Coordinate pair
point = (x, y)

# RGB color
color = (red, green, blue)

# Date
date = (year, month, day)

# Range (min, max)
range_tuple = (min_value, max_value)

# Database row
row = (id, name, email, age)

# Constants
WEEKDAYS = ("Mon", "Tue", "Wed", "Thu", "Fri")

# Configuration (immutable settings)
CONFIG = ("localhost", 8080, "admin", "password")

```