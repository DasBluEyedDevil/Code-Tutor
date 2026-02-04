---
type: "ANALOGY"
title: "Reactive Queries as a News Ticker"
---

Reactive queries with Flow are like subscribing to a news ticker at the bottom of a TV screen.

**Traditional queries** are like checking a news website manually—you load the page, read the headlines, and if you want updates, you have to reload the page yourself. You're responsible for checking frequently enough to catch important news.

**Flow-based reactive queries** are like a live news ticker that automatically updates—you "tune in" once by collecting the Flow, and whenever there's breaking news (database changes), it streams to your screen automatically. You don't poll or manually refresh; new data just appears.

**Database writes are the news events**—when you insert a new row or update existing data, that's "breaking news". Every Flow collecting that query receives the update immediately, just like all TVs tuned to that news channel see the ticker update simultaneously.

This eliminates the cache invalidation problem—you never show stale data because the UI is always observing the latest database state through the Flow ticker.
