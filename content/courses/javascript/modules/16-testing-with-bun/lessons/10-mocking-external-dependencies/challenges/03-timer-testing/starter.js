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

// Simple mock function
function mock() {
  const fn = (...args) => { fn._calls.push(args); };
  fn._calls = [];
  return fn;
}

// Fake timer implementation
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

// Override global setTimeout for testing
const originalSetTimeout = globalThis.setTimeout;
const originalClearTimeout = globalThis.clearTimeout;
globalThis.setTimeout = fakeSetTimeout;
globalThis.clearTimeout = fakeClearTimeout;

// Functions to test
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
    
    // Callback should NOT be called immediately
    expect(callback).toHaveBeenCalledTimes(0);

    // YOUR CODE: Advance time by 300ms and verify callback was called
  });

  it('only calls once for multiple rapid calls', () => {
    resetTimers();
    const callback = mock();
    const debounced = debounce(callback, 300);

    // YOUR CODE: Call debounced 5 times rapidly
    // Advance time past the delay
    // Verify callback was only called once with the last argument
  });

  it('resets timer on each call', () => {
    resetTimers();
    const callback = mock();
    const debounced = debounce(callback, 300);

    debounced('first');
    advanceTime(200);  // 200ms passed
    
    debounced('second');  // Reset timer
    advanceTime(200);  // Another 200ms (only 200ms since reset)
    
    // YOUR CODE: Verify callback hasn't been called yet
    // Then advance the remaining time and verify it's called with 'second'
  });
});

describe('Throttle with Fake Timers', () => {
  it('executes immediately on first call', () => {
    resetTimers();
    const callback = mock();
    const throttled = throttle(callback, 1000);

    throttled('first');
    
    // YOUR CODE: Verify callback was called immediately
  });

  it('ignores calls during throttle period', () => {
    resetTimers();
    const callback = mock();
    const throttled = throttle(callback, 1000);

    throttled('first');
    throttled('second');  // Should be ignored
    throttled('third');   // Should be ignored

    // YOUR CODE: Verify callback was only called once
  });

  it('allows next call after throttle period ends', () => {
    resetTimers();
    const callback = mock();
    const throttled = throttle(callback, 1000);

    throttled('first');
    advanceTime(1000);  // Throttle period ends
    throttled('second');

    // YOUR CODE: Verify callback was called twice
  });
});

// Restore global timers
globalThis.setTimeout = originalSetTimeout;
globalThis.clearTimeout = originalClearTimeout;

console.log('\n--- Timer Tests Complete ---');