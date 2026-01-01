# server/lib/src/protocol/user_session.yaml
class: UserSession
table: user_sessions
fields:
  userId: int, relation(parent=user_profiles)
  authKeyId: int?  # Links to Serverpod's auth key
  
  # Device information
  deviceName: String
  deviceType: String  # 'ios', 'android', 'web', 'desktop'
  deviceModel: String?
  osVersion: String?
  appVersion: String?
  
  # Location/network info
  ipAddress: String?
  location: String?  # City, Country from IP geolocation
  
  # Activity tracking
  lastActivityAt: DateTime
  createdAt: DateTime
  
  # Session state
  isActive: bool
  revokedAt: DateTime?

indexes:
  user_session_user_idx:
    fields: userId, isActive
  user_session_activity_idx:
    fields: lastActivityAt

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
    String? deviceModel,
    String? osVersion,
    String? appVersion,
    String? ipAddress,
    int? authKeyId,
  }) async {
    // Get location from IP (optional, requires GeoIP service)
    String? location;
    if (ipAddress != null) {
      location = await _getLocationFromIP(ipAddress);
    }
    
    final userSession = UserSession(
      userId: userId,
      authKeyId: authKeyId,
      deviceName: deviceName,
      deviceType: deviceType,
      deviceModel: deviceModel,
      osVersion: osVersion,
      appVersion: appVersion,
      ipAddress: ipAddress,
      location: location,
      lastActivityAt: DateTime.now(),
      createdAt: DateTime.now(),
      isActive: true,
    );
    
    return UserSession.db.insertRow(session, userSession);
  }
  
  /// Get all active sessions for a user
  Future<List<UserSession>> getActiveSessions(
    Session session,
    int userId,
  ) async {
    return UserSession.db.find(
      session,
      where: (t) => t.userId.equals(userId) & t.isActive.equals(true),
      orderBy: (t) => t.lastActivityAt,
      orderDescending: true,
    );
  }
  
  /// Revoke a specific session
  Future<bool> revokeSession(
    Session session,
    int userId,
    int sessionId,
  ) async {
    // Fetch the session
    final userSession = await UserSession.db.findById(session, sessionId);
    
    // Verify ownership
    if (userSession == null || userSession.userId != userId) {
      return false;
    }
    
    // Already revoked
    if (!userSession.isActive) {
      return true;
    }
    
    // Revoke the session
    await UserSession.db.updateRow(
      session,
      userSession.copyWith(
        isActive: false,
        revokedAt: DateTime.now(),
      ),
    );
    
    // Also invalidate the auth key if present
    if (userSession.authKeyId != null) {
      await _invalidateAuthKey(session, userSession.authKeyId!);
    }
    
    return true;
  }
  
  /// Revoke all sessions except the current one
  Future<int> revokeAllOtherSessions(
    Session session,
    int userId,
    int currentSessionId,
  ) async {
    // Find all other active sessions
    final sessions = await UserSession.db.find(
      session,
      where: (t) => t.userId.equals(userId) & 
                    t.isActive.equals(true) &
                    t.id.notEquals(currentSessionId),
    );
    
    // Revoke each one
    for (final userSession in sessions) {
      await UserSession.db.updateRow(
        session,
        userSession.copyWith(
          isActive: false,
          revokedAt: DateTime.now(),
        ),
      );
      
      if (userSession.authKeyId != null) {
        await _invalidateAuthKey(session, userSession.authKeyId!);
      }
    }
    
    return sessions.length;
  }
  
  /// Update last activity for a session
  Future<void> updateActivity(
    Session session,
    int sessionId,
  ) async {
    final userSession = await UserSession.db.findById(session, sessionId);
    
    if (userSession != null && userSession.isActive) {
      await UserSession.db.updateRow(
        session,
        userSession.copyWith(
          lastActivityAt: DateTime.now(),
        ),
      );
    }
  }
  
  /// Find session by auth key
  Future<UserSession?> findSessionByAuthKey(
    Session session,
    int authKeyId,
  ) async {
    return UserSession.db.findFirstRow(
      session,
      where: (t) => t.authKeyId.equals(authKeyId) & t.isActive.equals(true),
    );
  }
  
  /// Clean up old inactive sessions
  Future<int> cleanupOldSessions(
    Session session, {
    Duration olderThan = const Duration(days: 30),
  }) async {
    final cutoff = DateTime.now().subtract(olderThan);
    
    return UserSession.db.deleteWhere(
      session,
      where: (t) => t.isActive.equals(false) & 
                    t.revokedAt.lessThan(cutoff),
    );
  }
  
  // Helper methods
  
  Future<String?> _getLocationFromIP(String ipAddress) async {
    // TODO: Integrate with IP geolocation service
    // e.g., MaxMind GeoIP, ipinfo.io, etc.
    return null;
  }
  
  Future<void> _invalidateAuthKey(Session session, int authKeyId) async {
    // TODO: Call Serverpod's auth module to invalidate the key
    // This depends on your Serverpod version and auth setup
  }
}