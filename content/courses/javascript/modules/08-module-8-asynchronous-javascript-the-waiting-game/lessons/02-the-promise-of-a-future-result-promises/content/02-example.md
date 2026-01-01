---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Creating a Promise
let myPromise = new Promise((resolve, reject) => {
  // Simulating async operation
  let success = true;
  
  setTimeout(() => {
    if (success) {
      resolve('Operation successful!');  // Fulfilled
    } else {
      reject('Operation failed!');  // Rejected
    }
  }, 1000);
});

// Using a Promise
myPromise
  .then((result) => {
    console.log('Success:', result);
  })
  .catch((error) => {
    console.log('Error:', error);
  });

// Practical example: Fetching data
function fetchUserData(userId) {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      if (userId > 0) {
        resolve({ id: userId, name: 'Alice' });
      } else {
        reject('Invalid user ID');
      }
    }, 1000);
  });
}

fetchUserData(1)
  .then((user) => {
    console.log('User:', user.name);
  })
  .catch((error) => {
    console.log('Error:', error);
  });

// Chaining Promises
fetchUserData(1)
  .then((user) => {
    console.log('Got user:', user.name);
    return fetchUserPosts(user.id);  // Returns another promise
  })
  .then((posts) => {
    console.log('Got posts:', posts);
  })
  .catch((error) => {
    console.log('Error:', error);
  });

// Promise.all - Wait for multiple promises
let promise1 = fetchUserData(1);
let promise2 = fetchUserData(2);
let promise3 = fetchUserData(3);

Promise.all([promise1, promise2, promise3])
  .then((results) => {
    console.log('All users:', results);
  })
  .catch((error) => {
    console.log('At least one failed:', error);
  });

// Promise.race - First one to finish
Promise.race([promise1, promise2, promise3])
  .then((result) => {
    console.log('First result:', result);
  });

// Promise.withResolvers() - ES2024 new feature!
let { promise, resolve, reject } = Promise.withResolvers();

// Can resolve/reject from anywhere
setTimeout(() => resolve('Done!'), 1000);

promise.then(result => console.log(result));
```
