import typer

def main(bill: float, percent: int = 15, split: int = 1):
    """Calculate tip and split the bill."""
    tip = bill * (percent / 100)
    total_per_person = (bill + tip) / split
    print(f"Tip: ${tip:.2f} | Total per person: ${total_per_person:.2f}")

if __name__ == "__main__":
    typer.run(main)
