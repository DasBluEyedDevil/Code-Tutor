import typer

app = typer.Typer(help="Budget Tracker CLI", rich_markup_mode="rich")
budget: dict = {"limit": 0.0, "spent": 0.0}

@app.command()
def set(amount: float):
    """Set the monthly [bold]budget[/bold]."""
    budget["limit"] = amount
    budget["spent"] = 0.0
    print(f"Budget set to ${amount:.2f}")

@app.command()
def spend(amount: float, category: str):
    """Record an [bold red]expense[/bold red]."""
    budget["spent"] += amount
    print(f"Spent ${amount:.2f} on {category}")

@app.command()
def remaining():
    """Show budget remaining."""
    left = budget["limit"] - budget["spent"]
    print(f"Remaining: ${left:.2f}")

if __name__ == "__main__":
    app()
