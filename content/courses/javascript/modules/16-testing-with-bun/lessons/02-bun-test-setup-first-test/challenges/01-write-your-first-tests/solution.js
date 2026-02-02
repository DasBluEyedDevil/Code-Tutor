// Simulating bun:test functions for this exercise
const describe = (name, fn) => { console.log(`describe: ${name}`); fn(); };
const it = (name, fn) => {
  try { fn(); console.log(`  ✓ ${name}`); }
  catch (e) { console.log(`  ✗ ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); }
});

// Function to test
function multiply(a, b) {
  return a * b;
}

// Write your tests below
describe('multiply', () => {
  it('multiplies two positive numbers', () => {
    expect(multiply(3, 4)).toBe(12);
  });

  it('handles negative numbers', () => {
    expect(multiply(-2, 5)).toBe(-10);
  });

  it('returns zero when multiplied by zero', () => {
    expect(multiply(100, 0)).toBe(0);
  });
});