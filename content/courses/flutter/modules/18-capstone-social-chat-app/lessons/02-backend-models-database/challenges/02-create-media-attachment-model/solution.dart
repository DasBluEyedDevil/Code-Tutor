# server/lib/src/protocol/media_attachment.yaml
class: MediaAttachment
table: media_attachments
fields:
  # Polymorphic parent reference
  # Only one of these will be set
  postId: int?, relation(parent=posts, optional)
  messageId: int?, relation(parent=messages, optional)
  commentId: int?, relation(parent=comments, optional)
  
  # Uploader
  uploaderId: int, relation(parent=user_profiles)
  
  # Media type and status
  mediaType: MediaType
  uploadStatus: UploadStatus
  
  # URLs
  url: String
  thumbnailUrl: String?
  
  # File metadata
  fileName: String
  mimeType: String
  fileSize: int         # in bytes
  
  # Image/video dimensions
  width: int?
  height: int?
  
  # Audio/video duration in seconds
  duration: double?
  
  # For ordering multiple attachments
  sortOrder: int
  
  # Alt text for accessibility
  altText: String?
  
  # Processing metadata
  processingError: String?
  processedAt: DateTime?
  
  # Timestamps
  createdAt: DateTime
  updatedAt: DateTime?

indexes:
  # Find attachments for a post
  media_post_idx:
    fields: postId, sortOrder
  
  # Find attachments for a message
  media_message_idx:
    fields: messageId, sortOrder
  
  # Find attachments for a comment
  media_comment_idx:
    fields: commentId
  
  # Find pending uploads for processing
  media_status_idx:
    fields: uploadStatus, createdAt
  
  # Find user's uploads
  media_uploader_idx:
    fields: uploaderId, createdAt

---

# server/lib/src/protocol/media_type.yaml
enum: MediaType
values:
  - image     # jpg, png, gif, webp
  - video     # mp4, webm, mov
  - audio     # mp3, wav, ogg
  - document  # pdf, doc, txt
  - gif       # animated GIF (special handling)

---

# server/lib/src/protocol/upload_status.yaml
enum: UploadStatus
values:
  - pending     # Upload initiated, awaiting file
  - uploading   # File transfer in progress
  - processing  # Generating thumbnails, transcoding
  - ready       # Fully processed and available
  - failed      # Processing failed