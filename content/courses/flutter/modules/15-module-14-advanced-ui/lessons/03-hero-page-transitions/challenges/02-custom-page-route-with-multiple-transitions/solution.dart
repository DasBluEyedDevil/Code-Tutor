enum TransitionType {
  fade,
  slideUp,
  slideRight,
  scale,
}

class CustomPageRoute<T> extends PageRouteBuilder<T> {
  final Widget page;
  final TransitionType type;

  CustomPageRoute({
    required this.page,
    this.type = TransitionType.fade,
  }) : super(
    pageBuilder: (context, animation, secondaryAnimation) => page,
    transitionDuration: const Duration(milliseconds: 300),
    reverseTransitionDuration: const Duration(milliseconds: 300),
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      switch (type) {
        case TransitionType.fade:
          return FadeTransition(
            opacity: animation,
            child: child,
          );
        case TransitionType.slideUp:
          return SlideTransition(
            position: Tween<Offset>(
              begin: const Offset(0, 1),
              end: Offset.zero,
            ).animate(CurvedAnimation(
              parent: animation,
              curve: Curves.easeOutCubic,
            )),
            child: child,
          );
        case TransitionType.slideRight:
          return SlideTransition(
            position: Tween<Offset>(
              begin: const Offset(1, 0),
              end: Offset.zero,
            ).animate(CurvedAnimation(
              parent: animation,
              curve: Curves.easeOutCubic,
            )),
            child: child,
          );
        case TransitionType.scale:
          return ScaleTransition(
            scale: Tween<double>(
              begin: 0.0,
              end: 1.0,
            ).animate(CurvedAnimation(
              parent: animation,
              curve: Curves.easeOutBack,
            )),
            child: FadeTransition(
              opacity: animation,
              child: child,
            ),
          );
      }
    },
  );
}

class TransitionDemo extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Transition Demo')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            ElevatedButton(
              onPressed: () => Navigator.push(
                context,
                CustomPageRoute(
                  page: const DestinationScreen(title: 'Fade'),
                  type: TransitionType.fade,
                ),
              ),
              child: const Text('Fade Transition'),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () => Navigator.push(
                context,
                CustomPageRoute(
                  page: const DestinationScreen(title: 'Slide Up'),
                  type: TransitionType.slideUp,
                ),
              ),
              child: const Text('Slide Up Transition'),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () => Navigator.push(
                context,
                CustomPageRoute(
                  page: const DestinationScreen(title: 'Slide Right'),
                  type: TransitionType.slideRight,
                ),
              ),
              child: const Text('Slide Right Transition'),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () => Navigator.push(
                context,
                CustomPageRoute(
                  page: const DestinationScreen(title: 'Scale'),
                  type: TransitionType.scale,
                ),
              ),
              child: const Text('Scale Transition'),
            ),
          ],
        ),
      ),
    );
  }
}

class DestinationScreen extends StatelessWidget {
  final String title;
  
  const DestinationScreen({super.key, required this.title});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text(title)),
      body: Center(
        child: Text(
          'Arrived via $title transition',
          style: Theme.of(context).textTheme.titleLarge,
        ),
      ),
    );
  }
}