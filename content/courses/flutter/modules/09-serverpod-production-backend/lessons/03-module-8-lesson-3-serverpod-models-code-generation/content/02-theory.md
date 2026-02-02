---
type: "THEORY"
title: "Why Serverpod Models Matter"
---

**The Problem with Traditional Backend Development**

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

