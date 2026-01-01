// API Response from server
let apiResponse = {
  status: 'success',
  data: {
    user: {
      id: 12345,
      credentials: {
        firstName: 'Alice',
        lastName: 'Johnson',
        email: 'alice@example.com',
        password: 'hashed_secret',
        ssn: '123-45-6789'
      },
      skills: ['JavaScript', 'React', 'Node.js', 'Python', 'SQL'],
      preferences: {
        theme: 'dark',
        notifications: true
      }
    }
  }
};

let defaultPreferences = {
  theme: 'light',
  notifications: false,
  language: 'en',
  timezone: 'UTC'
};

// 1. Extract firstName, lastName, email from nested structure
// YOUR CODE HERE
console.log(`User: ${firstName} ${lastName} (${email})`);

// 2. Extract first two skills
// YOUR CODE HERE
console.log(`Primary skills: ${skill1}, ${skill2}`);

// 3. Create publicProfile WITHOUT password and ssn
// Hint: Use rest pattern to exclude, then spread to create new object
// YOUR CODE HERE
console.log('Public profile:', publicProfile);

// 4. Merge preferences (user prefs override defaults)
// YOUR CODE HERE
console.log('Final preferences:', finalPrefs);