---
type: KEY_POINT
---

- Extract repeated UI into custom `StatelessWidget` classes to make code reusable and each file focused on one component
- Mark all fields as `final` in widget classes -- widgets are immutable; state belongs in `State` objects
- Pass data through constructor parameters to make widgets configurable: `const ProductCard({required this.title, required this.price})`
- Use `const` constructors when all fields are compile-time constants to enable Flutter's build optimization
- Compose small widgets into larger ones -- a `PostWidget` can contain `UserAvatar`, `PostImage`, and `ActionBar` widgets
