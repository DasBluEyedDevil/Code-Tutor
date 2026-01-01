class StaggeredMenu extends StatefulWidget {
  final List<String> menuItems;

  const StaggeredMenu({super.key, required this.menuItems});

  @override
  State<StaggeredMenu> createState() => _StaggeredMenuState();
}

class _StaggeredMenuState extends State<StaggeredMenu>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late List<Animation<Offset>> _slideAnimations;
  late List<Animation<double>> _fadeAnimations;

  @override
  void initState() {
    super.initState();
    
    // Calculate duration based on item count
    final itemCount = widget.menuItems.length;
    _controller = AnimationController(
      vsync: this,
      duration: Duration(milliseconds: 400 + (itemCount * 100)),
    );

    _slideAnimations = [];
    _fadeAnimations = [];

    for (int i = 0; i < itemCount; i++) {
      final startPercent = i * 0.15;
      final endPercent = (startPercent + 0.4).clamp(0.0, 1.0);
      
      final interval = Interval(
        startPercent,
        endPercent,
        curve: Curves.easeOutCubic,
      );

      _slideAnimations.add(
        Tween<Offset>(
          begin: const Offset(-1, 0),
          end: Offset.zero,
        ).animate(CurvedAnimation(
          parent: _controller,
          curve: interval,
        )),
      );

      _fadeAnimations.add(
        Tween<double>(
          begin: 0.0,
          end: 1.0,
        ).animate(CurvedAnimation(
          parent: _controller,
          curve: interval,
        )),
      );
    }

    _controller.forward();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: List.generate(widget.menuItems.length, (index) {
        return AnimatedBuilder(
          animation: _controller,
          builder: (context, child) {
            return FadeTransition(
              opacity: _fadeAnimations[index],
              child: SlideTransition(
                position: _slideAnimations[index],
                child: child,
              ),
            );
          },
          child: Container(
            width: double.infinity,
            padding: const EdgeInsets.symmetric(
              horizontal: 16,
              vertical: 12,
            ),
            margin: const EdgeInsets.only(bottom: 8),
            decoration: BoxDecoration(
              color: Colors.white,
              borderRadius: BorderRadius.circular(8),
              boxShadow: [
                BoxShadow(
                  color: Colors.black.withOpacity(0.1),
                  blurRadius: 4,
                  offset: const Offset(0, 2),
                ),
              ],
            ),
            child: Row(
              children: [
                Icon(
                  Icons.circle,
                  size: 8,
                  color: Colors.blue.shade400,
                ),
                const SizedBox(width: 12),
                Text(
                  widget.menuItems[index],
                  style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w500,
                  ),
                ),
              ],
            ),
          ),
        );
      }),
    );
  }
}

// Usage:
// StaggeredMenu(
//   menuItems: ['Dashboard', 'Profile', 'Settings', 'Logout'],
// )