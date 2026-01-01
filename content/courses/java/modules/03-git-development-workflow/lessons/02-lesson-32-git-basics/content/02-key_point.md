---
type: "KEY_POINT"
title: "Essential Git Commands: init and status"
---

GIT INIT - Create a New Repository

To start tracking a project with Git:

$ cd my-java-project      # Navigate to your project folder
$ git init                 # Initialize Git tracking
  Initialized empty Git repository in /path/to/my-java-project/.git/

This creates a hidden '.git' folder that stores all version history.

NOTE: Only run 'git init' once per project, at the project root!


GIT STATUS - Check Current State

This is the command you'll use most often:

$ git status

Possible outputs:

1. Clean state (nothing to commit):
   On branch main
   nothing to commit, working tree clean

2. Untracked files (new files Git doesn't know about):
   Untracked files:
     HelloWorld.java

3. Modified files (existing files you've changed):
   Changes not staged for commit:
     modified: HelloWorld.java

4. Staged files (ready to commit):
   Changes to be committed:
     new file: HelloWorld.java

Run 'git status' frequently - it tells you exactly what's happening!