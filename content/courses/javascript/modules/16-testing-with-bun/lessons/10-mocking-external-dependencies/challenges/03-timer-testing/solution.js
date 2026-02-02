// Simulating bun:test with fake timers for this exercise
const describe = (name, fn) => { console.log(`\ndescribe: ${name}`); fn(); };
const it = async (name, fn) => {
  try { await fn(); console.log(`  \u2713 ${name}`); }
  catch (e) { console.log(`  \u2717 ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); },
  toHaveBeenCalledTimes: (n) => { if (val._calls.length !== n) throw new Error(`Expected ${n} calls, got ${val._calls.length}`); },
  toHaveBeenCalledWith: (arg) => { 
    if (!val._calls.some(c => c[0] === arg)) throw new Error(`Expected call with ${arg}`);
  }
});

function mock() {
  const fn = (...args) => { fn._calls.push(args); };
  fn._calls = [];
  return fn;
}

let currentTime = 0;
let timers = [];

function resetTimers() {
  currentTime = 0;
  timers = [];
}

function fakeSetTimeout(callback, delay) {
  const id = timers.length;
  timers.push({ callback, executeAt: currentTime + delay, id });
  return id;
}

function fakeClearTimeout(id) {
  timers = timers.filter(t => t.id !== id);
}

function advanceTime(ms) {
  currentTime += ms;
  const ready = timers.filter(t => t.executeAt <= currentTime);
  timers = timers.filter(t => t.executeAt > currentTime);
  ready.sort((a, b) => a.executeAt - b.executeAt);
  ready.forEach(t => t.callback());
}

const originalSetTimeout = globalThis.setTimeout;
const originalClearTimeout = globalThis.clearTimeout;
globalThis.setTimeout = fakeSetTimeout;
globalThis.clearTimeout = fakeClearTimeout;

function debounce(fn, delay) {
  let timeoutId;
  return function (...args) {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(() => fn.apply(this, args), delay);
  };
}

function throttle(fn, limit) {
  let inThrottle = false;
  return function (...args) {
    if (!inThrottle) {
      fn.apply(this, args);
      inThrottle = true;
      setTimeout(() => inThrottle = false, limit);
    }
  };
}

describe('Debounce with Fake Timers', () => {
  it('delays execution until after delay period', () => {
    resetTimers();
    const callback = mock();
    const debounced = debounce(callback, 300);

    debounced('test');
    
    expect(callback).toHaveBeenCalledTimes(0);

    advanceTime(300);
    expect(callback).toHaveBeenCalledTimes(1);
    expect(callback).toHaveBeenCalledWith('test');
  });

  it('only calls once for multiple rapid calls', () => {
    resetTimers();
    const callback = mock();
    const debounced = debounce(callback, 300);

    debounced('a');
    debounced('b');
    debounced('c');
    debounced('d');
    debounced('e');

    advanceTime(300);
    
    expect(callback).toHaveBeenCalledTimes(1);
    expect(callback).toHaveBeenCalledWith('e');
  });

  it('resets timer on each call', () => {
    resetTimers();
    const callback = mock();
    const debounced = debounce(callback, 300);

    debounced('first');
    advanceTime(200);
    
    debounced('second');
    advanceTime(200);
    
    expect(callback).toHaveBeenCalledTimes(0);
    
    advanceTime(100);
    expect(callback).toHaveBeenCalledTimes(1);
    expect(callback).toHaveBeenCalledWith('second');
  });
});

describe('Throttle with Fake Timers', () => {
  it('executes immediately on first call', () => {
    resetTimers();
    const callback = mock();
    const throttled = throttle(callback, 1000);

    throttled('first');
    
    expect(callback).toHaveBeenCalledTimes(1);
    expect(callback).toHaveBeenCalledWith('first');
  });

  it('ignores calls during throttle period', () => {
    resetTimers();
    const callback = mock();
    const throttled = throttle(callback, 1000);

    throttled('first');
    throttled('second');
    throttled('third');

    expect(callback).toHaveBeenCalledTimes(1);
  });

  it('allows next call after throttle period ends', () => {
    resetTimers();
    const callback = mock();
    const throttled = throttle(callback, 1000);

    throttled('first');
    advanceTime(1000);
    throttled('second');

    expect(callback).toHaveBeenCalledTimes(2);
  });
});

globalThis.setTimeout = originalSetTimeout;
globalThis.clearTimeout = originalClearTimeout;

console.log('\n--- Timer Tests Complete ---');