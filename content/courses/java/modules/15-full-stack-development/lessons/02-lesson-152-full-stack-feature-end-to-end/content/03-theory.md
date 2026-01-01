---
type: "THEORY"
title: "Step 2: Backend Service and Controller"
---

@Service
public class UserService {
    @Autowired
    private UserRepository userRepository;
    
    public List<User> getAllUsers() {
        return userRepository.findAll();
    }
    
    public User createUser(User user) {
        return userRepository.save(user);
    }
}

@RestController
@RequestMapping("/api/users")
@CrossOrigin(origins = "*")  // Allow frontend to call API
public class UserController {
    @Autowired
    private UserService userService;
    
    @GetMapping
    public List<User> getAll() {
        return userService.getAllUsers();
    }
    
    @PostMapping
    public User create(@RequestBody User user) {
        return userService.createUser(user);
    }
}

Now: http://localhost:8080/api/users returns JSON