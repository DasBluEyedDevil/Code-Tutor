---
type: "KEY_POINT"
title: "Architecture Overview - MVVM with Feature Folders"
---


**MVVM Pattern for Flutter**

We'll use MVVM (Model-View-ViewModel) adapted for Flutter with Riverpod:

- **Model**: Data classes from shared/ and local state
- **View**: Flutter widgets (stateless where possible)
- **ViewModel**: Riverpod providers (state + logic)

**Feature Folder Structure**

Each feature is self-contained:

```
features/
├── auth/
│   ├── data/
│   │   ├── auth_repository.dart
│   │   └── auth_local_storage.dart
│   ├── domain/
│   │   ├── auth_state.dart
│   │   └── user_model.dart
│   ├── presentation/
│   │   ├── screens/
│   │   │   ├── login_screen.dart
│   │   │   ├── register_screen.dart
│   │   │   └── forgot_password_screen.dart
│   │   ├── widgets/
│   │   │   ├── auth_form.dart
│   │   │   └── social_login_buttons.dart
│   │   └── providers/
│   │       └── auth_provider.dart
│   └── auth.dart           # Barrel export
│
├── chat/
│   ├── data/
│   │   ├── chat_repository.dart
│   │   ├── message_cache.dart
│   │   └── websocket_service.dart
│   ├── domain/
│   │   ├── message_model.dart
│   │   ├── room_model.dart
│   │   └── chat_state.dart
│   ├── presentation/
│   │   ├── screens/
│   │   │   ├── chat_list_screen.dart
│   │   │   ├── chat_room_screen.dart
│   │   │   └── new_chat_screen.dart
│   │   ├── widgets/
│   │   │   ├── message_bubble.dart
│   │   │   ├── chat_input.dart
│   │   │   ├── typing_indicator.dart
│   │   │   └── message_list.dart
│   │   └── providers/
│   │       ├── chat_list_provider.dart
│   │       ├── chat_room_provider.dart
│   │       └── typing_provider.dart
│   └── chat.dart
```

**Layer Responsibilities**

| Layer | Responsibility | Example |
|-------|----------------|--------|
| **data/** | API calls, caching, persistence | `ChatRepository.sendMessage()` |
| **domain/** | Business models, state definitions | `Message`, `ChatState` |
| **presentation/** | UI widgets, user interaction | `ChatRoomScreen`, `MessageBubble` |
| **providers/** | State management, business logic | `chatRoomProvider` |

