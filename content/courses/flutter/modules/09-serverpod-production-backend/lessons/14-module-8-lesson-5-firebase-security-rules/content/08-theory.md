---
type: "THEORY"
title: "Testing Security Rules"
---


### 1. Firebase Console Rules Playground

1. Go to Firebase Console → Firestore → Rules
2. Click **"Rules Playground"** tab
3. Simulate requests with different auth states

**Example test**:

### 2. Firebase Emulator Suite (Local Testing)


Then in your Flutter app:




```dart
// main.dart
import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:firebase_storage/firebase_storage.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
  );

  // Use emulators in debug mode
  if (kDebugMode) {
    FirebaseFirestore.instance.useFirestoreEmulator('localhost', 8080);
    FirebaseStorage.instance.useStorageEmulator('localhost', 9199);
  }

  runApp(const MyApp());
}
```
