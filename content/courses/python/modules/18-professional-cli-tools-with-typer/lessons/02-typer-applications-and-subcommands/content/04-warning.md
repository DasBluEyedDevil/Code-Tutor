---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Calling typer.run() Instead of app()**
```python
# WRONG - typer.run() is for single-function CLIs
app = typer.Typer()

@app.command()
def hello():
    print("Hello")

if __name__ == "__main__":
    typer.run(hello)  # Wrong!

# CORRECT - Use app() for Typer applications
if __name__ == "__main__":
    app()  # Correct!
```

**2. Forgetting the @app.command() Decorator**
```python
# WRONG - Function not registered as command
app = typer.Typer()

def greet(name: str):  # Missing decorator!
    print(f"Hello {name}")

# CORRECT - Decorator registers the command
@app.command()
def greet(name: str):
    print(f"Hello {name}")
```

**3. Using Reserved Names Like 'list' Without Renaming**
```python
# WRONG - Shadows built-in list
@app.command()
def list():  # Shadows Python's list!
    pass

# CORRECT - Rename the function or command
@app.command(name="list")
def list_items():  # Different function name
    pass
```

**4. Not Using typer.Exit() for Error Codes**
```python
# WRONG - Just returning doesn't set exit code
@app.command()
def check(item: str):
    if not valid(item):
        print("Error!")
        return  # Exit code is still 0!

# CORRECT - Use typer.Exit for proper exit codes
@app.command()
def check(item: str):
    if not valid(item):
        print("Error!")
        raise typer.Exit(code=1)  # Exit code is 1
```

**5. Creating App Inside if __name__ Block**
```python
# WRONG - App created too late for decorators
if __name__ == "__main__":
    app = typer.Typer()  # Too late!
    app()

# CORRECT - App created at module level
app = typer.Typer()

@app.command()
def hello():
    print("Hello")

if __name__ == "__main__":
    app()
```