---
type: "EXAMPLE"
title: "Setup Web Package"
---

Configure the frontend React package:

```json
// packages/web/package.json
{
  "name": "@app/web",
  "version": "1.0.0",
  "type": "module",
  "scripts": {
    "dev": "bunx vite",
    "build": "bunx vite build",
    "preview": "bunx vite preview",
    "typecheck": "tsc --noEmit"
  },
  "dependencies": {
    "@app/shared": "*",
    "react": "^18.2.0",
    "react-dom": "^18.2.0",
    "@tanstack/react-query": "^5.0.0"
  },
  "devDependencies": {
    "@vitejs/plugin-react": "^4.0.0",
    "vite": "^5.0.0",
    "typescript": "^5.0.0",
    "@types/react": "^18.0.0",
    "@types/react-dom": "^18.0.0"
  }
}
```
