import asyncio
import aiofiles
import json
from pathlib import Path
from datetime import datetime

async def read_transactions_async(file_path: str) -> dict:
    """Read a transaction file asynchronously."""
    # TODO: Use aiofiles to read and parse JSON
    async with aiofiles.open(file_path, 'r') as f:
        content = await f.read()
        return json.loads(content)

async def process_all_months(directory: str) -> dict:
    """Process all monthly transaction files concurrently."""
    path = Path(directory)
    files = list(path.glob('*_transactions.json'))
    
    # TODO: Read all files concurrently using asyncio.gather
    tasks = [read_transactions_async(str(f)) for f in files]
    all_data = await asyncio.gather(*tasks)
    
    # Aggregate by category
    totals = {}
    for month_data in all_data:
        for tx in month_data.get('transactions', []):
            category = tx.get('category', 'other')
            amount = tx.get('amount', 0)
            totals[category] = totals.get(category, 0) + amount
    
    return totals

async def write_report_async(path: str, report: dict) -> None:
    """Write summary report asynchronously."""
    # TODO: Use aiofiles to write the report
    pass

async def main():
    # Setup test data
    base = Path('finance_async_demo')
    base.mkdir(exist_ok=True)
    
    # Create sample files (using sync for setup)
    for month in ['jan', 'feb', 'mar']:
        data = {
            'month': month,
            'transactions': [
                {'amount': 100, 'category': 'food'},
                {'amount': 50, 'category': 'transport'}
            ]
        }
        (base / f'{month}_transactions.json').write_text(
            json.dumps(data, indent=2)
        )
    
    print("=== Async Transaction Processor ===")
    
    # Process all files concurrently
    start = datetime.now()
    totals = await process_all_months(str(base))
    elapsed = (datetime.now() - start).total_seconds()
    
    print(f"\nCategory Totals:")
    for category, total in totals.items():
        print(f"  {category}: ${total:.2f}")
    
    print(f"\nProcessed in {elapsed:.3f}s")

# Run the demo
asyncio.run(main())