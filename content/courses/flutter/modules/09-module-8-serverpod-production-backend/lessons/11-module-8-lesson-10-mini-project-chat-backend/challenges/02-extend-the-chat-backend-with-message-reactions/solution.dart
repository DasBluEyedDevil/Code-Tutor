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
  static int _nextId = 1;
  
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
    
    // Validate emoji
    if (!isValidEmoji(emoji)) {
      throw Exception('Invalid emoji: $emoji');
    }
    
    // Get the message and verify it exists
    final message = _messages.where((m) => m.id == messageId).firstOrNull;
    if (message == null) {
      throw Exception('Message not found');
    }
    
    // Verify user is a member of the message's room
    final isMember = _members.any(
      (m) => m.chatRoomId == message.chatRoomId && m.userId == userId,
    );
    if (!isMember) {
      throw Exception('Not a member of this room');
    }
    
    // Check if user already has this reaction (idempotent)
    final existing = _reactions.where(
      (r) => r.messageId == messageId && r.userId == userId && r.emoji == emoji,
    ).firstOrNull;
    
    if (existing != null) {
      return existing; // Return existing reaction
    }
    
    // Create and save the reaction
    final reaction = MessageReaction(
      id: _nextId++,
      messageId: messageId,
      userId: userId,
      emoji: emoji,
      createdAt: DateTime.now(),
    );
    
    _reactions.add(reaction);
    
    // Broadcast reaction to room subscribers
    session.log('Broadcasting reaction $emoji to room ${message.chatRoomId}');
    
    return reaction;
  }
  
  /// Remove a reaction from a message.
  Future<bool> removeReaction(
    Session session, {
    required int messageId,
    required String emoji,
  }) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw Exception('Not authenticated');
    
    // Find the reaction
    final reactionIndex = _reactions.indexWhere(
      (r) => r.messageId == messageId && r.userId == userId && r.emoji == emoji,
    );
    
    if (reactionIndex == -1) {
      return false; // Reaction doesn't exist
    }
    
    // Verify it belongs to current user (already checked in query)
    final reaction = _reactions[reactionIndex];
    if (reaction.userId != userId) {
      throw Exception('Cannot remove reaction from another user');
    }
    
    // Delete the reaction
    _reactions.removeAt(reactionIndex);
    
    // Broadcast removal to room subscribers
    final message = _messages.where((m) => m.id == messageId).firstOrNull;
    if (message != null) {
      session.log('Broadcasting reaction removal to room ${message.chatRoomId}');
    }
    
    return true;
  }
  
  /// Get aggregated reactions for a message.
  /// Groups reactions by emoji and counts them.
  Future<List<ReactionSummary>> getReactions(
    Session session,
    int messageId,
  ) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) throw Exception('Not authenticated');
    
    // Get the message and verify user has access
    final message = _messages.where((m) => m.id == messageId).firstOrNull;
    if (message == null) {
      throw Exception('Message not found');
    }
    
    // Verify user is a member
    final isMember = _members.any(
      (m) => m.chatRoomId == message.chatRoomId && m.userId == userId,
    );
    if (!isMember) {
      throw Exception('Not a member of this room');
    }
    
    // Get all reactions for this message
    final messageReactions = _reactions
        .where((r) => r.messageId == messageId)
        .toList();
    
    // Group by emoji
    final Map<String, List<MessageReaction>> grouped = {};
    for (final reaction in messageReactions) {
      grouped[reaction.emoji] ??= [];
      grouped[reaction.emoji]!.add(reaction);
    }
    
    // Create ReactionSummary for each emoji
    return grouped.entries.map((entry) {
      final emoji = entry.key;
      final reactions = entry.value;
      final userIds = reactions.map((r) => r.userId).toList();
      final currentUserReacted = userIds.contains(userId);
      
      return ReactionSummary(
        emoji: emoji,
        count: reactions.length,
        userIds: userIds,
        currentUserReacted: currentUserReacted,
      );
    }).toList();
  }
  
  /// Validate that a string is a valid emoji.
  bool isValidEmoji(String emoji) {
    // Emoji are typically 1-4 characters (due to modifiers, ZWJ sequences)
    if (emoji.isEmpty || emoji.length > 8) {
      return false;
    }
    
    // Check if it contains emoji characters using Unicode ranges
    // This is a simplified check - production should use a proper emoji library
    final emojiRegex = RegExp(
      r'^[\u{1F300}-\u{1F9FF}\u{2600}-\u{26FF}\u{2700}-\u{27BF}]+$',
      unicode: true,
    );
    
    // Also allow common emoji that might be outside these ranges
    final commonEmoji = ['👍', '👎', '❤️', '😀', '😂', '🎉', '🔥', '💯', '✨', '🙏'];
    
    return emojiRegex.hasMatch(emoji) || commonEmoji.contains(emoji);
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