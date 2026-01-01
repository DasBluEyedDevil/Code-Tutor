---
type: "THEORY"
title: "Exercise 1: Build a Shared Authentication Module"
---


Create a KMP module that handles user authentication across platforms.

### Requirements

1. **Shared Models** (commonMain):
   - `User` (id, email, name, token)
   - `LoginRequest` (email, password)
   - `RegisterRequest` (email, password, name)
   - `AuthResponse` (success, user, token, message)

2. **Shared AuthService** (commonMain):
   - `login(email, password): Result<User>`
   - `register(request): Result<User>`
   - `logout()`
   - `isLoggedIn(): Boolean`
   - `getCurrentUser(): User?`

3. **Platform-Specific Storage**:
   - Android: Use SharedPreferences
   - iOS: Use UserDefaults
   - Store and retrieve auth token

4. **Validation**:
   - Email validation
   - Password strength check (min 8 chars, 1 uppercase, 1 number)

---

