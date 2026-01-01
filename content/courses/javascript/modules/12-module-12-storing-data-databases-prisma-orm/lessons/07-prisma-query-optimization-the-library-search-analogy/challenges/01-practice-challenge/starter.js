// Query Optimization Simulator

class MockDatabase {
  constructor() {
    this.users = Array.from({ length: 100 }, (_, i) => ({
      id: i + 1,
      name: `User ${i + 1}`,
      email: `user${i + 1}@example.com`,
      department: ['Engineering', 'Sales', 'Marketing', 'HR'][i % 4],
      salary: 50000 + Math.floor(Math.random() * 50000),
      createdAt: new Date(2024, 0, i + 1)
    }));
  }
  
  findMany(options = {}) {
    let result = [...this.users];
    
    // Filter
    if (options.where) {
      result = result.filter(u => {
        for (let [key, value] of Object.entries(options.where)) {
          if (u[key] !== value) return false;
        }
        return true;
      });
    }
    
    // Order
    if (options.orderBy) {
      const [field, dir] = Object.entries(options.orderBy)[0];
      result.sort((a, b) => dir === 'desc' ? b[field] - a[field] : a[field] - b[field]);
    }
    
    // Pagination
    if (options.skip) result = result.slice(options.skip);
    if (options.take) result = result.slice(0, options.take);
    
    // Select
    if (options.select) {
      result = result.map(item => {
        const obj = {};
        for (let key of Object.keys(options.select)) {
          if (options.select[key]) obj[key] = item[key];
        }
        return obj;
      });
    }
    
    return result;
  }
  
  count(where = {}) {
    return this.users.filter(u => {
      for (let [k, v] of Object.entries(where)) {
        if (u[k] !== v) return false;
      }
      return true;
    }).length;
  }
  
  aggregate(options) {
    const result = {};
    const data = this.users;
    
    if (options._count) result._count = data.length;
    
    if (options._sum) {
      result._sum = {};
      for (let field of Object.keys(options._sum)) {
        result._sum[field] = data.reduce((s, u) => s + u[field], 0);
      }
    }
    
    if (options._avg) {
      result._avg = {};
      for (let field of Object.keys(options._avg)) {
        result._avg[field] = data.reduce((s, u) => s + u[field], 0) / data.length;
      }
    }
    
    return result;
  }
  
  groupBy(options) {
    const groups = {};
    
    for (let item of this.users) {
      const key = item[options.by[0]];
      if (!groups[key]) groups[key] = { [options.by[0]]: key, _items: [] };
      groups[key]._items.push(item);
    }
    
    return Object.values(groups).map(g => {
      const result = { [options.by[0]]: g[options.by[0]] };
      if (options._count) result._count = g._items.length;
      if (options._avg) {
        result._avg = {};
        for (let f of Object.keys(options._avg)) {
          result._avg[f] = g._items.reduce((s, i) => s + i[f], 0) / g._items.length;
        }
      }
      return result;
    });
  }
}

const db = new MockDatabase();

// Demo 1: Select specific fields
console.log('1. Select specific fields:');
const selected = db.findMany({ select: { id: true, name: true }, take: 3 });
console.log(selected);

// Demo 2: Pagination
console.log('\n2. Pagination (5 per page):');
for (let page = 1; page <= 3; page++) {
  const data = db.findMany({ skip: (page - 1) * 5, take: 5, select: { id: true, name: true } });
  console.log(`Page ${page}:`, data.map(u => u.name).join(', '));
}

// Demo 3: Count
console.log('\n3. Counts:');
console.log('Total users:', db.count());
console.log('Engineering:', db.count({ department: 'Engineering' }));

// Demo 4: Aggregations
console.log('\n4. Salary Stats:');
const stats = db.aggregate({ _count: true, _sum: { salary: true }, _avg: { salary: true } });
console.log(stats);

// Demo 5: Group By
console.log('\n5. By Department:');
const byDept = db.groupBy({ by: ['department'], _count: true, _avg: { salary: true } });
console.log(byDept);