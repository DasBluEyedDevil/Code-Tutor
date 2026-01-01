---
type: "THEORY"
title: "Step 1: Database Schema"
---

Create the tasks table:

CREATE TABLE tasks (
    id BIGSERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    completed BOOLEAN DEFAULT FALSE,
    user_id BIGINT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

Key decisions:
- BIGSERIAL: Auto-incrementing ID
- VARCHAR(255): Short text for title
- TEXT: Longer text for description
- BOOLEAN: True/false for completed status
- TIMESTAMP: When created/updated
- FOREIGN KEY: Links to users table
- ON DELETE CASCADE: Delete tasks when user deleted

Spring Boot can create this automatically:

application.yml:
spring:
  jpa:
    hibernate:
      ddl-auto: update  # Creates/updates tables automatically