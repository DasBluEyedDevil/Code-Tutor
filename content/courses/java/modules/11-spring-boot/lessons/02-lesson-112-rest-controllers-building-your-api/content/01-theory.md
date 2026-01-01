---
type: "THEORY"
title: "Creating a Complete REST API"
---

Let's build a User API with full CRUD operations:

@RestController
@RequestMapping("/api/users")
public class UserController {
    
    private List<User> users = new ArrayList<>();
    
    // GET all users
    @GetMapping
    public List<User> getAllUsers() {
        return users;
    }
    
    // GET single user by ID
    @GetMapping("/{id}")
    public User getUserById(@PathVariable Long id) {
        return users.stream()
            .filter(u -> u.getId().equals(id))
            .findFirst()
            .orElse(null);
    }
    
    // POST create new user
    @PostMapping
    public User createUser(@RequestBody User user) {
        users.add(user);
        return user;
    }
    
    // PUT update user
    @PutMapping("/{id}")
    public User updateUser(@PathVariable Long id, @RequestBody User updated) {
        // Find and update user
        return updated;
    }
    
    // DELETE user
    @DeleteMapping("/{id}")
    public void deleteUser(@PathVariable Long id) {
        users.removeIf(u -> u.getId().equals(id));
    }
}