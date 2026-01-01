---
type: "THEORY"
title: "Syntax Breakdown"
---

**Console - the central Rich object:**
```python
from rich.console import Console
console = Console()

console.print("[green]Success![/green]")  # Colored output
console.print("[bold red]Error![/bold red]")  # Bold + color
```

**Tables - display structured data:**
```python
from rich.table import Table

table = Table(title="Expenses")
table.add_column("Category", style="cyan")
table.add_column("Amount", style="green")
table.add_row("Food", "$150.00")
table.add_row("Transport", "$75.00")
console.print(table)
```

**Progress bar - show long operations:**
```python
from rich.progress import track

for item in track(transactions, description="Processing..."):
    process(item)  # Automatically shows progress
```

**Common colors:** green, red, yellow, blue, cyan, magenta, white