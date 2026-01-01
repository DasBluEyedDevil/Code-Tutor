// Mock Prisma client
class PrismaClient {
  constructor() {
    this.booksData = [];
    this.nextId = 1;
    
    this.book = {
      create: async (options) => {
        let book = {
          id: this.nextId++,
          ...options.data
        };
        this.booksData.push(book);
        return book;
      },
      
      findMany: async () => {
        return this.booksData;
      },
      
      findUnique: async (options) => {
        return this.booksData.find(b => b.id === options.where.id) || null;
      },
      
      update: async (options) => {
        let book = this.booksData.find(b => b.id === options.where.id);
        if (book) {
          Object.assign(book, options.data);
          return book;
        }
        return null;
      }
    };
  }
}

let prisma = new PrismaClient();

// Test CRUD operations
async function testPrisma() {
  // Create
  let book1 = await prisma.book.create({
    data: { title: '1984', author: 'George Orwell', pages: 328 }
  });
  console.log('Created:', book1);
  
  // Find all
  let books = await prisma.book.findMany();
  console.log('All books:', books);
  
  // Find one
  let found = await prisma.book.findUnique({ where: { id: 1 } });
  console.log('Found:', found);
  
  // Update
  let updated = await prisma.book.update({
    where: { id: 1 },
    data: { pages: 330 }
  });
  console.log('Updated:', updated);
}

testPrisma();