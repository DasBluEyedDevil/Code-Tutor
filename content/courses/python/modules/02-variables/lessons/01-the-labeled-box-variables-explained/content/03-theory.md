---
type: "THEORY"
title: "Syntax Breakdown"
---

Let's understand the rules for creating and using variables:

- **Creating a variable:** `variable_name = value`

  ```python
  name = "Sarah"  # Create a variable called 'name', put "Sarah" in it
  ```
  
  The `=` sign is like a label maker. It says: "From now on, whenever I say 'name', I mean 'Sarah'."

- **Variable naming rules:**

  - Can contain letters, numbers, and underscores: `my_age_2` ✅
  - Must start with a letter or underscore, NOT a number: `age2` ✅, `2age` ❌
  - Cannot contain spaces: `user_name` ✅, `user name` ❌
  - Case-sensitive: `Name` and `name` are different variables
  - Cannot use Python keywords (like `print`, `if`, `for`)

- **Good variable names are descriptive:**

  ```python
  # Bad - what does 'x' mean?
  x = 25

  # Good - clearly stores someone's age
  age = 25
  ```

- **Changing a variable's value:**

  ```python
  score = 100   # Start with 100
  score = 150   # Change it to 150
  # The old value (100) is gone; score now holds 150
  ```
  
  It's like taking items out of a labeled box and putting different items in!
