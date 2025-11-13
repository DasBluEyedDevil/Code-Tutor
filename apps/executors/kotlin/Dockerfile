FROM openjdk:17-slim

WORKDIR /app

# Install Kotlin compiler
RUN apt-get update && \
    apt-get install -y curl unzip && \
    curl -L https://github.com/JetBrains/kotlin/releases/download/v1.9.20/kotlin-compiler-1.9.20.zip -o kotlin.zip && \
    unzip kotlin.zip && \
    rm kotlin.zip && \
    apt-get install -y maven && \
    rm -rf /var/lib/apt/lists/*

ENV PATH="/app/kotlinc/bin:${PATH}"

# Copy pom.xml and download dependencies
COPY pom.xml .
RUN mvn dependency:go-offline

# Copy source code
COPY src ./src

# Build the executor service
RUN mvn clean package -DskipTests

# Create a non-root user
RUN useradd -m -u 1000 executor && \
    mkdir -p /tmp/kotlin-execution && \
    chown -R executor:executor /app /tmp/kotlin-execution

USER executor

EXPOSE 4002

CMD ["java", "-jar", "target/kotlin-executor-1.0.0.jar"]
