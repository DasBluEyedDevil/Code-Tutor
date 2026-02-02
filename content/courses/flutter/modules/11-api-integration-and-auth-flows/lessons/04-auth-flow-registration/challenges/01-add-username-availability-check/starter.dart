// lib/utils/debouncer.dart
import 'dart:async';

class Debouncer {
  final Duration delay;
  Timer? _timer;
  
  Debouncer({required this.delay});
  
  void run(void Function() action) {
    // TODO: Implement debounce logic
  }
  
  void dispose() {
    // TODO: Cancel any pending timer
  }
}

// In your registration screen, add:
// - Username TextFormField with availability indicator
// - _checkUsernameAvailability method
// - _usernameAvailability state variable