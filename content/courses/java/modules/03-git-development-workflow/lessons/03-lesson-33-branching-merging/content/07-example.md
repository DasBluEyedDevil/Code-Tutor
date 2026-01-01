---
type: "EXAMPLE"
title: "Complete Branch and Merge Example"
---

Here's a complete workflow showing branching, making changes, merging, and handling a conflict.

```bash
# === SETUP: Start with a clean main branch ===
$ git switch main
$ git log --oneline
a1b2c3d (HEAD -> main) Initial Calculator with add method

# === STEP 1: Create and work on feature branch ===
$ git switch -c feature/subtract
$ # Edit Calculator.java to add subtract method
$ git add Calculator.java
$ git commit -m 'feat: Add subtract method'

# === STEP 2: Meanwhile, someone else updates main ===
$ git switch main
$ # Edit Calculator.java to add comments
$ git add Calculator.java  
$ git commit -m 'docs: Add comments to add method'

# === STEP 3: Merge feature back to main ===
$ git merge feature/subtract
# If no conflict: Merge made by the 'ort' strategy.
# If conflict: CONFLICT (content): Merge conflict in Calculator.java

# === STEP 4: If there's a conflict, resolve it ===
$ git status  # Shows conflicted files
# Edit Calculator.java - remove conflict markers, keep both changes
$ git add Calculator.java
$ git commit -m 'Merge feature/subtract into main'

# === STEP 5: Clean up ===
$ git branch -d feature/subtract   # Delete merged branch
$ git log --oneline --graph        # See the merge visually
* e5f6g7h (HEAD -> main) Merge feature/subtract into main
|\
| * c3d4e5f (feature/subtract) feat: Add subtract method
* | b2c3d4e docs: Add comments to add method
|/
* a1b2c3d Initial Calculator with add method
```
