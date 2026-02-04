data class PluginConfig(
    val jvmTarget: Int = 17,
    val allWarningsAsErrors: Boolean = false,
    val optIn: List<String> = emptyList()
)

data class PluginConfigOverride(
    val jvmTarget: Int? = null,
    val allWarningsAsErrors: Boolean? = null,
    val optIn: List<String>? = null
)

// TODO: Implement ConfigBuilder
class ConfigBuilder {
    // 1. Start with PluginConfig defaults
    // 2. Apply project-level overrides (PluginConfigOverride)
    // 3. Apply CLI-level overrides (PluginConfigOverride)
    // 4. merge(): combine layers into final PluginConfig
    //    - Non-null override values replace defaults
    //    - List fields should be merged (defaults + overrides, deduplicated)

    fun withDefaults(defaults: PluginConfig): ConfigBuilder = TODO()
    fun withProjectOverrides(overrides: PluginConfigOverride): ConfigBuilder = TODO()
    fun withCliOverrides(overrides: PluginConfigOverride): ConfigBuilder = TODO()
    fun build(): PluginConfig = TODO()
}

fun main() {
    // No overrides -- defaults
    val config1 = ConfigBuilder()
        .withDefaults(PluginConfig())
        .build()
    println("jvmTarget=${config1.jvmTarget}, allWarningsAsErrors=${config1.allWarningsAsErrors}, optIn=${config1.optIn}")

    // Project overrides
    val config2 = ConfigBuilder()
        .withDefaults(PluginConfig())
        .withProjectOverrides(PluginConfigOverride(
            jvmTarget = 21,
            allWarningsAsErrors = true,
            optIn = listOf("kotlinx.coroutines.ExperimentalCoroutinesApi")
        ))
        .build()
    println("jvmTarget=${config2.jvmTarget}, allWarningsAsErrors=${config2.allWarningsAsErrors}, optIn=${config2.optIn}")

    // CLI overrides on top of project
    val config3 = ConfigBuilder()
        .withDefaults(PluginConfig())
        .withProjectOverrides(PluginConfigOverride(jvmTarget = 21, allWarningsAsErrors = true))
        .withCliOverrides(PluginConfigOverride(allWarningsAsErrors = false))
        .build()
    println("jvmTarget=${config3.jvmTarget}, allWarningsAsErrors=${config3.allWarningsAsErrors}")
}
