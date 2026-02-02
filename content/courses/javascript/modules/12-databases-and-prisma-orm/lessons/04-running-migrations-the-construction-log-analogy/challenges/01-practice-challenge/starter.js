// Migration system simulator
let migrations = [];
let appliedMigrations = new Set();

// Create migration
function createMigration(name, sql) {
  let timestamp = Date.now();
  let migration = {
    id: `${timestamp}_${name}`,
    name: name,
    sql: sql,
    createdAt: new Date().toISOString()
  };
  
  migrations.push(migration);
  console.log(`✓ Created migration: ${migration.id}`);
  return migration;
}

// Apply migrations
function applyMigrations() {
  console.log('\nApplying migrations...');
  
  for (let migration of migrations) {
    if (!appliedMigrations.has(migration.id)) {
      console.log(`  Running: ${migration.name}`);
      console.log(`  SQL: ${migration.sql}`);
      appliedMigrations.add(migration.id);
      console.log(`  ✓ Applied: ${migration.id}\n`);
    } else {
      console.log(`  ⊘ Skipped (already applied): ${migration.name}`);
    }
  }
}

// Get migration status
function getMigrationStatus() {
  console.log('=== Migration Status ===');
  console.log(`Total migrations: ${migrations.length}`);
  console.log(`Applied: ${appliedMigrations.size}`);
  console.log(`Pending: ${migrations.length - appliedMigrations.size}`);
}

// Create migrations
console.log('=== Creating Migrations ===\n');

createMigration('init', 'CREATE TABLE users (id INT, email TEXT, name TEXT);');

setTimeout(() => {
  createMigration('add_posts', 'CREATE TABLE posts (id INT, title TEXT, author_id INT);');
}, 100);

setTimeout(() => {
  createMigration('add_timestamps', 'ALTER TABLE users ADD COLUMN created_at TIMESTAMP;');
}, 200);

setTimeout(() => {
  applyMigrations();
  getMigrationStatus();
  
  console.log('\n=== Migration History ===');
  migrations.forEach((m, i) => {
    let status = appliedMigrations.has(m.id) ? '✓ Applied' : '⏳ Pending';
    console.log(`${i + 1}. ${m.name} - ${status}`);
  });
}, 400);