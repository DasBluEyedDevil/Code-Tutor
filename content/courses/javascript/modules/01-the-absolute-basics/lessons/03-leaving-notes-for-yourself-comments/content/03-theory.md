---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Two types of comments:

1. Single-line comments: //
   - Everything after // on that line is ignored
   - Great for short notes
   - Example: // This calculates the total price

2. Multi-line comments: /* */
   - Everything between /* and */ is ignored, even across multiple lines
   - Great for longer explanations or temporarily disabling multiple lines of code
   - Example:
     /*
       This function is complex, so here's how it works:
       First, it checks if the user is logged in...
     */

Pro tip: Use comments to explain WHY, not WHAT. The code itself shows WHAT it does. Comments should explain WHY you made that choice.

Good comment: // Using 30-day trial period instead of 7-day based on user feedback
Bad comment: // This sets the trial period to 30