---
type: "EXAMPLE"
title: "Complete Workflow: init, add, commit"
---

Let's walk through a complete example of creating a new Java project and making your first commits.

```bash
# Step 1: Create project directory
$ mkdir JavaCalculator
$ cd JavaCalculator

# Step 2: Initialize Git repository
$ git init
Initialized empty Git repository in /home/user/JavaCalculator/.git/

# Step 3: Create your first Java file
$ echo 'public class Calculator {
    public int add(int a, int b) {
        return a + b;
    }
}' > Calculator.java

# Step 4: Check status - file is untracked
$ git status
Untracked files:
  Calculator.java

# Step 5: Stage the file
$ git add Calculator.java

# Step 6: Check status again - file is staged
$ git status
Changes to be committed:
  new file: Calculator.java

# Step 7: Commit with a message
$ git commit -m 'feat: Add Calculator class with add method'
[main (root-commit) a1b2c3d] feat: Add Calculator class with add method
 1 file changed, 5 insertions(+)
 create mode 100644 Calculator.java

# Step 8: Verify the commit
$ git log --oneline
a1b2c3d (HEAD -> main) feat: Add Calculator class with add method
```
