// Simulating bun:test for this exercise
const describe = (name, fn) => { console.log(`\ndescribe: ${name}`); fn(); };
const it = async (name, fn) => {
  try { await fn(); console.log(`  \u2713 ${name}`); }
  catch (e) { console.log(`  \u2717 ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); },
  toEqual: (exp) => { if (JSON.stringify(val) !== JSON.stringify(exp)) throw new Error(`Expected ${JSON.stringify(exp)}`); },
  toHaveBeenCalledWith: (...args) => {
    const calls = val._calls || [];
    const found = calls.some(c => JSON.stringify(c) === JSON.stringify(args));
    if (!found) throw new Error(`Expected call with ${JSON.stringify(args)}`);
  },
  toHaveBeenCalledTimes: (n) => {
    const calls = val._calls || [];
    if (calls.length !== n) throw new Error(`Expected ${n} calls, got ${calls.length}`);
  }
});

function mock(implementation = () => {}) {
  const fn = (...args) => {
    fn._calls.push(args);
    return fn._returnValue !== undefined ? fn._returnValue : implementation(...args);
  };
  fn._calls = [];
  fn._returnValue = undefined;
  fn.mockReturnValue = (val) => { fn._returnValue = val; return fn; };
  fn.mockResolvedValue = (val) => { fn._returnValue = Promise.resolve(val); return fn; };
  return fn;
}

class UserRepository {
  constructor(database) {
    this.db = database;
  }

  async findById(id) {
    return await this.db.findById('users', id);
  }

  async create(userData) {
    return await this.db.create('users', userData);
  }

  async update(id, userData) {
    const existing = await this.db.findById('users', id);
    if (!existing) throw new Error('User not found');
    return await this.db.update('users', id, userData);
  }

  async delete(id) {
    return await this.db.delete('users', id);
  }
}

describe('UserRepository with Mock Database', () => {
  it('finds user by ID', async () => {
    const mockDatabase = {
      findById: mock().mockResolvedValue({ id: 1, name: 'Alice', email: 'alice@test.com' })
    };

    const repo = new UserRepository(mockDatabase);
    const user = await repo.findById(1);

    expect(user.name).toBe('Alice');
    expect(mockDatabase.findById).toHaveBeenCalledWith('users', 1);
  });

  it('creates new user', async () => {
    const mockDatabase = {
      create: mock().mockResolvedValue({ id: 2, name: 'Bob', email: 'bob@test.com' })
    };

    const repo = new UserRepository(mockDatabase);
    const userData = { name: 'Bob', email: 'bob@test.com' };
    const newUser = await repo.create(userData);

    expect(newUser.id).toBe(2);
    expect(mockDatabase.create).toHaveBeenCalledWith('users', userData);
  });

  it('updates existing user', async () => {
    const mockDatabase = {
      findById: mock().mockResolvedValue({ id: 1, name: 'Alice', email: 'alice@test.com' }),
      update: mock().mockResolvedValue({ id: 1, name: 'Alice Updated', email: 'alice@test.com' })
    };

    const repo = new UserRepository(mockDatabase);
    const updated = await repo.update(1, { name: 'Alice Updated' });

    expect(updated.name).toBe('Alice Updated');
    expect(mockDatabase.findById).toHaveBeenCalledWith('users', 1);
    expect(mockDatabase.update).toHaveBeenCalledWith('users', 1, { name: 'Alice Updated' });
  });

  it('throws error when updating non-existent user', async () => {
    const mockDatabase = {
      findById: mock().mockResolvedValue(null)
    };

    const repo = new UserRepository(mockDatabase);
    
    try {
      await repo.update(999, { name: 'Ghost' });
      throw new Error('Should have thrown');
    } catch (e) {
      expect(e.message).toBe('User not found');
    }
  });
});

console.log('\n--- Mock Service Tests Complete ---');