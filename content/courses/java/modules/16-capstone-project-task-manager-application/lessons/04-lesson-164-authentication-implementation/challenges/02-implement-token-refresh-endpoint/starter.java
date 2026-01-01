// Add this method to AuthController

@PostMapping("/refresh")
public ResponseEntity<AuthResponse> refreshToken(
        @AuthenticationPrincipal User user) {
    // TODO: Implement token refresh
}