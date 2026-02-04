---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Convention plugins enforce team standards** by centralizing configuration—one place to define Kotlin version, compiler flags, linting rules, test configurations. Apply plugins instead of duplicating configuration.

**Define plugins in `buildSrc` or composite builds** to share across projects. Convention plugins apply other plugins and configure them consistently: `id("kotlin-conventions")` applies Kotlin, sets JVM target, adds detekt.

**Version catalogs + convention plugins = maintainable build scripts**. Catalogs manage dependency versions, plugins manage configuration patterns. Individual module build files become trivial: apply plugins, declare dependencies.
