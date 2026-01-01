---
type: "THEORY"
title: "Creating and Switching Branches"
---

VIEWING BRANCHES:

$ git branch              # List local branches
* main                    # The * shows current branch
  feature/login
  feature/payment

$ git branch -a           # List all branches (including remote)


CREATING A NEW BRANCH:

$ git branch feature/add-multiply    # Create branch
$ git checkout feature/add-multiply  # Switch to it

Or use the shorthand:
$ git checkout -b feature/add-multiply  # Create AND switch

Or the modern way (Git 2.23+):
$ git switch -c feature/add-multiply    # Create AND switch


SWITCHING BRANCHES:

$ git checkout main       # Switch to main branch
$ git switch main         # Modern alternative

When you switch branches:
- Your working directory changes to that branch's files
- It's like stepping into a parallel universe
- Uncommitted changes come with you (be careful!)


DELETING A BRANCH:

$ git branch -d feature/old-branch    # Delete (safe - only if merged)
$ git branch -D feature/old-branch    # Force delete (even if not merged)