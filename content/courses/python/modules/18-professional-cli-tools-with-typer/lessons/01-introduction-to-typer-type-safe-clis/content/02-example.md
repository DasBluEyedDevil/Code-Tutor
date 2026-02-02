---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
$ python expense.py --help
Usage: expense.py [OPTIONS] AMOUNT CATEGORY

  Log an expense to your finance tracker.

Arguments:
  AMOUNT    The expense amount  [required]
  CATEGORY  Category (food, transport, etc.)  [required]

Options:
  --note TEXT  Optional note for this expense
  --help       Show this message and exit.

$ python expense.py 12.50 food
Logged: $12.50 in food

$ python expense.py 45.00 transport --note "Uber to airport"
Logged: $45.00 in transport (Uber to airport)
```

```python
import typer

def main(amount: float, category: str, note: str = ""):
    """Log an expense to your finance tracker."""
    msg = f"Logged: ${amount:.2f} in {category}"
    if note:
        msg += f" ({note})"
    print(msg)

if __name__ == "__main__":
    typer.run(main)

```
