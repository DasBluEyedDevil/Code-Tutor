---
type: "EXAMPLE"
title: "Hero with Text and Complex Layouts"
---


When your Hero contains multiple widgets, wrap them consistently:



```dart
// Product Card (Source)
class ProductCard extends StatelessWidget {
  final Product product;

  const ProductCard({super.key, required this.product});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () => _navigateToDetail(context),
      child: Card(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Hero for image
            Hero(
              tag: 'product-image-${product.id}',
              child: Image.network(
                product.imageUrl,
                height: 150,
                width: double.infinity,
                fit: BoxFit.cover,
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(8),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  // Hero for title
                  Hero(
                    tag: 'product-title-${product.id}',
                    child: Material(
                      color: Colors.transparent,
                      child: Text(
                        product.name,
                        style: Theme.of(context).textTheme.titleMedium,
                      ),
                    ),
                  ),
                  const SizedBox(height: 4),
                  // Hero for price
                  Hero(
                    tag: 'product-price-${product.id}',
                    child: Material(
                      color: Colors.transparent,
                      child: Text(
                        '\$${product.price.toStringAsFixed(2)}',
                        style: Theme.of(context).textTheme.titleLarge?.copyWith(
                          color: Colors.green,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  void _navigateToDetail(BuildContext context) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => ProductDetailScreen(product: product),
      ),
    );
  }
}

// Important: Wrap Text in Material widget when using Hero
// This prevents text rendering issues during flight
```
