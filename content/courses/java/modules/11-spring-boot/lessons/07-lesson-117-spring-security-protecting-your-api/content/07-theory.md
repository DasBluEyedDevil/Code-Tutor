---
type: "THEORY"
title: "Database-Based Authentication"
---

For production, load users from database:

1. User Entity:

@Entity
@Table(name = "users")
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @Column(unique = true, nullable = false)
    private String username;
    
    @Column(nullable = false)
    private String password;  // Hashed!
    
    private String role;  // USER, ADMIN, etc.
    
    // Getters, setters
}

2. Custom UserDetailsService:

@Service
public class CustomUserDetailsService implements UserDetailsService {
    
    @Autowired
    private UserRepository userRepository;
    
    @Override
    public UserDetails loadUserByUsername(String username) 
            throws UsernameNotFoundException {
        User user = userRepository.findByUsername(username)
            .orElseThrow(() -> new UsernameNotFoundException(
                "User not found: " + username));
        
        return org.springframework.security.core.userdetails.User
            .builder()
            .username(user.getUsername())
            .password(user.getPassword())  // Already hashed
            .roles(user.getRole())
            .build();
    }
}

Spring automatically calls loadUserByUsername() during login!