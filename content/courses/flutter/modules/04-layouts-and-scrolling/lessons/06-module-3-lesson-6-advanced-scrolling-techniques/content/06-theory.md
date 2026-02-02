---
type: "THEORY"
title: "NestedScrollView"
---


`NestedScrollView` lets you coordinate scrolling between a header (like a collapsing app bar) and a body (like a ListView). The header collapses as you scroll, with smooth synchronized motion.

This pattern is common in profile screens, product pages, and settings:



```dart
NestedScrollView(
  headerSliverBuilder: (context, innerBoxIsScrolled) {
    return [
      SliverAppBar(
        expandedHeight: 200,
        floating: false,
        pinned: true,
        flexibleSpace: FlexibleSpaceBar(
          title: Text('My App'),
          background: Image.network('url', fit: BoxFit.cover),
        ),
      ),
    ];
  },
  body: ListView.builder(
    itemCount: 50,
    itemBuilder: (context, index) {
      return ListTile(title: Text('Item $index'));
    },
  ),
)
```
