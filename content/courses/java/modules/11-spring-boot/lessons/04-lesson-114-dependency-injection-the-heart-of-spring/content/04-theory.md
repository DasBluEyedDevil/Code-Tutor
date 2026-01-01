---
type: "THEORY"
title: "Spring Stereotype Annotations"
---

These tell Spring to manage a class as a bean:

@Component - Generic bean (utility classes)
@Service - Business logic layer
@Repository - Data access layer
@Controller - Web MVC controller (returns HTML)
@RestController - REST API controller (returns JSON)

Example:

@Service
public class EmailService {
    // Spring manages this
}

@Repository
public interface UserRepository extends JpaRepository<User, Long> {
    // Spring implements this
}

All of these are variations of @Component!