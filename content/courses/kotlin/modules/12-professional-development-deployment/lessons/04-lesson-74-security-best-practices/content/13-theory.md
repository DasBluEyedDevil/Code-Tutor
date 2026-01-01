---
type: "THEORY"
title: "Solution 2"
---



---



```kotlin
class RateLimitPlugin(private val config: Configuration) {
    class Configuration {
        var maxRequests: Int = 100
        var windowMs: Long = 60000 // 1 minute
        var keyExtractor: (ApplicationCall) -> String = { call ->
            call.request.origin.remoteHost
        }
    }

    companion object Feature : ApplicationPlugin<Application, Configuration, RateLimitPlugin> {
        override val key = AttributeKey<RateLimitPlugin>("RateLimit")

        // ConcurrentHashMap is correct for thread-safe server-side state
        private val rateLimitData = ConcurrentHashMap<String, RateLimitInfo>()

        override fun install(
            pipeline: Application,
            configure: Configuration.() -> Unit
        ): RateLimitPlugin {
            val config = Configuration().apply(configure)
            val plugin = RateLimitPlugin(config)

            pipeline.intercept(ApplicationCallPipeline.Plugins) {
                val key = config.keyExtractor(call)
                val now = System.currentTimeMillis()

                val info = rateLimitData.getOrPut(key) {
                    RateLimitInfo(mutableListOf(), now)
                }

                synchronized(info) {
                    // Clean old requests
                    info.requests.removeIf { it < now - config.windowMs }

                    if (info.requests.size >= config.maxRequests) {
                        call.response.headers.append("X-RateLimit-Limit", config.maxRequests.toString())
                        call.response.headers.append("X-RateLimit-Remaining", "0")
                        call.response.headers.append("Retry-After", "60")

                        call.respond(HttpStatusCode.TooManyRequests, mapOf(
                            "error" to "Rate limit exceeded",
                            "limit" to config.maxRequests,
                            "window" to "${config.windowMs / 1000}s"
                        ))
                        finish()
                        return@intercept
                    }

                    info.requests.add(now)

                    call.response.headers.append("X-RateLimit-Limit", config.maxRequests.toString())
                    call.response.headers.append(
                        "X-RateLimit-Remaining",
                        (config.maxRequests - info.requests.size).toString()
                    )
                }
            }

            return plugin
        }
    }

    private data class RateLimitInfo(
        val requests: MutableList<Long>,
        val windowStart: Long
    )
}

// Usage
fun Application.module() {
    install(RateLimitPlugin) {
        maxRequests = 100
        windowMs = 60000 // 1 minute

        keyExtractor = { call ->
            // Use authenticated user ID if available, else IP
            call.principal<UserPrincipal>()?.id
                ?: call.request.origin.remoteHost
        }
    }

    routing {
        get("/api/data") {
            call.respond("Hello!")
        }
    }
}
```
