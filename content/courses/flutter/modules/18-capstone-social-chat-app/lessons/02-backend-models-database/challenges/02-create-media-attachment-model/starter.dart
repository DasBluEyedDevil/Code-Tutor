# server/lib/src/protocol/media_attachment.yaml
# Design a flexible media attachment system
#
# Requirements:
# - Support images, videos, audio, and documents
# - Can be attached to posts, messages, or comments
# - Track upload and processing status
# - Store metadata like dimensions, duration, file size
# - Support ordering when multiple attachments exist
#
# Think about:
# - How to reference the parent entity (post/message/comment)
# - What metadata is needed for each media type
# - How to handle upload progress and failures

class: MediaAttachment
table: media_attachments
fields:
  # TODO: Define the schema

indexes:
  # TODO: Add appropriate indexes

---

# server/lib/src/protocol/media_type.yaml
# TODO: Define MediaType enum

---

# server/lib/src/protocol/upload_status.yaml
# TODO: Define UploadStatus enum