# Stage 1: Build
FROM dart:stable AS build

WORKDIR /app
COPY pubspec.yaml pubspec.lock ./
RUN dart pub get

COPY . .
RUN dart compile exe bin/main.dart -o bin/server

# Stage 2: Production
FROM alpine:3.19 AS production

# TODO: Install required libraries
RUN apk add --no-cache ___

# TODO: Create non-root user
RUN addgroup -S ___ && adduser -S ___ -G ___

WORKDIR /app

# TODO: Copy necessary files from build stage
COPY --from=build /app/bin/server /app/
COPY --from=build /app/___/ /app/___/

# TODO: Set ownership and switch to non-root user
RUN chown -R ___:___ /app
USER ___

# TODO: Set environment variables
ENV SERVERPOD_ENV=___

EXPOSE 8080

# TODO: Add health check
HEALTHCHECK --interval=30s --timeout=10s --retries=3 \
    CMD ___

CMD ["./server", "--mode", "production"]