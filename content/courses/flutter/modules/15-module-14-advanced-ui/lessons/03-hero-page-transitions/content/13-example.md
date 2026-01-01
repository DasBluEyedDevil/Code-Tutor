---
type: "EXAMPLE"
title: "GoRouter with Hero Animations"
---


Combine Hero widgets with GoRouter:



```dart
// Product list screen
class ProductListScreen extends StatelessWidget {
  final List<Product> products;
  
  const ProductListScreen({super.key, required this.products});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Products')),
      body: GridView.builder(
        padding: const EdgeInsets.all(16),
        gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
          crossAxisCount: 2,
          childAspectRatio: 0.75,
          crossAxisSpacing: 16,
          mainAxisSpacing: 16,
        ),
        itemCount: products.length,
        itemBuilder: (context, index) {
          final product = products[index];
          return GestureDetector(
            onTap: () => context.go('/product/${product.id}'),
            child: Card(
              clipBehavior: Clip.antiAlias,
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Expanded(
                    child: Hero(
                      tag: 'product-${product.id}',
                      child: Image.network(
                        product.imageUrl,
                        fit: BoxFit.cover,
                        width: double.infinity,
                      ),
                    ),
                  ),
                  Padding(
                    padding: const EdgeInsets.all(8),
                    child: Text(
                      product.name,
                      style: Theme.of(context).textTheme.titleSmall,
                    ),
                  ),
                ],
              ),
            ),
          );
        },
      ),
    );
  }
}

// Product detail screen
class ProductDetailScreen extends StatelessWidget {
  final String productId;
  
  const ProductDetailScreen({super.key, required this.productId});

  @override
  Widget build(BuildContext context) {
    // In real app, fetch product by ID
    final product = getProductById(productId);
    
    return Scaffold(
      body: CustomScrollView(
        slivers: [
          SliverAppBar(
            expandedHeight: 300,
            pinned: true,
            flexibleSpace: FlexibleSpaceBar(
              background: Hero(
                tag: 'product-${product.id}',
                child: Image.network(
                  product.imageUrl,
                  fit: BoxFit.cover,
                ),
              ),
            ),
          ),
          SliverToBoxAdapter(
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    product.name,
                    style: Theme.of(context).textTheme.headlineMedium,
                  ),
                  const SizedBox(height: 8),
                  Text(
                    '\$${product.price.toStringAsFixed(2)}',
                    style: Theme.of(context).textTheme.titleLarge?.copyWith(
                      color: Colors.green,
                    ),
                  ),
                  const SizedBox(height: 16),
                  Text(product.description),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}

// GoRouter setup
final router = GoRouter(
  routes: [
    GoRoute(
      path: '/',
      builder: (context, state) => ProductListScreen(
        products: allProducts,
      ),
    ),
    GoRoute(
      path: '/product/:id',
      pageBuilder: (context, state) {
        final id = state.pathParameters['id']!;
        // Use MaterialPage to enable Hero animations
        return MaterialPage(
          key: state.pageKey,
          child: ProductDetailScreen(productId: id),
        );
      },
    ),
  ],
);
```
