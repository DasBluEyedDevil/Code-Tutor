---
type: "THEORY"
title: "Analytics"
---


### Firebase Analytics (Android)

**Track Events**:

**User Properties**:

### Mixpanel (Cross-Platform)


**Initialize and Track**:

### Backend Analytics

**Custom Analytics Service**:

---



```kotlin
import kotlinx.coroutines.launch
import kotlinx.coroutines.CoroutineScope

class AnalyticsService(
    private val database: Database,
    private val scope: CoroutineScope
) {
    suspend fun trackEvent(
        userId: String?,
        event: String,
        properties: Map<String, Any> = emptyMap()
    ) {
        scope.launch {
            try {
                database.transaction {
                    AnalyticsEvents.insert {
                        it[AnalyticsEvents.userId] = userId
                        it[AnalyticsEvents.event] = event
                        it[AnalyticsEvents.properties] = Json.encodeToString(properties)
                        it[AnalyticsEvents.timestamp] = System.currentTimeMillis()
                        it[AnalyticsEvents.ipAddress] = getCurrentIpAddress()
                        it[AnalyticsEvents.userAgent] = getCurrentUserAgent()
                    }
                }
            } catch (e: Exception) {
                logger.error("Failed to track event", e)
            }
        }
    }

    suspend fun getDailyActiveUsers(days: Int = 30): List<DailyStats> {
        return database.transaction {
            AnalyticsEvents
                .select { AnalyticsEvents.timestamp greaterEq (System.currentTimeMillis() - days * 24 * 3600000) }
                .groupBy { it[AnalyticsEvents.timestamp] / (24 * 3600000) }
                .map { (day, events) ->
                    DailyStats(
                        date = Date(day * 24 * 3600000),
                        activeUsers = events.map { it[AnalyticsEvents.userId] }.toSet().size,
                        eventCount = events.size
                    )
                }
        }
    }

    suspend fun getPopularFeatures(limit: Int = 10): List<FeatureStats> {
        return database.transaction {
            AnalyticsEvents
                .select { AnalyticsEvents.event eq "feature_used" }
                .groupBy { Json.decodeFromString<Map<String, String>>(it[AnalyticsEvents.properties])["feature_name"] }
                .map { (feature, events) ->
                    FeatureStats(
                        feature = feature ?: "unknown",
                        usageCount = events.size,
                        uniqueUsers = events.map { it[AnalyticsEvents.userId] }.toSet().size
                    )
                }
                .sortedByDescending { it.usageCount }
                .take(limit)
        }
    }
}
```
