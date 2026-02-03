---
type: "WARNING"
title: "Common Pitfalls"
---

## Final Deployment Pitfalls

**Deploying Without Monitoring**: A successful deployment means nothing if you cannot tell whether the application is healthy after launch. Set up health check endpoints, configure Application Insights or a similar monitoring service, and verify you receive alerts for errors and performance degradation before considering the deployment complete.

**Forgetting HTTPS in Production**: Serving your application over HTTP in production exposes authentication tokens, cookies, and user data to network interception. Configure HTTPS redirect middleware, enforce HSTS headers, and verify your TLS certificate is valid and auto-renewing.

**No Rollback Strategy**: If your deployment introduces a critical bug, you need to restore the previous version within minutes, not hours. Tag every deployment with a version, keep the previous Docker image available, and document the steps to roll back. Practice rollbacks before you need them in an emergency.

**Ignoring Logs After Deployment**: The first hours after a production deployment are critical. Monitor logs for unexpected exceptions, slow queries, and authentication failures. Many issues only appear under real traffic patterns that staging environments cannot replicate. Stay alert and be ready to respond.

**Not Celebrating the Launch**: Building and deploying a full-stack application from scratch is a significant achievement. Take a moment to appreciate what you have built -- from C# fundamentals through Clean Architecture, authentication, CI/CD, and production deployment. This is real engineering work.
