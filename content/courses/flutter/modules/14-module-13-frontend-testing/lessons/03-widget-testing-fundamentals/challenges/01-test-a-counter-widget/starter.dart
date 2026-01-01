// Widget to test:
class CounterScreen extends StatefulWidget {
  @override
  State<CounterScreen> createState() => _CounterScreenState();
}

class _CounterScreenState extends State<CounterScreen> {
  int count = 0;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(child: Text('$count', key: const Key('counter'))),
      floatingActionButton: Row(
        children: [
          IconButton(
            key: const Key('decrement'),
            icon: const Icon(Icons.remove),
            onPressed: () => setState(() => count--),
          ),
          IconButton(
            key: const Key('increment'),
            icon: const Icon(Icons.add),
            onPressed: () => setState(() => count++),
          ),
        ],
      ),
    );
  }
}

// TODO: Write widget tests
void main() {
  // Add your tests here
}