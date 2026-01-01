---
type: "THEORY"
title: "Handling Merge Conflicts"
---

A MERGE CONFLICT happens when Git can't automatically combine changes.

WHEN DO CONFLICTS OCCUR?
- Two branches modified the same line in the same file
- One branch deleted a file that another branch modified

WHAT HAPPENS DURING A CONFLICT:

$ git merge feature/new-algorithm
Auto-merging Calculator.java
CONFLICT (content): Merge conflict in Calculator.java
Automatic merge failed; fix conflicts and then commit the result.


WHAT THE CONFLICT LOOKS LIKE IN THE FILE:

public int add(int a, int b) {
<<<<<<< HEAD
    // Main branch version
    return a + b;
=======
    // Feature branch version
    int sum = a + b;
    System.out.println("Sum: " + sum);
    return sum;
>>>>>>> feature/new-algorithm
}

MEANING:
- <<<<<<< HEAD: Start of your current branch's version
- =======: Separator between the two versions
- >>>>>>> feature/...: Start of incoming branch's version


RESOLVING THE CONFLICT:

1. Open the file in your editor
2. Decide which version to keep (or combine them)
3. Remove the conflict markers (<<<<, ====, >>>>)
4. Save the file
5. Stage and commit:
   $ git add Calculator.java
   $ git commit -m 'Resolve merge conflict in Calculator.java'