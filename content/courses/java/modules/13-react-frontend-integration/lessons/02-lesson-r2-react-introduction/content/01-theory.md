---
type: "THEORY"
title: "Setting Up React with Vite"
---

Vite (French for 'fast') is the modern way to create React projects:

# Create new project
npm create vite@latest my-app -- --template react

# Navigate into project
cd my-app

# Install dependencies
npm install

# Start development server
npm run dev

Your app runs at http://localhost:5173

PROJECT STRUCTURE:
my-app/
├── index.html          # Entry HTML file
├── package.json        # Dependencies and scripts
├── vite.config.js      # Vite configuration
├── src/
│   ├── main.jsx        # React entry point
│   ├── App.jsx         # Root component
│   ├── App.css         # Styles for App
│   └── index.css       # Global styles
└── public/             # Static assets

WHY VITE?
- Instant server start (no bundling during dev)
- Lightning-fast hot module replacement (HMR)
- Optimized production builds
- Modern ES modules support
- Used by Vue, Svelte, and now React community