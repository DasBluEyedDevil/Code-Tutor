class SearchViewModel(
    private val repository: NoteRepository,
    private val dispatcher: CoroutineDispatcher = Dispatchers.Default
) {
    private val scope = CoroutineScope(dispatcher + SupervisorJob())
    
    private val _query = MutableStateFlow("")
    
    private val _results = MutableStateFlow<List<Note>>(emptyList())
    val results: StateFlow<List<Note>> = _results.asStateFlow()
    
    init {
        scope.launch {
            _query
                .debounce(300)  // Wait 300ms after last input
                .distinctUntilChanged()
                .collectLatest { query ->
                    _results.value = repository.search(query)
                }
        }
    }
    
    fun search(query: String) {
        _query.value = query
    }
}

// TODO: Write tests that verify:
// 1. Debounce prevents rapid searches (typing fast doesn't trigger search for each keystroke)
// 2. Search executes after debounce period
// 3. New search cancels previous (collectLatest behavior)