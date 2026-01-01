@app.command()
def categories():
    """Show spending by [bold]category[/bold]."""
    transactions = load_data()
    
    # Group by category
    by_category: dict[str, float] = {}
    for t in transactions:
        cat = t["category"]
        by_category[cat] = by_category.get(cat, 0) + t["amount"]
    
    table = Table(title="Spending by Category")
    table.add_column("Category", style="magenta")
    table.add_column("Total")
    
    for cat, total in sorted(by_category.items()):
        if total == 0:
            continue
        color = "green" if total > 0 else "red"
        sign = "+" if total > 0 else ""
        table.add_row(cat, f"[{____}]{sign}${abs(total):.2f}[/{____}]")
    
    console.____(table)
