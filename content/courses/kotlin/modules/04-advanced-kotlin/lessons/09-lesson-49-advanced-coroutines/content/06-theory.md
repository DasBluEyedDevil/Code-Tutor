---
type: "THEORY"
title: "Channels - Communication Between Coroutines"
---


Channels are hot streams for sending data between coroutines.

### Basic Channel


### Producer-Consumer Pattern


### Channel Buffering


### Fan-out and Fan-in


---



```kotlin
// Fan-out - multiple consumers
fun CoroutineScope.produceNumbers() = produce<Int> {
    var x = 1
    while (true) {
        send(x++)
        delay(100)
    }
}

fun CoroutineScope.consumeNumbers(id: Int, channel: ReceiveChannel<Int>) = launch {
    for (msg in channel) {
        println("Consumer $id received $msg")
    }
}

fun main() = runBlocking {
    val producer = produceNumbers()

    repeat(3) {
        consumeNumbers(it + 1, producer)
    }

    delay(1000)
    producer.cancel()
}

// Fan-in - multiple producers
suspend fun sendString(channel: SendChannel<String>, s: String, time: Long) {
    while (true) {
        delay(time)
        channel.send(s)
    }
}

fun main2() = runBlocking {
    val channel = Channel<String>()

    launch { sendString(channel, "foo", 200) }
    launch { sendString(channel, "bar", 500) }

    repeat(10) {
        println(channel.receive())
    }

    coroutineContext.cancelChildren()
}
```
