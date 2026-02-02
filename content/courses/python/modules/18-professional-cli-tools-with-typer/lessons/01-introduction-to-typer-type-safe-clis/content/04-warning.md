---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting to Call typer.run()**
```python
# WRONG - Function is defined but never runs
import typer

def main(name: str):
    print(f"Hello, {name}")

# Missing typer.run(main)!

# CORRECT - Function is registered as CLI
import typer

def main(name: str):
    print(f"Hello, {name}")

if __name__ == "__main__":
    typer.run(main)
```

**2. Using Wrong Type Hints for Optional Arguments**
```python
# WRONG - Optional without default makes it required
def main(name: str, count: int):
    pass  # count is required!

# CORRECT - Default value makes it optional
def main(name: str, count: int = 1):
    pass  # count is optional with default 1
```

**3. Confusing Positional Args and Options**
```python
# WRONG - Expecting --name flag but getting positional
def main(name: str):  # This is POSITIONAL, not --name
    pass

# CORRECT - Use Annotated for explicit control
from typing import Annotated
import typer

def main(name: Annotated[str, typer.Option()]):
    pass  # Now it's --name flag
```

**4. Missing Type Hints Entirely**
```python
# WRONG - No type hint means Typer can't validate
def main(count):  # What type is count?
    print(count * 2)

# CORRECT - Type hint enables validation
def main(count: int):
    print(count * 2)  # Typer validates it's an integer
```

**5. Not Installing typer[all] for Rich Features**
```python
# WRONG - Missing Rich integration
# pip install typer  # Basic install only

# CORRECT - Full install with Rich
# uv add typer[all]  # or: pip install typer[all]
# Now you get colored output and better error messages
```