---
type: "THEORY"
title: "Sending Notifications"
---


### Method 1: Firebase Console (Manual)

1. Go to Firebase Console → Cloud Messaging
2. Click **"Send your first message"**
3. Enter:
   - **Notification title**: "New Message!"
   - **Notification text**: "You have a new message from John"
4. Click **"Send test message"**
5. Paste your FCM token
6. Click **"Test"**

### Method 2: Send to Topics (Best for Broadcasts)


Then send via Firebase Console to "news" topic.

### Method 3: Send via Cloud Functions (Production)

Create a Cloud Function to send notifications:




```javascript
// Firebase Cloud Function (JavaScript/TypeScript)
const functions = require('firebase-functions');
const admin = require('firebase-admin');
admin.initializeApp();

exports.sendNotificationOnNewMessage = functions.firestore
  .document('chatRooms/{chatRoomId}/messages/{messageId}')
  .onCreate(async (snapshot, context) => {
    const message = snapshot.data();

    // Get recipient's FCM token
    const recipientDoc = await admin.firestore()
      .collection('users')
      .doc(message.recipientId)
      .get();

    const fcmTokens = recipientDoc.data().fcmTokens || [];

    if (fcmTokens.length === 0) return;

    // Send notification
    const payload = {
      notification: {
        title: 'New Message',
        body: `${message.senderName}: ${message.text}`,
      },
      data: {
        chatRoomId: context.params.chatRoomId,
        messageId: context.params.messageId,
      },
    };

    await admin.messaging().sendToDevice(fcmTokens, payload);
  });
```
