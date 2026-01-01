async function getRandomUser() {
  try {
    // YOUR CODE HERE
    // 1. fetch from URL
    // 2. Check response.ok
    // 3. Parse JSON
    // 4. Extract and return name
  } catch (error) {
    console.log('Error:', error);
    return null;
  }
}

// Test it
getRandomUser().then(name => console.log('Random user:', name));