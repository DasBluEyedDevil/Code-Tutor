const fs = require('fs');
const path = require('path');

const coursePath = path.join(__dirname, '..', 'content', 'courses', 'kotlin', 'course.json');
const content = fs.readFileSync(coursePath, 'utf8');
const data = JSON.parse(content);

// Define warnings for each lesson topic
const warnings = {
  '4.1': {
    title: 'Common Mistakes with Functional Programming',
    content: '### Mistake 1: Mutating State in Functional Code\n\nFunctional programming avoids mutation:\n\n### Mistake 2: Side Effects in Pure Functions\n\nPure functions should only depend on inputs:\n\n### Mistake 3: Overusing Functional Style\n\nNot everything needs to be functional - balance is key.',
    code: '// ❌ Wrong - mutating external state\nvar total = 0\nlistOf(1, 2, 3).forEach { total += it }\n\n// ✅ Correct - use fold/reduce\nval total = listOf(1, 2, 3).fold(0) { acc, n -> acc + n }\n\n// ❌ Wrong - side effect in map\nlist.map {\n    println(it)  // Side effect!\n    it * 2\n}\n\n// ✅ Correct - separate concerns\nlist.map { it * 2 }.forEach { println(it) }'
  },
  '4.2': {
    title: 'Common Mistakes with Lambdas',
    content: '### Mistake 1: Returning from Lambda vs Function\n\nBare return exits the enclosing function, not the lambda:\n\n### Mistake 2: Unused Lambda Parameters\n\nUse underscore for unused parameters:\n\n### Mistake 3: Capturing Mutable Variables\n\nLambdas capture variable references, not values.',
    code: '// ❌ Wrong - returns from enclosing function\nfun process(list: List<Int>) {\n    list.forEach {\n        if (it < 0) return  // Exits process()!\n        println(it)\n    }\n}\n\n// ✅ Correct - use labeled return\nlist.forEach {\n    if (it < 0) return@forEach  // Continues to next\n    println(it)\n}\n\n// Use _ for unused parameters\nmap.forEach { _, value -> println(value) }\n\n// Capture warning\nvar x = 10\nval lambda = { println(x) }\nx = 20\nlambda()  // Prints 20, not 10!'
  },
  '4.3': {
    title: 'Common Mistakes with Collections',
    content: '### Mistake 1: Chaining Too Many Operations\n\nLong chains can hurt performance - use sequences:\n\n### Mistake 2: Null Handling in Collections\n\nRemember collections can contain null:\n\n### Mistake 3: Modifying During Iteration\n\nDo not modify a collection while iterating.',
    code: '// ❌ Wrong - creates intermediate lists\nlist.filter { it > 0 }.map { it * 2 }.take(5)\n\n// ✅ Correct - use sequences for large lists\nlist.asSequence().filter { it > 0 }.map { it * 2 }.take(5).toList()\n\n// Handle nulls in collections\nval list: List<String?> = listOf("a", null, "b")\nlist.filterNotNull()  // ["a", "b"]\nlist.mapNotNull { it?.uppercase() }  // ["A", "B"]\n\n// ❌ Wrong - ConcurrentModificationException\nval list = mutableListOf(1, 2, 3)\nfor (item in list) {\n    if (item == 2) list.remove(item)  // Crash!\n}\n\n// ✅ Correct - use removeIf or filter\nlist.removeIf { it == 2 }'
  },
  '4.4': {
    title: 'Common Mistakes with Scope Functions',
    content: '### Mistake 1: Choosing the Wrong Scope Function\n\nEach has a specific purpose - learn the differences:\n\n### Mistake 2: Nesting Scope Functions\n\nDeeply nested scopes hurt readability:\n\n### Mistake 3: Using also for Transformations\n\nalso is for side effects, not transformations.',
    code: '// Scope function guide:\n// let   - transform and return result, it reference\n// run   - execute block with result, this reference\n// apply - configure object, returns same object\n// also  - side effects, returns same object\n// with  - work with object, non-null receiver\n\n// ❌ Wrong - too nested\nuser?.let { u ->\n    u.address?.let { a ->\n        a.city?.let { c -> /* ... */ }\n    }\n}\n\n// ✅ Correct - flatten\nval city = user?.address?.city\nif (city != null) { /* ... */ }\n\n// ❌ Wrong - also for transformation\nval result = data.also { it.uppercase() }  // Returns original!\n\n// ✅ Correct - let for transformation\nval result = data.let { it.uppercase() }'
  },
  '4.5': {
    title: 'Common Mistakes with Function Composition',
    content: '### Mistake 1: Complex Inline Compositions\n\nExtract complex logic into named functions:\n\n### Mistake 2: Type Mismatches in Chains\n\nEnsure function return types match next input:\n\n### Mistake 3: Overusing Currying\n\nCurrying is powerful but can reduce readability.',
    code: '// ❌ Wrong - hard to read\nval result = input\n    .let { transform1(it).let { transform2(it).let { transform3(it) } } }\n\n// ✅ Correct - name the pipeline\nfun process(input: String) = input\n    .let(::transform1)\n    .let(::transform2)\n    .let(::transform3)\n\n// Type matching in composition\nfun toInt(s: String): Int = s.toInt()\nfun double(n: Int): Int = n * 2\nfun toString(n: Int): String = n.toString()\n\n// Chain works: String -> Int -> Int -> String\nval pipeline = { s: String -> toString(double(toInt(s))) }\n\n// Currying example\nfun add(a: Int) = { b: Int -> a + b }\nval add5 = add(5)  // Returns (Int) -> Int\nadd5(3)  // 8'
  },
  '4.6': {
    title: 'Common Mistakes in Data Processing Pipelines',
    content: '### Mistake 1: Not Using Sequences for Large Data\n\nSequences process lazily and avoid intermediate collections:\n\n### Mistake 2: Ignoring Error Handling\n\nPipelines should handle failures gracefully:\n\n### Mistake 3: Complex Transform Logic\n\nKeep each step simple and focused.',
    code: '// ❌ Wrong - eager, creates many lists\nlargeList\n    .filter { validate(it) }\n    .map { transform(it) }\n    .take(10)\n\n// ✅ Correct - lazy processing\nlargeList.asSequence()\n    .filter { validate(it) }\n    .map { transform(it) }\n    .take(10)\n    .toList()\n\n// Error handling with Result\nfun processItem(item: String): Result<Data> =\n    runCatching { parse(item) }\n\nval results = items.map { processItem(it) }\nval successes = results.mapNotNull { it.getOrNull() }\nval failures = results.filter { it.isFailure }\n\n// Keep transforms simple\nfun parse(s: String) = s.trim()\nfun validate(s: String) = s.isNotBlank()\nfun transform(s: String) = s.uppercase()'
  },
  '4.7': {
    title: 'Common Mistakes with Generics',
    content: '### Mistake 1: Missing Type Parameters\n\nRaw types lose type safety:\n\n### Mistake 2: Variance Confusion\n\nin = consumer (contravariant), out = producer (covariant):\n\n### Mistake 3: Reified Type Erasure\n\nType information is erased at runtime without reified.',
    code: '// ❌ Wrong - raw type, no safety\nval list: List<*> = listOf(1, 2, 3)\nval first = list[0] as Int  // Unsafe cast\n\n// ✅ Correct - specific type\nval list: List<Int> = listOf(1, 2, 3)\nval first: Int = list[0]  // Safe\n\n// Variance: out = produce, in = consume\ninterface Producer<out T> { fun get(): T }\ninterface Consumer<in T> { fun accept(item: T) }\n\n// ❌ Wrong - type erased at runtime\nfun <T> isType(obj: Any): Boolean {\n    return obj is T  // Error: Cannot check erased type\n}\n\n// ✅ Correct - use reified\ninline fun <reified T> isType(obj: Any): Boolean {\n    return obj is T  // Works!\n}'
  },
  '4.9': {
    title: 'Common Mistakes with Advanced Coroutines',
    content: '### Mistake 1: Ignoring Cancellation\n\nAlways check for cancellation in long-running loops:\n\n### Mistake 2: Blocking in Coroutines\n\nAvoid blocking calls - use suspending alternatives:\n\n### Mistake 3: Exception Handling\n\nExceptions in coroutines propagate differently.',
    code: '// ❌ Wrong - ignores cancellation\nwhile (true) {\n    processItem()  // Never stops!\n}\n\n// ✅ Correct - check cancellation\nwhile (isActive) {\n    processItem()\n    yield()  // Or ensureActive()\n}\n\n// ❌ Wrong - blocking call\nwithContext(Dispatchers.IO) {\n    Thread.sleep(1000)  // Blocks thread!\n}\n\n// ✅ Correct - suspending delay\ndelay(1000)  // Suspends, does not block\n\n// Exception handling\nval handler = CoroutineExceptionHandler { _, e ->\n    println("Caught: $e")\n}\nscope.launch(handler) {\n    throw Exception("Oops")\n}'
  },
  '4.10': {
    title: 'Common Mistakes with Delegation',
    content: '### Mistake 1: Not Understanding Delegation Order\n\nDelegated properties initialize lazily - order matters:\n\n### Mistake 2: Observable vs Vetoable\n\nChoose the right delegate for your use case:\n\n### Mistake 3: Custom Delegate Errors\n\nImplement getValue/setValue correctly.',
    code: '// Lazy initialization happens on first access\nval expensive by lazy {\n    println("Computing...")\n    computeValue()  // Only called once\n}\n\n// Observable - notifies after change\nvar name by Delegates.observable("initial") { _, old, new ->\n    println("Changed from $old to $new")\n}\n\n// Vetoable - can reject changes\nvar age by Delegates.vetoable(0) { _, _, new ->\n    new >= 0  // Only allow non-negative\n}\nage = -5  // Rejected, stays 0\n\n// Custom delegate pattern\nclass LoggingDelegate<T>(private var value: T) {\n    operator fun getValue(thisRef: Any?, prop: KProperty<*>): T {\n        println("Getting ${prop.name}")\n        return value\n    }\n    operator fun setValue(thisRef: Any?, prop: KProperty<*>, newValue: T) {\n        println("Setting ${prop.name} = $newValue")\n        value = newValue\n    }\n}'
  },
  '4.11': {
    title: 'Common Mistakes with Annotations and Reflection',
    content: '### Mistake 1: Annotation Retention\n\nAnnotations may not be available at runtime by default:\n\n### Mistake 2: Reflection Performance\n\nReflection is slow - cache results:\n\n### Mistake 3: Kotlin vs Java Reflection\n\nUse Kotlin reflection for Kotlin classes.',
    code: '// Retention - default is CLASS (not available at runtime)\n@Retention(AnnotationRetention.RUNTIME)  // Required for reflection\n@Target(AnnotationTarget.CLASS)\nannotation class MyAnnotation\n\n// ❌ Wrong - reflects every time\nfun process(obj: Any) {\n    val props = obj::class.memberProperties  // Slow!\n    props.forEach { /* ... */ }\n}\n\n// ✅ Correct - cache reflection results\nprivate val propertyCache = mutableMapOf<KClass<*>, Collection<KProperty1<*, *>>>()\nfun getProperties(klass: KClass<*>) = propertyCache.getOrPut(klass) {\n    klass.memberProperties\n}\n\n// Use Kotlin reflection for Kotlin classes\nval kClass = MyClass::class  // KClass\nval jClass = MyClass::class.java  // Java Class\nkClass.memberProperties  // Kotlin properties\njClass.declaredFields  // Java fields'
  },
  '4.12': {
    title: 'Common Mistakes with DSLs',
    content: '### Mistake 1: Missing @DslMarker\n\nWithout DslMarker, implicit receivers leak:\n\n### Mistake 2: Overcomplicating DSL Syntax\n\nKeep DSL intuitive and focused:\n\n### Mistake 3: Forgetting Operator Overloading\n\nUse operators sparingly and intuitively.',
    code: '// ❌ Wrong - implicit receivers leak\nhtml {\n    body {\n        head { }  // Oops! Called on html, not body\n    }\n}\n\n// ✅ Correct - use @DslMarker\n@DslMarker\nannotation class HtmlDsl\n\n@HtmlDsl\nclass HTML { ... }\n\n@HtmlDsl\nclass Body { ... }\n\n// Now head{} in body{} is a compile error!\n\n// Keep DSL focused\nhtml {\n    head { title("My Page") }\n    body {\n        div {\n            p { text("Hello") }\n        }\n    }\n}\n\n// Operator overloading - keep intuitive\noperator fun Int.times(str: String) = str.repeat(this)\nprintln(3 * "ab")  // ababab'
  },
  '4.13': {
    title: 'Common Mistakes in Coroutine-Based Projects',
    content: '### Mistake 1: Missing Structured Concurrency\n\nAlways use proper scopes - avoid GlobalScope:\n\n### Mistake 2: Not Handling Timeouts\n\nAdd timeouts to prevent hanging operations:\n\n### Mistake 3: Resource Cleanup\n\nUse try-finally or use() for cleanup.',
    code: '// ❌ Wrong - GlobalScope leaks\nGlobalScope.launch {\n    doWork()  // Lives forever!\n}\n\n// ✅ Correct - scoped coroutines\nclass MyViewModel : ViewModel() {\n    fun load() {\n        viewModelScope.launch {\n            doWork()  // Cancelled with ViewModel\n        }\n    }\n}\n\n// Add timeouts\nwithTimeout(5000) {\n    fetchData()  // Throws TimeoutCancellationException\n}\n\n// Or nullable result\nval result = withTimeoutOrNull(5000) {\n    fetchData()\n}\n\n// Resource cleanup\ntry {\n    val resource = openResource()\n    resource.use { /* work */ }\n} finally {\n    cleanup()\n}'
  }
};

// Add warnings to lessons
let addedCount = 0;
data.modules.forEach(module => {
  module.lessons.forEach(lesson => {
    if (warnings[lesson.id] && !lesson.contentSections.some(s => s.type === 'WARNING')) {
      const w = warnings[lesson.id];
      lesson.contentSections.push({
        type: 'WARNING',
        title: w.title,
        content: w.content,
        code: w.code,
        language: 'kotlin'
      });
      console.log('Added WARNING to lesson', lesson.id);
      addedCount++;
    }
  });
});

fs.writeFileSync(coursePath, JSON.stringify(data, null, 2));
console.log(`\nFile updated successfully. Added ${addedCount} WARNING sections.`);
