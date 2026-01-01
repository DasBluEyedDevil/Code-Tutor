---
type: "THEORY"
title: "The Problem: Anyone Can Access Everything"
---

Without security, your API is wide open:

@GetMapping("/admin/users")
public List<User> getAllUsers() {
    return userRepository.findAll();
}

@DeleteMapping("/admin/users/{id}")
public void deleteUser(@PathVariable Long id) {
    userRepository.deleteById(id);
}

PROBLEMS:
❌ No login required - anyone can access
❌ No user identification - can't track who did what
❌ No permissions - regular users can delete data
❌ No protection - hackers can wreak havoc

Real applications need:
✓ AUTHENTICATION - Who are you? (Login)
✓ AUTHORIZATION - What can you do? (Permissions)
✓ Protection from attacks (CSRF, XSS, etc.)

Solution: SPRING SECURITY