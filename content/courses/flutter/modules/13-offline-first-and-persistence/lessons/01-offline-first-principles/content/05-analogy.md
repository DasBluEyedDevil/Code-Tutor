---
type: "ANALOGY"
title: "The Notebook You Always Carry"
---

Imagine you keep all your important notes in a cloud document that requires internet access. You are on a plane, in a tunnel, or in a rural area with no signal -- and suddenly you cannot access anything. Now imagine you also keep a physical notebook in your pocket with the same notes. You can always read and write in your notebook, and when you get back online, you sync any changes with the cloud version.

**Offline-first means your app always has that pocket notebook.** The local database (Drift, Hive, or SQLite) is the notebook. Your app reads from and writes to it first, regardless of network status. When connectivity returns, a sync engine reconciles the local notebook with the server, resolving any conflicts along the way.

The critical shift in thinking is this: the network is not a requirement, it is a bonus. Your app should work perfectly with just the notebook. The cloud sync makes it better, but never essential for basic operation.
