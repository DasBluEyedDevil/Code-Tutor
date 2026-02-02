---
type: "EXAMPLE"
title: "Optional in Practice"
---

Using Optional effectively in real code:

```java
import java.util.*;

record User(int id, String name, String email) {}

void main() {
    Map<Integer, User> users = Map.of(
        1, new User(1, "Alice", "alice@example.com"),
        2, new User(2, "Bob", "bob@example.com")
    );
    
    // Method returning Optional
    Optional<User> findUser(int id) {
        return Optional.ofNullable(users.get(id));
    }
    
    // orElse: provide default
    User user1 = findUser(1).orElse(new User(0, "Guest", "guest@example.com"));
    IO.println(user1.name());  // Alice
    
    User user99 = findUser(99).orElse(new User(0, "Guest", "guest@example.com"));
    IO.println(user99.name());  // Guest
    
    // orElseThrow: fail fast for required values
    try {
        User required = findUser(99).orElseThrow(
            () -> new RuntimeException("User not found")
        );
    } catch (RuntimeException e) {
        IO.println("Error: " + e.getMessage());
    }
    
    // map: transform if present
    Optional<String> email = findUser(1).map(User::email);
    email.ifPresent(e -> IO.println("Email: " + e));
    
    // filter: conditional extraction
    Optional<User> alice = findUser(1)
        .filter(u -> u.name().equals("Alice"));
    IO.println("Is Alice? " + alice.isPresent());  // true
    
    // flatMap: when the mapper returns Optional
    Optional<String> getEmailDomain(int userId) {
        return findUser(userId)
            .map(User::email)
            .flatMap(email -> {
                int at = email.indexOf('@');
                return at > 0 
                    ? Optional.of(email.substring(at + 1)) 
                    : Optional.empty();
            });
    }
    IO.println(getEmailDomain(1).orElse("N/A"));  // example.com
}
```
