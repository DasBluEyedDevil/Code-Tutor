---
type: "THEORY"
title: "Sessions in Distributed Systems"
---

Session storage becomes complex with multiple servers:

PROBLEM:
Server A creates session, stores in memory.
Load balancer sends next request to Server B.
Server B doesn't have that session - user logged out!

SOLUTIONS:

1. STICKY SESSIONS:
Load balancer always sends user to same server.
Problem: Server dies, sessions lost.

2. SESSION REPLICATION:
Servers sync sessions with each other.
Problem: Network overhead, complexity.

3. CENTRALIZED SESSION STORE:
All servers use shared store (Redis, database).
Best for most applications.

Spring Session with Redis:

<dependency>
    <groupId>org.springframework.session</groupId>
    <artifactId>spring-session-data-redis</artifactId>
</dependency>

application.properties:
spring.session.store-type=redis
spring.redis.host=localhost
spring.redis.port=6379

Now sessions survive server restarts and work across instances.

4. STATELESS (JWT):
No server-side session at all.
Covered in next lesson!