---
type: "THEORY"
title: "Application Logging"
---


### Logging Levels


**When to Use Each Level**:
- **ERROR**: Exceptions, failures, critical issues
- **WARN**: Potential problems, validation failures
- **INFO**: Important business events (user signup, purchase)
- **DEBUG**: Detailed execution flow (development only)
- **TRACE**: Very detailed (rarely used)

### Structured Logging

❌ **Bad** (String concatenation):

✅ **Good** (Structured):

### Logback Configuration

**src/main/resources/logback.xml**:

---



```xml
<configuration>
    <appender name="CONSOLE" class="ch.qos.logback.core.ConsoleAppender">
        <encoder class="net.logstash.logback.encoder.LogstashEncoder">
            <includeCallerData>true</includeCallerData>
        </encoder>
    </appender>

    <appender name="FILE" class="ch.qos.logback.core.rolling.RollingFileAppender">
        <file>logs/application.log</file>
        <rollingPolicy class="ch.qos.logback.core.rolling.TimeBasedRollingPolicy">
            <fileNamePattern>logs/application-%d{yyyy-MM-dd}.log</fileNamePattern>
            <maxHistory>30</maxHistory>
        </rollingPolicy>
        <encoder class="net.logstash.logback.encoder.LogstashEncoder"/>
    </appender>

    <!-- Application logs -->
    <logger name="com.example" level="INFO"/>

    <!-- Third-party logs (less verbose) -->
    <logger name="org.jetbrains.exposed" level="WARN"/>
    <logger name="io.ktor" level="INFO"/>

    <root level="INFO">
        <appender-ref ref="CONSOLE"/>
        <appender-ref ref="FILE"/>
    </root>
</configuration>
```
