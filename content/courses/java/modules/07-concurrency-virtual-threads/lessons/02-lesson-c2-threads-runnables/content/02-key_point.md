---
type: "KEY_POINT"
title: "Creating Threads: Two Approaches"
---

APPROACH 1: Implement Runnable (PREFERRED)

Runnable task = () -> {
    IO.println("Running in: " + Thread.currentThread().getName());
};

Thread thread = new Thread(task);
thread.start();

Why preferred:
- Separates WHAT to do (Runnable) from HOW to run it (Thread)
- Can reuse same Runnable with different executors
- Java is single-inheritance; Runnable doesn't use up your one extends

APPROACH 2: Extend Thread (RARE)

class MyThread extends Thread {
    @Override
    public void run() {
        IO.println("Running!");
    }
}

new MyThread().start();

Rarely used because:
- Uses up your single inheritance
- Couples task logic to Thread class

2025 REALITY: You rarely create Thread objects directly. Use ExecutorService or Virtual Threads instead. But understanding Thread is foundational.