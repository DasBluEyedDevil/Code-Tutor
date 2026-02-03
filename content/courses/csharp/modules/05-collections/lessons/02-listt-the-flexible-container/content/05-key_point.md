---
type: "KEY_POINT"
title: "List<T> as Your Default Collection"
---

## Key Takeaways

- **`List<T>` grows and shrinks dynamically** -- `.Add()` appends, `.Remove()` deletes, `.Insert()` places at a specific index. No fixed size limit.

- **Use `.Count` not `.Length`** -- lists use `.Count`, arrays use `.Length`. Both tell you the number of elements, but mixing them up causes compiler errors.

- **`List<T>` should be your default collection** -- unless you have a specific reason to use an array (fixed size, maximum performance), start with `List<T>`. It covers the vast majority of use cases.
