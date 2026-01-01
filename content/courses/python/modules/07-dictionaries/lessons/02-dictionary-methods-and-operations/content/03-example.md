---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Keys, Values, and Items ===
Keys: dict_keys(['Alice', 'Bob', 'Charlie'])
Values: dict_values([95, 87, 92])
Items: dict_items([('Alice', 95), ('Bob', 87), ('Charlie', 92)])

=== Looping Through Dictionaries ===

Looping through keys:
- Alice
- Bob
- Charlie

Looping through values:
- 95
- 87
- 92

Looping through items (key-value pairs):
- Alice scored 95
- Bob scored 87
- Charlie scored 92

=== get() and setdefault() ===
Age (missing): None
Age with default: 0
After setdefault: {'name': 'Alice', 'role': 'member'}

=== update() and pop() ===
Merged: {'a': 1, 'b': 2, 'c': 3, 'd': 4}
Popped value: 1
After pop: {'b': 2, 'c': 3, 'd': 4}

=== Practical: Word Counter ===
{'hello': 2, 'world': 1, 'python': 2, 'is': 1, 'great': 1}
```

```python
# Dictionary Methods and Operations

print("=== Keys, Values, and Items ===")

scores = {"Alice": 95, "Bob": 87, "Charlie": 92}

print(f"Keys: {scores.keys()}")
print(f"Values: {scores.values()}")
print(f"Items: {scores.items()}")

print("\n=== Looping Through Dictionaries ===")

print("\nLooping through keys:")
for name in scores:
    print(f"- {name}")

print("\nLooping through values:")
for score in scores.values():
    print(f"- {score}")

print("\nLooping through items (key-value pairs):")
for name, score in scores.items():
    print(f"- {name} scored {score}")

print("\n=== get() and setdefault() ===")

user = {"name": "Alice"}

# Safe access with get()
print(f"Age (missing): {user.get('age')}")
print(f"Age with default: {user.get('age', 0)}")

# setdefault adds key if missing
user.setdefault("role", "member")
print(f"After setdefault: {user}")

print("\n=== update() and pop() ===")

base = {"a": 1, "b": 2}
more = {"c": 3, "d": 4}

# Merge dictionaries
base.update(more)
print(f"Merged: {base}")

# Remove and return a value
value = base.pop("a")
print(f"Popped value: {value}")
print(f"After pop: {base}")

print("\n=== Practical: Word Counter ===")

words = ["hello", "world", "hello", "python", "is", "great", "python"]
word_counts = {}

for word in words:
    # If word exists, add 1; if not, start at 0 and add 1
    word_counts[word] = word_counts.get(word, 0) + 1

print(word_counts)
```
