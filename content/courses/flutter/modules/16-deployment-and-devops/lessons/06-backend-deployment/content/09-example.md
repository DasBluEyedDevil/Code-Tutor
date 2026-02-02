---
type: "EXAMPLE"
title: "SSL and Health Check Implementation"
---


Implementing health checks and SSL configuration for production:



```dart
## Health Check Endpoint Implementation

// lib/src/endpoints/health_endpoint.dart
import 'package:serverpod/serverpod.dart';

class HealthEndpoint extends Endpoint {
  /// Basic liveness check - is the server running?
  Future<Map<String, dynamic>> liveness(Session session) async {
    return {
      'status': 'ok',
      'timestamp': DateTime.now().toIso8601String(),
    };
  }

  /// Readiness check - can we serve requests?
  Future<Map<String, dynamic>> readiness(Session session) async {
    final checks = <String, dynamic>{};
    var allHealthy = true;

    // Check database
    try {
      await session.db.query('SELECT 1');
      checks['database'] = 'healthy';
    } catch (e) {
      checks['database'] = 'unhealthy: ${e.toString()}';
      allHealthy = false;
    }

    // Check Redis if configured
    try {
      final redis = session.serverpod.redisController;
      if (redis != null) {
        await redis.ping();
        checks['redis'] = 'healthy';
      }
    } catch (e) {
      checks['redis'] = 'unhealthy: ${e.toString()}';
      allHealthy = false;
    }

    return {
      'status': allHealthy ? 'ready' : 'not_ready',
      'checks': checks,
      'timestamp': DateTime.now().toIso8601String(),
    };
  }
}

---

## Route Configuration for Health Checks

// lib/server.dart
void run(List<String> args) async {
  final pod = Serverpod(
    args,
    Protocol(),
    Endpoints(),
    authenticationHandler: authHandler,
  );

  // Add custom routes for health checks
  pod.webServer.addRoute(
    RouteRoot(
      path: '/health',
      handler: (request) async {
        return Response.ok({'status': 'ok'});
      },
    ),
  );

  pod.webServer.addRoute(
    RouteRoot(
      path: '/ready',
      handler: (request) async {
        try {
          // Quick DB check
          await pod.db.query('SELECT 1');
          return Response.ok({'status': 'ready'});
        } catch (e) {
          return Response.internalServerError(
            body: {'status': 'not_ready', 'error': e.toString()},
          );
        }
      },
    ),
  );

  await pod.start();
}

---

## Nginx Reverse Proxy with SSL

# nginx.conf (if self-hosting)
server {
    listen 80;
    server_name api.myapp.com;
    return 301 https://$server_name$request_uri;
}

server {
    listen 443 ssl http2;
    server_name api.myapp.com;

    ssl_certificate /etc/letsencrypt/live/api.myapp.com/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/api.myapp.com/privkey.pem;
    ssl_protocols TLSv1.2 TLSv1.3;
    ssl_ciphers ECDHE-ECDSA-AES128-GCM-SHA256:ECDHE-RSA-AES128-GCM-SHA256;
    ssl_prefer_server_ciphers off;

    # HSTS
    add_header Strict-Transport-Security "max-age=63072000" always;

    # Security headers
    add_header X-Frame-Options DENY;
    add_header X-Content-Type-Options nosniff;
    add_header X-XSS-Protection "1; mode=block";

    location / {
        proxy_pass http://localhost:8080;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection "upgrade";
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_read_timeout 86400;
    }

    location /health {
        proxy_pass http://localhost:8080/health;
        access_log off;
    }
}

---

## Flutter Client Configuration for HTTPS

// In your Flutter app
final client = Client(
  'https://api.myapp.com/',
  authenticationKeyManager: FlutterAuthenticationKeyManager(),
)..connectivityMonitor = FlutterConnectivityMonitor();

// WebSocket connection (uses wss:// automatically with https)
await client.openStreamingConnection();

// For development with self-signed certs (NOT for production)
// In main.dart, before creating client:
if (kDebugMode) {
  HttpOverrides.global = DevHttpOverrides();
}

class DevHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    return super.createHttpClient(context)
      ..badCertificateCallback = (cert, host, port) => true;
  }
}
```
