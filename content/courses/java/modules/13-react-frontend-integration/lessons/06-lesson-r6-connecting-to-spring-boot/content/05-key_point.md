---
type: "KEY_POINT"
title: "Spring Boot JWT Endpoint"
---

The Spring Boot side for JWT authentication:

@RestController
@RequestMapping("/api/auth")
public class AuthController {
    
    private final AuthenticationManager authManager;
    private final JwtService jwtService;
    private final UserService userService;
    
    @PostMapping("/login")
    public ResponseEntity<?> login(@RequestBody LoginRequest request) {
        try {
            // Authenticate credentials
            Authentication auth = authManager.authenticate(
                new UsernamePasswordAuthenticationToken(
                    request.getEmail(), 
                    request.getPassword()
                )
            );
            
            // Generate JWT
            UserDetails user = (UserDetails) auth.getPrincipal();
            String token = jwtService.generateToken(user);
            
            return ResponseEntity.ok(new AuthResponse(token, user));
            
        } catch (BadCredentialsException e) {
            return ResponseEntity.status(401)
                .body(Map.of("message", "Invalid email or password"));
        }
    }
    
    @GetMapping("/me")
    public ResponseEntity<?> getCurrentUser(
            @AuthenticationPrincipal UserDetails user) {
        if (user == null) {
            return ResponseEntity.status(401)
                .body(Map.of("message", "Not authenticated"));
        }
        return ResponseEntity.ok(userService.findByEmail(user.getUsername()));
    }
}

EXPECTED RESPONSES:

POST /api/auth/login (success):
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "user": { "id": 1, "email": "john@example.com", "name": "John" }
}

POST /api/auth/login (failure):
{
    "message": "Invalid email or password"
}