---
type: "THEORY"
title: "Running Layouts on iOS"
---


### Platform-Specific Layout Considerations

While your Compose UI code works on both platforms, there are some differences to be aware of:

| Feature | Android | iOS |
|---------|---------|-----|
| **List Scrolling** | Overscroll glow effect | Bounce effect |
| **Scroll Bars** | Visible by default | Hidden by default |
| **Grid Performance** | RecyclerView-based | Native iOS rendering |
| **Safe Areas** | Status bar | Notch + Home indicator |

### Running on iOS Simulator

1. Ensure you have Xcode installed and configured
2. In Android Studio, select the iOS target
3. Choose an iOS Simulator (e.g., iPhone 15)
4. Click Run

Your layouts will automatically adapt to iOS safe areas when using `Scaffold`.

### Font Differences

By default, text rendering differs between platforms:

- **Android**: Uses Roboto font family
- **iOS**: Uses SF Pro font family

Material Theme handles this automatically, but be aware that text may appear slightly different on each platform. Always test on both simulators!

### Handling Platform-Specific Styling

```kotlin
// In commonMain - style adapts per platform
@Composable
fun ProductList(products: List<Product>) {
    LazyColumn(
        // contentPadding handles safe areas on both platforms
        contentPadding = PaddingValues(16.dp),
        verticalArrangement = Arrangement.spacedBy(12.dp)
    ) {
        items(products) { product ->
            ProductCard(product)
        }
    }
}
```

---

