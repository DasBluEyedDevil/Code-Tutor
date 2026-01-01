---
type: "EXAMPLE"
title: "A Day in the Life with Git"
---

Here's how a typical development session looks with Git:

Morning:
- You open your Java project
- You check the current status: git status
- You see no uncommitted changes - starting fresh!

Coding Session:
- You write a new method for validating user input
- You test it - it works!
- You stage your changes: git add UserValidator.java
- You commit: git commit -m 'Add input validation for email field'

Afternoon:
- You realize the validation has a bug with edge cases
- You compare with yesterday's version: git diff HEAD~1
- You see exactly what you changed
- You fix the bug and commit again

End of Day:
- You push your changes to GitHub: git push
- Your code is safely backed up in the cloud
- Your teammates can see your work tomorrow

This workflow becomes second nature - you'll commit dozens of times per day!

```bash
// Example: Your first day using Git

// Terminal commands (you'll learn these in the next lesson)
$ git init                              // Start tracking this project
$ git status                            // See what's changed
$ git add UserValidator.java            // Stage the file
$ git commit -m 'Add email validation'  // Save the snapshot
$ git log                               // See your history
$ git push origin main                  // Upload to GitHub
```
