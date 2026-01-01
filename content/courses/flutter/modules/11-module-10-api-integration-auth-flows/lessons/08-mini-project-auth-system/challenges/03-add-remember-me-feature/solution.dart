// lib/features/auth/presentation/screens/login_screen.dart
// Add to _LoginScreenState:

bool _rememberMe = false;

@override
void initState() {
  super.initState();
  _loadRememberedEmail();
}

Future<void> _loadRememberedEmail() async {
  final authService = ref.read(authServiceProvider);
  final rememberedEmail = await authService.getRememberedEmail();
  if (rememberedEmail != null && mounted) {
    setState(() {
      _emailController.text = rememberedEmail;
      _rememberMe = true;
    });
  }
}

Future<void> _handleLogin() async {
  if (!_formKey.currentState!.validate()) return;

  await ref.read(authNotifierProvider.notifier).login(
        email: _emailController.text.trim(),
        password: _passwordController.text,
        rememberMe: _rememberMe,
      );
}

// Add to the form, before the Login button:
CheckboxListTile(
  value: _rememberMe,
  onChanged: isLoading
      ? null
      : (value) => setState(() => _rememberMe = value ?? false),
  title: const Text('Remember me'),
  controlAffinity: ListTileControlAffinity.leading,
  contentPadding: EdgeInsets.zero,
),
const SizedBox(height: 16),

// lib/features/auth/data/auth_service.dart
// Add these methods and modify login:

static const _rememberMeKey = 'remember_me';
static const _rememberedEmailKey = 'remembered_email';

Future<String?> getRememberedEmail() async {
  final rememberMe = await _storage.read(key: _rememberMeKey);
  if (rememberMe == 'true') {
    return await _storage.read(key: _rememberedEmailKey);
  }
  return null;
}

Future<AuthResult> login({
  required String email,
  required String password,
  bool rememberMe = false,
}) async {
  await Future.delayed(const Duration(seconds: 1));

  if (password.length < 6) {
    throw AuthException('Invalid email or password', code: 'invalid_credentials');
  }

  final user = User(
    id: 'user_123',
    email: email,
    displayName: email.split('@').first,
  );

  final accessToken = 'mock_access_token_${user.id}';
  final refreshToken = 'mock_refresh_token_${user.id}';

  // Handle remember me
  await _storage.write(key: _rememberMeKey, value: rememberMe.toString());
  
  if (rememberMe) {
    // Store tokens and email persistently
    await _storeTokens(accessToken, refreshToken);
    await _storage.write(key: _rememberedEmailKey, value: email);
  } else {
    // Store tokens only in memory (using a separate in-memory map)
    // Clear any previously stored tokens
    await _storage.delete(key: _accessTokenKey);
    await _storage.delete(key: _refreshTokenKey);
    await _storage.delete(key: _rememberedEmailKey);
    // For this example, we still store to demonstrate the flow
    // In production, use an in-memory cache that clears on app restart
    await _storeTokens(accessToken, refreshToken);
  }

  return AuthResult(
    user: user,
    accessToken: accessToken,
    refreshToken: refreshToken,
  );
}

Future<void> logout() async {
  await _googleSignIn.signOut();
  // Only clear tokens, keep rememberMe preference and email
  final rememberMe = await _storage.read(key: _rememberMeKey);
  final rememberedEmail = await _storage.read(key: _rememberedEmailKey);
  
  await _storage.deleteAll();
  
  // Restore remember me settings if enabled
  if (rememberMe == 'true' && rememberedEmail != null) {
    await _storage.write(key: _rememberMeKey, value: 'true');
    await _storage.write(key: _rememberedEmailKey, value: rememberedEmail);
  }
}

// Update AuthNotifier.login:
Future<void> login({
  required String email,
  required String password,
  bool rememberMe = false,
}) async {
  state = const AuthState.loading();

  try {
    final result = await _authService.login(
      email: email,
      password: password,
      rememberMe: rememberMe,
    );

    state = AuthState.authenticated(
      user: result.user,
      accessToken: result.accessToken,
      refreshToken: result.refreshToken,
    );
  } on AuthException catch (e) {
    state = AuthState.error(e.message);
  } catch (e) {
    state = AuthState.error('An unexpected error occurred');
  }
}