---
type: "WARNING"
title: "Anti-Patterns"
---

**DON'T: Lazy-load at top level**
```javascript
// BAD: This delays every import of your module
const rarelyUsedLib = await import('heavy-library');
```

**DO: Lazy-load inside functions**
```javascript
// GOOD: Only load when needed
async function useRareFeature() {
  const lib = await import('heavy-library');
  return lib.doThing();
}
```

**DON'T: Top-level await in loops**
```javascript
// BAD: Sequential loading, very slow
for (const url of urls) {
  const data = await fetch(url);  // One at a time!
}
```

**DO: Parallelize**
```javascript
// GOOD: All at once
const results = await Promise.all(urls.map(fetch));
```