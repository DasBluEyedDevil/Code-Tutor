---
type: "THEORY"
title: "Adding a CLI Interface"
---

In addition to the REST API, we'll add a command-line interface using Typer. This demonstrates:

- Multiple entry points to the same application
- Reusing service layer from CLI
- Pathlib for file operations
- CSV/JSON export functionality

**CLI Commands:**
```bash
# Transaction management
finance add expense 45.99 "Grocery shopping" --category Food
finance add income 3000 "Monthly salary" --category Salary
finance list --month 2025-01
finance summary --period monthly

# Data export
finance export csv transactions.csv --start 2025-01-01 --end 2025-01-31
finance export json backup.json

# Budget checking
finance budget status
finance budget set Food 500 --period monthly
```