---
type: "THEORY"
title: "The Problem: One Thing at a Time"
---

Imagine a restaurant with ONE chef who must:
1. Take orders at the counter
2. Cook each dish
3. Serve the food
4. Clean tables

While cooking, customers wait. While cleaning, food gets cold. The chef can only do ONE thing at a time.

This is how single-threaded programs work:

void handleRequest() {
    fetchDataFromDatabase();  // 200ms waiting
    callExternalAPI();        // 500ms waiting
    processResults();         // 10ms actual work
}

The program WAITS 700ms but only WORKS for 10ms. That's 98.6% waiting!

Now imagine 100 users making requests. With single-threaded processing, user #100 waits for 99 others to finish. Unacceptable.