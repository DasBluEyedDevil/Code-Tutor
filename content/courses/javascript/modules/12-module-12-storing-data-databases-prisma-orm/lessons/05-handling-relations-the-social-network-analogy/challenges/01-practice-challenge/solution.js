// Complete Prisma relations simulator
let database = {
  authors: [
    { id: 1, name: 'J.K. Rowling', bio: 'British author' },
    { id: 2, name: 'George R.R. Martin', bio: 'American novelist' }
  ],
  books: [
    { id: 1, title: 'Harry Potter and the Philosopher\'s Stone', authorId: 1, published: true },
    { id: 2, title: 'Harry Potter and the Chamber of Secrets', authorId: 1, published: true },
    { id: 3, title: 'Fantastic Beasts', authorId: 1, published: true },
    { id: 4, title: 'A Game of Thrones', authorId: 2, published: true },
    { id: 5, title: 'A Clash of Kings', authorId: 2, published: true }
  ],
  categories: [
    { id: 1, name: 'Fantasy' },
    { id: 2, name: 'Adventure' },
    { id: 3, name: 'Young Adult' },
    { id: 4, name: 'Epic Fantasy' }
  ],
  // Many-to-many join table
  bookCategories: [
    { bookId: 1, categoryId: 1 },  // HP1 → Fantasy
    { bookId: 1, categoryId: 2 },  // HP1 → Adventure
    { bookId: 1, categoryId: 3 },  // HP1 → YA
    { bookId: 2, categoryId: 1 },  // HP2 → Fantasy
    { bookId: 2, categoryId: 3 },  // HP2 → YA
    { bookId: 3, categoryId: 1 },  // FB → Fantasy
    { bookId: 4, categoryId: 1 },  // GoT → Fantasy
    { bookId: 4, categoryId: 4 },  // GoT → Epic Fantasy
    { bookId: 5, categoryId: 1 },  // Clash → Fantasy
    { bookId: 5, categoryId: 4 }   // Clash → Epic Fantasy
  ]
};

// Prisma-style queries

function findAuthorWithBooks(authorId) {
  let author = database.authors.find(a => a.id === authorId);
  if (!author) return null;
  
  // One-to-many: get all books by this author
  let books = database.books.filter(b => b.authorId === authorId);
  
  // Include categories for each book
  books = books.map(book => {
    let categoryIds = database.bookCategories
      .filter(bc => bc.bookId === book.id)
      .map(bc => bc.categoryId);
    
    let categories = database.categories.filter(c => 
      categoryIds.includes(c.id)
    );
    
    return { ...book, categories };
  });
  
  return { ...author, books };
}

function findBookWithRelations(bookId) {
  let book = database.books.find(b => b.id === bookId);
  if (!book) return null;
  
  // Get author (many-to-one)
  let author = database.authors.find(a => a.id === book.authorId);
  
  // Get categories (many-to-many)
  let categoryIds = database.bookCategories
    .filter(bc => bc.bookId === bookId)
    .map(bc => bc.categoryId);
  
  let categories = database.categories.filter(c => 
    categoryIds.includes(c.id)
  );
  
  return { ...book, author, categories };
}

function findCategoryWithBooks(categoryId) {
  let category = database.categories.find(c => c.id === categoryId);
  if (!category) return null;
  
  // Many-to-many: get all books in this category
  let bookIds = database.bookCategories
    .filter(bc => bc.categoryId === categoryId)
    .map(bc => bc.bookId);
  
  let books = database.books.filter(b => bookIds.includes(b.id));
  
  // Include authors
  books = books.map(book => {
    let author = database.authors.find(a => a.id === book.authorId);
    return { ...book, author };
  });
  
  return { ...category, books };
}

function getStats() {
  return {
    totalAuthors: database.authors.length,
    totalBooks: database.books.length,
    totalCategories: database.categories.length,
    averageBooksPerAuthor: (database.books.length / database.authors.length).toFixed(1)
  };
}

// Test the relations
console.log('=== Prisma Relations Simulator ===\n');

console.log('1. ONE-TO-MANY: Author with Books');
console.log('Query: prisma.author.findUnique({ where: { id: 1 }, include: { books: true } })\n');
let jkRowling = findAuthorWithBooks(1);
console.log(JSON.stringify(jkRowling, null, 2));

console.log('\n2. MANY-TO-MANY: Book with Categories');
console.log('Query: prisma.book.findUnique({ where: { id: 1 }, include: { categories: true } })\n');
let hp1 = findBookWithRelations(1);
console.log(JSON.stringify(hp1, null, 2));

console.log('\n3. MANY-TO-MANY REVERSE: Category with Books');
console.log('Query: prisma.category.findUnique({ where: { id: 1 }, include: { books: true } })\n');
let fantasy = findCategoryWithBooks(1);
console.log(JSON.stringify(fantasy, null, 2));

console.log('\n4. Database Statistics');
console.log(getStats());