---
type: "ANALOGY"
title: "FP as a Conveyor Belt"
---

Functional programming is like a factory conveyor belt that transforms raw materials into finished products.

**Imperative programming is a worker modifying items in place**—taking a box, opening it, removing items, adding new ones, and handing the same box to the next person. If someone changes the box while others are using it, chaos ensues (race conditions).

**Functional programming is a conveyor belt with transformation stations**—each station takes items off the belt, creates new items based on what it saw, and places the new items back on the belt. The original items keep moving unchanged past the station.

**Pure functions are transformation stations**—they always produce the same output for the same input, don't modify what they receive, and don't affect other stations. If station A always turns red balls into blue cubes, you can predict its behavior perfectly.

**Composition is connecting stations in sequence**—the output of one station becomes the input to the next. Connect "wash", "cut", "package" stations, and you've built a complete production line from pure transformations.

**Immutability means items never change**—a red ball entering the "paint blue" station doesn't become blue; a new blue ball is created. The red ball continues existing for anyone else who needs it. This eliminates confusion about which version of an item you're looking at.

In imperative code, you modify boxes (mutable state). In functional code, you transform items on a belt (immutable transformations). The belt approach scales to parallel assembly lines (concurrency) without stations interfering with each other.
