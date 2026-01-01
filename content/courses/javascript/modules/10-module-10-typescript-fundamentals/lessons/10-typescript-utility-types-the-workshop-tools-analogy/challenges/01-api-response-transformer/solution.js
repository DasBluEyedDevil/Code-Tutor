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
type PublicUser = Pick<User, 'id' | 'name' | 'role'>;

// 3. UpdateUserInput - fields that can be updated
// Use Partial + Omit: can't update id, passwordHash, createdAt
type UpdateUserInput = Partial<Omit<User, 'id' | 'passwordHash' | 'createdAt'>>;

// 4. AdminUserView - everything except passwordHash
// Use Omit to exclude passwordHash
type AdminUserView = Omit<User, 'passwordHash'>;

// 5. Transform function
function toPublicUser(user: User): PublicUser {
  return {
    id: user.id,
    name: user.name,
    role: user.role
  };
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
// Public profile: { id: 1, name: 'Alice Johnson', role: 'admin' }

// Test update input type
const updateData: UpdateUserInput = {
  name: 'Alice Smith',
  preferences: { theme: 'light', emailNotifications: false }
};
console.log('Update data:', updateData);
// Update data: { name: 'Alice Smith', preferences: { theme: 'light', emailNotifications: false } }