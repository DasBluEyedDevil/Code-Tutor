// Identify each test type: unit, integration, or e2e

// Test 1: Check if formatPrice(10.5) returns '$10.50'
// Type: unit (tests single pure function)

// Test 2: Verify login form submits to API and stores token
// Type: integration (tests form + API + storage together)

// Test 3: User can browse products, add to cart, and checkout
// Type: e2e (tests complete user journey)

// Test 4: Check if validateEmail returns false for 'invalid'
// Type: unit (tests single pure function)

console.log('Test 1: formatPrice - unit test');
console.log('Test 2: login form + API - integration test');
console.log('Test 3: full checkout flow - e2e test');
console.log('Test 4: validateEmail - unit test');