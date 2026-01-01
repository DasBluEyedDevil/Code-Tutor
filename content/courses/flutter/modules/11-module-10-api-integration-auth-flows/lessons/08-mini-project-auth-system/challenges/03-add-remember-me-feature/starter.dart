// Add to login_screen.dart

// State variable
bool _rememberMe = false;

// TODO: Add checkbox to the form
// TODO: Pass rememberMe to the login method
// TODO: Store/clear tokens based on preference

// Modify AuthService to handle remember me:
Future<AuthResult> login({
  required String email,
  required String password,
  bool rememberMe = false,  // New parameter
}) async {
  // TODO: Implement remember me logic
  // 1. If rememberMe is true, store tokens persistently
  // 2. If rememberMe is false, store tokens in memory only
  // 3. Store the email for auto-fill if rememberMe is true
  throw UnimplementedError();
}