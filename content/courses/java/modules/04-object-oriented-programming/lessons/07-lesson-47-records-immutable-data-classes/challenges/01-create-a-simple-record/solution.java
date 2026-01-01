// Solution: Create a Simple Record
// Define the Book record
record Book(String title, String author, int pages) {}

void main() {
    // Create a Book record instance
    var book = new Book("1984", "Orwell", 328);
    
    // Access and print the title
    println(book.title());
}