---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Version catalogs centralize dependency versions** in `gradle/libs.versions.toml`, enabling consistent versions across modules and easier upgrades. Reference dependencies via type-safe accessors: `libs.ktor.server.core`.

**Use version ranges cautiously**—`1.+` enables automatic minor updates but risks breaking changes. Pin exact versions in production apps for reproducible builds; use ranges only for libraries.

**Dependency resolution conflicts occur when different libraries require different versions** of the same dependency. Gradle picks the highest version by default; use `constraints` or `force` to override when needed.
