---
type: "WARNING"
title: "Common Beginner Mistakes to Avoid"
---

MISTAKES YOU'LL WANT TO AVOID:

1. NOT COMMITTING OFTEN ENOUGH:
   Bad: One giant commit with 500 lines of changes
   Good: Multiple small commits, each with one logical change
   
2. VAGUE COMMIT MESSAGES:
   Bad: 'fixed stuff', 'updates', 'asdfgh'
   Good: 'Fix null pointer exception in login validation'
   
3. COMMITTING SENSITIVE DATA:
   Never commit: passwords, API keys, personal data
   Use .gitignore to exclude sensitive files
   
4. IGNORING .gitignore:
   Don't commit: compiled files (.class), IDE settings, node_modules
   Always set up .gitignore for your project type
   
5. FEAR OF COMMITTING:
   Git is forgiving - you can almost always undo mistakes
   Commit early, commit often, push regularly

Remember: The goal of version control is to give you confidence to experiment. If something breaks, you can always go back!