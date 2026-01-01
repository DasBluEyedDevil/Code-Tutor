---
type: "CODE"
title: ".dockerignore - Optimize Build Context"
---

Create a .dockerignore file to exclude unnecessary files from Docker builds:

```text
# .dockerignore
node_modules/
.git/
.gitignore
.env
.env.*
!.env.production
*.db
*.db-journal
.DS_Store
dist/
build/
.turbo/
.next/
coverage/
.vscode/
.idea/
*.log
npm-debug.log*
yarn-debug.log*
tmp/
temp/
README.md
.github/
```
