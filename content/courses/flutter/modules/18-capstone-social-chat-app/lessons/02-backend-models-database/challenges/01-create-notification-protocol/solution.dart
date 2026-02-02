# server/lib/src/protocol/notification.yaml
class: Notification
table: notifications
fields:
  # User who receives this notification
  userId: int, relation(parent=user_profiles)
  
  # Notification content
  title: String
  body: String
  notificationType: NotificationType
  
  # Related entity (what triggered this notification)
  relatedEntityId: int?
  relatedEntityType: String?
  
  # Deep link for navigation
  actionUrl: String?
  
  # Read tracking
  isRead: bool
  readAt: DateTime?
  
  # Push notification status
  isPushSent: bool
  pushSentAt: DateTime?
  
  # Timestamps
  createdAt: DateTime

indexes:
  # For fetching unread notifications
  notification_user_unread_idx:
    fields: userId, isRead, createdAt
  
  # For chronological listing
  notification_user_created_idx:
    fields: userId, createdAt
  
  # For finding notifications about specific entities
  notification_entity_idx:
    fields: relatedEntityType, relatedEntityId

---

# server/lib/src/protocol/notification_type.yaml
enum: NotificationType
values:
  - message       # New message received
  - like          # Someone liked your post/comment
  - comment       # Someone commented on your post
  - follow        # Someone followed you
  - mention       # Someone mentioned you
  - friendRequest # Friend request received
  - friendAccept  # Friend request accepted
  - system        # System announcement