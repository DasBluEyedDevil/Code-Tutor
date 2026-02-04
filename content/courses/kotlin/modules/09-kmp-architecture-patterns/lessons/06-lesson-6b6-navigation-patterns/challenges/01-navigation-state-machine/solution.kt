import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update

sealed interface Screen {
    data object Home : Screen
    data class Detail(val id: Int) : Screen
    data object Settings : Screen
}

data class NavigationState(
    val stack: List<Screen> = listOf(Screen.Home)
)

class NavigationRouter {
    private val _state = MutableStateFlow(NavigationState())
    val state: StateFlow<NavigationState> = _state.asStateFlow()

    val currentScreen: Screen
        get() = _state.value.stack.last()

    fun push(screen: Screen) {
        _state.update { current ->
            current.copy(stack = current.stack + screen)
        }
    }

    fun pop() {
        _state.update { current ->
            if (current.stack.size > 1) {
                current.copy(stack = current.stack.dropLast(1))
            } else {
                current // Don't pop the root screen
            }
        }
    }

    fun replaceTop(screen: Screen) {
        _state.update { current ->
            current.copy(stack = current.stack.dropLast(1) + screen)
        }
    }
}

fun main() {
    val router = NavigationRouter()
    println("current=${router.currentScreen}, stackSize=${router.state.value.stack.size}")

    router.push(Screen.Detail(42))
    println("current=${router.currentScreen}, stackSize=${router.state.value.stack.size}")

    router.pop()
    println("current=${router.currentScreen}, stackSize=${router.state.value.stack.size}")

    router.pop()
    println("current=${router.currentScreen}, stackSize=${router.state.value.stack.size}")
}
