---
type: "KEY_POINT"
title: "Using pathlib Throughout"
---

**Pathlib is used everywhere in the Finance Tracker:**

```python
# Config - data directory
@property
def data_dir(self) -> Path:
    path = self.base_dir / "data"
    path.mkdir(exist_ok=True)
    return path

# Export - ensure directory exists
output.parent.mkdir(parents=True, exist_ok=True)

# Read file contents
schema_file = Path("migrations/001_initial.sql")
schema = schema_file.read_text()

# Check if config exists
env_file = Path(".env")
if not env_file.exists():
    console.print("[red]Missing .env file![/red]")
    raise typer.Exit(1)

# List all migration files
migrations_dir = Path("migrations")
for migration in sorted(migrations_dir.glob("*.sql")):
    console.print(f"Found: {migration.name}")
```

**Key pathlib methods used:**
- `Path.mkdir(parents=True, exist_ok=True)` - Create directories
- `Path.open()` - Open files (context manager)
- `Path.read_text()` / `Path.write_text()` - Simple file I/O
- `Path.exists()` - Check existence
- `Path.glob()` - Find files by pattern
- `Path.parent` - Get parent directory
- `/` operator - Join paths