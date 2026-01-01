---
type: "THEORY"
title: "Setting Up Git"
---

Before using Git, you need to install it and configure your identity.

1. INSTALLING GIT:

Windows:
- Download from git-scm.com
- Run the installer (accept defaults)
- Use 'Git Bash' terminal or Windows Terminal

macOS:
- Open Terminal and type: git --version
- If not installed, it will prompt you to install Xcode Command Line Tools
- Or use Homebrew: brew install git

Linux (Ubuntu/Debian):
- sudo apt update && sudo apt install git

2. CONFIGURING YOUR IDENTITY:

Git needs to know who you are (for commit authorship):

$ git config --global user.name 'Your Name'
$ git config --global user.email 'your.email@example.com'

The --global flag sets this for all repositories on your computer.

3. VERIFY INSTALLATION:

$ git --version
  git version 2.43.0  (or similar)

$ git config --list
  user.name=Your Name
  user.email=your.email@example.com

You only need to do this setup once per computer!