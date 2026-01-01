---
type: "THEORY"
title: "Exercise: Refresh Token System"
---


Implement a refresh token mechanism for better security and UX.

### Background

Current system has a problem:
- Tokens expire after 1 hour
- User must login again every hour (poor UX)
- Longer expiration times are less secure

**Solution**: Two-token system:
- **Access Token**: Short-lived (15 minutes), used for API requests
- **Refresh Token**: Long-lived (7 days), used to get new access tokens

### Requirements

1. **Update Login Response**:
   - Return both `accessToken` and `refreshToken`
   - Access token expires in 15 minutes
   - Refresh token expires in 7 days

2. **Create Refresh Endpoint**:
   - `POST /api/auth/refresh`
   - Accepts: `{ "refreshToken": "..." }`
   - Returns: New access token (and optionally new refresh token)

3. **Store Refresh Tokens**:
   - Create `RefreshTokens` table
   - Fields: id, userId, token, expiresAt, createdAt
   - Each user can have multiple refresh tokens (different devices)

4. **Revocation Support**:
   - `POST /api/auth/logout` - Delete refresh token
   - `POST /api/auth/logout-all` - Delete all user's refresh tokens

5. **Security Requirements**:
   - Refresh tokens must be stored hashed (like passwords)
   - Each refresh token can be used only once (rotation)
   - Expired tokens are automatically invalid

### Starter Code


---



```kotlin
@Serializable
data class LoginResponse(
    val accessToken: String,
    val refreshToken: String,
    val user: User,
    val accessTokenExpiresIn: Long,   // 15 minutes
    val refreshTokenExpiresIn: Long,  // 7 days
    val message: String = "Login successful"
)

@Serializable
data class RefreshRequest(
    val refreshToken: String
)

@Serializable
data class RefreshResponse(
    val accessToken: String,
    val refreshToken: String,
    val accessTokenExpiresIn: Long
)

// TODO: Create RefreshTokens table
// TODO: Implement refresh token generation and validation
// TODO: Implement refresh endpoint
// TODO: Implement logout endpoints
```
