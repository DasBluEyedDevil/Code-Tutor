---
type: "THEORY"
title: "Syntax Breakdown"
---

This project introduces one new concept: **converting text to numbers**.

- **`int(age)`** - This converts text to an integer (whole number).

  **Why is this necessary?** When you use `input()`, Python treats everything as text (a string), even if the user types a number.

  ```python
  age = input("How old are you? ")  # If user types "24", age = "24" (text)
  age_months = age * 12              # ❌ Error! Can't multiply text

  age = input("How old are you? ")   # User types "24", age = "24" (text)
  age_months = int(age) * 12         # ✅ Works! int() converts "24" to 24 (number)
  ```

- **The `int()` function explained:**

  - `int` is short for "integer" (a whole number)
  - It takes text that looks like a number and converts it to an actual number
  - `int("24")` → `24`
  - `int("100")` → `100`

- **Using the converted number:**

  ```python
  age_months = int(age) * 12
  age_days = int(age) * 365
  ```

  Now we can do math! `*` means multiply, so if age is 24:
  - `24 * 12 = 288` months
  - `24 * 365 = 8,760` days

- **Everything else:** The rest is just clever use of what you already know:

  - Variables to remember information
  - F-strings to create personalized messages
  - String multiplication (`"="*50`) for decorative lines
  - Good prompts to make the conversation natural
