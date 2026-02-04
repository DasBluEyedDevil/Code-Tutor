---
type: "ANALOGY"
title: "Filing Cabinets vs. Sticky Notes vs. Spreadsheets"
---

Local storage options in Flutter are like different ways to organize information in an office. **SharedPreferences** is a sticky note on your monitor -- great for a few simple reminders like "dark mode: on" or "last login: Tuesday," but terrible for organizing hundreds of records. **Hive** is a filing cabinet with labeled folders -- fast to open, no setup required, and good for structured data that does not need complex queries. **Drift (SQLite)** is a full spreadsheet application -- it handles complex queries, relationships between tables, and migrations when your data structure changes.

Choosing the wrong tool creates real problems. Storing user preferences in SQLite is like maintaining a spreadsheet just to remember whether dark mode is on. Storing a thousand chat messages in SharedPreferences is like writing a novel on sticky notes. And trying to query relational data in Hive is like searching a filing cabinet for "all invoices from customers who also bought product X" -- possible, but painful.

**Match the storage tool to the data shape.** Simple key-value pairs go in SharedPreferences. Structured objects with fast reads go in Hive. Relational data with complex queries goes in Drift.
