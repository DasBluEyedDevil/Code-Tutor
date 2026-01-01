---
type: "THEORY"
title: "Extending the Error Class"
---

Custom errors allow you to group related errors and provide extra metadata that the standard `Error` class doesn't support.

### 1. The `extends Error` Syntax
To create a custom error, you create a class that inherits from the built-in `Error`. This ensures that your custom error still has all the standard features, like a `.stack` trace.

### 2. The `super(message)` Call
Inside your custom constructor, the very first thing you must do is call `super(message)`. This passes the message to the parent `Error` class so it can set up the basic object correctly.

### 3. Overriding `.name`
By default, every error has the name `"Error"`. To make yours unique (and to make it show up correctly in the console), you should set `this.name` inside your constructor.

### 4. Custom Properties
This is the real power of custom errors. You can add any properties you want:
*   `statusCode`: For API errors.
*   `field`: For validation errors.
*   `isRetryable`: For transient errors like network timeouts.

### 5. Benefits of Custom Classes
*   **Cleaner Catch Blocks:** Use `instanceof` to separate "User Errors" (which you might show to the user) from "System Errors" (which you only log internally).
*   **Consistency:** Every part of your app will handle a `DatabaseError` the same way.
*   **Searchability:** It's much easier to search your logs for `OrderProcessingError` than for a generic string like "failed to process."
