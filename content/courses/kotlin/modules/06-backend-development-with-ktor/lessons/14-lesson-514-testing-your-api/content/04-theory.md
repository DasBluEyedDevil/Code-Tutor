---
type: "THEORY"
title: "Setting Up Testing"
---


### Step 1: Add Test Dependencies

Update your `build.gradle.kts`:


---



```kotlin
dependencies {
    // Production dependencies
    implementation("io.ktor:ktor-server-core-jvm:3.4.0")
    implementation("io.ktor:ktor-server-cio-jvm:3.4.0")
    implementation("io.ktor:ktor-server-content-negotiation-jvm:3.4.0")
    implementation("io.ktor:ktor-serialization-kotlinx-json-jvm:3.4.0")
    implementation("io.ktor:ktor-server-auth-jvm:3.4.0")
    implementation("io.ktor:ktor-server-auth-jwt-jvm:3.4.0")
    implementation("org.jetbrains.exposed:exposed-core:1.0.0")
    implementation("org.jetbrains.exposed:exposed-jdbc:1.0.0")
    implementation("com.h2database:h2:2.2.224")
    implementation("com.zaxxer:HikariCP:5.1.0")
    implementation("de.nycode:bcrypt:2.3.0")
    implementation("com.auth0:java-jwt:4.5.0")
    implementation("io.insert-koin:koin-ktor:4.1.1")
    implementation("io.insert-koin:koin-logger-slf4j:4.1.1")

    // Test dependencies
    testImplementation("io.ktor:ktor-server-test-host:3.4.0")
    testImplementation("org.jetbrains.kotlin:kotlin-test-junit5:2.3.0")
    testImplementation("org.junit.jupiter:junit-jupiter-api:5.10.2")
    testRuntimeOnly("org.junit.jupiter:junit-jupiter-engine:5.10.2")
    testImplementation("io.insert-koin:koin-test:4.1.1")
    testImplementation("io.insert-koin:koin-test-junit5:4.1.1")
}

tasks.withType<Test> {
    useJUnitPlatform()
}
```
