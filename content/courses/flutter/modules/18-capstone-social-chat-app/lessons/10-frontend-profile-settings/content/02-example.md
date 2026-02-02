---
type: "EXAMPLE"
title: "Profile Screen"
---


**Building the ProfileScreen with User Stats and Posts Grid**

The ProfileScreen displays the user's avatar, bio, statistics (posts/followers/following), a grid of user posts, and contextual actions based on whether viewing own profile or another user's profile.



```dart
// lib/features/profile/presentation/screens/profile_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:cached_network_image/cached_network_image.dart';
import '../../domain/user_profile.dart';
import '../../providers/profile_provider.dart';
import '../../providers/current_user_provider.dart';
import '../widgets/profile_header.dart';
import '../widgets/profile_stats.dart';
import '../widgets/profile_posts_grid.dart';
import '../widgets/profile_shimmer.dart';

class ProfileScreen extends ConsumerWidget {
  final String? userId;

  const ProfileScreen({super.key, this.userId});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final currentUser = ref.watch(currentUserProvider);
    final isOwnProfile = userId == null || userId == currentUser?.id;

    final profileAsync = isOwnProfile
        ? ref.watch(currentUserProfileProvider)
        : ref.watch(userProfileProvider(userId!));

    return Scaffold(
      body: profileAsync.when(
        loading: () => const ProfileShimmer(),
        error: (error, stack) => _buildErrorState(context, ref, error),
        data: (profile) => _buildProfileContent(
          context,
          ref,
          profile,
          isOwnProfile,
        ),
      ),
    );
  }

  Widget _buildProfileContent(
    BuildContext context,
    WidgetRef ref,
    UserProfile profile,
    bool isOwnProfile,
  ) {
    return NestedScrollView(
      headerSliverBuilder: (context, innerBoxIsScrolled) {
        return [
          SliverAppBar(
            expandedHeight: 120,
            floating: false,
            pinned: true,
            title: Text(profile.username),
            actions: [
              if (isOwnProfile)
                IconButton(
                  icon: const Icon(Icons.add_box_outlined),
                  onPressed: () => _navigateToCreatePost(context),
                  tooltip: 'Create post',
                )
              else
                IconButton(
                  icon: const Icon(Icons.more_vert),
                  onPressed: () => _showUserOptions(context, profile),
                  tooltip: 'Options',
                ),
              if (isOwnProfile)
                IconButton(
                  icon: const Icon(Icons.menu),
                  onPressed: () => _navigateToSettings(context),
                  tooltip: 'Settings',
                ),
            ],
          ),
        ];
      },
      body: RefreshIndicator(
        onRefresh: () async {
          if (isOwnProfile) {
            ref.invalidate(currentUserProfileProvider);
          } else {
            ref.invalidate(userProfileProvider(userId!));
          }
        },
        child: CustomScrollView(
          slivers: [
            SliverToBoxAdapter(
              child: ProfileHeader(
                profile: profile,
                isOwnProfile: isOwnProfile,
                onEditProfile: isOwnProfile
                    ? () => _navigateToEditProfile(context)
                    : null,
                onFollow: isOwnProfile
                    ? null
                    : () => _handleFollow(ref, profile),
                onMessage: isOwnProfile
                    ? null
                    : () => _navigateToChat(context, profile),
              ),
            ),
            SliverToBoxAdapter(
              child: ProfileStats(
                postsCount: profile.postsCount,
                followersCount: profile.followersCount,
                followingCount: profile.followingCount,
                onPostsTap: () {},
                onFollowersTap: () => _navigateToFollowers(context, profile),
                onFollowingTap: () => _navigateToFollowing(context, profile),
              ),
            ),
            if (profile.bio != null && profile.bio!.isNotEmpty)
              SliverToBoxAdapter(
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                    horizontal: 16,
                    vertical: 8,
                  ),
                  child: Text(
                    profile.bio!,
                    style: Theme.of(context).textTheme.bodyMedium,
                  ),
                ),
              ),
            if (profile.website != null && profile.website!.isNotEmpty)
              SliverToBoxAdapter(
                child: Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16),
                  child: GestureDetector(
                    onTap: () => _openWebsite(profile.website!),
                    child: Text(
                      profile.website!,
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.primary,
                        fontWeight: FontWeight.w500,
                      ),
                    ),
                  ),
                ),
              ),
            const SliverToBoxAdapter(
              child: SizedBox(height: 16),
            ),
            SliverToBoxAdapter(
              child: Divider(height: 1),
            ),
            ProfilePostsGrid(userId: profile.id),
          ],
        ),
      ),
    );
  }

  Widget _buildErrorState(BuildContext context, WidgetRef ref, Object error) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(
            Icons.error_outline,
            size: 64,
            color: Theme.of(context).colorScheme.error,
          ),
          const SizedBox(height: 16),
          Text('Failed to load profile'),
          const SizedBox(height: 16),
          FilledButton(
            onPressed: () {
              if (userId == null) {
                ref.invalidate(currentUserProfileProvider);
              } else {
                ref.invalidate(userProfileProvider(userId!));
              }
            },
            child: const Text('Retry'),
          ),
        ],
      ),
    );
  }

  Future<void> _handleFollow(WidgetRef ref, UserProfile profile) async {
    if (profile.isFollowing) {
      await ref.read(userProfileProvider(profile.id).notifier).unfollow();
    } else {
      await ref.read(userProfileProvider(profile.id).notifier).follow();
    }
  }

  void _navigateToEditProfile(BuildContext context) {
    Navigator.of(context).pushNamed('/profile/edit');
  }

  void _navigateToSettings(BuildContext context) {
    Navigator.of(context).pushNamed('/settings');
  }

  void _navigateToCreatePost(BuildContext context) {
    Navigator.of(context).pushNamed('/post/create');
  }

  void _navigateToChat(BuildContext context, UserProfile profile) {
    Navigator.of(context).pushNamed(
      '/chat/new',
      arguments: {'userId': profile.id},
    );
  }

  void _navigateToFollowers(BuildContext context, UserProfile profile) {
    Navigator.of(context).pushNamed(
      '/profile/${profile.id}/followers',
    );
  }

  void _navigateToFollowing(BuildContext context, UserProfile profile) {
    Navigator.of(context).pushNamed(
      '/profile/${profile.id}/following',
    );
  }

  void _openWebsite(String url) {
    // Use url_launcher to open website
  }

  void _showUserOptions(BuildContext context, UserProfile profile) {
    showModalBottomSheet(
      context: context,
      builder: (context) => SafeArea(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            ListTile(
              leading: const Icon(Icons.share),
              title: const Text('Share profile'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: const Icon(Icons.link),
              title: const Text('Copy profile link'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: Icon(
                Icons.block,
                color: Theme.of(context).colorScheme.error,
              ),
              title: Text(
                'Block user',
                style: TextStyle(
                  color: Theme.of(context).colorScheme.error,
                ),
              ),
              onTap: () {
                Navigator.pop(context);
                _confirmBlockUser(context, profile);
              },
            ),
            ListTile(
              leading: Icon(
                Icons.flag,
                color: Theme.of(context).colorScheme.error,
              ),
              title: Text(
                'Report user',
                style: TextStyle(
                  color: Theme.of(context).colorScheme.error,
                ),
              ),
              onTap: () => Navigator.pop(context),
            ),
          ],
        ),
      ),
    );
  }

  void _confirmBlockUser(BuildContext context, UserProfile profile) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: Text('Block ${profile.username}?'),
        content: const Text(
          'They won\'t be able to find your profile, posts, or send you messages.',
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          FilledButton(
            onPressed: () {
              Navigator.pop(context);
              // Block user
            },
            style: FilledButton.styleFrom(
              backgroundColor: Theme.of(context).colorScheme.error,
            ),
            child: const Text('Block'),
          ),
        ],
      ),
    );
  }
}

---

// lib/features/profile/presentation/widgets/profile_header.dart
import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
import '../../domain/user_profile.dart';

class ProfileHeader extends StatelessWidget {
  final UserProfile profile;
  final bool isOwnProfile;
  final VoidCallback? onEditProfile;
  final VoidCallback? onFollow;
  final VoidCallback? onMessage;

  const ProfileHeader({
    super.key,
    required this.profile,
    required this.isOwnProfile,
    this.onEditProfile,
    this.onFollow,
    this.onMessage,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Padding(
      padding: const EdgeInsets.all(16),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // Avatar
          CircleAvatar(
            radius: 44,
            backgroundColor: theme.colorScheme.primaryContainer,
            backgroundImage: profile.avatarUrl != null
                ? CachedNetworkImageProvider(profile.avatarUrl!)
                : null,
            child: profile.avatarUrl == null
                ? Text(
                    profile.displayName[0].toUpperCase(),
                    style: TextStyle(
                      fontSize: 32,
                      fontWeight: FontWeight.bold,
                      color: theme.colorScheme.onPrimaryContainer,
                    ),
                  )
                : null,
          ),
          const SizedBox(width: 20),

          // Name and actions
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  profile.displayName,
                  style: theme.textTheme.titleLarge?.copyWith(
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 4),
                Text(
                  '@${profile.username}',
                  style: theme.textTheme.bodyMedium?.copyWith(
                    color: theme.colorScheme.onSurfaceVariant,
                  ),
                ),
                const SizedBox(height: 12),
                _buildActionButtons(context, theme),
              ],
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildActionButtons(BuildContext context, ThemeData theme) {
    if (isOwnProfile) {
      return SizedBox(
        width: double.infinity,
        child: OutlinedButton(
          onPressed: onEditProfile,
          child: const Text('Edit Profile'),
        ),
      );
    }

    return Row(
      children: [
        Expanded(
          child: profile.isFollowing
              ? OutlinedButton(
                  onPressed: onFollow,
                  child: const Text('Following'),
                )
              : FilledButton(
                  onPressed: onFollow,
                  child: const Text('Follow'),
                ),
        ),
        const SizedBox(width: 8),
        Expanded(
          child: OutlinedButton(
            onPressed: onMessage,
            child: const Text('Message'),
          ),
        ),
      ],
    );
  }
}

---

// lib/features/profile/presentation/widgets/profile_stats.dart
import 'package:flutter/material.dart';

class ProfileStats extends StatelessWidget {
  final int postsCount;
  final int followersCount;
  final int followingCount;
  final VoidCallback onPostsTap;
  final VoidCallback onFollowersTap;
  final VoidCallback onFollowingTap;

  const ProfileStats({
    super.key,
    required this.postsCount,
    required this.followersCount,
    required this.followingCount,
    required this.onPostsTap,
    required this.onFollowersTap,
    required this.onFollowingTap,
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [
          _StatItem(
            count: postsCount,
            label: 'Posts',
            onTap: onPostsTap,
          ),
          _StatItem(
            count: followersCount,
            label: 'Followers',
            onTap: onFollowersTap,
          ),
          _StatItem(
            count: followingCount,
            label: 'Following',
            onTap: onFollowingTap,
          ),
        ],
      ),
    );
  }
}

class _StatItem extends StatelessWidget {
  final int count;
  final String label;
  final VoidCallback onTap;

  const _StatItem({
    required this.count,
    required this.label,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return InkWell(
      onTap: onTap,
      borderRadius: BorderRadius.circular(8),
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
        child: Column(
          children: [
            Text(
              _formatCount(count),
              style: theme.textTheme.titleLarge?.copyWith(
                fontWeight: FontWeight.bold,
              ),
            ),
            Text(
              label,
              style: theme.textTheme.bodySmall?.copyWith(
                color: theme.colorScheme.onSurfaceVariant,
              ),
            ),
          ],
        ),
      ),
    );
  }

  String _formatCount(int count) {
    if (count >= 1000000) {
      return '${(count / 1000000).toStringAsFixed(1)}M';
    } else if (count >= 1000) {
      return '${(count / 1000).toStringAsFixed(1)}K';
    }
    return count.toString();
  }
}

---

// lib/features/profile/presentation/widgets/profile_posts_grid.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:cached_network_image/cached_network_image.dart';
import '../../providers/user_posts_provider.dart';
import '../../../posts/domain/post.dart';

class ProfilePostsGrid extends ConsumerWidget {
  final String userId;

  const ProfilePostsGrid({super.key, required this.userId});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final postsAsync = ref.watch(userPostsProvider(userId));

    return postsAsync.when(
      loading: () => const SliverToBoxAdapter(
        child: Center(
          child: Padding(
            padding: EdgeInsets.all(32),
            child: CircularProgressIndicator(),
          ),
        ),
      ),
      error: (error, _) => SliverToBoxAdapter(
        child: Center(
          child: Text('Failed to load posts'),
        ),
      ),
      data: (posts) => posts.isEmpty
          ? SliverToBoxAdapter(
              child: _buildEmptyState(context),
            )
          : SliverGrid(
              gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
                crossAxisCount: 3,
                crossAxisSpacing: 2,
                mainAxisSpacing: 2,
              ),
              delegate: SliverChildBuilderDelegate(
                (context, index) {
                  final post = posts[index];
                  return _PostGridItem(
                    post: post,
                    onTap: () => _navigateToPost(context, post),
                  );
                },
                childCount: posts.length,
              ),
            ),
    );
  }

  Widget _buildEmptyState(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(48),
      child: Column(
        children: [
          Icon(
            Icons.photo_camera_outlined,
            size: 64,
            color: Theme.of(context).colorScheme.onSurfaceVariant,
          ),
          const SizedBox(height: 16),
          Text(
            'No posts yet',
            style: Theme.of(context).textTheme.titleMedium,
          ),
        ],
      ),
    );
  }

  void _navigateToPost(BuildContext context, Post post) {
    Navigator.of(context).pushNamed('/post/${post.id}');
  }
}

class _PostGridItem extends StatelessWidget {
  final Post post;
  final VoidCallback onTap;

  const _PostGridItem({
    required this.post,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onTap,
      child: Stack(
        fit: StackFit.expand,
        children: [
          CachedNetworkImage(
            imageUrl: post.thumbnailUrl ?? post.imageUrls.first,
            fit: BoxFit.cover,
            placeholder: (context, url) => Container(
              color: Theme.of(context).colorScheme.surfaceContainerHighest,
            ),
            errorWidget: (context, url, error) => Container(
              color: Theme.of(context).colorScheme.errorContainer,
              child: const Icon(Icons.broken_image),
            ),
          ),
          if (post.imageUrls.length > 1)
            Positioned(
              top: 8,
              right: 8,
              child: Icon(
                Icons.collections,
                color: Colors.white,
                size: 20,
                shadows: [
                  Shadow(
                    color: Colors.black54,
                    blurRadius: 4,
                  ),
                ],
              ),
            ),
          if (post.isVideo)
            Positioned(
              top: 8,
              right: 8,
              child: Icon(
                Icons.play_circle_outline,
                color: Colors.white,
                size: 24,
                shadows: [
                  Shadow(
                    color: Colors.black54,
                    blurRadius: 4,
                  ),
                ],
              ),
            ),
        ],
      ),
    );
  }
}

---

// lib/features/profile/domain/user_profile.dart
import 'package:freezed_annotation/freezed_annotation.dart';

part 'user_profile.freezed.dart';
part 'user_profile.g.dart';

@freezed
class UserProfile with _$UserProfile {
  const UserProfile._();

  const factory UserProfile({
    required String id,
    required String username,
    required String displayName,
    String? avatarUrl,
    String? bio,
    String? website,
    @Default(0) int postsCount,
    @Default(0) int followersCount,
    @Default(0) int followingCount,
    @Default(false) bool isFollowing,
    @Default(false) bool isFollowedBy,
    @Default(false) bool isBlocked,
    @Default(false) bool isPrivate,
    DateTime? createdAt,
  }) = _UserProfile;

  factory UserProfile.fromJson(Map<String, dynamic> json) =>
      _$UserProfileFromJson(json);

  bool get isMutualFollow => isFollowing && isFollowedBy;
}

---

// lib/features/profile/providers/profile_provider.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../domain/user_profile.dart';
import '../data/profile_repository.dart';

final currentUserProfileProvider =
    AsyncNotifierProvider<CurrentUserProfileNotifier, UserProfile>(() {
  return CurrentUserProfileNotifier();
});

class CurrentUserProfileNotifier extends AsyncNotifier<UserProfile> {
  @override
  Future<UserProfile> build() async {
    return await ref.read(profileRepositoryProvider).getCurrentUserProfile();
  }

  Future<void> updateProfile({
    String? displayName,
    String? bio,
    String? website,
  }) async {
    final currentProfile = state.valueOrNull;
    if (currentProfile == null) return;

    state = const AsyncLoading();

    try {
      final updated = await ref.read(profileRepositoryProvider).updateProfile(
            displayName: displayName,
            bio: bio,
            website: website,
          );
      state = AsyncData(updated);
    } catch (e, st) {
      state = AsyncError(e, st);
    }
  }

  Future<void> updateAvatar(String imagePath) async {
    try {
      final updated = await ref
          .read(profileRepositoryProvider)
          .updateAvatar(imagePath);
      state = AsyncData(updated);
    } catch (e) {
      rethrow;
    }
  }
}

final userProfileProvider = AsyncNotifierProvider.family<
    UserProfileNotifier, UserProfile, String>(() {
  return UserProfileNotifier();
});

class UserProfileNotifier extends FamilyAsyncNotifier<UserProfile, String> {
  @override
  Future<UserProfile> build(String userId) async {
    return await ref.read(profileRepositoryProvider).getUserProfile(userId);
  }

  Future<void> follow() async {
    final profile = state.valueOrNull;
    if (profile == null) return;

    // Optimistic update
    state = AsyncData(profile.copyWith(
      isFollowing: true,
      followersCount: profile.followersCount + 1,
    ));

    try {
      await ref.read(profileRepositoryProvider).followUser(arg);
    } catch (e) {
      // Revert on failure
      state = AsyncData(profile);
      rethrow;
    }
  }

  Future<void> unfollow() async {
    final profile = state.valueOrNull;
    if (profile == null) return;

    // Optimistic update
    state = AsyncData(profile.copyWith(
      isFollowing: false,
      followersCount: profile.followersCount - 1,
    ));

    try {
      await ref.read(profileRepositoryProvider).unfollowUser(arg);
    } catch (e) {
      // Revert on failure
      state = AsyncData(profile);
      rethrow;
    }
  }
}
```
