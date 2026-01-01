---
type: "THEORY"
title: "Why Constructor Injection with 'final'?"
---

@Service
public class OrderService {
    private final UserService userService;      // final = immutable
    private final PaymentService paymentService;
    
    public OrderService(UserService userService, 
                       PaymentService paymentService) {
        this.userService = userService;
        this.paymentService = paymentService;
    }
}

BENEFITS OF 'final':
✓ Guarantees field is set exactly once
✓ Thread-safe
✓ Makes dependencies explicit
✓ Prevents accidental reassignment

When Spring starts:
1. Scans for @Component, @Service, etc.
2. Creates instances
3. Calls constructors with required dependencies
4. Stores beans in Application Context