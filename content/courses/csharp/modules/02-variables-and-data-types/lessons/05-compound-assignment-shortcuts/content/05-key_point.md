---
type: "KEY_POINT"
title: "Compound Assignment and Increment Operators"
---

## Key Takeaways

- **`+=`, `-=`, `*=`, `/=` modify in place** -- `score += 50` is shorthand for `score = score + 50`. Fewer characters means fewer typos, and the intent (modify this variable) is clearer.

- **`++` and `--` add or subtract 1** -- `lives--` decrements by one. These are the most common loop operators. For simple statements, prefix (`++x`) and postfix (`x++`) behave identically.

- **Prefix vs postfix matters in expressions** -- `Console.WriteLine(++x)` increments first then prints. `Console.WriteLine(x++)` prints first then increments. When in doubt, put the increment on its own line.
