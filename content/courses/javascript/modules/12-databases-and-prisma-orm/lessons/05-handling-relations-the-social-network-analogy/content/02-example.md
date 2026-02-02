---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Understanding Prisma Relations

// THREE TYPES OF RELATIONSHIPS:

// 1. ONE-TO-MANY (Most common)
// Example: One user has many posts
/*
model User {
  id    Int    @id @default(autoincrement())
  email String @unique
  name  String
  
  posts Post[]  // Array: User has many posts
}

model Post {
  id       Int    @id @default(autoincrement())
  title    String
  content  String?
  
  authorId Int    // Foreign key
  author   User   @relation(fields: [authorId], references: [id])
}
*/

console.log('=== ONE-TO-MANY Relationship ===');
console.log('One User → Many Posts');
console.log('Schema: User.posts (array), Post.author (single)');
console.log('');

// Example data structure:
let oneToManyExample = {
  user: {
    id: 1,
    email: 'alice@example.com',
    name: 'Alice',
    posts: [  // Array of posts
      { id: 1, title: 'First Post', authorId: 1 },
      { id: 2, title: 'Second Post', authorId: 1 },
      { id: 3, title: 'Third Post', authorId: 1 }
    ]
  }
};

console.log('Example data:');
console.log(JSON.stringify(oneToManyExample, null, 2));
console.log('');

// 2. ONE-TO-ONE (Less common)
// Example: One user has one profile
/*
model User {
  id      Int      @id @default(autoincrement())
  email   String   @unique
  name    String
  
  profile Profile? // Optional: User might not have profile yet
}

model Profile {
  id     Int    @id @default(autoincrement())
  bio    String?
  avatar String?
  
  userId Int     @unique  // UNIQUE makes it one-to-one!
  user   User    @relation(fields: [userId], references: [id])
}
*/

console.log('=== ONE-TO-ONE Relationship ===');
console.log('One User → One Profile');
console.log('Schema: Profile.userId must be @unique');
console.log('');

let oneToOneExample = {
  user: {
    id: 1,
    email: 'alice@example.com',
    name: 'Alice',
    profile: {  // Single profile object
      id: 1,
      bio: 'Software developer',
      avatar: 'https://example.com/alice.jpg',
      userId: 1
    }
  }
};

console.log('Example data:');
console.log(JSON.stringify(oneToOneExample, null, 2));
console.log('');

// 3. MANY-TO-MANY (Complex but powerful)
// Example: Users can like many posts, posts can be liked by many users
/*
model User {
  id        Int    @id @default(autoincrement())
  email     String @unique
  name      String
  
  likedPosts Post[] @relation("PostLikes")
}

model Post {
  id       Int      @id @default(autoincrement())
  title    String
  content  String?
  
  likedBy  User[]   @relation("PostLikes")
}

// Prisma creates a join table automatically:
// _PostLikes (userId, postId)
*/

console.log('=== MANY-TO-MANY Relationship ===');
console.log('Many Users ↔ Many Posts (likes)');
console.log('Schema: Both sides have arrays, named relation');
console.log('Prisma auto-creates join table: _PostLikes');
console.log('');

let manyToManyExample = {
  user: {
    id: 1,
    name: 'Alice',
    likedPosts: [  // Array of posts Alice liked
      { id: 5, title: 'Cool Post' },
      { id: 7, title: 'Amazing Article' }
    ]
  },
  post: {
    id: 5,
    title: 'Cool Post',
    likedBy: [  // Array of users who liked this post
      { id: 1, name: 'Alice' },
      { id: 2, name: 'Bob' },
      { id: 3, name: 'Charlie' }
    ]
  }
};

console.log('Example data:');
console.log(JSON.stringify(manyToManyExample, null, 2));
console.log('');

// EXPLICIT MANY-TO-MANY (when you need extra fields)
// Example: User enrollments in courses (with enrollment date)
/*
model User {
  id          Int          @id @default(autoincrement())
  name        String
  enrollments Enrollment[]
}

model Course {
  id          Int          @id @default(autoincrement())
  title       String
  enrollments Enrollment[]
}

model Enrollment {
  id         Int      @id @default(autoincrement())
  enrolledAt DateTime @default(now())
  grade      String?
  
  userId     Int
  user       User     @relation(fields: [userId], references: [id])
  
  courseId   Int
  course     Course   @relation(fields: [courseId], references: [id])
  
  @@unique([userId, courseId])  // Can't enroll in same course twice
}
*/

console.log('=== EXPLICIT MANY-TO-MANY ===');
console.log('When you need extra fields on the relationship');
console.log('Example: Enrollment date, grade, status');
console.log('');

// QUERYING RELATIONS WITH PRISMA CLIENT

let queryExamples = [
  '// Get user with all their posts',
  'let user = await prisma.user.findUnique({',
  '  where: { id: 1 },',
  '  include: { posts: true }  // Include related posts',
  '});',
  '',
  '// Get post with author info',
  'let post = await prisma.post.findUnique({',
  '  where: { id: 1 },',
  '  include: { author: true }  // Include related user',
  '});',
  '',
  '// Create post with author connection',
  'let post = await prisma.post.create({',
  '  data: {',
  '    title: "New Post",',
  '    content: "Hello!",',
  '    author: {',
  '      connect: { id: 1 }  // Connect to existing user',
  '    }',
  '  }',
  '});',
  '',
  '// Create user with posts in one go',
  'let user = await prisma.user.create({',
  '  data: {',
  '    email: "bob@example.com",',
  '    name: "Bob",',
  '    posts: {',
  '      create: [  // Create posts at same time',
  '        { title: "First", content: "Content 1" },',
  '        { title: "Second", content: "Content 2" }',
  '      ]',
  '    }',
  '  }',
  '});'
];

console.log('Querying Relations:');
queryExamples.forEach(line => console.log(line));
```
