# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Flutter Complete Development Course (dart)
- **Module:** Module 7: Flutter Development
- **Lesson:** Module 7, Lesson 2: JSON Parsing and Serialization (ID: 7.2)
- **Difficulty:** intermediate
- **Estimated Time:** 60 minutes

## Current Lesson Content

{
    "id":  "7.2",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "What is JSON?",
                                "content":  "\n**JSON (JavaScript Object Notation)** is like a universal language for data - every programming language understands it!\n\n**Think of JSON as a recipe card:**\n- Simple to read\n- Structured format\n- Easy to share\n\n**Example JSON:**\n\n**Why JSON?**\n- APIs send data as JSON\n- Lightweight (small file size)\n- Human-readable\n- Language-independent\n\n",
                                "code":  "{\n  \"name\": \"John Doe\",\n  \"age\": 25,\n  \"email\": \"john@example.com\",\n  \"isActive\": true,\n  \"hobbies\": [\"reading\", \"gaming\", \"coding\"]\n}",
                                "language":  "json"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "JSON Basics",
                                "content":  "\n### JSON Types\n\n\n**Maps to Dart:**\n- JSON string → Dart String\n- JSON number → Dart int or double\n- JSON boolean → Dart bool\n- JSON null → Dart null\n- JSON array → Dart List\n- JSON object → Dart Map\n\n",
                                "code":  "{\n  \"string\": \"Hello\",\n  \"number\": 42,\n  \"decimal\": 3.14,\n  \"boolean\": true,\n  \"null\": null,\n  \"array\": [1, 2, 3],\n  \"object\": {\"key\": \"value\"}\n}",
                                "language":  "json"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Manual JSON Parsing",
                                "content":  "\n### Simple Object\n\n\n**jsonDecode()** converts JSON string → Dart Map\n\n",
                                "code":  "import \u0027dart:convert\u0027;\n\n// JSON string from API\nString jsonString = \u0027\u0027\u0027\n{\n  \"id\": 1,\n  \"name\": \"John Doe\",\n  \"email\": \"john@example.com\"\n}\n\u0027\u0027\u0027;\n\n// Parse JSON string to Map\nMap\u003cString, dynamic\u003e json = jsonDecode(jsonString);\n\n// Access values\nint id = json[\u0027id\u0027];\nString name = json[\u0027name\u0027];\nString email = json[\u0027email\u0027];\n\nprint(\u0027ID: $id, Name: $name, Email: $email\u0027);\n// Output: ID: 1, Name: John Doe, Email: john@example.com",
                                "language":  "dart"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Creating a Model Class",
                                "content":  "\nInstead of using Map everywhere, create a class:\n\n\n",
                                "code":  "class User {\n  final int id;\n  final String name;\n  final String email;\n\n  User({\n    required this.id,\n    required this.name,\n    required this.email,\n  });\n\n  // Convert JSON Map to User object\n  factory User.fromJson(Map\u003cString, dynamic\u003e json) {\n    return User(\n      id: json[\u0027id\u0027],\n      name: json[\u0027name\u0027],\n      email: json[\u0027email\u0027],\n    );\n  }\n\n  // Convert User object to JSON Map\n  Map\u003cString, dynamic\u003e toJson() {\n    return {\n      \u0027id\u0027: id,\n      \u0027name\u0027: name,\n      \u0027email\u0027: email,\n    };\n  }\n}\n\n// Usage\nString jsonString = \u0027{\"id\": 1, \"name\": \"John\", \"email\": \"john@example.com\"}\u0027;\nMap\u003cString, dynamic\u003e jsonMap = jsonDecode(jsonString);\nUser user = User.fromJson(jsonMap);\n\nprint(user.name);  // John\n\n// Convert back to JSON\nString backToJson = jsonEncode(user.toJson());\nprint(backToJson);  // {\"id\":1,\"name\":\"John\",\"email\":\"john@example.com\"}",
                                "language":  "dart"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "json_serializable (Code Generation)",
                                "content":  "\nStop writing fromJson/toJson manually! Let code generation do it:\n\n### Setup\n\n\nRun: `flutter pub get`\n\n### Create Model with Annotations\n\n\n### Generate Code\n\n\nThis creates `user.g.dart` with all the parsing code automatically!\n\n**Or watch for changes:**\n\n",
                                "code":  "flutter pub run build_runner watch",
                                "language":  "bash"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "The Dart Macros Situation (2025 Reality Check)",
                                "content":  "\n### What Were Dart Macros?\n\nYou might have heard that **Dart Macros** would eliminate the need for `build_runner` and code generation. The promise was:\n- No more running `flutter pub run build_runner build`\n- Instant code generation\n- Cleaner developer experience\n\n### What Actually Happened\n\nGoogle **indefinitely delayed** Dart Macros in 2024-2025. The feature was more complex than anticipated, and the team pivoted to other priorities. There\u0027s no clear timeline for when (or if) macros will ship.\n\n### How to Survive Without Macros\n\n**Option 1: Stick with build_runner (Recommended)**\n\nThis is the battle-tested approach we\u0027ve been teaching:\n\n```dart\n// pubspec.yaml\ndev_dependencies:\n  build_runner: ^2.4.0\n  json_serializable: ^6.7.0\n```\n\nRun: `dart run build_runner build --delete-conflicting-outputs`\n\n**Pros**: Mature, well-documented, widely used\n**Cons**: Slower builds, extra step\n\n**Option 2: dart_mappable (Modern Alternative)**\n\n`dart_mappable` is a newer package with better DX:\n\n```dart\n// pubspec.yaml\ndependencies:\n  dart_mappable: ^4.2.0\n\ndev_dependencies:\n  build_runner: ^2.4.0\n  dart_mappable_builder: ^4.2.0\n```\n\n```dart\nimport \u0027package:dart_mappable/dart_mappable.dart\u0027;\n\npart \u0027user.mapper.dart\u0027;\n\n@MappableClass()\nclass User with UserMappable {\n  final int id;\n  final String name;\n  final String email;\n  \n  User({required this.id, required this.name, required this.email});\n}\n\n// Usage is cleaner:\nfinal user = User.fromJson(jsonString);\nfinal json = user.toJson();\nfinal copy = user.copyWith(name: \u0027New Name\u0027);\n```\n\n**Pros**: Better DX, includes `copyWith()`, supports polymorphism\n**Cons**: Still uses build_runner\n\n**Option 3: freezed (Full Featured)**\n\nFor complex models with immutability:\n\n```dart\n// pubspec.yaml\ndependencies:\n  freezed_annotation: ^2.4.0\n\ndev_dependencies:\n  build_runner: ^2.4.0\n  freezed: ^2.4.0\n  json_serializable: ^6.7.0\n```\n\n```dart\nimport \u0027package:freezed_annotation/freezed_annotation.dart\u0027;\n\npart \u0027user.freezed.dart\u0027;\npart \u0027user.g.dart\u0027;\n\n@freezed\nclass User with _$User {\n  const factory User({\n    required int id,\n    required String name,\n    required String email,\n  }) = _User;\n  \n  factory User.fromJson(Map\u003cString, dynamic\u003e json) =\u003e _$UserFromJson(json);\n}\n```\n\n**Pros**: Immutable by default, union types, pattern matching\n**Cons**: More boilerplate, steeper learning curve\n\n### The Bottom Line\n\n**Don\u0027t wait for macros.** Use `json_serializable` or `dart_mappable` today. When/if macros arrive, migration will be straightforward. The core concepts (model classes, serialization) remain the same.\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Custom Field Names",
                                "content":  "\nJSON field names don\u0027t match your Dart names? Use @JsonKey:\n\n\n",
                                "code":  "@JsonSerializable()\nclass User {\n  final int id;\n\n  @JsonKey(name: \u0027full_name\u0027)  // JSON has \"full_name\", Dart has \"fullName\"\n  final String fullName;\n\n  @JsonKey(name: \u0027email_address\u0027)\n  final String emailAddress;\n\n  User({\n    required this.id,\n    required this.fullName,\n    required this.emailAddress,\n  });\n\n  factory User.fromJson(Map\u003cString, dynamic\u003e json) =\u003e _$UserFromJson(json);\n  Map\u003cString, dynamic\u003e toJson() =\u003e _$UserToJson(this);\n}\n\n// JSON: {\"id\": 1, \"full_name\": \"John Doe\", \"email_address\": \"john@example.com\"}\n// Dart: User(id: 1, fullName: \"John Doe\", emailAddress: \"john@example.com\")",
                                "language":  "dart"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Complete Example: Blog App",
                                "content":  "\n\nRun: `flutter pub run build_runner build`\n\nNow use it:\n\n\n",
                                "code":  "// Parse JSON\nString jsonString = \u0027\u0027\u0027\n{\n  \"id\": 1,\n  \"title\": \"My First Post\",\n  \"content\": \"This is the content\",\n  \"author\": {\n    \"id\": 1,\n    \"name\": \"John Doe\",\n    \"avatar\": \"avatar.jpg\"\n  },\n  \"comments\": [\n    {\n      \"id\": 1,\n      \"text\": \"Great post!\",\n      \"author\": {\"id\": 2, \"name\": \"Jane\", \"avatar\": \"jane.jpg\"},\n      \"createdAt\": \"2025-01-01T12:00:00Z\"\n    }\n  ],\n  \"tags\": [\"flutter\", \"dart\"],\n  \"publishedAt\": \"2025-01-01T10:00:00Z\"\n}\n\u0027\u0027\u0027;\n\nMap\u003cString, dynamic\u003e json = jsonDecode(jsonString);\nPost post = Post.fromJson(json);\n\nprint(post.title);  // My First Post\nprint(post.author.name);  // John Doe\nprint(post.comments[0].text);  // Great post!\nprint(post.tags);  // [flutter, dart]",
                                "language":  "dart"
                            },
                            {
                                "type":  "EXPERIMENT",
                                "title":  "Best Practices",
                                "content":  "\n### 1. Always Use Models\n❌ **Bad**: Working with raw Maps\n\n✅ **Good**: Use model classes\n\n### 2. Use json_serializable for Complex Models\n❌ **Bad**: Manual parsing for 20+ fields\n\n✅ **Good**: Code generation\n\n### 3. Handle Null Safety\n\n### 4. Validate Data\n\n",
                                "code":  "factory User.fromJson(Map\u003cString, dynamic\u003e json) {\n  final user = _$UserFromJson(json);\n\n  // Validate\n  if (user.name.isEmpty) {\n    throw FormatException(\u0027Name cannot be empty\u0027);\n  }\n  if (!user.email.contains(\u0027@\u0027)) {\n    throw FormatException(\u0027Invalid email\u0027);\n  }\n\n  return user;\n}",
                                "language":  "dart"
                            },
                            {
                                "type":  "WARNING",
                                "title":  "Common Mistakes",
                                "content":  "\n❌ **Mistake 1**: Wrong type casting\n\n✅ **Fix**: Safe parsing\n\n❌ **Mistake 2**: Forgetting to generate code\n\n✅ **Fix**: Always run code generation after changes\n\n❌ **Mistake 3**: Not handling null\n\n✅ **Fix**: Use nullable types\n\n",
                                "code":  "final String? bio = json[\u0027bio\u0027];  // Safe!",
                                "language":  "dart"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What Did We Learn?",
                                "content":  "\n- ✅ JSON fundamentals and structure\n- ✅ Manual parsing with fromJson/toJson\n- ✅ Creating model classes\n- ✅ Handling nested objects\n- ✅ Parsing lists and arrays\n- ✅ json_serializable for code generation\n- ✅ Custom field names with @JsonKey\n- ✅ Nullable and default values\n- ✅ Best practices for type safety\n- ✅ The Dart Macros situation and alternatives (dart_mappable, freezed)\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Lesson Checkpoint",
                                "content":  "\n### Quiz\n\n**Question 1**: What does jsonDecode() return?\nA) A List\nB) A Map\u003cString, dynamic\u003e\nC) A String\nD) A User object\n\n**Question 2**: Why use json_serializable instead of manual parsing?\nA) It\u0027s faster at runtime\nB) It reduces boilerplate and prevents typos with code generation\nC) It uses less memory\nD) It\u0027s required by Flutter\n\n**Question 3**: How do you handle a JSON field that might be null?\nA) Use a non-nullable type\nB) Use a nullable type with String?\nC) Ignore it\nD) Crash the app\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Why This Matters",
                                "content":  "\n**Type-safe JSON parsing is essential because:**\n\n**Preventing Crashes**: Manual Map access with json[\u0027name\u0027] crashes if \u0027name\u0027 is misspelled or missing. Model classes catch these at compile time, not runtime when users see the crash.\n\n**Code Completion**: With models, your IDE autocompletes fields. With raw Maps, you\u0027re typing blind - one typo and your app breaks.\n\n**Refactoring Safety**: Rename a field? With models, the compiler finds every usage. With Maps, you\u0027ll miss some and ship bugs.\n\n**Documentation**: User.fromJson() is self-documenting - the model shows exactly what data you expect. Maps are opaque - you need to read API docs for every access.\n\n**Validation**: Models let you validate data in one place (fromJson). With Maps, validation code scatters everywhere, leading to inconsistencies.\n\n**Real-world impact**: Instagram\u0027s early Android app crashed 30% more than iOS because they used Maps instead of models. After switching to typed models, crash rate dropped 60% within a month.\n\n**Team Collaboration**: New developers understand your data structures instantly by reading model classes. Maps force them to dig through API documentation and guess.\n\n"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Answer Key",
                                "content":  "1. **B** - jsonDecode() converts a JSON string into a Map\u003cString, dynamic\u003e that you can then parse into model objects\n2. **B** - json_serializable generates parsing code automatically, reducing boilerplate and preventing typos/bugs from manual code\n3. **B** - Use nullable types (String?) to safely handle JSON fields that might be null or missing\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Next up is: Module 7, Lesson 3: Error Handling and Loading States**\n"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "7.2-challenge-0",
                           "title":  "Practice Challenge",
                           "description":  "Convert your Challenge 3 models to use json_serializable code generation. ---",
                           "instructions":  "Convert your Challenge 3 models to use json_serializable code generation. ---",
                           "starterCode":  "// Practice Challenge\n// Write your code below\n\nvoid main() {\n    \n}",
                           "solution":  "// Solution: Models with json_serializable Code Generation\n// Demonstrates type-safe JSON parsing with code generation\n//\n// Required dependencies in pubspec.yaml:\n//   dependencies:\n//     json_annotation: ^4.8.1\n//   dev_dependencies:\n//     build_runner: ^2.4.6\n//     json_serializable: ^6.7.1\n//\n// Run: dart run build_runner build\n\nimport \u0027package:json_annotation/json_annotation.dart\u0027;\n\n// Generated file - run build_runner to create\npart \u0027models.g.dart\u0027;\n\n// User model with json_serializable annotations\n@JsonSerializable()\nclass User {\n  final int id;\n  final String name;\n  final String username;\n  final String email;\n  final Address address;\n  final String? phone;  // Nullable field\n  final String? website;\n  final Company company;\n\n  const User({\n    required this.id,\n    required this.name,\n    required this.username,\n    required this.email,\n    required this.address,\n    this.phone,\n    this.website,\n    required this.company,\n  });\n\n  // Generated factory - connects to .g.dart file\n  factory User.fromJson(Map\u003cString, dynamic\u003e json) =\u003e _$UserFromJson(json);\n\n  // Generated method - connects to .g.dart file\n  Map\u003cString, dynamic\u003e toJson() =\u003e _$UserToJson(this);\n}\n\n// Address model - nested object\n@JsonSerializable()\nclass Address {\n  final String street;\n  final String suite;\n  final String city;\n  final String zipcode;\n  final Geo geo;\n\n  const Address({\n    required this.street,\n    required this.suite,\n    required this.city,\n    required this.zipcode,\n    required this.geo,\n  });\n\n  factory Address.fromJson(Map\u003cString, dynamic\u003e json) =\u003e _$AddressFromJson(json);\n  Map\u003cString, dynamic\u003e toJson() =\u003e _$AddressToJson(this);\n\n  // Helper to get formatted address\n  String get formatted =\u003e \u0027$street, $suite, $city $zipcode\u0027;\n}\n\n// Geo coordinates model\n@JsonSerializable()\nclass Geo {\n  final String lat;\n  final String lng;\n\n  const Geo({required this.lat, required this.lng});\n\n  factory Geo.fromJson(Map\u003cString, dynamic\u003e json) =\u003e _$GeoFromJson(json);\n  Map\u003cString, dynamic\u003e toJson() =\u003e _$GeoToJson(this);\n}\n\n// Company model\n@JsonSerializable()\nclass Company {\n  final String name;\n  final String catchPhrase;\n  final String bs;  // Business slogan\n\n  const Company({\n    required this.name,\n    required this.catchPhrase,\n    required this.bs,\n  });\n\n  factory Company.fromJson(Map\u003cString, dynamic\u003e json) =\u003e _$CompanyFromJson(json);\n  Map\u003cString, dynamic\u003e toJson() =\u003e _$CompanyToJson(this);\n}\n\n// Post model with custom field name mapping\n@JsonSerializable()\nclass Post {\n  @JsonKey(name: \u0027userId\u0027)  // Maps JSON key to Dart field\n  final int authorId;\n  final int id;\n  final String title;\n  final String body;\n\n  const Post({\n    required this.authorId,\n    required this.id,\n    required this.title,\n    required this.body,\n  });\n\n  factory Post.fromJson(Map\u003cString, dynamic\u003e json) =\u003e _$PostFromJson(json);\n  Map\u003cString, dynamic\u003e toJson() =\u003e _$PostToJson(this);\n}\n\n// Comment model\n@JsonSerializable()\nclass Comment {\n  final int postId;\n  final int id;\n  final String name;\n  final String email;\n  final String body;\n\n  const Comment({\n    required this.postId,\n    required this.id,\n    required this.name,\n    required this.email,\n    required this.body,\n  });\n\n  factory Comment.fromJson(Map\u003cString, dynamic\u003e json) =\u003e _$CommentFromJson(json);\n  Map\u003cString, dynamic\u003e toJson() =\u003e _$CommentToJson(this);\n}\n\n// Example usage and testing\nvoid main() {\n  // Example JSON data\n  final userJson = {\n    \u0027id\u0027: 1,\n    \u0027name\u0027: \u0027John Doe\u0027,\n    \u0027username\u0027: \u0027johnd\u0027,\n    \u0027email\u0027: \u0027john@example.com\u0027,\n    \u0027address\u0027: {\n      \u0027street\u0027: \u0027123 Main St\u0027,\n      \u0027suite\u0027: \u0027Apt 4\u0027,\n      \u0027city\u0027: \u0027Anytown\u0027,\n      \u0027zipcode\u0027: \u002712345\u0027,\n      \u0027geo\u0027: {\u0027lat\u0027: \u002740.7128\u0027, \u0027lng\u0027: \u0027-74.0060\u0027},\n    },\n    \u0027phone\u0027: \u0027555-1234\u0027,\n    \u0027website\u0027: \u0027johndoe.com\u0027,\n    \u0027company\u0027: {\n      \u0027name\u0027: \u0027Acme Inc\u0027,\n      \u0027catchPhrase\u0027: \u0027Innovation First\u0027,\n      \u0027bs\u0027: \u0027synergize scalable solutions\u0027,\n    },\n  };\n\n  // Parse JSON to typed model\n  final user = User.fromJson(userJson);\n  print(\u0027User: ${user.name}\u0027);\n  print(\u0027Email: ${user.email}\u0027);\n  print(\u0027Address: ${user.address.formatted}\u0027);\n  print(\u0027Company: ${user.company.name}\u0027);\n\n  // Convert back to JSON\n  final json = user.toJson();\n  print(\u0027\\nBack to JSON: $json\u0027);\n\n  // Example with Post (custom field mapping)\n  final postJson = {\n    \u0027userId\u0027: 1,  // Maps to authorId in Dart\n    \u0027id\u0027: 1,\n    \u0027title\u0027: \u0027My First Post\u0027,\n    \u0027body\u0027: \u0027Hello World!\u0027,\n  };\n\n  final post = Post.fromJson(postJson);\n  print(\u0027\\nPost by author ID: ${post.authorId}\u0027);\n  print(\u0027Title: ${post.title}\u0027);\n}",
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
    "title":  "Module 7, Lesson 2: JSON Parsing and Serialization",
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
- Search for "dart Module 7, Lesson 2: JSON Parsing and Serialization 2024 2025" to find latest practices
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

