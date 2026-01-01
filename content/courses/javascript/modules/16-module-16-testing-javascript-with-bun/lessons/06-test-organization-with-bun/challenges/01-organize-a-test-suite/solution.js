// Simulating bun:test lifecycle hooks
let setupLog = [];
const beforeAll = (fn) => { setupLog.push('beforeAll'); fn(); };
const afterAll = (fn) => { setupLog.push('afterAll scheduled'); };
const beforeEach = (fn) => { fn(); };
const afterEach = (fn) => {};

const describe = (name, fn) => { console.log(`describe: ${name}`); fn(); };
const it = (name, fn) => {
  try { fn(); console.log(`  \u2713 ${name}`); }
  catch (e) { console.log(`  \u2717 ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); },
  toContain: (exp) => { if (!val.includes(exp)) throw new Error(`Expected to contain ${exp}`); },
  toHaveLength: (exp) => { if (val.length !== exp) throw new Error(`Expected length ${exp}`); }
});

// Database simulation
let db = { connected: false, users: [] };

describe('UserService', () => {
  beforeAll(() => {
    db.connected = true;
    db.users = [];
    console.log('  [setup] Database connected');
  });

  afterAll(() => {
    db.connected = false;
    console.log('  [cleanup] Database disconnected');
  });

  beforeEach(() => {
    db.users = [];
  });

  describe('createUser', () => {
    it('creates a user in the database', () => {
      db.users.push({ id: 1, name: 'Alice' });
      
      expect(db.users).toHaveLength(1);
      expect(db.connected).toBe(true);
    });

    it('starts with empty users after reset', () => {
      expect(db.users).toHaveLength(0);
    });
  });
});

console.log('\nSetup log:', setupLog.join(' -> '));