class PulsingButton extends StatefulWidget {
  final VoidCallback onPressed;
  final IconData icon;

  const PulsingButton({
    super.key,
    required this.onPressed,
    required this.icon,
  });

  @override
  State<PulsingButton> createState() => _PulsingButtonState();
}

class _PulsingButtonState extends State<PulsingButton>
    with SingleTickerProviderStateMixin {
  // TODO: Create AnimationController
  // TODO: Create scale animation with Tween and CurvedAnimation
  
  @override
  void initState() {
    super.initState();
    // TODO: Initialize controller with 1 second duration
    // TODO: Create scale animation (1.0 to 1.15 with elasticOut curve)
    // TODO: Start repeating animation with reverse
  }

  @override
  void dispose() {
    // TODO: Dispose controller
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    // TODO: Use AnimatedBuilder to build pulsing button
    // Include glow effect that intensifies with scale
    return Container();
  }
}