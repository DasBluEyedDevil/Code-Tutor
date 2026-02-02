---
type: "EXAMPLE"
title: "Double Tap Example (Like Button)"
---



**Instagram-style double-tap to like!**



```dart
class LikeableImage extends StatefulWidget {
  @override
  _LikeableImageState createState() => _LikeableImageState();
}

class _LikeableImageState extends State<LikeableImage> {
  bool isLiked = false;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onDoubleTap: () {
        setState(() {
          isLiked = !isLiked;
        });
      },
      child: Stack(
        alignment: Alignment.center,
        children: [
          Image.network(
            'https://picsum.photos/400',
            width: 400,
            height: 400,
            fit: BoxFit.cover,
          ),
          if (isLiked)
            Icon(
              Icons.favorite,
              size: 100,
              color: Colors.red.withOpacity(0.7),
            ),
        ],
      ),
    );
  }
}
```
