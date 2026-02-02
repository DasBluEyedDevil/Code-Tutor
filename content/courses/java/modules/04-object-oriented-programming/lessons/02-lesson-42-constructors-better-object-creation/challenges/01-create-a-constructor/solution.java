// Solution: Create a Constructor
// This demonstrates using constructors to initialize objects

// Book class with constructor
class Book {
    String title;
    String author;
    int pages;
    
    // Constructor takes three parameters
    // Uses 'this' to distinguish field from parameter
    public Book(String title, String author, int pages) {
        this.title = title;
        this.author = author;
        this.pages = pages;
    }
}

public class Solution {
    public static void main(String[] args) {
        // Create a book using the constructor
        Book book = new Book("1984", "Orwell", 328);
        
        // Print the formatted output
        IO.println(book.title + " by " + book.author);
    }
}