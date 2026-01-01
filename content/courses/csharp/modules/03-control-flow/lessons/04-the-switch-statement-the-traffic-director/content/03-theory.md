---
type: "THEORY"
title: "Syntax Breakdown"
---

## Switch Statement (Classic)

**`switch (variable)`**: The variable in parentheses is what you're checking against each 'case'.

**`case value:`**: Each 'case' is a possible value. Must be a constant (like 1, "text", etc.)!

**`break;`**: CRITICAL! 'break' exits the switch. Don't forget it!

**`default:`**: Runs if no case matches. Like 'else' at the end.

## Switch Expression (Modern C# 8+)

**`variable switch { }`**: Note the variable comes BEFORE 'switch'! The whole thing RETURNS a value.

**`pattern => result`**: Use `=>` (arrow), not `:`. Each arm produces a value.

**`_`**: The discard pattern - matches everything. It's your default case.

**`Patterns`**: Use `or`/`and`/`not` keywords (not || or &&), plus relational operators like `>= 90`.