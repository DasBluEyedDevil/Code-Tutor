---
type: "EXAMPLE"
title: "Forking and Contributing to Open Source"
---

Contributing to open source is a great way to learn and build your portfolio. Here's the workflow:

```bash
# === STEP 1: FORK THE REPOSITORY ===
# On GitHub: Click 'Fork' button on the project page
# This creates YOUR copy at github.com/YOU/project

# === STEP 2: CLONE YOUR FORK ===
$ git clone git@github.com:YOUR-USERNAME/project.git
$ cd project

# === STEP 3: ADD ORIGINAL AS 'UPSTREAM' ===
$ git remote add upstream git@github.com:ORIGINAL-OWNER/project.git
$ git remote -v
origin    git@github.com:YOUR-USERNAME/project.git (fetch)
upstream  git@github.com:ORIGINAL-OWNER/project.git (fetch)

# === STEP 4: CREATE A BRANCH FOR YOUR CONTRIBUTION ===
$ git switch -c fix/typo-in-readme

# === STEP 5: MAKE YOUR CHANGES ===
$ # edit files...
$ git add README.md
$ git commit -m 'docs: Fix typo in installation instructions'

# === STEP 6: PUSH TO YOUR FORK ===
$ git push -u origin fix/typo-in-readme

# === STEP 7: CREATE PULL REQUEST ===
# Go to the ORIGINAL project on GitHub
# Click 'New Pull Request'
# Choose 'compare across forks'
# Select your fork and branch
# Write a description and submit!

# === KEEP YOUR FORK UPDATED ===
$ git fetch upstream
$ git switch main
$ git merge upstream/main
$ git push origin main
```
