// 1. Full User interface with all fields
interface User {
  id: number;
  name: string;
  email: string;
  passwordHash: string;
  role: 'admin' | 'user' | 'guest';
  createdAt: Date;
  lastLogin: Date | null;
  preferences: {
    theme: 'light' | 'dark';
    emailNotifications: boolean;
  };
}

// 2. PublicUser - only safe fields for public API
// Use Pick to select: id, name, role
type PublicUser = /* Your code here */;

// 3. UpdateUserInput - fields that can be updated
// Use Partial + Omit: can't update id, passwordHash, createdAt
type UpdateUserInput = /* Your code here */;

// 4. AdminUserView - everything except passwordHash
// Use Omit to exclude passwordHash
type AdminUserView = /* Your code here */;

// 5. Transform function
function toPublicUser(user: User): PublicUser {
  // Return only the public fields
}

// 6. Test with sample data
const fullUser: User = {
  id: 1,
  name: 'Alice Johnson',
  email: 'alice@company.com',
  passwordHash: 'hashed_password_12345',
  role: 'admin',
  createdAt: new Date('2024-01-15'),
  lastLogin: new Date('2024-06-20'),
  preferences: {
    theme: 'dark',
    emailNotifications: true
  }
};

const publicProfile = toPublicUser(fullUser);
console.log('Public profile:', publicProfile);

// Test update input type
const updateData: UpdateUserInput = {
  name: 'Alice Smith',
  preferences: { theme: 'light', emailNotifications: false }
};
console.log('Update data:', updateData);