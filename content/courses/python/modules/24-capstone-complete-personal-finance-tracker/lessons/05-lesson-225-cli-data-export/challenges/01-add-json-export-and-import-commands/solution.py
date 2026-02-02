import json as json_lib
from datetime import date

@export_app.command("json")
def export_json(
    output: Annotated[Path, typer.Argument(help="Output file path")],
):
    """Export all data to JSON backup."""
    
    async def _export():
        await Database.connect()
        try:
            tx_repo = TransactionRepository()
            cat_repo = CategoryRepository()
            
            transactions = await tx_repo.get_user_transactions(user_id=1, limit=100000)
            categories = await cat_repo.get_user_categories(user_id=1)
            
            backup = {
                "export_date": date.today().isoformat(),
                "version": "1.0",
                "categories": [
                    {
                        "id": c.id,
                        "name": c.name,
                        "type": c.type.value,
                        "icon": c.icon,
                    }
                    for c in categories
                ],
                "transactions": [
                    {
                        "id": t.id,
                        "amount": str(t.amount),
                        "description": t.description,
                        "category_id": t.category_id,
                        "transaction_date": t.transaction_date.isoformat(),
                    }
                    for t in transactions
                ],
            }
            
            output.parent.mkdir(parents=True, exist_ok=True)
            output.write_text(json_lib.dumps(backup, indent=2))
            
            console.print(f"✅ Exported {len(transactions)} transactions to {output}")
            
        finally:
            await Database.disconnect()
    
    run_async(_export())


@app.command(name="import")
def import_json(
    input_file: Annotated[Path, typer.Argument(help="JSON file to import")],
    merge: Annotated[bool, typer.Option("--merge")] = False,
):
    """Import data from JSON backup."""
    
    if not input_file.exists():
        console.print(f"[red]File not found: {input_file}[/red]")
        raise typer.Exit(1)
    
    async def _import():
        await Database.connect()
        try:
            data = json_lib.loads(input_file.read_text())
            
            if not merge:
                console.print("[yellow]Clearing existing data...[/yellow]")
                async with Database.connection() as conn:
                    await conn.execute("DELETE FROM transactions WHERE user_id = 1")
            
            tx_repo = TransactionRepository()
            imported = 0
            
            for tx_data in data["transactions"]:
                from .models.transaction import Transaction
                tx = Transaction.create(
                    amount=tx_data["amount"],
                    description=tx_data["description"],
                    category_id=tx_data["category_id"],
                    user_id=1,
                    transaction_date=date.fromisoformat(tx_data["transaction_date"]),
                )
                await tx_repo.create(tx)
                imported += 1
            
            console.print(f"✅ Imported {imported} transactions from {input_file}")
            
        finally:
            await Database.disconnect()
    
    run_async(_import())