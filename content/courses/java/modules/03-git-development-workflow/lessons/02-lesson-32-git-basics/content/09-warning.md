---
type: "WARNING"
title: "Critical Git Safety Rules"
---

THINGS YOU SHOULD NEVER COMMIT:

1. PASSWORDS AND SECRETS:
   - Database passwords
   - API keys and tokens
   - Private encryption keys
   - .env files with credentials
   Once committed, secrets can be found even after deletion!

2. PERSONAL DATA:
   - Customer information
   - Email addresses
   - Payment information
   If in doubt, don't commit it.

3. LARGE BINARY FILES:
   - Videos, large images
   - Database dumps
   - Compiled executables
   Git is optimized for text files, not binaries.

4. IDE AND OS FILES:
   - .idea/, .vscode/
   - .DS_Store, Thumbs.db
   These are personal to your setup.


IF YOU ACCIDENTALLY COMMIT SECRETS:
1. Change the password/key IMMEDIATELY
2. The secret is already exposed - treat it as compromised
3. Use 'git filter-branch' or BFG Repo Cleaner to remove from history
4. Force push the cleaned history

PREVENTION:
- Set up .gitignore BEFORE your first commit
- Use environment variables for secrets
- Review 'git diff --staged' before every commit