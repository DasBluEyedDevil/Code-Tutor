---
type: "KEY_POINT"
title: "Creating Optionals"
---

Create Optionals using factory methods, NEVER with the constructor:

Optional.of(value): When value is definitely non-null
  Optional<String> opt = Optional.of("hello");
  // Throws NullPointerException if value is null!

Optional.ofNullable(value): When value might be null
  Optional<String> opt = Optional.ofNullable(maybeNull);
  // Safe: wraps null as empty Optional

Optional.empty(): Explicit empty Optional
  Optional<String> empty = Optional.empty();

RULES:
- NEVER pass null to Optional.of() - use ofNullable()
- NEVER call get() without checking isPresent() first
- NEVER use Optional for class fields - use it for return types
- NEVER use Optional as method parameter - just use the value

Optional is designed for return types to indicate 'might not have a result'.