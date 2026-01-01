---
type: "EXAMPLE"
title: "Generating Serverpod Code"
---


**Running Code Generation**

After defining protocols, generate the Dart code:

```bash
# From the server directory
cd server

# Generate protocol code
serverpod generate

# This creates:
# - shared/lib/src/protocol/protocol.dart (barrel file)
# - shared/lib/src/protocol/*.dart (model classes)
# - server/lib/src/generated/*.dart (server-side code)
```

**Generated Model Example**

The YAML protocol generates a fully-featured Dart class:



```dart
// shared/lib/src/protocol/message.dart (auto-generated)
import 'package:serverpod_serialization/serverpod_serialization.dart';

enum MessageType with SerializableEntity {
  text,
  image,
  file,
  system;
  
  static MessageType fromJson(int index) => values[index];
  
  @override
  int toJson() => index;
}

class Message extends SerializableEntity {
  Message({
    required this.id,
    required this.roomId,
    required this.senderId,
    required this.content,
    required this.messageType,
    this.replyToId,
    required this.isEdited,
    required this.isDeleted,
    required this.readBy,
    required this.createdAt,
    this.updatedAt,
  });
  
  final int id;
  final int roomId;
  final int senderId;
  final String content;
  final MessageType messageType;
  final int? replyToId;
  final bool isEdited;
  final bool isDeleted;
  final List<int> readBy;
  final DateTime createdAt;
  final DateTime? updatedAt;
  
  // Serverpod generates these methods:
  // - fromJson()
  // - toJson()
  // - copyWith()
  // - toString()
  // - == operator
  // - hashCode
  
  @override
  Map<String, dynamic> toJson() => {
    'id': id,
    'roomId': roomId,
    'senderId': senderId,
    'content': content,
    'messageType': messageType.toJson(),
    'replyToId': replyToId,
    'isEdited': isEdited,
    'isDeleted': isDeleted,
    'readBy': readBy,
    'createdAt': createdAt.toIso8601String(),
    'updatedAt': updatedAt?.toIso8601String(),
  };
  
  Message copyWith({
    int? id,
    int? roomId,
    int? senderId,
    String? content,
    MessageType? messageType,
    int? replyToId,
    bool? isEdited,
    bool? isDeleted,
    List<int>? readBy,
    DateTime? createdAt,
    DateTime? updatedAt,
  }) {
    return Message(
      id: id ?? this.id,
      roomId: roomId ?? this.roomId,
      senderId: senderId ?? this.senderId,
      content: content ?? this.content,
      messageType: messageType ?? this.messageType,
      replyToId: replyToId ?? this.replyToId,
      isEdited: isEdited ?? this.isEdited,
      isDeleted: isDeleted ?? this.isDeleted,
      readBy: readBy ?? this.readBy,
      createdAt: createdAt ?? this.createdAt,
      updatedAt: updatedAt ?? this.updatedAt,
    );
  }
}
```
