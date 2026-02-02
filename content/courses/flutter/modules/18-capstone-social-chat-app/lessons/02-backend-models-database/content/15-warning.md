---
type: "WARNING"
title: "Common Database Design Mistakes"
---


**1. Missing Indexes on Foreign Keys**

Problem: Queries on relations become slow as data grows.

```yaml
# BAD - No index on foreign key
fields:
  conversationId: int, relation(parent=conversations)

# GOOD - Index for faster lookups
fields:
  conversationId: int, relation(parent=conversations)
indexes:
  message_conversation_idx:
    fields: conversationId
```

**2. Storing Computed Values Without Updates**

Problem: Denormalized counts become stale.

Solution: Update counts in the same transaction as the operation:

```dart
// When adding a like, also increment count
await session.db.transaction((transaction) async {
  await PostLike.db.insertRow(session, like);
  await Post.db.updateRow(session, post.copyWith(
    likeCount: post.likeCount + 1,
  ));
});
```

**3. N+1 Query Problems**

Problem: Fetching related data one at a time.

```dart
// BAD - N+1 queries
final messages = await Message.db.find(session);
for (final message in messages) {
  final sender = await UserProfile.db.findById(
    session, 
    message.senderId,
  );
}

// GOOD - Include relation in query
final messages = await Message.db.find(
  session,
  include: Message.include(sender: UserProfile.include()),
);
```

**4. Not Using Transactions**

Problem: Partial updates when operations fail.

Solution: Wrap related operations in transactions:

```dart
await session.db.transaction((transaction) async {
  // All or nothing
  await Message.db.insertRow(session, message);
  await Conversation.db.updateRow(session, conversation);
  await Participant.db.updateRow(session, participant);
});
```

**5. Exposing Internal IDs in URLs**

Problem: Sequential IDs reveal information and enable enumeration.

Solution: Use UUIDs for public-facing identifiers:

```yaml
fields:
  id: int          # Internal, auto-increment
  publicId: String  # UUID for external use
```

