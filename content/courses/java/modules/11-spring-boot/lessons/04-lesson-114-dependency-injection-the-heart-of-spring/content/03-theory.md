---
type: "THEORY"
title: "Spring Boot 4 Best Practice: Constructor Injection"
---

Mark classes as Spring beans:

@Service
public class UserService {
    private final UserRepository userRepository;
    
    // Constructor injection (BEST PRACTICE)
    public UserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }
    
    public User findById(Long id) {
        return userRepository.findById(id).orElse(null);
    }
}

Then inject into controllers:

@RestController
@RequestMapping("/api/users")
public class UserController {
    private final UserService userService;
    
    // Spring automatically injects UserService
    // No @Autowired needed for single constructor!
    public UserController(UserService userService) {
        this.userService = userService;
    }
    
    @GetMapping("/{id}")
    public User getUser(@PathVariable Long id) {
        return userService.findById(id);
    }
}

The full stack:
Controller → Service → Repository → Database
Spring wires this chain automatically!