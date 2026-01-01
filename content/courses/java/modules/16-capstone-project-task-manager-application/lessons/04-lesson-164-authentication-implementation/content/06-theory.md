---
type: "THEORY"
title: "AuthService and Password Encoding with BCrypt"
---

The AuthService implements authentication business logic. It uses BCrypt for secure password hashing and JwtService for token generation.

```java
// com/taskmanager/service/AuthService.java
package com.taskmanager.service;

import com.taskmanager.dto.request.LoginRequest;
import com.taskmanager.dto.request.RegisterRequest;
import com.taskmanager.dto.response.AuthResponse;
import com.taskmanager.exception.DuplicateResourceException;
import com.taskmanager.model.User;
import com.taskmanager.model.enums.Role;
import com.taskmanager.repository.UserRepository;
import com.taskmanager.security.JwtService;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
@Transactional
public class AuthService {

    private final UserRepository userRepository;
    private final PasswordEncoder passwordEncoder;
    private final JwtService jwtService;
    private final AuthenticationManager authenticationManager;

    public AuthService(UserRepository userRepository,
                       PasswordEncoder passwordEncoder,
                       JwtService jwtService,
                       AuthenticationManager authenticationManager) {
        this.userRepository = userRepository;
        this.passwordEncoder = passwordEncoder;
        this.jwtService = jwtService;
        this.authenticationManager = authenticationManager;
    }

    public AuthResponse register(RegisterRequest request) {
        // Check if email already exists
        if (userRepository.existsByEmail(request.getEmail())) {
            throw new DuplicateResourceException(
                "Email already registered: " + request.getEmail());
        }

        // Create new user with hashed password
        User user = new User();
        user.setEmail(request.getEmail().toLowerCase().trim());
        user.setPassword(passwordEncoder.encode(request.getPassword()));
        user.setName(request.getName().trim());
        user.setRole(Role.USER);

        User savedUser = userRepository.save(user);

        // Generate JWT token
        String token = jwtService.generateToken(savedUser);

        return new AuthResponse(token, savedUser.getEmail(), savedUser.getName());
    }

    public AuthResponse login(LoginRequest request) {
        try {
            // AuthenticationManager uses UserDetailsService and PasswordEncoder
            authenticationManager.authenticate(
                new UsernamePasswordAuthenticationToken(
                    request.getEmail().toLowerCase().trim(),
                    request.getPassword()
                )
            );
        } catch (BadCredentialsException e) {
            throw new BadCredentialsException("Invalid email or password");
        }

        // If authentication succeeds, load user and generate token
        User user = userRepository.findByEmail(request.getEmail().toLowerCase())
                .orElseThrow(() -> new BadCredentialsException("Invalid email or password"));

        String token = jwtService.generateToken(user);

        return new AuthResponse(token, user.getEmail(), user.getName());
    }
}

// com/taskmanager/exception/DuplicateResourceException.java
package com.taskmanager.exception;

public class DuplicateResourceException extends RuntimeException {
    public DuplicateResourceException(String message) {
        super(message);
    }
}
```

BCrypt Password Security:
BCrypt is the industry standard for password hashing:
- One-way hash: Cannot reverse to get original password
- Salt built-in: Each hash includes random salt, preventing rainbow table attacks
- Work factor: Configurable computational cost (default 10) makes brute force expensive
- Unique hashes: Same password produces different hash each time

Never store plain text passwords! Always use passwordEncoder.encode() before saving and let AuthenticationManager handle comparison during login.