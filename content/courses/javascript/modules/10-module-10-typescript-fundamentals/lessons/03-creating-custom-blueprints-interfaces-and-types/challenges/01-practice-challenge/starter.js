// Define your Book interface here
interface Book {
  // Add properties here
}

// Create two book objects
let book1: Book = {
  title: 'The Great Gatsby',
  author: 'F. Scott Fitzgerald',
  pages: 180,
  isbn: '978-0743273565',
  isRead: true
};

let book2: Book = {
  title: '1984',
  author: 'George Orwell',
  pages: 328,
  isRead: false
};

console.log('Book 1:', book1.title);
console.log('Book 2:', book2.title);