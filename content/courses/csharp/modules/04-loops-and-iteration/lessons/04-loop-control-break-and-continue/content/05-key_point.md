---
type: "KEY_POINT"
title: "Break and Continue for Loop Control"
---

## Key Takeaways

- **`break` exits the loop entirely** -- use it when you found what you are looking for and continuing would waste time. The program jumps to the first line after the loop.

- **`continue` skips the current iteration** -- the loop keeps running but the rest of the current iteration is skipped. Use it to bypass items that do not meet your criteria.

- **Both only affect the innermost loop** -- in nested loops, `break` exits only the loop it is directly inside. To exit multiple levels, use a flag variable or extract the logic into a method with `return`.
