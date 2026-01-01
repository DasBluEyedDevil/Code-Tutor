---
type: "EXAMPLE"
title: "Building and Running"
---

Commands to build and run your Docker container:

```bash
# Build the image
docker build -t myapp:latest .

# Explanation:
# docker build     - build an image
# -t myapp:latest  - tag it as 'myapp' with 'latest' version
# .                - use Dockerfile in current directory

# List images
docker images
# REPOSITORY    TAG       IMAGE ID       SIZE
# myapp         latest    abc123def      180MB

# Run the container
docker run -d -p 8080:8080 --name myapp-container myapp:latest

# Explanation:
# docker run                 - run a container
# -d                        - detached mode (background)
# -p 8080:8080              - map host port 8080 to container port 8080
# --name myapp-container    - name the container
# myapp:latest              - image to run

# View running containers
docker ps
# CONTAINER ID   IMAGE           STATUS         PORTS
# def456abc      myapp:latest    Up 5 seconds   0.0.0.0:8080->8080/tcp

# View logs
docker logs myapp-container
docker logs -f myapp-container  # Follow mode (like tail -f)

# Stop the container
docker stop myapp-container

# Remove the container
docker rm myapp-container

# Remove the image
docker rmi myapp:latest
```
