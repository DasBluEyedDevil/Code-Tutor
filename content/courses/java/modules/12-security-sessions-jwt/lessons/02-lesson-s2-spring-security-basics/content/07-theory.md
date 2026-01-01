---
type: "THEORY"
title: "Password Encoding"
---

NEVER store passwords in plain text:

// CATASTROPHICALLY WRONG
user.setPassword(rawPassword);  // Stored as-is

// If database is breached, all passwords exposed!

ALWAYS hash passwords:

@Bean
public PasswordEncoder passwordEncoder() {
    return new BCryptPasswordEncoder();
}

// Registration
String hashedPassword = passwordEncoder.encode(rawPassword);
user.setPassword(hashedPassword);
// Stored: $2a$10$N9qo8uLOickgx2ZMRZoMy...

// Login - Spring Security handles this automatically!
// It compares: encoder.matches(rawPassword, storedHash)

WHY BCrypt?
- Intentionally slow (prevents brute force)
- Built-in salt (prevents rainbow tables)
- Adjustable cost factor
- Industry standard

BCrypt cost factor:
new BCryptPasswordEncoder(12)  // 2^12 iterations
// Higher = more secure but slower
// 10 is default, 12+ recommended for 2025