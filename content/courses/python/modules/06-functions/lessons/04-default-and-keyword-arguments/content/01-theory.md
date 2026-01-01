---
type: "THEORY"
title: "Understanding the Concept"
---

Think about ordering coffee. At most cafes, if you just say "coffee," you'll get a medium, regular roast, hot coffee. Those are the DEFAULTS. But you CAN customize:

- "Large coffee" (changing size)
- "Iced coffee" (changing temperature)
- "Decaf, large, with oat milk" (changing multiple things)

**Default arguments** work the same way in Python. You set sensible defaults, and callers only specify what they want to change.

```python
def order_coffee(size="medium", roast="regular", temperature="hot"):
    print(f"One {size} {roast} {temperature} coffee!")

order_coffee()                          # medium regular hot coffee
order_coffee("large")                   # large regular hot coffee
order_coffee("small", "dark", "iced")  # small dark iced coffee
```

**Keyword arguments** let you specify parameters by NAME instead of position. This makes calls clearer and lets you skip parameters:

```python
order_coffee(temperature="iced")        # medium regular iced coffee
order_coffee(size="large", roast="decaf")  # large decaf hot coffee
```

This makes your functions much more flexible and easier to use!