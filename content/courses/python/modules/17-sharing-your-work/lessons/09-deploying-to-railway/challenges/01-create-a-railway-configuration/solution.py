# railway.toml

[build]
builder = "dockerfile"
dockerfilePath = "./Dockerfile"

[deploy]
replicas = 2

healthcheckPath = "/api/health"
healthcheckTimeout = 300

restartPolicyType = "on_failure"
restartPolicyMaxRetries = 5