FROM openjdk:17-slim

WORKDIR /app

# Install Maven for building the executor service
RUN apt-get update && \
    apt-get install -y maven && \
    rm -rf /var/lib/apt/lists/*

# Copy pom.xml and download dependencies
COPY pom.xml .
RUN mvn dependency:go-offline

# Copy source code
COPY src ./src

# Build the executor service
RUN mvn clean package -DskipTests

# Create a non-root user
RUN useradd -m -u 1000 executor && \
    mkdir -p /tmp/java-execution && \
    chown -R executor:executor /app /tmp/java-execution

USER executor

EXPOSE 4001

CMD ["java", "-jar", "target/java-executor-1.0.0.jar"]
