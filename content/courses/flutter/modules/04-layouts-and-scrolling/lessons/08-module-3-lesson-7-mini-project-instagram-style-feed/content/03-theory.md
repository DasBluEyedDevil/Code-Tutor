---
type: "THEORY"
title: "Step 2: The Instagram Post Widget"
---

A post is a vertical stack of sections: Header, Image, Actions, and Description. We'll create a reusable `PostWidget`.

```dart
class PostWidget extends StatelessWidget {
  final String username;
  final String imageUrl;

  const PostWidget({required this.username, required this.imageUrl, super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        // 1. Header
        ListTile(
          leading: const CircleAvatar(backgroundImage: NetworkImage('https://picsum.photos/50/50')),
          title: Text(username, style: const TextStyle(fontWeight: FontWeight.bold)),
          trailing: const Icon(Icons.more_vert),
        ),
        // 2. Image
        Image.network(imageUrl, width: double.infinity, height: 300, fit: BoxFit.cover),
        // 3. Action Buttons
        Row(
          children: [
            IconButton(icon: const Icon(Icons.favorite_border), onPressed: () {}),
            IconButton(icon: const Icon(Icons.chat_bubble_outline), onPressed: () {}),
            IconButton(icon: const Icon(Icons.send_outlined), onPressed: () {}),
            const Spacer(),
            IconButton(icon: const Icon(Icons.bookmark_border), onPressed: () {}),
          ],
        ),
        // 4. Likes & Caption
        const Padding(
          padding: EdgeInsets.symmetric(horizontal: 16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text('1,234 likes', style: TextStyle(fontWeight: FontWeight.bold)),
              SizedBox(height: 4),
              Text('View all 42 comments', style: TextStyle(color: Colors.grey)),
            ],
          ),
        ),
        const SizedBox(height: 16),
      ],
    );
  }
}
```