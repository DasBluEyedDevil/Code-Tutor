---
type: "THEORY"
title: "Composition: Widgets within Widgets"
---

Don't be afraid to create small widgets! Professional Flutter apps are made of hundreds of tiny, focused widgets.

**Why tiny widgets?**
- **Readability**: A 50-line file is easier to read than a 500-line file.
- **Reusability**: You can use that `StarRating` widget in ten different screens.
- **Performance**: Flutter is optimized for small, constant widgets. Only the parts that need to change will rebuild.

```dart
class ProductItem extends StatelessWidget {
  final Product product;

  const ProductItem({required this.product, super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        ProductThumbnail(product.imageUrl), // Small widget!
        ProductInfo(product.name, product.price), // Small widget!
        AddToCartButton(onTap: () => handleAdd(product)), // Small widget!
      ],
    );
  }
}
```