---
type: "EXAMPLE"
title: "Always Handle Rejections"
---

Unhandled promise rejections can crash Node.js or cause subtle bugs in browsers. Always catch them.

```javascript
// BAD: Unhandled rejection
async function riskyFunction() {
  throw new Error('Something went wrong');
}

riskyFunction(); // No await, no catch - unhandled rejection!

// GOOD: Always handle rejections
async function riskyFunction() {
  throw new Error('Something went wrong');
}

// Option 1: await in try-catch
async function main() {
  try {
    await riskyFunction();
  } catch (error) {
    console.error('Caught:', error.message);
  }
}
main();

// Option 2: .catch() on the promise
riskyFunction().catch(error => {
  console.error('Caught:', error.message);
});

// Real scenario: Fire-and-forget with error handling
function logUserAction(userId, action) {
  // This is async but we don't need to wait for it
  // STILL need to handle potential errors!
  saveToAnalytics(userId, action)
    .catch(error => {
      console.error('Failed to log action:', error.message);
      // Maybe queue for retry later
    });
}

// Called without await - that's OK since we have .catch()
logUserAction(123, 'clicked_button');

// Multiple independent async operations
async function processMultiple(items) {
  // Don't do this - unhandled if any reject!
  items.forEach(item => processItem(item));
  
  // Do this instead:
  await Promise.all(
    items.map(item => 
      processItem(item).catch(error => {
        console.error(`Failed to process ${item.id}:`, error.message);
        return null; // Return null for failed items
      })
    )
  );
}
```
