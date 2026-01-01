---
type: "EXAMPLE"
title: "User Model with Relations"
---


**Extending the User Model**

Add relationship tracking for friends and blocked users:



```yaml
# server/lib/src/protocol/user_relationship.yaml
class: UserRelationship
table: user_relationships
fields:
  # The user who initiated the relationship
  userId: int, relation(parent=user_profiles)
  
  # The target user
  targetUserId: int, relation(parent=user_profiles, field=targetUser)
  
  # Relationship type
  relationshipType: RelationshipType
  
  # Request status for friend requests
  status: RelationshipStatus
  
  # Timestamps
  createdAt: DateTime
  updatedAt: DateTime?

indexes:
  user_relationship_unique_idx:
    fields: userId, targetUserId
    unique: true
  user_relationship_user_idx:
    fields: userId
  user_relationship_target_idx:
    fields: targetUserId

---

# server/lib/src/protocol/relationship_type.yaml
enum: RelationshipType
values:
  - friend
  - blocked
  - muted

---

# server/lib/src/protocol/relationship_status.yaml
enum: RelationshipStatus
values:
  - pending    # Friend request sent
  - accepted   # Friend request accepted
  - rejected   # Friend request rejected
  - active     # For block/mute (immediately active)
```
