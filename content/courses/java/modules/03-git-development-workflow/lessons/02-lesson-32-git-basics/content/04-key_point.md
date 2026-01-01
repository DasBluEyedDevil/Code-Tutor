---
type: "KEY_POINT"
title: "git commit - Saving Snapshots"
---

The 'git commit' command creates a permanent snapshot of your staged changes.

BASIC SYNTAX:
$ git commit -m 'Your commit message here'

EXAMPLE:
$ git add HelloWorld.java
$ git commit -m 'Add greeting method to HelloWorld class'
  [main abc1234] Add greeting method to HelloWorld class
   1 file changed, 5 insertions(+)


WRITING GOOD COMMIT MESSAGES:

A good commit message is like a good news headline:
- Summarizes WHAT changed and WHY
- Uses imperative mood ('Add feature' not 'Added feature')
- Is specific and meaningful

GOOD MESSAGES:
✓ 'Fix null pointer exception in user login'
✓ 'Add email validation to registration form'
✓ 'Refactor database connection to use connection pool'
✓ 'Update README with installation instructions'

BAD MESSAGES:
✗ 'fix' (too vague)
✗ 'Updated stuff' (what stuff?)
✗ 'WIP' (work in progress - not ready to commit)
✗ 'asdfasdf' (meaningless)


CONVENTIONAL COMMITS (Industry Standard):
Many teams use prefixes:
- feat: Add user authentication
- fix: Resolve memory leak in image processing
- docs: Update API documentation
- refactor: Simplify validation logic
- test: Add unit tests for UserService