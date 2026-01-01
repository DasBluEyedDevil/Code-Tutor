---
type: "THEORY"
title: "Essential Dictionary Methods"
---

**Getting views of dictionary contents:**

```python
data = {"a": 1, "b": 2, "c": 3}

data.keys()    # dict_keys(['a', 'b', 'c'])
data.values()  # dict_values([1, 2, 3])
data.items()   # dict_items([('a', 1), ('b', 2), ('c', 3)])
```

**Safe access with `.get()` and `.setdefault()`:**

```python
user = {"name": "Alice"}

# get() returns None if key missing (no error)
user.get("age")           # None
user.get("age", 0)        # 0 (default value)

# setdefault() gets value OR sets it if missing
user.setdefault("role", "member")  # Adds "role": "member"
```

**Modifying dictionaries:**

```python
base = {"a": 1, "b": 2}
more = {"c": 3, "d": 4}

# update() merges dictionaries
base.update(more)         # base is now {'a': 1, 'b': 2, 'c': 3, 'd': 4}

# pop() removes and returns value
value = base.pop("a")     # value = 1, base no longer has 'a'
value = base.pop("x", 0)  # value = 0 (default), no error

# clear() removes everything
base.clear()              # base is now {}
```

**Python 3.9+ merge operators:**

```python
dict1 = {"a": 1}
dict2 = {"b": 2}

combined = dict1 | dict2   # {'a': 1, 'b': 2} (new dict)
dict1 |= dict2             # dict1 is now {'a': 1, 'b': 2}
```