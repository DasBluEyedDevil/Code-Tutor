---
type: "WARNING"
title: "Common Mistakes"
---

**1. Forgetting await before async calls:**
```javascript
// BAD: No await - error escapes try-catch!
try {
  fetchData(); // Returns promise, doesn't wait
} catch (error) {
  console.log('Never runs!'); // Promise rejection is unhandled
}

// GOOD: Always await
try {
  await fetchData();
} catch (error) {
  console.log('Now it catches!');
}
```

**2. Not catching in async functions:**
```javascript
// BAD: Async function without error handling
async function loadData() {
  const data = await fetch('/api/data'); // Can throw!
  return data.json();
}

// If fetch fails, the error propagates as an unhandled rejection
loadData(); // Unhandled if called without catch!

// GOOD: Handle internally OR make caller responsible
async function loadData() {
  try {
    const data = await fetch('/api/data');
    return data.json();
  } catch (error) {
    console.error('Failed:', error);
    return null;
  }
}
```

**3. Mixing async patterns incorrectly:**
```javascript
// BAD: try-catch doesn't catch .then() rejections
try {
  fetch('/api').then(r => r.json()).then(data => {
    throw new Error('Oops'); // Unhandled!
  });
} catch (e) {
  console.log('Never catches the throw above');
}

// GOOD: Use either async/await OR promise chains, not both
async function correct() {
  try {
    const r = await fetch('/api');
    const data = await r.json();
    // Now throwing here IS caught
    throw new Error('Oops');
  } catch (e) {
    console.log('Caught:', e.message);
  }
}
```

**4. Using Promise.all when allSettled is needed:**
```javascript
// BAD: One failure loses all results
const results = await Promise.all(urls.map(url => fetch(url)));
// If ANY fails, you get NOTHING

// GOOD: Use allSettled when you want partial results
const results = await Promise.allSettled(urls.map(url => fetch(url)));
const successful = results.filter(r => r.status === 'fulfilled');
```