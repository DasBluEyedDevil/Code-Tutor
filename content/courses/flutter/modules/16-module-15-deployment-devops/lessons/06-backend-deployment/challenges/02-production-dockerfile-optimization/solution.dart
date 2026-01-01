# Stage 1: Build
FROM dart:stable AS build

WORKDIR /app
COPY pubspec.yaml pubspec.lock ./
RUN dart pub get

COPY . .
RUN dart compile exe bin/main.dart -o bin/server

# Stage 2: Production
FROM alpine:3.19 AS production

# Install required libraries
RUN apk add --no-cache libc6-compat ca-certificates wget

# Create non-root user
RUN addgroup -S appgroup && adduser -S appuser -G appgroup

WORKDIR /app

# Copy necessary files from build stage
COPY --from=build /app/bin/server /app/
COPY --from=build /app/config/ /app/config/

# Set ownership and switch to non-root user
RUN chown -R appuser:appgroup /app
USER appuser

# Set environment variables
ENV SERVERPOD_ENV=production

EXPOSE 8080

# Add health check
HEALTHCHECK --interval=30s --timeout=10s --retries=3 \
    CMD wget --no-verbose --tries=1 --spider http://localhost:8080/health || exit 1

CMD ["./server", "--mode", "production"]