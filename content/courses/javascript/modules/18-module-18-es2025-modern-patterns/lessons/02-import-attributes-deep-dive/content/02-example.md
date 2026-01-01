---
type: "EXAMPLE"
title: "All Import Attribute Syntaxes"
---

Master every way to use import attributes:

```javascript
// Static imports - most common
import config from './config.json' with { type: 'json' };
import styles from './app.css' with { type: 'css' };

// Dynamic imports - note the different structure
const data = await import('./data.json', {
  with: { type: 'json' }
});

// Re-exports with attributes
export { default as config } from './config.json' with { type: 'json' };

// Namespace imports
import * as pkg from './package.json' with { type: 'json' };
console.log(pkg.default.name);

// Multiple attributes (future-proofing)
import schema from './schema.json' with {
  type: 'json',
  integrity: 'sha384-abc...'
};
```
