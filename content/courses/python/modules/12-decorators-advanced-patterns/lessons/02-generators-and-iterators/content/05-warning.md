---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Trying to Reuse an Exhausted Generator**
```python
# WRONG - Generator is exhausted after first use
def numbers():
    yield 1
    yield 2
    yield 3

gen = numbers()
print(list(gen))  # [1, 2, 3]
print(list(gen))  # [] - Empty! Generator exhausted

# CORRECT - Create a new generator each time
print(list(numbers()))  # [1, 2, 3]
print(list(numbers()))  # [1, 2, 3]
```

**2. Using return Instead of yield**
```python
# WRONG - Returns only once, not a generator
def get_numbers():
    for i in range(5):
        return i  # Stops immediately!

result = get_numbers()
print(result)  # Just 0, not a generator

# CORRECT - Use yield to create generator
def get_numbers():
    for i in range(5):
        yield i  # Pauses and resumes

for num in get_numbers():
    print(num)  # 0, 1, 2, 3, 4
```

**3. Calling len() on a Generator**
```python
# WRONG - Generators don't support len()
gen = (x for x in range(100))
print(len(gen))  # TypeError: object has no len()

# CORRECT - Convert to list first (uses memory)
gen = (x for x in range(100))
print(len(list(gen)))  # 100

# BETTER - Use a separate counter if you need length
count = sum(1 for _ in gen)
```

**4. Storing Generator Expression Without Calling**
```python
# WRONG - Generator created but source changes
numbers = [1, 2, 3]
gen = (x * 2 for x in numbers)
numbers.append(4)  # Modifying source!
print(list(gen))  # [2, 4, 6, 8] - Includes 4!

# CORRECT - Use list() to capture values immediately
numbers = [1, 2, 3]
result = list(x * 2 for x in numbers)  # Captured now
numbers.append(4)
print(result)  # [2, 4, 6] - Original values
```

**5. Using next() Without StopIteration Handling**
```python
# WRONG - StopIteration crashes program
def limited():
    yield 1
    yield 2

gen = limited()
print(next(gen))  # 1
print(next(gen))  # 2
print(next(gen))  # StopIteration error!

# CORRECT - Provide default value
gen = limited()
print(next(gen, 'done'))  # 1
print(next(gen, 'done'))  # 2
print(next(gen, 'done'))  # 'done' - No error
```