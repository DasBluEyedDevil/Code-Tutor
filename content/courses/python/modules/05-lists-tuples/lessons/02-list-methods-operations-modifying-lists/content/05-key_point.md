---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Lists are mutable** - methods change the list in place
- **append(item)**: Add to end (most common)
- **insert(index, item)**: Add at specific position
- **extend(list)**: Add multiple items
- **remove(item)**: Remove first occurrence by value
- **pop()**: Remove and return last item
- **pop(index)**: Remove and return item at index
- **sort()**: Sort in place (ascending)
- **reverse()**: Reverse in place
- **index(item)**: Find position (raises error if not found)
- **count(item)**: Count occurrences (returns 0 if not found)
- **+ operator**: Concatenate lists
- *** operator**: Repeat lists
- **in operator**: Check membership

### Important Distinctions:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Modify List (return None)</th><th>Return Information</th></tr><tr><td>append(), insert(), extend()</td><td>index() returns position</td></tr><tr><td>remove(), clear()</td><td>count() returns count</td></tr><tr><td>sort(), reverse()</td><td>pop() returns item</td></tr></table>### Safety Patterns:
```
# Before remove(), check membership:
if item in my_list:
    my_list.remove(item)

# Before index(), check existence:
if item in my_list:
    position = my_list.index(item)

# To keep original, use sorted():
original = [3, 1, 2]
new_list = sorted(original)  # original unchanged

```
### Common Mistakes:

<li>**Using return value of modifying methods**:```
# WRONG:
my_list = my_list.append(item)  # my_list becomes None!

# CORRECT:
my_list.append(item)  # my_list stays as list

```
</li><li>**Calling remove() on non-existent item**:```
# WRONG:
my_list.remove(item)  # Error if item not in list!

# CORRECT:
if item in my_list:
    my_list.remove(item)

```
</li><li>**append() vs extend() confusion**:```
my_list.append([1, 2])  # Adds ONE item (nested list)
my_list.extend([1, 2])  # Adds TWO items (1 and 2)

```
</li>
### Coming Up Next:
In **Lesson 3: List Slicing**, you'll learn how to:

- Extract portions of lists
- Use slice notation [start:stop:step]
- Copy lists properly
- Replace sections of lists
- Reverse lists with slicing

Slicing is one of Python's most powerful features!