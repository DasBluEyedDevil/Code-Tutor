import org.koin.dsl.module
import org.koin.core.module.dsl.viewModel
import org.koin.core.module.dsl.singleOf
import org.koin.core.module.dsl.bind

val bookModule = module {
    // 1. Database as singleton
    single { AppDatabase() }
    
    // 2. Queries from database
    single { get<AppDatabase>().booksQueries() }
    
    // 3. Repository implementation bound to interface
    singleOf(::BookRepositoryImpl) { bind<BookRepository>() }
    // Alternative syntax:
    // single<BookRepository> { BookRepositoryImpl(get()) }
    
    // 4. BookListViewModel
    viewModel { BookListViewModel(get()) }
    
    // 5. BookDetailViewModel with bookId parameter
    viewModel { parameters ->
        BookDetailViewModel(
            bookId = parameters.get(),
            repository = get()
        )
    }
}

// Usage example:
// val listViewModel = koinViewModel<BookListViewModel>()
// val detailViewModel = koinViewModel<BookDetailViewModel> { parametersOf("book-123") }