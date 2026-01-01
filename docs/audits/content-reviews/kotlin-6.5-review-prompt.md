# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Mobile Development with Compose Multiplatform
- **Lesson:** Lesson 6.5: Navigation (ID: 6.5)
- **Difficulty:** advanced
- **Estimated Time:** 75 minutes

## Current Lesson Content

{
    "id":  "6.5",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 75 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "\nMulti-screen navigation is essential for modern apps. Users expect smooth transitions between screens, deep linking support, and logical app flow.\n\n**Compose Navigation** provides a type-safe, declarative way to handle navigation with full integration into Compose. With Compose Multiplatform, the same navigation code works on both Android and iOS!\n\nIn this lesson, you\u0027ll master:\n- ✅ Navigation component setup\n- ✅ NavHost and NavController\n- ✅ Route definitions and navigation\n- ✅ Passing arguments between screens\n- ✅ Bottom navigation bars\n- ✅ Navigation drawer\n- ✅ Deep linking\n- ✅ **iOS-specific navigation patterns (swipe-back gesture)**\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Setup",
                                "content":  "\nAdd navigation dependency in `build.gradle.kts`:\n\n\nIn `gradle/libs.versions.toml`:\n\n\n---\n\n",
                                "code":  "[versions]\nnavigation = \"2.8.4\"\n\n[libraries]\nandroidx-navigation-compose = { group = \"androidx.navigation\", name = \"navigation-compose\", version.ref = \"navigation\" }",
                                "language":  "toml"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Basic Navigation",
                                "content":  "\n### NavController\n\n**NavController** manages navigation between screens:\n\n\n### NavHost\n\n**NavHost** defines navigation graph (screens and routes):\n\n\n### Screen Composables\n\n\n---\n\n",
                                "code":  "@Composable\nfun HomeScreen(onNavigateToProfile: () -\u003e Unit) {\n    Column(modifier = Modifier.fillMaxSize()) {\n        Text(\"Home Screen\", style = MaterialTheme.typography.headlineLarge)\n\n        Button(onClick = onNavigateToProfile) {\n            Text(\"Go to Profile\")\n        }\n    }\n}\n\n@Composable\nfun ProfileScreen(onNavigateBack: () -\u003e Unit) {\n    Column(modifier = Modifier.fillMaxSize()) {\n        Text(\"Profile Screen\", style = MaterialTheme.typography.headlineLarge)\n\n        Button(onClick = onNavigateBack) {\n            Text(\"Back\")\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Navigation with Arguments",
                                "content":  "\n### Passing Simple Arguments\n\n\n### Optional Arguments\n\n\n### Type-Safe Navigation (Recommended)\n\n\n---\n\n",
                                "code":  "// Define routes\nsealed class Screen(val route: String) {\n    object Home : Screen(\"home\")\n    object Profile : Screen(\"profile\")\n    data class Details(val userId: Int) : Screen(\"details/$userId\") {\n        companion object {\n            const val route = \"details/{userId}\"\n        }\n    }\n}\n\n// Navigation graph\nNavHost(navController = navController, startDestination = Screen.Home.route) {\n    composable(Screen.Home.route) {\n        HomeScreen(onNavigateToDetails = { userId -\u003e\n            navController.navigate(Screen.Details(userId).route)\n        })\n    }\n\n    composable(\n        route = Screen.Details.route,\n        arguments = listOf(navArgument(\"userId\") { type = NavType.IntType })\n    ) { backStackEntry -\u003e\n        val userId = backStackEntry.arguments?.getInt(\"userId\") ?: 0\n        DetailsScreen(userId = userId)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Bottom Navigation",
                                "content":  "\n### Setup\n\n\n---\n\n",
                                "code":  "import androidx.compose.material.icons.Icons\nimport androidx.compose.material.icons.filled.*\nimport androidx.compose.material3.*\nimport androidx.navigation.compose.currentBackStackEntryAsState\n\nsealed class BottomNavItem(val route: String, val icon: ImageVector, val label: String) {\n    object Home : BottomNavItem(\"home\", Icons.Default.Home, \"Home\")\n    object Search : BottomNavItem(\"search\", Icons.Default.Search, \"Search\")\n    object Profile : BottomNavItem(\"profile\", Icons.Default.Person, \"Profile\")\n}\n\n@Composable\nfun MainScreen() {\n    val navController = rememberNavController()\n    val items = listOf(\n        BottomNavItem.Home,\n        BottomNavItem.Search,\n        BottomNavItem.Profile\n    )\n\n    Scaffold(\n        bottomBar = {\n            NavigationBar {\n                val navBackStackEntry by navController.currentBackStackEntryAsState()\n                val currentRoute = navBackStackEntry?.destination?.route\n\n                items.forEach { item -\u003e\n                    NavigationBarItem(\n                        icon = { Icon(item.icon, contentDescription = item.label) },\n                        label = { Text(item.label) },\n                        selected = currentRoute == item.route,\n                        onClick = {\n                            navController.navigate(item.route) {\n                                popUpTo(navController.graph.startDestinationId) {\n                                    saveState = true\n                                }\n                                launchSingleTop = true\n                                restoreState = true\n                            }\n                        }\n                    )\n                }\n            }\n        }\n    ) { innerPadding -\u003e\n        NavHost(\n            navController = navController,\n            startDestination = BottomNavItem.Home.route,\n            modifier = Modifier.padding(innerPadding)\n        ) {\n            composable(BottomNavItem.Home.route) { HomeScreen() }\n            composable(BottomNavItem.Search.route) { SearchScreen() }\n            composable(BottomNavItem.Profile.route) { ProfileScreen() }\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Navigation Drawer",
                                "content":  "\n\n---\n\n",
                                "code":  "import androidx.compose.material3.DrawerValue\nimport androidx.compose.material3.ModalDrawerSheet\nimport androidx.compose.material3.ModalNavigationDrawer\nimport androidx.compose.material3.rememberDrawerState\nimport kotlinx.coroutines.launch\n\nsealed class DrawerItem(val route: String, val icon: ImageVector, val label: String) {\n    object Home : DrawerItem(\"home\", Icons.Default.Home, \"Home\")\n    object Settings : DrawerItem(\"settings\", Icons.Default.Settings, \"Settings\")\n    object About : DrawerItem(\"about\", Icons.Default.Info, \"About\")\n}\n\n@OptIn(ExperimentalMaterial3Api::class)\n@Composable\nfun AppWithDrawer() {\n    val navController = rememberNavController()\n    val drawerState = rememberDrawerState(initialValue = DrawerValue.Closed)\n    val scope = rememberCoroutineScope()\n\n    val drawerItems = listOf(\n        DrawerItem.Home,\n        DrawerItem.Settings,\n        DrawerItem.About\n    )\n\n    ModalNavigationDrawer(\n        drawerState = drawerState,\n        drawerContent = {\n            ModalDrawerSheet {\n                Text(\n                    \"My App\",\n                    modifier = Modifier.padding(16.dp),\n                    style = MaterialTheme.typography.headlineMedium\n                )\n\n                HorizontalDivider()\n\n                drawerItems.forEach { item -\u003e\n                    NavigationDrawerItem(\n                        icon = { Icon(item.icon, contentDescription = null) },\n                        label = { Text(item.label) },\n                        selected = false,\n                        onClick = {\n                            navController.navigate(item.route)\n                            scope.launch { drawerState.close() }\n                        },\n                        modifier = Modifier.padding(horizontal = 12.dp)\n                    )\n                }\n            }\n        }\n    ) {\n        Scaffold(\n            topBar = {\n                TopAppBar(\n                    title = { Text(\"My App\") },\n                    navigationIcon = {\n                        IconButton(onClick = {\n                            scope.launch { drawerState.open() }\n                        }) {\n                            Icon(Icons.Default.Menu, contentDescription = \"Menu\")\n                        }\n                    }\n                )\n            }\n        ) { innerPadding -\u003e\n            NavHost(\n                navController = navController,\n                startDestination = DrawerItem.Home.route,\n                modifier = Modifier.padding(innerPadding)\n            ) {\n                composable(DrawerItem.Home.route) { HomeScreen() }\n                composable(DrawerItem.Settings.route) { SettingsScreen() }\n                composable(DrawerItem.About.route) { AboutScreen() }\n            }\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Nested Navigation",
                                "content":  "\n\n---\n\n",
                                "code":  "// Main navigation graph\nNavHost(navController = mainNavController, startDestination = \"main\") {\n    // Auth flow\n    navigation(startDestination = \"login\", route = \"auth\") {\n        composable(\"login\") { LoginScreen() }\n        composable(\"register\") { RegisterScreen() }\n    }\n\n    // Main app flow\n    navigation(startDestination = \"home\", route = \"main\") {\n        composable(\"home\") { HomeScreen() }\n        composable(\"profile\") { ProfileScreen() }\n\n        // Nested settings flow\n        navigation(startDestination = \"settings_main\", route = \"settings\") {\n            composable(\"settings_main\") { SettingsScreen() }\n            composable(\"settings_account\") { AccountSettingsScreen() }\n            composable(\"settings_privacy\") { PrivacySettingsScreen() }\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Deep Linking",
                                "content":  "\n### Setup in Manifest\n\n\n### Deep Link in NavGraph\n\n\n---\n\n",
                                "code":  "composable(\n    route = \"profile/{userId}\",\n    arguments = listOf(navArgument(\"userId\") { type = NavType.IntType }),\n    deepLinks = listOf(navDeepLink { uriPattern = \"myapp://profile/{userId}\" })\n) { backStackEntry -\u003e\n    val userId = backStackEntry.arguments?.getInt(\"userId\") ?: 0\n    ProfileScreen(userId = userId)\n}\n\n// Users can open: myapp://profile/123\n// App navigates directly to ProfileScreen with userId=123",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 1: Multi-Screen App",
                                "content":  "\nCreate an app with 3 screens:\n1. **Home**: List of products, click to see details\n2. **Details**: Show product info, navigate back\n3. **Cart**: Show selected items\n\n### Requirements\n- Bottom navigation (Home, Cart)\n- Pass product ID to details\n- Back button on details screen\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 1",
                                "content":  "\n\n---\n\n",
                                "code":  "import androidx.compose.foundation.clickable\nimport androidx.compose.foundation.layout.*\nimport androidx.compose.foundation.lazy.LazyColumn\nimport androidx.compose.foundation.lazy.items\nimport androidx.compose.material.icons.Icons\nimport androidx.compose.material.icons.filled.*\nimport androidx.compose.material3.*\nimport androidx.compose.runtime.*\nimport androidx.compose.ui.Modifier\nimport androidx.compose.ui.unit.dp\nimport androidx.navigation.NavController\nimport androidx.navigation.NavType\nimport androidx.navigation.compose.*\nimport androidx.navigation.navArgument\n\ndata class Product(val id: Int, val name: String, val price: Double)\n\nsealed class Screen(val route: String) {\n    object Home : Screen(\"home\")\n    object Cart : Screen(\"cart\")\n    data class Details(val productId: Int) : Screen(\"details/$productId\") {\n        companion object {\n            const val route = \"details/{productId}\"\n        }\n    }\n}\n\n@Composable\nfun ShoppingApp() {\n    val navController = rememberNavController()\n    val cart = remember { mutableStateListOf\u003cProduct\u003e() }\n\n    Scaffold(\n        bottomBar = {\n            BottomNavigationBar(navController = navController, cartCount = cart.size)\n        }\n    ) { innerPadding -\u003e\n        NavHost(\n            navController = navController,\n            startDestination = Screen.Home.route,\n            modifier = Modifier.padding(innerPadding)\n        ) {\n            composable(Screen.Home.route) {\n                HomeScreen(\n                    onProductClick = { productId -\u003e\n                        navController.navigate(Screen.Details(productId).route)\n                    }\n                )\n            }\n\n            composable(\n                route = Screen.Details.route,\n                arguments = listOf(navArgument(\"productId\") { type = NavType.IntType })\n            ) { backStackEntry -\u003e\n                val productId = backStackEntry.arguments?.getInt(\"productId\") ?: 0\n                DetailsScreen(\n                    productId = productId,\n                    onAddToCart = { product -\u003e cart.add(product) },\n                    onBack = { navController.popBackStack() }\n                )\n            }\n\n            composable(Screen.Cart.route) {\n                CartScreen(\n                    items = cart,\n                    onRemove = { product -\u003e cart.remove(product) }\n                )\n            }\n        }\n    }\n}\n\n@Composable\nfun BottomNavigationBar(navController: NavController, cartCount: Int) {\n    NavigationBar {\n        val navBackStackEntry by navController.currentBackStackEntryAsState()\n        val currentRoute = navBackStackEntry?.destination?.route\n\n        NavigationBarItem(\n            icon = { Icon(Icons.Default.Home, contentDescription = null) },\n            label = { Text(\"Home\") },\n            selected = currentRoute == Screen.Home.route,\n            onClick = {\n                navController.navigate(Screen.Home.route) {\n                    popUpTo(Screen.Home.route) { inclusive = true }\n                }\n            }\n        )\n\n        NavigationBarItem(\n            icon = {\n                BadgedBox(badge = {\n                    if (cartCount \u003e 0) {\n                        Badge { Text(\"$cartCount\") }\n                    }\n                }) {\n                    Icon(Icons.Default.ShoppingCart, contentDescription = null)\n                }\n            },\n            label = { Text(\"Cart\") },\n            selected = currentRoute == Screen.Cart.route,\n            onClick = {\n                navController.navigate(Screen.Cart.route) {\n                    popUpTo(Screen.Home.route)\n                }\n            }\n        )\n    }\n}\n\n@Composable\nfun HomeScreen(onProductClick: (Int) -\u003e Unit) {\n    val products = remember {\n        listOf(\n            Product(1, \"Laptop\", 999.99),\n            Product(2, \"Mouse\", 29.99),\n            Product(3, \"Keyboard\", 79.99),\n            Product(4, \"Monitor\", 299.99)\n        )\n    }\n\n    LazyColumn(\n        modifier = Modifier.fillMaxSize(),\n        contentPadding = PaddingValues(16.dp),\n        verticalArrangement = Arrangement.spacedBy(8.dp)\n    ) {\n        items(products) { product -\u003e\n            Card(\n                modifier = Modifier\n                    .fillMaxWidth()\n                    .clickable { onProductClick(product.id) }\n            ) {\n                Row(\n                    modifier = Modifier.padding(16.dp),\n                    horizontalArrangement = Arrangement.SpaceBetween\n                ) {\n                    Column {\n                        Text(product.name, style = MaterialTheme.typography.titleMedium)\n                        Text(\"${product.price}\", color = MaterialTheme.colorScheme.primary)\n                    }\n                    Icon(Icons.Default.ChevronRight, contentDescription = null)\n                }\n            }\n        }\n    }\n}\n\n@OptIn(ExperimentalMaterial3Api::class)\n@Composable\nfun DetailsScreen(\n    productId: Int,\n    onAddToCart: (Product) -\u003e Unit,\n    onBack: () -\u003e Unit\n) {\n    val product = remember {\n        listOf(\n            Product(1, \"Laptop\", 999.99),\n            Product(2, \"Mouse\", 29.99),\n            Product(3, \"Keyboard\", 79.99),\n            Product(4, \"Monitor\", 299.99)\n        ).find { it.id == productId }\n    }\n\n    Scaffold(\n        topBar = {\n            TopAppBar(\n                title = { Text(\"Product Details\") },\n                navigationIcon = {\n                    IconButton(onClick = onBack) {\n                        Icon(Icons.Default.ArrowBack, contentDescription = \"Back\")\n                    }\n                }\n            )\n        }\n    ) { innerPadding -\u003e\n        Column(\n            modifier = Modifier\n                .fillMaxSize()\n                .padding(innerPadding)\n                .padding(16.dp)\n        ) {\n            product?.let { p -\u003e\n                Text(p.name, style = MaterialTheme.typography.headlineLarge)\n                Spacer(modifier = Modifier.height(16.dp))\n                Text(\"Price: ${p.price}\", style = MaterialTheme.typography.titleLarge)\n                Spacer(modifier = Modifier.height(32.dp))\n                Button(\n                    onClick = { onAddToCart(p) },\n                    modifier = Modifier.fillMaxWidth()\n                ) {\n                    Icon(Icons.Default.AddShoppingCart, contentDescription = null)\n                    Spacer(modifier = Modifier.width(8.dp))\n                    Text(\"Add to Cart\")\n                }\n            }\n        }\n    }\n}\n\n@Composable\nfun CartScreen(items: List\u003cProduct\u003e, onRemove: (Product) -\u003e Unit) {\n    Column(modifier = Modifier.fillMaxSize().padding(16.dp)) {\n        Text(\"Cart (${items.size} items)\", style = MaterialTheme.typography.headlineMedium)\n\n        if (items.isEmpty()) {\n            Box(modifier = Modifier.fillMaxSize(), contentAlignment = androidx.compose.ui.Alignment.Center) {\n                Text(\"Cart is empty\")\n            }\n        } else {\n            val total = items.sumOf { it.price }\n\n            LazyColumn(modifier = Modifier.weight(1f)) {\n                items(items) { product -\u003e\n                    Card(modifier = Modifier.fillMaxWidth().padding(vertical = 4.dp)) {\n                        Row(\n                            modifier = Modifier.padding(16.dp),\n                            horizontalArrangement = Arrangement.SpaceBetween\n                        ) {\n                            Column {\n                                Text(product.name)\n                                Text(\"${product.price}\", color = MaterialTheme.colorScheme.primary)\n                            }\n                            IconButton(onClick = { onRemove(product) }) {\n                                Icon(Icons.Default.Delete, contentDescription = \"Remove\")\n                            }\n                        }\n                    }\n                }\n            }\n\n            HorizontalDivider()\n            Row(\n                modifier = Modifier.fillMaxWidth().padding(vertical = 16.dp),\n                horizontalArrangement = Arrangement.SpaceBetween\n            ) {\n                Text(\"Total:\", style = MaterialTheme.typography.titleLarge)\n                Text(\"${String.format(\"%.2f\", total)}\", style = MaterialTheme.typography.titleLarge)\n            }\n            Button(onClick = { }, modifier = Modifier.fillMaxWidth()) {\n                Text(\"Checkout\")\n            }\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 2: Settings with Nested Navigation",
                                "content":  "\nCreate a settings screen with nested navigation:\n- Main settings (General, Account, Privacy)\n- Each opens a sub-screen\n- Back navigation works correctly\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 2",
                                "content":  "\n\n---\n\n",
                                "code":  "sealed class SettingsScreen(val route: String, val title: String) {\n    object Main : SettingsScreen(\"settings_main\", \"Settings\")\n    object General : SettingsScreen(\"settings_general\", \"General\")\n    object Account : SettingsScreen(\"settings_account\", \"Account\")\n    object Privacy : SettingsScreen(\"settings_privacy\", \"Privacy\")\n}\n\n@OptIn(ExperimentalMaterial3Api::class)\n@Composable\nfun SettingsApp() {\n    val navController = rememberNavController()\n\n    Scaffold(\n        topBar = {\n            val navBackStackEntry by navController.currentBackStackEntryAsState()\n            val currentRoute = navBackStackEntry?.destination?.route\n            val title = when (currentRoute) {\n                SettingsScreen.General.route -\u003e SettingsScreen.General.title\n                SettingsScreen.Account.route -\u003e SettingsScreen.Account.title\n                SettingsScreen.Privacy.route -\u003e SettingsScreen.Privacy.title\n                else -\u003e SettingsScreen.Main.title\n            }\n\n            TopAppBar(\n                title = { Text(title) },\n                navigationIcon = {\n                    if (currentRoute != SettingsScreen.Main.route) {\n                        IconButton(onClick = { navController.popBackStack() }) {\n                            Icon(Icons.Default.ArrowBack, contentDescription = \"Back\")\n                        }\n                    }\n                }\n            )\n        }\n    ) { innerPadding -\u003e\n        NavHost(\n            navController = navController,\n            startDestination = SettingsScreen.Main.route,\n            modifier = Modifier.padding(innerPadding)\n        ) {\n            composable(SettingsScreen.Main.route) {\n                SettingsMainScreen(onNavigate = { route -\u003e\n                    navController.navigate(route)\n                })\n            }\n\n            composable(SettingsScreen.General.route) {\n                GeneralSettingsScreen()\n            }\n\n            composable(SettingsScreen.Account.route) {\n                AccountSettingsScreen()\n            }\n\n            composable(SettingsScreen.Privacy.route) {\n                PrivacySettingsScreen()\n            }\n        }\n    }\n}\n\n@Composable\nfun SettingsMainScreen(onNavigate: (String) -\u003e Unit) {\n    LazyColumn {\n        item {\n            SettingsItem(\n                title = \"General\",\n                subtitle = \"App preferences\",\n                icon = Icons.Default.Settings,\n                onClick = { onNavigate(SettingsScreen.General.route) }\n            )\n        }\n        item {\n            SettingsItem(\n                title = \"Account\",\n                subtitle = \"Manage your account\",\n                icon = Icons.Default.Person,\n                onClick = { onNavigate(SettingsScreen.Account.route) }\n            )\n        }\n        item {\n            SettingsItem(\n                title = \"Privacy\",\n                subtitle = \"Privacy and security\",\n                icon = Icons.Default.Lock,\n                onClick = { onNavigate(SettingsScreen.Privacy.route) }\n            )\n        }\n    }\n}\n\n@Composable\nfun SettingsItem(\n    title: String,\n    subtitle: String,\n    icon: ImageVector,\n    onClick: () -\u003e Unit\n) {\n    Row(\n        modifier = Modifier\n            .fillMaxWidth()\n            .clickable(onClick = onClick)\n            .padding(16.dp),\n        verticalAlignment = androidx.compose.ui.Alignment.CenterVertically\n    ) {\n        Icon(icon, contentDescription = null, tint = MaterialTheme.colorScheme.primary)\n        Spacer(modifier = Modifier.width(16.dp))\n        Column(modifier = Modifier.weight(1f)) {\n            Text(title, style = MaterialTheme.typography.bodyLarge)\n            Text(subtitle, style = MaterialTheme.typography.bodySmall, color = MaterialTheme.colorScheme.onSurfaceVariant)\n        }\n        Icon(Icons.Default.ChevronRight, contentDescription = null)\n    }\n}\n\n@Composable\nfun GeneralSettingsScreen() {\n    var darkMode by remember { mutableStateOf(false) }\n    var notifications by remember { mutableStateOf(true) }\n\n    Column(modifier = Modifier.padding(16.dp)) {\n        SwitchSetting(\"Dark Mode\", darkMode) { darkMode = it }\n        SwitchSetting(\"Notifications\", notifications) { notifications = it }\n    }\n}\n\n@Composable\nfun AccountSettingsScreen() {\n    Column(modifier = Modifier.padding(16.dp)) {\n        Text(\"Email: user@example.com\")\n        Spacer(modifier = Modifier.height(16.dp))\n        Button(onClick = { }) {\n            Text(\"Change Password\")\n        }\n    }\n}\n\n@Composable\nfun PrivacySettingsScreen() {\n    var analytics by remember { mutableStateOf(true) }\n\n    Column(modifier = Modifier.padding(16.dp)) {\n        SwitchSetting(\"Share Analytics\", analytics) { analytics = it }\n    }\n}\n\n@Composable\nfun SwitchSetting(label: String, checked: Boolean, onCheckedChange: (Boolean) -\u003e Unit) {\n    Row(\n        modifier = Modifier.fillMaxWidth().padding(vertical = 8.dp),\n        horizontalArrangement = Arrangement.SpaceBetween,\n        verticalAlignment = androidx.compose.ui.Alignment.CenterVertically\n    ) {\n        Text(label)\n        Switch(checked = checked, onCheckedChange = onCheckedChange)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 3: Tab Navigation",
                                "content":  "\nCreate a tabbed interface:\n- 3 tabs: Feed, Discover, Profile\n- Use TabRow\n- Content changes based on selected tab\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 3",
                                "content":  "\n\n---\n\n",
                                "code":  "@Composable\nfun TabbedApp() {\n    var selectedTab by remember { mutableStateOf(0) }\n    val tabs = listOf(\"Feed\", \"Discover\", \"Profile\")\n\n    Column(modifier = Modifier.fillMaxSize()) {\n        TabRow(selectedTabIndex = selectedTab) {\n            tabs.forEachIndexed { index, title -\u003e\n                Tab(\n                    selected = selectedTab == index,\n                    onClick = { selectedTab = index },\n                    text = { Text(title) }\n                )\n            }\n        }\n\n        when (selectedTab) {\n            0 -\u003e FeedScreen()\n            1 -\u003e DiscoverScreen()\n            2 -\u003e ProfileScreen()\n        }\n    }\n}\n\n@Composable\nfun FeedScreen() {\n    Box(modifier = Modifier.fillMaxSize(), contentAlignment = androidx.compose.ui.Alignment.Center) {\n        Text(\"Feed Content\", style = MaterialTheme.typography.headlineMedium)\n    }\n}\n\n@Composable\nfun DiscoverScreen() {\n    Box(modifier = Modifier.fillMaxSize(), contentAlignment = androidx.compose.ui.Alignment.Center) {\n        Text(\"Discover Content\", style = MaterialTheme.typography.headlineMedium)\n    }\n}\n\n@Composable\nfun ProfileScreen() {\n    Box(modifier = Modifier.fillMaxSize(), contentAlignment = androidx.compose.ui.Alignment.Center) {\n        Text(\"Profile Content\", style = MaterialTheme.typography.headlineMedium)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Navigation on iOS",
                                "content":  "\n### Critical Platform Differences\n\nNavigation behavior differs significantly between Android and iOS. Understanding these differences is crucial for building great cross-platform apps.\n\n| Feature | Android | iOS |\n|---------|---------|-----|\n| **Back Navigation** | Hardware/gesture back button | Swipe from left edge |\n| **Back Button** | System provides it | You must add a back button in the UI |\n| **Navigation Bar** | Optional TopAppBar | Expected on every screen |\n| **Swipe Gesture** | Edge swipe optional | Users expect swipe-back |\n| **Tab Bar Position** | Bottom (Material) | Bottom (iOS standard) |\n| **Drawer** | Common pattern | Less common on iOS |\n\n### iOS Swipe-Back Gesture\n\nOn iOS, users expect to swipe from the left edge to go back. Compose Multiplatform supports this automatically when you use navigation properly:\n\n```kotlin\n// The navigation back stack handles iOS swipe-back\n@Composable\nfun DetailsScreen(\n    onNavigateBack: () -\u003e Unit\n) {\n    Scaffold(\n        topBar = {\n            TopAppBar(\n                title = { Text(\"Details\") },\n                // Always show back button on iOS!\n                navigationIcon = {\n                    IconButton(onClick = onNavigateBack) {\n                        Icon(\n                            Icons.AutoMirrored.Filled.ArrowBack,\n                            contentDescription = \"Back\"\n                        )\n                    }\n                }\n            )\n        }\n    ) { /* content */ }\n}\n```\n\n### Best Practices for Cross-Platform Navigation\n\n1. **Always include a visible back button** - iOS has no hardware back\n2. **Use TopAppBar consistently** - iOS users expect it\n3. **Avoid navigation drawers on iOS** - Use tab bars instead\n4. **Test swipe-back gesture** - Run on iOS Simulator regularly\n5. **Handle safe areas** - Status bar and home indicator differ\n\n### Running Navigation on iOS Simulator\n\n1. Build and run on iOS Simulator\n2. Navigate to a detail screen\n3. Try swiping from the left edge to go back\n4. Verify the back button works\n5. Test bottom navigation tabs\n\n### Platform-Specific Navigation Patterns\n\n```kotlin\n// In commonMain - adapts to each platform\n@Composable\nfun AppNavigation() {\n    val navController = rememberNavController()\n    \n    Scaffold(\n        // Bottom bar works on both platforms\n        bottomBar = {\n            NavigationBar {\n                // Tab items...\n            }\n        }\n    ) { padding -\u003e\n        NavHost(\n            navController = navController,\n            startDestination = \"home\",\n            modifier = Modifier.padding(padding)\n        ) {\n            composable(\"home\") {\n                HomeScreen(\n                    onItemClick = { id -\u003e\n                        navController.navigate(\"details/$id\")\n                    }\n                )\n            }\n            composable(\"details/{id}\") { backStackEntry -\u003e\n                DetailsScreen(\n                    id = backStackEntry.arguments?.getString(\"id\") ?: \"\",\n                    // popBackStack works on both Android and iOS!\n                    onNavigateBack = { navController.popBackStack() }\n                )\n            }\n        }\n    }\n}\n```\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why This Matters",
                                "content":  "\n**User Expectations**:\n- Smooth transitions between screens\n- Back button/gesture works correctly\n- Deep links open correct screens\n- State preserved during navigation\n- **iOS users expect swipe-back to work**\n\n**Statistics**:\n- Apps with poor navigation have **75% higher** uninstall rates\n- Users abandon apps if they can\u0027t find features within **3 taps**\n- Deep linking increases engagement by **2x**\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1\nWhat is NavController responsible for?\n\nA) Creating screens\nB) Managing navigation between destinations\nC) Displaying UI\nD) Handling user input\n\n### Question 2\nHow do you pass arguments between screens?\n\nA) Global variables\nB) Route parameters like \"details/{id}\"\nC) Shared preferences\nD) Broadcast receivers\n\n### Question 3\nWhat does `popBackStack()` do?\n\nA) Deletes all screens\nB) Navigates back to previous screen\nC) Opens a dialog\nD) Saves navigation state\n\n### Question 4\nWhat\u0027s the benefit of type-safe navigation with sealed classes?\n\nA) Faster performance\nB) Compile-time safety and autocomplete\nC) Smaller app size\nD) Better animations\n\n### Question 5\nWhen should you use nested navigation?\n\nA) For all navigation\nB) For grouping related screens (auth flow, settings)\nC) Never\nD) Only for deep linking\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quiz Answers",
                                "content":  "\n**Question 1: B** - NavController manages navigation state and transitions\n**Question 2: B** - Use route parameters: `\"details/{id}\"`, access with `navArgument`\n**Question 3: B** - Navigates back, removes current screen from stack\n**Question 4: B** - Compile-time checks prevent typos, IDE autocomplete\n**Question 5: B** - Group related screens logically (auth, settings, onboarding)\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What You\u0027ve Learned",
                                "content":  "\n✅ Setting up Navigation Compose\n✅ NavController and NavHost basics\n✅ Passing arguments between screens\n✅ Bottom navigation bars\n✅ Navigation drawer\n✅ Nested navigation graphs\n✅ Deep linking support\n✅ Type-safe navigation patterns\n✅ **iOS swipe-back gesture and back button requirements**\n✅ **Platform-specific navigation best practices**\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps",
                                "content":  "\nIn **Lesson 6.6: Networking and APIs**, you\u0027ll learn:\n- Ktor client setup for cross-platform networking\n- Kotlin serialization\n- Coroutines for async networking\n- Error handling\n- Loading states\n- Image loading\n\nGet ready to connect your app to the internet on both Android and iOS!\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "advanced",
    "title":  "Lesson 6.5: Navigation",
    "estimatedMinutes":  75
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current kotlin documentation
- Search the web for the latest kotlin version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "kotlin Lesson 6.5: Navigation 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "6.5",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

