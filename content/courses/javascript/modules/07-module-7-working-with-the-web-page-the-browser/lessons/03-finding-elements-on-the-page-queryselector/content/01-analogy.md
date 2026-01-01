---
type: "ANALOGY"
title: "The GPS for your Webpage"
---

Imagine you are looking for a specific house in a massive city. You have a few ways to describe where you want to go:

1.  **By Social Security Number (ID):** "Go to the house with the unique ID `house-42`." Only one house in the whole city has this ID.
2.  **By Neighborhood (Class):** "Go to all houses in the `HistoricDistict`." Many houses can share this label.
3.  **By Description (CSS Selector):** "Go to the first house that is a red brick building, inside a gated community, with a blue door."

In JavaScript, `querySelector` is your GPS. You give it a description (using the same language you use in CSS), and it travels through the DOM to find the matching element for you.

#### The "First Match" Rule
If you ask `querySelector` for a "house," and there are 100 houses in the city, it will stop at the **very first one** it finds and ignore the rest. If you want a list of *every* house, you need to use a different tool called `querySelectorAll`.
