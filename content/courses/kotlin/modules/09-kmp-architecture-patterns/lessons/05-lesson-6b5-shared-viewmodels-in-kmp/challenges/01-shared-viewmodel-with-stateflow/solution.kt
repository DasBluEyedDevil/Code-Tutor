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

class BookmarkViewModel {
    private val _state = MutableStateFlow(BookmarkState())
    val state: StateFlow<BookmarkState> = _state.asStateFlow()

    fun addBookmark(id: Int, title: String) {
        _state.update { current ->
            val newBookmark = Bookmark(id = id, title = title)
            current.copy(
                bookmarks = current.bookmarks + newBookmark,
                count = current.count + 1
            )
        }
    }

    fun removeBookmark(id: Int) {
        _state.update { current ->
            val filtered = current.bookmarks.filter { it.id != id }
            current.copy(
                bookmarks = filtered,
                count = filtered.size
            )
        }
    }

    fun toggleStarred(id: Int) {
        _state.update { current ->
            current.copy(
                bookmarks = current.bookmarks.map { bookmark ->
                    if (bookmark.id == id) bookmark.copy(isStarred = !bookmark.isStarred)
                    else bookmark
                }
            )
        }
    }
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
