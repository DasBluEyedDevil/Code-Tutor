---
type: "THEORY"
title: "Syntax Breakdown"
---

### Adding Methods:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Method</th><th>Syntax</th><th>Effect</th><th>Returns</th></tr><tr><td>append</td><td>list.append(item)</td><td>Adds item to end</td><td>None</td></tr><tr><td>insert</td><td>list.insert(index, item)</td><td>Adds item at index</td><td>None</td></tr><tr><td>extend</td><td>list.extend([items])</td><td>Adds all items to end</td><td>None</td></tr></table>#### Examples:
```
# append() - add to end
fruits = ["Apple", "Banana"]
fruits.append("Cherry")  # ["Apple", "Banana", "Cherry"]

# insert() - add at position
fruits.insert(0, "Avocado")  # ["Avocado", "Apple", "Banana", "Cherry"]
fruits.insert(2, "Blueberry")  # ["Avocado", "Apple", "Blueberry", "Banana", "Cherry"]

# extend() - add multiple items
fruits.extend(["Date", "Fig"])  # Adds both to end

# Common mistake:
fruits.append(["X", "Y"])  # Adds entire list as ONE item!
# Result: [..., ["X", "Y"]]  (nested list!)

```
### Removing Methods:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Method</th><th>Syntax</th><th>Effect</th><th>Returns</th></tr><tr><td>remove</td><td>list.remove(item)</td><td>Removes first occurrence</td><td>None</td></tr><tr><td>pop</td><td>list.pop()</td><td>Removes last item</td><td>Removed item</td></tr><tr><td>pop</td><td>list.pop(index)</td><td>Removes item at index</td><td>Removed item</td></tr><tr><td>clear</td><td>list.clear()</td><td>Removes all items</td><td>None</td></tr></table>#### Examples:
```
numbers = [1, 2, 3, 2, 4]

# remove() - removes first match
numbers.remove(2)  # [1, 3, 2, 4] (first 2 removed)

# pop() - removes and returns last
last = numbers.pop()  # last = 4, numbers = [1, 3, 2]

# pop(index) - removes and returns at index
second = numbers.pop(1)  # second = 3, numbers = [1, 2]

# clear() - empty the list
numbers.clear()  # []

```
#### remove() vs pop():
```
# remove(item) - find by VALUE
fruits = ["Apple", "Banana", "Cherry"]
fruits.remove("Banana")  # Finds and removes "Banana"

# pop(index) - remove by POSITION
fruits = ["Apple", "Banana", "Cherry"]
fruits.pop(1)  # Removes item at index 1 ("Banana")

# pop() - remove last item
fruits = ["Apple", "Banana", "Cherry"]
last = fruits.pop()  # Removes and returns "Cherry"

```
### Organizing Methods:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Method</th><th>Syntax</th><th>Effect</th><th>Returns</th></tr><tr><td>sort</td><td>list.sort()</td><td>Sorts ascending (A-Z, 0-9)</td><td>None</td></tr><tr><td>sort</td><td>list.sort(reverse=True)</td><td>Sorts descending (Z-A, 9-0)</td><td>None</td></tr><tr><td>reverse</td><td>list.reverse()</td><td>Reverses current order</td><td>None</td></tr></table>#### Examples:
```
numbers = [5, 2, 8, 1, 9]

# sort() - ascending (low to high)
numbers.sort()  # [1, 2, 5, 8, 9]

# sort(reverse=True) - descending (high to low)
numbers.sort(reverse=True)  # [9, 8, 5, 2, 1]

# reverse() - flip order
numbers = [1, 2, 3, 4, 5]
numbers.reverse()  # [5, 4, 3, 2, 1]

```
#### sort() vs sorted():
```
# list.sort() - modifies list, returns None
original = [3, 1, 2]
original.sort()  # original is now [1, 2, 3]

# sorted(list) - returns new list, keeps original
original = [3, 1, 2]
new_list = sorted(original)  # new_list = [1, 2, 3]
                              # original = [3, 1, 2] (unchanged)

```
### Finding & Counting Methods:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Method</th><th>Syntax</th><th>Returns</th><th>Error if not found?</th></tr><tr><td>index</td><td>list.index(item)</td><td>Index of first occurrence</td><td>Yes (ValueError)</td></tr><tr><td>count</td><td>list.count(item)</td><td>Number of occurrences</td><td>No (returns 0)</td></tr></table>#### Examples:
```
scores = [85, 92, 78, 92, 88, 92]

# index() - find position
position = scores.index(92)  # 1 (first occurrence)

# count() - count occurrences
count = scores.count(92)  # 3 (appears 3 times)
count_100 = scores.count(100)  # 0 (not in list)

# index() with non-existent item
try:
    position = scores.index(100)
except ValueError:
    print("Not found!")

# Safe approach with index()
if 100 in scores:
    position = scores.index(100)
else:
    position = -1  # Convention: -1 means "not found"

```
### List Operators:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Operator</th><th>Operation</th><th>Example</th><th>Result</th></tr><tr><td>+</td><td>Concatenate</td><td>[1,2] + [3,4]</td><td>[1,2,3,4]</td></tr><tr><td>*</td><td>Repeat</td><td>[1,2] * 3</td><td>[1,2,1,2,1,2]</td></tr><tr><td>in</td><td>Membership</td><td>5 in [1,5,9]</td><td>True</td></tr><tr><td>not in</td><td>Non-membership</td><td>7 not in [1,5,9]</td><td>True</td></tr></table>#### Examples:
```
# Concatenation (+)
list1 = [1, 2, 3]
list2 = [4, 5, 6]
combined = list1 + list2  # [1, 2, 3, 4, 5, 6]

# Repetition (*)
pattern = ["A", "B"]
repeated = pattern * 4  # ["A", "B", "A", "B", "A", "B", "A", "B"]

# Membership (in)
fruits = ["Apple", "Banana", "Cherry"]

if "Banana" in fruits:
    print("We have bananas!")

if "Grape" not in fruits:
    print("No grapes available")

```
### Method Return Values:
```
# These MODIFY the list and return None:
result = my_list.append(item)     # result = None
result = my_list.sort()           # result = None
result = my_list.reverse()        # result = None
result = my_list.remove(item)     # result = None

# These RETURN information:
item = my_list.pop()              # Returns removed item
index = my_list.index(item)       # Returns position
count = my_list.count(item)       # Returns count

# Common mistake:
fruits = ["Apple", "Banana"]
fruits = fruits.append("Cherry")  # WRONG! fruits is now None!

# Correct:
fruits.append("Cherry")  # fruits is still the list

```
### Safe Removal Patterns:
```
# Pattern 1: Check before remove()
if item in my_list:
    my_list.remove(item)
else:
    print(f"{item} not in list")

# Pattern 2: Try/except
try:
    my_list.remove(item)
except ValueError:
    print(f"{item} not in list")

# Pattern 3: Use count() to check
if my_list.count(item) > 0:
    my_list.remove(item)

```