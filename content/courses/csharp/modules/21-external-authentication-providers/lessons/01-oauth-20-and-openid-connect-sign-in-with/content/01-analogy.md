---
type: "ANALOGY"
title: "Understanding OAuth"
---

Imagine you arrive at a luxury hotel and want to use the valet parking service. You hand the valet attendant a special key - not your full key ring with your house keys, car alarm fob, and gym locker key - just the valet key. This limited key can start the car and open the doors, but it cannot open the trunk where your valuables are stored, and it cannot access the glove compartment. You are delegating LIMITED access to your car without giving full control.

OAuth works exactly the same way. When you click 'Sign in with Google', you are not giving the application your Google password. Instead, you are telling Google: 'Give this application a valet key - let them know who I am and maybe access my basic profile, but do not let them read my private emails or delete my files.'

The beauty of this system is trust delegation. You trust Google to verify your identity (they already know you from your Gmail login). The application trusts Google's verification (Google is a well-known identity provider). And you maintain control over what information and actions you are delegating - these are called SCOPES. Just like a valet key limits what the attendant can do with your car, OAuth scopes limit what the application can access.

If you later decide you no longer trust the application, you can revoke access from your Google account settings - like taking back the valet key. The application loses access immediately without you needing to change any passwords.