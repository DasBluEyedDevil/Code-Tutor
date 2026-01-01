---
type: "THEORY"
title: "Either - Typed Error Handling"
---


### What is Either?

`Either<L, R>` represents a value that is one of two types:
- `Left(value)` - conventionally the "error" case
- `Right(value)` - conventionally the "success" case (mnemonic: "right" means correct)

### Compared to Result

| Feature | `Result<T>` | `Either<E, T>` |
|---------|-------------|----------------|
| Error type | `Throwable` only | Any type `E` |
| Type safety | Limited | Full |
| Pattern matching | Limited | Exhaustive |
| Chaining | `mapCatching` | `flatMap`, `either { }` |

---

