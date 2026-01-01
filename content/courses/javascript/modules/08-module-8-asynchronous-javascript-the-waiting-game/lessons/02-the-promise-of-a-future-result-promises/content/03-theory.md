---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Promise syntax:

**Creating a Promise:**

let promise = new Promise((resolve, reject) => {
                           │        │
                           │        └─ Call when operation fails
                           └────────── Call when operation succeeds
  // Do async work
  if (success) {
    resolve(successValue);
  } else {
    reject(errorValue);
  }
});

**Using a Promise:**

promise
  .then((result) => {
    // Runs if promise resolves (success)
    console.log(result);
  })
  .catch((error) => {
    // Runs if promise rejects (failure)
    console.log(error);
  })
  .finally(() => {
    // Always runs (success or failure)
    console.log('Done');
  });

**Promise States:**

1. Pending - Initial state, not yet resolved or rejected
2. Fulfilled - Operation completed successfully (resolve called)
3. Rejected - Operation failed (reject called)

Once settled (fulfilled or rejected), state can't change!

**Promise Chaining:**

fetch('/api/user')
  .then(response => response.json())  // Parse JSON
  .then(user => {
    console.log('User:', user);
    return fetch('/api/posts/' + user.id);  // Next request
  })
  .then(response => response.json())
  .then(posts => {
    console.log('Posts:', posts);
  })
  .catch(error => {
    console.log('Error anywhere in chain:', error);
  });

**Promise Utilities:**

1. Promise.all([p1, p2, p3])
   - Waits for ALL to resolve
   - Rejects if ANY rejects
   - Returns array of results

2. Promise.race([p1, p2, p3])
   - Returns when FIRST settles (resolve or reject)

3. Promise.allSettled([p1, p2, p3])
   - Waits for ALL to settle
   - Never rejects
   - Returns array of {status, value/reason}

4. Promise.any([p1, p2, p3])
   - Returns when FIRST resolves
   - Rejects only if ALL reject

5. Promise.withResolvers() - ES2024!
   - Returns {promise, resolve, reject}
   - Useful when you need to resolve/reject from outside

**Error Handling:**

// Catch errors
promise.catch(error => console.log(error));

// Or use second argument to then
promise.then(
  result => console.log(result),
  error => console.log(error)
);