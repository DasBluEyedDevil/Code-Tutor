---
type: "THEORY"
title: "Syntax Breakdown"
---

Let's examine the magic:

- **`amount: float`** - A required positional argument. Typer knows it's required because there's no default value.
- **`category: str`** - Another required argument (position matters!).
- **`note: str = ""`** - An optional argument with default. Becomes `--note` flag.
- **`typer.run(main)`** - Converts your function into a CLI application.
- **The docstring** - Becomes the command description in `--help`.

**Type hint to CLI mapping:**
| Python Type | CLI Type |
|-------------|----------|
| `str` | Text argument |
| `int` | Integer (validated) |
| `float` | Decimal number |
| `bool` | Flag (`--name/--no-name`) |
| `Path` | File/directory path |
| `Optional[str]` | Optional argument |