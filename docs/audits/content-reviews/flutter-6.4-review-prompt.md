# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Flutter Complete Development Course (dart)
- **Module:** Module 6: Flutter Development
- **Lesson:** Module 6, Lesson 4: Deep Linking (ID: 6.4)
- **Difficulty:** intermediate
- **Estimated Time:** 60 minutes

## Current Lesson Content

{
    "id":  "6.4",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "What is Deep Linking?",
                                "content":  "\nImagine receiving an email: \"Check out this product!\" with a link. You tap it and:\n- ❌ **Without deep linking**: Opens browser → App store → Download app → Open app → Search for product\n- ✅ **With deep linking**: Opens app directly to that product!\n\n**Deep linking** = Direct shortcuts to specific content in your app.\n\n**Real-world examples:**\n- Instagram post link → Opens Instagram app to that post\n- Amazon product link → Opens Amazon app to product page\n- YouTube video link → Opens YouTube app playing that video\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Types of Deep Links",
                                "content":  "\n### 1. Custom URL Schemes (Old Way)\n\n**Problems:**\n- Any app can register the same scheme (security risk!)\n- No fallback if app isn\u0027t installed\n- Doesn\u0027t work on web\n\n### 2. App Links (Android) \u0026 Universal Links (iOS) (Modern Way)\n\n**Benefits:**\n- ✅ Secure (verified with your website)\n- ✅ Fallback to website if app not installed\n- ✅ Works on mobile, web, and desktop\n- ✅ Better user experience\n\n**We\u0027ll focus on the modern way!**\n\n",
                                "code":  "https://mycompany.com/product/123",
                                "language":  "dart"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Installation",
                                "content":  "\n\nRun: `flutter pub get`\n\n",
                                "code":  "# pubspec.yaml\ndependencies:\n  flutter:\n    sdk: flutter\n  go_router: ^17.0.0\n  app_links: ^6.4.1",
                                "language":  "yaml"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Step 1: Android Configuration (App Links)",
                                "content":  "\n### A. Update AndroidManifest.xml\n\n\n**Key parts:**\n- `android:autoVerify=\"true\"` - Tells Android to verify ownership\n- `android:scheme=\"https\"` - Use HTTPS (secure!)\n- `android:host=\"mycompany.com\"` - Your website domain\n\n### B. Create assetlinks.json\n\nHost this file at: `https://mycompany.com/.well-known/assetlinks.json`\n\n\n**To get SHA256 fingerprint:**\n\n\nCopy the SHA256 fingerprint from the output.\n\n",
                                "code":  "# Debug certificate (for testing)\nkeytool -list -v -keystore ~/.android/debug.keystore -alias androiddebugkey -storepass android -keypass android\n\n# Release certificate (for production)\nkeytool -list -v -keystore /path/to/your/release.keystore -alias your-key-alias",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Step 2: iOS Configuration (Universal Links)",
                                "content":  "\n### A. Update Info.plist\n\nFor Flutter 3.27+, deep linking is enabled by default. For earlier versions:\n\n\n### B. Enable Associated Domains in Xcode\n\n1. Open `ios/Runner.xcworkspace` in Xcode\n2. Select your project in the navigator\n3. Go to \"Signing \u0026 Capabilities\" tab\n4. Click \"+ Capability\"\n5. Add \"Associated Domains\"\n6. Add domain: `applinks:mycompany.com`\n\n### C. Create apple-app-site-association\n\nHost this file at: `https://mycompany.com/.well-known/apple-app-site-association`\n\n\n**To find your Team ID:**\n1. Open Xcode\n2. Go to project settings\n3. Look at \"Team\" field (10-character string)\n\n",
                                "code":  "{\n  \"applinks\": {\n    \"apps\": [],\n    \"details\": [\n      {\n        \"appID\": \"TEAM_ID.com.mycompany.myapp\",\n        \"paths\": [\"*\"]\n      }\n    ]\n  }\n}",
                                "language":  "json"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Step 3: Basic Deep Link Handling",
                                "content":  "\n\n**Test it:**\n1. Run the app\n2. Send yourself a link: `https://mycompany.com/product/456`\n3. Tap the link → App opens to Product 456!\n\n",
                                "code":  "import \u0027package:flutter/material.dart\u0027;\nimport \u0027package:go_router/go_router.dart\u0027;\nimport \u0027package:app_links/app_links.dart\u0027;\n\nvoid main() {\n  runApp(MyApp());\n}\n\nclass MyApp extends StatefulWidget {\n  @override\n  _MyAppState createState() =\u003e _MyAppState();\n}\n\nclass _MyAppState extends State\u003cMyApp\u003e {\n  late final AppLinks _appLinks;\n  final GoRouter _router = GoRouter(\n    routes: [\n      GoRoute(\n        path: \u0027/\u0027,\n        builder: (context, state) =\u003e HomeScreen(),\n      ),\n      GoRoute(\n        path: \u0027/product/:productId\u0027,\n        builder: (context, state) {\n          final productId = state.pathParameters[\u0027productId\u0027]!;\n          return ProductScreen(productId: productId);\n        },\n      ),\n    ],\n  );\n\n  @override\n  void initState() {\n    super.initState();\n    _initDeepLinks();\n  }\n\n  Future\u003cvoid\u003e _initDeepLinks() async {\n    _appLinks = AppLinks();\n\n    // Handle deep link when app is already running\n    _appLinks.uriLinkStream.listen((uri) {\n      print(\u0027Deep link received: $uri\u0027);\n      _router.go(uri.path);\n    });\n\n    // Handle deep link that opened the app\n    final initialUri = await _appLinks.getInitialLink();\n    if (initialUri != null) {\n      print(\u0027App opened with: $initialUri\u0027);\n      _router.go(initialUri.path);\n    }\n  }\n\n  @override\n  Widget build(BuildContext context) {\n    return MaterialApp.router(\n      routerConfig: _router,\n      title: \u0027Deep Linking Demo\u0027,\n    );\n  }\n}\n\nclass HomeScreen extends StatelessWidget {\n  @override\n  Widget build(BuildContext context) {\n    return Scaffold(\n      appBar: AppBar(title: Text(\u0027Home\u0027)),\n      body: Center(\n        child: Column(\n          mainAxisAlignment: MainAxisAlignment.center,\n          children: [\n            Text(\u0027Home Screen\u0027, style: TextStyle(fontSize: 24)),\n            SizedBox(height: 24),\n            ElevatedButton(\n              onPressed: () =\u003e context.go(\u0027/product/123\u0027),\n              child: Text(\u0027Go to Product 123\u0027),\n            ),\n          ],\n        ),\n      ),\n    );\n  }\n}\n\nclass ProductScreen extends StatelessWidget {\n  final String productId;\n\n  ProductScreen({required this.productId});\n\n  @override\n  Widget build(BuildContext context) {\n    return Scaffold(\n      appBar: AppBar(title: Text(\u0027Product $productId\u0027)),\n      body: Center(\n        child: Column(\n          mainAxisAlignment: MainAxisAlignment.center,\n          children: [\n            Icon(Icons.shopping_bag, size: 100),\n            SizedBox(height: 16),\n            Text(\n              \u0027Product ID: $productId\u0027,\n              style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),\n            ),\n            SizedBox(height: 16),\n            Text(\u0027This screen was opened via deep link!\u0027),\n          ],\n        ),\n      ),\n    );\n  }\n}",
                                "language":  "dart"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Testing Deep Links",
                                "content":  "\n### On Android (using ADB)\n\n\n### On iOS (using xcrun)\n\n\n### Manual Testing\n\n1. **Email yourself** the link: `https://mycompany.com/product/laptop`\n2. Open email on your phone\n3. Tap the link\n4. App should open to product page!\n\n",
                                "code":  "# Test deep link\nxcrun simctl openurl booted \"https://mycompany.com/product/laptop\"\n\n# Test another route\nxcrun simctl openurl booted \"https://mycompany.com/cart\"",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Verification Files Checklist",
                                "content":  "\n✅ **Android - assetlinks.json**\n- Location: `https://mycompany.com/.well-known/assetlinks.json`\n- Must be HTTPS (not HTTP)\n- No redirects allowed\n- Must return `Content-Type: application/json`\n\n✅ **iOS - apple-app-site-association**\n- Location: `https://mycompany.com/.well-known/apple-app-site-association`\n- Must be HTTPS\n- No `.json` extension!\n- Must return `Content-Type: application/json`\n\n**Test your files:**\n\nShould return `200 OK` with `Content-Type: application/json`\n\n",
                                "code":  "# Test Android file\ncurl -I https://mycompany.com/.well-known/assetlinks.json\n\n# Test iOS file\ncurl -I https://mycompany.com/.well-known/apple-app-site-association",
                                "language":  "bash"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Firebase Dynamic Links Alternative",
                                "content":  "\nFor more advanced features (analytics, short links, campaign tracking):\n\n\n\n**Dynamic Links can:**\n- Survive app installation (remember where user came from)\n- Track campaign performance\n- Create short links for sharing\n\n",
                                "code":  "// Handle Firebase Dynamic Links\nFirebaseDynamicLinks.instance.onLink.listen((dynamicLinkData) {\n  final Uri deepLink = dynamicLinkData.link;\n  // Handle the link\n  _router.go(deepLink.path);\n});",
                                "language":  "dart"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Common Issues and Solutions",
                                "content":  "\n### Issue 1: Link Opens Browser Instead of App\n\n**Cause:** Verification files not accessible or incorrect\n\n**Solution:**\n\n### Issue 2: Android App Not Verified\n\n**Solution:**\n\n### Issue 3: iOS Universal Links Not Working\n\n**Solutions:**\n- Make sure Associated Domains capability is added in Xcode\n- Check Team ID is correct in apple-app-site-association\n- Verify domain starts with `applinks:` in Xcode\n\n",
                                "code":  "# Check verification status\nadb shell pm get-app-links com.mycompany.myapp\n\n# Should show \"verified\" for your domain",
                                "language":  "bash"
                            },
                            {
                                "type":  "EXPERIMENT",
                                "title":  "Security Best Practices",
                                "content":  "\n✅ **DO:**\n- Use HTTPS for all deep links\n- Verify domains with assetlinks.json / apple-app-site-association\n- Validate incoming data from deep links\n- Handle invalid/malicious links gracefully\n\n❌ **DON\u0027T:**\n- Use HTTP (insecure!)\n- Trust deep link data without validation\n- Expose sensitive operations via deep links\n- Use custom schemes for production (use App Links/Universal Links)\n\n"
                            },
                            {
                                "type":  "WARNING",
                                "title":  "Common Mistakes",
                                "content":  "\n❌ **Mistake 1**: Forgetting android:autoVerify=\"true\"\n\n✅ **Fix**:\n\n❌ **Mistake 2**: Wrong file location\n\n✅ **Fix**:\n\n❌ **Mistake 3**: Not handling initial link\n\n✅ **Fix**:\n\n",
                                "code":  "// Handle both cases\n_appLinks.uriLinkStream.listen((uri) { ... });\nfinal initialUri = await _appLinks.getInitialLink();\nif (initialUri != null) { ... }",
                                "language":  "dart"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What Did We Learn?",
                                "content":  "\n- ✅ Deep linking fundamentals and benefits\n- ✅ App Links (Android) vs Universal Links (iOS)\n- ✅ Setting up verification files (assetlinks.json, apple-app-site-association)\n- ✅ Configuring AndroidManifest.xml and Info.plist\n- ✅ Using app_links package with GoRouter\n- ✅ Handling initial links and link streams\n- ✅ Testing deep links with ADB and xcrun\n- ✅ Security best practices\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Lesson Checkpoint",
                                "content":  "\n### Quiz\n\n**Question 1**: What\u0027s the main advantage of App Links/Universal Links over custom URL schemes?\nA) They\u0027re faster\nB) They\u0027re verified with your website and have fallback to web\nC) They use less battery\nD) They\u0027re easier to implement\n\n**Question 2**: Where should you host the assetlinks.json file for Android?\nA) `https://example.com/assetlinks.json`\nB) `https://example.com/.well-known/assetlinks.json`\nC) In your app\u0027s assets folder\nD) On Google Play Console\n\n**Question 3**: Which two methods do you need to handle deep links in all scenarios?\nA) `getInitialLink()` and `uriLinkStream.listen()`\nB) `onDeepLink()` and `handleLink()`\nC) `openUrl()` and `parseUrl()`\nD) `Navigator.push()` and `Navigator.pop()`\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why This Matters",
                                "content":  "\n**Deep linking is essential for:**\n\n**Marketing**: Share product links via email/SMS that open directly in your app, increasing conversions by 2-3x compared to \"open app → search\" flows.\n\n**User Experience**: User taps Instagram notification → Opens directly to that specific post, not the home feed. This seamless experience is expected in modern apps.\n\n**Re-engagement**: Send push notification with deep link to abandoned cart → User taps → Opens app directly to checkout, recovering lost sales.\n\n**Sharing**: User shares an interesting article from your news app → Friend taps link → Opens in app with content ready, creating viral growth loops.\n\n**Cross-platform**: Same link works on iOS, Android, and Web, simplifying your marketing efforts.\n\n**Real-world impact**: Airbnb saw 30% increase in bookings after implementing deep linking for shared listings!\n\n"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Answer Key",
                                "content":  "1. **B** - App Links and Universal Links are verified with your website, providing security and automatic fallback to web if app isn\u0027t installed\n2. **B** - The assetlinks.json file must be hosted at `https://example.com/.well-known/assetlinks.json` for Android verification\n3. **A** - You need `getInitialLink()` to handle the link that opened the app (cold start) and `uriLinkStream.listen()` to handle links while app is running\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Next up is: Module 6, Lesson 5: Bottom Navigation Bar**\n\n"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "6.4-challenge-0",
                           "title":  "Practice Challenge",
                           "description":  "Track which deep links are most popular using analytics. ---",
                           "instructions":  "Track which deep links are most popular using analytics. ---",
                           "starterCode":  "// Practice Challenge\n// Write your code below\n\nvoid main() {\n    \n}",
                           "solution":  "// Solution: Deep Link Analytics Tracking\n// Tracks and displays most popular deep links\n\nimport \u0027package:flutter/material.dart\u0027;\n\nvoid main() {\n  runApp(const AnalyticsApp());\n}\n\n// Simple analytics service\nclass DeepLinkAnalytics {\n  static final DeepLinkAnalytics _instance = DeepLinkAnalytics._internal();\n  factory DeepLinkAnalytics() =\u003e _instance;\n  DeepLinkAnalytics._internal();\n\n  final Map\u003cString, int\u003e _linkCounts = {};\n\n  void trackLink(String link) {\n    _linkCounts[link] = (_linkCounts[link] ?? 0) + 1;\n    print(\u0027Tracked: $link (count: ${_linkCounts[link]})\u0027);\n  }\n\n  Map\u003cString, int\u003e get stats =\u003e Map.unmodifiable(_linkCounts);\n\n  List\u003cMapEntry\u003cString, int\u003e\u003e get topLinks {\n    final sorted = _linkCounts.entries.toList()\n      ..sort((a, b) =\u003e b.value.compareTo(a.value));\n    return sorted.take(10).toList();\n  }\n}\n\nclass AnalyticsApp extends StatelessWidget {\n  const AnalyticsApp({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    return MaterialApp(\n      initialRoute: \u0027/\u0027,\n      onGenerateRoute: (settings) {\n        final path = settings.name ?? \u0027/\u0027;\n        \n        // Track every deep link navigation\n        DeepLinkAnalytics().trackLink(path);\n        \n        // Route handling\n        if (path == \u0027/\u0027) {\n          return MaterialPageRoute(builder: (_) =\u003e const HomeScreen());\n        }\n        if (path == \u0027/stats\u0027) {\n          return MaterialPageRoute(builder: (_) =\u003e const StatsScreen());\n        }\n        if (path.startsWith(\u0027/product/\u0027)) {\n          final id = path.split(\u0027/\u0027).last;\n          return MaterialPageRoute(builder: (_) =\u003e ProductScreen(id: id));\n        }\n        if (path.startsWith(\u0027/category/\u0027)) {\n          final cat = path.split(\u0027/\u0027).last;\n          return MaterialPageRoute(builder: (_) =\u003e CategoryScreen(category: cat));\n        }\n        return MaterialPageRoute(builder: (_) =\u003e const NotFoundScreen());\n      },\n    );\n  }\n}\n\nclass HomeScreen extends StatelessWidget {\n  const HomeScreen({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    return Scaffold(\n      appBar: AppBar(\n        title: const Text(\u0027Deep Link Analytics\u0027),\n        actions: [\n          IconButton(\n            icon: const Icon(Icons.analytics),\n            onPressed: () =\u003e Navigator.pushNamed(context, \u0027/stats\u0027),\n          ),\n        ],\n      ),\n      body: ListView(\n        padding: const EdgeInsets.all(16),\n        children: [\n          const Text(\u0027Tap links to track them:\u0027, style: TextStyle(fontSize: 18)),\n          const SizedBox(height: 16),\n          _buildLinkButton(context, \u0027/product/1\u0027, \u0027Product 1\u0027),\n          _buildLinkButton(context, \u0027/product/2\u0027, \u0027Product 2\u0027),\n          _buildLinkButton(context, \u0027/product/3\u0027, \u0027Product 3\u0027),\n          _buildLinkButton(context, \u0027/category/electronics\u0027, \u0027Electronics\u0027),\n          _buildLinkButton(context, \u0027/category/clothing\u0027, \u0027Clothing\u0027),\n        ],\n      ),\n    );\n  }\n\n  Widget _buildLinkButton(BuildContext context, String path, String label) {\n    return Padding(\n      padding: const EdgeInsets.only(bottom: 8),\n      child: ElevatedButton(\n        onPressed: () =\u003e Navigator.pushNamed(context, path),\n        child: Text(label),\n      ),\n    );\n  }\n}\n\nclass StatsScreen extends StatelessWidget {\n  const StatsScreen({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    final topLinks = DeepLinkAnalytics().topLinks;\n\n    return Scaffold(\n      appBar: AppBar(title: const Text(\u0027Link Analytics\u0027)),\n      body: topLinks.isEmpty\n          ? const Center(child: Text(\u0027No links tracked yet\u0027))\n          : ListView.builder(\n              itemCount: topLinks.length,\n              itemBuilder: (_, index) {\n                final entry = topLinks[index];\n                return ListTile(\n                  leading: CircleAvatar(child: Text(\u0027${index + 1}\u0027)),\n                  title: Text(entry.key),\n                  trailing: Chip(label: Text(\u0027${entry.value} visits\u0027)),\n                );\n              },\n            ),\n    );\n  }\n}\n\nclass ProductScreen extends StatelessWidget {\n  final String id;\n  const ProductScreen({super.key, required this.id});\n\n  @override\n  Widget build(BuildContext context) {\n    return Scaffold(\n      appBar: AppBar(title: Text(\u0027Product $id\u0027)),\n      body: Center(child: Text(\u0027Product ID: $id\u0027)),\n    );\n  }\n}\n\nclass CategoryScreen extends StatelessWidget {\n  final String category;\n  const CategoryScreen({super.key, required this.category});\n\n  @override\n  Widget build(BuildContext context) {\n    return Scaffold(\n      appBar: AppBar(title: Text(\u0027Category: $category\u0027)),\n      body: Center(child: Text(\u0027Category: $category\u0027)),\n    );\n  }\n}\n\nclass NotFoundScreen extends StatelessWidget {\n  const NotFoundScreen({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    return Scaffold(\n      appBar: AppBar(title: const Text(\u0027404\u0027)),\n      body: const Center(child: Text(\u0027Not Found\u0027)),\n    );\n  }\n}\n\n// Key concepts:\n// - Singleton pattern for analytics service\n// - Track in onGenerateRoute (catches all navigation)\n// - Map for counting link visits\n// - Sort by count for top links\n// - In production: use Firebase Analytics or similar",
                           "language":  "dart",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Widget builds without errors",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Read the instructions carefully and break down the problem into smaller steps."
                                         },
                                         {
                                             "level":  3,
                                             "text":  "If stuck, try writing out the solution in plain English first, then convert to dart code."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting semicolons",
                                                      "consequence":  "Syntax error",
                                                      "correction":  "Add ; at end of statements"
                                                  },
                                                  {
                                                      "mistake":  "Not handling null safety",
                                                      "consequence":  "Null check operator errors",
                                                      "correction":  "Use ? for nullable types, ! for assertion"
                                                  },
                                                  {
                                                      "mistake":  "Forgetting async/await",
                                                      "consequence":  "Future not awaited",
                                                      "correction":  "Add async to function, await before Future"
                                                  }
                                              ],
                           "difficulty":  "beginner"
                       }
                   ],
    "difficulty":  "intermediate",
    "title":  "Module 6, Lesson 4: Deep Linking",
    "estimatedMinutes":  60
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current dart documentation
- Search the web for the latest dart version and verify examples work with it
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
- Search for "dart Module 6, Lesson 4: Deep Linking 2024 2025" to find latest practices
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
  "lessonId": "6.4",
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

