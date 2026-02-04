import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update

data class Bookmark(
    val id: Int,
    val title: String,
    val isStarred: Boolean = false
)

data class BookmarkState(
    val bookmarks: List<Bookmark> = emptyList(),
    val count: Int = 0
)

// TODO: Implement BookmarkViewModel -- pure Kotlin, no platform imports
class BookmarkViewModel {
    // 1. Create MutableStateFlow<BookmarkState> as private _state
    // 2. Expose as StateFlow<BookmarkState> via asStateFlow()
    // 3. Implement addBookmark(id: Int, title: String)
    // 4. Implement removeBookmark(id: Int)
    // 5. Implement toggleStarred(id: Int)
}

fun main() {
    val vm = BookmarkViewModel()

    vm.addBookmark(1, "Kotlin Docs")
    println(vm.state.value)

    vm.toggleStarred(1)
    println(vm.state.value.bookmarks.first())

    vm.removeBookmark(1)
    println(vm.state.value)
}
