---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`int playerScore = 1500;`**: This creates an integer box AND puts 1500 in it immediately. This is called 'declaration with initialization' - doing both steps at once!

**`double temperature = -5.5;`**: Decimals use a PERIOD (.), not a comma! In C#, -5.5 is correct, -5,5 is wrong. Double is great for scientific calculations where tiny precision errors are acceptable.

**`decimal price = 19.99m;`**: The `m` suffix marks this as a decimal literal. Use `decimal` for money because it stores numbers in base-10, avoiding the rounding errors that `double` can have with values like 0.1.

**`int totalScore = playerScore + 500;`**: You can do math with variables! This takes the value from playerScore, adds 500, and stores the result in totalScore.