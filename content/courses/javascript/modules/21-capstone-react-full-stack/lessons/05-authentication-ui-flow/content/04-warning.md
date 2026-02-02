---
type: "WARNING"
title: "localStorage Security Considerations"
---

**Important Security Note:**

localStorage is convenient for learning and development, but has significant security implications:

**XSS (Cross-Site Scripting) Vulnerability:**
- Any injected JavaScript can access localStorage
- Tokens stored in localStorage are vulnerable to XSS attacks
- Example: `localStorage.getItem('token')` is accessible to any script on the page

**Better Alternatives for Production:**

1. **HTTP-Only Cookies (Recommended):**
   ```typescript
   // Server sets secure, HTTP-only cookie
   response.setHeader('Set-Cookie', 'token=xyz; HttpOnly; Secure; SameSite=Strict');

   // Browser automatically includes in requests
   // JavaScript cannot access it (protected from XSS)
   ```

2. **Session Storage (Short-lived):**
   - `sessionStorage` - cleared when tab closes
   - Better than localStorage, but still vulnerable to XSS
   - Good for single-session tokens

3. **Memory Storage (Most Secure):**
   - Store tokens in React state or context
   - Lost on page refresh
   - Never exposed to XSS

**When localStorage is Acceptable:**
- Learning projects and tutorials (this course)
- Development environments
- Demo applications
- When handling non-sensitive data

**For Production:**
- Use HTTP-only cookies for auth tokens
- Implement CSRF protection
- Validate tokens on every request
- Consider token expiration and refresh strategies