---
type: "THEORY"
title: "ResponseEntity - Better Control"
---

Return ResponseEntity for more control over HTTP response:

@GetMapping("/{id}")
public ResponseEntity<User> getUserById(@PathVariable Long id) {
    User user = findUserById(id);
    
    if (user == null) {
        return ResponseEntity.notFound().build();  // 404
    }
    
    return ResponseEntity.ok(user);  // 200 with body
}

@PostMapping
public ResponseEntity<User> createUser(@RequestBody User user) {
    User saved = userService.save(user);
    return ResponseEntity
        .status(HttpStatus.CREATED)  // 201
        .body(saved);
}

BENEFITS:
- Set custom status codes
- Add headers
- Full control over response