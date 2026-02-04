class PhotoGallery extends StatelessWidget {
  final List<String> photos = [
    'https://picsum.photos/id/10/400/400',
    'https://picsum.photos/id/20/400/400',
    'https://picsum.photos/id/30/400/400',
    'https://picsum.photos/id/40/400/400',
    'https://picsum.photos/id/50/400/400',
    'https://picsum.photos/id/60/400/400',
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
        itemCount: photos.length,
        itemBuilder: (context, index) {
          return GestureDetector(
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => PhotoDetailScreen(
                    photoUrl: photos[index],
                    index: index,
                  ),
                ),
              );
            },
            child: Hero(
              tag: 'photo-$index',
              child: ClipRRect(
                borderRadius: BorderRadius.circular(8),
                child: Image.network(
                  photos[index],
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

class PhotoDetailScreen extends StatelessWidget {
  final String photoUrl;
  final int index;

  const PhotoDetailScreen({
    super.key,
    required this.photoUrl,
    required this.index,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.black,
      appBar: AppBar(
        backgroundColor: Colors.transparent,
        elevation: 0,
        iconTheme: const IconThemeData(color: Colors.white),
      ),
      body: Center(
        child: Hero(
          tag: 'photo-$index',
          flightShuttleBuilder: (
            flightContext,
            animation,
            flightDirection,
            fromHeroContext,
            toHeroContext,
          ) {
            return AnimatedBuilder(
              animation: animation,
              builder: (context, child) {
                return Container(
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(
                      8 * (1 - animation.value),
                    ),
                    boxShadow: [
                      BoxShadow(
                        color: Colors.black.withValues(alpha: 0.5 * animation.value),
                        blurRadius: 30 * animation.value,
                        spreadRadius: 10 * animation.value,
                      ),
                    ],
                  ),
                  child: ClipRRect(
                    borderRadius: BorderRadius.circular(
                      8 * (1 - animation.value),
                    ),
                    child: Image.network(
                      photoUrl,
                      fit: BoxFit.cover,
                    ),
                  ),
                );
              },
            );
          },
          child: Image.network(
            photoUrl,
            fit: BoxFit.contain,
          ),
        ),
      ),
    );
  }
}