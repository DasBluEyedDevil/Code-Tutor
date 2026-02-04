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

class ConfigBuilder {
    private var defaults = PluginConfig()
    private var projectOverrides = PluginConfigOverride()
    private var cliOverrides = PluginConfigOverride()

    fun withDefaults(defaults: PluginConfig): ConfigBuilder = apply {
        this.defaults = defaults
    }

    fun withProjectOverrides(overrides: PluginConfigOverride): ConfigBuilder = apply {
        this.projectOverrides = overrides
    }

    fun withCliOverrides(overrides: PluginConfigOverride): ConfigBuilder = apply {
        this.cliOverrides = overrides
    }

    fun build(): PluginConfig {
        // Layer 1: Start with defaults
        // Layer 2: Apply project overrides
        val afterProject = PluginConfig(
            jvmTarget = projectOverrides.jvmTarget ?: defaults.jvmTarget,
            allWarningsAsErrors = projectOverrides.allWarningsAsErrors ?: defaults.allWarningsAsErrors,
            optIn = (defaults.optIn + (projectOverrides.optIn ?: emptyList())).distinct()
        )

        // Layer 3: Apply CLI overrides
        return PluginConfig(
            jvmTarget = cliOverrides.jvmTarget ?: afterProject.jvmTarget,
            allWarningsAsErrors = cliOverrides.allWarningsAsErrors ?: afterProject.allWarningsAsErrors,
            optIn = (afterProject.optIn + (cliOverrides.optIn ?: emptyList())).distinct()
        )
    }
}

fun main() {
    val config1 = ConfigBuilder()
        .withDefaults(PluginConfig())
        .build()
    println("jvmTarget=${config1.jvmTarget}, allWarningsAsErrors=${config1.allWarningsAsErrors}, optIn=${config1.optIn}")

    val config2 = ConfigBuilder()
        .withDefaults(PluginConfig())
        .withProjectOverrides(PluginConfigOverride(
            jvmTarget = 21,
            allWarningsAsErrors = true,
            optIn = listOf("kotlinx.coroutines.ExperimentalCoroutinesApi")
        ))
        .build()
    println("jvmTarget=${config2.jvmTarget}, allWarningsAsErrors=${config2.allWarningsAsErrors}, optIn=${config2.optIn}")

    val config3 = ConfigBuilder()
        .withDefaults(PluginConfig())
        .withProjectOverrides(PluginConfigOverride(jvmTarget = 21, allWarningsAsErrors = true))
        .withCliOverrides(PluginConfigOverride(allWarningsAsErrors = false))
        .build()
    println("jvmTarget=${config3.jvmTarget}, allWarningsAsErrors=${config3.allWarningsAsErrors}")
}
