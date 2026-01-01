---
type: "THEORY"
title: "The Problem: Classes That Create Their Own Dependencies"
---

Without dependency injection:

public class UserController {
    private UserService userService = new UserService();  // BAD!
    
    public User getUser(Long id) {
        return userService.findById(id);
    }
}

PROBLEMS:
❌ Tight Coupling - UserController is glued to UserService
❌ Hard to Test - Can't replace UserService with a mock
❌ Inflexible - Can't swap implementations
❌ Manual Lifecycle - You manage object creation

What if UserService needs a database connection?
Now UserController must manage that too!

Solution: DEPENDENCY INJECTION (DI)