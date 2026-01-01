// lib/utils/debouncer.dart
import 'dart:async';

class Debouncer {
  final Duration delay;
  Timer? _timer;
  
  Debouncer({required this.delay});
  
  void run(void Function() action) {
    _timer?.cancel();
    _timer = Timer(delay, action);
  }
  
  void dispose() {
    _timer?.cancel();
  }
}

// In registration_screen.dart, add these:

// State variables
enum UsernameAvailability { unchecked, checking, available, taken, invalid }
UsernameAvailability _usernameAvailability = UsernameAvailability.unchecked;
final _usernameController = TextEditingController();
final _debouncer = Debouncer(delay: const Duration(milliseconds: 500));

// Username validation
String? _validateUsername(String? value) {
  if (value == null || value.isEmpty) {
    return 'Please enter a username';
  }
  if (value.length < 3) {
    return 'Username must be at least 3 characters';
  }
  if (value.length > 20) {
    return 'Username cannot exceed 20 characters';
  }
  if (!RegExp(r'^[a-zA-Z0-9_]+$').hasMatch(value)) {
    return 'Only letters, numbers, and underscores allowed';
  }
  return null;
}

// Availability check
void _checkUsernameAvailability(String username) {
  if (_validateUsername(username) != null) {
    setState(() => _usernameAvailability = UsernameAvailability.invalid);
    return;
  }
  
  setState(() => _usernameAvailability = UsernameAvailability.checking);
  
  _debouncer.run(() async {
    try {
      final isAvailable = await ref.read(authServiceProvider)
          .checkUsernameAvailability(username);
      if (mounted) {
        setState(() {
          _usernameAvailability = isAvailable
              ? UsernameAvailability.available
              : UsernameAvailability.taken;
        });
      }
    } catch (e) {
      if (mounted) {
        setState(() => _usernameAvailability = UsernameAvailability.unchecked);
      }
    }
  });
}

// Username field widget
Widget _buildUsernameField() {
  return TextFormField(
    controller: _usernameController,
    decoration: InputDecoration(
      labelText: 'Username',
      hintText: 'Choose a unique username',
      prefixIcon: const Icon(Icons.alternate_email),
      border: const OutlineInputBorder(),
      suffixIcon: _buildUsernameSuffix(),
    ),
    onChanged: _checkUsernameAvailability,
    validator: _validateUsername,
  );
}

Widget? _buildUsernameSuffix() {
  switch (_usernameAvailability) {
    case UsernameAvailability.checking:
      return const Padding(
        padding: EdgeInsets.all(12),
        child: SizedBox(width: 20, height: 20, child: CircularProgressIndicator(strokeWidth: 2)),
      );
    case UsernameAvailability.available:
      return const Icon(Icons.check_circle, color: Colors.green);
    case UsernameAvailability.taken:
      return const Icon(Icons.cancel, color: Colors.red);
    default:
      return null;
  }
}