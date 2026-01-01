---
type: "WARNING"
title: "Authorization Security Pitfalls"
---

MISTAKE 1: Client-Side Only Authorization
// Frontend hides admin button
{isAdmin && <AdminButton />}

// But API is unprotected!
// Attacker directly calls /api/admin/delete

FIX: ALWAYS enforce on server side.

MISTAKE 2: Role in JWT Without Verification
// Token contains roles
{"roles": ["USER"]}

// User modifies to:
{"roles": ["ADMIN"]}

// If signature not verified, user is now admin!

FIX: Validate signature; consider fetching roles from DB.

MISTAKE 3: Insecure Direct Object Reference (IDOR)
// Get document by ID
@GetMapping("/documents/{id}")
public Document getDocument(@PathVariable Long id) {
    return documentRepo.findById(id);  // No ownership check!
}

// User A can access User B's documents by guessing IDs

FIX: Always verify ownership or permission.
@PostAuthorize("returnObject.owner == authentication.name")

MISTAKE 4: Missing Authorization on Modifications
// Checks permission to view
@PreAuthorize("hasAuthority('doc:read')")
public Document getDoc(Long id) { }

// Forgets to check permission to modify!
public void updateDoc(Document doc) { }  // No check!

FIX: Apply appropriate authorization to ALL sensitive methods.