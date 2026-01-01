---
type: "THEORY"
title: "Understanding the Concept"
---

Imagine you're searching through a stack of 100 papers for one specific document:

<ul style='background-color: #f0f0f0; padding: 15px;'>- **Normal loop:** Check all 100 papers, even after finding the one you need (wasteful!)
- **With break:** Stop immediately when you find it - paper #23? Done! Skip the remaining 77.

Or imagine processing a list of test scores, but some entries are marked as "invalid":

<ul style='background-color: #e3f2fd; padding: 15px;'>- **Normal loop:** Try to process every entry, causing errors on invalid ones
- **With continue:** Skip invalid entries, continue with the next valid one

This is **loop control** - fine-tuning how loops execute with three special statements:

- **break**: "Stop the loop entirely and exit"
- **continue**: "Skip the rest of this iteration and move to the next"
- **pass**: "Do nothing, but don't leave this block empty"

### Real-World Examples:

- **Password attempts (break)**:
WHILE attempts < 3:
&nbsp;&nbsp;&nbsp;&nbsp;Get password
&nbsp;&nbsp;&nbsp;&nbsp;IF correct → **break** (exit loop)
&nbsp;&nbsp;&nbsp;&nbsp;attempts++
- **Processing records (continue)**:
FOR each record:
&nbsp;&nbsp;&nbsp;&nbsp;IF record is corrupted → **continue** (skip it)
&nbsp;&nbsp;&nbsp;&nbsp;Process valid record
- **Menu system (break)**:
WHILE True:
&nbsp;&nbsp;&nbsp;&nbsp;Show menu
&nbsp;&nbsp;&nbsp;&nbsp;Get choice
&nbsp;&nbsp;&nbsp;&nbsp;IF choice == "quit" → **break**
- **Data filtering (continue)**:
FOR each email:
&nbsp;&nbsp;&nbsp;&nbsp;IF is_spam → **continue**
&nbsp;&nbsp;&nbsp;&nbsp;Add to inbox

These statements give you surgical precision over loop execution!