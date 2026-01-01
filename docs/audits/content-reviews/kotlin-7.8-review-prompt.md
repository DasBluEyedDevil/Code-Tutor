# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Professional Development & Deployment
- **Lesson:** Lesson 7.8: Final Capstone - Full Stack E-Commerce Platform (ID: 7.8)
- **Difficulty:** advanced
- **Estimated Time:** 30 minutes

## Current Lesson Content

{
    "id":  "7.8",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 12-16 hours\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Congratulations! 🎉",
                                "content":  "\nYou\u0027ve completed all 7 parts of the Kotlin Training Course! You\u0027ve learned:\n- Part 1: Kotlin fundamentals\n- Part 2: Object-oriented programming\n- Part 3: Functional programming and coroutines\n- Part 4: Collections and advanced features\n- Part 5: Backend development with Ktor\n- Part 6: Android development with Jetpack Compose\n- Part 7: Advanced topics (KMP, testing, security, deployment)\n\nNow it\u0027s time to prove your mastery by building a **complete, production-ready, full-stack e-commerce platform**!\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Project Overview: ShopKotlin",
                                "content":  "\n**ShopKotlin** is a modern e-commerce platform with:\n\n### Backend (Ktor)\n- RESTful API\n- PostgreSQL database\n- JWT authentication\n- Product catalog management\n- Shopping cart\n- Order processing\n- Payment integration (Stripe)\n- Admin panel\n- Comprehensive testing\n- CI/CD pipeline\n- Cloud deployment\n- Monitoring and analytics\n\n### Android App (Jetpack Compose)\n- Beautiful Material Design 3 UI\n- Product browsing and search\n- Shopping cart\n- User authentication\n- Order tracking\n- Offline support\n- Push notifications\n- Analytics\n\n### Full Feature Set\n- ✅ User registration and login\n- ✅ Product catalog with categories\n- ✅ Product search and filtering\n- ✅ Shopping cart management\n- ✅ Checkout with payment\n- ✅ Order history\n- ✅ Admin dashboard\n- ✅ Inventory management\n- ✅ Real-time order tracking\n- ✅ Email notifications\n- ✅ Analytics dashboard\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Architecture Overview",
                                "content":  "\n\n---\n\n",
                                "code":  "┌─────────────────────────────────────────────────────────────┐\n│                      Client Applications                     │\n├──────────────────────────┬──────────────────────────────────┤\n│   Android App            │   Web Admin Dashboard            │\n│   (Jetpack Compose)      │   (React/Vue - Optional)         │\n└──────────────────────────┴──────────────────────────────────┘\n                           │\n                           ▼\n┌─────────────────────────────────────────────────────────────┐\n│                     API Gateway / Load Balancer             │\n└─────────────────────────────────────────────────────────────┘\n                           │\n                           ▼\n┌─────────────────────────────────────────────────────────────┐\n│                    Ktor Backend API                          │\n├─────────────────────────────────────────────────────────────┤\n│  ┌────────────┐  ┌────────────┐  ┌────────────┐            │\n│  │   Auth     │  │  Products  │  │   Orders   │            │\n│  │  Service   │  │  Service   │  │  Service   │            │\n│  └────────────┘  └────────────┘  └────────────┘            │\n│                                                              │\n│  ┌────────────┐  ┌────────────┐  ┌────────────┐            │\n│  │  Payment   │  │   Email    │  │ Analytics  │            │\n│  │  Service   │  │  Service   │  │  Service   │            │\n│  └────────────┘  └────────────┘  └────────────┘            │\n└─────────────────────────────────────────────────────────────┘\n                           │\n                           ▼\n┌─────────────────────────────────────────────────────────────┐\n│                      Data Layer                              │\n├──────────────────┬──────────────────┬───────────────────────┤\n│   PostgreSQL     │      Redis       │     File Storage      │\n│   (Primary DB)   │     (Cache)      │   (S3/CloudStorage)   │\n└──────────────────┴──────────────────┴───────────────────────┘",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 1: Project Setup (1-2 hours)",
                                "content":  "\n### Backend Project Structure\n\n\n### build.gradle.kts (Backend)\n\n\n### Android Project Structure\n\n\n---\n\n",
                                "code":  "shopkotlin-android/\n├── app/\n│   ├── src/\n│   │   ├── main/\n│   │   │   ├── kotlin/com/shopkotlin/\n│   │   │   │   ├── MainActivity.kt\n│   │   │   │   ├── ShopKotlinApp.kt\n│   │   │   │   ├── ui/\n│   │   │   │   │   ├── screens/\n│   │   │   │   │   │   ├── auth/\n│   │   │   │   │   │   ├── home/\n│   │   │   │   │   │   ├── product/\n│   │   │   │   │   │   ├── cart/\n│   │   │   │   │   │   └── orders/\n│   │   │   │   │   ├── components/\n│   │   │   │   │   └── theme/\n│   │   │   │   ├── data/\n│   │   │   │   │   ├── api/\n│   │   │   │   │   ├── database/\n│   │   │   │   │   ├── repository/\n│   │   │   │   │   └── models/\n│   │   │   │   ├── domain/\n│   │   │   │   │   └── usecases/\n│   │   │   │   └── di/\n│   │   │   └── AndroidManifest.xml\n│   │   └── test/\n│   └── build.gradle.kts\n└── build.gradle.kts",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 2: Backend Development (4-6 hours)",
                                "content":  "\n### 2.1 Database Schema\n\n\n### 2.2 Models\n\n\n### 2.3 Core Services\n\n\n### 2.4 API Routes\n\n\n---\n\n",
                                "code":  "// src/main/kotlin/com/shopkotlin/routes/productRoutes.kt\npackage com.shopkotlin.routes\n\nimport com.shopkotlin.services.ProductService\nimport io.ktor.server.application.*\nimport io.ktor.server.response.*\nimport io.ktor.server.routing.*\nimport io.ktor.http.*\n\nfun Route.productRoutes(productService: ProductService) {\n    route(\"/api/products\") {\n        get {\n            val category = call.parameters[\"category\"]\n            val search = call.parameters[\"search\"]\n            val featured = call.parameters[\"featured\"]?.toBoolean()\n            val limit = call.parameters[\"limit\"]?.toIntOrNull() ?: 50\n            val offset = call.parameters[\"offset\"]?.toIntOrNull() ?: 0\n\n            val products = when {\n                search != null -\u003e productService.search(search, limit, offset)\n                category != null -\u003e productService.getByCategory(category, limit, offset)\n                featured == true -\u003e productService.getFeatured(limit)\n                else -\u003e productService.getAll(limit, offset)\n            }\n\n            call.respond(ApiResponse(success = true, data = products))\n        }\n\n        get(\"/{id}\") {\n            val id = call.parameters[\"id\"]\n                ?: return@get call.respond(\n                    HttpStatusCode.BadRequest,\n                    ApiResponse\u003cUnit\u003e(success = false, message = \"Product ID required\")\n                )\n\n            val product = productService.getById(id)\n                ?: return@get call.respond(\n                    HttpStatusCode.NotFound,\n                    ApiResponse\u003cUnit\u003e(success = false, message = \"Product not found\")\n                )\n\n            call.respond(ApiResponse(success = true, data = product))\n        }\n    }\n\n    route(\"/api/categories\") {\n        get {\n            val categories = productService.getAllCategories()\n            call.respond(ApiResponse(success = true, data = categories))\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 3: Android App Development (4-6 hours)",
                                "content":  "\n### 3.1 API Client\n\n\n### 3.2 Product Screen\n\n\n### 3.3 Cart ViewModel\n\n\n---\n\n",
                                "code":  "// app/src/main/kotlin/com/shopkotlin/ui/screens/cart/CartViewModel.kt\npackage com.shopkotlin.ui.screens.cart\n\nimport androidx.lifecycle.ViewModel\nimport androidx.lifecycle.viewModelScope\nimport com.shopkotlin.data.repository.CartRepository\nimport com.shopkotlin.models.CartItem\nimport kotlinx.coroutines.flow.*\nimport kotlinx.coroutines.launch\n\nclass CartViewModel(\n    private val cartRepository: CartRepository\n) : ViewModel() {\n\n    private val _cartItems = MutableStateFlow\u003cList\u003cCartItem\u003e\u003e(emptyList())\n    val cartItems: StateFlow\u003cList\u003cCartItem\u003e\u003e = _cartItems.asStateFlow()\n\n    val totalAmount: StateFlow\u003cDouble\u003e = cartItems.map { items -\u003e\n        items.sumOf { it.product.price * it.quantity }\n    }.stateIn(viewModelScope, SharingStarted.WhileSubscribed(5000), 0.0)\n\n    init {\n        loadCart()\n    }\n\n    private fun loadCart() {\n        viewModelScope.launch {\n            cartRepository.getCartItems().collect { items -\u003e\n                _cartItems.value = items\n            }\n        }\n    }\n\n    fun updateQuantity(productId: String, quantity: Int) {\n        viewModelScope.launch {\n            if (quantity \u003c= 0) {\n                cartRepository.removeFromCart(productId)\n            } else {\n                cartRepository.updateQuantity(productId, quantity)\n            }\n        }\n    }\n\n    fun removeItem(productId: String) {\n        viewModelScope.launch {\n            cartRepository.removeFromCart(productId)\n        }\n    }\n\n    fun clearCart() {\n        viewModelScope.launch {\n            cartRepository.clearCart()\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 4: Testing (2-3 hours)",
                                "content":  "\n### Backend Tests\n\n\n### Android Tests\n\n\n---\n\n",
                                "code":  "// app/src/test/kotlin/com/shopkotlin/CartViewModelTest.kt\npackage com.shopkotlin\n\nimport app.cash.turbine.test\nimport com.shopkotlin.data.repository.CartRepository\nimport com.shopkotlin.models.*\nimport com.shopkotlin.ui.screens.cart.CartViewModel\nimport io.mockk.*\nimport kotlinx.coroutines.flow.flowOf\nimport kotlinx.coroutines.test.*\nimport org.junit.jupiter.api.Test\nimport kotlin.test.assertEquals\n\nclass CartViewModelTest {\n\n    private val cartRepository = mockk\u003cCartRepository\u003e()\n    private val viewModel = CartViewModel(cartRepository)\n\n    @Test\n    fun `cart items should be loaded on init`() = runTest {\n        // Arrange\n        val cartItems = listOf(\n            CartItem(product = mockk(), quantity = 2)\n        )\n\n        coEvery { cartRepository.getCartItems() } returns flowOf(cartItems)\n\n        // Act\n        viewModel.cartItems.test {\n            val items = awaitItem()\n\n            // Assert\n            assertEquals(cartItems, items)\n        }\n    }\n\n    @Test\n    fun `updateQuantity should call repository`() = runTest {\n        coEvery { cartRepository.updateQuantity(any(), any()) } just Runs\n\n        viewModel.updateQuantity(\"product1\", 5)\n\n        coVerify { cartRepository.updateQuantity(\"product1\", 5) }\n    }\n\n    @Test\n    fun `totalAmount should sum all items`() = runTest {\n        val product1 = mockk\u003cProduct\u003e {\n            every { price } returns 10.0\n        }\n        val product2 = mockk\u003cProduct\u003e {\n            every { price } returns 20.0\n        }\n\n        val cartItems = listOf(\n            CartItem(product1, 2), // 20.0\n            CartItem(product2, 3)  // 60.0\n        )\n\n        coEvery { cartRepository.getCartItems() } returns flowOf(cartItems)\n\n        viewModel.totalAmount.test {\n            val total = awaitItem()\n            assertEquals(80.0, total, 0.01)\n        }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 5: CI/CD Pipeline (1-2 hours)",
                                "content":  "\n### GitHub Actions Workflow\n\n\n---\n\n",
                                "code":  "# .github/workflows/ci-cd.yml\nname: ShopKotlin CI/CD\n\non:\n  push:\n    branches: [ main, develop ]\n  pull_request:\n    branches: [ main ]\n\nenv:\n  JAVA_VERSION: \u002717\u0027\n\njobs:\n  backend-test:\n    name: Backend Tests\n    runs-on: ubuntu-latest\n\n    services:\n      postgres:\n        image: postgres:15\n        env:\n          POSTGRES_PASSWORD: testpass\n          POSTGRES_DB: shopkotlin_test\n        options: \u003e-\n          --health-cmd pg_isready\n          --health-interval 10s\n          --health-timeout 5s\n          --health-retries 5\n        ports:\n          - 5432:5432\n\n    steps:\n      - uses: actions/checkout@v4\n\n      - name: Set up JDK\n        uses: actions/setup-java@v4\n        with:\n          java-version: ${{ env.JAVA_VERSION }}\n          distribution: \u0027temurin\u0027\n\n      - name: Run backend tests\n        run: |\n          cd shopkotlin-backend\n          ./gradlew test\n\n      - name: Upload coverage\n        uses: codecov/codecov-action@v3\n        with:\n          files: shopkotlin-backend/build/reports/jacoco/test/jacocoTestReport.xml\n\n  backend-build:\n    name: Build Backend\n    needs: backend-test\n    runs-on: ubuntu-latest\n\n    steps:\n      - uses: actions/checkout@v4\n\n      - name: Set up JDK\n        uses: actions/setup-java@v4\n        with:\n          java-version: ${{ env.JAVA_VERSION }}\n          distribution: \u0027temurin\u0027\n\n      - name: Build JAR\n        run: |\n          cd shopkotlin-backend\n          ./gradlew shadowJar\n\n      - name: Build Docker image\n        run: |\n          cd shopkotlin-backend\n          docker build -t shopkotlin-backend:latest .\n\n      - name: Push to registry (main only)\n        if: github.ref == \u0027refs/heads/main\u0027\n        run: |\n          echo \"${{ secrets.DOCKER_PASSWORD }}\" | docker login -u \"${{ secrets.DOCKER_USERNAME }}\" --password-stdin\n          docker tag shopkotlin-backend:latest ${{ secrets.DOCKER_USERNAME }}/shopkotlin-backend:latest\n          docker push ${{ secrets.DOCKER_USERNAME }}/shopkotlin-backend:latest\n\n  android-test:\n    name: Android Tests\n    runs-on: ubuntu-latest\n\n    steps:\n      - uses: actions/checkout@v4\n\n      - name: Set up JDK\n        uses: actions/setup-java@v4\n        with:\n          java-version: ${{ env.JAVA_VERSION }}\n          distribution: \u0027temurin\u0027\n\n      - name: Run unit tests\n        run: |\n          cd shopkotlin-android\n          ./gradlew test\n\n  android-build:\n    name: Build Android APK\n    needs: android-test\n    runs-on: ubuntu-latest\n\n    steps:\n      - uses: actions/checkout@v4\n\n      - name: Set up JDK\n        uses: actions/setup-java@v4\n        with:\n          java-version: ${{ env.JAVA_VERSION }}\n          distribution: \u0027temurin\u0027\n\n      - name: Build debug APK\n        run: |\n          cd shopkotlin-android\n          ./gradlew assembleDebug\n\n      - name: Upload APK\n        uses: actions/upload-artifact@v4\n        with:\n          name: app-debug\n          path: shopkotlin-android/app/build/outputs/apk/debug/app-debug.apk\n\n  deploy:\n    name: Deploy to Production\n    needs: [backend-build, android-build]\n    if: github.ref == \u0027refs/heads/main\u0027\n    runs-on: ubuntu-latest\n\n    steps:\n      - name: Deploy to Heroku\n        uses: akhileshns/heroku-deploy@v3.12.14\n        with:\n          heroku_api_key: ${{ secrets.HEROKU_API_KEY }}\n          heroku_app_name: \"shopkotlin-api\"\n          heroku_email: ${{ secrets.HEROKU_EMAIL }}\n          appdir: \"shopkotlin-backend\"",
                                "language":  "yaml"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Phase 6: Deployment (1-2 hours)",
                                "content":  "\n### Docker Setup\n\n**shopkotlin-backend/Dockerfile**:\n\n**docker-compose.yml**:\n\n---\n\n",
                                "code":  "version: \u00273.8\u0027\n\nservices:\n  backend:\n    build: ./shopkotlin-backend\n    ports:\n      - \"8080:8080\"\n    environment:\n      - DB_HOST=db\n      - DB_PORT=5432\n      - DB_NAME=shopkotlin\n      - DB_USER=shopkotlin\n      - DB_PASSWORD=${DB_PASSWORD}\n      - JWT_SECRET=${JWT_SECRET}\n      - STRIPE_API_KEY=${STRIPE_API_KEY}\n    depends_on:\n      db:\n        condition: service_healthy\n\n  db:\n    image: postgres:15-alpine\n    environment:\n      - POSTGRES_DB=shopkotlin\n      - POSTGRES_USER=shopkotlin\n      - POSTGRES_PASSWORD=${DB_PASSWORD}\n    volumes:\n      - postgres_data:/var/lib/postgresql/data\n    healthcheck:\n      test: [\"CMD-SHELL\", \"pg_isready -U shopkotlin\"]\n      interval: 10s\n      timeout: 5s\n      retries: 5\n\n  nginx:\n    image: nginx:alpine\n    ports:\n      - \"80:80\"\n      - \"443:443\"\n    volumes:\n      - ./nginx.conf:/etc/nginx/nginx.conf:ro\n      - ./ssl:/etc/nginx/ssl:ro\n    depends_on:\n      - backend\n\nvolumes:\n  postgres_data:",
                                "language":  "yaml"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Extension Challenges",
                                "content":  "\nOnce you\u0027ve completed the core project, challenge yourself with these extensions:\n\n### 1. Advanced Features\n- ⭐ Product reviews and ratings\n- ⭐ Wishlist functionality\n- ⭐ Order tracking with real-time updates\n- ⭐ Coupon/discount codes\n- ⭐ Product recommendations (ML-based)\n- ⭐ Multi-currency support\n\n### 2. Mobile Enhancements\n- ⭐ Offline mode with Room database\n- ⭐ Push notifications for order updates\n- ⭐ Biometric authentication\n- ⭐ Dark mode\n- ⭐ Animations and transitions\n- ⭐ Widget for quick access\n\n### 3. Admin Features\n- ⭐ Admin dashboard (web or mobile)\n- ⭐ Inventory management\n- ⭐ Sales analytics\n- ⭐ User management\n- ⭐ Product CRUD operations\n\n### 4. Technical Improvements\n- ⭐ GraphQL instead of REST\n- ⭐ gRPC for mobile-backend communication\n- ⭐ Redis caching layer\n- ⭐ Elasticsearch for advanced search\n- ⭐ WebSockets for real-time features\n- ⭐ Rate limiting and DDoS protection\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Submission Checklist",
                                "content":  "\nBefore submitting, ensure you have:\n\n**Backend**:\n- [ ] All API endpoints working\n- [ ] JWT authentication implemented\n- [ ] PostgreSQL database setup\n- [ ] Stripe payment integration\n- [ ] Unit tests with 70%+ coverage\n- [ ] Integration tests for main flows\n- [ ] Docker container working\n- [ ] Deployed to cloud (Heroku/AWS/GCP)\n- [ ] Environment variables configured\n- [ ] Logging and error tracking (Sentry)\n\n**Android**:\n- [ ] All screens implemented\n- [ ] API integration complete\n- [ ] Authentication flow working\n- [ ] Cart and checkout functional\n- [ ] Order history displayed\n- [ ] Unit tests for ViewModels\n- [ ] UI tests for critical flows\n- [ ] APK built successfully\n- [ ] App runs on physical device\n\n**DevOps**:\n- [ ] CI/CD pipeline configured\n- [ ] Automated tests running\n- [ ] Docker images building\n- [ ] Deployment automated\n- [ ] Monitoring setup\n\n**Documentation**:\n- [ ] README with setup instructions\n- [ ] API documentation\n- [ ] Architecture diagrams\n- [ ] Environment setup guide\n- [ ] Deployment guide\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Final Thoughts",
                                "content":  "\nCongratulations on completing the Kotlin Training Course! 🎉🎉🎉\n\nYou\u0027ve built a **production-ready, full-stack e-commerce platform** using:\n- Kotlin (backend and Android)\n- Ktor (REST API)\n- PostgreSQL (database)\n- Jetpack Compose (modern Android UI)\n- JWT authentication\n- Stripe payments\n- Docker (containerization)\n- GitHub Actions (CI/CD)\n- Cloud deployment\n\n### You\u0027ve Mastered:\n✅ Kotlin fundamentals and advanced features\n✅ Backend development with Ktor\n✅ Android development with Jetpack Compose\n✅ Database design and optimization\n✅ API design and security\n✅ Testing strategies (unit, integration, UI)\n✅ DevOps practices (CI/CD, Docker)\n✅ Cloud deployment\n✅ Performance optimization\n✅ Security best practices\n✅ Monitoring and analytics\n\n### What\u0027s Next?\n\n**1. Enhance Your Project**:\n- Add the extension challenges\n- Deploy to production\n- Get real users\n- Collect feedback\n\n**2. Build Your Portfolio**:\n- Showcase ShopKotlin on GitHub\n- Write blog posts about your learnings\n- Create a portfolio website\n- Share on LinkedIn\n\n**3. Continue Learning**:\n- Explore Kotlin Multiplatform in depth\n- Learn Compose Multiplatform (desktop, web)\n- Study microservices architecture\n- Master Kubernetes and cloud-native development\n\n**4. Join the Community**:\n- Contribute to open-source Kotlin projects\n- Join Kotlin Slack/Discord communities\n- Attend Kotlin conferences (KotlinConf)\n- Share your knowledge through teaching\n\n### You\u0027re Ready!\n\nYou now have the skills to:\n- Build production Android apps\n- Develop scalable backend APIs\n- Work at modern tech companies\n- Start your own projects\n- Mentor other developers\n\n**The journey doesn\u0027t end here - it\u0027s just beginning!**\n\nKeep coding, keep learning, and most importantly, keep building amazing things with Kotlin! 🚀\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Resources",
                                "content":  "\n### Official Documentation\n- [Kotlin Official Docs](https://kotlinlang.org/docs/home.html)\n- [Ktor Documentation](https://ktor.io/docs/)\n- [Jetpack Compose](https://developer.android.com/jetpack/compose)\n- [Exposed ORM](https://github.com/JetBrains/Exposed/wiki)\n\n### Community\n- [Kotlin Slack](https://surveys.jetbrains.com/s3/kotlin-slack-sign-up)\n- [r/Kotlin](https://www.reddit.com/r/Kotlin/)\n- [Kotlin Blog](https://blog.jetbrains.com/kotlin/)\n- [Android Developers](https://developer.android.com/)\n\n### Books\n- \"Kotlin in Action\" by Dmitry Jemerov\n- \"Head First Kotlin\" by Dawn Griffiths\n- \"Effective Kotlin\" by Marcin Moskala\n\n### Courses\n- [Kotlin for Java Developers (Coursera)](https://www.coursera.org/learn/kotlin-for-java-developers)\n- [Android Basics with Compose](https://developer.android.com/courses/android-basics-compose/course)\n\n---\n\n**Thank you for completing this course! We believe in you! 💪**\n\n---\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "advanced",
    "title":  "Lesson 7.8: Final Capstone - Full Stack E-Commerce Platform",
    "estimatedMinutes":  30
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
- Search for "kotlin Lesson 7.8: Final Capstone - Full Stack E-Commerce Platform 2024 2025" to find latest practices
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
  "lessonId": "7.8",
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

