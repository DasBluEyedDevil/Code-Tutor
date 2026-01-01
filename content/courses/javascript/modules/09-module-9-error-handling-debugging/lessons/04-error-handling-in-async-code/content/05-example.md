---
type: "EXAMPLE"
title: "Promise.allSettled() - Waits for All, Reports Each Status"
---

Promise.allSettled() waits for all promises and tells you which succeeded and which failed.

```javascript
// Promise.allSettled - wait for ALL, never rejects
async function fetchAllUsersSettled(userIds) {
  const promises = userIds.map(id => fetchUser(id));
  
  // allSettled ALWAYS resolves, never rejects
  const results = await Promise.allSettled(promises);
  
  // Each result has { status: 'fulfilled', value } or { status: 'rejected', reason }
  
  const successful = results
    .filter(r => r.status === 'fulfilled')
    .map(r => r.value);
    
  const failed = results
    .filter(r => r.status === 'rejected')
    .map(r => r.reason);
  
  console.log(`Loaded ${successful.length} users, ${failed.length} failed`);
  
  return { successful, failed };
}

// Example output:
// {
//   successful: [{ id: 1, name: 'Alice' }, { id: 3, name: 'Charlie' }],
//   failed: [Error: User 2 not found]
// }

// Real-world example: Sending notifications
async function sendNotifications(users, message) {
  const results = await Promise.allSettled(
    users.map(user => sendNotification(user.id, message))
  );
  
  const sent = [];
  const failed = [];
  
  results.forEach((result, index) => {
    if (result.status === 'fulfilled') {
      sent.push(users[index]);
    } else {
      failed.push({
        user: users[index],
        error: result.reason.message
      });
    }
  });
  
  console.log(`Notifications: ${sent.length} sent, ${failed.length} failed`);
  
  // Can retry failed ones
  if (failed.length > 0) {
    console.log('Failed users:', failed.map(f => f.user.name).join(', '));
  }
  
  return { sent, failed };
}

// Comparing Promise.all vs Promise.allSettled
async function comparison() {
  const promises = [
    Promise.resolve('A'),
    Promise.reject(new Error('B failed')),
    Promise.resolve('C')
  ];
  
  // Promise.all - fails fast, you only get the error
  try {
    const results = await Promise.all(promises);
  } catch (error) {
    console.log('Promise.all caught:', error.message);
    // 'A' and 'C' results are lost!
  }
  
  // Promise.allSettled - you get everything
  const results = await Promise.allSettled(promises);
  console.log('Promise.allSettled results:');
  results.forEach((r, i) => {
    if (r.status === 'fulfilled') {
      console.log(`  ${i}: success - ${r.value}`);
    } else {
      console.log(`  ${i}: failed - ${r.reason.message}`);
    }
  });
  // Output:
  // 0: success - A
  // 1: failed - B failed
  // 2: success - C
}
```
