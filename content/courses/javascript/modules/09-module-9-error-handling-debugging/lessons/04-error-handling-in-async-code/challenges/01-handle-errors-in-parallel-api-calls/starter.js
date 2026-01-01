// Simulated fetch function - some users exist, some don't
function mockFetchUser(userId) {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      if (userId % 2 === 0) {
        resolve({ id: userId, name: `User ${userId}` });
      } else {
        reject(new Error(`User ${userId} not found`));
      }
    }, 100);
  });
}

async function fetchMultipleUsers(userIds) {
  // YOUR CODE HERE
  // 1. Use Promise.allSettled to fetch all users in parallel
  // 2. Separate successful results from failures
  // 3. Return { users: [...], errors: [...] }
}

// Test it
async function main() {
  const result = await fetchMultipleUsers([1, 2, 3, 4, 5]);
  console.log('Users:', result.users);
  console.log('Errors:', result.errors);
}

main();