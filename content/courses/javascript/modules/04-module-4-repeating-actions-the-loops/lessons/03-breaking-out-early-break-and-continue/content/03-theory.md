---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Two flow control keywords:

**break**
- Immediately exits the loop
- Execution continues after the loop
- Use when you've found what you're looking for
- Use when a condition makes continuing pointless

Example:
for (let i = 0; i < 100; i++) {
  if (found) {
    break;  // Don't check remaining 90 items
  }
}
console.log('Continue here');  // This runs after break

**continue**
- Skips the rest of the current iteration
- Goes directly to the next iteration
- Use to skip invalid/unwanted values
- Use to avoid deep nesting

Example:
for (let i = 0; i < 10; i++) {
  if (i === 5) {
    continue;  // Skip when i is 5
  }
  console.log(i);  // This doesn't run when i is 5
  // But runs for all other values
}

Comparing break vs continue:

break:
for (let i = 0; i < 5; i++) {
  if (i === 3) break;
  console.log(i);
}
// Prints: 0, 1, 2 (then EXITS loop)

continue:
for (let i = 0; i < 5; i++) {
  if (i === 3) continue;
  console.log(i);
}
// Prints: 0, 1, 2, 4 (SKIPS 3, continues loop)

Important: break and continue only affect the INNERMOST loop they're in. If you have nested loops, they don't break out of all loops.