---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Using print() Instead of console.print() for Rich Markup**
```python
# WRONG - Regular print doesn't interpret Rich markup
print("[green]Success[/green]")  # Outputs literal text!

# CORRECT - Use Console for Rich output
from rich.console import Console
console = Console()
console.print("[green]Success[/green]")  # Shows green text
```

**2. Forgetting to Close Rich Markup Tags**
```python
# WRONG - Unclosed tag causes formatting issues
console.print("[bold]Important message")  # Missing [/bold]!

# CORRECT - Always close your tags
console.print("[bold]Important message[/bold]")
```

**3. Forgetting to Print the Table**
```python
# WRONG - Creating table but never displaying it
table = Table(title="Expenses")
table.add_column("Amount")
table.add_row("$50")  # Table exists but not shown!

# CORRECT - Use console.print() to display
console.print(table)
```

**4. Using track() with Empty Collections**
```python
# WRONG - track() on None causes error
items = None
for item in track(items):  # TypeError!
    pass

# CORRECT - Ensure iterable exists
items = get_items() or []
for item in track(items, description="Processing"):
    pass
```

**5. Mixing Console Instances Incorrectly**
```python
# WRONG - Multiple consoles can conflict
console1 = Console()
console2 = Console()
console1.print(table)  # Might have display issues

# CORRECT - Use single console instance
console = Console()  # Create once, use everywhere
console.print(table)
```