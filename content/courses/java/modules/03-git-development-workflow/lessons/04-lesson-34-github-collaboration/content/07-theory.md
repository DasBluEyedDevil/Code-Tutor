---
type: "THEORY"
title: "Writing Good Pull Requests"
---

A good PR description helps reviewers understand your changes quickly.

PR TITLE:
- Be concise but descriptive
- Use the same prefixes as commits: feat:, fix:, docs:, etc.

Examples:
✓ 'feat: Add user authentication with JWT'
✓ 'fix: Resolve null pointer in checkout flow'
✗ 'Updates' (too vague)
✗ 'Fixed that thing we talked about' (not descriptive)

PR DESCRIPTION TEMPLATE:

## Summary
Brief description of what this PR does and why.

## Changes
- Added UserAuthentication class
- Integrated JWT token validation
- Updated login endpoint to return tokens

## Testing
- [ ] Unit tests pass
- [ ] Manual testing completed
- [ ] Tested edge cases: expired token, invalid token

## Screenshots (if applicable)
[Add screenshots for UI changes]

## Related Issues
Closes #123


PR ETIQUETTE:
1. Keep PRs small and focused (easier to review)
2. Respond promptly to review comments
3. Be open to feedback - it makes code better
4. Thank reviewers for their time