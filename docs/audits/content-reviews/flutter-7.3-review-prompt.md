# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Flutter Complete Development Course (dart)
- **Module:** Module 7: Flutter Development
- **Lesson:** Module 7, Lesson 3: Error Handling and Loading States (ID: 7.3)
- **Difficulty:** intermediate
- **Estimated Time:** 60 minutes

## Current Lesson Content

{
    "id":  "7.3",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "The Three States of Network Requests",
                                "content":  "\nEvery network request goes through these states:\n- 🔄 **Loading**: \"Please wait, I\u0027m getting your data\"\n- ✅ **Success**: \"Here\u0027s your data!\"\n- ❌ **Error**: \"Oops, something went wrong\"\n\n**Think of it like ordering pizza:**\n- Loading = Pizza is being made\n- Success = Pizza delivered!\n- Error = Pizza place closed / wrong address\n\n**Good apps show ALL three states to users!**\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Bad Example (No State Management)",
                                "content":  "\n\n**Problems:**\n- No loading indicator\n- No error handling\n- No retry option\n- Bad user experience!\n\n",
                                "code":  "class BadExample extends StatefulWidget {\n  @override\n  _BadExampleState createState() =\u003e _BadExampleState();\n}\n\nclass _BadExampleState extends State\u003cBadExample\u003e {\n  List\u003cPost\u003e posts = [];\n\n  @override\n  void initState() {\n    super.initState();\n    fetchPosts();  // What if this takes 10 seconds? User sees empty screen!\n  }\n\n  Future\u003cvoid\u003e fetchPosts() async {\n    final response = await http.get(\n      Uri.parse(\u0027https://jsonplaceholder.typicode.com/posts\u0027),\n    );\n    // What if network fails? App crashes! 💥\n    final List\u003cdynamic\u003e jsonData = jsonDecode(response.body);\n    setState(() {\n      posts = jsonData.map((json) =\u003e Post.fromJson(json)).toList();\n    });\n  }\n\n  @override\n  Widget build(BuildContext context) {\n    return ListView.builder(\n      itemCount: posts.length,\n      itemBuilder: (context, index) =\u003e PostCard(post: posts[index]),\n    );\n    // User sees: Empty screen while loading, no error messages!\n  }\n}",
                                "language":  "dart"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Good Example (With State Management)",
                                "content":  "\n\n**Benefits:**\n- ✅ Shows loading spinner\n- ✅ Handles all error types\n- ✅ Retry button\n- ✅ Great user experience!\n\n",
                                "code":  "import \u0027package:flutter/material.dart\u0027;\nimport \u0027package:http/http.dart\u0027 as http;\nimport \u0027dart:convert\u0027;\n\nclass GoodExample extends StatefulWidget {\n  @override\n  _GoodExampleState createState() =\u003e _GoodExampleState();\n}\n\nclass _GoodExampleState extends State\u003cGoodExample\u003e {\n  List\u003cPost\u003e posts = [];\n  bool isLoading = false;\n  String? errorMessage;\n\n  @override\n  void initState() {\n    super.initState();\n    fetchPosts();\n  }\n\n  Future\u003cvoid\u003e fetchPosts() async {\n    setState(() {\n      isLoading = true;\n      errorMessage = null;\n    });\n\n    try {\n      final response = await http.get(\n        Uri.parse(\u0027https://jsonplaceholder.typicode.com/posts\u0027),\n      ).timeout(Duration(seconds: 10));\n\n      if (response.statusCode == 200) {\n        final List\u003cdynamic\u003e jsonData = jsonDecode(response.body);\n        setState(() {\n          posts = jsonData.map((json) =\u003e Post.fromJson(json)).toList();\n          isLoading = false;\n        });\n      } else {\n        setState(() {\n          errorMessage = \u0027Server error: ${response.statusCode}\u0027;\n          isLoading = false;\n        });\n      }\n    } on TimeoutException catch (_) {\n      setState(() {\n        errorMessage = \u0027Request timeout. Please try again.\u0027;\n        isLoading = false;\n      });\n    } on SocketException catch (_) {\n      setState(() {\n        errorMessage = \u0027No internet connection.\u0027;\n        isLoading = false;\n      });\n    } catch (e) {\n      setState(() {\n        errorMessage = \u0027Failed to load posts: $e\u0027;\n        isLoading = false;\n      });\n    }\n  }\n\n  @override\n  Widget build(BuildContext context) {\n    if (isLoading) {\n      return Center(child: CircularProgressIndicator());\n    }\n\n    if (errorMessage != null) {\n      return Center(\n        child: Column(\n          mainAxisAlignment: MainAxisAlignment.center,\n          children: [\n            Icon(Icons.error_outline, size: 60, color: Colors.red),\n            SizedBox(height: 16),\n            Text(errorMessage!, textAlign: TextAlign.center),\n            SizedBox(height: 16),\n            ElevatedButton(\n              onPressed: fetchPosts,\n              child: Text(\u0027Retry\u0027),\n            ),\n          ],\n        ),\n      );\n    }\n\n    if (posts.isEmpty) {\n      return Center(child: Text(\u0027No posts found\u0027));\n    }\n\n    return ListView.builder(\n      itemCount: posts.length,\n      itemBuilder: (context, index) =\u003e PostCard(post: posts[index]),\n    );\n  }\n}",
                                "language":  "dart"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "FutureBuilder (Automatic State Management)",
                                "content":  "\nFlutter provides FutureBuilder to handle loading/error states automatically:\n\n\n**ConnectionState values:**\n- `none`: No connection\n- `waiting`: Loading...\n- `active`: Streaming data (for Streams)\n- `done`: Complete (check hasData or hasError)\n\n",
                                "code":  "class FutureBuilderExample extends StatefulWidget {\n  @override\n  _FutureBuilderExampleState createState() =\u003e _FutureBuilderExampleState();\n}\n\nclass _FutureBuilderExampleState extends State\u003cFutureBuilderExample\u003e {\n  late Future\u003cList\u003cPost\u003e\u003e futurePost;\n\n  @override\n  void initState() {\n    super.initState();\n    futurePost = fetchPosts();\n  }\n\n  Future\u003cList\u003cPost\u003e\u003e fetchPosts() async {\n    final response = await http.get(\n      Uri.parse(\u0027https://jsonplaceholder.typicode.com/posts\u0027),\n    );\n\n    if (response.statusCode == 200) {\n      final List\u003cdynamic\u003e jsonData = jsonDecode(response.body);\n      return jsonData.map((json) =\u003e Post.fromJson(json)).toList();\n    } else {\n      throw Exception(\u0027Failed to load posts\u0027);\n    }\n  }\n\n  @override\n  Widget build(BuildContext context) {\n    return FutureBuilder\u003cList\u003cPost\u003e\u003e(\n      future: futurePost,\n      builder: (context, snapshot) {\n        // Loading state\n        if (snapshot.connectionState == ConnectionState.waiting) {\n          return Center(child: CircularProgressIndicator());\n        }\n\n        // Error state\n        if (snapshot.hasError) {\n          return Center(\n            child: Column(\n              mainAxisAlignment: MainAxisAlignment.center,\n              children: [\n                Icon(Icons.error_outline, size: 60, color: Colors.red),\n                SizedBox(height: 16),\n                Text(\u0027Error: ${snapshot.error}\u0027),\n                SizedBox(height: 16),\n                ElevatedButton(\n                  onPressed: () {\n                    setState(() {\n                      futurePost = fetchPosts();  // Retry\n                    });\n                  },\n                  child: Text(\u0027Retry\u0027),\n                ),\n              ],\n            ),\n          );\n        }\n\n        // Success state\n        if (snapshot.hasData) {\n          final posts = snapshot.data!;\n\n          if (posts.isEmpty) {\n            return Center(child: Text(\u0027No posts found\u0027));\n          }\n\n          return ListView.builder(\n            itemCount: posts.length,\n            itemBuilder: (context, index) =\u003e PostCard(post: posts[index]),\n          );\n        }\n\n        // Fallback\n        return Center(child: Text(\u0027No data\u0027));\n      },\n    );\n  }\n}",
                                "language":  "dart"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Custom Loading Indicators",
                                "content":  "\n### Shimmer Effect\n\n\n### Skeleton Screen\n\n\n",
                                "code":  "class SkeletonCard extends StatelessWidget {\n  @override\n  Widget build(BuildContext context) {\n    return Card(\n      child: Padding(\n        padding: EdgeInsets.all(16),\n        child: Column(\n          crossAxisAlignment: CrossAxisAlignment.start,\n          children: [\n            Container(\n              width: double.infinity,\n              height: 200,\n              color: Colors.grey[300],\n            ),\n            SizedBox(height: 12),\n            Container(\n              width: 200,\n              height: 16,\n              color: Colors.grey[300],\n            ),\n            SizedBox(height: 8),\n            Container(\n              width: double.infinity,\n              height: 14,\n              color: Colors.grey[300],\n            ),\n          ],\n        ),\n      ),\n    );\n  }\n}",
                                "language":  "dart"
                            },
                            {
                                "type":  "EXPERIMENT",
                                "title":  "Error Messages Best Practices",
                                "content":  "\n### User-Friendly Messages\n\n❌ **Bad**: Technical jargon\n\n✅ **Good**: Human-readable\n\n### Map Errors to Messages\n\n\n",
                                "code":  "String getFriendlyErrorMessage(dynamic error) {\n  if (error is SocketException) {\n    return \u0027No internet connection. Please check your network.\u0027;\n  } else if (error is TimeoutException) {\n    return \u0027Request timeout. The server is slow or not responding.\u0027;\n  } else if (error is FormatException) {\n    return \u0027Received invalid data from server.\u0027;\n  } else if (error.toString().contains(\u0027404\u0027)) {\n    return \u0027The requested resource was not found.\u0027;\n  } else if (error.toString().contains(\u0027500\u0027)) {\n    return \u0027Server error. Please try again later.\u0027;\n  } else {\n    return \u0027Something went wrong. Please try again.\u0027;\n  }\n}",
                                "language":  "dart"
                            },
                            {
                                "type":  "EXPERIMENT",
                                "title":  "Retry Mechanisms",
                                "content":  "\n### Simple Retry\n\n\n### Retry Button with Countdown\n\n\n",
                                "code":  "class RetryButton extends StatefulWidget {\n  final VoidCallback onRetry;\n\n  RetryButton({required this.onRetry});\n\n  @override\n  _RetryButtonState createState() =\u003e _RetryButtonState();\n}\n\nclass _RetryButtonState extends State\u003cRetryButton\u003e {\n  int countdown = 0;\n\n  void startRetry() {\n    setState(() =\u003e countdown = 3);\n\n    Timer.periodic(Duration(seconds: 1), (timer) {\n      if (countdown \u003e 0) {\n        setState(() =\u003e countdown--);\n      } else {\n        timer.cancel();\n        widget.onRetry();\n      }\n    });\n  }\n\n  @override\n  Widget build(BuildContext context) {\n    return ElevatedButton(\n      onPressed: countdown \u003e 0 ? null : startRetry,\n      child: Text(countdown \u003e 0 ? \u0027Retrying in $countdown...\u0027 : \u0027Retry\u0027),\n    );\n  }\n}",
                                "language":  "dart"
                            },
                            {
                                "type":  "WARNING",
                                "title":  "Common Mistakes",
                                "content":  "\n❌ **Mistake 1**: No loading indicator\n\n✅ **Fix**: Always show loading state\n\n❌ **Mistake 2**: Generic error messages\n\n✅ **Fix**: User-friendly messages\n\n❌ **Mistake 3**: No retry option\n\n✅ **Fix**: Add retry button\n\n",
                                "code":  "// User must restart app to try again!",
                                "language":  "dart"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What Did We Learn?",
                                "content":  "\n- ✅ Three states: Loading, Success, Error\n- ✅ Manual state management with booleans\n- ✅ FutureBuilder for automatic state handling\n- ✅ User-friendly error messages\n- ✅ Retry mechanisms\n- ✅ Offline mode with cached data\n- ✅ Beautiful loading indicators (shimmer, skeleton)\n- ✅ Professional error UI design\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Lesson Checkpoint",
                                "content":  "\n### Quiz\n\n**Question 1**: What are the three states every network request should handle?\nA) Start, Middle, End\nB) Loading, Success, Error\nC) Request, Response, Complete\nD) Active, Inactive, Done\n\n**Question 2**: What does FutureBuilder\u0027s ConnectionState.waiting indicate?\nA) The Future is loading/in progress\nB) The Future completed successfully\nC) The Future failed\nD) No Future is assigned\n\n**Question 3**: Why use user-friendly error messages instead of technical ones?\nA) They\u0027re shorter\nB) They help users understand what happened and what to do next\nC) They use less memory\nD) They\u0027re required by Flutter\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why This Matters",
                                "content":  "\n**Professional error handling transforms user experience:**\n\n**Trust Building**: Apps that gracefully handle errors feel reliable. Apps that crash or show blank screens lose users forever. 60% of users uninstall apps after just one error.\n\n**User Empowerment**: \"No internet connection\" + retry button empowers users to fix the problem. Just showing \"Error\" makes them feel helpless and frustrated.\n\n**Offline First**: Mobile networks are unreliable. Apps that show cached data offline feel fast and reliable, even on bad connections. Instagram\u0027s offline mode increased engagement 40%.\n\n**Loading States**: Users tolerate 3-second loading times WITH indicators, but abandon after 1 second WITHOUT feedback. Loading spinners aren\u0027t just UI polish - they\u0027re essential for retention.\n\n**Error Recovery**: Automatic retries and offline queuing means users\u0027 actions aren\u0027t lost. Twitter\u0027s \"Failed to send - will retry\" saved millions of tweets from being abandoned.\n\n**Real-world impact**: When Spotify added offline mode and better error handling, customer support tickets dropped 50% and user satisfaction jumped 35%.\n\n**Professional Polish**: Error handling separates amateur apps from professional ones. It\u0027s the difference between \"college project\" and \"production ready.\"\n\n"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Answer Key",
                                "content":  "1. **B** - Every network request should handle Loading (waiting for data), Success (data received), and Error (request failed) states\n2. **A** - ConnectionState.waiting means the Future is currently loading/in progress, awaiting completion\n3. **B** - User-friendly messages help users understand what happened and provide actionable next steps, reducing frustration\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Next up is: Module 7, Lesson 4: Authentication and Headers**\n"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "7.3-challenge-0",
                           "title":  "Practice Challenge",
                           "description":  "Design beautiful error screens for: ---",
                           "instructions":  "Design beautiful error screens for: ---",
                           "starterCode":  "// Practice Challenge\n// Write your code below\n\nvoid main() {\n    \n}",
                           "solution":  "// Solution: Beautiful Error Screens\n// Professional error states for common network issues\n\nimport \u0027package:flutter/material.dart\u0027;\n\nvoid main() {\n  runApp(const ErrorScreensDemo());\n}\n\nclass ErrorScreensDemo extends StatelessWidget {\n  const ErrorScreensDemo({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    return MaterialApp(\n      title: \u0027Error Screens\u0027,\n      theme: ThemeData(\n        colorScheme: ColorScheme.fromSeed(seedColor: Colors.blue),\n        useMaterial3: true,\n      ),\n      home: const ErrorShowcase(),\n    );\n  }\n}\n\nclass ErrorShowcase extends StatelessWidget {\n  const ErrorShowcase({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    return Scaffold(\n      appBar: AppBar(\n        title: const Text(\u0027Error Screen Examples\u0027),\n        backgroundColor: Theme.of(context).colorScheme.inversePrimary,\n      ),\n      body: ListView(\n        padding: const EdgeInsets.all(16),\n        children: [\n          _buildNavButton(context, \u0027No Internet\u0027, const NoInternetScreen()),\n          _buildNavButton(context, \u0027Server Error\u0027, const ServerErrorScreen()),\n          _buildNavButton(context, \u0027Not Found\u0027, const NotFoundScreen()),\n          _buildNavButton(context, \u0027Timeout\u0027, const TimeoutScreen()),\n          _buildNavButton(context, \u0027Empty State\u0027, const EmptyStateScreen()),\n        ],\n      ),\n    );\n  }\n\n  Widget _buildNavButton(BuildContext context, String title, Widget screen) {\n    return Padding(\n      padding: const EdgeInsets.only(bottom: 12),\n      child: ElevatedButton(\n        onPressed: () =\u003e Navigator.push(\n          context,\n          MaterialPageRoute(builder: (_) =\u003e screen),\n        ),\n        child: Text(title),\n      ),\n    );\n  }\n}\n\n// Reusable error screen widget\nclass ErrorScreen extends StatelessWidget {\n  final IconData icon;\n  final Color iconColor;\n  final String title;\n  final String message;\n  final String? buttonText;\n  final VoidCallback? onRetry;\n  final Widget? customAction;\n\n  const ErrorScreen({\n    super.key,\n    required this.icon,\n    this.iconColor = Colors.grey,\n    required this.title,\n    required this.message,\n    this.buttonText,\n    this.onRetry,\n    this.customAction,\n  });\n\n  @override\n  Widget build(BuildContext context) {\n    return Scaffold(\n      appBar: AppBar(\n        title: const Text(\u0027Error\u0027),\n      ),\n      body: Center(\n        child: Padding(\n          padding: const EdgeInsets.all(32),\n          child: Column(\n            mainAxisAlignment: MainAxisAlignment.center,\n            children: [\n              // Animated icon container\n              Container(\n                width: 120,\n                height: 120,\n                decoration: BoxDecoration(\n                  color: iconColor.withOpacity(0.1),\n                  shape: BoxShape.circle,\n                ),\n                child: Icon(\n                  icon,\n                  size: 64,\n                  color: iconColor,\n                ),\n              ),\n              const SizedBox(height: 32),\n              // Error title\n              Text(\n                title,\n                style: Theme.of(context).textTheme.headlineSmall?.copyWith(\n                  fontWeight: FontWeight.bold,\n                ),\n                textAlign: TextAlign.center,\n              ),\n              const SizedBox(height: 12),\n              // Error message\n              Text(\n                message,\n                style: Theme.of(context).textTheme.bodyLarge?.copyWith(\n                  color: Colors.grey[600],\n                ),\n                textAlign: TextAlign.center,\n              ),\n              const SizedBox(height: 32),\n              // Retry button or custom action\n              if (onRetry != null)\n                ElevatedButton.icon(\n                  onPressed: onRetry,\n                  icon: const Icon(Icons.refresh),\n                  label: Text(buttonText ?? \u0027Try Again\u0027),\n                  style: ElevatedButton.styleFrom(\n                    padding: const EdgeInsets.symmetric(\n                      horizontal: 32,\n                      vertical: 16,\n                    ),\n                  ),\n                ),\n              if (customAction != null) customAction!,\n            ],\n          ),\n        ),\n      ),\n    );\n  }\n}\n\n// No Internet Connection Screen\nclass NoInternetScreen extends StatelessWidget {\n  const NoInternetScreen({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    return ErrorScreen(\n      icon: Icons.wifi_off,\n      iconColor: Colors.orange,\n      title: \u0027No Internet Connection\u0027,\n      message: \u0027Please check your internet connection and try again. Make sure you are connected to WiFi or mobile data.\u0027,\n      onRetry: () {\n        // Retry logic here\n        ScaffoldMessenger.of(context).showSnackBar(\n          const SnackBar(content: Text(\u0027Retrying...\u0027)),\n        );\n      },\n    );\n  }\n}\n\n// Server Error Screen (500)\nclass ServerErrorScreen extends StatelessWidget {\n  const ServerErrorScreen({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    return ErrorScreen(\n      icon: Icons.cloud_off,\n      iconColor: Colors.red,\n      title: \u0027Server Error\u0027,\n      message: \u0027Something went wrong on our end. Our team has been notified and is working on a fix. Please try again later.\u0027,\n      buttonText: \u0027Refresh\u0027,\n      onRetry: () {\n        ScaffoldMessenger.of(context).showSnackBar(\n          const SnackBar(content: Text(\u0027Refreshing...\u0027)),\n        );\n      },\n    );\n  }\n}\n\n// Not Found Screen (404)\nclass NotFoundScreen extends StatelessWidget {\n  const NotFoundScreen({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    return ErrorScreen(\n      icon: Icons.search_off,\n      iconColor: Colors.blue,\n      title: \u0027Page Not Found\u0027,\n      message: \u0027The page you are looking for does not exist or has been moved.\u0027,\n      customAction: Column(\n        children: [\n          ElevatedButton.icon(\n            onPressed: () =\u003e Navigator.pop(context),\n            icon: const Icon(Icons.arrow_back),\n            label: const Text(\u0027Go Back\u0027),\n          ),\n          const SizedBox(height: 12),\n          TextButton(\n            onPressed: () {\n              // Navigate to home\n              Navigator.popUntil(context, (route) =\u003e route.isFirst);\n            },\n            child: const Text(\u0027Go to Home\u0027),\n          ),\n        ],\n      ),\n    );\n  }\n}\n\n// Timeout Screen\nclass TimeoutScreen extends StatelessWidget {\n  const TimeoutScreen({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    return ErrorScreen(\n      icon: Icons.timer_off,\n      iconColor: Colors.amber,\n      title: \u0027Request Timeout\u0027,\n      message: \u0027The server is taking too long to respond. This could be due to a slow network or high server load.\u0027,\n      onRetry: () {\n        ScaffoldMessenger.of(context).showSnackBar(\n          const SnackBar(content: Text(\u0027Retrying with extended timeout...\u0027)),\n        );\n      },\n    );\n  }\n}\n\n// Empty State Screen\nclass EmptyStateScreen extends StatelessWidget {\n  const EmptyStateScreen({super.key});\n\n  @override\n  Widget build(BuildContext context) {\n    return Scaffold(\n      appBar: AppBar(\n        title: const Text(\u0027Empty State\u0027),\n      ),\n      body: Center(\n        child: Padding(\n          padding: const EdgeInsets.all(32),\n          child: Column(\n            mainAxisAlignment: MainAxisAlignment.center,\n            children: [\n              // Illustration placeholder\n              Container(\n                width: 150,\n                height: 150,\n                decoration: BoxDecoration(\n                  color: Colors.blue.withOpacity(0.1),\n                  borderRadius: BorderRadius.circular(75),\n                ),\n                child: const Icon(\n                  Icons.inbox_outlined,\n                  size: 80,\n                  color: Colors.blue,\n                ),\n              ),\n              const SizedBox(height: 32),\n              Text(\n                \u0027No Items Yet\u0027,\n                style: Theme.of(context).textTheme.headlineSmall?.copyWith(\n                  fontWeight: FontWeight.bold,\n                ),\n              ),\n              const SizedBox(height: 12),\n              Text(\n                \u0027Start by adding your first item. Tap the button below to get started!\u0027,\n                style: Theme.of(context).textTheme.bodyLarge?.copyWith(\n                  color: Colors.grey[600],\n                ),\n                textAlign: TextAlign.center,\n              ),\n              const SizedBox(height: 32),\n              ElevatedButton.icon(\n                onPressed: () {\n                  ScaffoldMessenger.of(context).showSnackBar(\n                    const SnackBar(content: Text(\u0027Add new item...\u0027)),\n                  );\n                },\n                icon: const Icon(Icons.add),\n                label: const Text(\u0027Add First Item\u0027),\n                style: ElevatedButton.styleFrom(\n                  padding: const EdgeInsets.symmetric(\n                    horizontal: 32,\n                    vertical: 16,\n                  ),\n                ),\n              ),\n            ],\n          ),\n        ),\n      ),\n    );\n  }\n}",
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
                                             "level":  2,
                                             "text":  "Use an if statement to check the condition."
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
    "title":  "Module 7, Lesson 3: Error Handling and Loading States",
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
- Search for "dart Module 7, Lesson 3: Error Handling and Loading States 2024 2025" to find latest practices
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
  "lessonId": "7.3",
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

