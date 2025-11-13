FROM rust:1.75-slim

WORKDIR /app

# Install dependencies
RUN apt-get update && \
    apt-get install -y pkg-config libssl-dev && \
    rm -rf /var/lib/apt/lists/*

# Copy Cargo files
COPY Cargo.toml Cargo.lock* ./

# Create a dummy main.rs to cache dependencies
RUN mkdir src && \
    echo "fn main() {}" > src/main.rs && \
    cargo build --release && \
    rm -rf src

# Copy actual source code
COPY src ./src

# Build the executor
RUN cargo build --release

# Create a non-root user
RUN useradd -m -u 1000 executor && \
    mkdir -p /tmp/rust-execution && \
    chown -R executor:executor /app /tmp/rust-execution

USER executor

EXPOSE 4003

CMD ["./target/release/rust-executor"]
