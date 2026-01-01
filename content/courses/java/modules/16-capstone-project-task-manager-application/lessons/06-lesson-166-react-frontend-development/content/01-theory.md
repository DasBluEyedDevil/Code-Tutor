---
type: "THEORY"
title: "Vite + React Project Setup"
---

We will use Vite as our build tool for the React frontend. Vite offers lightning-fast hot module replacement (HMR) and optimized builds, making it the modern choice for React development.

Project Creation:
Open a terminal and run:
```bash
npm create vite@latest frontend -- --template react
cd frontend
npm install
```

Install Additional Dependencies:
```bash
# Routing
npm install react-router-dom

# HTTP client
npm install axios

# Optional: Tailwind CSS for styling
npm install -D tailwindcss postcss autoprefixer
npx tailwindcss init -p
```

Tailwind Configuration (tailwind.config.js):
```javascript
/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
```

Add Tailwind directives to src/index.css:
```css
@tailwind base;
@tailwind components;
@tailwind utilities;
```

Environment Variables (.env):
```
VITE_API_URL=http://localhost:8080/api
```

Access in code:
```javascript
const API_URL = import.meta.env.VITE_API_URL;
```

Start Development Server:
```bash
npm run dev
```

The app will be available at http://localhost:5173. Vite's HMR means changes appear instantly without full page reloads.