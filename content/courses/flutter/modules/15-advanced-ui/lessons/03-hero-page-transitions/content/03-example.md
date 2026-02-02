---
type: "EXAMPLE"
title: "Basic Hero Implementation"
---


A photo gallery with Hero transitions:



```dart
// Photo Grid Screen (Source)
class PhotoGridScreen extends StatelessWidget {
  final List<String> imageUrls = [
    'https://picsum.photos/id/1/400/400',
    'https://picsum.photos/id/2/400/400',
    'https://picsum.photos/id/3/400/400',
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Photo Gallery')),
      body: GridView.builder(
        padding: const EdgeInsets.all(8),
        gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
          crossAxisCount: 2,
          crossAxisSpacing: 8,
          mainAxisSpacing: 8,
        ),
        itemCount: imageUrls.length,
        itemBuilder: (context, index) {
          return GestureDetector(
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => PhotoDetailScreen(
                    imageUrl: imageUrls[index],
                    heroTag: 'photo-$index', // Unique tag per photo
                  ),
                ),
              );
            },
            child: Hero(
              tag: 'photo-$index', // Must match destination
              child: ClipRRect(
                borderRadius: BorderRadius.circular(8),
                child: Image.network(
                  imageUrls[index],
                  fit: BoxFit.cover,
                ),
              ),
            ),
          );
        },
      ),
    );
  }
}

// Photo Detail Screen (Destination)
class PhotoDetailScreen extends StatelessWidget {
  final String imageUrl;
  final String heroTag;

  const PhotoDetailScreen({
    super.key,
    required this.imageUrl,
    required this.heroTag,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.black,
      appBar: AppBar(
        backgroundColor: Colors.transparent,
        elevation: 0,
      ),
      body: Center(
        child: Hero(
          tag: heroTag, // Matches source tag
          child: Image.network(
            imageUrl,
            fit: BoxFit.contain,
          ),
        ),
      ),
    );
  }
}
```
