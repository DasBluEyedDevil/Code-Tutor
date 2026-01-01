---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Swapping Dict with Duplicate Values Loses Data**
```python
# WRONG - If values aren't unique, you lose data!
grades = {"Alice": 90, "Bob": 90, "Charlie": 85}
swapped = {v: k for k, v in grades.items()}
print(swapped)  # {90: 'Bob', 85: 'Charlie'} - Alice is LOST!

# CORRECT - Group by value if duplicates exist
from collections import defaultdict
by_grade = defaultdict(list)
for name, grade in grades.items():
    by_grade[grade].append(name)
print(dict(by_grade))  # {90: ['Alice', 'Bob'], 85: ['Charlie']}
```

**2. Using Expensive Operations Inside Comprehensions**
```python
# WRONG - words.count() runs for EVERY word (O(n^2))!
text = "hello world hello python world python hello"
words = text.split()
counts = {word: words.count(word) for word in set(words)}  # Slow!

# CORRECT - Use Counter for counting (O(n))
from collections import Counter
counts = Counter(words)  # Much faster!
print(dict(counts))  # {'hello': 3, 'world': 2, 'python': 2}
```

**3. Confusing Dict and Set Comprehension Syntax**
```python
# WRONG - Missing colon creates SET, not dict!
result = {x for x in range(5)}  # This is a SET: {0, 1, 2, 3, 4}

# CORRECT - Use colon for key:value pairs
result = {x: x**2 for x in range(5)}  # This is a DICT
print(result)  # {0: 0, 1: 1, 2: 4, 3: 9, 4: 16}
```

**4. Nested Comprehensions That Are Hard to Read**
```python
# WRONG - This is too complex to understand!
result = {outer: {inner: outer*inner for inner in range(1,4)} 
          for outer in range(1,4) if outer != 2}

# CORRECT - Use regular loops for complex logic
result = {}
for outer in range(1, 4):
    if outer != 2:
        result[outer] = {}
        for inner in range(1, 4):
            result[outer][inner] = outer * inner
```

**5. Expecting Comprehension to Modify Original Dict**
```python
# WRONG - Comprehensions create NEW dicts, don't modify!
original = {"a": 1, "b": 2, "c": 3}
filtered = {k: v for k, v in original.items() if v > 1}
# original is unchanged! filtered is a new dict

# CORRECT - Assign back if you want to replace
original = {k: v for k, v in original.items() if v > 1}
# Now original only has items where value > 1
```