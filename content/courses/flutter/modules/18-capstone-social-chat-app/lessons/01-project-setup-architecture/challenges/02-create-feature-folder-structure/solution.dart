// app/lib/features/auth/auth.dart
/// Authentication feature barrel export
/// 
/// Provides user authentication functionality including:
/// - Email/password login and registration
/// - Session management
/// - Token refresh
/// - Password reset
/// 
/// Usage:
/// ```dart
/// import 'package:social_chat_app/features/auth/auth.dart';
/// ```
library auth;

// Data layer - repositories and data sources
export 'data/auth_repository.dart';
export 'data/auth_local_storage.dart';

// Domain layer - models and state
export 'domain/auth_state.dart';
export 'domain/user_credentials.dart';

// Presentation layer - screens
export 'presentation/screens/login_screen.dart';
export 'presentation/screens/register_screen.dart';
export 'presentation/screens/forgot_password_screen.dart';

// Presentation layer - widgets
export 'presentation/widgets/auth_form.dart';
export 'presentation/widgets/password_field.dart';
export 'presentation/widgets/social_login_buttons.dart';

// Presentation layer - providers
export 'presentation/providers/auth_provider.dart';
export 'presentation/providers/login_form_provider.dart';

// =====================================================

// app/lib/features/chat/chat.dart
/// Chat feature barrel export
/// 
/// Provides real-time messaging functionality including:
/// - Direct messages (one-on-one)
/// - Group chats with multiple participants
/// - Typing indicators
/// - Read receipts
/// - Message history with pagination
/// - Image and file sharing
/// 
/// Usage:
/// ```dart
/// import 'package:social_chat_app/features/chat/chat.dart';
/// ```
library chat;

// Data layer - repositories and services
export 'data/chat_repository.dart';
export 'data/message_cache.dart';
export 'data/websocket_service.dart';

// Domain layer - models and state
export 'domain/message_model.dart';
export 'domain/chat_room_model.dart';
export 'domain/chat_state.dart';
export 'domain/typing_state.dart';

// Presentation layer - screens
export 'presentation/screens/chat_list_screen.dart';
export 'presentation/screens/chat_room_screen.dart';
export 'presentation/screens/new_chat_screen.dart';
export 'presentation/screens/group_info_screen.dart';

// Presentation layer - widgets
export 'presentation/widgets/message_bubble.dart';
export 'presentation/widgets/chat_input.dart';
export 'presentation/widgets/typing_indicator.dart';
export 'presentation/widgets/message_list.dart';
export 'presentation/widgets/chat_list_tile.dart';
export 'presentation/widgets/read_receipt_indicator.dart';

// Presentation layer - providers
export 'presentation/providers/chat_list_provider.dart';
export 'presentation/providers/chat_room_provider.dart';
export 'presentation/providers/typing_provider.dart';
export 'presentation/providers/message_input_provider.dart';