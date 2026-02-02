---
type: "EXAMPLE"
title: "Step 6: Implement ChatRoomEndpoint"
---

The ChatRoomEndpoint manages chat rooms and their memberships.



```dart
// File: lib/src/endpoints/chat_room_endpoint.dart

import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class ChatRoomEndpoint extends Endpoint {
  /// Create a new group chat room.
  Future<ChatRoom> createGroup(
    Session session, {
    required String name,
    String? description,
    required List<int> memberUserIds,
  }) async {
    final currentUser = await _getCurrentUser(session);
    
    // Create the room
    final room = ChatRoom(
      name: name,
      description: description,
      isGroup: true,
      createdByUserId: currentUser.id!,
      createdAt: DateTime.now(),
    );
    
    final savedRoom = await ChatRoom.db.insertRow(session, room);
    
    // Add creator as admin
    await ChatMember.db.insertRow(
      session,
      ChatMember(
        chatRoomId: savedRoom.id!,
        userId: currentUser.id!,
        role: 'admin',
        isMuted: false,
        joinedAt: DateTime.now(),
      ),
    );
    
    // Add other members
    for (final userId in memberUserIds) {
      if (userId != currentUser.id) {
        await ChatMember.db.insertRow(
          session,
          ChatMember(
            chatRoomId: savedRoom.id!,
            userId: userId,
            role: 'member',
            isMuted: false,
            joinedAt: DateTime.now(),
          ),
        );
      }
    }
    
    // Send system message about room creation
    await ChatMessage.db.insertRow(
      session,
      ChatMessage(
        chatRoomId: savedRoom.id!,
        senderId: currentUser.id!,
        content: '${currentUser.displayName} created the group',
        messageType: 'system',
        createdAt: DateTime.now(),
        isDeleted: false,
      ),
    );
    
    return savedRoom;
  }
  
  /// Create or get a direct message room between two users.
  Future<ChatRoom> getOrCreateDirectMessage(
    Session session,
    int otherUserId,
  ) async {
    final currentUser = await _getCurrentUser(session);
    
    if (currentUser.id == otherUserId) {
      throw InvalidParameterException('Cannot create DM with yourself');
    }
    
    // Check if DM already exists between these users
    final existingRoom = await _findExistingDM(
      session,
      currentUser.id!,
      otherUserId,
    );
    
    if (existingRoom != null) {
      return existingRoom;
    }
    
    // Create new DM room
    final room = ChatRoom(
      isGroup: false,
      createdByUserId: currentUser.id!,
      createdAt: DateTime.now(),
    );
    
    final savedRoom = await ChatRoom.db.insertRow(session, room);
    
    // Add both users as members
    await ChatMember.db.insertRow(
      session,
      ChatMember(
        chatRoomId: savedRoom.id!,
        userId: currentUser.id!,
        role: 'member',
        isMuted: false,
        joinedAt: DateTime.now(),
      ),
    );
    
    await ChatMember.db.insertRow(
      session,
      ChatMember(
        chatRoomId: savedRoom.id!,
        userId: otherUserId,
        role: 'member',
        isMuted: false,
        joinedAt: DateTime.now(),
      ),
    );
    
    return savedRoom;
  }
  
  /// Get all chat rooms for the current user.
  Future<List<ChatRoom>> getMyRooms(Session session) async {
    final currentUser = await _getCurrentUser(session);
    
    // Get all room IDs where user is a member
    final memberships = await ChatMember.db.find(
      session,
      where: (t) => t.userId.equals(currentUser.id!),
    );
    
    if (memberships.isEmpty) {
      return [];
    }
    
    final roomIds = memberships.map((m) => m.chatRoomId).toList();
    
    // Fetch rooms, ordered by last message
    return await ChatRoom.db.find(
      session,
      where: (t) => t.id.inSet(roomIds.toSet()),
      orderBy: (t) => t.lastMessageAt,
      orderDescending: true,
    );
  }
  
  /// Get room details with members.
  Future<ChatRoom?> getRoomById(Session session, int roomId) async {
    await _requireMembership(session, roomId);
    return await ChatRoom.db.findById(session, roomId);
  }
  
  /// Get all members of a room.
  Future<List<ChatUser>> getRoomMembers(Session session, int roomId) async {
    await _requireMembership(session, roomId);
    
    final memberships = await ChatMember.db.find(
      session,
      where: (t) => t.chatRoomId.equals(roomId),
    );
    
    if (memberships.isEmpty) {
      return [];
    }
    
    final userIds = memberships.map((m) => m.userId).toSet();
    
    return await ChatUser.db.find(
      session,
      where: (t) => t.id.inSet(userIds),
    );
  }
  
  /// Leave a chat room.
  Future<bool> leaveRoom(Session session, int roomId) async {
    final currentUser = await _getCurrentUser(session);
    
    await ChatMember.db.deleteWhere(
      session,
      where: (t) => 
        t.chatRoomId.equals(roomId) & 
        t.userId.equals(currentUser.id!),
    );
    
    // Send system message
    await ChatMessage.db.insertRow(
      session,
      ChatMessage(
        chatRoomId: roomId,
        senderId: currentUser.id!,
        content: '${currentUser.displayName} left the chat',
        messageType: 'system',
        createdAt: DateTime.now(),
        isDeleted: false,
      ),
    );
    
    return true;
  }
  
  // Helper: Find existing DM between two users
  Future<ChatRoom?> _findExistingDM(
    Session session,
    int userId1,
    int userId2,
  ) async {
    // Find rooms where both users are members and it's not a group
    final result = await session.db.query(
      '''
      SELECT cr.id FROM chat_rooms cr
      INNER JOIN chat_members cm1 ON cr.id = cm1."chatRoomId"
      INNER JOIN chat_members cm2 ON cr.id = cm2."chatRoomId"
      WHERE cr."isGroup" = false
        AND cm1."userId" = @userId1
        AND cm2."userId" = @userId2
      LIMIT 1
      ''',
      parameters: {'userId1': userId1, 'userId2': userId2},
    );
    
    if (result.isEmpty) {
      return null;
    }
    
    final roomId = result.first.first as int;
    return await ChatRoom.db.findById(session, roomId);
  }
  
  // Helper: Get current user
  Future<ChatUser> _getCurrentUser(Session session) async {
    final authUserId = await session.auth.authenticatedUserId;
    if (authUserId == null) {
      throw AuthenticationRequiredException();
    }
    
    final user = await ChatUser.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(authUserId),
    );
    
    if (user == null) {
      throw NotFoundException('User profile not found');
    }
    
    return user;
  }
  
  // Helper: Verify user is member of room
  Future<ChatMember> _requireMembership(
    Session session,
    int roomId,
  ) async {
    final currentUser = await _getCurrentUser(session);
    
    final membership = await ChatMember.db.findFirstRow(
      session,
      where: (t) => 
        t.chatRoomId.equals(roomId) & 
        t.userId.equals(currentUser.id!),
    );
    
    if (membership == null) {
      throw ForbiddenException('Not a member of this room');
    }
    
    return membership;
  }
}

class ForbiddenException implements Exception {
  final String message;
  ForbiddenException(this.message);
  
  @override
  String toString() => message;
}
```
