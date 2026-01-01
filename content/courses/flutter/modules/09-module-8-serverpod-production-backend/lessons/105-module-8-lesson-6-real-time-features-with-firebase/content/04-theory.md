---
type: "THEORY"
title: "Firebase Real-Time Capabilities"
---


### 1. Firestore Snapshots (Real-Time Listeners)

**When data changes**:
1. Firebase detects the change
2. Pushes update to all listening devices
3. Flutter rebuilds UI automatically (with StreamBuilder)

### 2. Firestore Realtime Database
- Legacy real-time database (still used for specific cases)
- Extremely low latency (< 100ms)
- JSON tree structure
- Good for: presence, typing indicators, live cursors

### 3. Firebase Cloud Messaging (FCM)
- Push notifications
- Background messaging
- Topic-based messaging



```dart
// Listen to document changes
firestore.collection('chats').doc('room1').snapshots()

// Listen to collection changes
firestore.collection('messages').snapshots()
```
