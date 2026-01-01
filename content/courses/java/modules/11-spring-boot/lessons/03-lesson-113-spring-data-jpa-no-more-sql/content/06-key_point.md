---
type: "KEY_POINT"
title: "Service Layer Pattern"
---

BEST PRACTICE: Controller → Service → Repository

@RestController
public class UserController {
    @Autowired
    private UserService userService;  // Not Repository!
    
    @GetMapping("/api/users")
    public List<User> getAll() {
        return userService.getAllUsers();
    }
}

@Service
public class UserService {
    @Autowired
    private UserRepository userRepository;
    
    public List<User> getAllUsers() {
        return userRepository.findAll();
    }
    
    public User createUser(User user) {
        // Business logic here
        validateUser(user);
        return userRepository.save(user);
    }
}

WHY?
- Separation of concerns
- Business logic in service, not controller
- Easier to test
- Reusable services