---
type: "EXAMPLE"
title: "Error Propagation Through Async Chains"
---

Understanding how errors flow through async/await chains helps you handle them at the right level.

```javascript
// Errors propagate up the async chain automatically
async function level3() {
  throw new Error('Error in level 3');
}

async function level2() {
  // If we don't catch, error propagates up
  return await level3();
}

async function level1() {
  // If we don't catch, error propagates up
  return await level2();
}

async function main() {
  try {
    await level1();
  } catch (error) {
    // Catches error from level3, even though it bubbled through level2 and level1
    console.log('Caught in main:', error.message);
  }
}

main();

// Adding context as errors propagate
async function fetchData() {
  throw new Error('Network timeout');
}

async function processUserData(userId) {
  try {
    const data = await fetchData();
    return transform(data);
  } catch (error) {
    // Add context and re-throw
    throw new Error(`Failed to process user ${userId}`, { cause: error });
  }
}

async function generateReport(userIds) {
  try {
    const results = [];
    for (const userId of userIds) {
      results.push(await processUserData(userId));
    }
    return results;
  } catch (error) {
    // Add more context and re-throw
    throw new Error('Report generation failed', { cause: error });
  }
}

async function main() {
  try {
    await generateReport([1, 2, 3]);
  } catch (error) {
    // Full error chain preserved
    console.log('Main error:', error.message);
    console.log('Caused by:', error.cause?.message);
    console.log('Root cause:', error.cause?.cause?.message);
  }
}

main();
// Output:
// Main error: Report generation failed
// Caused by: Failed to process user 1
// Root cause: Network timeout
```
