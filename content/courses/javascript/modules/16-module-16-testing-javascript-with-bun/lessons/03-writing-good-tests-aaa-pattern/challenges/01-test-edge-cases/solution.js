// Simulating bun:test
const describe = (name, fn) => { console.log(`describe: ${name}`); fn(); };
const it = (name, fn) => {
  try { fn(); console.log(`  ✓ ${name}`); }
  catch (e) { console.log(`  ✗ ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); }
});

// Function to test: password must be 8+ chars with at least one number
function validatePassword(password) {
  if (!password || password.length < 8) return false;
  return /\d/.test(password);
}

describe('validatePassword', () => {
  it('returns true for valid password', () => {
    const password = 'secure123';
    const result = validatePassword(password);
    expect(result).toBe(true);
  });

  it('returns false for password without number', () => {
    const password = 'securepassword';
    const result = validatePassword(password);
    expect(result).toBe(false);
  });

  it('returns false for short password', () => {
    const password = 'abc1';
    const result = validatePassword(password);
    expect(result).toBe(false);
  });

  it('returns false for empty string', () => {
    const password = '';
    const result = validatePassword(password);
    expect(result).toBe(false);
  });

  it('returns false for null', () => {
    const password = null;
    const result = validatePassword(password);
    expect(result).toBe(false);
  });
});