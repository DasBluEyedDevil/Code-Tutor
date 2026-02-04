---
type: KEY_POINT
---

- Display user stats (posts, followers, following) and a grid of user posts on the profile screen using a `CustomScrollView` with slivers
- Differentiate own-profile vs. other-user-profile with contextual actions: "Edit Profile" button vs. "Follow/Unfollow" button
- Use `image_picker` to let users select a new profile picture from gallery or camera, then upload to the backend media endpoint
- Cache profile data locally so the profile screen loads instantly even when offline, with a background refresh when online
- Separate the edit screen from the view screen: `EditProfileScreen` uses `TextEditingController` for name, bio, and website fields
