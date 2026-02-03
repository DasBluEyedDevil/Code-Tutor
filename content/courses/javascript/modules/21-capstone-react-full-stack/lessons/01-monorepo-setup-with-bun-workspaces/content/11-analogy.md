---
type: "ANALOGY"
title: "The Apartment Building"
---

A monorepo is like an apartment building. Each package (api, web, shared) is a separate apartment with its own layout and purpose, but they all share the same foundation, plumbing, and electrical system (the root `package.json` and shared dependencies). The `shared` package is like the building's common area -- utilities that every apartment can access. Bun workspaces act as the building manager, coordinating installations and making sure packages can reference each other without publishing to npm first.
