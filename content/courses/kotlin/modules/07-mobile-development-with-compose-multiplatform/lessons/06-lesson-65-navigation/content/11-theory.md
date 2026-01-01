---
type: "THEORY"
title: "Solution 1"
---



---



```kotlin
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.navigation.NavController
import androidx.navigation.NavType
import androidx.navigation.compose.*
import androidx.navigation.navArgument

data class Product(val id: Int, val name: String, val price: Double)

sealed class Screen(val route: String) {
    object Home : Screen("home")
    object Cart : Screen("cart")
    data class Details(val productId: Int) : Screen("details/$productId") {
        companion object {
            const val route = "details/{productId}"
        }
    }
}

@Composable
fun ShoppingApp() {
    val navController = rememberNavController()
    val cart = remember { mutableStateListOf<Product>() }

    Scaffold(
        bottomBar = {
            BottomNavigationBar(navController = navController, cartCount = cart.size)
        }
    ) { innerPadding ->
        NavHost(
            navController = navController,
            startDestination = Screen.Home.route,
            modifier = Modifier.padding(innerPadding)
        ) {
            composable(Screen.Home.route) {
                HomeScreen(
                    onProductClick = { productId ->
                        navController.navigate(Screen.Details(productId).route)
                    }
                )
            }

            composable(
                route = Screen.Details.route,
                arguments = listOf(navArgument("productId") { type = NavType.IntType })
            ) { backStackEntry ->
                val productId = backStackEntry.arguments?.getInt("productId") ?: 0
                DetailsScreen(
                    productId = productId,
                    onAddToCart = { product -> cart.add(product) },
                    onBack = { navController.popBackStack() }
                )
            }

            composable(Screen.Cart.route) {
                CartScreen(
                    items = cart,
                    onRemove = { product -> cart.remove(product) }
                )
            }
        }
    }
}

@Composable
fun BottomNavigationBar(navController: NavController, cartCount: Int) {
    NavigationBar {
        val navBackStackEntry by navController.currentBackStackEntryAsState()
        val currentRoute = navBackStackEntry?.destination?.route

        NavigationBarItem(
            icon = { Icon(Icons.Default.Home, contentDescription = null) },
            label = { Text("Home") },
            selected = currentRoute == Screen.Home.route,
            onClick = {
                navController.navigate(Screen.Home.route) {
                    popUpTo(Screen.Home.route) { inclusive = true }
                }
            }
        )

        NavigationBarItem(
            icon = {
                BadgedBox(badge = {
                    if (cartCount > 0) {
                        Badge { Text("$cartCount") }
                    }
                }) {
                    Icon(Icons.Default.ShoppingCart, contentDescription = null)
                }
            },
            label = { Text("Cart") },
            selected = currentRoute == Screen.Cart.route,
            onClick = {
                navController.navigate(Screen.Cart.route) {
                    popUpTo(Screen.Home.route)
                }
            }
        )
    }
}

@Composable
fun HomeScreen(onProductClick: (Int) -> Unit) {
    val products = remember {
        listOf(
            Product(1, "Laptop", 999.99),
            Product(2, "Mouse", 29.99),
            Product(3, "Keyboard", 79.99),
            Product(4, "Monitor", 299.99)
        )
    }

    LazyColumn(
        modifier = Modifier.fillMaxSize(),
        contentPadding = PaddingValues(16.dp),
        verticalArrangement = Arrangement.spacedBy(8.dp)
    ) {
        items(products) { product ->
            Card(
                modifier = Modifier
                    .fillMaxWidth()
                    .clickable { onProductClick(product.id) }
            ) {
                Row(
                    modifier = Modifier.padding(16.dp),
                    horizontalArrangement = Arrangement.SpaceBetween
                ) {
                    Column {
                        Text(product.name, style = MaterialTheme.typography.titleMedium)
                        Text("$${product.price}", color = MaterialTheme.colorScheme.primary)
                    }
                    Icon(Icons.Default.ChevronRight, contentDescription = null)
                }
            }
        }
    }
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun DetailsScreen(
    productId: Int,
    onAddToCart: (Product) -> Unit,
    onBack: () -> Unit
) {
    val product = remember {
        listOf(
            Product(1, "Laptop", 999.99),
            Product(2, "Mouse", 29.99),
            Product(3, "Keyboard", 79.99),
            Product(4, "Monitor", 299.99)
        ).find { it.id == productId }
    }

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("Product Details") },
                navigationIcon = {
                    IconButton(onClick = onBack) {
                        Icon(Icons.Default.ArrowBack, contentDescription = "Back")
                    }
                }
            )
        }
    ) { innerPadding ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(innerPadding)
                .padding(16.dp)
        ) {
            product?.let { p ->
                Text(p.name, style = MaterialTheme.typography.headlineLarge)
                Spacer(modifier = Modifier.height(16.dp))
                Text("Price: $${p.price}", style = MaterialTheme.typography.titleLarge)
                Spacer(modifier = Modifier.height(32.dp))
                Button(
                    onClick = { onAddToCart(p) },
                    modifier = Modifier.fillMaxWidth()
                ) {
                    Icon(Icons.Default.AddShoppingCart, contentDescription = null)
                    Spacer(modifier = Modifier.width(8.dp))
                    Text("Add to Cart")
                }
            }
        }
    }
}

@Composable
fun CartScreen(items: List<Product>, onRemove: (Product) -> Unit) {
    Column(modifier = Modifier.fillMaxSize().padding(16.dp)) {
        Text("Cart (${items.size} items)", style = MaterialTheme.typography.headlineMedium)

        if (items.isEmpty()) {
            Box(modifier = Modifier.fillMaxSize(), contentAlignment = androidx.compose.ui.Alignment.Center) {
                Text("Cart is empty")
            }
        } else {
            val total = items.sumOf { it.price }

            LazyColumn(modifier = Modifier.weight(1f)) {
                items(items) { product ->
                    Card(modifier = Modifier.fillMaxWidth().padding(vertical = 4.dp)) {
                        Row(
                            modifier = Modifier.padding(16.dp),
                            horizontalArrangement = Arrangement.SpaceBetween
                        ) {
                            Column {
                                Text(product.name)
                                Text("$${product.price}", color = MaterialTheme.colorScheme.primary)
                            }
                            IconButton(onClick = { onRemove(product) }) {
                                Icon(Icons.Default.Delete, contentDescription = "Remove")
                            }
                        }
                    }
                }
            }

            HorizontalDivider()
            Row(
                modifier = Modifier.fillMaxWidth().padding(vertical = 16.dp),
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                Text("Total:", style = MaterialTheme.typography.titleLarge)
                Text("$${String.format("%.2f", total)}", style = MaterialTheme.typography.titleLarge)
            }
            Button(onClick = { }, modifier = Modifier.fillMaxWidth()) {
                Text("Checkout")
            }
        }
    }
}
```
