---
type: "THEORY"
title: "Advanced Layout Composables"
---


### Box (Stacking/Overlapping)

`Box` stacks children on top of each other - useful for overlaying elements:


**Alignment options**:

### LazyColumn (Efficient Vertical List)

`LazyColumn` efficiently renders only visible items - perfect for long lists:


**With custom data**:


**Key with items for better performance**:


### LazyRow (Efficient Horizontal List)

Same as `LazyColumn` but horizontal:


### LazyVerticalGrid (Grid Layout)

Display items in a grid:


**Grid column options**:

---



```kotlin
GridCells.Fixed(3)              // Exactly 3 columns
GridCells.Adaptive(120.dp)      // As many columns as fit (min 120dp each)
GridCells.FixedSize(120.dp)     // Fixed column width
```
