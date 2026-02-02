# server/lib/src/protocol/message_reaction.yaml
class: MessageReaction
table: message_reactions
fields:
  # TODO: Define fields for:
  # - messageId (relation to messages)
  # - userId (relation to user_profiles)
  # - emoji (the reaction emoji)
  # - createdAt

indexes:
  # TODO: Add indexes for:
  # - Unique constraint on messageId + userId + emoji
  # - Query by messageId

---

// server/lib/src/endpoints/reaction_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class ReactionEndpoint extends Endpoint {
  /// Toggle a reaction on a message (add if not exists, remove if exists)
  Future<ReactionResult> toggleReaction(
    Session session, {
    required int messageId,
    required String emoji,
  }) async {
    // TODO: Implement
    // 1. Authenticate user
    // 2. Verify user can access the message's conversation
    // 3. Check if reaction already exists
    // 4. Add or remove reaction
    // 5. Return updated reaction counts
    // 6. Broadcast reaction change
    throw UnimplementedError();
  }

  /// Get all reactions for a message with user details
  Future<MessageReactions> getMessageReactions(
    Session session, {
    required int messageId,
  }) async {
    // TODO: Implement
    // Return aggregated counts and list of reactors per emoji
    throw UnimplementedError();
  }

  /// Get users who reacted with a specific emoji
  Future<List<UserProfile>> getReactors(
    Session session, {
    required int messageId,
    required String emoji,
    int limit = 50,
  }) async {
    // TODO: Implement
    throw UnimplementedError();
  }
}

/// Result of toggling a reaction
class ReactionResult {
  final bool added;  // true if added, false if removed
  final String emoji;
  final Map<String, int> reactionCounts;  // emoji -> count
  
  ReactionResult({
    required this.added,
    required this.emoji,
    required this.reactionCounts,
  });
}

/// All reactions for a message
class MessageReactions {
  final int messageId;
  final Map<String, int> counts;  // emoji -> count
  final Map<String, List<ReactorInfo>> reactors;  // emoji -> users
  final Set<String> currentUserReactions;  // emojis current user has used
  
  MessageReactions({
    required this.messageId,
    required this.counts,
    required this.reactors,
    required this.currentUserReactions,
  });
}