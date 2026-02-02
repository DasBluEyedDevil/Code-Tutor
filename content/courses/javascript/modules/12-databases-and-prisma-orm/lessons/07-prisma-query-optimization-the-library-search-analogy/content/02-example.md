---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Prisma query optimization.

```javascript
// Prisma Query Optimization - Complete Guide

console.log('=== Prisma Query Optimization ===\n');

// 1. SELECTING SPECIFIC FIELDS
// Don't fetch data you don't need!

let selectExample = `
// BAD: Fetches ALL fields
const users = await prisma.user.findMany();
// Returns: { id, email, password, name, bio, avatar, createdAt, updatedAt, ... }

// GOOD: Only fetch what you need
const users = await prisma.user.findMany({
  select: {
    id: true,
    name: true,
    email: true
    // password, bio, avatar NOT fetched = faster!
  }
});
// Returns: { id, name, email }
`;

console.log('1. SELECTING SPECIFIC FIELDS:');
console.log(selectExample);

// 2. PAGINATION - OFFSET-BASED (skip/take)
// For simple pagination with page numbers

let offsetPaginationExample = `
// Page-based pagination
const page = 2;
const pageSize = 10;

const users = await prisma.user.findMany({
  skip: (page - 1) * pageSize,  // Skip first 10 (page 1)
  take: pageSize,                // Take next 10
  orderBy: { createdAt: 'desc' }
});

// Page 1: skip 0, take 10 (items 1-10)
// Page 2: skip 10, take 10 (items 11-20)
// Page 3: skip 20, take 10 (items 21-30)

// Get total count for pagination UI
const totalCount = await prisma.user.count();
const totalPages = Math.ceil(totalCount / pageSize);
`;

console.log('\n2. OFFSET-BASED PAGINATION (skip/take):');
console.log(offsetPaginationExample);

// 3. PAGINATION - CURSOR-BASED
// More efficient for large datasets

let cursorPaginationExample = `
// Cursor-based pagination (more efficient)
const firstPage = await prisma.user.findMany({
  take: 10,
  orderBy: { id: 'asc' }
});

// Get the last item's ID as cursor
const lastUser = firstPage[firstPage.length - 1];

// Next page: start after the cursor
const secondPage = await prisma.user.findMany({
  take: 10,
  skip: 1,                    // Skip the cursor itself
  cursor: { id: lastUser.id }, // Start from this ID
  orderBy: { id: 'asc' }
});

// Why cursor is better for large data:
// - Offset: "Skip 10000 rows" = DB scans 10000 rows
// - Cursor: "Start at ID 10001" = DB jumps directly
`;

console.log('\n3. CURSOR-BASED PAGINATION:');
console.log(cursorPaginationExample);

// 4. AGGREGATIONS
// Let the database do the math!

let aggregationExample = `
// COUNT - How many records?
const userCount = await prisma.user.count();
const activeUsers = await prisma.user.count({
  where: { isActive: true }
});

// AGGREGATE - Sum, average, min, max
const stats = await prisma.order.aggregate({
  _sum: { total: true },
  _avg: { total: true },
  _min: { total: true },
  _max: { total: true },
  _count: true
});
// Returns: { _sum: { total: 50000 }, _avg: { total: 125 }, ... }

// GROUP BY - Statistics per category
const salesByCategory = await prisma.order.groupBy({
  by: ['category'],
  _sum: { total: true },
  _count: true,
  orderBy: { _sum: { total: 'desc' } }
});
// Returns: [{ category: 'Electronics', _sum: { total: 25000 }, _count: 100 }, ...]
`;

console.log('\n4. AGGREGATIONS (count, sum, avg, min, max):');
console.log(aggregationExample);

// 5. GROUPBY
// Get statistics grouped by field

let groupByExample = `
// Group by single field
const postsByAuthor = await prisma.post.groupBy({
  by: ['authorId'],
  _count: { id: true },
  orderBy: { _count: { id: 'desc' } }
});

// Group by multiple fields
const orderStats = await prisma.order.groupBy({
  by: ['status', 'paymentMethod'],
  _sum: { total: true },
  _count: true,
  having: {
    total: { _sum: { gt: 1000 } }  // Only groups with sum > 1000
  }
});

// Group by date (using raw for date functions)
const dailySales = await prisma.$queryRaw\`
  SELECT DATE(created_at) as date, SUM(total) as daily_total
  FROM orders
  GROUP BY DATE(created_at)
  ORDER BY date DESC
\`;
`;

console.log('\n5. GROUP BY:');
console.log(groupByExample);

// 6. RAW SQL
// When Prisma's API isn't enough

let rawSqlExample = `
// Raw query with tagged template (safe from injection)
const users = await prisma.$queryRaw\`
  SELECT * FROM users 
  WHERE email LIKE \${searchPattern}
  ORDER BY created_at DESC
  LIMIT 10
\`;

// Raw query with Prisma.sql helper
import { Prisma } from '@prisma/client';

const search = '%john%';
const users = await prisma.$queryRaw(
  Prisma.sql\`SELECT * FROM users WHERE name ILIKE \${search}\`
);

// Execute raw (for INSERT, UPDATE, DELETE)
const result = await prisma.$executeRaw\`
  UPDATE users SET last_seen = NOW() WHERE id = \${userId}
\`;
// Returns number of affected rows

// DANGER: Never use string interpolation!
// WRONG: prisma.$queryRaw(\`SELECT * FROM users WHERE id = \${id}\`)
// This is vulnerable to SQL injection!
`;

console.log('\n6. RAW SQL (when needed):');
console.log(rawSqlExample);

// 7. INCLUDE VS SELECT
// Two ways to control what's fetched

let includeVsSelectExample = `
// INCLUDE: Add relations to default fields
const user = await prisma.user.findUnique({
  where: { id: 1 },
  include: {
    posts: true,    // Includes ALL post fields
    profile: true   // Includes ALL profile fields
  }
});
// Returns: { id, email, name, ..., posts: [...], profile: {...} }

// SELECT: Choose exactly what to fetch (more control)
const user = await prisma.user.findUnique({
  where: { id: 1 },
  select: {
    id: true,
    name: true,
    posts: {
      select: {
        id: true,
        title: true
        // content NOT fetched
      },
      take: 5  // Only first 5 posts
    }
  }
});
// Returns: { id, name, posts: [{ id, title }, ...] }

// CAN'T mix include and select at the same level!
// WRONG: { select: { id: true }, include: { posts: true } }
`;

console.log('\n7. INCLUDE vs SELECT:');
console.log(includeVsSelectExample);

// SIMULATION: Query Optimization Demo

console.log('\n=== SIMULATION: Query Optimization ===\n');

class MockDatabase {
  constructor() {
    // Generate sample data
    this.users = Array.from({ length: 100 }, (_, i) => ({
      id: i + 1,
      name: `User ${i + 1}`,
      email: `user${i + 1}@example.com`,
      age: 20 + (i % 50),
      department: ['Engineering', 'Sales', 'Marketing', 'Support'][i % 4],
      salary: 50000 + (i * 100),
      createdAt: new Date(2024, 0, 1 + i)
    }));
  }
  
  // Simulated Prisma methods
  findMany(options = {}) {
    let result = [...this.users];
    
    // Where filtering
    if (options.where) {
      result = result.filter(u => {
        for (let [key, value] of Object.entries(options.where)) {
          if (typeof value === 'object') {
            if (value.gte && u[key] < value.gte) return false;
            if (value.lte && u[key] > value.lte) return false;
            if (value.gt && u[key] <= value.gt) return false;
            if (value.lt && u[key] >= value.lt) return false;
          } else if (u[key] !== value) {
            return false;
          }
        }
        return true;
      });
    }
    
    // Ordering
    if (options.orderBy) {
      const [field, order] = Object.entries(options.orderBy)[0];
      result.sort((a, b) => {
        if (order === 'desc') return b[field] > a[field] ? 1 : -1;
        return a[field] > b[field] ? 1 : -1;
      });
    }
    
    // Pagination
    if (options.skip) result = result.slice(options.skip);
    if (options.take) result = result.slice(0, options.take);
    
    // Select specific fields
    if (options.select) {
      result = result.map(item => {
        const selected = {};
        for (let key of Object.keys(options.select)) {
          if (options.select[key]) selected[key] = item[key];
        }
        return selected;
      });
    }
    
    return result;
  }
  
  count(options = {}) {
    let result = this.users;
    if (options.where) {
      result = result.filter(u => {
        for (let [key, value] of Object.entries(options.where)) {
          if (u[key] !== value) return false;
        }
        return true;
      });
    }
    return result.length;
  }
  
  aggregate(options = {}) {
    let data = this.users;
    if (options.where) {
      data = data.filter(u => {
        for (let [key, value] of Object.entries(options.where)) {
          if (u[key] !== value) return false;
        }
        return true;
      });
    }
    
    const result = {};
    
    if (options._count) result._count = data.length;
    
    if (options._sum) {
      result._sum = {};
      for (let field of Object.keys(options._sum)) {
        result._sum[field] = data.reduce((sum, u) => sum + (u[field] || 0), 0);
      }
    }
    
    if (options._avg) {
      result._avg = {};
      for (let field of Object.keys(options._avg)) {
        const values = data.map(u => u[field]).filter(v => v != null);
        result._avg[field] = values.reduce((a, b) => a + b, 0) / values.length;
      }
    }
    
    if (options._min) {
      result._min = {};
      for (let field of Object.keys(options._min)) {
        result._min[field] = Math.min(...data.map(u => u[field]));
      }
    }
    
    if (options._max) {
      result._max = {};
      for (let field of Object.keys(options._max)) {
        result._max[field] = Math.max(...data.map(u => u[field]));
      }
    }
    
    return result;
  }
  
  groupBy(options = {}) {
    const groups = {};
    
    for (let item of this.users) {
      const key = options.by.map(f => item[f]).join('|');
      if (!groups[key]) {
        groups[key] = {
          ...Object.fromEntries(options.by.map(f => [f, item[f]])),
          _items: []
        };
      }
      groups[key]._items.push(item);
    }
    
    return Object.values(groups).map(group => {
      const result = { ...group };
      delete result._items;
      
      if (options._count) result._count = group._items.length;
      
      if (options._sum) {
        result._sum = {};
        for (let field of Object.keys(options._sum)) {
          result._sum[field] = group._items.reduce((sum, i) => sum + (i[field] || 0), 0);
        }
      }
      
      if (options._avg) {
        result._avg = {};
        for (let field of Object.keys(options._avg)) {
          result._avg[field] = group._items.reduce((sum, i) => sum + i[field], 0) / group._items.length;
        }
      }
      
      return result;
    });
  }
}

const db = new MockDatabase();

// Demo: Select specific fields
console.log('1. Select specific fields (saves bandwidth):');
const selectedUsers = db.findMany({
  select: { id: true, name: true },
  take: 3
});
console.log(selectedUsers);

// Demo: Pagination
console.log('\n2. Pagination (page 2, 5 per page):');
const page2 = db.findMany({
  skip: 5,
  take: 5,
  orderBy: { id: 'asc' },
  select: { id: true, name: true }
});
console.log(page2);

// Demo: Aggregations
console.log('\n3. Aggregations (salary stats):');
const stats = db.aggregate({
  _count: true,
  _sum: { salary: true },
  _avg: { salary: true },
  _min: { salary: true },
  _max: { salary: true }
});
console.log(stats);

// Demo: Group by
console.log('\n4. Group by department:');
const byDept = db.groupBy({
  by: ['department'],
  _count: true,
  _avg: { salary: true }
});
console.log(byDept);

// Demo: Combined query
console.log('\n5. Combined: Filter + Order + Paginate + Select:');
const combined = db.findMany({
  where: { department: 'Engineering' },
  orderBy: { salary: 'desc' },
  take: 3,
  select: { id: true, name: true, salary: true }
});
console.log(combined);
```
