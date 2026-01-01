---
type: "EXAMPLE"
title: "Real-World Use Case: Processing Large Files"
---

Iterator helpers shine when processing data streams:

```javascript
// Imagine reading a large log file line by line
async function* readLogLines(file) {
  // In real code, this would use Bun.file().stream()
  for await (const line of file) {
    yield line;
  }
}

// Process without loading entire file into memory
const errors = readLogLines(logFile)
  .filter(line => line.includes('ERROR'))
  .map(line => {
    const [timestamp, ...rest] = line.split(' ');
    return { timestamp, message: rest.join(' ') };
  })
  .take(100);  // Only get first 100 errors

// Another example: Paginated API results
function* fetchAllPages(baseUrl) {
  let page = 1;
  let hasMore = true;
  while (hasMore) {
    const response = await fetch(`${baseUrl}?page=${page}`);
    const data = await response.json();
    yield* data.items;  // Yield each item individually
    hasMore = data.hasNextPage;
    page++;
  }
}

// Process all items across all pages lazily
const activeUsers = fetchAllPages('/api/users')
  .filter(user => user.status === 'active')
  .map(user => user.email)
  .take(50);

for await (const email of activeUsers) {
  console.log(email);
}
```
