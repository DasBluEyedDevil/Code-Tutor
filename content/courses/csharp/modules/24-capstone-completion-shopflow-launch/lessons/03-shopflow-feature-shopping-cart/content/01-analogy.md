---
type: "ANALOGY"
title: "The Shopping Cart as Session State"
---

Think of a shopping cart like a personal shopping assistant following you through a store. When you are an anonymous visitor, the assistant holds your items but does not know who you are. They recognize you by a claim ticket, a temporary identifier that connects you to your selections. If you lose that ticket or close your browser, the assistant cannot find your items again.

When you log in, something magical happens. The assistant now knows your name and face. They can remember your cart even if you leave and come back days later. More importantly, if you had been shopping anonymously and then decide to log in, a good assistant merges your anonymous selections with any items you might have saved from a previous logged-in session. This cart merge operation is one of the trickiest parts of e-commerce implementation.

The assistant also needs to decide where to store your items. Carrying them around in their arms is fast but risky, as the items disappear if the assistant goes home for the night. Writing them in a notebook is more permanent but slower to access. The best assistants use a combination: quick access for active shoppers with periodic saves to the permanent record.

This maps directly to our technical decisions. Anonymous carts use session or local storage identifiers. Authenticated carts persist to the database under the user's ID. The merge operation reconciles anonymous and authenticated carts on login. And our storage strategy balances between distributed cache for speed and database for durability.