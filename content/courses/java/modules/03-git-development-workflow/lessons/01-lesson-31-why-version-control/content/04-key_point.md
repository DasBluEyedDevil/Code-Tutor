---
type: "KEY_POINT"
title: "The Git Workflow: Edit, Stage, Commit"
---

The basic Git workflow has three steps:

1. EDIT: Make changes to your files (write code!)
   └─> Changes exist only in your working directory

2. STAGE: Choose which changes to include in your next snapshot
   └─> git add <file> moves changes to the staging area

3. COMMIT: Save the staged changes as a permanent snapshot
   └─> git commit -m 'description' creates the snapshot

Think of it like packing for a trip:
1. You gather items from around your house (EDITING)
2. You put selected items in your suitcase (STAGING)
3. You close and lock the suitcase (COMMITTING)

You can open the suitcase later (go back to that commit) and find exactly what you packed!