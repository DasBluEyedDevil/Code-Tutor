---
type: "EXAMPLE"
title: "Connecting Local Repo to GitHub"
---

There are two ways to connect your code to GitHub:

OPTION 1: Push an existing local repository to GitHub

```bash
# 1. Create a new repository on GitHub (github.com/new)
#    - Give it a name (e.g., 'java-calculator')
#    - DON'T initialize with README (we have local commits)

# 2. In your local project, add the remote
$ cd JavaCalculator
$ git remote add origin git@github.com:yourusername/java-calculator.git

# 3. Push your code to GitHub
$ git push -u origin main
  # -u sets up tracking (only needed first time)

# Verify the remote
$ git remote -v
origin  git@github.com:yourusername/java-calculator.git (fetch)
origin  git@github.com:yourusername/java-calculator.git (push)


# OPTION 2: Clone an existing GitHub repository
$ git clone git@github.com:someuser/some-project.git
$ cd some-project
# You're ready to work - remote is already configured!
```
