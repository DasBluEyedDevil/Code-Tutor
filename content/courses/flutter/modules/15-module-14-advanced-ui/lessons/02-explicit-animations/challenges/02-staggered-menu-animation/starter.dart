class StaggeredMenu extends StatefulWidget {
  final List<String> menuItems;

  const StaggeredMenu({super.key, required this.menuItems});

  @override
  State<StaggeredMenu> createState() => _StaggeredMenuState();
}

class _StaggeredMenuState extends State<StaggeredMenu>
    with SingleTickerProviderStateMixin {
  // TODO: Create AnimationController
  // TODO: Create lists of slide and fade animations

  @override
  void initState() {
    super.initState();
    // TODO: Initialize controller
    // TODO: Create staggered animations using Interval
    // Hint: Each item should have Interval(i * 0.15, (i * 0.15) + 0.4)
    // TODO: Start animation
  }

  @override
  void dispose() {
    // TODO: Dispose controller
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    // TODO: Build menu with animated items
    return Container();
  }
}