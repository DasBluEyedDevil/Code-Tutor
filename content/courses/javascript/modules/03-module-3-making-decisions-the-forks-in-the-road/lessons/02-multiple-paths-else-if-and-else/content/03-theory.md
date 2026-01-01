---
type: "THEORY"
title: "Mutually Exclusive Logic"
---

The `else if` and `else` keywords allow you to expand a simple "Yes/No" decision into a "Case A / Case B / Case C" decision.

### 1. The `else if` Bridge
You can have as many `else if` statements as you want between your `if` and your `else`. They are only checked if the previous conditions were **false**.

### 2. The `else` Safety Net
The `else` block is optional, but it acts as a "catch-all." It has no condition in parentheses because it simply runs if **everything else** failed. It’s like the "Otherwise" in a sentence.

### 3. Mutual Exclusivity
A key feature of the `if/else if` chain is that **only one code block will run**. Even if multiple conditions are technically true, JavaScript only executes the block for the *first* true condition it encounters.

### 4. The Importance of Order
Because JavaScript stops at the first true condition, you must put your **most specific** conditions at the top and the **most general** ones at the bottom.
*   **Wrong:** `if (age > 0) ... else if (age > 18) ...` (Everyone is older than 0, so the 18+ check never happens!)
*   **Right:** `if (age > 18) ... else if (age > 0) ...`
