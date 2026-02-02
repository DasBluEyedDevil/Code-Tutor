---
type: "EXAMPLE"
title: "Step 7: Implement ChatMessageEndpoint"
---

The ChatMessageEndpoint handles message CRUD operations via HTTP. Real-time delivery is handled separately by the streaming endpoint.



```dart
// File: lib/src/endpoints/chat_message_endpoint.dart

import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import 'chat_stream_endpoint.dart';

class ChatMessageEndpoint extends Endpoint {
  /// Send a text message to a chat room.
  Future<ChatMessage> sendMessage(
    Session session, {
    required int chatRoomId,
    required String content,
    int? replyToMessageId,
  }) async {
    final currentUser = await _getCurrentUser(session);
    await _requireMembership(session, chatRoomId);
    
    // Validate content
    if (content.trim().isEmpty) {
      throw InvalidParameterException('Message content cannot be empty');
    }
    
    if (content.length > 4000) {
      throw InvalidParameterException('Message too long (max 4000 characters)');
    }
    
    // Create the message
    final message = ChatMessage(
      chatRoomId: chatRoomId,
      senderId: currentUser.id!,
      content: content.trim(),
      messageType: 'text',
      replyToMessageId: replyToMessageId,
      createdAt: DateTime.now(),
      isDeleted: false,
    );
    
    final savedMessage = await ChatMessage.db.insertRow(session, message);
    
    // Update room's lastMessageAt
    await _updateRoomLastMessage(session, chatRoomId);
    
    // Broadcast to connected clients via streaming
    ChatStreamEndpoint.broadcastMessage(chatRoomId, savedMessage, currentUser);
    
    return savedMessage;
  }
  
  /// Send a message with an attachment.
  Future<ChatMessage> sendAttachment(
    Session session, {
    required int chatRoomId,
    required String attachmentUrl,
    required String attachmentName,
    required String attachmentMimeType,
    required int attachmentSize,
    String? caption,
  }) async {
    final currentUser = await _getCurrentUser(session);
    await _requireMembership(session, chatRoomId);
    
    // Determine message type from MIME type
    String messageType = 'file';
    if (attachmentMimeType.startsWith('image/')) {
      messageType = 'image';
    } else if (attachmentMimeType.startsWith('video/')) {
      messageType = 'video';
    } else if (attachmentMimeType.startsWith('audio/')) {
      messageType = 'audio';
    }
    
    final message = ChatMessage(
      chatRoomId: chatRoomId,
      senderId: currentUser.id!,
      content: caption ?? '',
      messageType: messageType,
      attachmentUrl: attachmentUrl,
      attachmentName: attachmentName,
      attachmentSize: attachmentSize,
      attachmentMimeType: attachmentMimeType,
      createdAt: DateTime.now(),
      isDeleted: false,
    );
    
    final savedMessage = await ChatMessage.db.insertRow(session, message);
    
    await _updateRoomLastMessage(session, chatRoomId);
    ChatStreamEndpoint.broadcastMessage(chatRoomId, savedMessage, currentUser);
    
    return savedMessage;
  }
  
  /// Get message history for a room with pagination.
  Future<List<ChatMessage>> getMessages(
    Session session, {
    required int chatRoomId,
    int limit = 50,
    DateTime? before,
  }) async {
    await _requireMembership(session, chatRoomId);
    
    if (before != null) {
      return await ChatMessage.db.find(
        session,
        where: (t) => 
          t.chatRoomId.equals(chatRoomId) &
          t.createdAt.isSmallerThan(before) &
          t.isDeleted.equals(false),
        orderBy: (t) => t.createdAt,
        orderDescending: true,
        limit: limit,
      );
    }
    
    return await ChatMessage.db.find(
      session,
      where: (t) => 
        t.chatRoomId.equals(chatRoomId) &
        t.isDeleted.equals(false),
      orderBy: (t) => t.createdAt,
      orderDescending: true,
      limit: limit,
    );
  }
  
  /// Edit a message (only by sender, within time limit).
  Future<ChatMessage> editMessage(
    Session session, {
    required int messageId,
    required String newContent,
  }) async {
    final currentUser = await _getCurrentUser(session);
    
    final message = await ChatMessage.db.findById(session, messageId);
    if (message == null) {
      throw NotFoundException('Message not found');
    }
    
    // Only sender can edit
    if (message.senderId != currentUser.id) {
      throw ForbiddenException('Cannot edit message from another user');
    }
    
    // Only allow editing within 15 minutes
    final timeSinceCreation = DateTime.now().difference(message.createdAt);
    if (timeSinceCreation.inMinutes > 15) {
      throw ForbiddenException('Cannot edit message after 15 minutes');
    }
    
    // Only text messages can be edited
    if (message.messageType != 'text') {
      throw ForbiddenException('Only text messages can be edited');
    }
    
    final updated = message.copyWith(
      content: newContent.trim(),
      editedAt: DateTime.now(),
    );
    
    return await ChatMessage.db.updateRow(session, updated);
  }
  
  /// Delete a message (soft delete).
  Future<bool> deleteMessage(Session session, int messageId) async {
    final currentUser = await _getCurrentUser(session);
    
    final message = await ChatMessage.db.findById(session, messageId);
    if (message == null) {
      throw NotFoundException('Message not found');
    }
    
    // Check if user is sender or room admin
    final membership = await _requireMembership(session, message.chatRoomId);
    
    if (message.senderId != currentUser.id && membership.role != 'admin') {
      throw ForbiddenException('Cannot delete this message');
    }
    
    // Soft delete
    final deleted = message.copyWith(
      isDeleted: true,
      content: 'This message was deleted',
    );
    
    await ChatMessage.db.updateRow(session, deleted);
    
    return true;
  }
  
  /// Mark messages as read (update lastReadAt for member).
  Future<void> markAsRead(Session session, int chatRoomId) async {
    final currentUser = await _getCurrentUser(session);
    
    final membership = await ChatMember.db.findFirstRow(
      session,
      where: (t) => 
        t.chatRoomId.equals(chatRoomId) &
        t.userId.equals(currentUser.id!),
    );
    
    if (membership != null) {
      final updated = membership.copyWith(
        lastReadAt: DateTime.now(),
      );
      await ChatMember.db.updateRow(session, updated);
    }
  }
  
  /// Get unread message count for a room.
  Future<int> getUnreadCount(Session session, int chatRoomId) async {
    final currentUser = await _getCurrentUser(session);
    
    final membership = await ChatMember.db.findFirstRow(
      session,
      where: (t) => 
        t.chatRoomId.equals(chatRoomId) &
        t.userId.equals(currentUser.id!),
    );
    
    if (membership == null || membership.lastReadAt == null) {
      // Never read - count all messages
      return await ChatMessage.db.count(
        session,
        where: (t) => 
          t.chatRoomId.equals(chatRoomId) &
          t.isDeleted.equals(false),
      );
    }
    
    // Count messages after lastReadAt
    return await ChatMessage.db.count(
      session,
      where: (t) => 
        t.chatRoomId.equals(chatRoomId) &
        t.createdAt.isGreaterThan(membership.lastReadAt!) &
        t.isDeleted.equals(false),
    );
  }
  
  // Helper: Update room's last message timestamp
  Future<void> _updateRoomLastMessage(Session session, int roomId) async {
    final room = await ChatRoom.db.findById(session, roomId);
    if (room != null) {
      final updated = room.copyWith(lastMessageAt: DateTime.now());
      await ChatRoom.db.updateRow(session, updated);
    }
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
  
  // Helper: Verify membership
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
```
