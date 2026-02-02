---
type: "THEORY"
title: "Understanding Relations"
---

Relations define how database tables connect to each other. Serverpod supports three types of relationships:

**1. One-to-One Relationship**

Each record in Table A relates to exactly one record in Table B, and vice versa.

Example: User and UserProfile - each user has exactly one profile.

**2. One-to-Many Relationship**

One record in Table A can relate to many records in Table B, but each record in Table B relates to only one record in Table A.

Example: User and Post - one user can have many posts, but each post belongs to one user.

**3. Many-to-Many Relationship**

Records in Table A can relate to many records in Table B, and vice versa.

Example: Post and Tag - one post can have many tags, and one tag can be on many posts.

**Defining Relations in YAML:**

Relations are defined by adding special field types that reference other models.

