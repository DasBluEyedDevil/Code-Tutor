---
type: "THEORY"
title: "Method-Level Security"
---

URL-based security isn't always enough:

@Service
public class UserService {
    
    public User getUser(Long id) {
        // Anyone authenticated can call this method
        // But should manager only see their team?
        // Should users only see themselves?
    }
}

Method security provides fine-grained control:

@Configuration
@EnableMethodSecurity  // Enable method-level security
public class SecurityConfig { }

Now use annotations on methods:

@PreAuthorize("hasRole('ADMIN')")
public void deleteUser(Long id) { }

@PreAuthorize("hasAuthority('report:view')")
public Report generateReport() { }

@PostAuthorize("returnObject.owner == authentication.name")
public Document getDocument(Long id) { }

@PreFilter and @PostFilter for collections.