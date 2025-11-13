FROM dart:stable

WORKDIR /app

# Copy pubspec files
COPY pubspec.yaml ./

# Get dependencies
RUN dart pub get

# Copy source code
COPY . .

# Create a non-root user
RUN useradd -m -u 1000 executor && \
    mkdir -p /tmp/dart-execution && \
    chown -R executor:executor /app /tmp/dart-execution

USER executor

EXPOSE 4007

CMD ["dart", "run", "bin/server.dart"]
