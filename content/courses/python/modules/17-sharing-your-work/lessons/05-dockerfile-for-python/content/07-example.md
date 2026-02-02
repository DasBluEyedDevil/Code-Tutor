---
type: "EXAMPLE"
title: "The .dockerignore File"
---

**Just like .gitignore, but for Docker builds**

Prevents copying unnecessary files into your image, making builds faster and images smaller.

```text
# .dockerignore

# Git
.git
.gitignore

# Python bytecode
__pycache__
*.pyc
*.pyo
*.pyd
.Python

# Virtual environments (we build fresh in Docker)
.venv
venv
env

# Environment files with secrets
.env
.env.*
!.env.example

# IDE settings
.vscode
.idea
*.swp
*.swo

# Test and coverage
.pytest_cache
.coverage
htmlcov
.tox

# Documentation
docs
*.md
!README.md

# Docker files (prevent recursive issues)
Dockerfile*
docker-compose*.yml

# CI/CD
.github
.gitlab-ci.yml

# OS files
.DS_Store
Thumbs.db
```
