---
type: "EXAMPLE"
title: "Database Seeding Script"
---


**Creating Test Data**

Create a seeding script for development:



```dart
// server/lib/src/util/seed_database.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

/// Seeds the database with test data for development
class DatabaseSeeder {
  final Session session;
  
  DatabaseSeeder(this.session);
  
  /// Run all seeders
  Future<void> seedAll() async {
    print('Starting database seeding...');
    
    await _seedUsers();
    await _seedConversations();
    await _seedMessages();
    await _seedPosts();
    
    print('Database seeding complete!');
  }
  
  /// Create test users
  Future<List<UserProfile>> _seedUsers() async {
    print('Seeding users...');
    
    final users = <UserProfile>[];
    final testUsers = [
      ('alice', 'Alice Johnson', 'alice@example.com'),
      ('bob', 'Bob Smith', 'bob@example.com'),
      ('charlie', 'Charlie Brown', 'charlie@example.com'),
      ('diana', 'Diana Prince', 'diana@example.com'),
    ];
    
    for (final (username, displayName, email) in testUsers) {
      final user = UserProfile(
        userInfoId: users.length + 1,  // Mock user info ID
        username: username,
        displayName: displayName,
        email: email,
        isOnline: false,
        isVerified: true,
        isDeleted: false,
        createdAt: DateTime.now(),
      );
      
      final inserted = await UserProfile.db.insertRow(session, user);
      users.add(inserted);
      print('  Created user: $username');
    }
    
    return users;
  }
  
  /// Create test conversations
  Future<void> _seedConversations() async {
    print('Seeding conversations...');
    
    // Direct conversation between Alice and Bob
    final directConvo = Conversation(
      conversationType: ConversationType.direct,
      isArchived: false,
      isDeleted: false,
      createdAt: DateTime.now(),
    );
    
    final insertedDirect = await Conversation.db.insertRow(
      session, 
      directConvo,
    );
    
    // Add participants
    await Participant.db.insertRow(session, Participant(
      conversationId: insertedDirect.id!,
      userId: 1,  // Alice
      role: ParticipantRole.member,
      unreadCount: 0,
      isMuted: false,
      hasLeft: false,
      joinedAt: DateTime.now(),
    ));
    
    await Participant.db.insertRow(session, Participant(
      conversationId: insertedDirect.id!,
      userId: 2,  // Bob
      role: ParticipantRole.member,
      unreadCount: 0,
      isMuted: false,
      hasLeft: false,
      joinedAt: DateTime.now(),
    ));
    
    print('  Created direct conversation: Alice <-> Bob');
    
    // Group conversation
    final groupConvo = Conversation(
      conversationType: ConversationType.group,
      name: 'Flutter Developers',
      description: 'A group for Flutter enthusiasts',
      createdById: 1,  // Alice created it
      isArchived: false,
      isDeleted: false,
      createdAt: DateTime.now(),
    );
    
    final insertedGroup = await Conversation.db.insertRow(
      session, 
      groupConvo,
    );
    
    // Add all users to group
    for (var i = 1; i <= 4; i++) {
      await Participant.db.insertRow(session, Participant(
        conversationId: insertedGroup.id!,
        userId: i,
        role: i == 1 ? ParticipantRole.owner : ParticipantRole.member,
        unreadCount: 0,
        isMuted: false,
        hasLeft: false,
        joinedAt: DateTime.now(),
      ));
    }
    
    print('  Created group: Flutter Developers');
  }
  
  /// Create test messages
  Future<void> _seedMessages() async {
    print('Seeding messages...');
    
    final messages = [
      (1, 1, 'Hey Bob! How are you?'),
      (1, 2, 'Hi Alice! I\'m doing great, thanks!'),
      (1, 1, 'Want to work on the Flutter project?'),
      (1, 2, 'Sure! Let me check the repo.'),
    ];
    
    for (final (convoId, senderId, content) in messages) {
      await Message.db.insertRow(session, Message(
        conversationId: convoId,
        senderId: senderId,
        content: content,
        messageType: MessageType.text,
        isEdited: false,
        isDeleted: false,
        createdAt: DateTime.now(),
      ));
    }
    
    print('  Created ${messages.length} messages');
  }
  
  /// Create test posts
  Future<void> _seedPosts() async {
    print('Seeding posts...');
    
    final post = Post(
      authorId: 1,  // Alice
      content: 'Just finished setting up the Serverpod backend! '
               'The code generation is amazing.',
      contentType: PostContentType.text,
      likeCount: 0,
      commentCount: 0,
      shareCount: 0,
      visibility: PostVisibility.public,
      isEdited: false,
      isDeleted: false,
      isPinned: false,
      createdAt: DateTime.now(),
    );
    
    await Post.db.insertRow(session, post);
    print('  Created 1 post');
  }
}
```
