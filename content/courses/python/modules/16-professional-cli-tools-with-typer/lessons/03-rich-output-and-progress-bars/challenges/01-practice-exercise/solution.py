import typer
from rich.console import Console
from rich.table import Table

app = typer.Typer(rich_markup_mode="rich")
console = Console()

categories = [
    {"name": "Food", "spent": 450, "budget": 500},
    {"name": "Transport", "spent": 200, "budget": 150},
    {"name": "Entertainment", "spent": 80, "budget": 100},
]

@app.command()
def report():
    """Show category spending report."""
    table = Table(title="Budget Report")
    table.add_column("Category", style="cyan")
    table.add_column("Spent")
    table.add_column("Budget")
    table.add_column("Status")
    
    for cat in categories:
        color = "green" if cat["spent"] <= cat["budget"] else "red"
        status = "OK" if cat["spent"] <= cat["budget"] else "OVER"
        table.add_row(
            cat["name"],
            f"${cat['spent']}",
            f"${cat['budget']}",
            f"[{color}]{status}[/{color}]"
        )
    console.print(table)

if __name__ == "__main__":
    app()
