---
type: "EXAMPLE"
title: "Chat Screen Implementation"
---


**Building the Chat Screen with Reversed ListView**

The ChatScreen implements a reversed ListView for messages, a message input bar with send button, image attachment support, and a scroll-to-bottom FAB.



```dart
// lib/features/chat/presentation/screens/chat_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:image_picker/image_picker.dart';
import '../../domain/conversation.dart';
import '../../domain/message.dart';
import '../../providers/messages_provider.dart';
import '../../providers/chat_connection_provider.dart';
import '../widgets/message_bubble.dart';
import '../widgets/message_input_bar.dart';
import '../widgets/typing_indicator_bar.dart';
import '../widgets/chat_date_separator.dart';

class ChatScreen extends ConsumerStatefulWidget {
  final String conversationId;
  final Conversation? conversation;

  const ChatScreen({
    super.key,
    required this.conversationId,
    this.conversation,
  });

  @override
  ConsumerState<ChatScreen> createState() => _ChatScreenState();
}

class _ChatScreenState extends ConsumerState<ChatScreen> {
  final _scrollController = ScrollController();
  final _textController = TextEditingController();
  final _focusNode = FocusNode();
  final _imagePicker = ImagePicker();
  
  bool _showScrollToBottom = false;
  bool _isLoadingMore = false;

  @override
  void initState() {
    super.initState();
    _scrollController.addListener(_onScroll);
    // Mark conversation as read when opened
    WidgetsBinding.instance.addPostFrameCallback((_) {
      ref.read(messagesProvider(widget.conversationId).notifier).markAsRead();
    });
  }

  @override
  void dispose() {
    _scrollController.dispose();
    _textController.dispose();
    _focusNode.dispose();
    super.dispose();
  }

  void _onScroll() {
    // Show scroll-to-bottom FAB when scrolled up
    final showFab = _scrollController.offset > 200;
    if (showFab != _showScrollToBottom) {
      setState(() => _showScrollToBottom = showFab);
    }

    // Load more messages when near the top (remember: reversed list)
    if (_scrollController.position.pixels >=
        _scrollController.position.maxScrollExtent - 200) {
      _loadMoreMessages();
    }
  }

  Future<void> _loadMoreMessages() async {
    if (_isLoadingMore) return;
    setState(() => _isLoadingMore = true);
    
    await ref
        .read(messagesProvider(widget.conversationId).notifier)
        .loadMore();
    
    setState(() => _isLoadingMore = false);
  }

  void _scrollToBottom() {
    _scrollController.animateTo(
      0,
      duration: const Duration(milliseconds: 300),
      curve: Curves.easeOut,
    );
  }

  Future<void> _sendMessage() async {
    final text = _textController.text.trim();
    if (text.isEmpty) return;

    _textController.clear();
    await ref
        .read(messagesProvider(widget.conversationId).notifier)
        .sendMessage(text);
    
    _scrollToBottom();
  }

  Future<void> _pickImage() async {
    final image = await _imagePicker.pickImage(
      source: ImageSource.gallery,
      maxWidth: 1920,
      maxHeight: 1920,
      imageQuality: 85,
    );

    if (image != null) {
      await ref
          .read(messagesProvider(widget.conversationId).notifier)
          .sendImageMessage(image.path);
      _scrollToBottom();
    }
  }

  Future<void> _takePhoto() async {
    final image = await _imagePicker.pickImage(
      source: ImageSource.camera,
      maxWidth: 1920,
      maxHeight: 1920,
      imageQuality: 85,
    );

    if (image != null) {
      await ref
          .read(messagesProvider(widget.conversationId).notifier)
          .sendImageMessage(image.path);
      _scrollToBottom();
    }
  }

  @override
  Widget build(BuildContext context) {
    final messagesAsync = ref.watch(messagesProvider(widget.conversationId));
    final connectionState = ref.watch(chatConnectionProvider);
    final theme = Theme.of(context);

    return Scaffold(
      appBar: _buildAppBar(context, theme, connectionState),
      body: Column(
        children: [
          // Connection status banner
          if (connectionState != ConnectionStatus.connected)
            _buildConnectionBanner(theme, connectionState),

          // Messages list
          Expanded(
            child: messagesAsync.when(
              loading: () => const Center(child: CircularProgressIndicator()),
              error: (error, _) => _buildErrorState(context, error),
              data: (messages) => _buildMessagesList(context, messages),
            ),
          ),

          // Typing indicator
          const TypingIndicatorBar(),

          // Message input
          MessageInputBar(
            controller: _textController,
            focusNode: _focusNode,
            onSend: _sendMessage,
            onAttachImage: _pickImage,
            onTakePhoto: _takePhoto,
          ),
        ],
      ),
      floatingActionButton: _showScrollToBottom
          ? FloatingActionButton.small(
              onPressed: _scrollToBottom,
              child: const Icon(Icons.keyboard_arrow_down),
            )
          : null,
    );
  }

  PreferredSizeWidget _buildAppBar(
    BuildContext context,
    ThemeData theme,
    ConnectionStatus connectionState,
  ) {
    final conversation = widget.conversation;

    return AppBar(
      titleSpacing: 0,
      title: Row(
        children: [
          Stack(
            children: [
              CircleAvatar(
                radius: 18,
                backgroundImage: conversation?.avatarUrl != null
                    ? NetworkImage(conversation!.avatarUrl!)
                    : null,
                child: conversation?.avatarUrl == null
                    ? Text(conversation?.displayName[0] ?? '?')
                    : null,
              ),
              if (conversation?.isOnline == true)
                Positioned(
                  right: 0,
                  bottom: 0,
                  child: Container(
                    width: 12,
                    height: 12,
                    decoration: BoxDecoration(
                      color: Colors.green,
                      shape: BoxShape.circle,
                      border: Border.all(
                        color: theme.colorScheme.surface,
                        width: 2,
                      ),
                    ),
                  ),
                ),
            ],
          ),
          const SizedBox(width: 12),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  conversation?.displayName ?? 'Chat',
                  style: theme.textTheme.titleMedium,
                ),
                Text(
                  conversation?.isOnline == true ? 'Online' : 'Offline',
                  style: theme.textTheme.bodySmall?.copyWith(
                    color: conversation?.isOnline == true
                        ? Colors.green
                        : theme.colorScheme.onSurfaceVariant,
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
      actions: [
        IconButton(
          icon: const Icon(Icons.call),
          onPressed: () {},
          tooltip: 'Voice call',
        ),
        IconButton(
          icon: const Icon(Icons.videocam),
          onPressed: () {},
          tooltip: 'Video call',
        ),
        PopupMenuButton<String>(
          onSelected: (value) => _handleMenuAction(context, value),
          itemBuilder: (context) => [
            const PopupMenuItem(
              value: 'search',
              child: Text('Search'),
            ),
            const PopupMenuItem(
              value: 'mute',
              child: Text('Mute notifications'),
            ),
            const PopupMenuItem(
              value: 'clear',
              child: Text('Clear chat'),
            ),
          ],
        ),
      ],
    );
  }

  Widget _buildConnectionBanner(ThemeData theme, ConnectionStatus status) {
    final color = status == ConnectionStatus.reconnecting
        ? Colors.orange
        : theme.colorScheme.error;
    final message = status == ConnectionStatus.reconnecting
        ? 'Reconnecting...'
        : 'No connection';

    return Container(
      width: double.infinity,
      padding: const EdgeInsets.symmetric(vertical: 8),
      color: color,
      child: Text(
        message,
        textAlign: TextAlign.center,
        style: const TextStyle(color: Colors.white),
      ),
    );
  }

  Widget _buildMessagesList(BuildContext context, List<Message> messages) {
    if (messages.isEmpty) {
      return Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              Icons.chat_bubble_outline,
              size: 64,
              color: Theme.of(context).colorScheme.onSurfaceVariant,
            ),
            const SizedBox(height: 16),
            Text(
              'No messages yet',
              style: Theme.of(context).textTheme.titleMedium,
            ),
            const SizedBox(height: 8),
            Text(
              'Say hello!',
              style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                    color: Theme.of(context).colorScheme.onSurfaceVariant,
                  ),
            ),
          ],
        ),
      );
    }

    return ListView.builder(
      controller: _scrollController,
      reverse: true,
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 16),
      itemCount: messages.length + (_isLoadingMore ? 1 : 0),
      itemBuilder: (context, index) {
        // Loading indicator at the top (end of reversed list)
        if (_isLoadingMore && index == messages.length) {
          return const Padding(
            padding: EdgeInsets.all(16),
            child: Center(child: CircularProgressIndicator()),
          );
        }

        final message = messages[index];
        final previousMessage =
            index < messages.length - 1 ? messages[index + 1] : null;
        final nextMessage = index > 0 ? messages[index - 1] : null;

        // Check if we need a date separator
        final showDateSeparator = previousMessage == null ||
            !_isSameDay(message.createdAt, previousMessage.createdAt);

        // Check if this is the start/end of a message group
        final isFirstInGroup = previousMessage == null ||
            previousMessage.senderId != message.senderId ||
            !_isSameDay(message.createdAt, previousMessage.createdAt);
        final isLastInGroup = nextMessage == null ||
            nextMessage.senderId != message.senderId;

        return Column(
          children: [
            if (showDateSeparator)
              ChatDateSeparator(date: message.createdAt),
            MessageBubble(
              message: message,
              isFirstInGroup: isFirstInGroup,
              isLastInGroup: isLastInGroup,
              onLongPress: () => _showMessageOptions(context, message),
            ),
          ],
        );
      },
    );
  }

  bool _isSameDay(DateTime a, DateTime b) {
    return a.year == b.year && a.month == b.month && a.day == b.day;
  }

  Widget _buildErrorState(BuildContext context, Object error) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          const Icon(Icons.error_outline, size: 64),
          const SizedBox(height: 16),
          Text('Failed to load messages'),
          const SizedBox(height: 16),
          FilledButton(
            onPressed: () {
              ref.invalidate(messagesProvider(widget.conversationId));
            },
            child: const Text('Retry'),
          ),
        ],
      ),
    );
  }

  void _handleMenuAction(BuildContext context, String action) {
    switch (action) {
      case 'search':
        // Navigate to search in chat
        break;
      case 'mute':
        // Toggle mute
        break;
      case 'clear':
        _confirmClearChat(context);
        break;
    }
  }

  void _confirmClearChat(BuildContext context) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Clear chat?'),
        content: const Text('This will delete all messages in this chat.'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          FilledButton(
            onPressed: () {
              Navigator.pop(context);
              // Clear chat
            },
            child: const Text('Clear'),
          ),
        ],
      ),
    );
  }

  void _showMessageOptions(BuildContext context, Message message) {
    showModalBottomSheet(
      context: context,
      builder: (context) => SafeArea(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            ListTile(
              leading: const Icon(Icons.copy),
              title: const Text('Copy'),
              onTap: () => Navigator.pop(context),
            ),
            if (message.isFromMe)
              ListTile(
                leading: const Icon(Icons.delete_outline),
                title: const Text('Delete'),
                onTap: () => Navigator.pop(context),
              ),
            ListTile(
              leading: const Icon(Icons.reply),
              title: const Text('Reply'),
              onTap: () => Navigator.pop(context),
            ),
          ],
        ),
      ),
    );
  }
}

---

// lib/features/chat/presentation/widgets/message_input_bar.dart
import 'package:flutter/material.dart';

class MessageInputBar extends StatelessWidget {
  final TextEditingController controller;
  final FocusNode focusNode;
  final VoidCallback onSend;
  final VoidCallback onAttachImage;
  final VoidCallback onTakePhoto;

  const MessageInputBar({
    super.key,
    required this.controller,
    required this.focusNode,
    required this.onSend,
    required this.onAttachImage,
    required this.onTakePhoto,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 8),
      decoration: BoxDecoration(
        color: theme.colorScheme.surface,
        boxShadow: [
          BoxShadow(
            color: Colors.black.withOpacity(0.05),
            blurRadius: 10,
            offset: const Offset(0, -2),
          ),
        ],
      ),
      child: SafeArea(
        child: Row(
          crossAxisAlignment: CrossAxisAlignment.end,
          children: [
            // Attachment button
            IconButton(
              icon: const Icon(Icons.add_circle_outline),
              onPressed: () => _showAttachmentOptions(context),
              tooltip: 'Attach',
            ),

            // Text input
            Expanded(
              child: Container(
                constraints: const BoxConstraints(maxHeight: 120),
                decoration: BoxDecoration(
                  color: theme.colorScheme.surfaceContainerHighest,
                  borderRadius: BorderRadius.circular(24),
                ),
                child: TextField(
                  controller: controller,
                  focusNode: focusNode,
                  maxLines: null,
                  textCapitalization: TextCapitalization.sentences,
                  decoration: InputDecoration(
                    hintText: 'Message',
                    border: InputBorder.none,
                    contentPadding: const EdgeInsets.symmetric(
                      horizontal: 16,
                      vertical: 10,
                    ),
                    suffixIcon: IconButton(
                      icon: const Icon(Icons.emoji_emotions_outlined),
                      onPressed: () {},
                      tooltip: 'Emoji',
                    ),
                  ),
                  onSubmitted: (_) => onSend(),
                ),
              ),
            ),

            const SizedBox(width: 8),

            // Send button
            ValueListenableBuilder<TextEditingValue>(
              valueListenable: controller,
              builder: (context, value, child) {
                final hasText = value.text.trim().isNotEmpty;
                return AnimatedContainer(
                  duration: const Duration(milliseconds: 200),
                  child: hasText
                      ? IconButton.filled(
                          icon: const Icon(Icons.send),
                          onPressed: onSend,
                          tooltip: 'Send',
                        )
                      : IconButton(
                          icon: const Icon(Icons.mic),
                          onPressed: () {},
                          tooltip: 'Voice message',
                        ),
                );
              },
            ),
          ],
        ),
      ),
    );
  }

  void _showAttachmentOptions(BuildContext context) {
    showModalBottomSheet(
      context: context,
      builder: (context) => SafeArea(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            ListTile(
              leading: Container(
                padding: const EdgeInsets.all(10),
                decoration: BoxDecoration(
                  color: Colors.purple.withOpacity(0.1),
                  shape: BoxShape.circle,
                ),
                child: const Icon(Icons.image, color: Colors.purple),
              ),
              title: const Text('Gallery'),
              onTap: () {
                Navigator.pop(context);
                onAttachImage();
              },
            ),
            ListTile(
              leading: Container(
                padding: const EdgeInsets.all(10),
                decoration: BoxDecoration(
                  color: Colors.pink.withOpacity(0.1),
                  shape: BoxShape.circle,
                ),
                child: const Icon(Icons.camera_alt, color: Colors.pink),
              ),
              title: const Text('Camera'),
              onTap: () {
                Navigator.pop(context);
                onTakePhoto();
              },
            ),
            ListTile(
              leading: Container(
                padding: const EdgeInsets.all(10),
                decoration: BoxDecoration(
                  color: Colors.blue.withOpacity(0.1),
                  shape: BoxShape.circle,
                ),
                child: const Icon(Icons.insert_drive_file, color: Colors.blue),
              ),
              title: const Text('Document'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: Container(
                padding: const EdgeInsets.all(10),
                decoration: BoxDecoration(
                  color: Colors.green.withOpacity(0.1),
                  shape: BoxShape.circle,
                ),
                child: const Icon(Icons.location_on, color: Colors.green),
              ),
              title: const Text('Location'),
              onTap: () => Navigator.pop(context),
            ),
          ],
        ),
      ),
    );
  }
}
```
