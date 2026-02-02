---
type: "THEORY"
title: "Controlling Image Size"
---


### Fixed Width and Height


### Using fit Property


**Common fit values**:
- `BoxFit.cover` - Fill space, may crop
- `BoxFit.contain` - Fit entirely, may have empty space
- `BoxFit.fill` - Stretch to fill (may distort)
- `BoxFit.fitWidth` - Fit width, height adjusts
- `BoxFit.fitHeight` - Fit height, width adjusts



```dart
Image.network(
  'https://picsum.photos/200/300',
  width: 300,
  height: 200,
  fit: BoxFit.cover,  // Fills the space, may crop
)
```
