// server/lib/src/protocol/user_session.yaml
# TODO: Define the UserSession model
# Track: userId, deviceName, deviceType, lastActivityAt, createdAt, etc.

---

// server/lib/src/services/session_manager.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class SessionManager {
  
  /// Create a new session record when user logs in
  Future<UserSession> createSession(
    Session session,
    int userId, {
    required String deviceName,
    required String deviceType,
    String? ipAddress,
  }) async {
    // TODO: Implement
    throw UnimplementedError();
  }
  
  /// Get all active sessions for a user
  Future<List<UserSession>> getActiveSessions(
    Session session,
    int userId,
  ) async {
    // TODO: Implement
    throw UnimplementedError();
  }
  
  /// Revoke a specific session
  Future<bool> revokeSession(
    Session session,
    int userId,
    int sessionId,
  ) async {
    // TODO: Implement
    // Verify the session belongs to this user before revoking
    throw UnimplementedError();
  }
  
  /// Revoke all sessions except the current one
  Future<int> revokeAllOtherSessions(
    Session session,
    int userId,
    int currentSessionId,
  ) async {
    // TODO: Implement
    // Return count of revoked sessions
    throw UnimplementedError();
  }
  
  /// Update last activity for a session
  Future<void> updateActivity(
    Session session,
    int sessionId,
  ) async {
    // TODO: Implement
    throw UnimplementedError();
  }
}