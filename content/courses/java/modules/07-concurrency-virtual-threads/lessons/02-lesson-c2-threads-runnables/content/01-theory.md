---
type: "THEORY"
title: "What is a Thread?"
---

A thread is an independent path of execution within a program.

Every Java program starts with ONE thread: the main thread.

void main() {
    System.out.println("Hello");  // Runs on main thread
    doSomething();                 // Still main thread
}

But you can create ADDITIONAL threads:

// Main thread creates a new thread
Thread worker = new Thread(() -> {
    System.out.println("I'm on a different thread!");
});
worker.start();  // Now TWO threads running!

Each thread has its own:
- Call stack (method calls, local variables)
- Program counter (current instruction)

Threads SHARE:
- Heap memory (objects, arrays)
- Static variables
- File handles, network connections

This sharing is powerful but dangerous - it's where bugs come from.