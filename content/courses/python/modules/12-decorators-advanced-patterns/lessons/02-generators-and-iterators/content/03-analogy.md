---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Generator function:**
```python
def my_generator():
    yield 1
    yield 2
    yield 3

gen = my_generator()  # Creates generator object
print(next(gen))      # 1
print(next(gen))      # 2
```

**Generator expression:**
```python
# List comprehension (all at once)
squares_list = [x**2 for x in range(10)]

# Generator expression (lazy)
squares_gen = (x**2 for x in range(10))
```

**Iterator protocol:**
```python
class MyIterator:
    def __iter__(self):
        return self
    
    def __next__(self):
        # Return next value or raise StopIteration
        pass
```

**Using generators:**
```python
# In for loop
for item in my_generator():
    print(item)

# With next()
gen = my_generator()
value = next(gen)

# Convert to list (caution: loads all into memory)
all_values = list(my_generator())
```