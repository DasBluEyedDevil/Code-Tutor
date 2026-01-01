---
type: "KEY_POINT"
title: "Session Security Concerns"
---

Sessions have specific vulnerabilities:

SESSION HIJACKING:
If attacker gets your session cookie, they become you.

Protections:
- HttpOnly: JavaScript can't read cookie
- Secure: Cookie only sent over HTTPS
- SameSite: Prevents cross-site cookie sending

Set-Cookie: JSESSIONID=ABC123; HttpOnly; Secure; SameSite=Strict

SESSION FIXATION:
Attacker sets YOUR session ID before you log in,
then uses same ID after you authenticate.

Protection: Generate NEW session ID on login.

Spring Security does this automatically:
http.sessionManagement(session -> session
    .sessionFixation().changeSessionId()  // Default!
)

SESSION TIMEOUT:
Sessions should expire after inactivity.

application.properties:
server.servlet.session.timeout=30m  # 30 minutes