---
type: "EXAMPLE"
title: "Generated Migration Example"
---


**Understanding Generated SQL**

Here's what Serverpod generates for our models:



```sql
-- migrations/[timestamp]/definition.sql

-- User Profiles table
CREATE TABLE "user_profiles" (
  "id" bigserial PRIMARY KEY,
  "userInfoId" bigint NOT NULL,
  "username" text NOT NULL,
  "displayName" text NOT NULL,
  "email" text NOT NULL,
  "avatarUrl" text,
  "bio" text,
  "isOnline" boolean NOT NULL DEFAULT false,
  "lastSeenAt" timestamp without time zone,
  "isVerified" boolean NOT NULL DEFAULT false,
  "isDeleted" boolean NOT NULL DEFAULT false,
  "createdAt" timestamp without time zone NOT NULL,
  "updatedAt" timestamp without time zone
);

-- Indexes for user_profiles
CREATE UNIQUE INDEX "user_profile_username_idx" ON "user_profiles" ("username");
CREATE UNIQUE INDEX "user_profile_email_idx" ON "user_profiles" ("email");
CREATE UNIQUE INDEX "user_profile_user_info_idx" ON "user_profiles" ("userInfoId");

-- Conversations table
CREATE TABLE "conversations" (
  "id" bigserial PRIMARY KEY,
  "conversationType" integer NOT NULL,
  "name" text,
  "description" text,
  "avatarUrl" text,
  "createdById" bigint,
  "lastMessageId" bigint,
  "lastMessageAt" timestamp without time zone,
  "lastMessagePreview" text,
  "isArchived" boolean NOT NULL DEFAULT false,
  "isDeleted" boolean NOT NULL DEFAULT false,
  "createdAt" timestamp without time zone NOT NULL,
  "updatedAt" timestamp without time zone
);

-- Messages table
CREATE TABLE "messages" (
  "id" bigserial PRIMARY KEY,
  "conversationId" bigint NOT NULL,
  "senderId" bigint NOT NULL,
  "content" text NOT NULL,
  "messageType" integer NOT NULL,
  "mediaUrls" json,
  "thumbnailUrl" text,
  "fileName" text,
  "fileSize" integer,
  "replyToId" bigint,
  "isEdited" boolean NOT NULL DEFAULT false,
  "isDeleted" boolean NOT NULL DEFAULT false,
  "deletedAt" timestamp without time zone,
  "createdAt" timestamp without time zone NOT NULL,
  "updatedAt" timestamp without time zone
);

-- Indexes for messages
CREATE INDEX "message_conversation_idx" ON "messages" ("conversationId", "createdAt");
CREATE INDEX "message_sender_idx" ON "messages" ("senderId");

-- Foreign key constraints
ALTER TABLE "messages" 
  ADD CONSTRAINT "messages_fk_conversation" 
  FOREIGN KEY ("conversationId") 
  REFERENCES "conversations"("id") 
  ON DELETE CASCADE;

ALTER TABLE "messages" 
  ADD CONSTRAINT "messages_fk_sender" 
  FOREIGN KEY ("senderId") 
  REFERENCES "user_profiles"("id") 
  ON DELETE SET NULL;

-- Participants table
CREATE TABLE "participants" (
  "id" bigserial PRIMARY KEY,
  "conversationId" bigint NOT NULL,
  "userId" bigint NOT NULL,
  "role" integer NOT NULL DEFAULT 2,
  "lastReadAt" timestamp without time zone,
  "lastReadMessageId" bigint,
  "unreadCount" integer NOT NULL DEFAULT 0,
  "isMuted" boolean NOT NULL DEFAULT false,
  "mutedUntil" timestamp without time zone,
  "hasLeft" boolean NOT NULL DEFAULT false,
  "leftAt" timestamp without time zone,
  "joinedAt" timestamp without time zone NOT NULL
);

-- Unique constraint for participants
CREATE UNIQUE INDEX "participant_unique_idx" 
  ON "participants" ("conversationId", "userId");
```
