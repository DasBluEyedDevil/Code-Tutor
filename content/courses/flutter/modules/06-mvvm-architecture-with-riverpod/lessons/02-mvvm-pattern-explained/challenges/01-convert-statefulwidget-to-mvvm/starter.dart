// STARTER CODE: Messy StatefulWidget
// Your job: Refactor this into MVVM!

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

void main() {
  runApp(const ProviderScope(child: MyApp()));
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(home: CounterPage());
  }
}

// THIS IS THE MESSY CODE - REFACTOR IT!
class CounterPage extends StatefulWidget {
  @override
  State<CounterPage> createState() => _CounterPageState();
}

class _CounterPageState extends State<CounterPage> {
  // State mixed with UI
  int count = 0;
  DateTime? lastModified;

  // Logic mixed with UI
  void increment() {
    if (count < 50) {
      setState(() {
        count++;
        lastModified = DateTime.now();
      });
    }
  }

  void decrement() {
    if (count > 0) {
      setState(() {
        count--;
        lastModified = DateTime.now();
      });
    }
  }

  void reset() {
    setState(() {
      count = 0;
      lastModified = null;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Counter'),
        actions: [
          IconButton(icon: Icon(Icons.refresh), onPressed: reset),
        ],
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text('$count', style: TextStyle(fontSize: 48)),
            if (lastModified != null)
              Text('Modified: ${lastModified!.hour}:${lastModified!.minute}'),
            SizedBox(height: 20),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                ElevatedButton(onPressed: decrement, child: Text('-')),
                SizedBox(width: 20),
                ElevatedButton(onPressed: increment, child: Text('+')),
              ],
            ),
          ],
        ),
      ),
    );
  }
}

// TODO: Create these three parts:
//
// 1. MODEL: CounterState class
//    - int count
//    - DateTime? lastModified
//    - copyWith method
//
// 2. VIEWMODEL: CounterViewModel extends Notifier<CounterState>
//    - increment() with max 50 validation
//    - decrement() with min 0 validation  
//    - reset()
//
// 3. VIEW: CounterScreen extends ConsumerWidget
//    - Same UI but uses ref.watch and ref.read
//    - NO logic in the widget!