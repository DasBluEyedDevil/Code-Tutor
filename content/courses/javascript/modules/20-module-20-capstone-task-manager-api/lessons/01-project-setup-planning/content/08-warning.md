---
type: "WARNING"
title: "Environment Variables"
---

Create a `.env` file for sensitive configuration. Never commit this to git!

```bash
# .env
DATABASE_URL="file:./dev.db"
JWT_SECRET="your-super-secret-key-change-in-production"
PORT=3000
```

**Security Checklist:**
1. Add `.env` to your `.gitignore` immediately
2. Use a strong, random JWT_SECRET in production (at least 32 characters)
3. Never log sensitive values like passwords or tokens
4. Use environment variables for all configuration that changes between environments

```bash
# .gitignore
node_modules/
.env
*.db
*.db-journal
```