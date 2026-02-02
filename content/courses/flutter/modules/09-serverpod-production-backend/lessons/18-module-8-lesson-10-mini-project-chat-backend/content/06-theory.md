---
type: "THEORY"
title: "Step 4: Understanding the Endpoint Architecture"
---

With our models defined, let us plan the endpoint structure. Good endpoint design follows these principles:

**Single Responsibility:**
Each endpoint class handles one domain:
- UserEndpoint: User profiles and authentication
- ChatRoomEndpoint: Room management
- ChatMessageEndpoint: Message CRUD via HTTP
- ChatStreamEndpoint: Real-time streaming

**HTTP vs Streaming:**

Use HTTP endpoints for:
- User registration and profile updates
- Creating and managing chat rooms
- Fetching message history (paginated)
- Uploading file attachments

Use Streaming endpoints for:
- Real-time message delivery
- Typing indicators
- Online/offline presence
- Room membership changes

**Authentication:**

All endpoints require authentication except:
- User registration
- Email verification
- Password reset

We will use Serverpod's built-in authentication and extend it with our ChatUser profiles.

**Error Handling:**

Consistent error responses:
- 400: Bad request (invalid input)
- 401: Unauthorized (not logged in)
- 403: Forbidden (no permission)
- 404: Not found (room/message does not exist)
- 500: Server error

