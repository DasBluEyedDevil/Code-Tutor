---
type: "LEGACY_COMPARISON"
title: "Express CORS Equivalent"
---

Express requires installing a separate cors package. If you encounter older codebases, here's how Express CORS differs from Hono.

```javascript
// Express CORS (requires npm install cors)
import express from 'express';
import cors from 'cors';  // Separate package!

const app = express();

// Basic CORS
app.use(cors());

// With options
app.use(cors({
  origin: 'https://myapp.com',
  credentials: true,
  methods: ['GET', 'POST', 'PUT', 'DELETE']  // Note: 'methods' not 'allowMethods'
}));

// Multiple origins (callback style)
app.use(cors({
  origin: function(origin, callback) {
    if (allowedOrigins.includes(origin)) {
      callback(null, true);
    } else {
      callback(new Error('Not allowed by CORS'));
    }
  }
}));
```
