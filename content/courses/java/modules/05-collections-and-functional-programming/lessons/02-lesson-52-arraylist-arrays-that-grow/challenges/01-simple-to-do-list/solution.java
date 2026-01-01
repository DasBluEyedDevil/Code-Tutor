// Solution: Simple To-Do List
// This demonstrates using ArrayList for dynamic storage

import java.util.ArrayList;

public class ToDoList {
    // ArrayList to store tasks
    ArrayList<String> tasks;
    
    // Constructor initializes the ArrayList
    public ToDoList() {
        tasks = new ArrayList<>();
    }
    
    // Add a task to the list
    public void addTask(String task) {
        tasks.add(task);
    }
    
    // Get the number of tasks
    public int getTaskCount() {
        return tasks.size();
    }
    
    // Get a task by index
    public String getTask(int index) {
        return tasks.get(index);
    }
}