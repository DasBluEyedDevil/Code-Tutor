---
type: "EXAMPLE"
title: "Installing and Configuring flyctl"
---

**flyctl is the Fly.io command-line interface:**

```bash
# Step 1: Install flyctl

# On macOS:
brew install flyctl
# Or:
curl -L https://fly.io/install.sh | sh

# On Linux:
curl -L https://fly.io/install.sh | sh

# On Windows (PowerShell):
iwr https://fly.io/install.ps1 -useb | iex

# Step 2: Sign up / Login
fly auth signup    # Create new account
# or
fly auth login     # Login to existing account

# Step 3: Launch your app (from project directory)
cd finance-tracker
fly launch

# fly launch will:
# 1. Detect your app type (Python/Docker)
# 2. Generate fly.toml configuration
# 3. Ask about region, resources, databases
# 4. Optionally deploy immediately

# Step 4: Create PostgreSQL database
fly postgres create --name finance-db
# Choose region and size when prompted

# Step 5: Attach database to your app
fly postgres attach finance-db
# Adds DATABASE_URL to your app secrets

# Step 6: Deploy
fly deploy

# Step 7: Open in browser
fly open
```
