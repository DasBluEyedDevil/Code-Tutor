---
type: "THEORY"
title: "Why Spring Security?"
---

You COULD implement security yourself:

// DIY Authentication (DON'T DO THIS)
@PostMapping("/login")
public String login(String username, String password) {
    User user = userRepo.findByUsername(username);
    if (user != null && user.getPassword().equals(password)) {
        session.setAttribute("user", user);
        return "redirect:/dashboard";
    }
    return "redirect:/login?error";
}

PROBLEMS:
- Plain text password comparison
- No brute force protection
- No session fixation protection
- No CSRF protection
- No remember-me functionality
- No logout handling
- No concurrent session control

Spring Security provides ALL of this out of the box:
- Battle-tested by millions of applications
- Updated for new vulnerabilities
- Integrates with Spring ecosystem
- Extensible for custom requirements

Don't reinvent the wheel. Security is too important to get wrong.