// Solution: Simple Task Queue
// This demonstrates using LinkedList as a FIFO queue

import java.util.LinkedList;

public class TaskQueue {
    // LinkedList for efficient add/remove at ends
    LinkedList<String> tasks;
    
    // Constructor initializes the LinkedList
    public TaskQueue() {
        tasks = new LinkedList<>();
    }
    
    // Enqueue: add to end of queue
    public void enqueue(String task) {
        tasks.addLast(task);
    }
    
    // Dequeue: remove and return first task
    public String dequeue() {
        // Return null if queue is empty
        if (tasks.isEmpty()) {
            return null;
        }
        return tasks.removeFirst();
    }
    
    // Get current queue size
    public int size() {
        return tasks.size();
    }
}