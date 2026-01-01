---
type: "EXAMPLE"
title: "Promise.all() - Fails Fast on First Error"
---

Promise.all() runs promises in parallel but fails immediately when any promise rejects.

```javascript
// Promise.all - fails fast on first error
async function fetchAllUsers(userIds) {
  try {
    // Create an array of promises
    const promises = userIds.map(id => fetchUser(id));
    
    // Wait for ALL to complete
    const users = await Promise.all(promises);
    console.log('All users loaded:', users.length);
    return users;
    
  } catch (error) {
    // If ANY promise rejects, we end up here immediately
    // Other promises continue running but results are discarded
    console.error('Failed to load users:', error.message);
    return [];
  }
}

// Demonstrating fail-fast behavior
async function demo() {
  const slowSuccess = new Promise(resolve => {
    setTimeout(() => {
      console.log('Slow success completed');
      resolve('slow');
    }, 2000);
  });
  
  const fastFailure = new Promise((resolve, reject) => {
    setTimeout(() => {
      console.log('Fast failure happening');
      reject(new Error('Fast failure'));
    }, 500);
  });
  
  try {
    // This will fail after 500ms even though slowSuccess needs 2000ms
    const results = await Promise.all([slowSuccess, fastFailure]);
    console.log('Results:', results); // Never reached
  } catch (error) {
    console.log('Caught after ~500ms:', error.message);
    // Output: Caught after ~500ms: Fast failure
    // Note: 'Slow success completed' will still log after 2 seconds!
  }
}

demo();

// Real-world example: Loading dashboard data
async function loadDashboard(userId) {
  try {
    const [user, stats, notifications] = await Promise.all([
      fetchUser(userId),
      fetchUserStats(userId),
      fetchNotifications(userId)
    ]);
    
    return { user, stats, notifications };
  } catch (error) {
    console.error('Dashboard load failed:', error.message);
    // If any request fails, entire dashboard fails
    throw error;
  }
}
```
