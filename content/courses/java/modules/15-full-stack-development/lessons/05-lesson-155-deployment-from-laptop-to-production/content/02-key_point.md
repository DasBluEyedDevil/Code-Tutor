---
type: "KEY_POINT"
title: "Deployment is Like Opening a Restaurant"
---

DEVELOPMENT (Test kitchen at home):
You're experimenting with recipes in your home kitchen
Like: Running on localhost, making changes

BUILD (Prepare for opening):
Package all your recipes, ingredients, equipment
Like: mvn package creates myapp.jar

TEST ENVIRONMENT (Soft opening):
Invite friends and family to test the restaurant
Like: Deploy to staging server, run tests

PRODUCTION (Grand opening):
Real customers, real money, real reviews
Like: Deploy to production server, monitor everything

CONFIGURATION:
Home kitchen uses different equipment than restaurant
Like: Dev uses localhost, prod uses real database URL
Solution: Environment variables (swap out settings)