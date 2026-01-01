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
  late AnimationController _controller;
  late Animation<double> _scaleAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 1000),
    );

    _scaleAnimation = Tween<double>(
      begin: 1.0,
      end: 1.15,
    ).animate(CurvedAnimation(
      parent: _controller,
      curve: Curves.elasticOut,
    ));

    _controller.repeat(reverse: true);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: _controller,
      builder: (context, child) {
        final glowIntensity = _controller.value;
        return Transform.scale(
          scale: _scaleAnimation.value,
          child: Container(
            width: 64,
            height: 64,
            decoration: BoxDecoration(
              shape: BoxShape.circle,
              color: Colors.blue,
              boxShadow: [
                BoxShadow(
                  color: Colors.blue.withOpacity(0.3 + (glowIntensity * 0.4)),
                  blurRadius: 8 + (glowIntensity * 16),
                  spreadRadius: glowIntensity * 4,
                ),
              ],
            ),
            child: Material(
              color: Colors.transparent,
              child: InkWell(
                customBorder: const CircleBorder(),
                onTap: widget.onPressed,
                child: Center(
                  child: Icon(
                    widget.icon,
                    color: Colors.white,
                    size: 32,
                  ),
                ),
              ),
            ),
          ),
        );
      },
    );
  }
}