# server/lib/src/protocol/message_reaction.yaml
class: MessageReaction
table: message_reactions
fields:
  messageId: int, relation(parent=messages)
  userId: int, relation(parent=user_profiles)
  emoji: String
  createdAt: DateTime

indexes:
  reaction_unique_idx:
    fields: messageId, userId, emoji
    unique: true
  reaction_message_idx:
    fields: messageId, emoji
  reaction_user_idx:
    fields: userId, createdAt

---

// server/lib/src/endpoints/reaction_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import '../services/chat_service.dart';

class ReactionEndpoint extends Endpoint {
  final ChatService _chatService = ChatService();
  
  // Allowed emoji reactions
  static const Set<String> allowedEmojis = {
    '\u{1F44D}',  // thumbs up
    '\u{2764}',   // red heart
    '\u{1F602}',  // face with tears of joy
    '\u{1F62E}',  // surprised face
    '\u{1F622}',  // crying face
    '\u{1F621}',  // angry face
    '\u{1F64F}',  // folded hands
    '\u{1F389}',  // party popper
    '\u{1F525}',  // fire
    '\u{1F44F}',  // clapping hands
  };

  /// Toggle a reaction on a message
  Future<ReactionResult> toggleReaction(
    Session session, {
    required int messageId,
    required String emoji,
  }) async {
    // 1. Authenticate
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw ReactionException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      throw ReactionException(
        code: 'user_not_found',
        message: 'User not found',
      );
    }
    
    // Validate emoji (optional - can allow any emoji)
    // if (!allowedEmojis.contains(emoji)) {
    //   throw ReactionException(
    //     code: 'invalid_emoji',
    //     message: 'This emoji is not allowed',
    //   );
    // }
    
    // 2. Get message and verify access
    final message = await Message.db.findById(session, messageId);
    if (message == null || message.isDeleted) {
      throw ReactionException(
        code: 'message_not_found',
        message: 'Message not found',
      );
    }
    
    // Verify user is in the conversation
    final participation = await ConversationParticipant.db.findFirstRow(
      session,
      where: (t) => t.conversationId.equals(message.conversationId) &
                    t.userId.equals(user.id!) &
                    t.isActive.equals(true),
    );
    
    if (participation == null) {
      throw ReactionException(
        code: 'access_denied',
        message: 'You are not a member of this conversation',
      );
    }
    
    // 3. Check if reaction exists
    final existing = await MessageReaction.db.findFirstRow(
      session,
      where: (t) => t.messageId.equals(messageId) &
                    t.userId.equals(user.id!) &
                    t.emoji.equals(emoji),
    );
    
    bool added;
    if (existing != null) {
      // 4a. Remove existing reaction
      await MessageReaction.db.deleteRow(session, existing);
      added = false;
    } else {
      // 4b. Add new reaction
      await MessageReaction.db.insertRow(
        session,
        MessageReaction(
          messageId: messageId,
          userId: user.id!,
          emoji: emoji,
          createdAt: DateTime.now(),
        ),
      );
      added = true;
    }
    
    // 5. Get updated reaction counts
    final reactionCounts = await _getReactionCounts(session, messageId);
    
    // 6. Broadcast reaction change
    await _chatService.broadcastReaction(
      session,
      message.conversationId,
      ChatStreamReaction(
        messageId: messageId,
        userId: user.id!,
        userName: user.displayName,
        emoji: emoji,
        added: added,
        reactionCounts: reactionCounts,
      ),
    );
    
    return ReactionResult(
      added: added,
      emoji: emoji,
      reactionCounts: reactionCounts,
    );
  }

  /// Get all reactions for a message
  Future<MessageReactions> getMessageReactions(
    Session session, {
    required int messageId,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    UserProfile? currentUser;
    if (userId != null) {
      currentUser = await UserProfile.db.findFirstRow(
        session,
        where: (t) => t.userInfoId.equals(userId),
      );
    }
    
    // Get all reactions
    final reactions = await MessageReaction.db.find(
      session,
      where: (t) => t.messageId.equals(messageId),
      orderBy: (t) => t.createdAt,
    );
    
    // Aggregate by emoji
    final counts = <String, int>{};
    final reactorsByEmoji = <String, List<ReactorInfo>>{};
    final currentUserReactions = <String>{};
    
    for (final reaction in reactions) {
      // Increment count
      counts[reaction.emoji] = (counts[reaction.emoji] ?? 0) + 1;
      
      // Get user info
      final user = await UserProfile.db.findById(session, reaction.userId);
      
      reactorsByEmoji.putIfAbsent(reaction.emoji, () => []).add(
        ReactorInfo(
          userId: reaction.userId,
          userName: user?.displayName,
          userAvatarUrl: user?.avatarUrl,
          reactedAt: reaction.createdAt,
        ),
      );
      
      // Track current user's reactions
      if (currentUser != null && reaction.userId == currentUser.id) {
        currentUserReactions.add(reaction.emoji);
      }
    }
    
    return MessageReactions(
      messageId: messageId,
      counts: counts,
      reactors: reactorsByEmoji,
      currentUserReactions: currentUserReactions,
    );
  }

  /// Get users who reacted with a specific emoji
  Future<List<UserProfile>> getReactors(
    Session session, {
    required int messageId,
    required String emoji,
    int limit = 50,
  }) async {
    final reactions = await MessageReaction.db.find(
      session,
      where: (t) => t.messageId.equals(messageId) & t.emoji.equals(emoji),
      orderBy: (t) => t.createdAt,
      orderDescending: true,
      limit: limit,
    );
    
    final users = <UserProfile>[];
    for (final reaction in reactions) {
      final user = await UserProfile.db.findById(session, reaction.userId);
      if (user != null) {
        users.add(user);
      }
    }
    
    return users;
  }
  
  /// Helper to get reaction counts
  Future<Map<String, int>> _getReactionCounts(
    Session session,
    int messageId,
  ) async {
    final reactions = await MessageReaction.db.find(
      session,
      where: (t) => t.messageId.equals(messageId),
    );
    
    final counts = <String, int>{};
    for (final reaction in reactions) {
      counts[reaction.emoji] = (counts[reaction.emoji] ?? 0) + 1;
    }
    
    return counts;
  }
}

class ReactionException implements Exception {
  final String code;
  final String message;
  
  ReactionException({required this.code, required this.message});
  
  @override
  String toString() => 'ReactionException: [$code] $message';
}

class ReactionResult {
  final bool added;
  final String emoji;
  final Map<String, int> reactionCounts;
  
  ReactionResult({
    required this.added,
    required this.emoji,
    required this.reactionCounts,
  });
}

class MessageReactions {
  final int messageId;
  final Map<String, int> counts;
  final Map<String, List<ReactorInfo>> reactors;
  final Set<String> currentUserReactions;
  
  MessageReactions({
    required this.messageId,
    required this.counts,
    required this.reactors,
    required this.currentUserReactions,
  });
  
  /// Get total reaction count
  int get totalCount => counts.values.fold(0, (a, b) => a + b);
  
  /// Check if current user has reacted with an emoji
  bool hasReacted(String emoji) => currentUserReactions.contains(emoji);
}

class ReactorInfo {
  final int userId;
  final String? userName;
  final String? userAvatarUrl;
  final DateTime reactedAt;
  
  ReactorInfo({
    required this.userId,
    this.userName,
    this.userAvatarUrl,
    required this.reactedAt,
  });
}

class ChatStreamReaction {
  final int messageId;
  final int userId;
  final String? userName;
  final String emoji;
  final bool added;
  final Map<String, int> reactionCounts;
  
  ChatStreamReaction({
    required this.messageId,
    required this.userId,
    this.userName,
    required this.emoji,
    required this.added,
    required this.reactionCounts,
  });
}