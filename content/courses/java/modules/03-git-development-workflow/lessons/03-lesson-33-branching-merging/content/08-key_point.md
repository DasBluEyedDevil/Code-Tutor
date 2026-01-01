---
type: "KEY_POINT"
title: "Branch Naming Conventions"
---

Good branch names make collaboration easier. Here are common conventions:

FEATURE BRANCHES:
- feature/user-authentication
- feature/add-payment-processing
- feature/dark-mode
- feat/login (shorter version)

BUG FIX BRANCHES:
- bugfix/login-error
- fix/null-pointer-in-checkout
- hotfix/security-patch (urgent production fix)

OTHER COMMON PATTERNS:
- release/v1.2.0 (preparing a release)
- docs/update-readme
- refactor/simplify-validation
- test/add-unit-tests
- experiment/try-new-algorithm

RULES:
✓ Use lowercase
✓ Use hyphens or slashes to separate words
✓ Be descriptive but concise
✓ Include ticket/issue number if applicable: feature/JIRA-123-user-login

✗ Avoid spaces (use hyphens)
✗ Avoid special characters except / and -
✗ Don't use your name as a branch: john-branch (not descriptive)

TIP: If your team uses a project management tool (Jira, GitHub Issues), reference the ticket number in the branch name for traceability.