---
type: "KEY_POINT"
title: "git diff - Comparing Changes"
---

The 'git diff' command shows exactly what changed in your files.

COMMON USES:

1. See unstaged changes (working directory vs staging area):
   $ git diff

2. See staged changes (staging area vs last commit):
   $ git diff --staged
   or
   $ git diff --cached

3. See all changes since last commit:
   $ git diff HEAD

4. Compare two commits:
   $ git diff abc1234 def5678

5. Compare with previous commit:
   $ git diff HEAD~1
   (HEAD~1 means 'one commit before HEAD')

6. Diff a specific file:
   $ git diff Calculator.java


READING DIFF OUTPUT:

- Lines starting with '+' are ADDITIONS (green)
- Lines starting with '-' are DELETIONS (red)
- Lines starting with '@@' show line numbers
- Context lines (unchanged) have no prefix

Example output:
@@ -1,5 +1,8 @@
 public class Calculator {
     public int add(int a, int b) {
         return a + b;
     }
+
+    public int subtract(int a, int b) {
+        return a - b;
+    }
 }