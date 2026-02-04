---
type: "KEY_POINT"
title: "Key Takeaways"
---

**`Either<E, A>` represents one of two values**—Left for errors, Right for success. It's more expressive than Result because error types are explicit: `Either<ValidationError, User>` vs `Result<User>`.

**`Option<A>` makes nullability explicit** in functional style—`Some(value)` for present values, `None` for absent. Use when you want FP composition operators (`map`, `flatMap`, `fold`) over nullable types.

**`Validated` accumulates multiple errors** instead of failing on first error like Either. Use for form validation where you want to show all errors at once rather than one at a time.
