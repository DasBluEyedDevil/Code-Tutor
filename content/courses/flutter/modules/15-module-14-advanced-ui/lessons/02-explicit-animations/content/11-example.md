---
type: "EXAMPLE"
title: "Staggered List Animation"
---


A list where items slide in one after another:



```dart
class StaggeredList extends StatefulWidget {
  final List<String> items;
  
  const StaggeredList({super.key, required this.items});

  @override
  State<StaggeredList> createState() => _StaggeredListState();
}

class _StaggeredListState extends State<StaggeredList>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late List<Animation<double>> _fadeAnimations;
  late List<Animation<Offset>> _slideAnimations;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: Duration(milliseconds: 300 + (widget.items.length * 100)),
    );

    _fadeAnimations = [];
    _slideAnimations = [];

    for (int i = 0; i < widget.items.length; i++) {
      // Calculate stagger timing
      final startPercent = i * 0.1;
      final endPercent = (startPercent + 0.4).clamp(0.0, 1.0);
      final interval = Interval(
        startPercent,
        endPercent,
        curve: Curves.easeOut,
      );

      _fadeAnimations.add(
        Tween<double>(begin: 0.0, end: 1.0).animate(
          CurvedAnimation(parent: _controller, curve: interval),
        ),
      );

      _slideAnimations.add(
        Tween<Offset>(
          begin: const Offset(0.5, 0),
          end: Offset.zero,
        ).animate(
          CurvedAnimation(parent: _controller, curve: interval),
        ),
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
    return ListView.builder(
      itemCount: widget.items.length,
      itemBuilder: (context, index) {
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
          child: ListTile(
            leading: CircleAvatar(child: Text('${index + 1}')),
            title: Text(widget.items[index]),
          ),
        );
      },
    );
  }
}

// Usage:
StaggeredList(
  items: ['First Item', 'Second Item', 'Third Item', 'Fourth Item'],
)
```
