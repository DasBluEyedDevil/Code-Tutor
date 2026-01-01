---
type: "THEORY"
title: "Step 1: The Stories Section"
---

Instagram's stories are at the top and scroll horizontally. We'll use a `SizedBox` with a specific height and a `ListView` with `scrollDirection: Axis.horizontal`.

```dart
class StoriesSection extends StatelessWidget {
  const StoriesSection({super.key});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 110,
      child: ListView.builder(
        scrollDirection: Axis.horizontal,
        itemCount: 10,
        padding: const EdgeInsets.symmetric(horizontal: 8),
        itemBuilder: (context, index) {
          return Padding(
            padding: const EdgeInsets.all(8.0),
            child: Column(
              children: [
                Container(
                  padding: const EdgeInsets.all(3),
                  decoration: const BoxDecoration(
                    shape: BoxShape.circle,
                    gradient: LinearGradient(
                      colors: [Colors.yellow, Colors.red, Colors.purple],
                    ),
                  ),
                  child: const CircleAvatar(
                    radius: 30,
                    backgroundImage: NetworkImage('https://picsum.photos/100/100'),
                  ),
                ),
                const Text('User', style: TextStyle(fontSize: 12)),
              ],
            ),
          );
        },
      ),
    );
  }
}
```