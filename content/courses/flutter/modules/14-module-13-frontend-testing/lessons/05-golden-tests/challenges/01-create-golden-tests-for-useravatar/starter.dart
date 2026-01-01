// UserAvatar widget:
class UserAvatar extends StatelessWidget {
  final String? imageUrl;
  final String initials;
  final double size;

  const UserAvatar({
    this.imageUrl,
    required this.initials,
    this.size = 40,
  });

  @override
  Widget build(BuildContext context) {
    return CircleAvatar(
      radius: size / 2,
      backgroundImage: imageUrl != null ? NetworkImage(imageUrl!) : null,
      child: imageUrl == null ? Text(initials) : null,
    );
  }
}

// TODO: Write golden tests
void main() {
  // Test avatar with initials
  // Test avatar with image
  // Test different sizes
}