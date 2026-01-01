---
type: "THEORY"
title: "Solution 3"
---



---



```kotlin
// API endpoints for analytics
fun Route.analyticsRoutes(analyticsService: AnalyticsService) {
    authenticate("jwt") {
        get("/api/analytics/daily-active-users") {
            val days = call.parameters["days"]?.toIntOrNull() ?: 30
            val stats = analyticsService.getDailyActiveUsers(days)
            call.respond(stats)
        }

        get("/api/analytics/popular-features") {
            val limit = call.parameters["limit"]?.toIntOrNull() ?: 10
            val features = analyticsService.getPopularFeatures(limit)
            call.respond(features)
        }

        get("/api/analytics/user-retention") {
            val cohortDate = call.parameters["cohort"]
                ?: throw BadRequestException("Cohort date required")

            val retention = analyticsService.getUserRetention(cohortDate)
            call.respond(retention)
        }

        get("/api/analytics/conversion-funnel") {
            val funnel = analyticsService.getConversionFunnel()
            call.respond(funnel)
        }
    }
}

// Analytics queries
class AnalyticsService(private val database: Database) {
    suspend fun getConversionFunnel(): ConversionFunnel {
        return database.transaction {
            val signups = AnalyticsEvents
                .select { AnalyticsEvents.event eq "sign_up" }
                .count()

            val productViews = AnalyticsEvents
                .select { AnalyticsEvents.event eq "view_product" }
                .groupBy { it[AnalyticsEvents.userId] }
                .count()

            val addedToCart = AnalyticsEvents
                .select { AnalyticsEvents.event eq "add_to_cart" }
                .groupBy { it[AnalyticsEvents.userId] }
                .count()

            val purchases = AnalyticsEvents
                .select { AnalyticsEvents.event eq "purchase" }
                .groupBy { it[AnalyticsEvents.userId] }
                .count()

            ConversionFunnel(
                steps = listOf(
                    FunnelStep("Sign Up", signups, 100.0),
                    FunnelStep("View Product", productViews, (productViews.toDouble() / signups * 100)),
                    FunnelStep("Add to Cart", addedToCart, (addedToCart.toDouble() / signups * 100)),
                    FunnelStep("Purchase", purchases, (purchases.toDouble() / signups * 100))
                ),
                conversionRate = (purchases.toDouble() / signups * 100)
            )
        }
    }

    suspend fun getUserRetention(cohortDate: String): RetentionStats {
        // Implementation for cohort analysis
        // Returns percentage of users still active after N days
        return database.transaction {
            // Complex query...
            RetentionStats(/*...*/)
        }
    }
}

data class ConversionFunnel(
    val steps: List<FunnelStep>,
    val conversionRate: Double
)

data class FunnelStep(
    val name: String,
    val count: Long,
    val percentageOfTotal: Double
)
```
