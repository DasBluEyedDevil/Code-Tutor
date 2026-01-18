---
type: "THEORY"
title: "The Labeled Storage Room"
---

Imagine you're managing a massive warehouse. Every item you store has a specific location, but instead of remembering row numbers and shelf codes like `0x7FFD2`, you use **labels**.

When you say `let apples = 5`, you are telling the computer: "Hey, reserve some space in your memory warehouse, put the number 5 in there, and let me refer to that space as 'apples' from now on."

#### Why labels matter
Without variables, you'd have to remember the exact value or where it lives every time you need it. If you use the price of an item ($19.99) in ten different calculations and the price changes, you'd have to find and update all ten lines. With a variable like `const basePrice = 19.99`, you update it **once**, and the change ripples through your entire "warehouse" automatically.

#### The difference between a Box and a Safe
In JavaScript, we have different types of containers:
1.  **The `let` Box:** A standard storage box. You can open it up, take out the contents, and put something else in later. This is for things that change, like your `gameScore` or the `currentTemperature`.
2.  **The `const` Safe:** Once you put something in and close the door, it's locked. You can see what's inside, but you can't replace it. This is for things that stay the same, like your `birthYear` or a `conversionRate`.