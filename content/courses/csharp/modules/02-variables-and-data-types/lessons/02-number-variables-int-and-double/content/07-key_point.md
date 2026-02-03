---
type: "KEY_POINT"
title: "Choosing the Right Numeric Type"
---

## Key Takeaways

- **Use `int` for whole numbers, `double` for measurements, `decimal` for money** -- `int` is fastest for counting. `double` has tiny rounding errors (fine for science). `decimal` is exact for financial calculations -- always use it for prices and payments.

- **The `m` suffix marks decimal literals** -- write `19.99m` not `19.99`. Without the `m`, C# treats it as a `double` and the compiler will report an error.

- **Integer division truncates** -- `10 / 3` equals `3`, not `3.333`. The decimal part is discarded. Cast to `double` first if you need the fractional result: `(double)10 / 3`.
