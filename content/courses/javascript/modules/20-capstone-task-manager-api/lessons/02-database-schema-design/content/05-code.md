---
type: "EXAMPLE"
title: "Create a Seed Script"
---

Create sample data for development and testing:

```typescript
// prisma/seed.ts
import { PrismaClient } from '@prisma/client';

const prisma = new PrismaClient();

async function main() {
  console.log('Seeding database...');

  // Create a demo user (password: 'password123')
  // In real app, this hash would come from Bun.password.hash()
  const demoHash = await Bun.password.hash('password123');
  
  const user = await prisma.user.upsert({
    where: { email: 'demo@example.com' },
    update: {},
    create: {
      email: 'demo@example.com',
      passwordHash: demoHash,
      name: 'Demo User',
    },
  });

  console.log('Created user:', user.email);

  // Create categories
  const workCategory = await prisma.category.upsert({
    where: { 
      userId_name: { userId: user.id, name: 'Work' }
    },
    update: {},
    create: {
      name: 'Work',
      color: '#EF4444', // Red
      userId: user.id,
    },
  });

  const personalCategory = await prisma.category.upsert({
    where: { 
      userId_name: { userId: user.id, name: 'Personal' }
    },
    update: {},
    create: {
      name: 'Personal',
      color: '#22C55E', // Green
      userId: user.id,
    },
  });

  console.log('Created categories:', workCategory.name, personalCategory.name);

  // Create sample tasks
  const tasks = await prisma.task.createMany({
    data: [
      {
        title: 'Complete API documentation',
        description: 'Write OpenAPI spec for all endpoints',
        status: 'in_progress',
        priority: 'high',
        userId: user.id,
        categoryId: workCategory.id,
        dueDate: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000), // 1 week
      },
      {
        title: 'Review pull requests',
        description: 'Check pending PRs from team',
        status: 'pending',
        priority: 'medium',
        userId: user.id,
        categoryId: workCategory.id,
      },
      {
        title: 'Buy groceries',
        description: 'Milk, eggs, bread, vegetables',
        status: 'pending',
        priority: 'low',
        userId: user.id,
        categoryId: personalCategory.id,
      },
      {
        title: 'Learn Prisma',
        description: 'Complete the Prisma tutorial',
        status: 'completed',
        priority: 'medium',
        userId: user.id,
        categoryId: personalCategory.id,
      },
    ],
    skipDuplicates: true,
  });

  console.log('Created tasks:', tasks.count);
  console.log('Seeding complete!');
}

main()
  .catch((e) => {
    console.error('Seeding failed:', e);
    process.exit(1);
  })
  .finally(async () => {
    await prisma.$disconnect();
  });
```
