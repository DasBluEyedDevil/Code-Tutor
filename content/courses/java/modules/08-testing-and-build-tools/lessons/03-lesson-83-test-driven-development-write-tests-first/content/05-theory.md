---
type: "THEORY"
title: "💻 TDD Example: Building a Password Validator"
---

```java
STEP 1: Write first test (RED)
@Test
void testPasswordTooShort() {
    assertFalse(PasswordValidator.isValid("abc"));
}
// Fails: PasswordValidator doesn't exist

STEP 2: Write minimal code (GREEN)
public class PasswordValidator {
    public static boolean isValid(String password) {
        return password.length() >= 8;
    }
}
// Test passes!

STEP 3: Add another test (RED)
@Test
void testPasswordMustHaveNumber() {
    assertFalse(PasswordValidator.isValid("abcdefgh"));
}
// Fails: password "abcdefgh" is considered valid

STEP 4: Update code (GREEN)
public static boolean isValid(String password) {
    if (password.length() < 8) return false;
    return password.matches(".*\\d.*");  // Must contain digit
}
// Both tests pass!

STEP 5: Continue with more tests
@Test
void testPasswordMustHaveUppercase() { ... }
@Test
void testValidPassword() { ... }

Build feature incrementally, one test at a time!
```