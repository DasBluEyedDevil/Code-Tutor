// Authentication System Test Plan
//
// Feature: User Authentication
// Components: Registration, Login, Token Management

// === UNIT TESTS ===
// Test individual functions in isolation

// Unit Test 1:
// What: Email validation function returns true for valid emails
// Why: Email validation is a pure function that can be tested without
//      any dependencies. It prevents invalid emails from entering the system.

// Unit Test 2:
// What: Password hasher produces different output for same password with different salts
// Why: Ensures our hashing is properly salted, which is critical for security.
//      This is a pure function with no external dependencies.

// Unit Test 3:
// What: JWT token generator includes correct claims (user ID, expiration)
// Why: Token structure is critical for authentication. Testing the generator
//      in isolation verifies the token format without needing a full request.

// Unit Test 4 (bonus):
// What: Password validation rejects passwords under 8 characters
// Why: Enforces security policy. Pure function, easy to test all edge cases.

// === INTEGRATION TESTS ===
// Test components working together

// Integration Test 1:
// What: User registration saves hashed password to database
// Why: Tests that the registration service correctly integrates with
//      both the password hasher AND the database. Verifies the password
//      stored is not plaintext.

// Integration Test 2:
// What: Login service retrieves user and validates password
// Why: Tests the interaction between database lookup, password comparison,
//      and token generation. These components must work together correctly.

// === END-TO-END TESTS ===
// Test complete user workflows

// E2E Test 1:
// What: Full registration -> login -> access protected route flow
// Why: Verifies the complete user journey works from start to finish.
//      A user should be able to register, login, receive a token, and
//      use that token to access protected endpoints. This catches issues
//      that only appear when all systems work together in production-like
//      conditions.