---
type: "EXAMPLE"
title: "Practical Branching Workflow"
---

Let's create a feature branch, make changes, and prepare to merge.

```bash
# Start on main branch with a working Calculator
$ git branch
* main

# Create and switch to a new feature branch
$ git switch -c feature/add-multiply
Switched to a new branch 'feature/add-multiply'

# Verify we're on the new branch
$ git branch
  main
* feature/add-multiply

# Make changes to Calculator.java - add multiply method
$ echo '    public int multiply(int a, int b) {
        return a * b;
    }' >> Calculator.java

# Stage and commit the changes
$ git add Calculator.java
$ git commit -m 'feat: Add multiply method to Calculator'
[feature/add-multiply b2c3d4e] feat: Add multiply method to Calculator
 1 file changed, 3 insertions(+)

# View the log - shows our new commit
$ git log --oneline
b2c3d4e (HEAD -> feature/add-multiply) feat: Add multiply method
a1b2c3d (main) feat: Add Calculator with add method

# Switch back to main - notice the multiply code disappears!
$ git switch main
$ cat Calculator.java   # Only has add method - multiply isn't here

# Switch back to feature branch - multiply code reappears!
$ git switch feature/add-multiply
$ cat Calculator.java   # Has both add and multiply methods
```
