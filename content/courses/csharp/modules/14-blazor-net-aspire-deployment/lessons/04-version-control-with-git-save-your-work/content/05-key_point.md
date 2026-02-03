---
type: "KEY_POINT"
title: "Git Essentials for .NET Projects"
---

## Key Takeaways

- **Commit often with meaningful messages** -- use present tense: "Add product endpoint" not "Added product endpoint." Each commit should represent one logical change.

- **`.gitignore` must exclude build outputs** -- always ignore `bin/`, `obj/`, `.vs/`, `*.user`, and secret files. Never commit `appsettings.Development.json` with real connection strings or API keys.

- **Branches isolate features** -- work on a branch (`git checkout -b feature/cart`), then merge to main when complete. This protects the main branch from incomplete work.
