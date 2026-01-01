---
type: "THEORY"
title: "UI Implementation"
---

With the state management in place, building the UI is straightforward. You need two key components: status badges for presence and a typing indicator.

**Status Badge:**

A small colored dot indicating online (green), away (yellow), or offline (gray) status.

**Typing Indicator:**

Animated dots with text showing who is typing.

**Best Practices:**
- Use subtle animations that don't distract
- Position indicators consistently
- Handle multiple typing users gracefully
- Provide accessibility labels



```dart
// presence_badge.dart
class PresenceBadge extends StatelessWidget {
  final int userId;
  final double size;
  final bool showBorder;
  
  const PresenceBadge({
    required this.userId,
    this.size = 12,
    this.showBorder = true,
    super.key,
  });
  
  @override
  Widget build(BuildContext context) {
    return Consumer<PresenceStateManager>(
      builder: (context, presence, child) {
        final status = presence.getStatus(userId);
        
        return Semantics(
          label: _getAccessibilityLabel(status),
          child: Container(
            width: size,
            height: size,
            decoration: BoxDecoration(
              color: _getColor(status),
              shape: BoxShape.circle,
              border: showBorder
                  ? Border.all(
                      color: Theme.of(context).scaffoldBackgroundColor,
                      width: 2,
                    )
                  : null,
            ),
          ),
        );
      },
    );
  }
  
  Color _getColor(PresenceStatus status) {
    switch (status) {
      case PresenceStatus.online:
        return Colors.green;
      case PresenceStatus.away:
        return Colors.amber;
      case PresenceStatus.offline:
        return Colors.grey;
    }
  }
  
  String _getAccessibilityLabel(PresenceStatus status) {
    switch (status) {
      case PresenceStatus.online:
        return 'Online';
      case PresenceStatus.away:
        return 'Away';
      case PresenceStatus.offline:
        return 'Offline';
    }
  }
}

// Avatar with presence badge
class AvatarWithPresence extends StatelessWidget {
  final int userId;
  final String? imageUrl;
  final String name;
  final double radius;
  
  const AvatarWithPresence({
    required this.userId,
    required this.name,
    this.imageUrl,
    this.radius = 24,
    super.key,
  });
  
  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        CircleAvatar(
          radius: radius,
          backgroundImage: imageUrl != null ? NetworkImage(imageUrl!) : null,
          child: imageUrl == null ? Text(name[0].toUpperCase()) : null,
        ),
        Positioned(
          right: 0,
          bottom: 0,
          child: PresenceBadge(userId: userId),
        ),
      ],
    );
  }
}

// typing_indicator_widget.dart
class TypingIndicatorWidget extends StatefulWidget {
  final String conversationId;
  
  const TypingIndicatorWidget({
    required this.conversationId,
    super.key,
  });
  
  @override
  State<TypingIndicatorWidget> createState() => _TypingIndicatorWidgetState();
}

class _TypingIndicatorWidgetState extends State<TypingIndicatorWidget>
    with TickerProviderStateMixin {
  late List<AnimationController> _dotControllers;
  late List<Animation<double>> _dotAnimations;
  
  @override
  void initState() {
    super.initState();
    _initAnimations();
  }
  
  void _initAnimations() {
    _dotControllers = List.generate(
      3,
      (index) => AnimationController(
        vsync: this,
        duration: const Duration(milliseconds: 600),
      ),
    );
    
    _dotAnimations = _dotControllers.map((controller) {
      return Tween<double>(begin: 0, end: -8).animate(
        CurvedAnimation(
          parent: controller,
          curve: Curves.easeInOut,
        ),
      );
    }).toList();
    
    // Start staggered animations
    for (var i = 0; i < _dotControllers.length; i++) {
      Future.delayed(Duration(milliseconds: i * 150), () {
        if (mounted) {
          _dotControllers[i].repeat(reverse: true);
        }
      });
    }
  }
  
  @override
  Widget build(BuildContext context) {
    return Consumer<PresenceStateManager>(
      builder: (context, presence, child) {
        final typingUsers = presence.getTypingUsers(widget.conversationId);
        
        if (typingUsers.isEmpty) {
          return const SizedBox.shrink();
        }
        
        return AnimatedContainer(
          duration: const Duration(milliseconds: 200),
          padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
          child: Row(
            children: [
              // Avatars of typing users (max 3)
              ...typingUsers.take(3).map((user) => Padding(
                padding: const EdgeInsets.only(right: 4),
                child: CircleAvatar(
                  radius: 12,
                  backgroundImage: user.avatarUrl != null
                      ? NetworkImage(user.avatarUrl!)
                      : null,
                  child: user.avatarUrl == null
                      ? Text(user.name[0], style: TextStyle(fontSize: 10))
                      : null,
                ),
              )),
              const SizedBox(width: 8),
              // Animated dots
              Row(
                mainAxisSize: MainAxisSize.min,
                children: List.generate(3, (index) {
                  return AnimatedBuilder(
                    animation: _dotAnimations[index],
                    builder: (context, child) {
                      return Transform.translate(
                        offset: Offset(0, _dotAnimations[index].value),
                        child: Container(
                          width: 6,
                          height: 6,
                          margin: const EdgeInsets.symmetric(horizontal: 2),
                          decoration: BoxDecoration(
                            color: Theme.of(context).colorScheme.primary,
                            shape: BoxShape.circle,
                          ),
                        ),
                      );
                    },
                  );
                }),
              ),
              const SizedBox(width: 8),
              // Typing text
              Flexible(
                child: Text(
                  _buildTypingText(typingUsers),
                  style: Theme.of(context).textTheme.bodySmall?.copyWith(
                    color: Theme.of(context).colorScheme.outline,
                    fontStyle: FontStyle.italic,
                  ),
                  overflow: TextOverflow.ellipsis,
                ),
              ),
            ],
          ),
        );
      },
    );
  }
  
  String _buildTypingText(List<TypingUser> users) {
    if (users.isEmpty) return '';
    if (users.length == 1) {
      return '${users[0].name} is typing...';
    }
    if (users.length == 2) {
      return '${users[0].name} and ${users[1].name} are typing...';
    }
    return '${users[0].name} and ${users.length - 1} others are typing...';
  }
  
  @override
  void dispose() {
    for (final controller in _dotControllers) {
      controller.dispose();
    }
    super.dispose();
  }
}
```
