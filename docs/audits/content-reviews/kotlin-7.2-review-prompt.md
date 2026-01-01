# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Professional Development & Deployment
- **Lesson:** Lesson 7.2: Testing Strategies (ID: 7.2)
- **Difficulty:** advanced
- **Estimated Time:** 80 minutes

## Current Lesson Content

{
    "id":  "7.2",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 80 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "\nTesting is not optional in professional software development - it\u0027s a critical skill that separates hobbyist code from production-ready applications.\n\nIn this lesson, you\u0027ll master advanced testing strategies for Kotlin applications:\n- ✅ Unit testing with JUnit 5 and Kotest\n- ✅ Mocking dependencies with MockK\n- ✅ Testing coroutines and flows\n- ✅ Testing Jetpack Compose UI\n- ✅ Test-driven development (TDD)\n- ✅ Measuring code coverage\n\nBy the end, you\u0027ll write tests that give you confidence to refactor, deploy, and sleep peacefully at night.\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why Testing Matters",
                                "content":  "\n### The Cost of Bugs\n\n**Production Bug Cost**:\n\n**Real Example**: A banking app bug that allowed duplicate withdrawals:\n- Development: Could be caught with 1 unit test ($100)\n- Production: Cost $2.3M in fraudulent transactions + reputation damage\n\n**Statistics**:\n- Well-tested codebases have 40-80% fewer production bugs\n- Companies with good test coverage deploy 46x more frequently\n- Automated tests reduce debugging time by 60%\n\n---\n\n",
                                "code":  "Bug found in:\n└─ Development (writing code): $100\n└─ Testing (QA phase): $1,000\n└─ Staging (before release): $10,000\n└─ Production (after release): $100,000+",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Testing Pyramid",
                                "content":  "\n### The Right Balance\n\n\n**Unit Tests (70%)**:\n- Test individual functions/classes in isolation\n- Fast (milliseconds)\n- Easy to write and maintain\n- Run on every code change\n\n**Integration Tests (20%)**:\n- Test multiple components together\n- Medium speed (seconds)\n- Test real interactions\n\n**E2E Tests (10%)**:\n- Test entire user flows\n- Slow (minutes)\n- Fragile (UI changes break tests)\n- Only for critical paths\n\n---\n\n",
                                "code":  "       /\\\n      /  \\     E2E Tests (UI)\n     /    \\    10% - Slow, expensive, brittle\n    /------\\\n   /        \\  Integration Tests\n  /          \\ 20% - Medium speed, test components together\n /------------\\\n/              \\ Unit Tests\n----------------  70% - Fast, cheap, test individual functions",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "JUnit 5 Fundamentals",
                                "content":  "\n### Basic Test Structure\n\n\n**Simple Test**:\n\n### Test Lifecycle\n\n\n### Parameterized Tests\n\n\n---\n\n",
                                "code":  "import org.junit.jupiter.params.ParameterizedTest\nimport org.junit.jupiter.params.provider.*\n\nclass ValidationTest {\n\n    @ParameterizedTest\n    @ValueSource(strings = [\"test@example.com\", \"user@domain.co\", \"name+tag@email.com\"])\n    fun `valid emails should pass validation`(email: String) {\n        assertTrue(Validator.isValidEmail(email))\n    }\n\n    @ParameterizedTest\n    @CsvSource(\n        \"0, INFANT\",\n        \"5, CHILD\",\n        \"13, TEEN\",\n        \"20, ADULT\",\n        \"70, SENIOR\"\n    )\n    fun `age categories should be correct`(age: Int, expectedCategory: String) {\n        assertEquals(expectedCategory, getAgeCategory(age))\n    }\n\n    @ParameterizedTest\n    @MethodSource(\"passwordProvider\")\n    fun `weak passwords should fail validation`(password: String) {\n        assertFalse(Validator.isStrongPassword(password))\n    }\n\n    companion object {\n        @JvmStatic\n        fun passwordProvider() = listOf(\n            \"123\",\n            \"password\",\n            \"abc123\",\n            \"NoNumber\"\n        )\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Kotest - Beautiful Testing DSL",
                                "content":  "\n### Why Kotest?\n\nKotest provides a more readable, Kotlin-idiomatic testing syntax.\n\n\n**Comparison**:\n\n**JUnit**:\n\n**Kotest**:\n\n### Kotest Matchers\n\n\n---\n\n",
                                "code":  "import io.kotest.matchers.*\nimport io.kotest.matchers.collections.*\nimport io.kotest.matchers.string.*\n\nclass KotestMatchersTest : StringSpec({\n\n    \"string matchers\" {\n        val name = \"Kotlin\"\n\n        name shouldStartWith \"Kot\"\n        name shouldEndWith \"lin\"\n        name shouldContain \"otl\"\n        name shouldHaveLength 6\n        name shouldMatch \"K[a-z]+\".toRegex()\n    }\n\n    \"collection matchers\" {\n        val list = listOf(1, 2, 3, 4, 5)\n\n        list shouldHaveSize 5\n        list shouldContain 3\n        list shouldContainAll listOf(1, 3, 5)\n        list.shouldBeSorted()\n\n        val emptyList = emptyList\u003cInt\u003e()\n        emptyList.shouldBeEmpty()\n    }\n\n    \"numeric matchers\" {\n        val price = 99.99\n\n        price shouldBeGreaterThan 50.0\n        price shouldBeLessThan 100.0\n        price.shouldBeBetween(90.0, 100.0)\n    }\n\n    \"exception matchers\" {\n        shouldThrow\u003cIllegalArgumentException\u003e {\n            require(false) { \"Error message\" }\n        }.message shouldBe \"Error message\"\n    }\n})",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "MockK - Powerful Mocking",
                                "content":  "\n### Why Mock?\n\n**Problem**: Testing a service that depends on a database:\n\n\nTo test `UserService`, we don\u0027t want to:\n- Set up a real database\n- Insert test data\n- Clean up after tests\n- Deal with slow I/O operations\n\n**Solution**: Mock the database!\n\n\n### Basic Mocking\n\n\n### Advanced Mocking\n\n**Relaxed Mocks** (return default values):\n\n**Spy** (real object with partial mocking):\n\n**Capture Arguments**:\n\n---\n\n",
                                "code":  "@Test\nfun `verify method was called with specific arguments`() {\n    val mockRepo = mockk\u003cUserRepository\u003e(relaxed = true)\n    val service = UserService(mockRepo)\n\n    val slot = slot\u003cUser\u003e()\n\n    service.updateUser(User(1, \"John\", \"john@example.com\"))\n\n    verify { mockRepo.update(capture(slot)) }\n\n    assertEquals(\"John\", slot.captured.name)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Testing Coroutines",
                                "content":  "\n### runTest - The Testing Coroutine\n\n\n**Basic Coroutine Test**:\n\n### Testing Flows\n\n\n### Testing ViewModels with Coroutines\n\n\n---\n\n",
                                "code":  "import kotlinx.coroutines.*\nimport kotlinx.coroutines.test.*\nimport org.junit.jupiter.api.*\n\nclass UserViewModelTest {\n\n    private val testDispatcher = StandardTestDispatcher()\n\n    @BeforeEach\n    fun setup() {\n        Dispatchers.setMain(testDispatcher)\n    }\n\n    @AfterEach\n    fun cleanup() {\n        Dispatchers.resetMain()\n    }\n\n    @Test\n    fun `loading users should update state`() = runTest {\n        val mockRepo = mockk\u003cUserRepository\u003e()\n        every { mockRepo.getUsers() } returns flowOf(\n            listOf(User(1, \"John\"), User(2, \"Jane\"))\n        )\n\n        val viewModel = UserViewModel(mockRepo)\n\n        // Trigger action\n        viewModel.loadUsers()\n\n        // Advance until idle\n        advanceUntilIdle()\n\n        // Verify state\n        assertEquals(2, viewModel.users.value.size)\n        assertEquals(false, viewModel.isLoading.value)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Testing Jetpack Compose UI",
                                "content":  "\n### Compose Testing Library\n\n\n**Basic Compose Test**:\n\n### Testing Interactions\n\n\n---\n\n",
                                "code":  "@Test\nfun todoList_addItem_showsInList() {\n    composeTestRule.setContent {\n        TodoApp()\n    }\n\n    // Enter new todo\n    composeTestRule.onNodeWithTag(\"todoInput\")\n        .performTextInput(\"Buy groceries\")\n\n    // Click add button\n    composeTestRule.onNodeWithTag(\"addButton\")\n        .performClick()\n\n    // Verify item appears\n    composeTestRule.onNodeWithText(\"Buy groceries\")\n        .assertIsDisplayed()\n\n    // Verify input is cleared\n    composeTestRule.onNodeWithTag(\"todoInput\")\n        .assertTextEquals(\"\")\n}\n\n@Test\nfun todoItem_clickCheckbox_marksAsComplete() {\n    composeTestRule.setContent {\n        TodoItem(\n            todo = Todo(id = 1, text = \"Test\", completed = false),\n            onToggle = { }\n        )\n    }\n\n    // Initially unchecked\n    composeTestRule.onNodeWithTag(\"checkbox-1\")\n        .assertIsOff()\n\n    // Click checkbox\n    composeTestRule.onNodeWithTag(\"checkbox-1\")\n        .performClick()\n\n    // Verify it\u0027s checked\n    composeTestRule.onNodeWithTag(\"checkbox-1\")\n        .assertIsOn()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Test-Driven Development (TDD)",
                                "content":  "\n### The TDD Cycle\n\n\n### Example: Building a Password Validator\n\n**Step 1: Write the test (Red)**:\n\nTest fails (class doesn\u0027t exist yet) ❌\n\n**Step 2: Minimal implementation (Green)**:\n\nTest passes ✅\n\n**Step 3: Add more tests**:\n\n**Step 4: Implement to pass all tests**:\n\n**Benefits of TDD**:\n- Forces you to think about design before implementation\n- Ensures code is testable\n- Provides immediate feedback\n- Creates a safety net for refactoring\n\n---\n\n",
                                "code":  "class PasswordValidator {\n    fun isValid(password: String): Boolean {\n        if (password.length \u003c 8) return false\n        if (!password.any { it.isUpperCase() }) return false\n        if (!password.any { it.isDigit() }) return false\n        return true\n    }\n\n    fun getErrors(password: String): List\u003cString\u003e {\n        val errors = mutableListOf\u003cString\u003e()\n\n        if (password.length \u003c 8) {\n            errors.add(\"Password must be at least 8 characters\")\n        }\n        if (!password.any { it.isUpperCase() }) {\n            errors.add(\"Password must contain an uppercase letter\")\n        }\n        if (!password.any { it.isDigit() }) {\n            errors.add(\"Password must contain a number\")\n        }\n\n        return errors\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Coverage",
                                "content":  "\n### Measuring Coverage with JaCoCo\n\n\n**Run Coverage**:\n\n**View Report**:\nOpen `build/reports/jacoco/test/html/index.html`\n\n### Coverage Metrics\n\n**What\u0027s Good Coverage?**:\n- **80%+**: Excellent\n- **60-80%**: Good\n- **40-60%**: Needs improvement\n- **\u003c40%**: Risky\n\n**Important**: 100% coverage ≠ bug-free code. Focus on testing critical paths.\n\n---\n\n",
                                "code":  "./gradlew test jacocoTestReport",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 1: Build a Tested Repository",
                                "content":  "\nCreate a `ProductRepository` with full test coverage.\n\n### Requirements\n\n1. **ProductRepository** with methods:\n   - `getProducts(): List\u003cProduct\u003e`\n   - `getProduct(id: String): Product?`\n   - `createProduct(product: Product): Result\u003cProduct\u003e`\n   - `updateProduct(id: String, product: Product): Result\u003cProduct\u003e`\n   - `deleteProduct(id: String): Result\u003cUnit\u003e`\n\n2. **Tests** (use MockK):\n   - Test successful operations\n   - Test error cases (not found, network errors)\n   - Test caching behavior\n   - Verify mock interactions\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 1",
                                "content":  "\n\n**Tests**:\n\n---\n\n",
                                "code":  "// src/test/kotlin/com/example/repository/ProductRepositoryTest.kt\npackage com.example.repository\n\nimport io.mockk.*\nimport kotlinx.coroutines.test.runTest\nimport org.junit.jupiter.api.BeforeEach\nimport org.junit.jupiter.api.Test\nimport kotlin.test.*\n\nclass ProductRepositoryTest {\n\n    private lateinit var mockApi: ProductApi\n    private lateinit var repository: ProductRepository\n\n    @BeforeEach\n    fun setup() {\n        mockApi = mockk()\n        repository = ProductRepository(mockApi)\n    }\n\n    @Test\n    fun `getProducts should fetch from API and cache results`() = runTest {\n        val products = listOf(\n            Product(\"1\", \"Laptop\", 999.99, 10),\n            Product(\"2\", \"Mouse\", 29.99, 50)\n        )\n\n        coEvery { mockApi.getProducts() } returns products\n\n        val result = repository.getProducts()\n\n        assertEquals(2, result.size)\n        assertEquals(\"Laptop\", result[0].name)\n\n        coVerify(exactly = 1) { mockApi.getProducts() }\n    }\n\n    @Test\n    fun `getProducts should return cached data when API fails`() = runTest {\n        val products = listOf(Product(\"1\", \"Laptop\", 999.99, 10))\n\n        // First call succeeds\n        coEvery { mockApi.getProducts() } returns products\n        repository.getProducts()\n\n        // Second call fails\n        coEvery { mockApi.getProducts() } throws Exception(\"Network error\")\n        val result = repository.getProducts()\n\n        // Should return cached data\n        assertEquals(1, result.size)\n        assertEquals(\"Laptop\", result[0].name)\n    }\n\n    @Test\n    fun `getProduct should return cached product if available`() = runTest {\n        val product = Product(\"1\", \"Laptop\", 999.99, 10)\n\n        coEvery { mockApi.getProducts() } returns listOf(product)\n        repository.getProducts() // Populate cache\n\n        val result = repository.getProduct(\"1\")\n\n        assertNotNull(result)\n        assertEquals(\"Laptop\", result.name)\n\n        // API not called (used cache)\n        coVerify(exactly = 0) { mockApi.getProduct(any()) }\n    }\n\n    @Test\n    fun `getProduct should fetch from API if not cached`() = runTest {\n        val product = Product(\"1\", \"Laptop\", 999.99, 10)\n\n        coEvery { mockApi.getProduct(\"1\") } returns product\n\n        val result = repository.getProduct(\"1\")\n\n        assertNotNull(result)\n        assertEquals(\"Laptop\", result.name)\n\n        coVerify(exactly = 1) { mockApi.getProduct(\"1\") }\n    }\n\n    @Test\n    fun `getProduct should return null when product not found`() = runTest {\n        coEvery { mockApi.getProduct(\"999\") } throws Exception(\"Not found\")\n\n        val result = repository.getProduct(\"999\")\n\n        assertNull(result)\n    }\n\n    @Test\n    fun `createProduct should call API and cache result`() = runTest {\n        val newProduct = Product(\"3\", \"Keyboard\", 79.99, 30)\n\n        coEvery { mockApi.createProduct(newProduct) } returns newProduct\n\n        val result = repository.createProduct(newProduct)\n\n        assertTrue(result.isSuccess)\n        assertEquals(\"Keyboard\", result.getOrNull()?.name)\n\n        // Verify cached\n        val cached = repository.getProduct(\"3\")\n        assertNotNull(cached)\n        assertEquals(\"Keyboard\", cached.name)\n    }\n\n    @Test\n    fun `createProduct should return failure when API fails`() = runTest {\n        val newProduct = Product(\"3\", \"Keyboard\", 79.99, 30)\n\n        coEvery { mockApi.createProduct(newProduct) } throws Exception(\"Server error\")\n\n        val result = repository.createProduct(newProduct)\n\n        assertTrue(result.isFailure)\n    }\n\n    @Test\n    fun `updateProduct should update cache on success`() = runTest {\n        val updated = Product(\"1\", \"Gaming Laptop\", 1299.99, 5)\n\n        coEvery { mockApi.updateProduct(\"1\", updated) } returns updated\n\n        val result = repository.updateProduct(\"1\", updated)\n\n        assertTrue(result.isSuccess)\n        assertEquals(\"Gaming Laptop\", result.getOrNull()?.name)\n    }\n\n    @Test\n    fun `deleteProduct should remove from cache`() = runTest {\n        val product = Product(\"1\", \"Laptop\", 999.99, 10)\n\n        // Add to cache\n        coEvery { mockApi.getProduct(\"1\") } returns product\n        repository.getProduct(\"1\")\n\n        // Delete\n        coEvery { mockApi.deleteProduct(\"1\") } just Runs\n\n        val result = repository.deleteProduct(\"1\")\n\n        assertTrue(result.isSuccess)\n\n        // Verify removed from cache\n        coEvery { mockApi.getProduct(\"1\") } throws Exception(\"Not found\")\n        val cached = repository.getProduct(\"1\")\n        assertNull(cached)\n    }\n\n    @Test\n    fun `clearCache should remove all cached products`() = runTest {\n        val products = listOf(Product(\"1\", \"Laptop\", 999.99, 10))\n\n        coEvery { mockApi.getProducts() } returns products\n        repository.getProducts()\n\n        repository.clearCache()\n\n        coEvery { mockApi.getProduct(\"1\") } throws Exception(\"Not found\")\n        val cached = repository.getProduct(\"1\")\n        assertNull(cached)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 2: Test a Compose Screen",
                                "content":  "\nCreate tests for a shopping cart screen.\n\n### Requirements\n\n1. **CartScreen Composable**:\n   - Displays list of cart items\n   - Shows total price\n   - Has \"Checkout\" button\n   - Can remove items\n\n2. **Tests**:\n   - Verify items are displayed\n   - Verify total is calculated correctly\n   - Test remove item functionality\n   - Test checkout button click\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 2",
                                "content":  "\n\n**Tests**:\n\n---\n\n",
                                "code":  "// src/androidTest/kotlin/com/example/ui/CartScreenTest.kt\nclass CartScreenTest {\n\n    @get:Rule\n    val composeTestRule = createComposeRule()\n\n    @Test\n    fun cartScreen_emptyCart_showsEmptyMessage() {\n        composeTestRule.setContent {\n            CartScreen(items = emptyList())\n        }\n\n        composeTestRule.onNodeWithTag(\"emptyMessage\")\n            .assertIsDisplayed()\n            .assertTextEquals(\"Your cart is empty\")\n    }\n\n    @Test\n    fun cartScreen_withItems_displaysAllItems() {\n        val items = listOf(\n            CartItem(\"1\", \"Laptop\", 999.99, 1),\n            CartItem(\"2\", \"Mouse\", 29.99, 2)\n        )\n\n        composeTestRule.setContent {\n            CartScreen(items = items)\n        }\n\n        composeTestRule.onNodeWithTag(\"cartItem-1\").assertIsDisplayed()\n        composeTestRule.onNodeWithTag(\"cartItem-2\").assertIsDisplayed()\n        composeTestRule.onNodeWithText(\"Laptop\").assertExists()\n        composeTestRule.onNodeWithText(\"Mouse\").assertExists()\n    }\n\n    @Test\n    fun cartScreen_calculatesCorrectTotal() {\n        val items = listOf(\n            CartItem(\"1\", \"Laptop\", 999.99, 1),\n            CartItem(\"2\", \"Mouse\", 29.99, 2)\n        )\n\n        composeTestRule.setContent {\n            CartScreen(items = items)\n        }\n\n        // Total: 999.99 + (29.99 * 2) = 1059.97\n        composeTestRule.onNodeWithTag(\"totalPrice\")\n            .assertTextContains(\"$1059.97\")\n    }\n\n    @Test\n    fun cartScreen_clickRemove_callsOnRemoveItem() {\n        val items = listOf(CartItem(\"1\", \"Laptop\", 999.99, 1))\n        var removedId: String? = null\n\n        composeTestRule.setContent {\n            CartScreen(\n                items = items,\n                onRemoveItem = { id -\u003e removedId = id }\n            )\n        }\n\n        composeTestRule.onNodeWithTag(\"removeButton-1\").performClick()\n\n        assertEquals(\"1\", removedId)\n    }\n\n    @Test\n    fun cartScreen_clickCheckout_callsOnCheckout() {\n        val items = listOf(CartItem(\"1\", \"Laptop\", 999.99, 1))\n        var checkoutCalled = false\n\n        composeTestRule.setContent {\n            CartScreen(\n                items = items,\n                onCheckout = { checkoutCalled = true }\n            )\n        }\n\n        composeTestRule.onNodeWithTag(\"checkoutButton\").performClick()\n\n        assertTrue(checkoutCalled)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 3: TDD - Build a Shopping Cart",
                                "content":  "\nUse TDD to build a shopping cart with these features:\n- Add items\n- Remove items\n- Calculate total\n- Apply discount codes\n\nWrite tests first, then implement!\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution 3",
                                "content":  "\n**Tests First**:\n\n**Implementation**:\n\n---\n\n",
                                "code":  "data class Item(val name: String, val price: Double, val quantity: Int = 1)\n\nclass ShoppingCart {\n    private val items = mutableMapOf\u003cString, Item\u003e()\n    private var discountPercent = 0.0\n\n    fun addItem(name: String, price: Double) {\n        val existing = items[name]\n        if (existing != null) {\n            items[name] = existing.copy(quantity = existing.quantity + 1)\n        } else {\n            items[name] = Item(name, price, 1)\n        }\n    }\n\n    fun removeItem(name: String) {\n        items.remove(name)\n    }\n\n    fun getItemCount(name: String): Int {\n        return items[name]?.quantity ?: 0\n    }\n\n    fun getTotal(): Double {\n        val subtotal = items.values.sumOf { it.price * it.quantity }\n        return subtotal * (1 - discountPercent / 100)\n    }\n\n    fun applyDiscount(code: String): Result\u003cUnit\u003e {\n        if (items.isEmpty()) {\n            return Result.failure(Exception(\"Cannot apply discount to empty cart\"))\n        }\n\n        val discount = when (code) {\n            \"SAVE10\" -\u003e 10.0\n            \"SAVE20\" -\u003e 20.0\n            \"SAVE50\" -\u003e 50.0\n            else -\u003e return Result.failure(Exception(\"Invalid discount code\"))\n        }\n\n        discountPercent = discount\n        return Result.success(Unit)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why This Matters",
                                "content":  "\n### Career Impact\n\n**Job Requirements**:\n- 95% of backend/Android jobs require testing skills\n- \"Write unit tests\" appears in 87% of Kotlin job postings\n- Companies with good tests ship 46x more frequently\n\n**Salary Impact**:\n- Developers who write tests earn 15-20% more\n- Testing expertise = senior-level skill\n\n**Real Examples**:\n- **Airbnb**: Requires 80% code coverage\n- **Google**: All code changes need tests\n- **Spotify**: TDD is standard practice\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1\nWhat\u0027s the recommended ratio in the testing pyramid?\n\nA) 70% unit, 20% integration, 10% E2E\nB) Equal distribution (33% each)\nC) 100% integration tests\nD) 10% unit, 90% E2E\n\n### Question 2\nWhat does MockK\u0027s `every` block do?\n\nA) Runs a test multiple times\nB) Defines the behavior of a mock\nC) Verifies a method was called\nD) Creates a real object\n\n### Question 3\nHow do you test a suspending function?\n\nA) Use `@Test suspend fun`\nB) Use `runBlocking`\nC) Use `runTest`\nD) Can\u0027t test suspending functions\n\n### Question 4\nWhat does `composeTestRule.onNodeWithTag(\"button\")` do?\n\nA) Creates a button\nB) Finds a composable with testTag(\"button\")\nC) Tags the current test\nD) Deletes a button\n\n### Question 5\nWhat\u0027s the first step in TDD?\n\nA) Write implementation\nB) Write a failing test\nC) Refactor code\nD) Deploy to production\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quiz Answers",
                                "content":  "\n**Question 1: A) 70% unit, 20% integration, 10% E2E**\n\nThe testing pyramid recommends:\n- **Most**: Unit tests (fast, isolated)\n- **Some**: Integration tests\n- **Few**: E2E tests (slow, brittle)\n\n---\n\n**Question 2: B) Defines the behavior of a mock**\n\n\nTells the mock: \"When getUser(1) is called, return this user\"\n\n---\n\n**Question 3: C) Use `runTest`**\n\n\n`runTest` provides a coroutine scope for testing.\n\n---\n\n**Question 4: B) Finds a composable with testTag(\"button\")**\n\n\nTest tags help locate composables in tests.\n\n---\n\n**Question 5: B) Write a failing test**\n\nTDD cycle:\n1. **Red**: Write failing test\n2. **Green**: Write minimal code to pass\n3. **Refactor**: Improve code\n\n---\n\n",
                                "code":  "Button(modifier = Modifier.testTag(\"button\")) { }\n\ncomposeTestRule.onNodeWithTag(\"button\").performClick()",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What You\u0027ve Learned",
                                "content":  "\n✅ Why testing is critical for professional development\n✅ The testing pyramid and when to use each test type\n✅ JUnit 5 fundamentals and parameterized tests\n✅ Kotest for beautiful, Kotlin-idiomatic tests\n✅ MockK for powerful mocking and verification\n✅ Testing coroutines and flows with kotlinx-coroutines-test\n✅ Testing Jetpack Compose UI components\n✅ Test-driven development (TDD) workflow\n✅ Measuring code coverage with JaCoCo\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps",
                                "content":  "\nIn **Lesson 7.3: Performance Optimization**, you\u0027ll learn:\n- Profiling tools to identify bottlenecks\n- Memory management and leak detection\n- Optimizing coroutines and flows\n- Compose recomposition optimization\n- Database query optimization\n- Network performance best practices\n\nGreat tests give you confidence to optimize fearlessly!\n\n---\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "advanced",
    "title":  "Lesson 7.2: Testing Strategies",
    "estimatedMinutes":  80
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
- Search for "kotlin Lesson 7.2: Testing Strategies 2024 2025" to find latest practices
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
  "lessonId": "7.2",
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

