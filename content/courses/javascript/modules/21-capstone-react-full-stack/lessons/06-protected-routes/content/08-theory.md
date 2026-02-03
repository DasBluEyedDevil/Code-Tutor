---
type: "THEORY"
title: "Challenge: Token Refresh Flow"
---

## Challenge: Implement Token Refresh

Expanding on the authentication system, implement a token refresh mechanism:

**Requirements:**
1. **Track token expiration** using JWT decode library
2. **Auto-refresh tokens** before they expire using a refresh endpoint
3. **Handle refresh failures** by logging out the user
4. **Queue API requests** during token refresh to prevent race conditions

**Implementation Steps:**
1. Decode JWT token to get expiration time
2. Create a `useRefreshToken` hook that:
   - Checks if token is expiring soon (within 5 minutes)
   - Calls `/api/auth/refresh` to get new token
   - Updates token in localStorage and context
3. Update `fetchWithAuth` to automatically refresh tokens
4. Handle the edge case where refresh also fails (force logout)

**Bonus Challenge:**
- Implement silent token refresh in the background using useEffect
- Add a "Your session is expiring" warning modal (1 min before expiration)
- Allow users to extend their session by clicking a button
- Create an axios interceptor pattern for cleaner token management