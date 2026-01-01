---
type: "THEORY"
title: "Loop Control Flow"
---

Sometimes, the logic defined in the `for` or `while` header isn't enough. `break` and `continue` allow you to change the loop's behavior based on conditions *inside* the loop.

### 1. `break` (The Emergency Stop)
When JavaScript hits a `break` statement, it immediately jumps out of the current loop and continues with the first line of code *after* the loop's closing brace `}`. 
*   **Common Use Case:** Finding a specific result in a list (like a search function). Once you find it, you don't want to waste computer power looking at the rest of the list.

### 2. `continue` (The Skip Button)
When JavaScript hits a `continue` statement, it stops running the code for the *current* lap and immediately jumps to the next one.
*   **In a `for` loop:** It jumps to the increment (`i++`) step.
*   **In a `while` loop:** It jumps back to the condition check.
*   **Common Use Case:** Filtering out "bad" data. If a user record is missing an email address, you might `continue` to the next user instead of trying to send them an email.

### 3. Loop Scope
Crucially, these keywords only affect the **innermost** loop they are in. If you have a loop inside a loop, `break` will stop the inner loop, but the outer loop will keep running!
