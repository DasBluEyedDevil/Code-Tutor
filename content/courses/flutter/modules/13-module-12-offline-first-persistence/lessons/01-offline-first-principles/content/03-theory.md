---
type: "THEORY"
title: "Sync Strategies Comparison"
---


**1. Last-Write-Wins (LWW)**
- Simplest approach
- Latest timestamp wins conflicts
- Risk: Overwrites valid changes
- Use case: Simple settings, preferences

**2. Server-Wins**
- Server always has priority
- Local changes discarded on conflict
- Use case: Read-heavy data, reference data

**3. Client-Wins**
- Local changes always persist
- Server adapts to client
- Use case: User-generated content drafts

**4. Merge Strategy**
- Field-level conflict resolution
- Combines changes intelligently
- Use case: Collaborative documents

**5. Operational Transform (OT)**
- Transform operations for consistency
- Real-time collaboration
- Use case: Google Docs-style editing

**6. CRDTs (Conflict-free Replicated Data Types)**
- Mathematically guaranteed merge
- No conflicts possible
- Use case: Distributed systems, collaborative apps

**Choosing a Strategy:**
| Data Type | Recommended Strategy |
|-----------|---------------------|
| User settings | Last-Write-Wins |
| Reference data | Server-Wins |
| Notes/drafts | Client-Wins |
| Shared documents | Merge or CRDTs |

