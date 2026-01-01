// Simulating bun:test
const mock = (fn) => {
  const mockFn = (...args) => { mockFn._calls.push(args); return mockFn._returnValue; };
  mockFn._calls = [];
  mockFn._returnValue = undefined;
  mockFn.mockReturnValue = (val) => { mockFn._returnValue = val; return mockFn; };
  return mockFn;
};
let _fakeTime = 0;
const setSystemTime = (date) => { _fakeTime = date ? date.getTime() : 0; };
const advanceTime = (ms) => {
  _fakeTime += ms;
  global._pendingTimers?.forEach(t => {
    if (t.time <= _fakeTime && !t.called) {
      t.called = true; t.fn();
    }
  });
};
global._pendingTimers = [];
const originalSetTimeout = setTimeout;
global.setTimeout = (fn, ms) => {
  global._pendingTimers.push({ fn, time: (_fakeTime || 0) + ms, called: false });
};

const describe = (name, fn) => { console.log(`describe: ${name}`); fn(); };
const it = async (name, fn) => {
  try { await fn(); console.log(`  \u2713 ${name}`); }
  catch (e) { console.log(`  \u2717 ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); },
  toHaveBeenCalled: () => { if (val._calls.length === 0) throw new Error('Expected function to be called'); },
  not: { toHaveBeenCalled: () => { if (val._calls?.length > 0) throw new Error('Expected function not to be called'); } }
});
const beforeEach = (fn) => fn();
const afterEach = (fn) => {};

// Functions to test
async function fetchUserData(id) {
  await new Promise(r => originalSetTimeout(r, 10));
  if (id <= 0) throw new Error('Invalid ID');
  return { id, name: 'Test User' };
}

function debounce(fn, delay) {
  let timeoutId;
  return (...args) => {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(() => fn(...args), delay);
  };
}

describe('fetchUserData', () => {
  it('returns user data for valid id', async () => {
    const user = await fetchUserData(1);
    expect(user.name).toBe('Test User');
  });

  it('throws error for invalid id', async () => {
    try {
      await fetchUserData(0);
      throw new Error('Should have thrown');
    } catch (e) {
      expect(e.message).toBe('Invalid ID');
    }
  });
});

describe('debounce', () => {
  beforeEach(() => {
    setSystemTime(new Date('2024-01-01'));
  });

  it('delays function execution', () => {
    const fn = mock();
    const debounced = debounce(fn, 500);

    debounced();
    expect(fn).not.toHaveBeenCalled();

    advanceTime(500);
    expect(fn).toHaveBeenCalled();
  });
});