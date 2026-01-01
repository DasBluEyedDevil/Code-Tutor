---
type: "THEORY"
title: "The Complete Picture: Database → Backend → Frontend"
---

Let's build a complete "User List" feature:

1. DATABASE (MySQL/PostgreSQL):
CREATE TABLE users (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(255) UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

2. BACKEND (Spring Boot):
Entity, Repository, Service, Controller

3. FRONTEND (HTML + JavaScript):
Display users, add new user form