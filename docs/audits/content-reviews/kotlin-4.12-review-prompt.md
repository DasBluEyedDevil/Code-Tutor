# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Advanced Kotlin
- **Lesson:** Lesson 4.12: DSLs and Type-Safe Builders (ID: 4.12)
- **Difficulty:** intermediate
- **Estimated Time:** 70 minutes

## Current Lesson Content

{
    "id":  "4.12",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 70 minutes\n**Difficulty**: Advanced\n**Prerequisites**: Parts 1-3, Functional Programming basics\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nDomain-Specific Languages (DSLs) are specialized mini-languages designed for specific problem domains. Kotlin\u0027s features—especially lambda with receiver—make it perfect for creating beautiful, type-safe DSLs that feel like natural language.\n\nYou\u0027ve already used DSLs if you\u0027ve worked with Gradle build scripts, Ktor routing, or HTML builders. These aren\u0027t magic—they\u0027re well-designed Kotlin code that you can create yourself!\n\nIn this lesson, you\u0027ll learn:\n- What DSLs are and when to use them\n- Lambda with receiver syntax\n- Type-safe builders pattern\n- Creating HTML DSL\n- Creating configuration DSL\n- `@DslMarker` annotation for scope control\n\nBy the end, you\u0027ll build expressive APIs that feel like custom languages!\n\n---\n\n"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept: What Are DSLs?",
                                "content":  "\n### Internal vs External DSLs\n\n**External DSL**: A separate language with its own parser (like SQL, regex)\n\n\n**Internal DSL**: Built within the host language (Kotlin)\n\n\n### Why DSLs in Kotlin?\n\nKotlin DSLs are readable, type-safe, and have IDE support:\n\n\n---\n\n",
                                "code":  "// Without DSL\nval table = Table()\ntable.setWidth(\"100%\")\nval row = Row()\nval cell = Cell()\ncell.setText(\"Hello\")\nrow.addCell(cell)\ntable.addRow(row)\n\n// With DSL\ntable {\n    width = \"100%\"\n    row {\n        cell { text = \"Hello\" }\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Lambda with Receiver",
                                "content":  "\nThe foundation of Kotlin DSLs is **lambda with receiver**.\n\n### Regular Lambda\n\n\n### Lambda with Receiver\n\n\n**Key Difference**: `StringBuilder.() -\u003e Unit` means `this` inside the lambda is `StringBuilder`.\n\n### Visualizing the Difference\n\n\n### Standard Library Examples\n\nKotlin\u0027s standard library uses lambdas with receiver:\n\n\n---\n\n",
                                "code":  "// apply\nval person = Person().apply {\n    name = \"Alice\"  // this.name\n    age = 25        // this.age\n}\n\n// with\nval result = with(person) {\n    println(name)   // this.name\n    println(age)    // this.age\n}\n\n// buildString (actually uses lambda with receiver)\nval text = buildString {\n    append(\"Line 1\")\n    appendLine()\n    append(\"Line 2\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Type-Safe Builders",
                                "content":  "\nType-safe builders use lambdas with receiver to create hierarchical structures.\n\n### Simple Example: List Builder\n\n\n### Nested Builders\n\n\n---\n\n",
                                "code":  "class Item(val name: String)\n\nclass ItemList {\n    private val items = mutableListOf\u003cItem\u003e()\n\n    fun item(name: String) {\n        items.add(Item(name))\n    }\n\n    fun getItems(): List\u003cItem\u003e = items\n}\n\nclass ShoppingList {\n    private val lists = mutableListOf\u003cItemList\u003e()\n\n    fun category(name: String, action: ItemList.() -\u003e Unit) {\n        println(\"Category: $name\")\n        val list = ItemList()\n        list.action()\n        lists.add(list)\n    }\n\n    fun getAllItems(): List\u003cItem\u003e = lists.flatMap { it.getItems() }\n}\n\nfun shoppingList(action: ShoppingList.() -\u003e Unit): ShoppingList {\n    val list = ShoppingList()\n    list.action()\n    return list\n}\n\nfun main() {\n    val list = shoppingList {\n        category(\"Fruits\") {\n            item(\"Apple\")\n            item(\"Banana\")\n            item(\"Orange\")\n        }\n\n        category(\"Vegetables\") {\n            item(\"Carrot\")\n            item(\"Broccoli\")\n        }\n    }\n\n    println(\"\\nAll items:\")\n    list.getAllItems().forEach { println(\"  - ${it.name}\") }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "HTML DSL Example",
                                "content":  "\nLet\u0027s build a complete HTML DSL!\n\n### Basic Structure\n\n\n### Using the HTML DSL\n\n\n### Enhanced HTML with Attributes\n\n\n---\n\n",
                                "code":  "class EnhancedDiv : Tag(\"div\") {\n    var id: String\n        get() = \"\"\n        set(value) { attribute(\"id\", value) }\n\n    var cssClass: String\n        get() = \"\"\n        set(value) { attribute(\"class\", value) }\n\n    fun p(action: EnhancedP.() -\u003e Unit) = initTag(EnhancedP(), action)\n}\n\nclass EnhancedP : Tag(\"p\") {\n    var style: String\n        get() = \"\"\n        set(value) { attribute(\"style\", value) }\n\n    fun text(content: String) = initTag(Text(content)) {}\n}\n\nfun enhancedHtml(action: EnhancedHTML.() -\u003e Unit): EnhancedHTML {\n    val html = EnhancedHTML()\n    html.action()\n    return html\n}\n\nclass EnhancedHTML : Tag(\"html\") {\n    fun body(action: EnhancedBody.() -\u003e Unit) = initTag(EnhancedBody(), action)\n}\n\nclass EnhancedBody : Tag(\"body\") {\n    fun div(action: EnhancedDiv.() -\u003e Unit) = initTag(EnhancedDiv(), action)\n}\n\nfun main() {\n    val page = enhancedHtml {\n        body {\n            div {\n                id = \"main\"\n                cssClass = \"container\"\n\n                p {\n                    style = \"color: blue;\"\n                    text(\"Styled paragraph\")\n                }\n            }\n        }\n    }\n\n    println(page)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Configuration DSL",
                                "content":  "\nCreate a type-safe configuration DSL:\n\n\n---\n\n",
                                "code":  "class Server {\n    var host: String = \"localhost\"\n    var port: Int = 8080\n    var ssl: Boolean = false\n}\n\nclass Database {\n    var url: String = \"\"\n    var username: String = \"\"\n    var password: String = \"\"\n    var maxConnections: Int = 10\n}\n\nclass AppConfig {\n    private var serverConfig: Server? = null\n    private var databaseConfig: Database? = null\n\n    fun server(action: Server.() -\u003e Unit) {\n        serverConfig = Server().apply(action)\n    }\n\n    fun database(action: Database.() -\u003e Unit) {\n        databaseConfig = Database().apply(action)\n    }\n\n    fun getServer(): Server = serverConfig ?: Server()\n    fun getDatabase(): Database = databaseConfig ?: Database()\n\n    override fun toString(): String {\n        return \"\"\"\n            Server: ${getServer().host}:${getServer().port} (SSL: ${getServer().ssl})\n            Database: ${getDatabase().url} (Max connections: ${getDatabase().maxConnections})\n        \"\"\".trimIndent()\n    }\n}\n\nfun config(action: AppConfig.() -\u003e Unit): AppConfig {\n    return AppConfig().apply(action)\n}\n\nfun main() {\n    val appConfig = config {\n        server {\n            host = \"0.0.0.0\"\n            port = 3000\n            ssl = true\n        }\n\n        database {\n            url = \"jdbc:postgresql://localhost:5432/mydb\"\n            username = \"admin\"\n            password = \"secret\"\n            maxConnections = 20\n        }\n    }\n\n    println(appConfig)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "@DslMarker - Scope Control",
                                "content":  "\n`@DslMarker` prevents implicit receiver mixing in nested DSLs.\n\n### The Problem Without @DslMarker\n\n\n### Solution with @DslMarker\n\n\n**Benefits**:\n- Prevents calling outer scope functions\n- Makes DSL structure clearer\n- Reduces errors\n\n---\n\n",
                                "code":  "@DslMarker\nannotation class HtmlTagMarker\n\n@HtmlTagMarker\nabstract class MarkedTag(val name: String) {\n    private val children = mutableListOf\u003cMarkedTag\u003e()\n\n    protected fun \u003cT : MarkedTag\u003e initTag(tag: T, action: T.() -\u003e Unit): T {\n        tag.action()\n        children.add(tag)\n        return tag\n    }\n\n    fun render(): String {\n        val childrenHtml = children.joinToString(\"\") { it.render() }\n        return if (children.isEmpty()) {\n            \"\u003c$name /\u003e\"\n        } else {\n            \"\u003c$name\u003e$childrenHtml\u003c/$name\u003e\"\n        }\n    }\n}\n\n@HtmlTagMarker\nclass MarkedHTML : MarkedTag(\"html\") {\n    fun body(action: MarkedBody.() -\u003e Unit) = initTag(MarkedBody(), action)\n}\n\n@HtmlTagMarker\nclass MarkedBody : MarkedTag(\"body\") {\n    fun div(action: MarkedDiv.() -\u003e Unit) = initTag(MarkedDiv(), action)\n}\n\n@HtmlTagMarker\nclass MarkedDiv : MarkedTag(\"div\") {\n    fun p(action: MarkedP.() -\u003e Unit) = initTag(MarkedP(), action)\n}\n\n@HtmlTagMarker\nclass MarkedP : MarkedTag(\"p\")\n\nfun main() {\n    val page = MarkedHTML().apply {\n        body {\n            div {\n                p { }\n                // body { }  // ❌ Error: can\u0027t call body from here\n            }\n        }\n    }\n\n    println(page.render())\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Advanced DSL Pattern: Builder with Validation",
                                "content":  "\n\n---\n\n",
                                "code":  "class ValidationException(message: String) : Exception(message)\n\n@DslMarker\nannotation class FormMarker\n\n@FormMarker\nclass Form {\n    private val fields = mutableListOf\u003cField\u003e()\n    var submitUrl: String = \"\"\n\n    fun textField(action: TextField.() -\u003e Unit) {\n        fields.add(TextField().apply(action))\n    }\n\n    fun emailField(action: EmailField.() -\u003e Unit) {\n        fields.add(EmailField().apply(action))\n    }\n\n    fun numberField(action: NumberField.() -\u003e Unit) {\n        fields.add(NumberField().apply(action))\n    }\n\n    fun validate() {\n        if (submitUrl.isBlank()) {\n            throw ValidationException(\"Submit URL is required\")\n        }\n\n        fields.forEach { it.validate() }\n    }\n\n    fun render(): String {\n        return \"\"\"\n            Form (submit to: $submitUrl)\n            Fields:\n            ${fields.joinToString(\"\\n\") { \"  - ${it.render()}\" }}\n        \"\"\".trimIndent()\n    }\n}\n\n@FormMarker\nabstract class Field {\n    var name: String = \"\"\n    var label: String = \"\"\n    var required: Boolean = false\n\n    abstract fun validate()\n    abstract fun render(): String\n\n    protected fun baseValidation() {\n        if (name.isBlank()) {\n            throw ValidationException(\"Field name is required\")\n        }\n    }\n}\n\n@FormMarker\nclass TextField : Field() {\n    var minLength: Int = 0\n    var maxLength: Int = Int.MAX_VALUE\n\n    override fun validate() {\n        baseValidation()\n        if (minLength \u003c 0) {\n            throw ValidationException(\"$name: minLength cannot be negative\")\n        }\n        if (maxLength \u003c minLength) {\n            throw ValidationException(\"$name: maxLength must be \u003e= minLength\")\n        }\n    }\n\n    override fun render() = \"TextField(\u0027$name\u0027, label=\u0027$label\u0027, required=$required, length=$minLength..$maxLength)\"\n}\n\n@FormMarker\nclass EmailField : Field() {\n    override fun validate() {\n        baseValidation()\n    }\n\n    override fun render() = \"EmailField(\u0027$name\u0027, label=\u0027$label\u0027, required=$required)\"\n}\n\n@FormMarker\nclass NumberField : Field() {\n    var min: Int = Int.MIN_VALUE\n    var max: Int = Int.MAX_VALUE\n\n    override fun validate() {\n        baseValidation()\n        if (max \u003c min) {\n            throw ValidationException(\"$name: max must be \u003e= min\")\n        }\n    }\n\n    override fun render() = \"NumberField(\u0027$name\u0027, label=\u0027$label\u0027, required=$required, range=$min..$max)\"\n}\n\nfun form(action: Form.() -\u003e Unit): Form {\n    val form = Form()\n    form.action()\n    form.validate()\n    return form\n}\n\nfun main() {\n    val contactForm = form {\n        submitUrl = \"/contact\"\n\n        textField {\n            name = \"fullName\"\n            label = \"Full Name\"\n            required = true\n            minLength = 3\n            maxLength = 100\n        }\n\n        emailField {\n            name = \"email\"\n            label = \"Email Address\"\n            required = true\n        }\n\n        numberField {\n            name = \"age\"\n            label = \"Age\"\n            min = 18\n            max = 120\n        }\n    }\n\n    println(contactForm.render())\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercises",
                                "content":  "\n### Exercise 1: JSON Builder (Medium)\n\nCreate a type-safe JSON builder DSL.\n\n**Requirements**:\n- Support objects and arrays\n- Support primitives (string, number, boolean, null)\n- Nested structures\n- Pretty-print output\n\n**Solution**:\n\n\n### Exercise 2: SQL Query Builder (Hard)\n\nCreate a type-safe SQL query builder.\n\n**Requirements**:\n- SELECT with columns\n- FROM with table\n- WHERE with conditions\n- ORDER BY\n- LIMIT\n\n**Solution**:\n\n\n### Exercise 3: Test DSL (Hard)\n\nCreate a test framework DSL similar to Kotest or Spek.\n\n**Requirements**:\n- describe/it blocks\n- Nested contexts\n- Assertions\n- Setup/teardown hooks\n\n**Solution**:\n\n\n---\n\n",
                                "code":  "@DslMarker\nannotation class TestMarker\n\n@TestMarker\nclass TestSuite(val name: String) {\n    private val specs = mutableListOf\u003cSpec\u003e()\n    private var beforeEach: (() -\u003e Unit)? = null\n    private var afterEach: (() -\u003e Unit)? = null\n\n    fun describe(description: String, action: Context.() -\u003e Unit) {\n        specs.add(Context(description).apply(action))\n    }\n\n    fun beforeEach(action: () -\u003e Unit) {\n        beforeEach = action\n    }\n\n    fun afterEach(action: () -\u003e Unit) {\n        afterEach = action\n    }\n\n    fun run() {\n        println(\"Test Suite: $name\\n\")\n        var passed = 0\n        var failed = 0\n\n        specs.forEach { spec -\u003e\n            val results = spec.run(beforeEach, afterEach)\n            passed += results.first\n            failed += results.second\n        }\n\n        println(\"\\n${passed} passed, $failed failed\")\n    }\n}\n\n@TestMarker\nsealed class Spec {\n    abstract fun run(beforeEach: (() -\u003e Unit)?, afterEach: (() -\u003e Unit)?): Pair\u003cInt, Int\u003e\n}\n\n@TestMarker\nclass Context(private val description: String) : Spec() {\n    private val tests = mutableListOf\u003cTest\u003e()\n    private val subContexts = mutableListOf\u003cContext\u003e()\n\n    fun it(description: String, action: () -\u003e Unit) {\n        tests.add(Test(description, action))\n    }\n\n    fun describe(description: String, action: Context.() -\u003e Unit) {\n        subContexts.add(Context(description).apply(action))\n    }\n\n    override fun run(beforeEach: (() -\u003e Unit)?, afterEach: (() -\u003e Unit)?): Pair\u003cInt, Int\u003e {\n        println(\"  $description\")\n        var passed = 0\n        var failed = 0\n\n        tests.forEach { test -\u003e\n            val result = test.run(beforeEach, afterEach)\n            if (result.first == 1) passed++ else failed++\n        }\n\n        subContexts.forEach { context -\u003e\n            val results = context.run(beforeEach, afterEach)\n            passed += results.first\n            failed += results.second\n        }\n\n        return Pair(passed, failed)\n    }\n}\n\n@TestMarker\nclass Test(private val description: String, private val action: () -\u003e Unit) : Spec() {\n    override fun run(beforeEach: (() -\u003e Unit)?, afterEach: (() -\u003e Unit)?): Pair\u003cInt, Int\u003e {\n        return try {\n            beforeEach?.invoke()\n            action()\n            afterEach?.invoke()\n\n            println(\"    ✅ $description\")\n            Pair(1, 0)\n        } catch (e: AssertionError) {\n            println(\"    ❌ $description: ${e.message}\")\n            Pair(0, 1)\n        }\n    }\n}\n\nfun testSuite(name: String, action: TestSuite.() -\u003e Unit): TestSuite {\n    return TestSuite(name).apply(action)\n}\n\nfun assertEquals(expected: Any?, actual: Any?) {\n    if (expected != actual) {\n        throw AssertionError(\"Expected $expected but got $actual\")\n    }\n}\n\nfun main() {\n    val suite = testSuite(\"Calculator Tests\") {\n        beforeEach {\n            println(\"      [Setup]\")\n        }\n\n        afterEach {\n            println(\"      [Teardown]\")\n        }\n\n        describe(\"Addition\") {\n            it(\"should add positive numbers\") {\n                assertEquals(5, 2 + 3)\n            }\n\n            it(\"should add negative numbers\") {\n                assertEquals(-5, -2 + -3)\n            }\n        }\n\n        describe(\"Multiplication\") {\n            it(\"should multiply numbers\") {\n                assertEquals(6, 2 * 3)\n            }\n\n            it(\"should fail example\") {\n                assertEquals(10, 2 * 3)  // This will fail\n            }\n\n            describe(\"Edge cases\") {\n                it(\"should handle zero\") {\n                    assertEquals(0, 0 * 100)\n                }\n            }\n        }\n    }\n\n    suite.run()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1: Lambda with Receiver\n\nWhat\u0027s the difference between `(T) -\u003e Unit` and `T.() -\u003e Unit`?\n\n**A)** They\u0027re identical\n**B)** First takes T as parameter, second has T as receiver (this)\n**C)** Second is faster\n**D)** First is type-safe, second isn\u0027t\n\n**Answer**: **B** - `(T) -\u003e Unit` takes T as a parameter, while `T.() -\u003e Unit` has T as the receiver, accessible as `this`.\n\n---\n\n### Question 2: DSL Marker\n\nWhat does `@DslMarker` do?\n\n**A)** Makes DSLs faster\n**B)** Prevents implicit receiver mixing in nested scopes\n**C)** Enables reflection on DSLs\n**D)** Makes DSLs type-safe\n\n**Answer**: **B** - `@DslMarker` prevents accidentally calling outer scope functions from inner scopes in nested DSLs.\n\n---\n\n### Question 3: Type-Safe Builders\n\nWhat makes a builder \"type-safe\"?\n\n**A)** It\u0027s written in Kotlin\n**B)** Compiler checks types at compile time\n**C)** It uses strings\n**D)** It throws exceptions\n\n**Answer**: **B** - Type-safe builders leverage Kotlin\u0027s type system so the compiler catches errors at compile time.\n\n---\n\n### Question 4: When to Use DSLs\n\nWhen should you create a DSL?\n\n**A)** For every class\n**B)** When you have complex, hierarchical configurations\n**C)** Only for HTML\n**D)** Never, they\u0027re too complex\n\n**Answer**: **B** - DSLs are best for complex, hierarchical configurations where a fluent API improves readability.\n\n---\n\n### Question 5: initTag Pattern\n\nIn HTML DSL, what does `initTag` typically do?\n\n**A)** Deletes a tag\n**B)** Creates, configures, and adds a child tag\n**C)** Validates HTML\n**D)** Converts to string\n\n**Answer**: **B** - `initTag` creates a tag, runs its configuration lambda, adds it to children, and returns it.\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Summary",
                                "content":  "\nCongratulations! You\u0027ve mastered DSLs and type-safe builders in Kotlin. Here\u0027s what you learned:\n\n✅ **DSLs** - Creating domain-specific languages in Kotlin\n✅ **Lambda with Receiver** - Foundation of DSL syntax\n✅ **Type-Safe Builders** - Hierarchical structure creation\n✅ **HTML DSL** - Practical builder pattern example\n✅ **Configuration DSL** - Type-safe configuration\n✅ **@DslMarker** - Scope control in nested DSLs\n\n### Key Takeaways\n\n1. **Lambda with receiver** makes `this` implicit\n2. **Type-safe builders** catch errors at compile time\n3. **@DslMarker** prevents scope confusion\n4. **DSLs improve readability** for complex configurations\n5. **Use judiciously** - don\u0027t over-engineer simple cases\n\n### Next Steps\n\nIn the next lesson, we\u0027ll bring everything together in the **Part 4 Capstone Project** - building a complete task scheduler that uses generics, coroutines, delegation, reflection, and DSLs!\n\n---\n\n**Practice Challenge**: Create a routing DSL for a web framework with GET/POST/PUT/DELETE methods, path parameters, middleware, and type-safe request handlers.\n\n"
                            }
                        ],
    "challenges":  [

                   ],
    "difficulty":  "intermediate",
    "title":  "Lesson 4.12: DSLs and Type-Safe Builders",
    "estimatedMinutes":  70
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
- Search for "kotlin Lesson 4.12: DSLs and Type-Safe Builders 2024 2025" to find latest practices
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
  "lessonId": "4.12",
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

