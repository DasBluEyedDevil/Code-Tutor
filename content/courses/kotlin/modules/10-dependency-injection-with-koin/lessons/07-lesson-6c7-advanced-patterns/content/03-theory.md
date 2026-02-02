---
type: "THEORY"
title: "Feature Flags with Dynamic Modules"
---

Load modules conditionally based on feature flags:

```kotlin
// Feature implementations
val newCheckoutModule = module {
    single<CheckoutFlow> { NewCheckoutFlowImpl(get()) }
    viewModel { NewCheckoutViewModel(get()) }
}

val legacyCheckoutModule = module {
    single<CheckoutFlow> { LegacyCheckoutFlowImpl(get()) }
    viewModel { LegacyCheckoutViewModel(get()) }
}

// Dynamic loading
class FeatureManager(private val featureFlags: FeatureFlags) {
    
    fun initializeFeatures() {
        val modules = buildList {
            // Always load core
            add(coreModule)
            
            // Conditional features
            if (featureFlags.isNewCheckoutEnabled) {
                add(newCheckoutModule)
            } else {
                add(legacyCheckoutModule)
            }
            
            if (featureFlags.isAnalyticsEnabled) {
                add(analyticsModule)
            }
        }
        
        loadKoinModules(modules)
    }
    
    fun toggleFeature(feature: Feature, enabled: Boolean) {
        when (feature) {
            Feature.NEW_CHECKOUT -> {
                if (enabled) {
                    unloadKoinModules(legacyCheckoutModule)
                    loadKoinModules(newCheckoutModule)
                } else {
                    unloadKoinModules(newCheckoutModule)
                    loadKoinModules(legacyCheckoutModule)
                }
            }
        }
    }
}
```