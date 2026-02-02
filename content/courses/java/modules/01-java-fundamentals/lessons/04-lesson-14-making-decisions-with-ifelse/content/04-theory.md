---
type: "THEORY"
title: "Adding 'Else' for Two Paths"
---

What if you want to do something different when the condition is false?
Use 'else':

int age = 15;
if (age >= 18) {
    IO.println("You can vote!");
} else {
    IO.println("Too young to vote.");
}

Now:
- If age >= 18 is TRUE → prints "You can vote!"
- If age >= 18 is FALSE → prints "Too young to vote."

Only ONE of these blocks will run, never both!