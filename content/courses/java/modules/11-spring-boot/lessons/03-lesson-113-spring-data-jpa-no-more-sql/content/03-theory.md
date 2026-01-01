---
type: "THEORY"
title: "Creating a Repository"
---

Extend JpaRepository - Spring does the rest:

public interface UserRepository extends JpaRepository<User, Long> {
    // That's it! Spring generates implementation
}

FREE METHODS (no code needed):
- findAll() → SELECT * FROM users
- findById(id) → SELECT * FROM users WHERE id = ?
- save(user) → INSERT or UPDATE
- deleteById(id) → DELETE FROM users WHERE id = ?
- count() → SELECT COUNT(*) FROM users
- existsById(id) → Check if exists

Usage in service:

@Service
public class UserService {
    @Autowired
    private UserRepository userRepository;
    
    public List<User> getAllUsers() {
        return userRepository.findAll();
    }
    
    public User saveUser(User user) {
        return userRepository.save(user);
    }
}