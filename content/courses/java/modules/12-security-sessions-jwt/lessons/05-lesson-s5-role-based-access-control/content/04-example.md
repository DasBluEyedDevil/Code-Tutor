---
type: "EXAMPLE"
title: "@PreAuthorize Expressions"
---

SpEL (Spring Expression Language) for complex authorization:

```java
@Service
public class DocumentService {

    // Simple role check
    @PreAuthorize("hasRole('ADMIN')")
    public void deleteAllDocuments() { }

    // Multiple roles (OR)
    @PreAuthorize("hasRole('ADMIN') or hasRole('MANAGER')")
    public List<Document> getAllDocuments() { }

    // Permission-based
    @PreAuthorize("hasAuthority('document:write')")
    public Document createDocument(Document doc) { }

    // Access method parameters
    @PreAuthorize("#userId == authentication.principal.id")
    public User getUser(Long userId) { }

    // Complex expression - only own documents or admin
    @PreAuthorize("#document.ownerId == authentication.principal.id " +
                  "or hasRole('ADMIN')")
    public void updateDocument(@P("document") Document document) { }

    // Check returned value (runs AFTER method)
    @PostAuthorize("returnObject.owner == authentication.name " +
                   "or hasRole('ADMIN')")
    public Document getDocument(Long id) {
        return documentRepository.findById(id).orElseThrow();
    }

    // Filter collections - only return permitted items
    @PostFilter("filterObject.department == authentication.principal.department " +
                "or hasRole('ADMIN')")
    public List<Document> searchDocuments(String query) {
        return documentRepository.search(query);  // Filter applied after
    }

    // Custom security method
    @PreAuthorize("@documentSecurity.canAccess(authentication, #id)")
    public Document getSecureDocument(Long id) { }
}

// Custom security bean
@Component("documentSecurity")
public class DocumentSecurityService {
    
    public boolean canAccess(Authentication auth, Long documentId) {
        // Complex business logic
        Document doc = documentRepository.findById(documentId).orElse(null);
        if (doc == null) return false;
        
        User user = (User) auth.getPrincipal();
        return doc.getOwnerId().equals(user.getId()) ||
               doc.getSharedWith().contains(user.getId()) ||
               user.getRoles().contains("ADMIN");
    }
}
```
