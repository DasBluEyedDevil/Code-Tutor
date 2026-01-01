---
type: "EXAMPLE"
title: "Edit Profile Screen"
---


**Building the EditProfileScreen**

The EditProfileScreen provides editable fields for name, bio, and website, allows updating the profile picture with image picker integration, and includes save/cancel actions with proper loading states.



```dart
// lib/features/profile/presentation/screens/edit_profile_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:image_picker/image_picker.dart';
import 'package:cached_network_image/cached_network_image.dart';
import 'dart:io';
import '../../providers/profile_provider.dart';
import '../../providers/edit_profile_provider.dart';

class EditProfileScreen extends ConsumerStatefulWidget {
  const EditProfileScreen({super.key});

  @override
  ConsumerState<EditProfileScreen> createState() => _EditProfileScreenState();
}

class _EditProfileScreenState extends ConsumerState<EditProfileScreen> {
  final _formKey = GlobalKey<FormState>();
  final _displayNameController = TextEditingController();
  final _bioController = TextEditingController();
  final _websiteController = TextEditingController();
  final _imagePicker = ImagePicker();

  String? _selectedImagePath;
  bool _isLoading = false;
  bool _hasChanges = false;

  @override
  void initState() {
    super.initState();
    _initializeFields();
  }

  void _initializeFields() {
    final profile = ref.read(currentUserProfileProvider).valueOrNull;
    if (profile != null) {
      _displayNameController.text = profile.displayName;
      _bioController.text = profile.bio ?? '';
      _websiteController.text = profile.website ?? '';
    }

    // Listen for changes
    _displayNameController.addListener(_onFieldChanged);
    _bioController.addListener(_onFieldChanged);
    _websiteController.addListener(_onFieldChanged);
  }

  void _onFieldChanged() {
    final profile = ref.read(currentUserProfileProvider).valueOrNull;
    if (profile == null) return;

    final hasChanges = _displayNameController.text != profile.displayName ||
        _bioController.text != (profile.bio ?? '') ||
        _websiteController.text != (profile.website ?? '') ||
        _selectedImagePath != null;

    if (hasChanges != _hasChanges) {
      setState(() => _hasChanges = hasChanges);
    }
  }

  @override
  void dispose() {
    _displayNameController.dispose();
    _bioController.dispose();
    _websiteController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final profileAsync = ref.watch(currentUserProfileProvider);
    final theme = Theme.of(context);

    return PopScope(
      canPop: !_hasChanges,
      onPopInvokedWithResult: (didPop, result) {
        if (!didPop && _hasChanges) {
          _showDiscardDialog();
        }
      },
      child: Scaffold(
        appBar: AppBar(
          title: const Text('Edit Profile'),
          leading: IconButton(
            icon: const Icon(Icons.close),
            onPressed: () {
              if (_hasChanges) {
                _showDiscardDialog();
              } else {
                Navigator.of(context).pop();
              }
            },
          ),
          actions: [
            TextButton(
              onPressed: _hasChanges && !_isLoading ? _saveProfile : null,
              child: _isLoading
                  ? const SizedBox(
                      width: 20,
                      height: 20,
                      child: CircularProgressIndicator(strokeWidth: 2),
                    )
                  : const Text('Save'),
            ),
          ],
        ),
        body: profileAsync.when(
          loading: () => const Center(child: CircularProgressIndicator()),
          error: (error, _) => Center(child: Text('Error: $error')),
          data: (profile) => Form(
            key: _formKey,
            child: ListView(
              padding: const EdgeInsets.all(16),
              children: [
                // Avatar section
                Center(
                  child: Stack(
                    children: [
                      GestureDetector(
                        onTap: _showImagePickerOptions,
                        child: CircleAvatar(
                          radius: 50,
                          backgroundColor: theme.colorScheme.primaryContainer,
                          backgroundImage: _selectedImagePath != null
                              ? FileImage(File(_selectedImagePath!))
                              : (profile.avatarUrl != null
                                  ? CachedNetworkImageProvider(
                                      profile.avatarUrl!,
                                    )
                                  : null),
                          child: _selectedImagePath == null &&
                                  profile.avatarUrl == null
                              ? Text(
                                  profile.displayName[0].toUpperCase(),
                                  style: TextStyle(
                                    fontSize: 36,
                                    color:
                                        theme.colorScheme.onPrimaryContainer,
                                  ),
                                )
                              : null,
                        ),
                      ),
                      Positioned(
                        bottom: 0,
                        right: 0,
                        child: Container(
                          padding: const EdgeInsets.all(4),
                          decoration: BoxDecoration(
                            color: theme.colorScheme.primary,
                            shape: BoxShape.circle,
                          ),
                          child: Icon(
                            Icons.camera_alt,
                            size: 20,
                            color: theme.colorScheme.onPrimary,
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
                const SizedBox(height: 8),
                Center(
                  child: TextButton(
                    onPressed: _showImagePickerOptions,
                    child: const Text('Change photo'),
                  ),
                ),
                const SizedBox(height: 24),

                // Display name field
                TextFormField(
                  controller: _displayNameController,
                  decoration: const InputDecoration(
                    labelText: 'Name',
                    border: OutlineInputBorder(),
                  ),
                  validator: (value) {
                    if (value == null || value.trim().isEmpty) {
                      return 'Name is required';
                    }
                    if (value.length > 50) {
                      return 'Name must be 50 characters or less';
                    }
                    return null;
                  },
                  textInputAction: TextInputAction.next,
                ),
                const SizedBox(height: 16),

                // Bio field
                TextFormField(
                  controller: _bioController,
                  decoration: const InputDecoration(
                    labelText: 'Bio',
                    border: OutlineInputBorder(),
                    alignLabelWithHint: true,
                  ),
                  maxLines: 4,
                  maxLength: 150,
                  validator: (value) {
                    if (value != null && value.length > 150) {
                      return 'Bio must be 150 characters or less';
                    }
                    return null;
                  },
                ),
                const SizedBox(height: 16),

                // Website field
                TextFormField(
                  controller: _websiteController,
                  decoration: const InputDecoration(
                    labelText: 'Website',
                    border: OutlineInputBorder(),
                    hintText: 'https://example.com',
                  ),
                  keyboardType: TextInputType.url,
                  validator: (value) {
                    if (value != null && value.isNotEmpty) {
                      final uri = Uri.tryParse(value);
                      if (uri == null || !uri.hasScheme) {
                        return 'Enter a valid URL';
                      }
                    }
                    return null;
                  },
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  void _showImagePickerOptions() {
    showModalBottomSheet(
      context: context,
      builder: (context) => SafeArea(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            ListTile(
              leading: const Icon(Icons.photo_library),
              title: const Text('Choose from gallery'),
              onTap: () {
                Navigator.pop(context);
                _pickImage(ImageSource.gallery);
              },
            ),
            ListTile(
              leading: const Icon(Icons.camera_alt),
              title: const Text('Take a photo'),
              onTap: () {
                Navigator.pop(context);
                _pickImage(ImageSource.camera);
              },
            ),
            if (_selectedImagePath != null ||
                ref.read(currentUserProfileProvider).valueOrNull?.avatarUrl !=
                    null)
              ListTile(
                leading: Icon(
                  Icons.delete,
                  color: Theme.of(context).colorScheme.error,
                ),
                title: Text(
                  'Remove photo',
                  style: TextStyle(
                    color: Theme.of(context).colorScheme.error,
                  ),
                ),
                onTap: () {
                  Navigator.pop(context);
                  _removePhoto();
                },
              ),
          ],
        ),
      ),
    );
  }

  Future<void> _pickImage(ImageSource source) async {
    try {
      final image = await _imagePicker.pickImage(
        source: source,
        maxWidth: 500,
        maxHeight: 500,
        imageQuality: 85,
      );

      if (image != null) {
        setState(() {
          _selectedImagePath = image.path;
          _hasChanges = true;
        });
      }
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Failed to pick image: $e')),
      );
    }
  }

  void _removePhoto() {
    setState(() {
      _selectedImagePath = null;
      _hasChanges = true;
    });
    // Note: Would need to track "remove avatar" state separately
  }

  Future<void> _saveProfile() async {
    if (!_formKey.currentState!.validate()) return;

    setState(() => _isLoading = true);

    try {
      // Upload new avatar if selected
      if (_selectedImagePath != null) {
        await ref
            .read(currentUserProfileProvider.notifier)
            .updateAvatar(_selectedImagePath!);
      }

      // Update profile fields
      await ref.read(currentUserProfileProvider.notifier).updateProfile(
            displayName: _displayNameController.text.trim(),
            bio: _bioController.text.trim(),
            website: _websiteController.text.trim(),
          );

      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Profile updated')),
        );
        Navigator.of(context).pop();
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text('Failed to update profile: $e')),
        );
      }
    } finally {
      if (mounted) {
        setState(() => _isLoading = false);
      }
    }
  }

  void _showDiscardDialog() {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Discard changes?'),
        content: const Text(
          'You have unsaved changes. Are you sure you want to discard them?',
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Keep editing'),
          ),
          FilledButton(
            onPressed: () {
              Navigator.pop(context);
              Navigator.of(this.context).pop();
            },
            child: const Text('Discard'),
          ),
        ],
      ),
    );
  }
}

---

// lib/features/profile/data/profile_repository.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../domain/user_profile.dart';
import '../../../core/providers/serverpod_client_provider.dart';

final profileRepositoryProvider = Provider<ProfileRepository>((ref) {
  return ProfileRepository(ref);
});

class ProfileRepository {
  final Ref _ref;

  ProfileRepository(this._ref);

  Future<UserProfile> getCurrentUserProfile() async {
    final client = _ref.read(serverpodClientProvider);
    final response = await client.user.getCurrentProfile();
    return UserProfile.fromJson(response.toJson());
  }

  Future<UserProfile> getUserProfile(String userId) async {
    final client = _ref.read(serverpodClientProvider);
    final response = await client.user.getProfile(userId: userId);
    return UserProfile.fromJson(response.toJson());
  }

  Future<UserProfile> updateProfile({
    String? displayName,
    String? bio,
    String? website,
  }) async {
    final client = _ref.read(serverpodClientProvider);
    final response = await client.user.updateProfile(
      displayName: displayName,
      bio: bio,
      website: website,
    );
    return UserProfile.fromJson(response.toJson());
  }

  Future<UserProfile> updateAvatar(String imagePath) async {
    final client = _ref.read(serverpodClientProvider);

    // Upload image first
    final imageUrl = await client.upload.uploadFile(imagePath);

    // Then update profile with new avatar URL
    final response = await client.user.updateAvatar(avatarUrl: imageUrl);
    return UserProfile.fromJson(response.toJson());
  }

  Future<void> followUser(String userId) async {
    final client = _ref.read(serverpodClientProvider);
    await client.user.follow(userId: userId);
  }

  Future<void> unfollowUser(String userId) async {
    final client = _ref.read(serverpodClientProvider);
    await client.user.unfollow(userId: userId);
  }

  Future<void> blockUser(String userId) async {
    final client = _ref.read(serverpodClientProvider);
    await client.user.block(userId: userId);
  }

  Future<void> unblockUser(String userId) async {
    final client = _ref.read(serverpodClientProvider);
    await client.user.unblock(userId: userId);
  }
}
```
