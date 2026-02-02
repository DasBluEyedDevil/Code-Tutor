---
type: "THEORY"
title: "Dart Backend Options"
---


Dart has several options for backend development. Let's compare the three main ones:

### Shelf (Low-Level)
**What it is**: The foundational HTTP server library for Dart. Part of Dart's standard ecosystem.

**Analogy**: Like building a house with raw lumber. You have complete control, but you handle everything yourself.

**Example**:
```dart
import 'package:shelf/shelf.dart';
import 'package:shelf/shelf_io.dart' as io;

Response handleRequest(Request request) {
  return Response.ok('Hello, World!');
}

void main() async {
  var handler = const Pipeline().addHandler(handleRequest);
  var server = await io.serve(handler, 'localhost', 8080);
  print('Server running on port ${server.port}');
}
```

**When to use**: Learning how HTTP works, simple scripts, building your own framework.

---

### Dart Frog (Lightweight, File-Based Routing)
**What it is**: A minimalist backend framework by Very Good Ventures. Think of it as "Next.js for Dart."

**Analogy**: Like a kit home. Structure is provided, but you have flexibility to customize.

**Key Features**:
- File-based routing (create a file, get a route)
- Built-in hot reload for development
- Middleware support
- Easy to learn, fast to prototype

**When to use**: Prototypes, small-to-medium APIs, learning backend concepts, when you want simplicity.

---

### Serverpod (Full-Featured)
**What it is**: A complete backend framework with ORM, authentication, real-time, and more. Think of it as "Rails for Dart."

**Analogy**: Like a pre-built mansion. Everything included, but more to learn.

**Key Features**:
- Built-in PostgreSQL ORM
- Authentication system
- Real-time communication
- Automatic client code generation
- File handling, caching, logging

**When to use**: Production applications, complex backends, when you need batteries-included.

---

### Course Approach

**This course teaches Dart Frog first**, then progresses to Serverpod. Why?

1. **Dart Frog is simpler** - fewer concepts to learn upfront
2. **Core concepts transfer** - routing, middleware, handlers work similarly
3. **Appropriate complexity** - match your tool to your problem

Start simple with Dart Frog, then level up to Serverpod for production features.

