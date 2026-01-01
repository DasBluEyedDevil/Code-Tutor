// Simulated Prisma schema structure
let schema = {
  models: {
    Product: {
      fields: {
        id: { type: 'Int', primaryKey: true, autoIncrement: true },
        name: { type: 'String', required: true },
        price: { type: 'Float', required: true },
        inStock: { type: 'Boolean', default: true },
        description: { type: 'String', required: false },
        createdAt: { type: 'DateTime', default: 'now()' }
      }
    }
  }
};

// Function to generate SQL from model
function generateModelSQL(modelName, model) {
  let sql = `CREATE TABLE ${modelName} (\n`;
  
  let fields = [];
  for (let [fieldName, field] of Object.entries(model.fields)) {
    let line = `  ${fieldName} `;
    
    // Type mapping
    if (field.type === 'Int') line += 'INTEGER';
    else if (field.type === 'String') line += 'TEXT';
    else if (field.type === 'Float') line += 'REAL';
    else if (field.type === 'Boolean') line += 'BOOLEAN';
    else if (field.type === 'DateTime') line += 'TIMESTAMP';
    
    // Constraints
    if (field.primaryKey) line += ' PRIMARY KEY';
    if (field.autoIncrement) line += ' AUTOINCREMENT';
    if (field.required) line += ' NOT NULL';
    if (field.default !== undefined) {
      if (field.default === 'now()') line += ' DEFAULT CURRENT_TIMESTAMP';
      else if (typeof field.default === 'boolean') line += ` DEFAULT ${field.default ? 1 : 0}`;
      else line += ` DEFAULT ${field.default}`;
    }
    
    fields.push(line);
  }
  
  sql += fields.join(',\n');
  sql += '\n);';
  
  return sql;
}

// Generate SQL
let sql = generateModelSQL('Product', schema.models.Product);
console.log('Generated SQL:\n');
console.log(sql);

// Display schema
console.log('\nPrisma Schema Structure:');
console.log(JSON.stringify(schema, null, 2));