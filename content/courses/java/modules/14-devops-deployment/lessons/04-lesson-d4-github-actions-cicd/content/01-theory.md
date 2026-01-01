---
type: "THEORY"
title: "What is GitHub Actions?"
---

GitHub Actions is a CI/CD platform built into GitHub. It runs automated workflows in response to events:

EVENTS THAT TRIGGER WORKFLOWS:
- push: Code pushed to repository
- pull_request: PR opened, updated, or merged
- schedule: Cron-based scheduling
- workflow_dispatch: Manual trigger
- release: New release published

WHAT CAN WORKFLOWS DO?
- Build and test your code
- Run linters and security scanners
- Build Docker images
- Deploy to cloud platforms
- Send notifications
- Almost anything you can script!

KEY CONCEPTS:

Workflow: A configurable automated process
  - Defined in .github/workflows/*.yml
  - Triggered by events
  - Contains one or more jobs

Job: A set of steps that execute on the same runner
  - Runs on a virtual machine
  - Jobs run in parallel by default
  - Can depend on other jobs

Step: An individual task
  - Runs a command or action
  - Steps run sequentially
  - Share data within a job

Action: A reusable unit of code
  - Community-maintained (actions/checkout, actions/setup-java)
  - Or your own custom actions