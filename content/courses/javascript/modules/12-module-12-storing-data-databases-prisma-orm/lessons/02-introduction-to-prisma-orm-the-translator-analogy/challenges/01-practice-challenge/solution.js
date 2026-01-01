// Complete Prisma-style mock client
class PrismaClient {
  constructor() {
    this.booksData = [];
    this.nextId = 1;
    
    this.book = {
      create: async (options) => {
        let book = {
          id: this.nextId++,
          ...options.data,
          createdAt: new Date()
        };
        this.booksData.push(book);
        console.log(`✓ Created book ID ${book.id}`);
        return book;
      },
      
      findMany: async (options = {}) => {
        let books = this.booksData;
        
        // Filter if where clause provided
        if (options.where) {
          books = books.filter(book => {
            return Object.entries(options.where).every(([key, value]) => {
              return book[key] === value;
            });
          });
        }
        
        return books;
      },
      
      findUnique: async (options) => {
        let book = this.booksData.find(b => b.id === options.where.id);
        return book || null;
      },
      
      update: async (options) => {
        let book = this.booksData.find(b => b.id === options.where.id);
        if (book) {
          Object.assign(book, options.data);
          book.updatedAt = new Date();
          console.log(`✓ Updated book ID ${book.id}`);
          return book;
        }
        throw new Error(`Book with ID ${options.where.id} not found`);
      },
      
      delete: async (options) => {
        let index = this.booksData.findIndex(b => b.id === options.where.id);
        if (index !== -1) {
          let deleted = this.booksData.splice(index, 1)[0];
          console.log(`✓ Deleted book ID ${deleted.id}`);
          return deleted;
        }
        throw new Error(`Book with ID ${options.where.id} not found`);
      },
      
      count: async () => {
        return this.booksData.length;
      }
    };
  }
}

let prisma = new PrismaClient();

// Comprehensive test
async function testPrisma() {
  console.log('=== Prisma-Style ORM Demo ===\n');
  
  // Create books
  await prisma.book.create({
    data: { title: '1984', author: 'George Orwell', pages: 328 }
  });
  
  await prisma.book.create({
    data: { title: 'The Great Gatsby', author: 'F. Scott Fitzgerald', pages: 180 }
  });
  
  // Find all
  let allBooks = await prisma.book.findMany();
  console.log('\nAll books:', allBooks.length);
  
  // Find specific
  let book = await prisma.book.findUnique({ where: { id: 1 } });
  console.log('\nFound book:', book.title);
  
  // Update
  await prisma.book.update({
    where: { id: 1 },
    data: { pages: 330 }
  });
  
  // Count
  let count = await prisma.book.count();
  console.log('\nTotal books:', count);
  
  // Delete
  await prisma.book.delete({ where: { id: 2 } });
  
  // Final state
  let remaining = await prisma.book.findMany();
  console.log('\nRemaining books:', remaining);
}

testPrisma();