---
type: "THEORY"
title: "The Data Sorting Machine"
---

Imagine a machine that sorts incoming items into specific trays based on what they are. This machine is JavaScript's **Type System**.

1.  **The Text Tray (Strings):** This is for anything that is "content." It doesn't matter if it's a single letter, a full sentence, or even a number wrapped in quotes (like `"123"`). These are like labels or letters—you can't add them together mathematically, but you can join them to make longer messages.
2.  **The Calculation Tray (Numbers):** This is for pure values. Whole numbers, decimals, or even negative numbers. These go into the tray that is connected to a calculator. You can perform math on them, compare their sizes, and use them for counts.
3.  **The Logic Switch (Booleans):** This is the simplest tray. It only has two positions: **True** (on) or **False** (off). This is what the computer uses to make decisions later on.
4.  **The Empty Tray (Null & Undefined):** Sometimes, an item is missing or hasn't arrived yet. JavaScript has special trays for "nothingness"—either because we haven't decided what goes there yet (`undefined`) or because we explicitly want to say "this is empty" (`null`).

Why does this matter? Because JavaScript treats these trays very differently. If you try to multiply a "Word" by a "Number," the sorting machine gets confused and might give you a strange result like `NaN` (Not a Number)!
