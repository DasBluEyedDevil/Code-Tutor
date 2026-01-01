---
type: "KEY_POINT"
title: "Spring Security is Like a Building with Security Guards"
---

BUILDING WITHOUT SECURITY:
- Anyone walks in through any door
- Access CEO's office? Sure!
- Delete company files? Go ahead!
- No one knows who did what

BUILDING WITH SECURITY:
1. LOBBY (Public Area):
   - Anyone can enter
   - Read brochures, see directory
   - = Public endpoints (/login, /register)

2. BADGE CHECK (Authentication):
   - Show your ID badge
   - Verify you are who you claim
   - = Username + Password

3. ACCESS LEVELS (Authorization):
   - Green badge: Employee areas only
   - Blue badge: Manager areas
   - Red badge: Executive areas
   - = Roles: USER, MANAGER, ADMIN

4. SECURITY LOG:
   - Record who entered when
   - Track actions taken
   - = Audit logging

Spring Security = The entire security system!