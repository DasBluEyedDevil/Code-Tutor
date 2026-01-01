---
type: "WARNING"
title: "Session Security Checklist"
---

Before deploying, verify:

[] Cookie Flags:
   - HttpOnly: Prevents XSS stealing cookies
   - Secure: HTTPS only
   - SameSite=Strict or Lax

application.properties:
server.servlet.session.cookie.http-only=true
server.servlet.session.cookie.secure=true
server.servlet.session.cookie.same-site=strict

[] Session Timeout:
   - Set appropriate timeout
   - Balance security vs user experience

server.servlet.session.timeout=30m

[] Session Fixation:
   - Generate new ID on login (Spring default)

[] Logout:
   - Invalidate session completely
   - Clear remember-me tokens

.logout(logout -> logout
    .logoutUrl("/logout")
    .invalidateHttpSession(true)
    .clearAuthentication(true)
    .deleteCookies("JSESSIONID", "remember-me")
)

[] CSRF:
   - Enabled for browser-based apps
   - Token in forms and AJAX