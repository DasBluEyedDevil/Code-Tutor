---
type: "THEORY"
title: "Exercise 3: Plugin System"
---


**Goal**: Create an extensible plugin system.

**Requirements**:
1. Interface `Plugin` with properties: `name`, `version`, methods: `initialize()`, `execute()`, `shutdown()`
2. Interface `Configurable` with method: `configure(settings: Map<String, String>)`
3. Create 3 different plugin types
4. Create a `PluginManager` that loads and manages plugins

---

