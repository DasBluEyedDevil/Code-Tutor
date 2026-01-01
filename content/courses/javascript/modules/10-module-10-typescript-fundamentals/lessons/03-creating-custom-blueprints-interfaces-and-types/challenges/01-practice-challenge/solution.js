// Book interface with optional isbn
interface Book {
  title: string;
  author: string;
  pages: number;
  isbn?: string;  // Optional property
  isRead: boolean;
}

// Book with all properties
let book1: Book = {
  title: 'The Great Gatsby',
  author: 'F. Scott Fitzgerald',
  pages: 180,
  isbn: '978-0743273565',
  isRead: true
};

// Book without optional isbn
let book2: Book = {
  title: '1984',
  author: 'George Orwell',
  pages: 328,
  isRead: false
};

console.log('Book 1:', book1.title);  // 'The Great Gatsby'
console.log('Book 2:', book2.title);  // '1984'
console.log('Book 1 ISBN:', book1.isbn); // '978-0743273565'
console.log('Book 2 ISBN:', book2.isbn); // undefined