---
type: "EXAMPLE"
title: "Typer CLI Implementation"
---

Complete CLI with Typer:

```python
# src/finance_tracker/cli.py
import asyncio
from datetime import date, datetime
from decimal import Decimal
from pathlib import Path
from typing import Annotated

import typer
from rich.console import Console
from rich.table import Table

from .database import Database
from .models.transaction import TransactionType
from .repositories.transaction import TransactionRepository
from .repositories.category import CategoryRepository


app = typer.Typer(help="Personal Finance Tracker CLI")
console = Console()


def run_async(coro):
    """Helper to run async code from sync CLI."""
    return asyncio.run(coro)


@app.command()
def add(
    transaction_type: Annotated[
        str, 
        typer.Argument(help="'income' or 'expense'")
    ],
    amount: Annotated[
        float,
        typer.Argument(help="Transaction amount")
    ],
    description: Annotated[
        str,
        typer.Argument(help="Transaction description")
    ],
    category: Annotated[
        str,
        typer.Option("--category", "-c", help="Category name")
    ] = "Other",
    transaction_date: Annotated[
        str | None,
        typer.Option("--date", "-d", help="Date (YYYY-MM-DD)")
    ] = None,
):
    """Add a new transaction."""
    
    async def _add():
        await Database.connect()
        try:
            # Get or create category
            cat_repo = CategoryRepository()
            cat = await cat_repo.get_by_name(category, user_id=1)  # TODO: proper user handling
            
            if not cat:
                console.print(f"[yellow]Category '{category}' not found. Creating...[/yellow]")
                cat = await cat_repo.create_category(
                    name=category,
                    type=TransactionType(transaction_type),
                    user_id=1,
                )
            
            # Create transaction
            tx_repo = TransactionRepository()
            from .models.transaction import Transaction
            
            tx = await tx_repo.create(Transaction.create(
                amount=Decimal(str(amount)),
                description=description,
                category_id=cat.id,
                user_id=1,
                transaction_date=(
                    date.fromisoformat(transaction_date) 
                    if transaction_date 
                    else date.today()
                ),
            ))
            
            emoji = "💰" if transaction_type == "income" else "💸"
            console.print(f"{emoji} Added: ${amount:.2f} - {description}")
            
        finally:
            await Database.disconnect()
    
    run_async(_add())


@app.command(name="list")
def list_transactions(
    month: Annotated[
        str | None,
        typer.Option("--month", "-m", help="Filter by month (YYYY-MM)")
    ] = None,
    limit: Annotated[
        int,
        typer.Option("--limit", "-n", help="Number of transactions")
    ] = 20,
):
    """List recent transactions."""
    
    async def _list():
        await Database.connect()
        try:
            repo = TransactionRepository()
            
            # Parse month filter
            start_date = None
            end_date = None
            if month:
                year, mon = map(int, month.split("-"))
                start_date = date(year, mon, 1)
                if mon == 12:
                    end_date = date(year + 1, 1, 1)
                else:
                    end_date = date(year, mon + 1, 1)
            
            transactions = await repo.get_user_transactions(
                user_id=1,
                start_date=start_date,
                end_date=end_date,
                limit=limit,
            )
            
            if not transactions:
                console.print("[dim]No transactions found.[/dim]")
                return
            
            # Build table
            table = Table(title="Transactions")
            table.add_column("Date", style="cyan")
            table.add_column("Description")
            table.add_column("Amount", justify="right")
            table.add_column("Category")
            
            for tx in transactions:
                # Get category name (simplified)
                cat_repo = CategoryRepository()
                cat = await cat_repo.get_by_id(tx.category_id, tx.user_id)
                cat_name = cat.name if cat else "Unknown"
                
                # Format amount with color
                amount_str = f"${tx.amount:.2f}"
                style = "green" if "income" in cat_name.lower() else "red"
                
                table.add_row(
                    str(tx.transaction_date),
                    tx.description[:40],
                    f"[{style}]{amount_str}[/{style}]",
                    cat_name,
                )
            
            console.print(table)
            
        finally:
            await Database.disconnect()
    
    run_async(_list())


@app.command()
def summary(
    period: Annotated[
        str,
        typer.Option("--period", "-p", help="'monthly' or 'yearly'")
    ] = "monthly",
):
    """Show spending summary."""
    
    async def _summary():
        await Database.connect()
        try:
            repo = TransactionRepository()
            
            today = date.today()
            if period == "monthly":
                start_date = today.replace(day=1)
                if today.month == 12:
                    end_date = date(today.year + 1, 1, 1)
                else:
                    end_date = today.replace(month=today.month + 1, day=1)
            else:  # yearly
                start_date = today.replace(month=1, day=1)
                end_date = date(today.year + 1, 1, 1)
            
            summary = await repo.get_summary(
                user_id=1,
                start_date=start_date,
                end_date=end_date,
            )
            
            console.print(f"\n📊 [bold]{period.title()} Summary[/bold]")
            console.print(f"   Period: {start_date} to {end_date}")
            console.print(f"   💰 Income:   [green]${summary.total_income:,.2f}[/green]")
            console.print(f"   💸 Expenses: [red]${summary.total_expenses:,.2f}[/red]")
            console.print(f"   📈 Net:      [{'green' if summary.net_balance >= 0 else 'red'}]${summary.net_balance:,.2f}[/]")
            console.print(f"   🎯 Savings:  {summary.savings_rate:.1f}%")
            console.print(f"   📝 Transactions: {summary.transaction_count}")
            
        finally:
            await Database.disconnect()
    
    run_async(_summary())


# Export subcommand group
export_app = typer.Typer(help="Export data to files")
app.add_typer(export_app, name="export")


@export_app.command("csv")
def export_csv(
    output: Annotated[
        Path,
        typer.Argument(help="Output file path")
    ],
    start: Annotated[
        str | None,
        typer.Option("--start", help="Start date (YYYY-MM-DD)")
    ] = None,
    end: Annotated[
        str | None,
        typer.Option("--end", help="End date (YYYY-MM-DD)")
    ] = None,
):
    """Export transactions to CSV."""
    import csv
    
    async def _export():
        await Database.connect()
        try:
            repo = TransactionRepository()
            
            transactions = await repo.get_user_transactions(
                user_id=1,
                start_date=date.fromisoformat(start) if start else None,
                end_date=date.fromisoformat(end) if end else None,
                limit=10000,
            )
            
            # Ensure parent directory exists (pathlib!)
            output.parent.mkdir(parents=True, exist_ok=True)
            
            with output.open("w", newline="", encoding="utf-8") as f:
                writer = csv.writer(f)
                writer.writerow(["date", "description", "amount", "category_id"])
                
                for tx in transactions:
                    writer.writerow([
                        tx.transaction_date.isoformat(),
                        tx.description,
                        str(tx.amount),
                        tx.category_id,
                    ])
            
            console.print(f"✅ Exported {len(transactions)} transactions to {output}")
            
        finally:
            await Database.disconnect()
    
    run_async(_export())


if __name__ == "__main__":
    app()
```
