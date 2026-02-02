---
type: "EXAMPLE"
title: "One-to-One Relationship"
---

Here is how to define a one-to-one relationship between User and UserProfile:



```yaml
# lib/src/protocol/user.yaml
class: User
table: users
fields:
  email: String
  username: String

# lib/src/protocol/user_profile.yaml  
class: UserProfile
table: user_profiles
fields:
  # Foreign key reference to User
  userId: int
  user: User?, relation(field: userId)
  
  # Profile-specific fields
  fullName: String?
  bio: String?
  website: String?
  location: String?
  birthDate: DateTime?
  
# The relation(field: userId) tells Serverpod:
# - userId stores the foreign key value
# - user is the related User object (loaded via include)

# Usage in endpoints:
# Get profile with user included
final profile = await UserProfile.db.findFirstRow(
  session,
  where: (t) => t.userId.equals(userId),
  include: UserProfile.include(
    user: User.include(),
  ),
);

# Now profile.user contains the User object
print(profile?.user?.username);
```
