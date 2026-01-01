---
type: "ANALOGY"
title: "The Treasure Chest Inventory"
---

Imagine you've found a mysterious treasure chest. It has a bunch of items inside, and each item has a little tag tied to it describing what it is (e.g., "Weapon", "Gold Coins", "Magic Scroll").

You want to make a list of everything in the chest, but you don't know ahead of time what the tags say.

To do this, you have three options:
1.  **List just the tags:** "Give me a list of all the names written on the tags." (`Object.keys`)
2.  **List just the items:** "Forget the tags, just show me the gold and the weapons." (`Object.values`)
3.  **List both:** "Give me a list of every tag and the item it belongs to." (`Object.entries`)

In programming, we call this **Object Iteration**. Since objects aren't numbered lists like arrays, we need these special "viewing modes" to loop through them and see what's inside.
