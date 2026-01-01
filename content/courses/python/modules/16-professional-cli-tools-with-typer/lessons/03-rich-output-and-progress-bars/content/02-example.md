---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
$ python finance.py transactions
                    Transactions
+----+------------+----------+---------+-------------+
| ID | Date       | Category | Amount  | Note        |
+----+------------+----------+---------+-------------+
| 1  | 2024-01-15 | food     | -$45.00 | Groceries   |
| 2  | 2024-01-15 | salary   | +$2000  | January pay |
| 3  | 2024-01-16 | transport| -$25.00 | Uber        |
+----+------------+----------+---------+-------------+

$ python finance.py summary
Income:   $2000.00
Expenses: $70.00
Balance:  $1930.00
```

```python
import typer
from rich.console import Console
from rich.table import Table
from rich.progress import track
import time

app = typer.Typer(rich_markup_mode="rich")
console = Console()

# Sample transaction data
transactions = [
    {"id": 1, "date": "2024-01-15", "category": "food", "amount": -45.00, "note": "Groceries"},
    {"id": 2, "date": "2024-01-15", "category": "salary", "amount": 2000.00, "note": "January pay"},
    {"id": 3, "date": "2024-01-16", "category": "transport", "amount": -25.00, "note": "Uber"},
]

@app.command(name="list")
def list_transactions():
    """Show all transactions in a [bold green]beautiful table[/bold green]."""
    table = Table(title="Transactions")
    table.add_column("ID", style="cyan")
    table.add_column("Date")
    table.add_column("Category", style="magenta")
    table.add_column("Amount", style="green")
    table.add_column("Note")
    
    for t in transactions:
        amount_str = f"+${t['amount']:.2f}" if t['amount'] > 0 else f"-${abs(t['amount']):.2f}"
        table.add_row(
            str(t["id"]),
            t["date"],
            t["category"],
            amount_str,
            t["note"]
        )
    console.print(table)

@app.command()
def summary():
    """Show financial [bold]summary[/bold]."""
    income = sum(t["amount"] for t in transactions if t["amount"] > 0)
    expenses = abs(sum(t["amount"] for t in transactions if t["amount"] < 0))
    balance = income - expenses
    
    console.print(f"[green]Income:[/green]   ${income:.2f}")
    console.print(f"[red]Expenses:[/red] ${expenses:.2f}")
    console.print(f"[bold]Balance:[/bold]  ${balance:.2f}")

if __name__ == "__main__":
    app()

```
