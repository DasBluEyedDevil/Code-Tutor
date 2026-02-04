---
type: "EXAMPLE"
title: "Catch for Exception Conversion"
---


Converting exceptions to typed errors:



```kotlin
import arrow.core.raise.*
import java.io.IOException
import java.net.SocketTimeoutException

sealed interface FetchError {
    data class Network(val message: String) : FetchError
    data object Timeout : FetchError
    data class Parse(val body: String) : FetchError
}

context(raise: Raise<FetchError>)
suspend fun fetchData(url: String): Data {
    // catch converts exceptions to typed errors
    val response = catch(
        block = { httpClient.get(url) },
        catch = { e ->
            when (e) {
                is SocketTimeoutException -> raise.raise(FetchError.Timeout)
                is IOException -> raise.raise(FetchError.Network(e.message ?: "Unknown"))
                else -> throw e  // Re-throw unexpected exceptions
            }
        }
    )

    // Another catch for parsing
    return catch(
        block = { json.decodeFromString<Data>(response.body) },
        catch = { raise.raise(FetchError.Parse(response.body)) }
    )
}

// Simpler version with mapError
context(raise: Raise<FetchError>)
suspend fun fetchDataSimple(url: String): Data {
    val response = Either.catch { httpClient.get(url) }
        .mapLeft { FetchError.Network(it.message ?: "Unknown") }
        .bind()

    return Either.catch { json.decodeFromString<Data>(response.body) }
        .mapLeft { FetchError.Parse(response.body) }
        .bind()
}
```
