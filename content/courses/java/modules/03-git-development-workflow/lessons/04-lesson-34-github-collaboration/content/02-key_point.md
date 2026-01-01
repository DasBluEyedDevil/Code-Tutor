---
type: "KEY_POINT"
title: "Setting Up GitHub"
---

STEP 1: Create a GitHub Account
- Go to github.com and sign up
- Choose a professional username (you'll use it forever!)
- Set up two-factor authentication for security

STEP 2: Create an SSH Key (Recommended)
SSH keys let you push/pull without entering your password each time.

$ ssh-keygen -t ed25519 -C 'your.email@example.com'
  # Press Enter to accept default location
  # Enter a passphrase (optional but recommended)

$ cat ~/.ssh/id_ed25519.pub
  # Copy this output

In GitHub:
- Go to Settings > SSH and GPG keys
- Click 'New SSH key'
- Paste your public key and save

STEP 3: Test Connection
$ ssh -T git@github.com
  Hi username! You've successfully authenticated...

ALTERNATIVE: Use HTTPS with Personal Access Token
- GitHub > Settings > Developer settings > Personal access tokens
- Generate a token with 'repo' scope
- Use this token as your password when prompted