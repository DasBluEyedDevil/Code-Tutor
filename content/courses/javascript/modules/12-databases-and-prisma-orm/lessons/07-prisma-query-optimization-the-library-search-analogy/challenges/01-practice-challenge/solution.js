// Complete Query Optimization Simulator

class MockDatabase {
  constructor() {
    this.queryCount = 0;
    this.users = Array.from({ length: 100 }, (_, i) => ({
      id: i + 1,
      name: `User ${i + 1}`,
      email: `user${i + 1}@example.com`,
      department: ['Engineering', 'Sales', 'Marketing', 'HR'][i % 4],
      salary: 50000 + Math.floor(Math.random() * 50000),
      isActive: Math.random() > 0.2,
      createdAt: new Date(2024, 0, i + 1)
    }));
  }
  
  log(operation, details) {
    this.queryCount++;
    console.log(`  [Query ${this.queryCount}] ${operation}: ${details}`);
  }
  
  findMany(options = {}) {
    let result = [...this.users];
    let queryDetails = [];
    
    if (options.where) {
      result = result.filter(u => {
        for (let [key, value] of Object.entries(options.where)) {
          if (typeof value === 'object') {
            if (value.gte && u[key] < value.gte) return false;
            if (value.lte && u[key] > value.lte) return false;
          } else if (u[key] !== value) {
            return false;
          }
        }
        return true;
      });
      queryDetails.push(`WHERE ${JSON.stringify(options.where)}`);
    }
    
    if (options.orderBy) {
      const [field, dir] = Object.entries(options.orderBy)[0];
      result.sort((a, b) => {
        if (typeof a[field] === 'string') {
          return dir === 'desc' ? b[field].localeCompare(a[field]) : a[field].localeCompare(b[field]);
        }
        return dir === 'desc' ? b[field] - a[field] : a[field] - b[field];
      });
      queryDetails.push(`ORDER BY ${field} ${dir.toUpperCase()}`);
    }
    
    if (options.skip) {
      result = result.slice(options.skip);
      queryDetails.push(`SKIP ${options.skip}`);
    }
    
    if (options.take) {
      result = result.slice(0, options.take);
      queryDetails.push(`TAKE ${options.take}`);
    }
    
    if (options.select) {
      const fields = Object.keys(options.select).filter(k => options.select[k]);
      result = result.map(item => {
        const obj = {};
        for (let key of fields) {
          obj[key] = item[key];
        }
        return obj;
      });
      queryDetails.push(`SELECT ${fields.join(', ')}`);
    } else {
      queryDetails.push('SELECT *');
    }
    
    this.log('findMany', queryDetails.join(' '));
    return result;
  }
  
  count(where = null) {
    let result = this.users;
    
    if (where) {
      result = result.filter(u => {
        for (let [k, v] of Object.entries(where)) {
          if (u[k] !== v) return false;
        }
        return true;
      });
      this.log('count', `WHERE ${JSON.stringify(where)}`);
    } else {
      this.log('count', 'all records');
    }
    
    return result.length;
  }
  
  aggregate(options) {
    let data = this.users;
    const result = {};
    const ops = [];
    
    if (options.where) {
      data = data.filter(u => {
        for (let [k, v] of Object.entries(options.where)) {
          if (u[k] !== v) return false;
        }
        return true;
      });
    }
    
    if (options._count) {
      result._count = data.length;
      ops.push('COUNT');
    }
    
    if (options._sum) {
      result._sum = {};
      for (let field of Object.keys(options._sum)) {
        result._sum[field] = data.reduce((s, u) => s + (u[field] || 0), 0);
        ops.push(`SUM(${field})`);
      }
    }
    
    if (options._avg) {
      result._avg = {};
      for (let field of Object.keys(options._avg)) {
        const values = data.filter(u => u[field] != null);
        result._avg[field] = Math.round(values.reduce((s, u) => s + u[field], 0) / values.length);
        ops.push(`AVG(${field})`);
      }
    }
    
    if (options._min) {
      result._min = {};
      for (let field of Object.keys(options._min)) {
        result._min[field] = Math.min(...data.map(u => u[field]).filter(v => v != null));
        ops.push(`MIN(${field})`);
      }
    }
    
    if (options._max) {
      result._max = {};
      for (let field of Object.keys(options._max)) {
        result._max[field] = Math.max(...data.map(u => u[field]).filter(v => v != null));
        ops.push(`MAX(${field})`);
      }
    }
    
    this.log('aggregate', ops.join(', '));
    return result;
  }
  
  groupBy(options) {
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
    
    const result = Object.values(groups).map(g => {
      const row = { ...g };
      delete row._items;
      
      if (options._count) row._count = g._items.length;
      
      if (options._sum) {
        row._sum = {};
        for (let f of Object.keys(options._sum)) {
          row._sum[f] = g._items.reduce((s, i) => s + (i[f] || 0), 0);
        }
      }
      
      if (options._avg) {
        row._avg = {};
        for (let f of Object.keys(options._avg)) {
          row._avg[f] = Math.round(g._items.reduce((s, i) => s + i[f], 0) / g._items.length);
        }
      }
      
      return row;
    });
    
    if (options.orderBy) {
      const [key, dir] = Object.entries(options.orderBy)[0];
      result.sort((a, b) => {
        const aVal = key.startsWith('_') ? a[key] : a._sum?.[key] || a._avg?.[key] || 0;
        const bVal = key.startsWith('_') ? b[key] : b._sum?.[key] || b._avg?.[key] || 0;
        return dir === 'desc' ? bVal - aVal : aVal - bVal;
      });
    }
    
    this.log('groupBy', `BY ${options.by.join(', ')}`);
    return result;
  }
}

const db = new MockDatabase();

console.log('=== Query Optimization Demo ===\n');

// Demo 1: Full fetch vs Select
console.log('1. COMPARISON: Full fetch vs Select');
console.log('\nFetching ALL fields:');
const allFields = db.findMany({ take: 2 });
console.log(`  Fields returned: ${Object.keys(allFields[0]).length}`);
console.log(`  Data: ${JSON.stringify(allFields[0])}`);

console.log('\nFetching SELECTED fields:');
const selectedFields = db.findMany({ take: 2, select: { id: true, name: true } });
console.log(`  Fields returned: ${Object.keys(selectedFields[0]).length}`);
console.log(`  Data: ${JSON.stringify(selectedFields[0])}`);

// Demo 2: Pagination
console.log('\n2. PAGINATION (5 per page):');
for (let page = 1; page <= 3; page++) {
  const data = db.findMany({
    skip: (page - 1) * 5,
    take: 5,
    select: { id: true, name: true }
  });
  console.log(`  Page ${page}: IDs ${data.map(u => u.id).join(', ')}`);
}

// Demo 3: Count comparison
console.log('\n3. COMPARISON: length vs count()');
console.log('\nBAD: Fetching all to get count:');
const allUsers = db.findMany();
console.log(`  Count: ${allUsers.length}`);

console.log('\nGOOD: Using count():');
const count = db.count();
console.log(`  Count: ${count}`);

// Demo 4: Aggregations
console.log('\n4. AGGREGATIONS:');
const stats = db.aggregate({
  _count: true,
  _sum: { salary: true },
  _avg: { salary: true },
  _min: { salary: true },
  _max: { salary: true }
});
console.log(`  Total: ${stats._count} users`);
console.log(`  Total Salary: $${stats._sum.salary.toLocaleString()}`);
console.log(`  Avg Salary: $${stats._avg.salary.toLocaleString()}`);
console.log(`  Salary Range: $${stats._min.salary.toLocaleString()} - $${stats._max.salary.toLocaleString()}`);

// Demo 5: Group By
console.log('\n5. GROUP BY Department:');
const byDept = db.groupBy({
  by: ['department'],
  _count: true,
  _avg: { salary: true },
  orderBy: { _count: 'desc' }
});
byDept.forEach(d => {
  console.log(`  ${d.department}: ${d._count} employees, avg $${d._avg.salary.toLocaleString()}`);
});

console.log(`\n=== Total Queries Executed: ${db.queryCount} ===`);