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
    implementation("io.ktor:ktor-server-core-jvm:3.0.2")
    implementation("io.ktor:ktor-server-cio-jvm:3.0.2")
    implementation("io.ktor:ktor-server-content-negotiation-jvm:3.0.2")
    implementation("io.ktor:ktor-serialization-kotlinx-json-jvm:3.0.2")
    implementation("io.ktor:ktor-server-auth-jvm:3.0.2")
    implementation("io.ktor:ktor-server-auth-jwt-jvm:3.0.2")
    implementation("org.jetbrains.exposed:exposed-core:0.50.0")
    implementation("org.jetbrains.exposed:exposed-jdbc:0.50.0")
    implementation("com.h2database:h2:2.2.224")
    implementation("com.zaxxer:HikariCP:5.1.0")
    implementation("de.nycode:bcrypt:2.3.0")
    implementation("com.auth0:java-jwt:4.5.0")
    implementation("io.insert-koin:koin-ktor:4.0.3")
    implementation("io.insert-koin:koin-logger-slf4j:4.0.3")

    // Test dependencies
    testImplementation("io.ktor:ktor-server-test-host:3.0.2")
    testImplementation("org.jetbrains.kotlin:kotlin-test-junit5:2.0.0")
    testImplementation("org.junit.jupiter:junit-jupiter-api:5.10.2")
    testRuntimeOnly("org.junit.jupiter:junit-jupiter-engine:5.10.2")
    testImplementation("io.insert-koin:koin-test:4.0.3")
    testImplementation("io.insert-koin:koin-test-junit5:4.0.3")
}

tasks.withType<Test> {
    useJUnitPlatform()
}
```
