---
type: "THEORY"
title: "⚠️ Avoid Field Injection (Old Way)"
---

❌ OLD STYLE - Not recommended in Spring Boot 4:

@Service
public class OldStyleService {
    @Autowired
    private UserRepository userRepository;  // Field injection
}

WHY AVOID?
- Cannot make field 'final'
- Harder to test (requires reflection)
- Dependencies not explicit
- Nullable references possible

✓ MODERN STYLE - Use constructor injection:

@Service
public class ModernService {
    private final UserRepository userRepository;
    
    public ModernService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }
}