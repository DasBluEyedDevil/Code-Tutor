# server/lib/src/protocol/notification.yaml
# TODO: Define the Notification class
# Fields needed:
# - id (auto-generated)
# - userId (relation to user_profiles)
# - title
# - body
# - notificationType (enum)
# - relatedEntityId (optional - the post, message, user that triggered this)
# - relatedEntityType (optional - 'post', 'message', 'user', etc.)
# - isRead
# - readAt (optional)
# - createdAt
#
# Add indexes for:
# - userId + isRead (for fetching unread notifications)
# - userId + createdAt (for chronological listing)

class: Notification
table: notifications
fields:
  # TODO: Add fields here

indexes:
  # TODO: Add indexes here

---

# server/lib/src/protocol/notification_type.yaml
# TODO: Define the NotificationType enum
# Values: message, like, comment, follow, mention, system