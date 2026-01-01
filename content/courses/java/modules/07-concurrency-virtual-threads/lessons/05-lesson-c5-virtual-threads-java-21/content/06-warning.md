---
type: "WARNING"
title: "Virtual Thread Pitfalls"
---

PINNING: Virtual threads can get 'pinned' to carrier threads:

1. synchronized blocks/methods:
   synchronized (lock) {
       Thread.sleep(1000);  // PINNED - blocks carrier!
   }
   
   FIX: Use ReentrantLock instead:
   lock.lock();
   try {
       Thread.sleep(1000);  // Not pinned
   } finally {
       lock.unlock();
   }

2. Native methods/JNI:
   Can't be unmounted during native code execution

DETECTING PINNING:
java -Djdk.tracePinnedThreads=full MyApp

DON'T POOL VIRTUAL THREADS:
// WRONG! Virtual threads are cheap, don't pool them
ExecutorService pool = Executors.newFixedThreadPool(100);
pool.submit(virtualThread);  // Defeats the purpose

// RIGHT! One virtual thread per task
var executor = Executors.newVirtualThreadPerTaskExecutor();

DON'T USE FOR CPU-BOUND WORK:
Virtual threads shine for I/O-bound tasks. For CPU-intensive work, platform threads with parallelStream() or ForkJoinPool are still appropriate.