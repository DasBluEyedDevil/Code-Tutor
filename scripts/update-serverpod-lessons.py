#!/usr/bin/env python3
"""
Script to update Flutter course with Serverpod lessons 8.3 and 8.4
"""

import json
import sys

# Path to the course file
COURSE_FILE = r"C:\Users\dasbl\Downloads\Code-Tutor\content\courses\flutter\course.json"

# New Lesson 8.3: Models & Code Generation
LESSON_8_3 = {
    "id": "8.3",
    "title": "Module 8, Lesson 3: Serverpod Models & Code Generation",
    "moduleId": "module-08",
    "order": 4,
    "estimatedMinutes": 45,
    "difficulty": "intermediate",
    "contentSections": [
        {
            "type": "THEORY",
            "title": "What You Will Learn",
            "content": """In this lesson, you will master Serverpod's model system and code generation capabilities. By the end, you will understand how to define data models using YAML, generate type-safe Dart code for both server and client, and leverage automatic serialization.

**Learning Objectives:**
- Understand the protocol/ folder structure and its purpose
- Write YAML model definitions with fields and relations
- Run the serverpod generate command to produce Dart code
- Understand the generated files for server, client, and protocol
- Use type-safe client models in your Flutter app
- Leverage automatic JSON serialization (toJson/fromJson)

"""
        },
        {
            "type": "THEORY",
            "title": "Why Serverpod Models Matter",
            "content": """**The Problem with Traditional Backend Development**

In traditional backend development, you often face these challenges:

1. **Duplicate Model Definitions**: You write the same model in Dart for Flutter, then again in your backend language (Node.js, Python, etc.). When the model changes, you must update both places.

2. **Manual Serialization**: You write toJson() and fromJson() methods by hand. This is tedious and error-prone.

3. **Type Mismatches**: The Flutter client expects a String, but the server sends an int. Runtime errors crash your app.

4. **API Contract Drift**: Over time, the client and server models diverge. Nobody notices until production breaks.

**Serverpod's Solution**

Serverpod solves all of these problems with a single approach: **Define your models once in YAML, generate everything else.**

When you define a model in Serverpod:
- The server model is generated automatically
- The client model is generated automatically
- Serialization (toJson/fromJson) is generated automatically
- Type safety is guaranteed at compile time
- The Flutter client and Dart server always stay in sync

This is the power of **full-stack Dart** with code generation.

"""
        },
        {
            "type": "ANALOGY",
            "title": "Real-World Analogy: The Architectural Blueprint",
            "content": """Think of Serverpod model definitions like an architectural blueprint for a building.

**Without a Blueprint (Traditional Approach)**:
- The foundation team builds based on verbal instructions
- The framing team interprets things differently
- The electrician makes assumptions about wall locations
- Everyone works from different understandings
- The building has misaligned walls and broken connections

**With a Blueprint (Serverpod Approach)**:
- ONE master document defines everything
- Foundation team reads the blueprint
- Framing team reads the SAME blueprint
- Electrician reads the SAME blueprint
- Everyone builds from a single source of truth
- The building comes together perfectly

**In Serverpod:**
- Your YAML model definition is the blueprint
- The server code reads from this blueprint
- The client code reads from the SAME blueprint
- The protocol (API contract) reads from the SAME blueprint
- Everything stays perfectly aligned

One definition. Perfect synchronization. No drift.

"""
        },
        {
            "type": "THEORY",
            "title": "The Protocol Folder Structure",
            "content": """Serverpod projects have a specific folder structure. The most important folder for models is the **protocol/** folder.

**Project Structure Overview:**

```
my_project/
├── my_project_server/           # Server-side Dart code
│   ├── lib/
│   │   └── src/
│   │       ├── endpoints/       # API endpoint classes
│   │       └── generated/       # Auto-generated server code
│   └── protocol/                # YOUR MODEL DEFINITIONS GO HERE
│       ├── user.yaml
│       ├── post.yaml
│       └── comment.yaml
│
├── my_project_client/           # Generated client library
│   └── lib/
│       └── src/
│           └── protocol/        # Auto-generated client models
│
└── my_project_flutter/          # Your Flutter app
    └── lib/
        └── ...                  # Uses my_project_client
```

**Key Points:**

1. **protocol/ folder**: This is where you write your YAML model definitions. Every .yaml file here becomes a model.

2. **generated/ folders**: Never edit these! They are recreated every time you run serverpod generate.

3. **my_project_client/**: This entire package is generated. Your Flutter app imports it to get type-safe access to your models and endpoints.

4. **Naming Convention**: The project name (my_project) becomes the prefix for all generated packages.

"""
        },
        {
            "type": "EXAMPLE",
            "title": "Your First YAML Model Definition",
            "content": """Let's create a User model. In Serverpod, models are defined in YAML files within the protocol/ folder.

**File: my_project_server/protocol/user.yaml**
""",
            "code": """# protocol/user.yaml
# This YAML file defines the User model for Serverpod

class: User
table: users  # Creates a database table named 'users'
fields:
  # Primary key (id) is added automatically by Serverpod

  name: String
  # A required String field. Cannot be null.

  email: String
  # Another required String field.

  age: int?
  # An optional integer. The ? makes it nullable.

  isActive: bool
  # A required boolean field.

  createdAt: DateTime
  # Stores when the user was created.

  profileImageUrl: String?
  # Optional URL for profile image.

  role: String, default="'user'"
  # String with a default value. Note the nested quotes.

# After running 'serverpod generate', this creates:
# 1. Server model: lib/src/generated/user.dart
# 2. Client model: my_project_client/lib/src/protocol/user.dart
# 3. Database table: 'users' with all these columns""",
            "language": "yaml"
        },
        {
            "type": "THEORY",
            "title": "Serverpod Model Syntax Deep Dive",
            "content": """Let's understand every part of the YAML model syntax.

**1. The class Keyword**

```yaml
class: User
```

This names your model. It becomes a Dart class named `User`. Use PascalCase for class names.

**2. The table Keyword (Optional)**

```yaml
table: users
```

If present, Serverpod creates a database table with this name. Use snake_case for table names. If omitted, the model exists only in memory (useful for DTOs).

**3. The fields Section**

```yaml
fields:
  fieldName: Type
```

This defines all the properties of your model.

**Field Types Supported:**

| Type | Description | Example |
|------|-------------|---------|
| String | Text data | `name: String` |
| int | Integer numbers | `age: int` |
| double | Decimal numbers | `price: double` |
| bool | True/false | `isActive: bool` |
| DateTime | Date and time | `createdAt: DateTime` |
| ByteData | Binary data | `imageData: ByteData` |
| Duration | Time duration | `timeout: Duration` |
| UuidValue | UUID identifiers | `uuid: UuidValue` |
| List<T> | List of items | `tags: List<String>` |
| Map<K,V> | Key-value pairs | `metadata: Map<String, String>` |
| CustomType | Other models | `author: User` |

**4. Making Fields Optional**

Add `?` after the type to make it nullable:

```yaml
fields:
  requiredField: String     # Must have a value
  optionalField: String?    # Can be null
```

**5. Default Values**

```yaml
fields:
  status: String, default="'pending'"
  count: int, default='0'
  isPublic: bool, default='true'
```

Note: String defaults need nested quotes: `"'value'"`. Other types use single quotes: `'0'`, `'true'`.

"""
        },
        {
            "type": "EXAMPLE",
            "title": "Complex Model with Relations",
            "content": """Real applications have related data. Let's define a Post model that belongs to a User.

**File: my_project_server/protocol/post.yaml**
""",
            "code": """# protocol/post.yaml
# A Post model with a relation to User

class: Post
table: posts
fields:
  title: String
  # The post title, required.

  content: String
  # The post body, required.

  authorId: int
  # Foreign key to the users table.
  # This stores the id of the User who wrote the post.

  author: User?, relation=userId
  # This creates a relation to the User model.
  # The ? makes it optional (not always loaded).
  # relation=userId means it uses authorId as the foreign key.
  # When you fetch a Post, you can optionally include the author.

  publishedAt: DateTime?
  # When the post was published. Null if still a draft.

  isPublished: bool, default='false'
  # Whether the post is visible to the public.

  viewCount: int, default='0'
  # How many times the post has been viewed.

  tags: List<String>?
  # Optional list of tags for categorization.

indexes:
  # Database indexes for faster queries
  post_author_idx:
    fields: authorId
    # Index on authorId for fast lookups by author

  post_published_idx:
    fields: isPublished, publishedAt
    # Composite index for finding published posts by date""",
            "language": "yaml"
        },
        {
            "type": "KEY_POINT",
            "title": "Understanding Relations",
            "content": """**Relations connect your models together.**

In the Post example above:
- `authorId: int` stores the actual foreign key value (the User's id)
- `author: User?, relation=userId` creates the relation

**Why Both Fields?**

1. **authorId** is stored in the database. It's the raw integer foreign key.

2. **author** is the actual User object. It's loaded when you explicitly request it.

**Loading Related Data:**

```dart
// Just get the post (author is null)
final post = await Post.db.findById(session, postId);
print(post?.authorId); // 42 (the integer)
print(post?.author);   // null (not loaded)

// Get post WITH author loaded
final postWithAuthor = await Post.db.findById(
  session,
  postId,
  include: Post.include(author: User.include()),
);
print(postWithAuthor?.author?.name); // "John Doe"
```

**Relation Types:**

- **One-to-Many**: One User has many Posts (shown above)
- **Many-to-One**: Many Posts belong to one User (shown above)
- **One-to-One**: One User has one Profile
- **Many-to-Many**: Posts have many Tags, Tags have many Posts

"""
        },
        {
            "type": "EXAMPLE",
            "title": "Enum Definitions",
            "content": """Serverpod also supports enum types for fixed sets of values.

**File: my_project_server/protocol/user_role.yaml**
""",
            "code": """# protocol/user_role.yaml
# An enum for user roles

enum: UserRole
values:
  - guest
  - user
  - moderator
  - admin

# This generates a Dart enum:
#
# enum UserRole with SerializableModel {
#   guest,
#   user,
#   moderator,
#   admin;
#
#   // Plus serialization methods
# }

# You can then use it in your models:
#
# class: User
# fields:
#   role: UserRole, default='UserRole.user'""",
            "language": "yaml"
        },
        {
            "type": "THEORY",
            "title": "Running serverpod generate",
            "content": """After defining your models in YAML, you must generate the Dart code.

**The Generate Command:**

```bash
# Navigate to your server project
cd my_project_server

# Run the generator
serverpod generate
```

**What Happens During Generation:**

1. **Parses YAML files**: Reads all .yaml files in protocol/

2. **Validates definitions**: Checks for errors (typos, invalid types, etc.)

3. **Generates server models**: Creates Dart classes in lib/src/generated/

4. **Generates client models**: Creates Dart classes in my_project_client/

5. **Generates protocol**: Creates the API contract

6. **Updates database migrations**: Prepares SQL for schema changes

**When to Run Generate:**

Run `serverpod generate` after:
- Adding a new .yaml model file
- Modifying an existing model
- Adding or changing endpoints
- Any change to the protocol/ folder

**Pro Tip:** Many developers set up file watchers to auto-run generate on save.

"""
        },
        {
            "type": "WARNING",
            "title": "Never Edit Generated Files",
            "content": """**Critical Rule: NEVER edit files in generated/ folders!**

The following folders are auto-generated:
- `my_project_server/lib/src/generated/`
- `my_project_client/lib/src/protocol/`

**Why?**

Every time you run `serverpod generate`, these folders are **completely overwritten**. Any manual changes you make will be lost.

**What if you need custom logic?**

1. **Extension methods**: Add functionality without modifying the class
   ```dart
   extension UserExtensions on User {
     String get fullName => name; // Custom logic
     bool get isAdult => (age ?? 0) >= 18;
   }
   ```

2. **Wrapper classes**: Create your own class that wraps the generated one

3. **Partial classes**: Serverpod supports custom code in separate files (advanced)

**If you find yourself wanting to edit generated code, you're probably doing something wrong. Ask yourself: "Can I solve this with my YAML definition or an extension?"**

"""
        },
        {
            "type": "EXAMPLE",
            "title": "Generated Server Model",
            "content": """Let's see what gets generated from our User.yaml definition.

**Generated File: my_project_server/lib/src/generated/user.dart**
""",
            "code": """// GENERATED CODE - DO NOT MODIFY BY HAND
// This file is auto-generated by Serverpod

import 'package:serverpod/serverpod.dart';

class User extends TableRow {
  @override
  String get tableName => 'users';

  // The auto-generated id field (primary key)
  @override
  int? id;

  // Your defined fields
  String name;
  String email;
  int? age;
  bool isActive;
  DateTime createdAt;
  String? profileImageUrl;
  String role;

  // Constructor
  User({
    this.id,
    required this.name,
    required this.email,
    this.age,
    required this.isActive,
    required this.createdAt,
    this.profileImageUrl,
    this.role = 'user',
  });

  // Auto-generated serialization
  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      id: json['id'] as int?,
      name: json['name'] as String,
      email: json['email'] as String,
      age: json['age'] as int?,
      isActive: json['isActive'] as bool,
      createdAt: DateTime.parse(json['createdAt'] as String),
      profileImageUrl: json['profileImageUrl'] as String?,
      role: json['role'] as String? ?? 'user',
    );
  }

  @override
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'email': email,
      'age': age,
      'isActive': isActive,
      'createdAt': createdAt.toIso8601String(),
      'profileImageUrl': profileImageUrl,
      'role': role,
    };
  }

  // Database operations (simplified example)
  static final db = UserRepository();

  // Copy with method for immutable updates
  User copyWith({
    int? id,
    String? name,
    String? email,
    int? age,
    bool? isActive,
    DateTime? createdAt,
    String? profileImageUrl,
    String? role,
  }) {
    return User(
      id: id ?? this.id,
      name: name ?? this.name,
      email: email ?? this.email,
      age: age ?? this.age,
      isActive: isActive ?? this.isActive,
      createdAt: createdAt ?? this.createdAt,
      profileImageUrl: profileImageUrl ?? this.profileImageUrl,
      role: role ?? this.role,
    );
  }
}""",
            "language": "dart"
        },
        {
            "type": "THEORY",
            "title": "Type-Safe Client Generation",
            "content": """The magic of Serverpod is that it generates a complete client library for your Flutter app.

**The Client Package**

When you run `serverpod generate`, it creates/updates:
```
my_project_client/
├── lib/
│   ├── my_project_client.dart      # Main export file
│   └── src/
│       └── protocol/
│           ├── user.dart           # Client User model
│           ├── post.dart           # Client Post model
│           ├── client.dart         # API client class
│           └── protocol.dart       # Protocol definitions
└── pubspec.yaml
```

**Using in Flutter:**

```dart
// In your Flutter app's pubspec.yaml
dependencies:
  my_project_client:
    path: ../my_project_client
```

**Type Safety Across the Stack:**

```dart
// In Flutter - This is EXACTLY the same User class!
import 'package:my_project_client/my_project_client.dart';

void createUser() {
  final user = User(
    name: 'Alice',
    email: 'alice@example.com',
    isActive: true,
    createdAt: DateTime.now(),
  );

  // The IDE knows all the fields and their types!
  print(user.name);     // String
  print(user.age);      // int?
  print(user.isActive); // bool
}
```

**No Runtime Surprises:**

If the server changes a field type from String to int, the client code won't compile. You catch errors at build time, not in production.

"""
        },
        {
            "type": "EXAMPLE",
            "title": "Automatic Serialization in Action",
            "content": """Serverpod handles all JSON conversion automatically. Here's how it works in practice.
""",
            "code": """// SERVER SIDE - In an endpoint
import 'package:serverpod/serverpod.dart';

class UserEndpoint extends Endpoint {
  // Return a User - Serverpod serializes to JSON automatically
  Future<User> getUser(Session session, int userId) async {
    final user = await User.db.findById(session, userId);
    if (user == null) {
      throw Exception('User not found');
    }
    return user; // Automatically converted to JSON
  }

  // Accept a User - Serverpod deserializes from JSON automatically
  Future<User> createUser(Session session, User user) async {
    // 'user' is already a User object, deserialized from client JSON
    final savedUser = await User.db.insertRow(session, user);
    return savedUser; // Returned as JSON to client
  }
}

// CLIENT SIDE - In Flutter
import 'package:my_project_client/my_project_client.dart';

class UserService {
  final Client client;

  UserService(this.client);

  Future<User> fetchUser(int userId) async {
    // Serverpod handles JSON deserialization
    // You get a fully typed User object!
    final user = await client.user.getUser(userId);

    print(user.name);      // Typed as String
    print(user.email);     // Typed as String
    print(user.createdAt); // Typed as DateTime

    return user;
  }

  Future<User> createUser(String name, String email) async {
    final newUser = User(
      name: name,
      email: email,
      isActive: true,
      createdAt: DateTime.now(),
    );

    // Serverpod handles JSON serialization
    // newUser is sent as JSON, response comes back as User
    return await client.user.createUser(newUser);
  }
}

// You NEVER write toJson() or fromJson() manually!
// No JSON parsing code anywhere!
// Full type safety from Flutter to PostgreSQL!""",
            "language": "dart"
        },
        {
            "type": "KEY_POINT",
            "title": "Summary: The Model Workflow",
            "content": """**The Serverpod Model Workflow:**

1. **Define** - Create a .yaml file in protocol/
   ```yaml
   class: User
   table: users
   fields:
     name: String
     email: String
   ```

2. **Generate** - Run the code generator
   ```bash
   cd my_project_server
   serverpod generate
   ```

3. **Use on Server** - Import and use the model
   ```dart
   import 'package:my_project_server/src/generated/protocol.dart';

   final user = User(name: 'Alice', email: 'a@b.com');
   await User.db.insertRow(session, user);
   ```

4. **Use on Client** - Import the client package
   ```dart
   import 'package:my_project_client/my_project_client.dart';

   final user = await client.user.getUser(42);
   print(user.name);
   ```

**Benefits:**
- Single source of truth (YAML)
- Type safety across the entire stack
- Automatic serialization
- No duplicate model definitions
- Compile-time error checking
- IDE autocomplete for all models

"""
        },
        {
            "type": "WARNING",
            "title": "Common Mistakes to Avoid",
            "content": """**Mistake 1: Forgetting to Run Generate**

After changing YAML files, you MUST run `serverpod generate`. Otherwise:
- New models won't exist
- Changed fields won't update
- You'll get confusing errors

**Mistake 2: Incorrect YAML Syntax**

```yaml
# WRONG - Missing colon after fields
fields
  name: String

# CORRECT
fields:
  name: String
```

```yaml
# WRONG - String default without nested quotes
fields:
  status: String, default='pending'

# CORRECT
fields:
  status: String, default="'pending'"
```

**Mistake 3: Editing Generated Files**

Never edit files in generated/ folders. Your changes WILL be lost.

**Mistake 4: Mismatched Package Versions**

If server and client packages are out of sync, you'll get serialization errors. Always regenerate both by running `serverpod generate` from the server folder.

**Mistake 5: Circular Relations**

```yaml
# This can cause issues
class: User
fields:
  posts: List<Post>  # User has posts

class: Post
fields:
  author: User       # Post has user
  # Both trying to include each other!
```

Use explicit include statements when fetching to avoid infinite loops.

"""
        }
    ],
    "challenges": [
        {
            "type": "FREE_CODING",
            "id": "8.3-challenge-1",
            "title": "Define a Product Model",
            "description": "Create a YAML model definition for an e-commerce Product with various field types.",
            "instructions": """Create a Product model with the following requirements:

1. The model should be named 'Product' with a table called 'products'
2. Include these fields:
   - name (required String)
   - description (optional String)
   - price (required double)
   - stockQuantity (required int with default 0)
   - isAvailable (required bool with default true)
   - createdAt (required DateTime)
   - categoryId (required int for foreign key)
   - imageUrls (optional List of Strings)

Write the complete YAML definition.""",
            "starterCode": """# protocol/product.yaml
# Define your Product model here

class: Product
# Add table and fields below
""",
            "solution": """# protocol/product.yaml
# Product model for e-commerce application

class: Product
table: products
fields:
  name: String
  # Required product name

  description: String?
  # Optional product description

  price: double
  # Required price in dollars

  stockQuantity: int, default='0'
  # How many items in stock, defaults to 0

  isAvailable: bool, default='true'
  # Whether product can be purchased

  createdAt: DateTime
  # When the product was added

  categoryId: int
  # Foreign key to categories table

  imageUrls: List<String>?
  # Optional list of image URLs

indexes:
  product_category_idx:
    fields: categoryId
  product_available_idx:
    fields: isAvailable""",
            "language": "yaml",
            "testCases": [],
            "hints": [
                {
                    "level": 1,
                    "text": "Start with 'class: Product' and 'table: products'"
                },
                {
                    "level": 2,
                    "text": "Use 'String?' for optional strings, 'List<String>?' for optional lists"
                },
                {
                    "level": 3,
                    "text": "Default values use format: fieldName: type, default='value'"
                }
            ],
            "commonMistakes": [
                {
                    "mistake": "Forgetting the question mark for optional fields",
                    "consequence": "Fields become required and cause runtime errors when null",
                    "correction": "Add ? after the type for optional fields: description: String?"
                },
                {
                    "mistake": "Using wrong quote style for defaults",
                    "consequence": "YAML parsing errors or incorrect default values",
                    "correction": "Use single quotes for numbers/bools: default='0', double nested for strings: default=\"'value'\""
                }
            ],
            "difficulty": "intermediate"
        },
        {
            "type": "FREE_CODING",
            "id": "8.3-challenge-2",
            "title": "Create Related Models",
            "description": "Define two related models: Category and Product with a one-to-many relationship.",
            "instructions": """Create two models with a relationship:

1. Category model:
   - Fields: name (String), description (String?), isActive (bool)
   - Table: categories

2. Product model:
   - Fields: name (String), price (double), categoryId (int)
   - A relation field that links to Category
   - Table: products

Show both YAML files.""",
            "starterCode": """# protocol/category.yaml
class: Category
# Complete this model

---

# protocol/product.yaml
class: Product
# Complete this model with relation to Category
""",
            "solution": """# protocol/category.yaml
class: Category
table: categories
fields:
  name: String
  description: String?
  isActive: bool, default='true'

---

# protocol/product.yaml
class: Product
table: products
fields:
  name: String
  price: double
  categoryId: int
  category: Category?, relation=categoryId

indexes:
  product_category_idx:
    fields: categoryId""",
            "language": "yaml",
            "testCases": [],
            "hints": [
                {
                    "level": 1,
                    "text": "The relation field syntax is: category: Category?, relation=categoryId"
                },
                {
                    "level": 2,
                    "text": "You need both categoryId (the int foreign key) and category (the relation)"
                }
            ],
            "commonMistakes": [
                {
                    "mistake": "Only defining the relation without the foreign key field",
                    "consequence": "No actual database column to store the relationship",
                    "correction": "Always include both: categoryId: int AND category: Category?, relation=categoryId"
                }
            ],
            "difficulty": "intermediate"
        },
        {
            "type": "FREE_CODING",
            "id": "8.3-challenge-3",
            "title": "Define an Enum and Use It",
            "description": "Create an OrderStatus enum and an Order model that uses it.",
            "instructions": """Create:

1. An OrderStatus enum with values: pending, processing, shipped, delivered, cancelled

2. An Order model with:
   - orderId (String)
   - customerId (int)
   - status (OrderStatus with default pending)
   - totalAmount (double)
   - createdAt (DateTime)
   - shippedAt (DateTime, optional)

Write both YAML definitions.""",
            "starterCode": """# protocol/order_status.yaml
enum: OrderStatus
# Add values

---

# protocol/order.yaml
class: Order
# Add fields using the OrderStatus enum
""",
            "solution": """# protocol/order_status.yaml
enum: OrderStatus
values:
  - pending
  - processing
  - shipped
  - delivered
  - cancelled

---

# protocol/order.yaml
class: Order
table: orders
fields:
  orderId: String
  customerId: int
  status: OrderStatus, default='OrderStatus.pending'
  totalAmount: double
  createdAt: DateTime
  shippedAt: DateTime?

indexes:
  order_customer_idx:
    fields: customerId
  order_status_idx:
    fields: status""",
            "language": "yaml",
            "testCases": [],
            "hints": [
                {
                    "level": 1,
                    "text": "Enum values are listed under 'values:' with each value on a new line starting with -"
                },
                {
                    "level": 2,
                    "text": "To use enum as default: status: OrderStatus, default='OrderStatus.pending'"
                }
            ],
            "commonMistakes": [
                {
                    "mistake": "Using string value for enum default instead of enum syntax",
                    "consequence": "Type mismatch and compilation errors",
                    "correction": "Use default='OrderStatus.pending' not default='pending'"
                }
            ],
            "difficulty": "intermediate"
        }
    ]
}

# New Lesson 8.4: Endpoints & Methods
LESSON_8_4 = {
    "id": "8.4",
    "title": "Module 8, Lesson 4: Serverpod Endpoints & Methods",
    "moduleId": "module-08",
    "order": 5,
    "estimatedMinutes": 45,
    "difficulty": "intermediate",
    "contentSections": [
        {
            "type": "THEORY",
            "title": "What You Will Learn",
            "content": """In this lesson, you will master Serverpod's endpoint system - the way your Flutter app communicates with your Dart server. By the end, you will be able to create API endpoints, handle authentication, work with databases, and call your server methods from Flutter.

**Learning Objectives:**
- Create endpoint classes that extend the Endpoint base class
- Understand the Session parameter and its capabilities
- Define methods with proper return types and automatic serialization
- Call endpoints from your Flutter client with full type safety
- Handle errors gracefully in endpoints
- Organize endpoints following best practices

"""
        },
        {
            "type": "THEORY",
            "title": "What are Endpoints?",
            "content": """**Endpoints are the API of your Serverpod application.**

In traditional REST APIs, you define routes like:
- GET /api/users/123
- POST /api/users
- PUT /api/users/123
- DELETE /api/users/123

In Serverpod, you define **methods on endpoint classes**:

```dart
class UserEndpoint extends Endpoint {
  Future<User?> getUser(Session session, int userId) async { ... }
  Future<User> createUser(Session session, User user) async { ... }
  Future<User> updateUser(Session session, User user) async { ... }
  Future<bool> deleteUser(Session session, int userId) async { ... }
}
```

**Key Differences from REST:**

| REST API | Serverpod Endpoint |
|----------|-------------------|
| URL routes (/api/users) | Class methods (user.getUser) |
| JSON strings | Typed Dart objects |
| Manual parsing | Automatic serialization |
| HTTP verbs (GET, POST) | Method names |
| Separate client SDK | Generated client |

**Benefits of Serverpod Endpoints:**

1. **Type Safety**: Parameters and return types are checked at compile time
2. **No Boilerplate**: No JSON parsing, no URL routing configuration
3. **Auto-Generated Client**: Flutter client code is generated automatically
4. **IDE Support**: Autocomplete for all endpoint methods

"""
        },
        {
            "type": "ANALOGY",
            "title": "Real-World Analogy: The Restaurant Kitchen",
            "content": """Think of your Serverpod server like a professional restaurant kitchen.

**The Endpoint Class = A Kitchen Station**

Like how a kitchen has stations (grill station, salad station, dessert station), your server has endpoint classes:
- UserEndpoint (handles user operations)
- PostEndpoint (handles post operations)
- OrderEndpoint (handles order operations)

**The Methods = Dishes You Can Order**

Each station can prepare specific dishes. Each endpoint has specific methods:
- UserEndpoint.getUser() - Get me a user (like ordering a steak)
- UserEndpoint.createUser() - Create a new user (like ordering a custom dish)
- PostEndpoint.listPosts() - Get all posts (like ordering the tasting menu)

**The Session = The Order Ticket**

When a waiter takes your order, they write a ticket with:
- Your table number (user authentication)
- Special requests (request context)
- The kitchen's resources (database access)

The Session parameter is like that ticket - it carries all the context needed to fulfill the request.

**The Flutter Client = The Waiter**

The generated Flutter client is like a waiter who:
- Knows all the dishes (methods) available
- Takes your order to the right station (endpoint)
- Brings back exactly what you ordered (typed response)
- Handles any problems (error handling)

"""
        },
        {
            "type": "EXAMPLE",
            "title": "Creating Your First Endpoint",
            "content": """Let's create a complete UserEndpoint with all standard operations.

**File: my_project_server/lib/src/endpoints/user_endpoint.dart**
""",
            "code": """import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

/// Endpoint for user-related operations.
///
/// All methods are automatically exposed to the Flutter client.
/// The endpoint name becomes: client.user.methodName()
class UserEndpoint extends Endpoint {

  /// Get a user by their ID.
  ///
  /// Parameters:
  ///   - session: Provided by Serverpod, contains auth and db access
  ///   - userId: The ID of the user to retrieve
  ///
  /// Returns the User if found, null otherwise.
  Future<User?> getUser(Session session, int userId) async {
    // Use the database through the session
    return await User.db.findById(session, userId);
  }

  /// Get all users (with optional pagination).
  ///
  /// Parameters:
  ///   - session: Provided by Serverpod
  ///   - limit: Maximum users to return (default 50)
  ///   - offset: Number of users to skip (for pagination)
  Future<List<User>> getAllUsers(
    Session session, {
    int limit = 50,
    int offset = 0,
  }) async {
    return await User.db.find(
      session,
      limit: limit,
      offset: offset,
      orderBy: (t) => t.createdAt,
      orderDescending: true,
    );
  }

  /// Create a new user.
  ///
  /// The User object comes from the Flutter client, fully typed.
  /// Serverpod handles all JSON deserialization automatically.
  Future<User> createUser(Session session, User user) async {
    // Insert the user into the database
    // The id will be auto-generated by PostgreSQL
    return await User.db.insertRow(session, user);
  }

  /// Update an existing user.
  ///
  /// Returns the updated User.
  /// Throws if the user doesn't exist.
  Future<User> updateUser(Session session, User user) async {
    if (user.id == null) {
      throw Exception('Cannot update user without id');
    }

    return await User.db.updateRow(session, user);
  }

  /// Delete a user by ID.
  ///
  /// Returns true if deleted, false if user didn't exist.
  Future<bool> deleteUser(Session session, int userId) async {
    final rowsDeleted = await User.db.deleteWhere(
      session,
      where: (t) => t.id.equals(userId),
    );
    return rowsDeleted > 0;
  }

  /// Find users by email domain.
  ///
  /// Example: getUsersByDomain('gmail.com') returns all Gmail users.
  Future<List<User>> getUsersByDomain(
    Session session,
    String domain,
  ) async {
    return await User.db.find(
      session,
      where: (t) => t.email.like('%@$domain'),
    );
  }

  /// Count total users.
  Future<int> countUsers(Session session) async {
    return await User.db.count(session);
  }
}""",
            "language": "dart"
        },
        {
            "type": "THEORY",
            "title": "The Session Parameter",
            "content": """**Every endpoint method receives a Session as its first parameter.**

The Session is your gateway to everything:

**1. Database Access**

```dart
Future<User?> getUser(Session session, int id) async {
  // session provides database connection
  return await User.db.findById(session, id);
}
```

**2. Authentication Info**

```dart
Future<User?> getCurrentUser(Session session) async {
  // Get the authenticated user's ID
  final userId = await session.auth.authenticatedUserId;

  if (userId == null) {
    throw Exception('Not authenticated');
  }

  return await User.db.findById(session, userId);
}
```

**3. Logging**

```dart
Future<void> processOrder(Session session, int orderId) async {
  session.log('Processing order: $orderId');

  // ... process the order ...

  session.log('Order $orderId completed');
}
```

**4. Server Configuration**

```dart
Future<String> getServerInfo(Session session) async {
  final serverId = session.server.serverId;
  return 'Running on server: $serverId';
}
```

**5. Message Passing (for real-time features)**

```dart
Future<void> broadcastMessage(Session session, String message) async {
  // Send to all connected clients
  session.messages.postMessage('chat', message);
}
```

**Key Rule:** Always accept Session as the first parameter. Serverpod provides it automatically.

"""
        },
        {
            "type": "KEY_POINT",
            "title": "Session is NOT the HTTP Request",
            "content": """**Important Distinction:**

In many web frameworks, you work with raw HTTP requests:
```javascript
// Express.js style
app.get('/users/:id', (req, res) => {
  const userId = req.params.id;  // Parse from URL
  const authToken = req.headers.authorization;  // Parse from headers
  // ... lots of manual parsing
});
```

**In Serverpod, Session abstracts all of this away:**

```dart
// Serverpod style
Future<User?> getUser(Session session, int userId) async {
  // userId is already parsed and typed!
  // Authentication is already verified!
  // Database is ready to use!
  return await User.db.findById(session, userId);
}
```

**What Session Handles For You:**
- Connection management
- Authentication state
- Database transactions
- Logging
- Error tracking
- Message queues
- Caching

**You focus on business logic. Serverpod handles infrastructure.**

"""
        },
        {
            "type": "EXAMPLE",
            "title": "Return Types and Automatic Serialization",
            "content": """Serverpod automatically handles serialization for all supported types.
""",
            "code": """import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class ProductEndpoint extends Endpoint {

  // Return a single model - automatically serialized to JSON
  Future<Product> getProduct(Session session, int id) async {
    final product = await Product.db.findById(session, id);
    if (product == null) {
      throw Exception('Product not found');
    }
    return product;  // Sent as JSON to client
  }

  // Return a nullable model - null becomes JSON null
  Future<Product?> findProductByName(Session session, String name) async {
    return await Product.db.findFirstRow(
      session,
      where: (t) => t.name.equals(name),
    );  // Returns null if not found
  }

  // Return a List - automatically becomes JSON array
  Future<List<Product>> getAllProducts(Session session) async {
    return await Product.db.find(session);  // List<Product> -> JSON array
  }

  // Return primitive types
  Future<int> countProducts(Session session) async {
    return await Product.db.count(session);  // int -> JSON number
  }

  Future<bool> isInStock(Session session, int productId) async {
    final product = await Product.db.findById(session, productId);
    return product?.stockQuantity != null && product!.stockQuantity > 0;
  }

  Future<String> getProductName(Session session, int productId) async {
    final product = await Product.db.findById(session, productId);
    return product?.name ?? 'Unknown';  // String -> JSON string
  }

  Future<double> getAveragePrice(Session session) async {
    final products = await Product.db.find(session);
    if (products.isEmpty) return 0.0;

    final total = products.fold<double>(
      0.0,
      (sum, p) => sum + p.price,
    );
    return total / products.length;  // double -> JSON number
  }

  // Return complex nested structures
  Future<Map<String, dynamic>> getProductStats(Session session) async {
    final products = await Product.db.find(session);

    return {
      'totalProducts': products.length,
      'averagePrice': products.isEmpty
          ? 0.0
          : products.fold<double>(0, (s, p) => s + p.price) / products.length,
      'inStockCount': products.where((p) => p.stockQuantity > 0).length,
    };
  }

  // Void methods - no return value
  Future<void> logProductView(Session session, int productId) async {
    session.log('Product viewed: $productId');
    // Update view count, analytics, etc.
  }
}""",
            "language": "dart"
        },
        {
            "type": "EXAMPLE",
            "title": "Calling Endpoints from Flutter",
            "content": """Now let's see how to call these endpoints from your Flutter app.
""",
            "code": """// In your Flutter app
import 'package:my_project_client/my_project_client.dart';
import 'package:serverpod_flutter/serverpod_flutter.dart';

// 1. Create the client (usually in main.dart or a service)
late Client client;

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  // Initialize the Serverpod client
  client = Client(
    'http://localhost:8080/',  // Your server URL
    authenticationKeyManager: FlutterAuthenticationKeyManager(),
  );

  runApp(MyApp());
}

// 2. Use the client in your widgets/services
class ProductService {

  // Get all products
  Future<List<Product>> fetchProducts() async {
    try {
      // client.product matches ProductEndpoint
      // .getAllProducts matches the method name
      return await client.product.getAllProducts();
    } catch (e) {
      print('Error fetching products: $e');
      rethrow;
    }
  }

  // Get a single product
  Future<Product> fetchProduct(int id) async {
    // Full type safety! The return type is Product
    final product = await client.product.getProduct(id);
    return product;
  }

  // Find product by name (nullable return)
  Future<Product?> findByName(String name) async {
    // Return type is Product? - might be null
    return await client.product.findProductByName(name);
  }

  // Create a new product
  Future<Product> createProduct({
    required String name,
    required double price,
    required int categoryId,
  }) async {
    // Create the Product object
    final product = Product(
      name: name,
      price: price,
      categoryId: categoryId,
      stockQuantity: 0,
      isAvailable: true,
      createdAt: DateTime.now(),
    );

    // Send to server - returns the created product with id
    return await client.product.createProduct(product);
  }

  // Update a product
  Future<Product> updateProduct(Product product) async {
    return await client.product.updateProduct(product);
  }

  // Get stats
  Future<Map<String, dynamic>> getStats() async {
    return await client.product.getProductStats();
  }
}

// 3. Use in a widget
class ProductListScreen extends StatefulWidget {
  @override
  _ProductListScreenState createState() => _ProductListScreenState();
}

class _ProductListScreenState extends State<ProductListScreen> {
  List<Product> _products = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _loadProducts();
  }

  Future<void> _loadProducts() async {
    setState(() => _isLoading = true);

    try {
      // Call the endpoint - fully typed!
      _products = await client.product.getAllProducts();
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Failed to load: $e')),
      );
    } finally {
      setState(() => _isLoading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    if (_isLoading) {
      return Center(child: CircularProgressIndicator());
    }

    return ListView.builder(
      itemCount: _products.length,
      itemBuilder: (context, index) {
        final product = _products[index];
        return ListTile(
          title: Text(product.name),  // Type-safe access
          subtitle: Text('\$${product.price.toStringAsFixed(2)}'),
        );
      },
    );
  }
}""",
            "language": "dart"
        },
        {
            "type": "THEORY",
            "title": "Error Handling in Endpoints",
            "content": """**Proper error handling makes your API robust and user-friendly.**

**Throwing Exceptions**

```dart
Future<User> getUser(Session session, int userId) async {
  final user = await User.db.findById(session, userId);

  if (user == null) {
    // This exception reaches the Flutter client
    throw Exception('User with id $userId not found');
  }

  return user;
}
```

**Custom Exception Types**

Serverpod provides `SerializableException` for type-safe errors:

```dart
// Define in protocol/exceptions.yaml
exception: UserNotFoundException
fields:
  userId: int
  message: String
```

```dart
// Use in your endpoint
Future<User> getUser(Session session, int userId) async {
  final user = await User.db.findById(session, userId);

  if (user == null) {
    throw UserNotFoundException(
      userId: userId,
      message: 'User not found',
    );
  }

  return user;
}
```

**Handling on Client Side**

```dart
// In Flutter
try {
  final user = await client.user.getUser(123);
  print('Got user: ${user.name}');
} on UserNotFoundException catch (e) {
  // Typed exception handling!
  print('User ${e.userId} not found: ${e.message}');
} catch (e) {
  // Generic error handling
  print('Unexpected error: $e');
}
```

"""
        },
        {
            "type": "EXAMPLE",
            "title": "Comprehensive Error Handling Example",
            "content": """Here's a complete example with proper error handling.
""",
            "code": """// SERVER: lib/src/endpoints/order_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class OrderEndpoint extends Endpoint {

  /// Create a new order with validation.
  Future<Order> createOrder(
    Session session,
    Order order,
  ) async {
    // Validate the order
    if (order.items.isEmpty) {
      throw ArgumentError('Order must have at least one item');
    }

    if (order.totalAmount <= 0) {
      throw ArgumentError('Order total must be positive');
    }

    // Check if customer exists
    final customer = await Customer.db.findById(
      session,
      order.customerId,
    );

    if (customer == null) {
      throw CustomerNotFoundException(
        customerId: order.customerId,
        message: 'Customer not found',
      );
    }

    // Check stock for all items
    for (final item in order.items) {
      final product = await Product.db.findById(session, item.productId);

      if (product == null) {
        throw ProductNotFoundException(
          productId: item.productId,
          message: 'Product not found',
        );
      }

      if (product.stockQuantity < item.quantity) {
        throw InsufficientStockException(
          productId: item.productId,
          requested: item.quantity,
          available: product.stockQuantity,
        );
      }
    }

    // All validation passed - create the order
    try {
      final createdOrder = await Order.db.insertRow(session, order);

      // Update stock quantities
      for (final item in order.items) {
        await Product.db.updateRow(
          session,
          Product(
            id: item.productId,
            stockQuantity: -item.quantity, // Will be added to current
          ),
        );
      }

      session.log('Order created: ${createdOrder.id}');
      return createdOrder;

    } catch (e) {
      session.log('Failed to create order: $e', level: LogLevel.error);
      throw Exception('Failed to create order. Please try again.');
    }
  }
}

// CLIENT: Using the endpoint in Flutter
class OrderService {
  final Client client;

  OrderService(this.client);

  Future<OrderResult> placeOrder(Order order) async {
    try {
      final createdOrder = await client.order.createOrder(order);
      return OrderResult.success(createdOrder);

    } on CustomerNotFoundException catch (e) {
      return OrderResult.error(
        'Customer account not found. Please log in again.',
      );

    } on ProductNotFoundException catch (e) {
      return OrderResult.error(
        'Product ${e.productId} is no longer available.',
      );

    } on InsufficientStockException catch (e) {
      return OrderResult.error(
        'Only ${e.available} items available (you requested ${e.requested}).',
      );

    } on ArgumentError catch (e) {
      return OrderResult.error('Invalid order: ${e.message}');

    } catch (e) {
      return OrderResult.error('Something went wrong. Please try again.');
    }
  }
}

// Simple result wrapper
class OrderResult {
  final Order? order;
  final String? errorMessage;
  final bool isSuccess;

  OrderResult.success(this.order)
      : errorMessage = null, isSuccess = true;

  OrderResult.error(this.errorMessage)
      : order = null, isSuccess = false;
}""",
            "language": "dart"
        },
        {
            "type": "KEY_POINT",
            "title": "Endpoint Organization Best Practices",
            "content": """**Organize endpoints by domain, not by operation type.**

**Good Structure:**
```
lib/src/endpoints/
├── user_endpoint.dart      # All user operations
├── product_endpoint.dart   # All product operations
├── order_endpoint.dart     # All order operations
├── cart_endpoint.dart      # All cart operations
└── auth_endpoint.dart      # Authentication operations
```

**Bad Structure:**
```
lib/src/endpoints/
├── get_endpoints.dart      # All GET operations (mixed domains)
├── post_endpoints.dart     # All POST operations (mixed domains)
├── delete_endpoints.dart   # All DELETE operations (mixed domains)
```

**Naming Conventions:**

1. **Endpoint Class**: `{Domain}Endpoint` (PascalCase)
   - UserEndpoint, ProductEndpoint, OrderEndpoint

2. **Methods**: Use verb-noun format (camelCase)
   - getUser, createUser, updateUser, deleteUser
   - listProducts, findProductByName, countProducts
   - placeOrder, cancelOrder, getOrderHistory

3. **File Names**: `{domain}_endpoint.dart` (snake_case)
   - user_endpoint.dart, product_endpoint.dart

**Method Grouping Within an Endpoint:**

```dart
class UserEndpoint extends Endpoint {
  // === CRUD Operations ===
  Future<User> createUser(...) async { }
  Future<User?> getUser(...) async { }
  Future<User> updateUser(...) async { }
  Future<bool> deleteUser(...) async { }

  // === Query Operations ===
  Future<List<User>> getAllUsers(...) async { }
  Future<List<User>> searchUsers(...) async { }
  Future<int> countUsers(...) async { }

  // === Authentication Related ===
  Future<User?> getCurrentUser(...) async { }
  Future<void> updatePassword(...) async { }
}
```

"""
        },
        {
            "type": "WARNING",
            "title": "Common Endpoint Mistakes",
            "content": """**Mistake 1: Forgetting the Session Parameter**

```dart
// WRONG - Missing session
Future<User?> getUser(int userId) async { ... }

// CORRECT - Session is required first parameter
Future<User?> getUser(Session session, int userId) async { ... }
```

**Mistake 2: Not Running Generate After Changes**

After adding or modifying endpoints, you MUST run:
```bash
serverpod generate
```

Otherwise, the client won't know about your new methods.

**Mistake 3: Returning Non-Serializable Types**

```dart
// WRONG - HttpRequest is not serializable
Future<HttpRequest> getRequest(Session session) async { ... }

// CORRECT - Return serializable types only
Future<Map<String, String>> getRequestHeaders(Session session) async { ... }
```

**Mistake 4: Long-Running Operations Without Feedback**

```dart
// BAD - Client waits forever with no feedback
Future<void> processLargeFile(Session session, ByteData file) async {
  // 10 minute operation with no progress updates
}

// BETTER - Use streaming or status polling
Future<String> startProcessing(Session session, ByteData file) async {
  // Start background job, return job ID immediately
  return jobId;
}

Future<ProcessingStatus> getStatus(Session session, String jobId) async {
  // Client can poll this for progress
}
```

**Mistake 5: Exposing Internal Methods**

```dart
class UserEndpoint extends Endpoint {
  // This helper should be private!
  // BAD - Exposed to client
  Future<void> validateEmail(Session session, String email) async { }

  // GOOD - Private helper (not exposed)
  bool _isValidEmail(String email) {
    return email.contains('@');
  }
}
```

Only public methods are exposed to the client. Use private methods (starting with _) for internal logic.

"""
        },
        {
            "type": "THEORY",
            "title": "Authentication in Endpoints",
            "content": """**Serverpod provides built-in authentication support.**

**Checking if User is Authenticated:**

```dart
Future<User> getCurrentUser(Session session) async {
  // Get the authenticated user's ID
  final userId = await session.auth.authenticatedUserId;

  if (userId == null) {
    throw NotAuthenticatedException();
  }

  final user = await User.db.findById(session, userId);
  if (user == null) {
    throw Exception('User record not found');
  }

  return user;
}
```

**Requiring Authentication:**

```dart
// All methods in this endpoint require authentication
class SecureEndpoint extends Endpoint {
  @override
  bool get requireLogin => true;  // Enforces auth for all methods

  Future<String> getSecretData(Session session) async {
    // Only authenticated users can call this
    return 'Top secret!';
  }
}
```

**Role-Based Access:**

```dart
Future<void> deleteUser(Session session, int userId) async {
  // Check if current user is admin
  final currentUserId = await session.auth.authenticatedUserId;
  final currentUser = await User.db.findById(session, currentUserId!);

  if (currentUser?.role != 'admin') {
    throw UnauthorizedException('Only admins can delete users');
  }

  await User.db.deleteWhere(
    session,
    where: (t) => t.id.equals(userId),
  );
}
```

**Scopes (for fine-grained permissions):**

```dart
@override
Set<Scope> get requiredScopes => {Scope('users:write')};

Future<User> createUser(Session session, User user) async {
  // Only users with 'users:write' scope can call this
  return await User.db.insertRow(session, user);
}
```

"""
        },
        {
            "type": "EXAMPLE",
            "title": "Complete Real-World Endpoint",
            "content": """Here's a production-ready endpoint with all best practices applied.
""",
            "code": """import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

/// Endpoint for blog post operations.
///
/// Handles creating, reading, updating, and deleting blog posts.
/// Some operations require authentication.
class PostEndpoint extends Endpoint {

  // === PUBLIC METHODS (No auth required) ===

  /// Get a published post by ID.
  /// Returns null if post doesn't exist or is not published.
  Future<Post?> getPost(Session session, int postId) async {
    final post = await Post.db.findById(
      session,
      postId,
      include: Post.include(
        author: User.include(),  // Include author data
      ),
    );

    // Only return published posts publicly
    if (post == null || !post.isPublished) {
      return null;
    }

    // Increment view count (fire and forget)
    _incrementViewCount(session, postId);

    return post;
  }

  /// List published posts with pagination.
  Future<List<Post>> listPosts(
    Session session, {
    int limit = 20,
    int offset = 0,
    String? tag,
  }) async {
    return await Post.db.find(
      session,
      where: (t) {
        var condition = t.isPublished.equals(true);
        if (tag != null) {
          // Filter by tag if provided
          condition = condition & t.tags.like('%$tag%');
        }
        return condition;
      },
      limit: limit,
      offset: offset,
      orderBy: (t) => t.publishedAt,
      orderDescending: true,
      include: Post.include(author: User.include()),
    );
  }

  /// Search posts by title or content.
  Future<List<Post>> searchPosts(
    Session session,
    String query,
  ) async {
    if (query.length < 3) {
      throw ArgumentError('Search query must be at least 3 characters');
    }

    return await Post.db.find(
      session,
      where: (t) =>
        t.isPublished.equals(true) & (
          t.title.ilike('%$query%') |
          t.content.ilike('%$query%')
        ),
      limit: 50,
      orderBy: (t) => t.publishedAt,
      orderDescending: true,
    );
  }

  // === AUTHENTICATED METHODS ===

  /// Create a new post. Requires authentication.
  Future<Post> createPost(Session session, Post post) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw NotAuthenticatedException();
    }

    // Validate
    if (post.title.trim().isEmpty) {
      throw ArgumentError('Title cannot be empty');
    }
    if (post.content.trim().length < 100) {
      throw ArgumentError('Content must be at least 100 characters');
    }

    // Set author and timestamps
    final now = DateTime.now();
    final postToCreate = post.copyWith(
      authorId: userId,
      createdAt: now,
      publishedAt: post.isPublished ? now : null,
      viewCount: 0,
    );

    final created = await Post.db.insertRow(session, postToCreate);
    session.log('Post created: ${created.id} by user $userId');

    return created;
  }

  /// Update a post. Only the author can update their own posts.
  Future<Post> updatePost(Session session, Post post) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw NotAuthenticatedException();
    }

    // Verify ownership
    final existingPost = await Post.db.findById(session, post.id!);
    if (existingPost == null) {
      throw PostNotFoundException(postId: post.id!);
    }
    if (existingPost.authorId != userId) {
      throw UnauthorizedException('You can only edit your own posts');
    }

    // Handle publish state change
    Post postToUpdate = post;
    if (!existingPost.isPublished && post.isPublished) {
      // First time publishing
      postToUpdate = post.copyWith(publishedAt: DateTime.now());
    }

    return await Post.db.updateRow(session, postToUpdate);
  }

  /// Delete a post. Only the author can delete their own posts.
  Future<bool> deletePost(Session session, int postId) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw NotAuthenticatedException();
    }

    final post = await Post.db.findById(session, postId);
    if (post == null) {
      return false;  // Already deleted or never existed
    }

    if (post.authorId != userId) {
      throw UnauthorizedException('You can only delete your own posts');
    }

    final deleted = await Post.db.deleteRow(session, post);
    session.log('Post deleted: $postId by user $userId');

    return deleted;
  }

  /// Get posts by the current user (including drafts).
  Future<List<Post>> getMyPosts(Session session) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw NotAuthenticatedException();
    }

    return await Post.db.find(
      session,
      where: (t) => t.authorId.equals(userId),
      orderBy: (t) => t.createdAt,
      orderDescending: true,
    );
  }

  // === PRIVATE HELPERS ===

  /// Increment view count without blocking the response.
  void _incrementViewCount(Session session, int postId) {
    // Run async without awaiting
    Post.db.findById(session, postId).then((post) {
      if (post != null) {
        Post.db.updateRow(
          session,
          post.copyWith(viewCount: post.viewCount + 1),
        );
      }
    });
  }
}""",
            "language": "dart"
        },
        {
            "type": "KEY_POINT",
            "title": "Summary: The Endpoint Workflow",
            "content": """**Creating and Using Endpoints:**

1. **Create Endpoint Class**
   ```dart
   // lib/src/endpoints/my_endpoint.dart
   class MyEndpoint extends Endpoint {
     Future<Result> myMethod(Session session, ...) async {
       // Implementation
     }
   }
   ```

2. **Generate Client Code**
   ```bash
   cd my_project_server
   serverpod generate
   ```

3. **Call from Flutter**
   ```dart
   final result = await client.my.myMethod(...);
   ```

**Key Points:**
- Endpoints are classes that extend `Endpoint`
- Session is always the first parameter
- Methods are automatically exposed to the client
- Return types are automatically serialized
- Exceptions propagate to the client
- Use `requireLogin` for authenticated endpoints
- Private methods (starting with _) are not exposed

**Best Practices:**
- One endpoint per domain (UserEndpoint, PostEndpoint)
- Clear method names (getUser, createPost, deleteOrder)
- Proper error handling with typed exceptions
- Authentication checks where needed
- Logging for important operations
- Input validation before database operations

"""
        }
    ],
    "challenges": [
        {
            "type": "FREE_CODING",
            "id": "8.4-challenge-1",
            "title": "Create a Task Endpoint",
            "description": "Create an endpoint for managing todo tasks with CRUD operations.",
            "instructions": """Create a TaskEndpoint with the following methods:

1. getTasks(Session session) - Returns all tasks
2. getTask(Session session, int taskId) - Returns a single task or null
3. createTask(Session session, Task task) - Creates and returns a new task
4. updateTask(Session session, Task task) - Updates and returns the task
5. deleteTask(Session session, int taskId) - Deletes and returns success boolean

Assume a Task model exists with: id, title, isCompleted, createdAt fields.""",
            "starterCode": """import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class TaskEndpoint extends Endpoint {
  // Implement the 5 methods here

}
""",
            "solution": """import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class TaskEndpoint extends Endpoint {

  /// Get all tasks ordered by creation date.
  Future<List<Task>> getTasks(Session session) async {
    return await Task.db.find(
      session,
      orderBy: (t) => t.createdAt,
      orderDescending: true,
    );
  }

  /// Get a single task by ID.
  Future<Task?> getTask(Session session, int taskId) async {
    return await Task.db.findById(session, taskId);
  }

  /// Create a new task.
  Future<Task> createTask(Session session, Task task) async {
    if (task.title.trim().isEmpty) {
      throw ArgumentError('Task title cannot be empty');
    }

    final taskToCreate = task.copyWith(
      createdAt: DateTime.now(),
      isCompleted: false,
    );

    return await Task.db.insertRow(session, taskToCreate);
  }

  /// Update an existing task.
  Future<Task> updateTask(Session session, Task task) async {
    if (task.id == null) {
      throw ArgumentError('Task ID is required for update');
    }

    final existing = await Task.db.findById(session, task.id!);
    if (existing == null) {
      throw Exception('Task not found');
    }

    return await Task.db.updateRow(session, task);
  }

  /// Delete a task by ID.
  Future<bool> deleteTask(Session session, int taskId) async {
    final task = await Task.db.findById(session, taskId);
    if (task == null) {
      return false;
    }

    await Task.db.deleteRow(session, task);
    return true;
  }
}""",
            "language": "dart",
            "testCases": [],
            "hints": [
                {
                    "level": 1,
                    "text": "Each method needs Session as the first parameter"
                },
                {
                    "level": 2,
                    "text": "Use Task.db.find(), findById(), insertRow(), updateRow(), deleteRow()"
                },
                {
                    "level": 3,
                    "text": "Return types should match: List<Task>, Task?, Task, Task, bool"
                }
            ],
            "commonMistakes": [
                {
                    "mistake": "Forgetting to check if task exists before update/delete",
                    "consequence": "Null reference errors or confusing behavior",
                    "correction": "Always check with findById() before updateRow() or deleteRow()"
                }
            ],
            "difficulty": "intermediate"
        },
        {
            "type": "FREE_CODING",
            "id": "8.4-challenge-2",
            "title": "Add Authentication to Endpoint",
            "description": "Create a secure endpoint that requires user authentication.",
            "instructions": """Create a NoteEndpoint where:

1. All methods require authentication (use requireLogin getter)
2. getMyNotes() returns only notes belonging to the authenticated user
3. createNote() automatically sets the authorId to the current user
4. deleteNote() only allows deleting own notes (check authorId matches)

Handle authentication errors with appropriate exceptions.""",
            "starterCode": """import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class NoteEndpoint extends Endpoint {
  // Hint: Override requireLogin getter

  // Implement authenticated methods

}
""",
            "solution": """import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class NoteEndpoint extends Endpoint {

  // Require authentication for all methods
  @override
  bool get requireLogin => true;

  /// Get all notes for the current user.
  Future<List<Note>> getMyNotes(Session session) async {
    final userId = await session.auth.authenticatedUserId;
    // userId guaranteed non-null due to requireLogin

    return await Note.db.find(
      session,
      where: (t) => t.authorId.equals(userId!),
      orderBy: (t) => t.createdAt,
      orderDescending: true,
    );
  }

  /// Create a note for the current user.
  Future<Note> createNote(Session session, Note note) async {
    final userId = await session.auth.authenticatedUserId;

    if (note.content.trim().isEmpty) {
      throw ArgumentError('Note content cannot be empty');
    }

    final noteToCreate = note.copyWith(
      authorId: userId!,
      createdAt: DateTime.now(),
    );

    return await Note.db.insertRow(session, noteToCreate);
  }

  /// Delete a note (only if owned by current user).
  Future<bool> deleteNote(Session session, int noteId) async {
    final userId = await session.auth.authenticatedUserId;

    final note = await Note.db.findById(session, noteId);
    if (note == null) {
      return false;
    }

    // Security check: only delete own notes
    if (note.authorId != userId) {
      throw UnauthorizedException(
        'You can only delete your own notes',
      );
    }

    await Note.db.deleteRow(session, note);
    return true;
  }
}

// Custom exception for unauthorized access
class UnauthorizedException implements Exception {
  final String message;
  UnauthorizedException(this.message);

  @override
  String toString() => 'UnauthorizedException: $message';
}""",
            "language": "dart",
            "testCases": [],
            "hints": [
                {
                    "level": 1,
                    "text": "Override the requireLogin getter to return true"
                },
                {
                    "level": 2,
                    "text": "Use session.auth.authenticatedUserId to get the current user's ID"
                },
                {
                    "level": 3,
                    "text": "Before deleting, compare note.authorId with the authenticated user's ID"
                }
            ],
            "commonMistakes": [
                {
                    "mistake": "Not checking ownership before delete/update operations",
                    "consequence": "Users can modify or delete other users' data",
                    "correction": "Always verify the resource's authorId matches the authenticated user"
                }
            ],
            "difficulty": "intermediate"
        },
        {
            "type": "FREE_CODING",
            "id": "8.4-challenge-3",
            "title": "Call Endpoints from Flutter",
            "description": "Write Flutter code to call Serverpod endpoints and handle responses.",
            "instructions": """Write a ProductService class in Flutter that:

1. Has a constructor accepting a Client instance
2. Implements fetchAllProducts() that calls client.product.getAllProducts()
3. Implements fetchProduct(int id) that gets a single product
4. Implements createProduct(String name, double price) that creates a new product
5. Handles errors and returns appropriate results

Use try-catch for error handling.""",
            "starterCode": """import 'package:my_project_client/my_project_client.dart';

class ProductService {
  // Add constructor and client field

  // Implement methods to call endpoints

}
""",
            "solution": """import 'package:my_project_client/my_project_client.dart';

class ProductService {
  final Client client;

  ProductService(this.client);

  /// Fetch all products from the server.
  Future<List<Product>> fetchAllProducts() async {
    try {
      return await client.product.getAllProducts();
    } catch (e) {
      print('Error fetching products: $e');
      rethrow;
    }
  }

  /// Fetch a single product by ID.
  /// Returns null if not found.
  Future<Product?> fetchProduct(int id) async {
    try {
      return await client.product.getProduct(id);
    } on Exception catch (e) {
      if (e.toString().contains('not found')) {
        return null;
      }
      rethrow;
    }
  }

  /// Create a new product.
  Future<Product> createProduct(String name, double price) async {
    if (name.trim().isEmpty) {
      throw ArgumentError('Product name cannot be empty');
    }
    if (price <= 0) {
      throw ArgumentError('Price must be positive');
    }

    final product = Product(
      name: name,
      price: price,
      stockQuantity: 0,
      isAvailable: true,
      createdAt: DateTime.now(),
      categoryId: 1, // Default category
    );

    try {
      return await client.product.createProduct(product);
    } catch (e) {
      print('Error creating product: $e');
      rethrow;
    }
  }

  /// Delete a product.
  Future<bool> deleteProduct(int id) async {
    try {
      return await client.product.deleteProduct(id);
    } catch (e) {
      print('Error deleting product: $e');
      return false;
    }
  }
}""",
            "language": "dart",
            "testCases": [],
            "hints": [
                {
                    "level": 1,
                    "text": "Store the Client in a final field and accept it in the constructor"
                },
                {
                    "level": 2,
                    "text": "The client has properties matching endpoint names: client.product, client.user, etc."
                },
                {
                    "level": 3,
                    "text": "Create Product objects before sending to createProduct endpoint"
                }
            ],
            "commonMistakes": [
                {
                    "mistake": "Not handling exceptions from server calls",
                    "consequence": "Unhandled exceptions crash the app",
                    "correction": "Wrap endpoint calls in try-catch and handle errors appropriately"
                }
            ],
            "difficulty": "intermediate"
        }
    ]
}


def main():
    print(f"Loading course file: {COURSE_FILE}")

    # Read the course file
    with open(COURSE_FILE, 'r', encoding='utf-8') as f:
        course = json.load(f)

    # Find module-08
    module_08 = None
    module_08_idx = None
    for idx, module in enumerate(course.get('modules', [])):
        if module.get('id') == 'module-08':
            module_08 = module
            module_08_idx = idx
            break

    if module_08 is None:
        print("ERROR: module-08 not found!")
        return 1

    print(f"Found module-08 at index {module_08_idx}")
    print(f"Current lessons: {len(module_08.get('lessons', []))}")

    # Find and replace lessons 8.3 and 8.4
    lessons = module_08.get('lessons', [])
    new_lessons = []
    replaced_83 = False
    replaced_84 = False

    for lesson in lessons:
        if lesson.get('id') == '8.3':
            new_lessons.append(LESSON_8_3)
            replaced_83 = True
            print("Replaced lesson 8.3")
        elif lesson.get('id') == '8.4':
            new_lessons.append(LESSON_8_4)
            replaced_84 = True
            print("Replaced lesson 8.4")
        else:
            new_lessons.append(lesson)

    # If lessons weren't found, we need to add them
    if not replaced_83:
        print("Lesson 8.3 not found - will add it")
        # Insert after 8.2 or at the end
        insert_idx = len(new_lessons)
        for i, lesson in enumerate(new_lessons):
            if lesson.get('id') == '8.2':
                insert_idx = i + 1
                break
        new_lessons.insert(insert_idx, LESSON_8_3)

    if not replaced_84:
        print("Lesson 8.4 not found - will add it")
        # Insert after 8.3
        insert_idx = len(new_lessons)
        for i, lesson in enumerate(new_lessons):
            if lesson.get('id') == '8.3':
                insert_idx = i + 1
                break
        new_lessons.insert(insert_idx, LESSON_8_4)

    # Update the module
    module_08['lessons'] = new_lessons
    course['modules'][module_08_idx] = module_08

    # Write back
    print(f"Writing updated course file...")
    with open(COURSE_FILE, 'w', encoding='utf-8') as f:
        json.dump(course, f, indent=2, ensure_ascii=False)

    print("Done! Lessons 8.3 and 8.4 have been updated with Serverpod content.")
    return 0


if __name__ == '__main__':
    sys.exit(main())
