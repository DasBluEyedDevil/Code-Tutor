@PostMapping("/refresh")
public ResponseEntity<AuthResponse> refreshToken(
        @AuthenticationPrincipal User user) {
    // User is already authenticated (JWT was valid)
    // Generate new token with fresh expiration
    String newToken = jwtService.generateToken(user);
    
    return ResponseEntity.ok(
        new AuthResponse(newToken, user.getEmail(), user.getName()));
}

// Note: You'll need to inject JwtService into AuthController:
// private final JwtService jwtService;
// 
// public AuthController(AuthService authService, JwtService jwtService) {
//     this.authService = authService;
//     this.jwtService = jwtService;
// }