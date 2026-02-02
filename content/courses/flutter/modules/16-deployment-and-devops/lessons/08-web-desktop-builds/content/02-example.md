---
type: "EXAMPLE"
title: "Web Builds"
---


Build and deploy Flutter web applications to various hosting platforms:



```bash
# Build for web with different renderers
# HTML renderer - smaller download, better text rendering
flutter build web --web-renderer html

# CanvasKit renderer - better performance, consistent rendering
flutter build web --web-renderer canvaskit

# Auto (default) - HTML on mobile, CanvasKit on desktop
flutter build web --web-renderer auto

# Production build with optimizations
flutter build web --release --tree-shake-icons

# Build output: build/web/

---

# Deploy to Firebase Hosting
# Install Firebase CLI: npm install -g firebase-tools

# Initialize Firebase in your project
firebase init hosting
# Select: build/web as public directory
# Configure as single-page app: Yes
# Set up automatic builds: Optional

# Deploy to Firebase
firebase deploy --only hosting

# firebase.json configuration
{
  "hosting": {
    "public": "build/web",
    "ignore": ["firebase.json", "**/.*", "**/node_modules/**"],
    "rewrites": [
      {
        "source": "**",
        "destination": "/index.html"
      }
    ],
    "headers": [
      {
        "source": "**/*.@(js|css)",
        "headers": [
          {
            "key": "Cache-Control",
            "value": "max-age=31536000"
          }
        ]
      }
    ]
  }
}

---

# Deploy to Vercel
# Install Vercel CLI: npm install -g vercel

# Create vercel.json in project root
{
  "buildCommand": "flutter build web --release",
  "outputDirectory": "build/web",
  "framework": null,
  "rewrites": [
    { "source": "/(.*)", "destination": "/index.html" }
  ]
}

# Deploy
vercel --prod

---

# Deploy to Netlify
# Create netlify.toml in project root
[build]
  command = "flutter build web --release"
  publish = "build/web"

[[redirects]]
  from = "/*"
  to = "/index.html"
  status = 200

# Deploy via Netlify CLI or GitHub integration
netlify deploy --prod

---

# GitHub Actions for Web Deployment
name: Deploy Web

on:
  push:
    branches: [main]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      
      - uses: subosito/flutter-action@v2
        with:
          flutter-version: '3.24.0'
          cache: true

      - run: flutter pub get
      - run: flutter build web --release --web-renderer canvaskit

      - name: Deploy to GitHub Pages
        uses: peaceiris/actions-gh-pages@v4
        with:
          github_token: ${{ secrets.GITHUB_TOKEN }}
          publish_dir: ./build/web
```
