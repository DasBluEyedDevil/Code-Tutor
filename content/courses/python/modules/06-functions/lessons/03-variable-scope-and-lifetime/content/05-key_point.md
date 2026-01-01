---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Local variables** are created inside functions and only exist while the function runs
- **Global variables** are created outside functions and exist for the entire program
- **Reading globals** works without any special keyword
- **Modifying globals** requires the `global` keyword inside the function
- **Same names can exist** in different scopes without conflict
- **LEGB rule**: Python searches Local, Enclosing, Global, Built-in (in that order)
- **Best practice**: Minimize use of global variables - they make code harder to debug
- **Parameters are local** - They're like local variables that get their initial value from the caller
- **Return values** are the preferred way to get data out of functions (not global variables)