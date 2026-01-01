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
  // Arrange-Act-Assert pattern
  
  it('returns true for valid password', () => {
    // Arrange
    const password = 'secure123';
    // Act
    const result = validatePassword(password);
    // Assert
    expect(result).toBe(true);
  });

  it('returns false for password without number', () => {
    // YOUR CODE HERE
  });

  it('returns false for short password', () => {
    // YOUR CODE HERE
  });

  it('returns false for empty string', () => {
    // YOUR CODE HERE
  });

  it('returns false for null', () => {
    // YOUR CODE HERE
  });
});