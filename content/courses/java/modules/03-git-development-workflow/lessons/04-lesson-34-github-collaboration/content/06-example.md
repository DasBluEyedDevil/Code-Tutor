---
type: "EXAMPLE"
title: "Complete GitHub Collaboration Workflow"
---

Here's a realistic example of working on a team project:

```bash
# === MORNING: Start fresh ===
$ git switch main
$ git pull origin main    # Get latest changes from team

# === CREATE FEATURE BRANCH ===
$ git switch -c feature/user-profile-page

# === DO YOUR WORK ===
# ... write code, make commits ...
$ git add UserProfile.java
$ git commit -m 'feat: Add user profile page layout'
$ git add ProfileService.java
$ git commit -m 'feat: Add service to fetch user data'

# === PUSH YOUR BRANCH ===
$ git push -u origin feature/user-profile-page

# === CREATE PULL REQUEST ON GITHUB ===
# Go to github.com/yourteam/project
# Click 'Compare & pull request'
# Fill in:
#   Title: Add user profile page
#   Description: 
#     - Added UserProfile component
#     - Integrated with ProfileService
#     - Includes responsive layout
# Request review from teammate
# Submit!

# === RESPOND TO REVIEW FEEDBACK ===
# Reviewer comments: 'Add null check for empty profile'
$ git switch feature/user-profile-page
# ... make the requested changes ...
$ git add UserProfile.java
$ git commit -m 'fix: Add null check for empty profile'
$ git push
# The PR automatically updates!

# === AFTER APPROVAL: MERGE ON GITHUB ===
# Click 'Merge pull request'
# Click 'Delete branch' (on GitHub)

# === CLEAN UP LOCALLY ===
$ git switch main
$ git pull origin main    # Get the merged changes
$ git branch -d feature/user-profile-page  # Delete local branch
```
