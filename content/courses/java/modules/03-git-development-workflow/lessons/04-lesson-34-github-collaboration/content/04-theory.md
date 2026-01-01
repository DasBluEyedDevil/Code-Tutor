---
type: "THEORY"
title: "Push and Pull: Syncing with Remote"
---

GIT PUSH - Upload Local Changes

Sends your commits from local to remote:
$ git push origin main

Breakdown:
- 'push': Upload commits
- 'origin': The remote name
- 'main': The branch to push

If you set up tracking (-u flag), you can just use:
$ git push


GIT PULL - Download Remote Changes

Gets commits from remote to your local repo:
$ git pull origin main

Or simply:
$ git pull

WHAT PULL DOES:
1. Fetches changes from remote (git fetch)
2. Merges them into your current branch (git merge)


GIT FETCH - Download Without Merging

Sometimes you want to see changes before merging:
$ git fetch origin
$ git log origin/main  # See what's new
$ git merge origin/main  # Merge when ready


TYPICAL WORKFLOW:
1. Start your day: git pull (get latest changes)
2. Do your work: edit, add, commit
3. End your day: git push (share your changes)
4. Repeat!