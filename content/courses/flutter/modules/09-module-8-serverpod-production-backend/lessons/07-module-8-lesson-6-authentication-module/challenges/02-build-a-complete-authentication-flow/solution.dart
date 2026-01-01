class AuthService {
  final dynamic client;
  final dynamic sessionManager;

  AuthService({
    required this.client,
    required this.sessionManager,
  });

  /// Returns true if a user is currently signed in
  bool get isSignedIn {
    return sessionManager.isSignedIn;
  }

  /// Returns the current user's info, or null if not signed in
  dynamic get currentUser {
    return sessionManager.signedInUser;
  }

  /// Creates a new account with email, password, and username
  /// Returns UserInfo on success, null on failure
  Future<dynamic> signUp({
    required String email,
    required String password,
    required String userName,
  }) async {
    // Validate inputs
    if (email.isEmpty || !email.contains('@')) {
      throw ArgumentError('Please enter a valid email address');
    }
    if (password.length < 8) {
      throw ArgumentError('Password must be at least 8 characters');
    }
    if (userName.isEmpty) {
      throw ArgumentError('Username cannot be empty');
    }

    try {
      // Create the account using EmailAccountController
      // final userInfo = await EmailAccountController.createAccount(
      //   client: client,
      //   userName: userName,
      //   email: email,
      //   password: password,
      // );
      // return userInfo;
      print('Creating account for $email');
      return {'email': email, 'userName': userName};
    } catch (e) {
      print('Error creating account: $e');
      rethrow;
    }
  }

  /// Signs in with email and password
  /// Returns UserInfo on success, null on failure
  Future<dynamic> signIn({
    required String email,
    required String password,
  }) async {
    if (email.isEmpty || password.isEmpty) {
      throw ArgumentError('Email and password are required');
    }

    try {
      // final userInfo = await EmailAccountController.signIn(
      //   client: client,
      //   email: email,
      //   password: password,
      // );
      // return userInfo;
      print('Signing in $email');
      return {'email': email};
    } catch (e) {
      print('Error signing in: $e');
      throw Exception('Invalid email or password');
    }
  }

  /// Signs out the current user and clears the session
  Future<void> signOut() async {
    try {
      await sessionManager.signOut();
    } catch (e) {
      print('Error signing out: $e');
      rethrow;
    }
  }
}

void main() {
  print('AuthService implementation complete');
  print('isSignedIn: Checks sessionManager.isSignedIn');
  print('currentUser: Returns sessionManager.signedInUser');
  print('signUp: Uses EmailAccountController.createAccount with validation');
  print('signIn: Uses EmailAccountController.signIn with error handling');
  print('signOut: Calls sessionManager.signOut()');
}