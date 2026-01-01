---
type: "ANALOGY"
title: "Understanding the Concept"
---

Using an API is like ordering from a restaurant:

1. **You look at the menu** (API documentation) - What dishes (endpoints) are available?
2. **You place an order** (fetch request) - 'I'll have the user data for ID 123, please'
3. **Kitchen prepares it** (server processes) - Takes time, you wait
4. **Server brings your food** (response) - Here's your data!
5. **You eat it** (use the data) - Display on your webpage

APIs (Application Programming Interfaces) are how websites talk to servers. fetch() is JavaScript's built-in way to request data from APIs. It returns a Promise, so we use async/await!