import json
import os

filepath = 'content/courses/flutter/course.json'

with open(filepath, 'r') as f:
    data = json.load(f)

# Module 14 Updates
module_14_updates = {
    "14.2": {
        "Build Commands Reference": """Use these commands to build your Flutter web app with WebAssembly support.
- `flutter run -d chrome --wasm`: Runs the app in Chrome using the Wasm renderer for development.
- `flutter build web --wasm`: Builds the production version of your app using Wasm.
- `flutter build web --wasm --release`: explicitly enables release mode optimizations."""
    },
    "14.3": {
        "Platform Detection in Dart": """Since your app might run on multiple platforms, it's important to detect the environment.
- `kIsWeb`: A constant from `package:flutter/foundation.dart` that returns true if running on the web.
- `defaultTargetPlatform`: Helps identify the specific OS (Android, iOS, Windows, etc.) even when running on the web (if configured) or native."""
    },
    "14.4": {
        "Image Optimization": """Optimizing images is crucial for web performance.
- Use **WebP** format for smaller file sizes.
- Use `cacheWidth` and `cacheHeight` in `Image.network` to decode images at a specific size, reducing memory usage.
- Implement `loadingBuilder` to show a progress indicator while the image downloads."""
    },
    "14.5": {
        "Offline Support Basics": """To create a robust PWA, you need to handle offline states.
- Use `connectivity_plus` to detect network status changes.
- Wrap your app or widgets in a `StreamBuilder` that listens to connectivity changes.
- Display a banner or alternative UI when the user is offline."""
    },
    "14.7": {
        "Adding PWA Features": """A Portfolio PWA needs to look good and function like a native app.
- **Responsive Layout**: Use `LayoutBuilder` or `MediaQuery` to adapt the UI for mobile, tablet, and desktop screens.
- **Navigation**: Swap between a Drawer (mobile) and a Top Bar (desktop).
- **Theme**: Support Light and Dark modes for better user accessibility."""
    }
}

count = 0
for module in data['modules']:
    if module['id'] == 'module-14':
        for lesson in module['lessons']:
            lesson_id = lesson['id']
            if lesson_id in module_14_updates:
                updates = module_14_updates[lesson_id]
                if 'contentSections' in lesson:
                    for section in lesson['contentSections']:
                        title = section['title']
                        if title in updates:
                            # Only update if content is empty or very short
                            current_content = section.get('content', '').strip()
                            if len(current_content) < 10:
                                section['content'] = updates[title]
                                print(f"Updated Lesson {lesson_id} - Section '{title}'")
                                count += 1

print(f"Total updates applied: {count}")

with open(filepath, 'w') as f:
    json.dump(data, f, indent=2)
