---
type: "EXAMPLE"
title: "Import Attributes Syntax"
---

Use the `with` keyword to declare what type of content you're importing:

```javascript
// Import JSON with type assertion
import config from './config.json' with { type: 'json' };
import packageJson from '../package.json' with { type: 'json' };

console.log(`Running ${packageJson.name} v${packageJson.version}`);
console.log(`Database: ${config.database.host}`);

// Import CSS (browser/bundler contexts)
import styles from './styles.css' with { type: 'css' };

// Dynamic import with attributes
const data = await import('./data.json', { with: { type: 'json' } });
```
