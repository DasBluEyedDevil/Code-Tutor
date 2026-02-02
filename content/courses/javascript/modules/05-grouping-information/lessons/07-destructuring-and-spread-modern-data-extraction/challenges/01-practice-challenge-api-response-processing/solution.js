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
let {
  data: {
    user: {
      credentials: { firstName, lastName, email }
    }
  }
} = apiResponse;
console.log(`User: ${firstName} ${lastName} (${email})`);
// User: Alice Johnson (alice@example.com)

// 2. Extract first two skills
let {
  data: {
    user: {
      skills: [skill1, skill2]
    }
  }
} = apiResponse;
console.log(`Primary skills: ${skill1}, ${skill2}`);
// Primary skills: JavaScript, React

// 3. Create publicProfile WITHOUT password and ssn
let { password, ssn, ...safeCredentials } = apiResponse.data.user.credentials;
let publicProfile = {
  ...safeCredentials,
  skills: apiResponse.data.user.skills
};
console.log('Public profile:', publicProfile);
// { firstName: 'Alice', lastName: 'Johnson', email: 'alice@example.com', skills: [...] }

// 4. Merge preferences (user prefs override defaults)
let finalPrefs = {
  ...defaultPreferences,
  ...apiResponse.data.user.preferences
};
console.log('Final preferences:', finalPrefs);
// { theme: 'dark', notifications: true, language: 'en', timezone: 'UTC' }