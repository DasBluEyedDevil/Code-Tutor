---
type: "THEORY"
title: "JWT Security"
---


### Secure JWT Implementation

❌ **Bad** (Insecure):

✅ **Good** (Secure):

### Refresh Tokens


---



```kotlin
@Entity
data class RefreshToken(
    @PrimaryKey val id: String = UUID.randomUUID().toString(),
    val userId: String,
    val token: String,
    val expiresAt: Long,
    val createdAt: Long = System.currentTimeMillis()
)

object TokenService {
    private const val REFRESH_TOKEN_EXPIRATION = 7 * 24 * 3600000L // 7 days

    fun generateTokenPair(user: User): TokenPair {
        val accessToken = JwtConfig.generateToken(user)

        val refreshToken = RefreshToken(
            userId = user.id,
            token = generateSecureRandomToken(),
            expiresAt = System.currentTimeMillis() + REFRESH_TOKEN_EXPIRATION
        )

        refreshTokenRepository.save(refreshToken)

        return TokenPair(accessToken, refreshToken.token)
    }

    suspend fun refreshAccessToken(refreshToken: String): String? {
        val token = refreshTokenRepository.findByToken(refreshToken) ?: return null

        if (token.expiresAt < System.currentTimeMillis()) {
            refreshTokenRepository.delete(token.id)
            return null
        }

        val user = userRepository.findById(token.userId) ?: return null

        return JwtConfig.generateToken(user)
    }

    private fun generateSecureRandomToken(): String {
        val bytes = ByteArray(32)
        SecureRandom().nextBytes(bytes)
        return bytes.joinToString("") { "%02x".format(it) }
    }
}

data class TokenPair(
    val accessToken: String,
    val refreshToken: String
)
```
