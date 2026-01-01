---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Never store plain passwords** - Always hash with salt (use passlib in production)
- **Use OAuth2PasswordBearer** - FastAPI's built-in security for token extraction
- **python-jose for JWT** - `pip install python-jose[cryptography]` for token encoding/decoding
- **Dependency injection for auth** - Use Depends(get_current_user) for protected routes
- **Role-based access** - Create dependency factories like require_role('admin')
- **Pydantic for validation** - EmailStr, Field() constraints handle input validation
- **Rate limiting as dependency** - Implement as async dependency function
- **CORSMiddleware** - FastAPI's built-in CORS handling
- **HTTPS in production** - Encrypt data in transit
- **Swagger UI auth** - FastAPI automatically adds Authorize button for OAuth2