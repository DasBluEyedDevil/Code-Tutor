---
type: "EXAMPLE"
title: "Building and Running the Docker Image"
---

**Common Docker commands for your project:**

```bash
# Build the Docker image
# -t tags the image with a name:version
# . is the build context (current directory)
docker build -t finance-tracker:latest .

# List all images
docker images

# Run the container
# -p maps host port 8000 to container port 8000
# -d runs in detached mode (background)
docker run -p 8000:8000 -d finance-tracker:latest

# List running containers
docker ps

# View container logs
docker logs <container_id>

# Stop the container
docker stop <container_id>

# Remove the container
docker rm <container_id>

# Remove the image
docker rmi finance-tracker:latest

# Build with no cache (full rebuild)
docker build --no-cache -t finance-tracker:latest .

# Check image size
docker images finance-tracker
# Expected output:
# REPOSITORY        TAG       IMAGE ID       CREATED        SIZE
# finance-tracker   latest    abc123def     1 minute ago   180MB
```
