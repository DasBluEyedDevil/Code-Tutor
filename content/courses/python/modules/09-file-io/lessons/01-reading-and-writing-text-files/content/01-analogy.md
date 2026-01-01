---
type: "ANALOGY"
title: "The Concept: Your Program's Filing Cabinet"
---

Until now, all your data disappeared when your program ended. Variables, lists, dictionaries - gone! Like writing on a whiteboard that gets erased every time you leave the room.

**Files are permanent storage** - like writing in a notebook instead of on a whiteboard. Data stays even after your program ends.

**Real-world analogy: A Filing Cabinet**

Opening a file is like opening a filing cabinet drawer:

**READ mode ('r'):** Open drawer to READ documents
- You can look at the contents
- You CANNOT add or change anything
- If the drawer (file) doesn't exist → Error!

**WRITE mode ('w'):** Open drawer to WRITE new documents
- You can add content
- WARNING: Erases everything that was there before (starts fresh)
- If drawer doesn't exist → Creates it automatically

**APPEND mode ('a'):** Open drawer to ADD MORE documents
- You can add content to the END
- Keeps existing content (doesn't erase)
- If drawer doesn't exist → Creates it

**Common operations:**

1. **Reading a file:**
   - Open filing cabinet ('r' mode)
   - Read the documents (read() or readlines())
   - Close filing cabinet

2. **Writing a file:**
   - Open filing cabinet ('w' mode)
   - Write your documents (write())
   - Close filing cabinet (IMPORTANT - saves changes!)

**Critical rule:** Always CLOSE files when done! Like closing the filing cabinet drawer. If you don't close it, changes might not be saved, and other programs can't access the file.

Python has a better way (context managers with `with`) that auto-closes files, which we'll learn in the next lesson.