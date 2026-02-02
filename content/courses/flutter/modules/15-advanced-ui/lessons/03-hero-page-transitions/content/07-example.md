---
type: "EXAMPLE"
title: "Custom flightShuttleBuilder"
---


Customize the widget shown during Hero flight:



```dart
class CustomHeroExample extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Hero(
      tag: 'custom-hero',
      // Custom widget during flight
      flightShuttleBuilder: (
        BuildContext flightContext,
        Animation<double> animation,
        HeroFlightDirection flightDirection,
        BuildContext fromHeroContext,
        BuildContext toHeroContext,
      ) {
        // Add shadow that fades during flight
        return AnimatedBuilder(
          animation: animation,
          builder: (context, child) {
            return Container(
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(
                  // Animate corner radius
                  flightDirection == HeroFlightDirection.push
                    ? 8 * (1 - animation.value)
                    : 8 * animation.value,
                ),
                boxShadow: [
                  BoxShadow(
                    color: Colors.black.withOpacity(0.3 * animation.value),
                    blurRadius: 20 * animation.value,
                    spreadRadius: 5 * animation.value,
                  ),
                ],
              ),
              child: ClipRRect(
                borderRadius: BorderRadius.circular(
                  flightDirection == HeroFlightDirection.push
                    ? 8 * (1 - animation.value)
                    : 8 * animation.value,
                ),
                child: Image.network(
                  'https://picsum.photos/400/400',
                  fit: BoxFit.cover,
                ),
              ),
            );
          },
        );
      },
      // Placeholder while hero is flying
      placeholderBuilder: (context, heroSize, child) {
        return Container(
          width: heroSize.width,
          height: heroSize.height,
          decoration: BoxDecoration(
            color: Colors.grey.shade200,
            borderRadius: BorderRadius.circular(8),
          ),
          child: const Center(
            child: CircularProgressIndicator(),
          ),
        );
      },
      child: ClipRRect(
        borderRadius: BorderRadius.circular(8),
        child: Image.network(
          'https://picsum.photos/400/400',
          fit: BoxFit.cover,
        ),
      ),
    );
  }
}

// Arc motion for Hero flight path
class ArcTween extends MaterialRectArcTween {
  ArcTween({required Rect begin, required Rect end})
      : super(begin: begin, end: end);
}

// Usage:
Hero(
  tag: 'arc-hero',
  createRectTween: (begin, end) {
    return ArcTween(begin: begin!, end: end!);
  },
  child: MyWidget(),
)
```
