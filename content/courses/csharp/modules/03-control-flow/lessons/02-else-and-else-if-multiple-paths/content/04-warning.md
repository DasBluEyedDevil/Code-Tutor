---
type: "WARNING"
title: "Watch Out!"
---

## Common Pitfalls with else if Chains

**Wrong order of conditions:** If checking ranges, always start with the highest first! If you check `score >= 70` before `score >= 90`, a score of 95 will match the 70 check first and show 'C' instead of 'A'.

**Putting else if after else:** The final `else` must be LAST! C# will give you an error if you try to add `else if` after `else`.

**Overlapping conditions:** Make sure your conditions don't overlap unexpectedly. Each condition should be mutually exclusive or ordered from most specific to least specific.

**Missing final else:** While optional, forgetting a final `else` can lead to silent bugs where no code runs and you get unexpected behavior.