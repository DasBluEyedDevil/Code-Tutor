---
type: "THEORY"
title: "💻 Complete Multi-Layer Service"
---

```java
Repository:

@Repository
public interface BookRepository extends JpaRepository<Book, Long> {
    // Spring auto-implements CRUD methods
}

Service:

@Service
public class BookService {
    private final BookRepository bookRepository;
    
    public BookService(BookRepository bookRepository) {
        this.bookRepository = bookRepository;
    }
    
    public List<Book> getAllBooks() {
        return bookRepository.findAll();
    }
    
    public Book saveBook(Book book) {
        return bookRepository.save(book);
    }
}

Controller:

@RestController
@RequestMapping("/api/books")
public class BookController {
    private final BookService bookService;
    
    public BookController(BookService bookService) {
        this.bookService = bookService;
    }
    
    @GetMapping
    public List<Book> getAll() {
        return bookService.getAllBooks();
    }
    
    @PostMapping
    public Book create(@RequestBody Book book) {
        return bookService.saveBook(book);
    }
}

The Dependency Chain:
BookController → BookService → BookRepository → Database

Spring automatically wires everything!
```