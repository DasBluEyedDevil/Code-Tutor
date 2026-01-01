---
type: "THEORY"
title: "Alerting"
---


### Alert Configuration (Example: PagerDuty)


### Health Check Endpoint


---



```kotlin
fun Route.healthCheck(
    database: Database,
    redis: RedisClient
) {
    get("/health") {
        val status = mutableMapOf<String, Any>()

        // Check database
        val dbHealthy = try {
            database.transaction {
                exec("SELECT 1") { }
                true
            }
        } catch (e: Exception) {
            status["database_error"] = e.message ?: "Unknown"
            false
        }

        // Check Redis
        val redisHealthy = try {
            redis.ping()
            true
        } catch (e: Exception) {
            status["redis_error"] = e.message ?: "Unknown"
            false
        }

        status["database"] = if (dbHealthy) "healthy" else "unhealthy"
        status["redis"] = if (redisHealthy) "healthy" else "unhealthy"
        status["status"] = if (dbHealthy && redisHealthy) "healthy" else "unhealthy"
        status["timestamp"] = System.currentTimeMillis()

        val statusCode = if (dbHealthy && redisHealthy) {
            HttpStatusCode.OK
        } else {
            HttpStatusCode.ServiceUnavailable
        }

        call.respond(statusCode, status)
    }
}
```
