---
type: "THEORY"
title: "git add - Staging Changes"
---

The 'git add' command moves changes from your working directory to the staging area.

Think of staging as choosing which photos to put in an album:
- You have 20 photos (all your changed files)
- You select 5 for this album (stage specific files)
- You create the album (commit those 5)

COMMANDS:

1. Stage a specific file:
   $ git add HelloWorld.java

2. Stage multiple specific files:
   $ git add HelloWorld.java UserService.java

3. Stage all files in current directory:
   $ git add .

4. Stage all changes in the entire repository:
   $ git add -A
   or
   $ git add --all

5. Stage parts of a file interactively:
   $ git add -p HelloWorld.java

BEST PRACTICES:
- Don't blindly 'git add .' - review what you're staging
- Stage related changes together
- Keep commits focused on one logical change