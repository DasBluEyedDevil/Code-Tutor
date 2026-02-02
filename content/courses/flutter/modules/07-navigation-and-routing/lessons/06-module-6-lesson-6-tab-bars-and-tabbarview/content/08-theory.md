---
type: "THEORY"
title: "Preserving Tab State"
---


By default, TabBarView rebuilds tabs when switching. To preserve state:


**Without mixin**: Scroll position lost when switching tabs
**With mixin**: Scroll position preserved! 🎉



```dart
class MyTab extends StatefulWidget {
  @override
  _MyTabState createState() => _MyTabState();
}

class _MyTabState extends State<MyTab>
    with AutomaticKeepAliveClientMixin {  // Add this mixin!

  @override
  bool get wantKeepAlive => true;  // Preserve state!

  @override
  Widget build(BuildContext context) {
    super.build(context);  // Must call super.build()

    return ListView.builder(
      itemCount: 100,
      itemBuilder: (context, index) => ListTile(
        title: Text('Item $index'),
      ),
    );
  }
}
```
