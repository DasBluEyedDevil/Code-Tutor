import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update

// TODO: Define Screen sealed interface with:
//   - Home (data object)
//   - Detail(val id: Int) (data class)
//   - Settings (data object)

// TODO: Define NavigationState data class with:
//   - stack: List<Screen> (default listOf(Screen.Home))

// TODO: Implement NavigationRouter
class NavigationRouter {
    // 1. Create StateFlow<NavigationState>
    // 2. Add a 'currentScreen' property (stack.last())
    // 3. Implement push(screen: Screen) -- add to stack
    // 4. Implement pop() -- remove last if stack.size > 1
    // 5. Implement replaceTop(screen: Screen) -- replace last screen
}

fun main() {
    val router = NavigationRouter()
    println("current=${router.currentScreen}, stackSize=${router.state.value.stack.size}")

    router.push(Screen.Detail(42))
    println("current=${router.currentScreen}, stackSize=${router.state.value.stack.size}")

    router.pop()
    println("current=${router.currentScreen}, stackSize=${router.state.value.stack.size}")

    // Pop on root should be safe
    router.pop()
    println("current=${router.currentScreen}, stackSize=${router.state.value.stack.size}")
}
