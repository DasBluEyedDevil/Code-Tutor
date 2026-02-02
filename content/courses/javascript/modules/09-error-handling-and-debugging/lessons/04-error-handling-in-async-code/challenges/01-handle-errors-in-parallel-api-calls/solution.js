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
  // Fetch all users in parallel
  const results = await Promise.allSettled(
    userIds.map(id => mockFetchUser(id))
  );
  
  const users = [];
  const errors = [];
  
  // Process each result
  results.forEach((result, index) => {
    if (result.status === 'fulfilled') {
      users.push(result.value);
    } else {
      errors.push({
        id: userIds[index],
        message: result.reason.message
      });
    }
  });
  
  return { users, errors };
}

// Test it
async function main() {
  const result = await fetchMultipleUsers([1, 2, 3, 4, 5]);
  console.log('Users:', result.users);
  // Users: [{ id: 2, name: 'User 2' }, { id: 4, name: 'User 4' }]
  console.log('Errors:', result.errors);
  // Errors: [{ id: 1, message: 'User 1 not found' }, { id: 3, message: 'User 3 not found' }, { id: 5, message: 'User 5 not found' }]
}

main();