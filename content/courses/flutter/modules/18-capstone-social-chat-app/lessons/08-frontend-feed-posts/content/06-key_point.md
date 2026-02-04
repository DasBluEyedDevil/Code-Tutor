---
type: KEY_POINT
---

- Implement infinite scroll by detecting when the user reaches the bottom of the ListView and triggering the next page load
- Use a Riverpod `AsyncNotifier` to manage feed state including posts list, loading flag, pagination cursor, and error handling
- Pull-to-refresh resets the feed to page 1; implement it with `RefreshIndicator` wrapping the list
- Each post card is a custom widget with user avatar, content, image, like/comment counts, and timestamp
- Optimistic like/unlike updates the count and icon immediately, then syncs with the backend in the background
