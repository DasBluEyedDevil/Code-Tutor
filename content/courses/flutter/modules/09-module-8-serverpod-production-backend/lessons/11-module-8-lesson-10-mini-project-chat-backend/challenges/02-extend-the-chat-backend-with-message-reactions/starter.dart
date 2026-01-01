// Protocol definition for MessageReaction
// File: lib/src/protocol/message_reaction.yaml
//
// class: MessageReaction
// table: message_reactions
// fields:
//   messageId: int
//   userId: int
//   emoji: String
//   createdAt: DateTime
// indexes:
//   reaction_unique_idx:
//     fields: messageId, userId, emoji
//     unique: true

import 'package:serverpod/serverpod.dart';

// Placeholder classes for compilation
class MessageReaction {
  final int? id;
  final int messageId;
  final int userId;
  final String emoji;
  final DateTime createdAt;
  
  MessageReaction({
    this.id,
    required this.messageId,
    required this.userId,
    required this.emoji,
    required this.createdAt,
  });
}

class ChatMessage {
  final int? id;
  final int chatRoomId;
  ChatMessage({this.id, required this.chatRoomId});
}

class ChatMember {
  final int chatRoomId;
  final int userId;
  ChatMember({required this.chatRoomId, required this.userId});
}

class Session {
  final _Auth auth = _Auth();
  void log(String message) => print(message);
}

class _Auth {
  Future<int?> authenticatedUserId = Future.value(1);
}

/// Aggregated reaction data for display
class ReactionSummary {
  final String emoji;
  final int count;
  final List<int> userIds;
  final bool currentUserReacted;
  
  ReactionSummary({
    required this.emoji,
    required this.count,
    required this.userIds,
    required this.currentUserReacted,
  });
  
  @override
  String toString() => '$emoji: $count (you: $currentUserReacted)';
}

class ReactionEndpoint {
  // Simulated database
  static final List<MessageReaction> _reactions = [];
  static final List<ChatMessage> _messages = [
    ChatMessage(id: 1, chatRoomId: 100),
    ChatMessage(id: 2, chatRoomId: 100),
  ];
  static final List<ChatMember> _members = [
    ChatMember(chatRoomId: 100, userId: 1),
    ChatMember(chatRoomId: 100, userId: 2),
  ];
  
  /// Add a reaction to a message.
  /// Returns the created reaction or throws if invalid.
  Future<MessageReaction> addReaction(
    Session session, {
    required int messageId,
    required String emoji,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw Exception('Not authenticated');
    
    // TODO: Validate emoji is a valid emoji character
    // Hint: Check if emoji is 1-4 characters and contains emoji
    
    // TODO: Get the message and verify it exists
    
    // TODO: Verify user is a member of the message's room
    
    // TODO: Check if user already has this reaction
    // If so, return existing reaction (idempotent)
    
    // TODO: Create and save the reaction
    
    // TODO: Broadcast reaction to room subscribers
    
    throw UnimplementedError('Implement addReaction');
  }
  
  /// Remove a reaction from a message.
  Future<bool> removeReaction(
    Session session, {
    required int messageId,
    required String emoji,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw Exception('Not authenticated');
    
    // TODO: Find the reaction
    
    // TODO: Verify it belongs to current user
    
    // TODO: Delete the reaction
    
    // TODO: Broadcast removal to room subscribers
    
    throw UnimplementedError('Implement removeReaction');
  }
  
  /// Get aggregated reactions for a message.
  /// Groups reactions by emoji and counts them.
  Future<List<ReactionSummary>> getReactions(
    Session session,
    int messageId,
  ) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw Exception('Not authenticated');
    
    // TODO: Get the message and verify user has access
    
    // TODO: Get all reactions for this message
    
    // TODO: Group by emoji and create ReactionSummary for each
    // Include whether current user has reacted with each emoji
    
    throw UnimplementedError('Implement getReactions');
  }
  
  /// Validate that a string is a valid emoji.
  bool isValidEmoji(String emoji) {
    // TODO: Implement emoji validation
    // Emoji are typically 1-4 characters (due to modifiers)
    // Should match Unicode emoji pattern
    return false;
  }
}

// Test your implementation
void main() async {
  final endpoint = ReactionEndpoint();
  final session = Session();
  
  print('Testing Reaction Endpoint\n');
  
  // Test 1: Add reaction
  print('--- Test 1: Add reaction ---');
  try {
    final reaction = await endpoint.addReaction(
      session,
      messageId: 1,
      emoji: '👍',
    );
    print('Added reaction: ${reaction.emoji} to message ${reaction.messageId}');
  } catch (e) {
    print('Error: $e');
  }
  
  // Test 2: Add same reaction (should be idempotent)
  print('\n--- Test 2: Idempotent add ---');
  try {
    final reaction = await endpoint.addReaction(
      session,
      messageId: 1,
      emoji: '👍',
    );
    print('Returned existing reaction: ${reaction.emoji}');
  } catch (e) {
    print('Error: $e');
  }
  
  // Test 3: Add different reaction
  print('\n--- Test 3: Different emoji ---');
  try {
    await endpoint.addReaction(session, messageId: 1, emoji: '❤️');
    print('Added heart reaction');
  } catch (e) {
    print('Error: $e');
  }
  
  // Test 4: Get reactions
  print('\n--- Test 4: Get reactions ---');
  try {
    final reactions = await endpoint.getReactions(session, 1);
    print('Reactions for message 1:');
    for (final r in reactions) {
      print('  $r');
    }
  } catch (e) {
    print('Error: $e');
  }
  
  // Test 5: Remove reaction
  print('\n--- Test 5: Remove reaction ---');
  try {
    final removed = await endpoint.removeReaction(
      session,
      messageId: 1,
      emoji: '👍',
    );
    print('Removed: $removed');
  } catch (e) {
    print('Error: $e');
  }
  
  // Test 6: Invalid emoji
  print('\n--- Test 6: Invalid emoji ---');
  try {
    await endpoint.addReaction(session, messageId: 1, emoji: 'abc');
    print('Should have thrown!');
  } catch (e) {
    print('Correctly rejected: $e');
  }
  
  print('\nTests completed!');
}