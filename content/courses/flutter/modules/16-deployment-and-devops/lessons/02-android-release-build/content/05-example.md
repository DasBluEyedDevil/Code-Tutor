---
type: "EXAMPLE"
title: "Update .gitignore"
---


Ensure sensitive files are never committed:

**android/.gitignore (add these lines):**



```gitignore
# Existing entries...

# Release signing
key.properties
*.jks
*.keystore

# Local configuration
local.properties
```
