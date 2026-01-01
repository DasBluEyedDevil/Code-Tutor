---
type: "THEORY"
title: "Step 3: Putting It All Together"
---

Finally, combine the `StoriesSection` and multiple `PostWidget` instances into a single `ListView`.

```dart
class InstagramFeed extends StatelessWidget {
  const InstagramFeed({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Instagram', style: TextStyle(fontFamily: 'Pacifico', fontSize: 24)),
        actions: [
          IconButton(icon: const Icon(Icons.favorite_border), onPressed: () {}),
          IconButton(icon: const Icon(Icons.chat_bubble_outline), onPressed: () {}),
        ],
      ),
      body: ListView(
        children: const [
          StoriesSection(),
          Divider(),
          PostWidget(username: 'flutter_dev', imageUrl: 'https://picsum.photos/400/300?1'),
          PostWidget(username: 'dart_lang', imageUrl: 'https://picsum.photos/400/300?2'),
          PostWidget(username: 'google_ads', imageUrl: 'https://picsum.photos/400/300?3'),
        ],
      ),
    );
  }
}
```