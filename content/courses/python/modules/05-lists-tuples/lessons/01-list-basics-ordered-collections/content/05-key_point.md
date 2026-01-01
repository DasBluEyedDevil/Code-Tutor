---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Lists** are ordered collections created with square brackets: [item1, item2, ...]
- **Zero-based indexing**: First item is index 0, not 1
- **len(list)** returns the number of items
- **Positive indices**: 0, 1, 2, ... (left to right)
- **Negative indices**: -1, -2, -3, ... (right to left, -1 is last)
- **Valid range**: 0 to len(list)-1 (or -len to -1)
- **IndexError** occurs when index is out of range
- **enumerate()** provides both index and value
- **List can contain mixed types**: [1, "hello", True, 3.14]
- **First item**: list[0] or list[-len(list)]
- **Last item**: list[-1] or list[len(list)-1]

### Essential Index Formulas:
```
# For a list with n items:
first_index = 0
last_index = len(list) - 1
middle_index = len(list) // 2

# Convert 1-based position to 0-based index:
index = position - 1

# Convert 0-based index to 1-based position:
position = index + 1

# Check if index is valid:
if 0 <= index < len(list):
    # Safe to access

```
### Iteration Patterns:
```
# Pattern 1: Just values
for item in my_list:
    print(item)

# Pattern 2: Just indices
for i in range(len(my_list)):
    print(i, my_list[i])

# Pattern 3: Both (recommended!)
for index, value in enumerate(my_list):
    print(index, value)

# Pattern 4: 1-based numbering
for position, value in enumerate(my_list, start=1):
    print(position, value)

```
### Before Moving On:
Make sure you can:

- Create lists with square brackets
- Access items using positive indices (0, 1, 2, ...)
- Access items using negative indices (-1, -2, -3, ...)
- Get list length with len()
- Iterate through lists with for loops
- Use enumerate() for index + value
- Validate indices before accessing
- Calculate first, last, and middle indices

### Coming Up Next:
In **Lesson 2: List Methods & Operations**, you'll learn how to:

- Modify lists: append(), insert(), remove()
- Sort and reverse lists
- Find items: index(), count()
- Combine lists with + and *
- Check membership with 'in'

Lists become much more powerful when you can change them!