---
type: "EXAMPLE"
title: "Message Delivery Status Updates"
---


**Tracking Message Delivery and Read Receipts**

Users expect to know when their messages are delivered and read. This requires careful tracking and real-time updates without overwhelming the system with status broadcasts.



```dart
// server/lib/src/services/message_status_service.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

/// Service for tracking and broadcasting message delivery status
class MessageStatusService {
  /// Update message status when delivered to a recipient
  Future<void> markDelivered(
    Session session, {
    required int messageId,
    required int recipientId,
  }) async {
    final now = DateTime.now();
    
    // Check if status record exists
    final existing = await MessageStatus.db.findFirstRow(
      session,
      where: (t) => t.messageId.equals(messageId) & 
                    t.userId.equals(recipientId),
    );
    
    if (existing == null) {
      // Create new status record
      await MessageStatus.db.insertRow(
        session,
        MessageStatus(
          messageId: messageId,
          userId: recipientId,
          deliveredAt: now,
        ),
      );
    } else if (existing.deliveredAt == null) {
      // Update existing record
      await MessageStatus.db.updateRow(
        session,
        existing.copyWith(deliveredAt: now),
      );
    }
    
    // Check if all recipients have received the message
    await _checkAndUpdateMessageStatus(session, messageId);
  }
  
  /// Mark message as read by a recipient
  Future<void> markRead(
    Session session, {
    required int messageId,
    required int recipientId,
  }) async {
    final now = DateTime.now();
    
    final existing = await MessageStatus.db.findFirstRow(
      session,
      where: (t) => t.messageId.equals(messageId) & 
                    t.userId.equals(recipientId),
    );
    
    if (existing == null) {
      await MessageStatus.db.insertRow(
        session,
        MessageStatus(
          messageId: messageId,
          userId: recipientId,
          deliveredAt: now,
          readAt: now,
        ),
      );
    } else if (existing.readAt == null) {
      await MessageStatus.db.updateRow(
        session,
        existing.copyWith(
          deliveredAt: existing.deliveredAt ?? now,
          readAt: now,
        ),
      );
    }
    
    await _checkAndUpdateMessageStatus(session, messageId);
  }
  
  /// Bulk mark messages as read (when user scrolls through conversation)
  Future<void> bulkMarkRead(
    Session session, {
    required int conversationId,
    required int recipientId,
    required int upToMessageId,
  }) async {
    final now = DateTime.now();
    
    // Get all unread messages in the conversation up to the specified message
    final messages = await Message.db.find(
      session,
      where: (t) => t.conversationId.equals(conversationId) &
                    t.id.lessOrEqualThan(upToMessageId) &
                    t.senderId.notEquals(recipientId) &
                    t.isDeleted.equals(false),
    );
    
    for (final message in messages) {
      final existing = await MessageStatus.db.findFirstRow(
        session,
        where: (t) => t.messageId.equals(message.id!) & 
                      t.userId.equals(recipientId),
      );
      
      if (existing == null) {
        await MessageStatus.db.insertRow(
          session,
          MessageStatus(
            messageId: message.id!,
            userId: recipientId,
            deliveredAt: now,
            readAt: now,
          ),
        );
      } else if (existing.readAt == null) {
        await MessageStatus.db.updateRow(
          session,
          existing.copyWith(
            deliveredAt: existing.deliveredAt ?? now,
            readAt: now,
          ),
        );
      }
    }
    
    // Update participant's unread count
    final participation = await ConversationParticipant.db.findFirstRow(
      session,
      where: (t) => t.conversationId.equals(conversationId) &
                    t.userId.equals(recipientId),
    );
    
    if (participation != null) {
      await ConversationParticipant.db.updateRow(
        session,
        participation.copyWith(
          lastReadAt: now,
          lastReadMessageId: upToMessageId,
          unreadCount: 0,
          updatedAt: now,
        ),
      );
    }
  }
  
  /// Get detailed status for a message (for sender to see who read it)
  Future<MessageStatusDetails> getMessageStatus(
    Session session, {
    required int messageId,
  }) async {
    final message = await Message.db.findById(session, messageId);
    if (message == null) {
      throw Exception('Message not found');
    }
    
    // Get all participants except sender
    final participants = await ConversationParticipant.db.find(
      session,
      where: (t) => t.conversationId.equals(message.conversationId) &
                    t.userId.notEquals(message.senderId) &
                    t.isActive.equals(true),
    );
    
    // Get status for each participant
    final statuses = await MessageStatus.db.find(
      session,
      where: (t) => t.messageId.equals(messageId),
    );
    
    final statusMap = {for (final s in statuses) s.userId: s};
    
    final recipientStatuses = <RecipientStatus>[];
    int deliveredCount = 0;
    int readCount = 0;
    
    for (final participant in participants) {
      final status = statusMap[participant.userId];
      final user = await UserProfile.db.findById(session, participant.userId);
      
      if (status?.deliveredAt != null) deliveredCount++;
      if (status?.readAt != null) readCount++;
      
      recipientStatuses.add(RecipientStatus(
        userId: participant.userId,
        userName: user?.displayName,
        userAvatarUrl: user?.avatarUrl,
        deliveredAt: status?.deliveredAt,
        readAt: status?.readAt,
      ));
    }
    
    // Determine overall status
    String overallStatus;
    if (readCount == participants.length) {
      overallStatus = 'read';
    } else if (deliveredCount == participants.length) {
      overallStatus = 'delivered';
    } else if (deliveredCount > 0 || readCount > 0) {
      overallStatus = 'partial';
    } else {
      overallStatus = 'sent';
    }
    
    return MessageStatusDetails(
      messageId: messageId,
      overallStatus: overallStatus,
      totalRecipients: participants.length,
      deliveredCount: deliveredCount,
      readCount: readCount,
      recipientStatuses: recipientStatuses,
    );
  }
  
  /// Check and update the message's overall status
  Future<void> _checkAndUpdateMessageStatus(
    Session session,
    int messageId,
  ) async {
    final message = await Message.db.findById(session, messageId);
    if (message == null) return;
    
    // Get all participants except sender
    final participantCount = await ConversationParticipant.db.count(
      session,
      where: (t) => t.conversationId.equals(message.conversationId) &
                    t.userId.notEquals(message.senderId) &
                    t.isActive.equals(true),
    );
    
    // Count delivered and read
    final statuses = await MessageStatus.db.find(
      session,
      where: (t) => t.messageId.equals(messageId),
    );
    
    final deliveredCount = statuses.where((s) => s.deliveredAt != null).length;
    final readCount = statuses.where((s) => s.readAt != null).length;
    
    // Determine new status
    String newStatus;
    if (readCount >= participantCount) {
      newStatus = 'read';
    } else if (deliveredCount >= participantCount) {
      newStatus = 'delivered';
    } else {
      newStatus = message.status; // Keep current
    }
    
    // Update if changed
    if (message.status != newStatus) {
      await Message.db.updateRow(
        session,
        message.copyWith(status: newStatus),
      );
    }
  }
}

/// Detailed status information for a message
class MessageStatusDetails {
  final int messageId;
  final String overallStatus;
  final int totalRecipients;
  final int deliveredCount;
  final int readCount;
  final List<RecipientStatus> recipientStatuses;
  
  MessageStatusDetails({
    required this.messageId,
    required this.overallStatus,
    required this.totalRecipients,
    required this.deliveredCount,
    required this.readCount,
    required this.recipientStatuses,
  });
  
  /// Get summary text for display
  String get summaryText {
    if (totalRecipients == 1) {
      // Direct message
      switch (overallStatus) {
        case 'read':
          return 'Read';
        case 'delivered':
          return 'Delivered';
        case 'sent':
          return 'Sent';
        default:
          return 'Sending...';
      }
    } else {
      // Group message
      if (readCount == totalRecipients) {
        return 'Read by all';
      } else if (readCount > 0) {
        return 'Read by $readCount of $totalRecipients';
      } else if (deliveredCount == totalRecipients) {
        return 'Delivered to all';
      } else if (deliveredCount > 0) {
        return 'Delivered to $deliveredCount of $totalRecipients';
      } else {
        return 'Sent';
      }
    }
  }
}

/// Status for a single recipient
class RecipientStatus {
  final int userId;
  final String? userName;
  final String? userAvatarUrl;
  final DateTime? deliveredAt;
  final DateTime? readAt;
  
  RecipientStatus({
    required this.userId,
    this.userName,
    this.userAvatarUrl,
    this.deliveredAt,
    this.readAt,
  });
  
  bool get isDelivered => deliveredAt != null;
  bool get isRead => readAt != null;
  
  String get statusText {
    if (isRead) {
      return 'Read';
    } else if (isDelivered) {
      return 'Delivered';
    } else {
      return 'Pending';
    }
  }
}
```
