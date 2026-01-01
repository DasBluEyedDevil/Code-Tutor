---
type: "THEORY"
title: "⚠️ Password Security - Never Store Plain Passwords!"
---

❌ TERRIBLE - Plain passwords:

users table:
| id | username | password    |
|----|----------|-------------|
| 1  | alice    | password123 |  ← ANYONE CAN READ THIS!
| 2  | bob      | secret456   |  ← DATABASE BREACH = DISASTER

✓ CORRECT - Hashed passwords:

users table:
| id | username | password                                                   |
|----|----------|---------------------------------------------------------|
| 1  | alice    | $2a$10$abcd1234...xyz789 |  ← Hashed, can't reverse
| 2  | bob      | $2a$10$wxyz9876...abc123 |  ← Even if stolen, useless

Use BCryptPasswordEncoder:

@Service
public class UserService {
    @Autowired
    private PasswordEncoder passwordEncoder;
    
    public User registerUser(String username, String plainPassword) {
        User user = new User();
        user.setUsername(username);
        
        // Hash password before saving
        String hashedPassword = passwordEncoder.encode(plainPassword);
        user.setPassword(hashedPassword);
        
        return userRepository.save(user);
    }
}

BCrypt:
- One-way hash (can't reverse)
- Salted (same password = different hash)
- Slow on purpose (prevents brute force)
- Industry standard