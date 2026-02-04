---
type: KEY_POINT
---

- `Image.network(url)` loads images from the internet; `Image.asset(path)` loads images bundled with your app
- Register asset images in `pubspec.yaml` under the `flutter > assets` section or they will not be found at runtime
- `BoxFit.cover` fills the container by cropping; `BoxFit.contain` shows the entire image with possible letterboxing
- `ClipRRect` with `borderRadius` rounds image corners -- wrap any image widget to apply rounded clipping
- Always provide `width` and `height` constraints on network images to prevent layout jumps while the image loads
