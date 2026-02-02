// Simulated Prisma relations
let database = {
  authors: [
    {
      id: 1,
      name: 'J.K. Rowling',
      books: []  // Will populate
    }
  ],
  books: [
    {
      id: 1,
      title: 'Harry Potter',
      authorId: 1,
      categories: []  // Will populate
    },
    {
      id: 2,
      title: 'Fantastic Beasts',
      authorId: 1,
      categories: []
    }
  ],
  categories: [
    { id: 1, name: 'Fantasy', books: [] },
    { id: 2, name: 'Adventure', books: [] }
  ]
};

// Function to get author with books
function getAuthorWithBooks(authorId) {
  let author = database.authors.find(a => a.id === authorId);
  if (!author) return null;
  
  // Find books by this author (one-to-many)
  let books = database.books.filter(b => b.authorId === authorId);
  
  return {
    ...author,
    books: books
  };
}

// Function to get book with categories
function getBookWithCategories(bookId) {
  let book = database.books.find(b => b.id === bookId);
  if (!book) return null;
  
  // Many-to-many: find categories for this book
  let categories = database.categories.filter(c => 
    c.books.includes(bookId)
  );
  
  return {
    ...book,
    categories: categories
  };
}

// Set up many-to-many relations
database.categories[0].books = [1, 2];  // Fantasy has both books
database.categories[1].books = [1];     // Adventure has HP only

database.books[0].categories = [1, 2];  // HP is Fantasy + Adventure
database.books[1].categories = [1];     // FB is Fantasy only

// Test relations
console.log('=== One-to-Many: Author → Books ===');
let authorWithBooks = getAuthorWithBooks(1);
console.log(JSON.stringify(authorWithBooks, null, 2));

console.log('\n=== Many-to-Many: Book ↔ Categories ===');
let bookWithCategories = getBookWithCategories(1);
console.log(JSON.stringify(bookWithCategories, null, 2));