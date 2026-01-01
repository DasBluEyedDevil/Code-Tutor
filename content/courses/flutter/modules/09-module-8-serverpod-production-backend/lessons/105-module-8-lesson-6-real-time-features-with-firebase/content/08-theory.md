---
type: "THEORY"
title: "Part 4: Live Data Updates (Like Counter)"
---


Build a live like counter that updates in real-time!




```dart
// Like button with real-time count
class LiveLikeButton extends StatelessWidget {
  final String postId;

  const LiveLikeButton({super.key, required this.postId});

  @override
  Widget build(BuildContext context) {
    final authService = AuthService();
    final currentUserId = authService.currentUser?.uid;

    return StreamBuilder<DocumentSnapshot>(
      stream: FirebaseFirestore.instance
          .collection('posts')
          .doc(postId)
          .snapshots(),
      builder: (context, snapshot) {
        if (!snapshot.hasData) {
          return const IconButton(
            icon: Icon(Icons.favorite_border),
            onPressed: null,
          );
        }

        final data = snapshot.data!.data() as Map<String, dynamic>?;
        final likes = data?['likes'] as List? ?? [];
        final hasLiked = currentUserId != null && likes.contains(currentUserId);
        final likeCount = likes.length;

        return Row(
          children: [
            IconButton(
              icon: Icon(
                hasLiked ? Icons.favorite : Icons.favorite_border,
                color: hasLiked ? Colors.red : null,
              ),
              onPressed: () async {
                if (currentUserId == null) return;

                if (hasLiked) {
                  await FirebaseFirestore.instance
                      .collection('posts')
                      .doc(postId)
                      .update({
                    'likes': FieldValue.arrayRemove([currentUserId]),
                  });
                } else {
                  await FirebaseFirestore.instance
                      .collection('posts')
                      .doc(postId)
                      .update({
                    'likes': FieldValue.arrayUnion([currentUserId]),
                  });
                }
              },
            ),
            Text('$likeCount'),
          ],
        );
      },
    );
  }
}
```
