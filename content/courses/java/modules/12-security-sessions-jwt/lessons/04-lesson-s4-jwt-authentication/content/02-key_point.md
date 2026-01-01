---
type: "KEY_POINT"
title: "JWT Structure"
---

JWT (JSON Web Token) has three parts:

HEADER.PAYLOAD.SIGNATURE

HEADER (Algorithm & Type):
{
  "alg": "HS256",
  "typ": "JWT"
}
Base64URL encoded: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9

PAYLOAD (Claims - the data):
{
  "sub": "user-123",           // Subject (user ID)
  "name": "John Doe",
  "roles": ["USER", "ADMIN"],
  "iat": 1516239022,           // Issued At
  "exp": 1516242622            // Expiration
}
Base64URL encoded: eyJzdWIiOiJ1c2VyLTEyMyIsIm5hbWUiOiJKb2huIERvZSJ9

SIGNATURE:
HMAC-SHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret
)

FULL TOKEN:
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.
eyJzdWIiOiJ1c2VyLTEyMyIsIm5hbWUiOiJKb2huIERvZSJ9.
SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c

CAUTION: Payload is ENCODED, not ENCRYPTED.
Anyone can decode and read it. Never put secrets in JWT!