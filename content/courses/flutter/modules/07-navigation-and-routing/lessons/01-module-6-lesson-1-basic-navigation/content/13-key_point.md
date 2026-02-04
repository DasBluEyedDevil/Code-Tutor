---
type: KEY_POINT
---

- `Navigator.push()` adds a new screen on top of the current one; `Navigator.pop()` removes it and returns to the previous screen
- `MaterialPageRoute` provides platform-appropriate transitions (slide from right on iOS, slide up on Android)
- Pass data to a new screen through its constructor parameters: `Navigator.push(context, MaterialPageRoute(builder: (_) => DetailScreen(item: item)))`
- Use `Navigator.pop(context, result)` to send data back to the previous screen and await it with `final result = await Navigator.push(...)`
- The navigation stack works like a deck of cards -- push adds on top, pop removes the top card
